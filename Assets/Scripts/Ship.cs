using System;
using System.Collections;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float shipAcceleration = 10f;
    [SerializeField] private float shipMaxVelocity = 10f;
    [SerializeField] private float shipRotationSpeed = 180f;
    
    [Header("Weapon Settings")]
    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private ParticleSystem destroyedParticles;
    
    [Header("Weapon Settings - Bullet")]
    [SerializeField] private Rigidbody2D bulletPrefab;
    [SerializeField] private float bulletSpeed = 8f;
    
    [Header("Weapon Settings - Laser")]
    [SerializeField] private Rigidbody2D laserPrefab;
    [SerializeField] private float laserSpeed = 12f;
    [SerializeField] private int laserChargeFixed = 3;
    [SerializeField] private float laserCooldownFixed = 10f;
    
    public int laserChargeCurent;
    public float laserCooldownCurent;

    private Rigidbody2D _shipRigidbody;
    public bool isAlive = true;
    public bool isAccelerating = false;
    private ShipUI _shipUI;

    public event Action OnDied;

    
    private void Start()
    {
        _shipRigidbody = GetComponent<Rigidbody2D>();
        _shipUI = FindObjectOfType<ShipUI>();
        laserChargeCurent = laserChargeFixed;
        laserCooldownCurent = laserCooldownFixed;
    }

    private void Update()
    {
        if (isAlive)
        {
            _shipUI.UpdateUI();

            if (laserChargeCurent == 0)
            {
                StartCoroutine(LaserCooldown());
                _shipUI.UpdateLaserCooldown(laserChargeCurent);
                laserCooldownCurent -= Time.deltaTime;
            }
        }
    }

    private void FixedUpdate()
    {
        if (isAlive && isAccelerating)
        {
            _shipRigidbody.AddForce(shipAcceleration * transform.up);
            _shipRigidbody.velocity = Vector2.ClampMagnitude(_shipRigidbody.velocity, shipMaxVelocity);
        }
    }

    public void ShootBullet()
    {
        Rigidbody2D bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
        Vector2 shipVelocity = _shipRigidbody.velocity;
        Vector2 shipDirection = transform.up;
        float shipForwardSpeed = Vector2.Dot(shipVelocity, shipDirection);
            
        if (shipForwardSpeed < 0) 
        { 
            shipForwardSpeed = 0; 
        }

        bullet.velocity = shipDirection * shipForwardSpeed;
        bullet.AddForce(bulletSpeed * transform.up, ForceMode2D.Impulse);
    }
    
    public void ShootLaser()
    {
        
        Rigidbody2D laser = Instantiate(laserPrefab, bulletSpawn.position, Quaternion.identity);
        Vector2 shipVelocity = _shipRigidbody.velocity;
        Vector2 shipDirection = transform.up;
        float shipForwardSpeed = Vector2.Dot(shipVelocity, shipDirection);
            
        if (shipForwardSpeed < 0) 
        { 
            shipForwardSpeed = 0; 
        }

        laser.transform.rotation = transform.rotation;
        laser.velocity = shipDirection * shipForwardSpeed;
        laser.AddForce(laserSpeed * transform.up, ForceMode2D.Impulse);
        laserChargeCurent--;
    }

    private IEnumerator LaserCooldown()
    {
        yield return new WaitForSeconds(laserCooldownCurent);
        laserChargeCurent = laserChargeFixed;
        laserCooldownCurent = laserCooldownFixed;
        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Asteroid asteroid) || collision.TryGetComponent(out Saucer saucer))
        {
            isAlive = false;
            Instantiate(destroyedParticles, transform.position, Quaternion.identity);
            OnDied?.Invoke();
            Destroy(gameObject);
        }
    }
}

public class GameOverController : IDisposable
{
    private readonly Ship _ship;
    private readonly GameOverUI _gameOverUI;

    public GameOverController(Ship ship, GameOverUI gameOverUI)
    {
        this._ship = ship;
        this._gameOverUI = gameOverUI;
    }

    public void Initialize()
    {
        _ship.OnDied += GameOver;
    }

    private void GameOver()
    {
        _gameOverUI.GameOver();
    }

    public void Dispose()
    {
        _ship.OnDied -= GameOver;
    }
}
