using UnityEngine;

public class Monster : MonoBehaviour
{
    void OnMouseDown()
    {
        Collect();
    }

    void Collect()
    {
        Debug.Log("Monster collected!");
        // Add inventory logic or animation here
        Destroy(gameObject);
    }
}