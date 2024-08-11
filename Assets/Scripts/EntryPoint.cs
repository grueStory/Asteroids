using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private Ship _ship;
    [SerializeField] private AsteroidFactory _asteroidFactory;
    [SerializeField] private SaucerFactory _saucerFactory;
    [SerializeField] private ShipController _shipController;
    [SerializeField] private ScoreUI _scoreUI;
    
    private void Start()
    {
        Score score = new();
        GameInput gameInput = new();
        _shipController.Construct(_ship, gameInput);
        _scoreUI.Construct(score);
    }
}