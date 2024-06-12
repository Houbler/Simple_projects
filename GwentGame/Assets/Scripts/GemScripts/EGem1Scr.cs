using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EGem1Scr : MonoBehaviour
{
    public SpriteRenderer GemSpriteRendererE1;

    public Sprite EmptyGemSprite, FullGemSprite;
    void Start()
    {
        GemSpriteRendererE1.GetComponent<SpriteRenderer>();
        if (GemSpriteRendererE1.sprite == null)
        {
            GemSpriteRendererE1.sprite = FullGemSprite;
        }
    }

    public void TurnOnGemSprite()
    {
        if (GemSpriteRendererE1.sprite == EmptyGemSprite)
        {
            GemSpriteRendererE1.sprite = FullGemSprite;
        }
    }
    public void TurnOffGemSprite()
    {
        if (GemSpriteRendererE1.sprite == FullGemSprite)
        {
            GemSpriteRendererE1.sprite = EmptyGemSprite;
        }
    }
}
