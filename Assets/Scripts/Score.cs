using System;

public class Score
{
    public event Action Updated;
    
    public int Value { get; private set; }

    public void AddAsteroidScore()
    {
        AddScore(10);
    }
    
    public void AddSaucerScore()
    {
        AddScore(20);
    }

    private void AddScore(int value)
    {
        Value += value;
        Updated?.Invoke();
    }
}