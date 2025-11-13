using UnityEngine;
using UnityEngine.UI;

public class ConvertTexture2DtoSprite : MonoBehaviour
{
    public Texture2D myTexture;  
    public Image uiImage;        

    void Start()
    {
        if (myTexture != null && uiImage != null)
        {
        
            Sprite sprite = Sprite.Create(
                myTexture,
                new Rect(0, 0, myTexture.width, myTexture.height),
                new Vector2(0.5f, 0.5f)  
            );

           
            uiImage.sprite = sprite;
        }
    }
}
