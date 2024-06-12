using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // textMesh

public class Game 
{
    public List<Card> EnemyDeck, PlayerDeck;
                      
    public Game()
    {
        EnemyDeck = GiveEnemyDeckCard(); 
        PlayerDeck = GivePlayerDeckCard();
    }

    List<Card> GiveEnemyDeckCard()
    {
        List<Card> list = new List<Card>(); 
        for (int i = 0; i < 22; i++)
        {
            int NumberCard = Random.Range(0, CardManager.EnemyCardsList.Count);
            list.Add(CardManager.EnemyCardsList[NumberCard]);
            CardManager.EnemyCardsList.RemoveAt(NumberCard);
        }
        return list;
    }

    List<Card> GivePlayerDeckCard() 
    {
        List<Card> list = new List<Card>();
        for (int i = 0; i < 22; i++)
        {
            int NumberCard = Random.Range(0, CardManager.PlayerCardsList.Count);
            list.Add(CardManager.PlayerCardsList[NumberCard]);
            CardManager.PlayerCardsList.RemoveAt(NumberCard);
        }
        return list;
    }
}

public class GameManagerScr : MonoBehaviour
{
    public Game CurrentGame;
    public Transform EnemyHand, PlayerHand,
                     EnemyMeleeF, PlayerMeleeF,
                     EnemyArcherF, PlayerArcherF,
                     EnemySiegeF, PlayerSiegeF,
                     DeadPCardsF, DeadECardsF,
                     WeatherCardsF, DeadWeatherCardsF;

    public GameObject CardPref, ResultGO;

    PGem1Scr PGem1;
    PGem2Scr PGem2;
    EGem1Scr EGem1;
    EGem2Scr EGem2;

    int Turn;
    public int AmountPlayerCards = 0, AmountEnemyCards = 0,
               PlayerScore = 0, EnemyScore = 0,
               PSFScore = 0, PAFScore = 0, PMFScore = 0, EMFScore = 0, EAFScore = 0, ESFScore = 0,
               SumPlayer1, SumPlayer2, SumPlayer3 = 0, SumPlayerEnd, SumEnemy1, SumEnemy2, SumEnemy3 = 0, SumEnemyEnd;

    public TextMeshProUGUI ResultTxt, TurnTxt,
                           AmountPCardsTxt, AmountECardsTxt,
                           PSTxt, ESTxt, PSFSTxt, PAFSTxt, PMFSTxt, EMFSTxt, EAFSTxt, ESFSTxt;

    public List<CardInfoScr> EnemyHandCards = new List<CardInfoScr>(),
                             EnemySiegeCards = new List<CardInfoScr>(),
                             EnemyArcherCards = new List<CardInfoScr>(),
                             EnemyMeleeCards = new List<CardInfoScr>(),
                             EnemyDeadCards = new List<CardInfoScr>(),
                             PlayerMeleeCards = new List<CardInfoScr>(),
                             PlayerArcherCards = new List<CardInfoScr>(),
                             PlayerSiegeCards = new List<CardInfoScr>(),
                             PlayerHandCards = new List<CardInfoScr>(),
                             PlayerDeadCards = new List<CardInfoScr>(),
                             WeatherCards = new List<CardInfoScr>(),
                             DeadWeatherCards = new List<CardInfoScr>();

    bool EnemyWon1Round, PlayerWon1Round, PlayerPassed, PlayerWon, EnemyWon;
    bool FrostIsON, FogIsON, RainIsON;
    bool IsPassLegit = true;
    public bool EnemyPassed = false;
    
    public bool IsPlayerTurn 
    {
        get
        {
            return Turn % 2 == 0;
        }
    }

    void Start()
    {
        StartGame();
    }

    public void RestartGame()
    {
        StopAllCoroutines();

        foreach (var card in PlayerHandCards)
            Destroy(card.gameObject);
        foreach (var card in PlayerDeadCards)
            Destroy(card.gameObject);
        foreach (var card in EnemyDeadCards)
            Destroy(card.gameObject);
        foreach (var card in EnemyHandCards)
            Destroy(card.gameObject);
        foreach (var card in WeatherCards)
            Destroy(card.gameObject);

        foreach (var card in PlayerMeleeCards)
            Destroy(card.gameObject);
        foreach (var card in PlayerArcherCards)
            Destroy(card.gameObject);
        foreach (var card in PlayerSiegeCards)
            Destroy(card.gameObject);
        foreach (var card in EnemyMeleeCards)
            Destroy(card.gameObject);
        foreach (var card in EnemyArcherCards)
            Destroy(card.gameObject);
        foreach (var card in EnemySiegeCards)
            Destroy(card.gameObject);

        EnemyHandCards.Clear();
        EnemySiegeCards.Clear();
        EnemyArcherCards.Clear();
        EnemyMeleeCards.Clear();
        EnemyDeadCards.Clear();
        
        PlayerMeleeCards.Clear();
        PlayerArcherCards.Clear();
        PlayerSiegeCards.Clear();
        PlayerHandCards.Clear();
        PlayerDeadCards.Clear();

        WeatherCards.Clear();

        FrostIsON = FogIsON = RainIsON = false;
        IsPassLegit = true;

        CardManager.PlayerCardsList.Clear();
        CardManager.EnemyCardsList.Clear();
        GetComponent<CardManagerScr>().AddCards();

        StartGame();
    }

    void StartGame()
    {
        Turn = 0;

        CurrentGame = new Game(); 

        GiveHandCards(CurrentGame.EnemyDeck, EnemyHand); 
        GiveHandCards(CurrentGame.PlayerDeck, PlayerHand);

        ChangePlayerScore();
        ChangeEnemyScore();

        PGem1 = GameObject.Find("PlayerGem1").GetComponent<PGem1Scr>();
        PGem2 = GameObject.Find("PlayerGem2").GetComponent<PGem2Scr>();
        EGem1 = GameObject.Find("EnemyGem1").GetComponent<EGem1Scr>();
        EGem2 = GameObject.Find("EnemyGem2").GetComponent<EGem2Scr>();

        PGem1.TurnOnGemSprite();
        PGem2.TurnOnGemSprite();
        EGem1.TurnOnGemSprite();
        EGem2.TurnOnGemSprite();

        EnemyWon1Round = PlayerWon1Round = PlayerPassed = PlayerWon = EnemyWon = false;

        ResultGO.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (IsPlayerTurn == true) && (IsPassLegit == true))
        {
            PlayerPassed = true;
            //StopAllCoroutines();
            Debug.Log("Passed by player");
            if ((EnemyScore <= PlayerScore) && (EnemyHandCards.Count != 0) && !EnemyPassed)
            {
                ChangeTurn();
                Debug.Log("skip3");
                //StartCoroutine(EnemyTurn(EnemyHandCards));
            }
            else
                Newround();
        }
    }

    void GiveHandCards(List<Card> deck, Transform hand) 
    {
        int i = 0;
        while (i++ < 10)
            GiveCardToHand(deck, hand);
    }

    void GiveCardToHand(List<Card> deck, Transform hand) 
    {
        if (deck.Count == 0) 
            return;

        Card card = deck[0]; 

        GameObject cardGO = Instantiate(CardPref, hand, false); // превращаем в GO

        if (hand == EnemyHand) 
        {
            cardGO.GetComponent<CardInfoScr>().ShowCardInfo(card);
            EnemyHandCards.Add(cardGO.GetComponent<CardInfoScr>());
        }
        else
        {
            cardGO.GetComponent<CardInfoScr>().ShowCardInfo(card);
            PlayerHandCards.Add(cardGO.GetComponent<CardInfoScr>());
        }
        deck.RemoveAt(0); 
    }

    IEnumerator EnemyTurn(List<CardInfoScr> cards)
    {   
        yield return new WaitForSeconds(0.5f);
        int RandomCard = Random.Range(0, cards.Count);
        if (!(cards.Count == 0))
        {
            if (RandomCard < cards.Count)
            {
                CheckWeather();
                if (cards[RandomCard].Type == 1)
                {
                    cards[RandomCard].GetComponent<CardMovementScr>().MoveToField(EnemyMeleeF);
                    yield return new WaitForSeconds(.51f);
                    cards[RandomCard].transform.SetParent(EnemyMeleeF); 
                    CheckWeatherForCard(cards[RandomCard]);
                    EnemyMeleeCards.Add(cards[RandomCard]);
                    EnemyHandCards.Remove(cards[RandomCard]); 
                    ChangeEnemyScore();
                }
                else if (cards[RandomCard].Type == 2)
                {
                    cards[RandomCard].GetComponent<CardMovementScr>().MoveToField(EnemyArcherF);
                    yield return new WaitForSeconds(.51f);
                    cards[RandomCard].transform.SetParent(EnemyArcherF);
                    CheckWeatherForCard(cards[RandomCard]);
                    EnemyArcherCards.Add(cards[RandomCard]);
                    EnemyHandCards.Remove(cards[RandomCard]);
                    ChangeEnemyScore();
                }
                else if (cards[RandomCard].Type == 3)
                {
                    cards[RandomCard].GetComponent<CardMovementScr>().MoveToField(EnemySiegeF);
                    yield return new WaitForSeconds(.51f);
                    cards[RandomCard].transform.SetParent(EnemySiegeF);
                    CheckWeatherForCard(cards[RandomCard]);
                    EnemySiegeCards.Add(cards[RandomCard]);
                    EnemyHandCards.Remove(cards[RandomCard]);
                    ChangeEnemyScore();
                }
                else if (cards[RandomCard].Type == 0)
                {
                    cards[RandomCard].GetComponent<CardMovementScr>().MoveToField(WeatherCardsF);
                    yield return new WaitForSeconds(0.51f);
                    StartCoroutine(WeatherCardSpell(cards[RandomCard]));
                    cards[RandomCard].transform.SetParent(WeatherCardsF);

                    WeatherCards.Add(cards[RandomCard]); 
                    EnemyHandCards.Remove(cards[RandomCard]); 

                    ChangeEnemyScore();
                    yield return new WaitForSeconds(0.5f);
                }
            }

            void CheckWeather()
            {
                Debug.Log("Gonna check is this card in weather field");
                bool Check = false;
                int k = 0;
                while (Check == false)
                {
                    if ((WeatherCards.Find(cards => cards.WeatherInfo == 1) && (cards[RandomCard].WeatherInfo == 1)) ||
                   (WeatherCards.Find(cards => cards.WeatherInfo == 2) && (cards[RandomCard].WeatherInfo == 2)) ||
                   (WeatherCards.Find(cards => cards.WeatherInfo == 3) && (cards[RandomCard].WeatherInfo == 3)) ||
                   (cards[RandomCard].WeatherInfo == 4) && (WeatherCards.Count == 0))
                    {
                        RandomCard = Random.Range(0, cards.Count);
                        k++;
                        if (k == 20)
                        {
                            ChangeTurn(); // mb risk
                            Newround();
                            Check = true;
                            ChangeEnemyScore();
                        }
                    }
                    else
                    {
                        Check = true;
                    }
                }
                
            }
            yield return new WaitForSeconds(0.3f);

            //Debug.Log("Before type 0 Card index" + RandomCard);
            //if(RandomCard != 0)
            //    Debug.Log("Before type 0 Card type" + cards[RandomCard].Type);

            
        }
        else
        {
            Debug.Log("skip1");
            EnemyPassed = true;
            ChangeTurn();
        }

        if (PlayerPassed == true)
        {
            if ((cards.Count != 0) && (EnemyScore <= PlayerScore))
            {
                yield return StartCoroutine(EnemyTurn(EnemyHandCards));
                Newround();
            }
            else
            {
                Newround();
            }
        }
        ChangeEnemyScore();
        if (!IsPlayerTurn)
        {
            yield return new WaitForSeconds(0.5f);
            Debug.Log("skip2");
            ChangeTurn();
        }
    }

    public void ChangeTurn() 
    {
        //StopAllCoroutines(); // мб лишнее
        Turn++;
        if (EnemyPassed == false)
        {
            if (IsPlayerTurn) 
                              
                Debug.Log("Player");
            TurnTxt.text = "Your Turn";
            if (!IsPlayerTurn)
            {
                Debug.Log("EnemyTurn");
                TurnTxt.text = "Enemy's Turn";
                StartCoroutine(EnemyTurn(EnemyHandCards));
            }
        }
        else
        {
            TurnTxt.text = "Enemy Passed";
        }
    }

    //void GiveNewCards() 
    //{
    //    GiveCardToHand(CurrentGame.EnemyDeck, EnemyHand);
    //    GiveCardToHand(CurrentGame.PlayerDeck, PlayerHand);
    //}

    public void ChangePlayerScore()
    {
        PlayerScore = 0;
        PMFScore = 0;
        AmountPlayerCards = 0;
        for (int i = 0; i < PlayerMeleeCards.Count; i++)
        {
            PMFScore += PlayerMeleeCards[i].PowerInfo;
        }
        PMFSTxt.text = PMFScore.ToString();

        PAFScore = 0;
        for (int i = 0; i < PlayerArcherCards.Count; i++)
        {
            PAFScore += PlayerArcherCards[i].PowerInfo;
        }
        PAFSTxt.text = PAFScore.ToString();

        PSFScore = 0;
        for (int i = 0; i < PlayerSiegeCards.Count; i++)
        {
            PSFScore += PlayerSiegeCards[i].PowerInfo;
        }
        PSFSTxt.text = PSFScore.ToString();

        PlayerScore = PAFScore + PMFScore + PSFScore;
        PSTxt.text = PlayerScore.ToString();

        AmountPlayerCards = PlayerHandCards.Count;
        AmountPCardsTxt.text = AmountPlayerCards.ToString();
    }

    public void ChangeEnemyScore()
    {
        EnemyScore = 0;
        EMFScore = 0;
        for (int i = 0; i < EnemyMeleeCards.Count; i++)
        {
            EMFScore += EnemyMeleeCards[i].PowerInfo;
        }
        EMFSTxt.text = EMFScore.ToString();

        EAFScore = 0;
        for (int i = 0; i < EnemyArcherCards.Count; i++)
        {
            EAFScore += EnemyArcherCards[i].PowerInfo;
        }
        EAFSTxt.text = EAFScore.ToString();

        ESFScore = 0;
        for (int i = 0; i < EnemySiegeCards.Count; i++)
        {
            ESFScore += EnemySiegeCards[i].PowerInfo;
        }
        ESFSTxt.text = ESFScore.ToString();

        EnemyScore = EAFScore + EMFScore + ESFScore;
        ESTxt.text = EnemyScore.ToString();

        AmountEnemyCards = EnemyHandCards.Count;
        AmountECardsTxt.text = AmountEnemyCards.ToString();
    }

    void Newround()
    {
        Debug.Log("New round");
        StopAllCoroutines();
        Turn = 0;
        IsPassLegit = true;
        PlayerPassed = false;
        EnemyPassed = false;
        FrostIsON = FogIsON = RainIsON = false;

        if (IsPlayerTurn) 
            Debug.Log("Player");
        TurnTxt.text = "Your Turn";
        if (!IsPlayerTurn)
        {
            Debug.Log("EnemyTurn");
            TurnTxt.text = "Enemy's Turn";
        }

        CheckSumScore();
        CheckForResult();

        StartCoroutine(RemoveDeadCards());
    }

    IEnumerator RemoveDeadCards()
    {
        IsPassLegit = false;
        int num = PlayerMeleeCards.Count;
        for (int i = 0; i < num; i++)
        {
            PlayerMeleeCards[0].GetComponent<CardMovementScr>().MoveToFieldQuickly(DeadPCardsF);
            yield return new WaitForSeconds(.21f);
            PlayerMeleeCards[0].transform.SetParent(DeadPCardsF);
            PlayerDeadCards.Add(PlayerMeleeCards[0]);
            PlayerMeleeCards.Remove(PlayerMeleeCards[0]);
        }

        num = PlayerArcherCards.Count;
        for (int i = 0; i < num; i++)
        {
            PlayerArcherCards[0].GetComponent<CardMovementScr>().MoveToFieldQuickly(DeadPCardsF);
            yield return new WaitForSeconds(.21f);
            PlayerArcherCards[0].transform.SetParent(DeadPCardsF);
            PlayerDeadCards.Add(PlayerArcherCards[0]);
            PlayerArcherCards.Remove(PlayerArcherCards[0]);
        }

        num = PlayerSiegeCards.Count;
        for (int i = 0; i < num; i++)
        {
            PlayerSiegeCards[0].GetComponent<CardMovementScr>().MoveToFieldQuickly(DeadPCardsF);
            yield return new WaitForSeconds(.21f);
            PlayerSiegeCards[0].transform.SetParent(DeadPCardsF);
            PlayerDeadCards.Add(PlayerSiegeCards[0]);
            PlayerSiegeCards.Remove(PlayerSiegeCards[0]);
        }
        ChangePlayerScore();

        num = EnemyMeleeCards.Count;
        for (int i = 0; i < num; i++)
        {
            EnemyMeleeCards[0].GetComponent<CardMovementScr>().MoveToFieldQuickly(DeadECardsF);
            yield return new WaitForSeconds(.21f);
            EnemyMeleeCards[0].transform.SetParent(DeadECardsF);
            EnemyDeadCards.Add(EnemyMeleeCards[0]);
            EnemyMeleeCards.Remove(EnemyMeleeCards[0]);
        }

        num = EnemyArcherCards.Count;
        for (int i = 0; i < num; i++)
        {
            EnemyArcherCards[0].GetComponent<CardMovementScr>().MoveToFieldQuickly(DeadECardsF);
            yield return new WaitForSeconds(.21f);
            EnemyArcherCards[0].transform.SetParent(DeadECardsF);
            EnemyDeadCards.Add(EnemyArcherCards[0]);
            EnemyArcherCards.Remove(EnemyArcherCards[0]);
        }

        num = EnemySiegeCards.Count;
        for (int i = 0; i < num; i++)
        {
            EnemySiegeCards[0].GetComponent<CardMovementScr>().MoveToFieldQuickly(DeadECardsF);
            yield return new WaitForSeconds(.21f);
            EnemySiegeCards[0].transform.SetParent(DeadECardsF);
            EnemyDeadCards.Add(EnemySiegeCards[0]);
            EnemySiegeCards.Remove(EnemySiegeCards[0]);
        }
        ChangeEnemyScore();

        StartCoroutine(RemoveWeatherCards());
        IsPassLegit = true;
    }

    public IEnumerator WeatherCardSpell(CardInfoScr card)
    {
        int weatherInfo = card.WeatherInfo;
        if (weatherInfo == 1)
        {
            for (int i = 0; i < PlayerMeleeCards.Count; i++)
            {
                if (PlayerMeleeCards[i].HeroInfo == false)
                {
                    PlayerMeleeCards[i].PowerInfo = 1;
                    PlayerMeleeCards[i].PowerTxt.text = PlayerMeleeCards[i].PowerInfo.ToString();
                }
            }
            for (int i = 0; i < EnemyMeleeCards.Count; i++)
            {
                if (EnemyMeleeCards[i].HeroInfo == false)
                {
                    EnemyMeleeCards[i].PowerInfo = 1;
                    EnemyMeleeCards[i].PowerTxt.text = EnemyMeleeCards[i].PowerInfo.ToString();
                }
            }
            FrostIsON = true;
            ChangePlayerScore();
            ChangeEnemyScore();
            Debug.Log("Frost ON");
        }
        else if (weatherInfo == 2)
        {
            for (int i = 0; i < PlayerArcherCards.Count; i++)
            {
                if (PlayerArcherCards[i].HeroInfo == false)
                {
                    PlayerArcherCards[i].PowerInfo = 1;
                    PlayerArcherCards[i].PowerTxt.text = PlayerArcherCards[i].PowerInfo.ToString();
                }
            }
            for (int i = 0; i < EnemyArcherCards.Count; i++)
            {
                if (EnemyArcherCards[i].HeroInfo == false)
                {
                    EnemyArcherCards[i].PowerInfo = 1;
                    EnemyArcherCards[i].PowerTxt.text = EnemyArcherCards[i].PowerInfo.ToString();
                }
            }
            FogIsON = true;
            ChangePlayerScore();
            ChangeEnemyScore();
            Debug.Log("Fog ON");
        }
        else if (weatherInfo == 3)
        {
            for (int i = 0; i < PlayerSiegeCards.Count; i++)
            {
                if (PlayerSiegeCards[i].HeroInfo == false)
                {
                    PlayerSiegeCards[i].PowerInfo = 1;
                    PlayerSiegeCards[i].PowerTxt.text = PlayerSiegeCards[i].PowerInfo.ToString();
                }
            }
            for (int i = 0; i < EnemySiegeCards.Count; i++)
            {
                if (EnemySiegeCards[i].HeroInfo == false)
                {
                    EnemySiegeCards[i].PowerInfo = 1;
                    EnemySiegeCards[i].PowerTxt.text = EnemySiegeCards[i].PowerInfo.ToString();
                }
            }
            RainIsON = true;
            ChangePlayerScore();
            ChangeEnemyScore();
            Debug.Log("Rain On");
        }
        else if (weatherInfo == 4)
        {
            yield return new WaitForSeconds(0.25f);
            for (int i = 0; i < PlayerMeleeCards.Count; i++)
            {
                if (PlayerMeleeCards[i].HeroInfo == false)
                {
                    PlayerMeleeCards[i].PowerInfo = PlayerMeleeCards[i].TruePower;
                    PlayerMeleeCards[i].PowerTxt.text = PlayerMeleeCards[i].PowerInfo.ToString();
                }
            }
            for (int i = 0; i < EnemyMeleeCards.Count; i++)
            {
                if (EnemyMeleeCards[i].HeroInfo == false)
                {
                    EnemyMeleeCards[i].PowerInfo = EnemyMeleeCards[i].TruePower;
                    EnemyMeleeCards[i].PowerTxt.text = EnemyMeleeCards[i].PowerInfo.ToString();
                }
            }
            for (int i = 0; i < PlayerArcherCards.Count; i++)
            {
                if (PlayerArcherCards[i].HeroInfo == false)
                {
                    PlayerArcherCards[i].PowerInfo = PlayerArcherCards[i].TruePower;
                    PlayerArcherCards[i].PowerTxt.text = PlayerArcherCards[i].PowerInfo.ToString();
                }
            }
            for (int i = 0; i < EnemyArcherCards.Count; i++)
            {
                if (EnemyArcherCards[i].HeroInfo == false)
                {
                    EnemyArcherCards[i].PowerInfo = EnemyArcherCards[i].TruePower;
                    EnemyArcherCards[i].PowerTxt.text = EnemyArcherCards[i].PowerInfo.ToString();
                }
            }
            for (int i = 0; i < PlayerSiegeCards.Count; i++)
            {
                if (PlayerSiegeCards[i].HeroInfo == false)
                {
                    PlayerSiegeCards[i].PowerInfo = PlayerSiegeCards[i].TruePower;
                    PlayerSiegeCards[i].PowerTxt.text = PlayerSiegeCards[i].PowerInfo.ToString();
                }
            }
            for (int i = 0; i < EnemySiegeCards.Count; i++)
            {
                if (EnemySiegeCards[i].HeroInfo == false)
                {
                    EnemySiegeCards[i].PowerInfo = EnemySiegeCards[i].TruePower;
                    EnemySiegeCards[i].PowerTxt.text = EnemySiegeCards[i].PowerInfo.ToString();
                }
            }
            ChangePlayerScore();
            ChangeEnemyScore();
            FrostIsON = FogIsON = RainIsON = false;
            Debug.Log("Clear Sky On");
            StartCoroutine(GetComponent<GameManagerScr>().RemoveWeatherCards());
            yield return new WaitForSeconds(0.8f);
        }
    }

    public void CheckWeatherForCard(CardInfoScr card)
    {
        if (FrostIsON == true && card.Type == 1 && card.HeroInfo == false)
        {
            card.PowerInfo = 1;
            card.PowerTxt.text = card.PowerInfo.ToString();
        }
        if (FogIsON == true && card.Type == 2 && card.HeroInfo == false)
        {
            card.PowerInfo = 1;
            card.PowerTxt.text = card.PowerInfo.ToString();
        }
        if (RainIsON == true && card.Type == 3 && card.HeroInfo == false)
        {
            card.PowerInfo = 1;
            card.PowerTxt.text = card.PowerInfo.ToString();
        }
    }
    public IEnumerator RemoveWeatherCards()
    {
        int num = WeatherCards.Count;
        for (int i = 0; i < num; i++)
        {
            WeatherCards[0].GetComponent<CardMovementScr>().MoveToFieldQuickly(DeadWeatherCardsF);
            yield return new WaitForSeconds(.21f);
            WeatherCards[0].transform.SetParent(DeadWeatherCardsF);
            DeadWeatherCards.Add(WeatherCards[0]);
            WeatherCards.Remove(WeatherCards[0]);
        }
    }

    void CheckSumScore()
    {
        if ((PlayerWon1Round == false) && (EnemyWon1Round == false))
        {
            SumPlayer1 = PlayerScore;
            SumEnemy1 = EnemyScore;
        }
        else if (((PlayerWon1Round == true) || (EnemyWon1Round == true)) && !((PlayerWon1Round == true) && (EnemyWon1Round == true)) && ((PlayerWon == false) || (EnemyWon == false)))
        {
            SumPlayer2 = PlayerScore;
            SumEnemy2 = EnemyScore;
            SumPlayerEnd = SumPlayer1 + SumPlayer2;
            SumEnemyEnd = SumEnemy1 + SumEnemy2;
        }
        else if (((PlayerWon1Round == true) && (EnemyWon1Round == true)) || (PlayerWon == true) || (EnemyWon == true))
        {
            SumPlayer3 = PlayerScore;
            SumEnemy3 = EnemyScore;
            SumPlayerEnd = SumPlayer1 + SumPlayer2 + SumPlayer3;
            SumEnemyEnd = SumEnemy1 + SumEnemy2 + SumEnemy3;
        }
    }
    void CheckForResult()
    {
        if (PlayerWon == false && EnemyWon == false)
        {
            if ((PlayerScore >= EnemyScore) && (PlayerWon1Round == false))
            {
                EGem1.TurnOffGemSprite();
                PlayerWon1Round = true;
            }
            else if ((PlayerScore >= EnemyScore) && (PlayerWon1Round == true))
            {
                EGem2.TurnOffGemSprite();
                PlayerWon = true;
                // Player win
            }
            else if ((PlayerScore < EnemyScore) && (EnemyWon1Round == false))
            {
                PGem1.TurnOffGemSprite();
                EnemyWon1Round = true;
            }
            else if ((PlayerScore < EnemyScore) && (EnemyWon1Round == true))
            {
                PGem2.TurnOffGemSprite();
                EnemyWon = true;
                // Enemy win
            }
            CheckSumScore();

            if (PlayerWon == true || EnemyWon == true || (PlayerWon == true && EnemyWon == true))
            {
                ResultGO.SetActive(true);
                StopAllCoroutines();

                if (PlayerWon == true && EnemyWon == false)
                    ResultTxt.text = "Player won! Scoreboard: \nPlayer. 1: " + SumPlayer1 + "\t2: " + SumPlayer2 + "\t3: " + SumPlayer3 + "\tSum: " + SumPlayerEnd +
                                                             "\nEnemy.  1: " + SumEnemy1 + "\t2: " + SumEnemy2 + "\t3: " + SumEnemy3 + "\tSum: " + SumEnemyEnd;
                else if (PlayerWon == false && EnemyWon == true)
                    ResultTxt.text = "Enemy won! Scoreboard: \nPlayer. 1: " + SumPlayer1 + "\t2: " + SumPlayer2 + "\t3: " + SumPlayer3 + "\tSum: " + SumPlayerEnd +
                                                            "\nEnemy.  1: " + SumEnemy1 + "\t2: " + SumEnemy2 + "\t3: " + SumEnemy3 + "\tSum: " + SumEnemyEnd;
            }
        }
    }
}
