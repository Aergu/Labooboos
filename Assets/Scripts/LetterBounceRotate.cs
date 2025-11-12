using UnityEngine;

public class LetterBounceRotate : MonoBehaviour
{
    public float rotationSpeed = 100f;   // speed of rotation (You can edit all of these in the inspector) //Mathias
    public float bounceHeight = 0.25f;   // how high it bounces
    public float bounceSpeed = 5f;       // bounce frequency

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.localPosition;
    }

    void Update()
    {
        
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

        
        float newY = startPos.y + Mathf.Sin(Time.time * bounceSpeed) * bounceHeight;
        transform.localPosition = new Vector3(startPos.x, newY, startPos.z);
    }
}