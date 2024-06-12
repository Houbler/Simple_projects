using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PGem1Scr : MonoBehaviour
{
    public SpriteRenderer GemSpriteRendererP1;

    public Sprite EmptyGemSprite, FullGemSprite;
    void Start()
    {
        GemSpriteRendererP1.GetComponent<SpriteRenderer>();
        if(GemSpriteRendererP1.sprite == null)
        {
            GemSpriteRendererP1.sprite = FullGemSprite;
        }
    }

    public void TurnOnGemSprite()
    {
        if (GemSpriteRendererP1.sprite == EmptyGemSprite)
        {
            GemSpriteRendererP1.sprite = FullGemSprite;
        }
    }
    public void TurnOffGemSprite()
    {
        if (GemSpriteRendererP1.sprite == FullGemSprite)
        {
            GemSpriteRendererP1.sprite = EmptyGemSprite;
        }
    }
}
