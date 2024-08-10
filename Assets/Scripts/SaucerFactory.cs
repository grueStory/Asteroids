using UnityEngine;

public class SaucerFactory : MonoBehaviour
{
    [SerializeField] private Saucer saucerPrefab;
    public int saucerCount = 0;
    public int destroyedSaucers = 0;
    public int level;

    private void Update()
    {
        if (saucerCount == 0)
        {
            level++;

            int numSaucer = level;
            for (int i = 0; i < numSaucer; i++)
            {
                SpawnSaucer();
            }
        }
    }

    private void SpawnSaucer()
    {
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
        Saucer saucer = Instantiate(saucerPrefab, worldSpawnPosition, Quaternion.identity);
        saucer.gameManager = this;
    }
}