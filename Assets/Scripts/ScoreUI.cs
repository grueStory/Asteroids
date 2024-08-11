using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private Text _scoreText;

    private Score _score;

    public void Construct(Score score)
    {
        _score = score;
    }

    private void Start()
    {
        _score.Updated += Draw;
    }

    private void OnDestroy()
    {
        _score.Updated -= Draw;
    }

    private void Draw() => _scoreText.text = $"Score: {_score.Value}";
    
}