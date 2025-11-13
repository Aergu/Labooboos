using UnityEngine;
using UnityEngine.InputSystem;

public class TapMonsterManager : MonoBehaviour
{
   [Header("Layer for monsters")] public LayerMask monsterLayer;
   public Camera arCamera;
   public int pointsPerMonster;
   public ScoreManager scoreManager;

   private void Update()
   {
       if (Touchscreen.current != null)
       {
           var touch = Touchscreen.current.primaryTouch;
           if (touch.press.wasReleasedThisFrame)
           {
               HandleTap(touch.position.ReadValue());
           }
       }
       else if (Mouse.current != null &&
                Mouse.current.leftButton.wasReleasedThisFrame)
       {
           HandleTap(Mouse.current.position.ReadValue());
       }
   }

  private void HandleTap(Vector2 screenPos)
   {
       if (arCamera == null)
       {
           Debug.LogWarning("AR Camera not assigned!");
           return;
       }
       
       Ray ray = arCamera.ScreenPointToRay(screenPos);
       Debug.DrawRay(ray.origin, ray.direction * 10f, Color.red, 2f);

       if (Physics.Raycast(ray, out RaycastHit hit, 500f, monsterLayer))
       {
           Debug.Log("Hit: " + hit.collider.name);
           Destroy(hit.collider.gameObject);

           if (scoreManager != null)
               scoreManager.AddScore(pointsPerMonster);
           else
               Debug.LogWarning("Score Manager isn't assigned!");
       }
       else
       {
           Debug.Log("Ray missed everything.");
       }
   }
}

