using System;
using UnityEngine;

public class Saucer : MonoBehaviour, IEnemy
{
    public event Action<IEnemy> Destroyed;
    
    [SerializeField] private ParticleSystem _destroyedParticles;
    [SerializeField] private int _hitPoints = 3;
    [SerializeField] private float _speed = 2f;
    [SerializeField] private Transform _target;
    
    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector2 direction = (_target.position - transform.position).normalized;
        _rb.velocity = direction * _speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Bullet bullet))
        {
            _hitPoints--;
            Destroy(collision.gameObject);

            if (_hitPoints <= 0)
            {
                Instantiate(_destroyedParticles, transform.position, Quaternion.identity);
                Destroyed?.Invoke(this);
                Destroy(gameObject);
            }
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