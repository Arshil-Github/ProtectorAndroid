using UnityEngine;

public class OtherFeatures : MonoBehaviour
{
    public SpriteRenderer playerRenderer;
    void Update()
    {

        if(GameObject.Find("GlobalObject") != null)
        {
            if (GlobalObject.Instance.playerSprite != null)
            {
                playerRenderer.sprite = GlobalObject.Instance.playerSprite;
            }
            if (GlobalObject.Instance.playerColor != null)
            {
                playerRenderer.color = GlobalObject.Instance.playerColor;
            }
        }
    }
}
