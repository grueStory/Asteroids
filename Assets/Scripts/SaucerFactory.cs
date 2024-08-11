public class SaucerFactory : EnemyFactory<Saucer>
{
    protected override int CalculateEnemiesCount()
    {
        return Level++;
    }
}