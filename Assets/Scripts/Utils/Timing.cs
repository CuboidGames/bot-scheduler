namespace BotScheduler.Utils
{
  public class Timing
  {
    public static float EaseInOut(float t)
    {
      float sqt = t * t;

      return sqt / (2.0f * (sqt - t) + 1.0f);
    }
  }
}