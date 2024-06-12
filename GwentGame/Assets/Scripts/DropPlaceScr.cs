using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum FieldType 
{
    SELF_HAND,
    SELF_FIELD_MELEE,
    SELF_FIELD_ARCHER,
    SELF_FIELD_SIEGE,
    ENEMY_HAND,
    ENEMY_FIELD_MELEE,
    ENEMY_FIELD_ARCHER,
    ENEMY_FIELD_SIEGE,
    WEATHER_FIELD,
}

public class DropPlaceScr : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public FieldType Type;
    public bool IsPlaceable;

    public void OnDrop(PointerEventData eventData) 
    {
        CardMovementScr card = eventData.pointerDrag.GetComponent<CardMovementScr>();

        IsPlaceable = (card.DefaultParent.GetComponent<DropPlaceScr>().Type == FieldType.SELF_HAND) &&
                       card.GameManager.IsPlayerTurn;

        if ((!IsPlaceable) || ((Type != FieldType.SELF_FIELD_MELEE) && (Type != FieldType.SELF_FIELD_ARCHER) &&
                               (Type != FieldType.SELF_FIELD_SIEGE) && (Type != FieldType.WEATHER_FIELD)))
            return;

        if (card.GameManager.IsPlayerTurn)
        {
            if (card.GetComponent<CardInfoScr>().Type == 0 && Type == FieldType.WEATHER_FIELD)
            {
                if (card.GameManager.WeatherCards.Count == 0 && card.GetComponent<CardInfoScr>().WeatherInfo == 4) // если мы кладем clearsky в пустой филд
                    return;

                for (int i = 0; i < card.GameManager.WeatherCards.Count; i++)
                {
                    if (card.GetComponent<CardInfoScr>().WeatherInfo == card.GameManager.WeatherCards[i].WeatherInfo)  //не повторяемся
                        return;
                }
                
                card.GameManager.PlayerHandCards.Remove(card.GetComponent<CardInfoScr>()); 
                card.GameManager.WeatherCards.Add(card.GetComponent<CardInfoScr>()); 
                card.DefaultParent = transform;
                StartCoroutine(card.GameManager.WeatherCardSpell(card.GetComponent<CardInfoScr>()));
                card.GameManager.ChangePlayerScore();
                
                if (!card.GameManager.EnemyPassed)
                    card.GameManager.ChangeTurn();
                
            }
            if (card.GetComponent<CardInfoScr>().Type == 1 && Type == FieldType.SELF_FIELD_MELEE)
            {
                card.GameManager.PlayerHandCards.Remove(card.GetComponent<CardInfoScr>());
                card.GameManager.PlayerMeleeCards.Add(card.GetComponent<CardInfoScr>());
                card.DefaultParent = transform;
                card.GameManager.CheckWeatherForCard(card.GetComponent<CardInfoScr>());
                card.GameManager.ChangePlayerScore();
                if (!card.GameManager.EnemyPassed)
                    card.GameManager.ChangeTurn();
            }
            if (card.GetComponent<CardInfoScr>().Type == 2 && Type == FieldType.SELF_FIELD_ARCHER)
            {
                card.GameManager.PlayerHandCards.Remove(card.GetComponent<CardInfoScr>());
                card.GameManager.PlayerArcherCards.Add(card.GetComponent<CardInfoScr>());
                card.DefaultParent = transform;
                card.GameManager.CheckWeatherForCard(card.GetComponent<CardInfoScr>());
                card.GameManager.ChangePlayerScore();
                if (!card.GameManager.EnemyPassed)
                    card.GameManager.ChangeTurn();
            }
            if (card.GetComponent<CardInfoScr>().Type == 3 && Type == FieldType.SELF_FIELD_SIEGE)
            {
                card.GameManager.PlayerHandCards.Remove(card.GetComponent<CardInfoScr>());
                card.GameManager.PlayerSiegeCards.Add(card.GetComponent<CardInfoScr>());
                card.DefaultParent = transform;
                card.GameManager.CheckWeatherForCard(card.GetComponent<CardInfoScr>());
                card.GameManager.ChangePlayerScore();
                if (!card.GameManager.EnemyPassed)
                    card.GameManager.ChangeTurn();
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData) 
    {
        if (eventData.pointerDrag == null || Type == FieldType.ENEMY_FIELD_MELEE ||
            Type == FieldType.ENEMY_FIELD_ARCHER || Type == FieldType.ENEMY_FIELD_SIEGE ||
            Type == FieldType.ENEMY_HAND || Type == FieldType.SELF_HAND)                                                                
            return;

        CardMovementScr card = eventData.pointerDrag.GetComponent<CardMovementScr>(); 

        if (card.GetComponent<CardInfoScr>().Type == 0 && Type == FieldType.WEATHER_FIELD)
        {
            if (card.GameManager.WeatherCards.Count == 0 && card.GetComponent<CardInfoScr>().WeatherInfo == 4) 
                return;

            for (int i = 0; i < card.GameManager.WeatherCards.Count; i++)
            {
                if (card.GetComponent<CardInfoScr>().WeatherInfo == card.GameManager.WeatherCards[i].WeatherInfo)  
                    return;
            }
            card.DefaultTempCardParent = transform;
        }

        if (card.GetComponent<CardInfoScr>().Type == 1 && Type == FieldType.SELF_FIELD_MELEE)
            card.DefaultTempCardParent = transform; 

        if (card.GetComponent<CardInfoScr>().Type == 2 && Type == FieldType.SELF_FIELD_ARCHER)
            card.DefaultTempCardParent = transform;

        if (card.GetComponent<CardInfoScr>().Type == 3 && Type == FieldType.SELF_FIELD_SIEGE)
            card.DefaultTempCardParent = transform;
    }

    public void OnPointerExit(PointerEventData eventData) 
    {
        if (eventData.pointerDrag == null) 
            return;

        CardMovementScr card = eventData.pointerDrag.GetComponent<CardMovementScr>(); 

        if (card && card.DefaultTempCardParent == transform) // проверка на то лежала ли эта карта здесь раньше 
            card.DefaultTempCardParent = card.DefaultParent;
    }
}
