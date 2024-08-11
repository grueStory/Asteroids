public class AsteroidFactory : EnemyFactory<Asteroid>
{
    protected override int CalculateEnemiesCount()
    {
        return Level * 2 + 2;
    }
}