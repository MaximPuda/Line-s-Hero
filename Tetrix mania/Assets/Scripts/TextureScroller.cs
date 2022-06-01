using UnityEngine;
using UnityEngine.UI;


public class TextureScroller : MonoBehaviour
{
    [SerializeField] private RawImage image;
    [SerializeField] private float x;
    [SerializeField] private float y;

    void Update()
    {
        image.uvRect =  new Rect(image.uvRect.position + new Vector2(x, y) * Time.deltaTime, image.uvRect.size);
    }
}
