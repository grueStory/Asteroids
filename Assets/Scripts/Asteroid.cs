using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Asteroid : MonoBehaviour, IEnemy
{
    public event Action<IEnemy> Destroyed;
    
    [SerializeField] private ParticleSystem _destroyedParticles;
    [SerializeField] private int _size = 3;

    private void Start()
    {
        transform.localScale = 0.5f * _size * Vector3.one;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector2 direction = (new Vector2(Random.value - 0.5f , Random.value - 0.5f) * 2f).normalized;
        float spawnSpeed = Random.Range(4f - _size, 5f - _size);
        rb.AddForce(direction * spawnSpeed, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Bullet bullet))
        {
            bullet.Destroy();

            if (_size > 1)
            {
                for (int i = 0; i < 2; i++)
                {
                    Asteroid newAsteroid = Instantiate(this, transform.position, Quaternion.identity);
                    newAsteroid._size = _size - 1;
                    //newAsteroid._asteroidFactory = _asteroidFactory;
                }
            }

            Instantiate(_destroyedParticles, transform.position, Quaternion.identity);
            Destroyed?.Invoke(this);
            Destroy(gameObject);
        }

        if (collision.TryGetComponent(out Laser laser))
        {
            Destroy(collision.gameObject);
            Instantiate(_destroyedParticles, transform.position, Quaternion.identity);
            Destroyed?.Invoke(this);
            Destroy(gameObject);
        }
    }
}