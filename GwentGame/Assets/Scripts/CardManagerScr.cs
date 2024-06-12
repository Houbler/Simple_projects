using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct Card 
{
    public enum CardType
    {
        WEATHER,
        MELEE,
        ARCHER,
        SIEGE,
    }

    public enum WeatherType
    {
        NO_WEATHER,
        FROST,
        FOG,
        RAIN,
        CLEAR_SKY,
    }
    public enum AbilityType
    {
        NO_ABIL,
        MEDIC_ABIL,
        IMPROVEOTHERS,
        X2FORFRIEND_ABIL,
    }
    public string Name;
    public Sprite Logo;
    public int Power;
    public bool Hero;

    public List<AbilityType> Abilities;
    public WeatherType Weather;
    public CardType Type;
    public bool HasAbility
    {
        get
        {
            return Abilities.Count > 0;
        }
    }

    public bool IsWeatherCard
    {
        get
        {
            return Weather != WeatherType.NO_WEATHER;
        }
    }

    public Card(string name, string logoPath, int power, bool hero, CardType type, AbilityType abilityType = 0, WeatherType weathertype = 0) // конструктор карты
    {
        Name = name;
        Logo = Resources.Load<Sprite>(logoPath);
        Power = power;
        Hero = hero;
        Abilities = new List<AbilityType>();
        Weather = weathertype;
        Type = type;

        if (abilityType != 0)
            Abilities.Add(abilityType);
    }

    //public Card GetCopy()
    //{
    //    Card card = this;
    //    card.Abilities = new List<AbilityType>(Abilities);
    //    return card;
    //}

    public int GetCardType(Card.CardType cardType)
    {
        int type = (int)cardType;
        return type;
    }

    public int GetWeatherType(Card.WeatherType weatherType)
    {
        int weather = (int)weatherType;
        return weather;
    }
}

public static class CardManager // храним все карты в игре
{
    public static List<Card> PlayerCardsList = new List<Card>();
    public static List<Card> EnemyCardsList = new List<Card>();

}



public class CardManagerScr : MonoBehaviour
{
    private void Awake() 
    {
        //CardManager.PlayerCardsList.Add(new Card("Taler", "Sprites/Cards/taler_s", 1, 2, false)); //spy
        CardManager.PlayerCardsList.Add(new Card("Redan", "Sprites/Cards/redan_2_m", 1, false, Card.CardType.MELEE, Card.AbilityType.NO_ABIL)); // добавляем карты
        CardManager.PlayerCardsList.Add(new Card("Redan", "Sprites/Cards/redan_1_m", 1, false, Card.CardType.MELEE, Card.AbilityType.NO_ABIL));
        CardManager.PlayerCardsList.Add(new Card("Blue_lines", "Sprites/Cards/blue_lines_m", 4, false, Card.CardType.MELEE, Card.AbilityType.X2FORFRIEND_ABIL));
        CardManager.PlayerCardsList.Add(new Card("Blue_lines", "Sprites/Cards/blue_lines_m", 4, false, Card.CardType.MELEE, Card.AbilityType.X2FORFRIEND_ABIL));
        CardManager.PlayerCardsList.Add(new Card("Blue_lines", "Sprites/Cards/blue_lines_m", 4, false, Card.CardType.MELEE, Card.AbilityType.X2FORFRIEND_ABIL));
        CardManager.PlayerCardsList.Add(new Card("Keira_mec", "Sprites/Cards/keira_mec_a", 5, false, Card.CardType.ARCHER, Card.AbilityType.NO_ABIL));
        CardManager.PlayerCardsList.Add(new Card("Medic", "Sprites/Cards/medic_s", 5, false, Card.CardType.SIEGE, Card.AbilityType.MEDIC_ABIL));
        CardManager.PlayerCardsList.Add(new Card("Sheldon", "Sprites/Cards/sheldon_a", 4, false, Card.CardType.ARCHER, Card.AbilityType.NO_ABIL));
        CardManager.PlayerCardsList.Add(new Card("Yarpen", "Sprites/Cards/yarpen_m", 2, false, Card.CardType.MELEE, Card.AbilityType.NO_ABIL));
        CardManager.PlayerCardsList.Add(new Card("Trebushet", "Sprites/Cards/trebushet_s", 6, false, Card.CardType.SIEGE, Card.AbilityType.NO_ABIL));
        CardManager.PlayerCardsList.Add(new Card("Trebushet", "Sprites/Cards/trebushet_day_s", 6, false, Card.CardType.SIEGE, Card.AbilityType.NO_ABIL));
        CardManager.PlayerCardsList.Add(new Card("Siege_master", "Sprites/Cards/siege_master_1_s", 1, false, Card.CardType.SIEGE, Card.AbilityType.IMPROVEOTHERS));
        CardManager.PlayerCardsList.Add(new Card("Siege_master", "Sprites/Cards/siege_master_2_s", 1, false, Card.CardType.SIEGE, Card.AbilityType.IMPROVEOTHERS));
        CardManager.PlayerCardsList.Add(new Card("Siege_master", "Sprites/Cards/siege_master_3_s", 1, false, Card.CardType.SIEGE, Card.AbilityType.IMPROVEOTHERS));
        CardManager.PlayerCardsList.Add(new Card("Ballista", "Sprites/Cards/ballista_s", 6, false, Card.CardType.SIEGE, Card.AbilityType.NO_ABIL));
        CardManager.PlayerCardsList.Add(new Card("Katapulta", "Sprites/Cards/katapulta_s", 8, false, Card.CardType.SIEGE, Card.AbilityType.X2FORFRIEND_ABIL));
        CardManager.PlayerCardsList.Add(new Card("Katapulta", "Sprites/Cards/katapulta_s", 8, false, Card.CardType.SIEGE, Card.AbilityType.X2FORFRIEND_ABIL));
        CardManager.PlayerCardsList.Add(new Card("Bianka", "Sprites/Cards/bianka_m", 5, false, Card.CardType.MELEE, Card.AbilityType.NO_ABIL));
        CardManager.PlayerCardsList.Add(new Card("Detmold", "Sprites/Cards/detmold_a", 6, false, Card.CardType.ARCHER, Card.AbilityType.NO_ABIL));
        CardManager.PlayerCardsList.Add(new Card("Esterad", "Sprites/Cards/esterad_m", 10, true, Card.CardType.MELEE, Card.AbilityType.NO_ABIL));
        CardManager.PlayerCardsList.Add(new Card("Filippa", "Sprites/Cards/filippa_a", 10, true, Card.CardType.ARCHER, Card.AbilityType.NO_ABIL));
        CardManager.PlayerCardsList.Add(new Card("Pehtura", "Sprites/Cards/pehtura_m", 1, false, Card.CardType.MELEE, Card.AbilityType.X2FORFRIEND_ABIL));
        CardManager.PlayerCardsList.Add(new Card("Pehtura", "Sprites/Cards/pehtura_m", 1, false, Card.CardType.MELEE, Card.AbilityType.X2FORFRIEND_ABIL));
        CardManager.PlayerCardsList.Add(new Card("Rubaily", "Sprites/Cards/rubaily_a", 5, false, Card.CardType.ARCHER, Card.AbilityType.X2FORFRIEND_ABIL));
        CardManager.PlayerCardsList.Add(new Card("Rubaily", "Sprites/Cards/rubaily_a", 5, false, Card.CardType.ARCHER, Card.AbilityType.X2FORFRIEND_ABIL));
        CardManager.PlayerCardsList.Add(new Card("Rubaily", "Sprites/Cards/rubaily_a", 5, false, Card.CardType.ARCHER, Card.AbilityType.X2FORFRIEND_ABIL));
        CardManager.PlayerCardsList.Add(new Card("Sabrina", "Sprites/Cards/sabrina_a", 1, false, Card.CardType.ARCHER, Card.AbilityType.NO_ABIL));
        CardManager.PlayerCardsList.Add(new Card("Sheala", "Sprites/Cards/sheala_a", 5, false, Card.CardType.ARCHER, Card.AbilityType.NO_ABIL));
        CardManager.PlayerCardsList.Add(new Card("Tower", "Sprites/Cards/tower_s", 6, false, Card.CardType.SIEGE, Card.AbilityType.NO_ABIL));
        CardManager.PlayerCardsList.Add(new Card("Vernon", "Sprites/Cards/vernon_m", 10, true, Card.CardType.MELEE, Card.AbilityType.NO_ABIL));
        CardManager.PlayerCardsList.Add(new Card("Yan", "Sprites/Cards/yan_m", 10, true, Card.CardType.MELEE, Card.AbilityType.NO_ABIL));
        CardManager.PlayerCardsList.Add(new Card("Zigfrid", "Sprites/Cards/zigfrid_m", 5, false, Card.CardType.MELEE, Card.AbilityType.NO_ABIL));

        CardManager.PlayerCardsList.Add(new Card("Frost", "Sprites/Weather/frost_weather", 0, false, Card.CardType.WEATHER,
                                                  Card.AbilityType.NO_ABIL, Card.WeatherType.FROST));
        CardManager.PlayerCardsList.Add(new Card("Frost", "Sprites/Weather/frost_weather", 0, false, Card.CardType.WEATHER,
                                                  Card.AbilityType.NO_ABIL, Card.WeatherType.FROST));
        CardManager.PlayerCardsList.Add(new Card("Frost", "Sprites/Weather/frost_weather", 0, false, Card.CardType.WEATHER,
                                                  Card.AbilityType.NO_ABIL, Card.WeatherType.FROST));
        CardManager.PlayerCardsList.Add(new Card("Fog", "Sprites/Weather/fog_weather", 0, false, Card.CardType.WEATHER,
                                                  Card.AbilityType.NO_ABIL, Card.WeatherType.FOG));
        CardManager.PlayerCardsList.Add(new Card("Fog", "Sprites/Weather/fog_weather", 0, false, Card.CardType.WEATHER,
                                                  Card.AbilityType.NO_ABIL, Card.WeatherType.FOG));
        CardManager.PlayerCardsList.Add(new Card("Fog", "Sprites/Weather/fog_weather", 0, false, Card.CardType.WEATHER,
                                                  Card.AbilityType.NO_ABIL, Card.WeatherType.FOG));
        CardManager.PlayerCardsList.Add(new Card("Rain", "Sprites/Weather/rain_weather", 0, false, Card.CardType.WEATHER,
                                                  Card.AbilityType.NO_ABIL, Card.WeatherType.RAIN));
        CardManager.PlayerCardsList.Add(new Card("Rain", "Sprites/Weather/rain_weather", 0, false, Card.CardType.WEATHER,
                                                  Card.AbilityType.NO_ABIL, Card.WeatherType.RAIN));
        CardManager.PlayerCardsList.Add(new Card("Rain", "Sprites/Weather/rain_weather", 0, false, Card.CardType.WEATHER,
                                                  Card.AbilityType.NO_ABIL, Card.WeatherType.RAIN));
        CardManager.PlayerCardsList.Add(new Card("Clear_sky", "Sprites/Weather/clear_sky_weather", 0, false, Card.CardType.WEATHER,
                                                  Card.AbilityType.NO_ABIL, Card.WeatherType.CLEAR_SKY));
        CardManager.PlayerCardsList.Add(new Card("Clear_sky", "Sprites/Weather/clear_sky_weather", 0, false, Card.CardType.WEATHER,
                                                  Card.AbilityType.NO_ABIL, Card.WeatherType.CLEAR_SKY));

        //CardManager.EnemyCardsList.Add(new Card("Taler", "Sprites/Cards/taler_s", 1, 2, false)); //spy
        CardManager.EnemyCardsList.Add(new Card("Redan", "Sprites/Cards/redan_2_m", 1, false, Card.CardType.MELEE, Card.AbilityType.NO_ABIL)); // добавляем карты
        CardManager.EnemyCardsList.Add(new Card("Redan", "Sprites/Cards/redan_1_m", 1, false, Card.CardType.MELEE, Card.AbilityType.NO_ABIL));
        CardManager.EnemyCardsList.Add(new Card("Blue_lines", "Sprites/Cards/blue_lines_m", 4, false, Card.CardType.MELEE, Card.AbilityType.X2FORFRIEND_ABIL));
        CardManager.EnemyCardsList.Add(new Card("Blue_lines", "Sprites/Cards/blue_lines_m", 4, false, Card.CardType.MELEE, Card.AbilityType.X2FORFRIEND_ABIL));
        CardManager.EnemyCardsList.Add(new Card("Blue_lines", "Sprites/Cards/blue_lines_m", 4, false, Card.CardType.MELEE, Card.AbilityType.X2FORFRIEND_ABIL));
        CardManager.EnemyCardsList.Add(new Card("Keira_mec", "Sprites/Cards/keira_mec_a", 5, false, Card.CardType.ARCHER, Card.AbilityType.NO_ABIL));
        CardManager.EnemyCardsList.Add(new Card("Medic", "Sprites/Cards/medic_s", 5, false, Card.CardType.SIEGE, Card.AbilityType.MEDIC_ABIL));
        CardManager.EnemyCardsList.Add(new Card("Sheldon", "Sprites/Cards/sheldon_a", 4, false, Card.CardType.ARCHER, Card.AbilityType.NO_ABIL));
        CardManager.EnemyCardsList.Add(new Card("Yarpen", "Sprites/Cards/yarpen_m", 2, false, Card.CardType.MELEE, Card.AbilityType.NO_ABIL));
        CardManager.EnemyCardsList.Add(new Card("Trebushet", "Sprites/Cards/trebushet_s", 6, false, Card.CardType.SIEGE, Card.AbilityType.NO_ABIL));
        CardManager.EnemyCardsList.Add(new Card("Trebushet", "Sprites/Cards/trebushet_day_s", 6, false, Card.CardType.SIEGE, Card.AbilityType.NO_ABIL));
        CardManager.EnemyCardsList.Add(new Card("Siege_master", "Sprites/Cards/siege_master_1_s", 1, false, Card.CardType.SIEGE, Card.AbilityType.IMPROVEOTHERS));
        CardManager.EnemyCardsList.Add(new Card("Siege_master", "Sprites/Cards/siege_master_2_s", 1, false, Card.CardType.SIEGE, Card.AbilityType.IMPROVEOTHERS));
        CardManager.EnemyCardsList.Add(new Card("Siege_master", "Sprites/Cards/siege_master_3_s", 1, false, Card.CardType.SIEGE, Card.AbilityType.IMPROVEOTHERS));
        CardManager.EnemyCardsList.Add(new Card("Ballista", "Sprites/Cards/ballista_s", 6, false, Card.CardType.SIEGE, Card.AbilityType.NO_ABIL));
        CardManager.EnemyCardsList.Add(new Card("Katapulta", "Sprites/Cards/katapulta_s", 8, false, Card.CardType.SIEGE, Card.AbilityType.X2FORFRIEND_ABIL));
        CardManager.EnemyCardsList.Add(new Card("Katapulta", "Sprites/Cards/katapulta_s", 8, false, Card.CardType.SIEGE, Card.AbilityType.X2FORFRIEND_ABIL));
        CardManager.EnemyCardsList.Add(new Card("Bianka", "Sprites/Cards/bianka_m", 5, false, Card.CardType.MELEE, Card.AbilityType.NO_ABIL));
        CardManager.EnemyCardsList.Add(new Card("Detmold", "Sprites/Cards/detmold_a", 6, false, Card.CardType.ARCHER, Card.AbilityType.NO_ABIL));
        CardManager.EnemyCardsList.Add(new Card("Esterad", "Sprites/Cards/esterad_m", 10, true, Card.CardType.MELEE, Card.AbilityType.NO_ABIL));
        CardManager.EnemyCardsList.Add(new Card("Filippa", "Sprites/Cards/filippa_a", 10, true, Card.CardType.ARCHER, Card.AbilityType.NO_ABIL));
        CardManager.EnemyCardsList.Add(new Card("Pehtura", "Sprites/Cards/pehtura_m", 1, false, Card.CardType.MELEE, Card.AbilityType.X2FORFRIEND_ABIL));
        CardManager.EnemyCardsList.Add(new Card("Pehtura", "Sprites/Cards/pehtura_m", 1, false, Card.CardType.MELEE, Card.AbilityType.X2FORFRIEND_ABIL));
        CardManager.EnemyCardsList.Add(new Card("Rubaily", "Sprites/Cards/rubaily_a", 5, false, Card.CardType.ARCHER, Card.AbilityType.X2FORFRIEND_ABIL));
        CardManager.EnemyCardsList.Add(new Card("Rubaily", "Sprites/Cards/rubaily_a", 5, false, Card.CardType.ARCHER, Card.AbilityType.X2FORFRIEND_ABIL));
        CardManager.EnemyCardsList.Add(new Card("Rubaily", "Sprites/Cards/rubaily_a", 5, false, Card.CardType.ARCHER, Card.AbilityType.X2FORFRIEND_ABIL));
        CardManager.EnemyCardsList.Add(new Card("Sabrina", "Sprites/Cards/sabrina_a", 1, false, Card.CardType.ARCHER, Card.AbilityType.NO_ABIL));
        CardManager.EnemyCardsList.Add(new Card("Sheala", "Sprites/Cards/sheala_a", 5, false, Card.CardType.ARCHER, Card.AbilityType.NO_ABIL));
        CardManager.EnemyCardsList.Add(new Card("Tower", "Sprites/Cards/tower_s", 6, false, Card.CardType.SIEGE, Card.AbilityType.NO_ABIL));
        CardManager.EnemyCardsList.Add(new Card("Vernon", "Sprites/Cards/vernon_m", 10, true, Card.CardType.MELEE, Card.AbilityType.NO_ABIL));
        CardManager.EnemyCardsList.Add(new Card("Yan", "Sprites/Cards/yan_m", 10, true, Card.CardType.MELEE, Card.AbilityType.NO_ABIL));
        CardManager.EnemyCardsList.Add(new Card("Zigfrid", "Sprites/Cards/zigfrid_m", 5, false, Card.CardType.MELEE, Card.AbilityType.NO_ABIL));

        CardManager.EnemyCardsList.Add(new Card("Frost", "Sprites/Weather/frost_weather", 0, false, Card.CardType.WEATHER,
                                           Card.AbilityType.NO_ABIL, Card.WeatherType.FROST));
        CardManager.EnemyCardsList.Add(new Card("Frost", "Sprites/Weather/frost_weather", 0, false, Card.CardType.WEATHER,
                                           Card.AbilityType.NO_ABIL, Card.WeatherType.FROST));
        CardManager.EnemyCardsList.Add(new Card("Frost", "Sprites/Weather/frost_weather", 0, false, Card.CardType.WEATHER,
                                           Card.AbilityType.NO_ABIL, Card.WeatherType.FROST));
        CardManager.EnemyCardsList.Add(new Card("Fog", "Sprites/Weather/fog_weather", 0, false, Card.CardType.WEATHER,
                                           Card.AbilityType.NO_ABIL, Card.WeatherType.FOG));
        CardManager.EnemyCardsList.Add(new Card("Fog", "Sprites/Weather/fog_weather", 0, false, Card.CardType.WEATHER,
                                           Card.AbilityType.NO_ABIL, Card.WeatherType.FOG));
        CardManager.EnemyCardsList.Add(new Card("Fog", "Sprites/Weather/fog_weather", 0, false, Card.CardType.WEATHER,
                                           Card.AbilityType.NO_ABIL, Card.WeatherType.FOG));
        CardManager.EnemyCardsList.Add(new Card("Rain", "Sprites/Weather/rain_weather", 0, false, Card.CardType.WEATHER,
                                           Card.AbilityType.NO_ABIL, Card.WeatherType.RAIN));
        CardManager.EnemyCardsList.Add(new Card("Rain", "Sprites/Weather/rain_weather", 0, false, Card.CardType.WEATHER,
                                           Card.AbilityType.NO_ABIL, Card.WeatherType.RAIN));
        CardManager.EnemyCardsList.Add(new Card("Rain", "Sprites/Weather/rain_weather", 0, false, Card.CardType.WEATHER,
                                           Card.AbilityType.NO_ABIL, Card.WeatherType.RAIN));
        CardManager.EnemyCardsList.Add(new Card("Clear_sky", "Sprites/Weather/clear_sky_weather", 0, false, Card.CardType.WEATHER,
                                           Card.AbilityType.NO_ABIL, Card.WeatherType.CLEAR_SKY));
        CardManager.EnemyCardsList.Add(new Card("Clear_sky", "Sprites/Weather/clear_sky_weather", 0, false, Card.CardType.WEATHER,
                                                  Card.AbilityType.NO_ABIL, Card.WeatherType.CLEAR_SKY));

    }

    public void AddCards()
    {
        Awake();
    }
}

