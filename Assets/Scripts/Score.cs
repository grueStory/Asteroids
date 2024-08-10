using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text score;

    public Score(int destroyedAsteroids, int destroyedSaucers)
    {
        score.text = "Score: " + (destroyedAsteroids * 10) + (destroyedSaucers * 20);
    }
}