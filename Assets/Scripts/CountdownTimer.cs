public class CountdownTimer
{
    public bool enabled { get; set; }
    double currentTime;

    public CountdownTimer(double value)
    {
        this.enabled = false;
        currentTime = value;
    }

    public void SetStartTime(double seconds)
    {
        currentTime = seconds;
    }

    public double GetTime()
    {
        return currentTime;
    }

    public void UpdateTime(float delta)
    {
        if (currentTime > 0)
            currentTime -= delta;
        else
        {
            enabled = false;
            currentTime = 0;
        }
    }
}
