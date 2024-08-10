using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    private void Start()
    {
        Ship ship = new Ship();
        GameInput gameInput = new GameInput();
        new PlayerController(ship, gameInput);
        AsteroidFactory asteroidFactory = new AsteroidFactory();
        SaucerFactory saucerFactory = new SaucerFactory();
        Score score = new Score(asteroidFactory.destroyedAsteroids, saucerFactory.destroyedSaucers);
        new GameOverUI();
        new ShipUI();
    }
}