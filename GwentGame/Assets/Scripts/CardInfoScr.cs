using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardInfoScr : MonoBehaviour
{

    public Card SelfCard;
    public Image Logo;
    public TextMeshProUGUI PowerTxt;
    public int PowerInfo;
    public int Type;
    public int WeatherInfo;
    public string CardName;
    public bool HeroInfo;
    public int TruePower;

    public void ShowCardInfo(Card card)
    {
        SelfCard = card;
        Logo.sprite = card.Logo;
        Logo.preserveAspect = true; // чтобы соотношения сторон сохранялись как у jpg
        PowerInfo = card.Power;
        if (!(card.IsWeatherCard))
            PowerTxt.text = card.Power.ToString();
        if (card.Hero == true)
            PowerTxt.color = new Color(127, 255, 0);
        Type = card.GetCardType(card.Type);
        WeatherInfo = card.GetWeatherType(card.Weather);
        CardName = card.Name;
        HeroInfo = card.Hero;
        TruePower = PowerInfo;
    }
}
