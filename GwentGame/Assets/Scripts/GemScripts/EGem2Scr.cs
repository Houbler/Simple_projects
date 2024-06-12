using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EGem2Scr : MonoBehaviour
{
    public SpriteRenderer GemSpriteRendererE2;

    public Sprite EmptyGemSprite, FullGemSprite;
    void Start()
    {
        GemSpriteRendererE2.GetComponent<SpriteRenderer>();
        if (GemSpriteRendererE2.sprite == null)
        {
            GemSpriteRendererE2.sprite = FullGemSprite;
        }
    }

    public void TurnOnGemSprite()
    {
        if (GemSpriteRendererE2.sprite == EmptyGemSprite)
        {
            GemSpriteRendererE2.sprite = FullGemSprite;
        }
    }
    public void TurnOffGemSprite()
    {
        if (GemSpriteRendererE2.sprite == FullGemSprite)
        {
            GemSpriteRendererE2.sprite = EmptyGemSprite;
        }
    }
}
