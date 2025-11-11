using UnityEngine;
using System;

public class TimeManager : MonoBehaviour
{
   public int nightStartHour;
   public int nightEndHour;

   public bool IsNightTime()
   {
      int hour = DateTime.Now.Hour;

      return (nightStartHour > nightEndHour)
         ? (hour >= nightStartHour || hour < nightEndHour)
         : (hour >= nightStartHour && hour < nightEndHour);
   }
}