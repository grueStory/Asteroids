using System;

public class GameOverController : IDisposable
{
    private readonly Ship _ship;
    private readonly GameOverUI _gameOverUI;

    public GameOverController(Ship ship, GameOverUI gameOverUI)
    {
        this._ship = ship;
        this._gameOverUI = gameOverUI;
    }

    public void Initialize()
    {
        _ship.OnDied += GameOver;
    }

    private async void GameOver()
    {
        _gameOverUI.GameOver();
    }

    public void Dispose()
    {
        _ship.OnDied -= GameOver;
    }
}