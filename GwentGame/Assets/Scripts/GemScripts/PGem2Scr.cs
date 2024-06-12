using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PGem2Scr : MonoBehaviour
{
    public SpriteRenderer GemSpriteRendererP2;

    public Sprite EmptyGemSprite, FullGemSprite;
    void Start()
    {
        GemSpriteRendererP2.GetComponent<SpriteRenderer>();
        if (GemSpriteRendererP2.sprite == null)
        {
            GemSpriteRendererP2.sprite = FullGemSprite;
        }
    }

    public void TurnOnGemSprite()
    {
        if (GemSpriteRendererP2.sprite == EmptyGemSprite)
        {
            GemSpriteRendererP2.sprite = FullGemSprite;
        }
    }
    public void TurnOffGemSprite()
    {
        if (GemSpriteRendererP2.sprite == FullGemSprite)
        {
            GemSpriteRendererP2.sprite = EmptyGemSprite;
        }
    }
}
