using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [Tooltip("Start of night time (24-hour format)")]
    public int nightStart = 20;
    [Tooltip("End of night time (24-hour format)")]
    public int nightEnd = 6;

    public bool IsNightTime()
    {
        int hour = System.DateTime.Now.Hour;
        return hour >= nightStart || hour < nightEnd;
    }
}