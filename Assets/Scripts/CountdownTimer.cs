public class CountdownTimer
{
    public bool enabled { get; set; }

    //public byte number { get; set; }

    double currentTime;

    public CountdownTimer()
    {
        //this.number = num;
        this.enabled = false;
        currentTime = 0;
    }
}
