using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private ParticleSystem _destroyedParticles;
    [SerializeField] private int _size = 3;

    private AsteroidFactory _asteroidFactory;

    private void Start()
    {
        transform.localScale = 0.5f * _size * Vector3.one;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector2 direction = new Vector2(Random.value, Random.value).normalized;
        float spawnSpeed = Random.Range(4f - _size, 5f - _size);
        rb.AddForce(direction * spawnSpeed, ForceMode2D.Impulse);
        _asteroidFactory.asteroidCount++;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Bullet bullet))
        {
            _asteroidFactory.asteroidCount--;
            Destroy(collision.gameObject);

            if (_size > 1)
            {
                for (int i = 0; i < 2; i++)
                {
                    Asteroid newAsteroid = Instantiate(this, transform.position, Quaternion.identity);
                    newAsteroid._size = _size - 1;
                    newAsteroid._asteroidFactory = _asteroidFactory;
                }
            }

            Instantiate(_destroyedParticles, transform.position, Quaternion.identity);
            _asteroidFactory.destroyedAsteroids++;
            Destroy(gameObject);
        }

        if (collision.TryGetComponent(out Laser laser))
        {
            _asteroidFactory.asteroidCount--;
            Destroy(collision.gameObject);
            Instantiate(_destroyedParticles, transform.position, Quaternion.identity);
            _asteroidFactory.destroyedAsteroids++;
            Destroy(gameObject);
        }
    }
}