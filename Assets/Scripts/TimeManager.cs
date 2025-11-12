using UnityEngine;
using System;

public class TimeManager : MonoBehaviour
{ 
    [Header("Assign your 3D prefabs in the scene")]
    public GameObject[] monsters;
    
   public int nightStartHour;
   public int nightEndHour;
   
   [Header ("Debug")]
   public bool forceNight = false;
   

   private void Start()
   {
       UpdateVisibility();
       InvokeRepeating(nameof(UpdateVisibility), 0, 10f);
   }

   private void UpdateVisibility()
   {
       int hour = DateTime.Now.Hour;
       bool isNight = forceNight || (nightStartHour > nightEndHour
               ? (hour >= nightStartHour || hour < nightEndHour)
               : (hour >= nightStartHour && hour < nightEndHour)
           );

       foreach (GameObject monster in monsters)
       {
           if(monster != null)
               monster.SetActive(isNight);
           
       }
   }

   public bool IsNightTime()
   {
       int hour = DateTime.Now.Hour;
       return forceNight || (
           nightStartHour > nightEndHour
               ? (hour >= nightStartHour || hour < nightEndHour)
               : (hour >= nightStartHour && hour < nightEndHour));
   }
}