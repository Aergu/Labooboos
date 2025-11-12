using UnityEngine;
using UnityEngine.InputSystem;
public class TapMonsterManager : MonoBehaviour
{
   [Header("Layer for monsters")] public LayerMask monsterLayer;
   public Camera arCamera;

   private void Update()
   {
       if (Touchscreen.current.primaryTouch.press.isPressed)
       {
           Vector2 touchPos = Touchscreen.current.primaryTouch
               .position.ReadValue();
           Debug.Log("Touch detected at: " + touchPos);
           HandleTap(touchPos);
       }

       if (Mouse.current.leftButton.wasPressedThisFrame)
       {
           Vector2 mousePos = Mouse.current.position.ReadValue();
           Debug.Log("Mouse click at: " + mousePos);
           HandleTap(mousePos);
       }
   }

  private void HandleTap(Vector2 screenPos)
   {
       Ray ray = arCamera.ScreenPointToRay(screenPos);
       Debug.DrawRay(ray.origin, ray.direction * 10f, Color.red, 2f);
       Debug.Log("Ray origin: " + ray.origin + ", direction: " + ray.direction);

       if (Physics.Raycast(ray, out RaycastHit hit, 500f, monsterLayer, QueryTriggerInteraction.Collide))
       {
           Debug.Log("Hit: " + hit.collider.name);
           Destroy(hit.collider.gameObject);
       }
   }
}

