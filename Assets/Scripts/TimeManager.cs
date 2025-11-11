using UnityEngine;
using System;

public class TimeManager : MonoBehaviour
{
   public int nightStartHour;
   public int nightEndHour;

   public bool IsNightTime()
   {
      int hour = DateTime.Now.Hour;

      if (nightStartHour > nightEndHour)
         return hour >= nightStartHour || hour < nightEndHour;

      return hour >= nightStartHour && hour < nightEndHour;
   }
}