using UnityEngine;

public class Saucer : MonoBehaviour
{
    [SerializeField] private ParticleSystem _destroyedParticles;
    [SerializeField] private int _hitPoints = 3;
    [SerializeField] private float _speed = 2f;

    private SaucerFactory _saucerFactory;
    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _saucerFactory.saucerCount++;
    }

    private void Update()
    {
        Transform enemy = GameObject.FindGameObjectWithTag("Player").transform;
        Vector2 direction = (enemy.position - transform.position).normalized;
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
                _saucerFactory.saucerCount--;
                Instantiate(_destroyedParticles, transform.position, Quaternion.identity);
                _saucerFactory.destroyedSaucers++;
                Destroy(gameObject);
            }
        }

        if (collision.TryGetComponent(out Laser laser))
        {
            Destroy(collision.gameObject);
            _saucerFactory.saucerCount--;
            Instantiate(_destroyedParticles, transform.position, Quaternion.identity);
            _saucerFactory.destroyedSaucers++;
            Destroy(gameObject);
        }
    }
}