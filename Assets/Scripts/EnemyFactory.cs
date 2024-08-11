using UnityEngine;

public abstract class EnemyFactory<T> : MonoBehaviour
    where T : MonoBehaviour, IEnemy
{
    [SerializeField] private T _prefab;
    private int _count = 0;
    protected int Level;
    private Score _score;
    
    public void Construct(Score score)
    {
        _score = score;
    }

    private void Update()
    { 
        if (_count == 0)
        {
            Level++;

            int count = CalculateEnemiesCount();
            
            for (int i = 0; i < count; i++)
            {
                Spawn();
            }
        }
    }

    protected abstract int CalculateEnemiesCount();

    private void Spawn()
    {
        _count++;
        float offset = Random.Range(0f, 1f);
        Vector2 viewportSpawnPosition = Vector2.zero;
        int edge = Random.Range(0, 4);
        
        if (edge == 0)
        {
            viewportSpawnPosition = new Vector2(offset, 0);
        }
        else if (edge == 1)
        {
            viewportSpawnPosition = new Vector2(offset, 1);
        }
        else if (edge == 2)
        {
            viewportSpawnPosition = new Vector2(0, offset);
        }
        else if (edge == 3)
        {
            viewportSpawnPosition = new Vector2(1, offset);
        }

        Vector2 worldSpawnPosition = Camera.main.ViewportToWorldPoint(viewportSpawnPosition);
        T saucer = Instantiate(_prefab, worldSpawnPosition, Quaternion.identity);
        saucer.Destroyed += UpdateScore;
        saucer.Destroyed += OnEnemyDestroyed;
    }

    private void OnEnemyDestroyed(IEnemy enemy)
    {
        enemy.Destroyed -= OnEnemyDestroyed;
        _count--;
    }
    
    private void UpdateScore(IEnemy enemy)
    {
        enemy.Destroyed -= UpdateScore;
        _score.AddSaucerScore();
    }
}