using UnityEngine;
using UnityEngine.InputSystem;
public class TapMonsterManager : MonoBehaviour
{
   [Header("Layer for monsters")] public LayerMask monsterLayer;
   public Camera arCamera;

   private void Update()
   {
       if (Touchscreen.current != null)
       {
           var touch = Touchscreen.current.primaryTouch;
           if (touch.press.wasReleasedThisFrame)
           {
               Vector2 touchPos = touch.position.ReadValue();
               HandleTap(touchPos);
           }
       }
       else if (Mouse.current != null &&
                Mouse.current.leftButton.wasReleasedThisFrame)
       {
           Vector2 mousePos = Mouse.current.position.ReadValue();
           HandleTap(mousePos);
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

       if (Physics.Raycast(ray, out RaycastHit hit, 500f, monsterLayer, QueryTriggerInteraction.Collide))
       {
           Debug.Log("Hit: " + hit.collider.name);
           Destroy(hit.collider.gameObject);
       }
       else
       {
           Debug.Log("Ray missed everything.");
       }
   }
}

