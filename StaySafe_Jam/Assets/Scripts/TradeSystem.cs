using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TradeSystem : MonoBehaviour
{
    public HouseObject houseObject;

    [Space(5)]
    [Header("Canvas Panel")]
    public Canvas QuestionCanvas;
    public Canvas TradeView;
    public Canvas NeighborOverview;
    public Canvas FamilyStoryCanvas;

    [Space(5)]
    [Header("Text Fields")]
    public TMP_Text DayTimeText;
    public TMP_Text DayText;
    [Space(5)]
    [Header("FamilyStories")]
    public TMP_Text[] FamilyStory;
    [Header("Description")]
    public TMP_Text[] DesTpText;
    public TMP_Text[] DesHgText;
    public TMP_Text[] DesFoodText;
    [Header("AddAmount")]
    public TMP_Text[] ToiletpaperText;
    public TMP_Text[] HygienText;
    public TMP_Text[] FoodText;
    [Header("DefaultText")]
    public TMP_Text[] DTpText;
    public TMP_Text[] DHgText;
    public TMP_Text[] DFdText;
    [Space(5)]
    [Header("Button")]
    public TMP_Text[] ButtonText;


    [Space(5)]
    [Header("House Limits")]
    public int House1Limit = 2;
    public int House2Limit = 2;
    public int House3Limit = 2;
    public int House4Limit = 2;

    private bool DoTrade = false;
    private int tradePerDay = 3;
    private int weekdays = 1;
    private int week = 0;
    private int randTradeRequest;

    private int TpCount;
    private int HgCount;
    private int FoodCount;

    private int TpAmount;
    private int HgAmount;
    private int FoodAmount;

    private int[] Trader1 = new int[3];
    private int[] Trader2 = new int[3];
    private int[] Trader3 = new int[3];
    private int[] Trader4 = new int[3];

    private void Awake()
    {
        //GameObject[] manager = GameObject.FindGameObjectsWithTag("Manager");
        //if (manager.Length > 1)
        //    Destroy(this.gameObject);
        //DontDestroyOnLoad(this.gameObject);

        // Scriptable Object reset
        for (int i = 0; i < houseObject.Ressource.Length; i++)
        {
            houseObject.Ressource[i].Toiletpaper = 0;
            houseObject.Ressource[i].Hygiene = 0;
            houseObject.Ressource[i].Food = 0;
        }
    }

    void Start()
    {
        ChangeDay(0);
        ChangeDaytime();

        QuestionCanvas.gameObject.SetActive(true);
        TradeView.gameObject.SetActive(false);
        NeighborOverview.gameObject.SetActive(false);
        FamilyStoryCanvas.gameObject.SetActive(false);

        for (int i = 0; i <= houseObject.Ressource.Length; i++)
        {
            HouseState(i);
        }
    }

    // gameplay dayroutine
    public void TradeRoutine()
    {
        ChangeToTrade(true);

        if (tradePerDay > 0)
        {
            for (int i = 0; i <= houseObject.Ressource.Length; i++)
            {
                HouseState(i);
                TradeRequest(i);
            }
        }
        else
        {
            //RessourceReduce();
            tradePerDay = 3;
        }

    }


    #region ButtonFunktionen
    public void FamilyStoryButton(int Num)
    {
        FamilyStoryCanvas.gameObject.SetActive(true);
        QuestionCanvas.gameObject.SetActive(false);
        TradeView.gameObject.SetActive(false);
        NeighborOverview.gameObject.SetActive(false);

        switch (Num)
        {
            case 0:
                FamilyStory[0].gameObject.SetActive(true);
                FamilyStory[1].gameObject.SetActive(false);
                FamilyStory[2].gameObject.SetActive(false);
                FamilyStory[3].gameObject.SetActive(false);
                break;
            case 1:
                FamilyStory[1].gameObject.SetActive(true);
                FamilyStory[0].gameObject.SetActive(false);
                FamilyStory[2].gameObject.SetActive(false);
                FamilyStory[3].gameObject.SetActive(false);
                break;
            case 2:
                FamilyStory[2].gameObject.SetActive(true);
                FamilyStory[1].gameObject.SetActive(false);
                FamilyStory[0].gameObject.SetActive(false);
                FamilyStory[3].gameObject.SetActive(false);
                break;
            case 3:
                FamilyStory[3].gameObject.SetActive(true);
                FamilyStory[1].gameObject.SetActive(false);
                FamilyStory[2].gameObject.SetActive(false);
                FamilyStory[0].gameObject.SetActive(false);
                break;
            default:
                FamilyStory[0].gameObject.SetActive(false);
                FamilyStory[1].gameObject.SetActive(false);
                FamilyStory[2].gameObject.SetActive(false);
                FamilyStory[3].gameObject.SetActive(false);
                break;
        }
        
    }

    public void QuestionTrade(bool trade)
    {
        DoTrade = trade;

        if (DoTrade == true)
        {
            TradeRoutine();
        }
        if (DoTrade == false)
        {
            tradePerDay--;
            ChangeDaytime();
        }
    }

    public void TradeTp(int Num)
    {
        switch (Num)
        {
            case 1:
                houseObject.Ressource[1].Toiletpaper += Trader1[0]; //TpAmount

                PlayerGiveRessources(Trader1[0], 0, 0);
                break;

            case 2:
                houseObject.Ressource[Num].Toiletpaper += Trader2[0]; //TpAmount

                PlayerGiveRessources(Trader2[0], 0, 0);
                break;

            case 3:
                houseObject.Ressource[Num].Toiletpaper += Trader3[0]; //TpAmount

                PlayerGiveRessources(Trader3[0], 0, 0);
                break;

            case 4:
                houseObject.Ressource[Num].Toiletpaper += Trader4[0]; //TpAmount

                PlayerGiveRessources(Trader4[0], 0, 0);
                break;

            default:
                break;
        }
        UpdateRessourceDescriptionText(0, houseObject.Ressource[0].Toiletpaper, houseObject.Ressource[0].Hygiene, houseObject.Ressource[0].Food);
        UpdateRessourceDescriptionText(Num, houseObject.Ressource[Num].Toiletpaper, houseObject.Ressource[Num].Hygiene, houseObject.Ressource[Num].Food);


        // restartTrade - Different Time(?)
        tradePerDay--;
        ChangeToTrade(false);
        ChangeDaytime();
    }

    public void TradeHg(int Num)
    {
        switch (Num)
        {
            case 1:
                houseObject.Ressource[1].Hygiene += Trader1[1]; //TpAmount

                PlayerGiveRessources(0, Trader1[1], 0);
                break;

            case 2:
                houseObject.Ressource[2].Hygiene += Trader2[1]; //TpAmount

                PlayerGiveRessources(0, Trader2[1], 0);
                break;

            case 3:
                houseObject.Ressource[3].Hygiene += Trader3[1]; //TpAmount

                PlayerGiveRessources(0, Trader3[1], 0);
                break;

            case 4:
                houseObject.Ressource[4].Hygiene += Trader4[1]; //TpAmount

                PlayerGiveRessources(0, Trader4[1], 0);
                break;

            default:
                break;
        }
        UpdateRessourceDescriptionText(0, houseObject.Ressource[0].Toiletpaper, houseObject.Ressource[0].Hygiene, houseObject.Ressource[0].Food);
        UpdateRessourceDescriptionText(Num, houseObject.Ressource[Num].Toiletpaper, houseObject.Ressource[Num].Hygiene, houseObject.Ressource[Num].Food);


        // restartTrade - Different Time(?)
        tradePerDay--;
        ChangeToTrade(false);
        ChangeDaytime();
    }

    public void TradeFood(int Num)
    {
        switch (Num)
        {
            case 1:
                houseObject.Ressource[1].Food += Trader1[2]; //TpAmount

                PlayerGiveRessources(0, 0, Trader1[2]);
                break;

            case 2:
                houseObject.Ressource[2].Food += Trader2[2]; //TpAmount

                PlayerGiveRessources(0, 0, Trader2[2]);
                break;

            case 3:
                houseObject.Ressource[3].Food += Trader3[2]; //TpAmount

                PlayerGiveRessources(0, 0, Trader3[2]);
                break;

            case 4:
                houseObject.Ressource[4].Food += Trader4[2]; //TpAmount

                PlayerGiveRessources(0, 0, Trader4[2]);
                break;

            default:
                break;
        }
        UpdateRessourceDescriptionText(0, houseObject.Ressource[0].Toiletpaper, houseObject.Ressource[0].Hygiene, houseObject.Ressource[0].Food);
        UpdateRessourceDescriptionText(Num, houseObject.Ressource[Num].Toiletpaper, houseObject.Ressource[Num].Hygiene, houseObject.Ressource[Num].Food);


        // restartTrade - Different Time(?)
        tradePerDay--;
        ChangeToTrade(false);
        ChangeDaytime();
    }

    // define trade (Amount)
    //public void Trade(int Num)
    //{
    //    switch (Num)
    //    {
    //        case 1:
    //            houseObject.Ressource[1].Toiletpaper += Trader1[0]; //TpAmount
    //            houseObject.Ressource[1].Hygiene += Trader1[1];     // HgAmount
    //            houseObject.Ressource[1].Food += Trader1[2];        // FoodAmount

    //            PlayerGiveRessources(Trader1[0], Trader1[1], Trader1[2]);
    //            //Debug.Log("TpAmount = " + Trader1[0] + ", HgAmount = " + Trader1[1] + ", FoodAmount = " + Trader1[2]);
    //            break;

    //        case 2:
    //            houseObject.Ressource[2].Toiletpaper += Trader2[0]; //TpAmount
    //            houseObject.Ressource[2].Hygiene += Trader2[1];     // HgAmount
    //            houseObject.Ressource[2].Food += Trader2[2];        // FoodAmount

    //            PlayerGiveRessources(Trader2[0], Trader2[1], Trader2[2]);
    //            //Debug.Log("Trader2: TpAmount = " + Trader2[0] + ", HgAmount = " + Trader2[1] + ", FoodAmount = " + Trader2[2]);
    //            break;

    //        case 3:
    //            houseObject.Ressource[3].Toiletpaper += Trader3[0]; //TpAmount
    //            houseObject.Ressource[3].Hygiene += Trader3[1];     // HgAmount
    //            houseObject.Ressource[3].Food += Trader3[2];        // FoodAmount

    //            PlayerGiveRessources(Trader3[0], Trader3[1], Trader3[2]);
    //            //Debug.Log("Trader3: TpAmount = " + Trader3[0] + ", HgAmount = " + Trader3[1] + ", FoodAmount = " + Trader3[2]);
    //            break;

    //        case 4:
    //            houseObject.Ressource[4].Toiletpaper += Trader4[0]; //TpAmount
    //            houseObject.Ressource[4].Hygiene += Trader4[1];     // HgAmount
    //            houseObject.Ressource[4].Food += Trader4[2];        // FoodAmount

    //            PlayerGiveRessources(Trader4[0], Trader4[1], Trader4[2]);
    //            //Debug.Log("Trader4: TpAmount = " + Trader4[0] + ", HgAmount = " + Trader4[1] + ", FoodAmount = " + Trader4[2]);
    //            break;

    //        default:
    //            break;
    //    }

    //    tradePerDay--;
    //    // restartTrade - Different Time(?)
    //    ChangeToTrade(false);
    //    ChangeDaytime();
    //}

    public void NeighborOverviewCanvas(bool change)
    {
        if (change == true)
        {
            QuestionCanvas.gameObject.SetActive(false);
            TradeView.gameObject.SetActive(false);
            NeighborOverview.gameObject.SetActive(true);
            FamilyStoryCanvas.gameObject.SetActive(false);
        }
        else if (change == false)
        {
            QuestionCanvas.gameObject.SetActive(true);
            TradeView.gameObject.SetActive(false);
            NeighborOverview.gameObject.SetActive(false);
            FamilyStoryCanvas.gameObject.SetActive(false);
        }

    }
    #endregion


    #region RessourceManagment
    // Reduce Ressources at end of the day
    public void RessourceReduce()
    {
        for (int i = 0; i < houseObject.Ressource.Length; i++)
        {
            if (i < 0)
            {
                houseObject.Ressource[i].Toiletpaper /= 2;
                houseObject.Ressource[i].Hygiene /= 2;
                houseObject.Ressource[i].Food /= 2;
            }

            if (i == 0)
            {
                if (houseObject.Ressource[i].Toiletpaper > 1)
                    houseObject.Ressource[i].Toiletpaper -= 1;
                if (houseObject.Ressource[i].Hygiene > 1) 
                    houseObject.Ressource[i].Hygiene -= 1;
                if (houseObject.Ressource[i].Food > 1) 
                    houseObject.Ressource[i].Food -= 1;
            }

            UpdateRessourceDescriptionText(i, houseObject.Ressource[i].Toiletpaper, houseObject.Ressource[i].Hygiene, houseObject.Ressource[i].Food);
            CheckForLostHouse(i);
        }
    }

    public void PlayerGiveRessources(int Tp, int Hg, int Fd)
    {
        houseObject.Ressource[0].Toiletpaper -= Tp;
        houseObject.Ressource[0].Hygiene -= Hg;
        houseObject.Ressource[0].Food -= Fd;

        UpdateRessourceDescriptionText(0, Tp, Hg, Fd);
    }

    public void UpdateRessourceDescriptionText(int Num, int Tp, int Hg, int Fd)
    {
        Tp = Mathf.Clamp(Tp, 0, 100);
        Hg = Mathf.Clamp(Hg, 0, 100);
        Fd = Mathf.Clamp(Fd, 0, 100);

        //DesTpText[Num].text = Tp.ToString();
        //DesHgText[Num].text = Hg.ToString();
        //DesFoodText[Num].text = Fd.ToString();
        DesTpText[Num].text = houseObject.Ressource[Num].Toiletpaper.ToString();
        DesHgText[Num].text = houseObject.Ressource[Num].Hygiene.ToString();
        DesFoodText[Num].text = houseObject.Ressource[Num].Food.ToString();

        CheckForLostHouse(Num);
    }
    #endregion


    #region DayRoutine
    // delivery -> Sound
    public void Delivery()
    {
        houseObject.Ressource[0].Toiletpaper += 3;
        houseObject.Ressource[0].Hygiene += 3;
        houseObject.Ressource[0].Food += 3;
        Debug.Log("DELIVERY !");
    }

    // changeDay -> Sound, animation
    public void ChangeDay(int day)
    {
        RessourceReduce();
        weekdays += day;

        switch (weekdays)
        {
            case 1:
                DayText.text = "Monday";
                Delivery();
                break;
            case 2:
                DayText.text = "Tuesday";
                break;
            case 3:
                DayText.text = "Wednesday";
                break;
            case 4:
                DayText.text = "Thursday ";
                Delivery();
                break;
            case 5:
                DayText.text = "Friday ";
                break;
            case 6:
                DayText.text = "Saturday ";
                break;
            case 7:
                DayText.text = "Sunday";
                break;

            default:
                break;
        }

        if (weekdays == 7)
        {
            week += 1;
            weekdays = 0;

            CheckWeekEnd();
        }
    }

    //change dayTime (picture/animation) -> Sound?
    public void ChangeDaytime()
    {
        if (tradePerDay == 3)
        {
            DayTimeText.text = "Morning";

            // change image/Gif
        }
        else if (tradePerDay == 2)
        {
            DayTimeText.text = "Afternoon";

            // change image/Gif
        }
        else if (tradePerDay == 1)
        {
            DayTimeText.text = "Evening";

            // change image/Gif
        }
        else
        {
            ChangeDay(1);
            DayTimeText.text = "Morning";
        }
    }
    #endregion

    public void ChangeToTrade(bool trade)
    {
        if (trade)
        {
            QuestionCanvas.gameObject.SetActive(false);
            TradeView.gameObject.SetActive(true);
            NeighborOverview.gameObject.SetActive(false);
        }
        else if (!trade)
        {
            QuestionCanvas.gameObject.SetActive(true);
            TradeView.gameObject.SetActive(false);
            NeighborOverview.gameObject.SetActive(false);
        }
    }

    #region Trade System & HouseState
    // Getting state and ressource amount of the houses
    public void HouseState(int houseNum)
    {
        switch (houseNum)
        {
            case 0:
                TpCount = houseObject.Ressource[0].Toiletpaper;
                HgCount = houseObject.Ressource[0].Hygiene;
                FoodCount = houseObject.Ressource[0].Food;
                //DesTpText[0].text = TpCount.ToString();
                //DesHgText[0].text = HgCount.ToString();
                //DesFoodText[0].text = FoodCount.ToString();
                UpdateRessourceDescriptionText(0, TpCount, HgCount, FoodCount);
                break;

            case 1:
                TpCount = houseObject.Ressource[1].Toiletpaper;
                HgCount = houseObject.Ressource[1].Hygiene;
                FoodCount = houseObject.Ressource[1].Food;
                //DesTpText[1].text = TpCount.ToString();
                //DesHgText[1].text = HgCount.ToString();
                //DesFoodText[1].text = FoodCount.ToString();
                UpdateRessourceDescriptionText(1, TpCount, HgCount, FoodCount);
                break;

            case 2:
                TpCount = houseObject.Ressource[2].Toiletpaper;
                HgCount = houseObject.Ressource[2].Hygiene;
                FoodCount = houseObject.Ressource[2].Food;
                //DesTpText[2].text = TpCount.ToString();
                //DesHgText[2].text = HgCount.ToString();
                //DesFoodText[2].text = FoodCount.ToString();
                UpdateRessourceDescriptionText(2, TpCount, HgCount, FoodCount);
                break;

            case 3:
                TpCount = houseObject.Ressource[3].Toiletpaper;
                HgCount = houseObject.Ressource[3].Hygiene;
                FoodCount = houseObject.Ressource[3].Food;
                //DesTpText[3].text = TpCount.ToString();
                //DesHgText[3].text = HgCount.ToString();
                //DesFoodText[3].text = FoodCount.ToString();
                UpdateRessourceDescriptionText(3, TpCount, HgCount, FoodCount);
                break;

            case 4:
                TpCount = houseObject.Ressource[4].Toiletpaper;
                HgCount = houseObject.Ressource[4].Hygiene;
                FoodCount = houseObject.Ressource[4].Food;
                //DesTpText[4].text = TpCount.ToString();
                //DesHgText[4].text = HgCount.ToString();
                //DesFoodText[4].text = FoodCount.ToString();
                UpdateRessourceDescriptionText(4, TpCount, HgCount, FoodCount);
                break;

            default:
                break;
        }
    }

    public void TradeRequest(int houseNum)
    {
        switch (houseNum)
        {
            case 1:
                randTradeRequest = Random.Range(1, 3);

                switch (randTradeRequest)
                {
                    case 1:
                        if (houseObject.Ressource[1].Toiletpaper < House1Limit)
                        {
                            TpAmount = GetRandomRessourceCount(2, houseObject.Ressource[0].Toiletpaper);
                            //Debug.Log("1: HighTrade - Tp");
                        }
                        else
                        {
                            TpAmount = GetRandomRessourceCount(0, 2);
                            //Debug.Log("1: LowTrade - Tp");
                        }
                        ToiletpaperText[0].text = TpAmount.ToString();
                        DTpText[0].gameObject.SetActive(true);
                        DHgText[0].gameObject.SetActive(false);
                        DFdText[0].gameObject.SetActive(false);
                        break;

                    case 2:
                        if (houseObject.Ressource[1].Hygiene < House1Limit)
                        {
                            HgAmount = GetRandomRessourceCount(2, houseObject.Ressource[0].Hygiene);
                            //Debug.Log("1: HighTrade - HG");
                        }
                        else
                        {
                            HgAmount = GetRandomRessourceCount(0, 2);
                            //Debug.Log("1: LowTrade - Hg");
                        }
                        HygienText[0].text = HgAmount.ToString();
                        DTpText[0].gameObject.SetActive(false);
                        DHgText[0].gameObject.SetActive(true);
                        DFdText[0].gameObject.SetActive(false);
                        break;

                    case 3:
                        if (houseObject.Ressource[1].Hygiene < House1Limit)
                        {
                            FoodAmount = GetRandomRessourceCount(2, houseObject.Ressource[0].Food);
                            //Debug.Log("1: HighTrade - Food");
                        }
                        else
                        {
                            FoodAmount = GetRandomRessourceCount(0, 2);
                            //Debug.Log("1: LowTrade - Food");
                        }
                        FoodText[0].text = FoodAmount.ToString();
                        DTpText[0].gameObject.SetActive(false);
                        DHgText[0].gameObject.SetActive(false);
                        DFdText[0].gameObject.SetActive(true);
                        break;

                    default:
                        break;
                }

                Trader1[0] = TpAmount;
                Trader1[1] = HgAmount;
                Trader1[2] = FoodAmount;

                break;

            case 2:
                randTradeRequest = Random.Range(1, 3);

                switch (randTradeRequest)
                {
                    case 1:
                        if (houseObject.Ressource[2].Toiletpaper < House2Limit)
                        {
                            TpAmount = GetRandomRessourceCount(2, houseObject.Ressource[0].Toiletpaper);
                            //Debug.Log("1: HighTrade - Tp");
                        }
                        else
                        {
                            TpAmount = GetRandomRessourceCount(0, 2);
                            //Debug.Log("1: LowTrade - Tp");
                        }
                        ToiletpaperText[1].text = TpAmount.ToString();
                        DTpText[1].gameObject.SetActive(true);
                        DHgText[1].gameObject.SetActive(false);
                        DFdText[1].gameObject.SetActive(false);
                        break;

                    case 2:
                        if (houseObject.Ressource[2].Hygiene < House2Limit)
                        {
                            HgAmount = GetRandomRessourceCount(2, houseObject.Ressource[0].Hygiene);
                            //Debug.Log("1: HighTrade - HG");
                        }
                        else
                        {
                            HgAmount = GetRandomRessourceCount(0, 2);
                            //Debug.Log("1: LowTrade - Hg");
                        }
                        HygienText[1].text = HgAmount.ToString();
                        DTpText[1].gameObject.SetActive(false);
                        DHgText[1].gameObject.SetActive(true);
                        DFdText[1].gameObject.SetActive(false);
                        break;

                    case 3:
                        if (houseObject.Ressource[2].Hygiene < House2Limit)
                        {
                            FoodAmount = GetRandomRessourceCount(2, houseObject.Ressource[0].Food);
                            //Debug.Log("1: HighTrade - Food");
                        }
                        else
                        {
                            FoodAmount = GetRandomRessourceCount(0, 2);
                            //Debug.Log("1: LowTrade - Food");
                        }
                        FoodText[1].text = FoodAmount.ToString();
                        DTpText[1].gameObject.SetActive(false);
                        DHgText[1].gameObject.SetActive(false);
                        DFdText[1].gameObject.SetActive(true);
                        break;

                    default:
                        break;
                }

                Trader2[0] = TpAmount;
                Trader2[1] = HgAmount;
                Trader2[2] = FoodAmount;

                break;

            case 3:
                randTradeRequest = Random.Range(1, 3);

                switch (randTradeRequest)
                {
                    case 1:
                        if (houseObject.Ressource[3].Toiletpaper < House3Limit)
                        {
                            TpAmount = GetRandomRessourceCount(2, houseObject.Ressource[0].Toiletpaper);
                            //Debug.Log("1: HighTrade - Tp");
                        }
                        else
                        {
                            TpAmount = GetRandomRessourceCount(0, 2);
                            //Debug.Log("1: LowTrade - Tp");
                        }
                        ToiletpaperText[2].text = TpAmount.ToString();
                        DTpText[2].gameObject.SetActive(true);
                        DHgText[2].gameObject.SetActive(false);
                        DFdText[2].gameObject.SetActive(false);
                        break;

                    case 2:
                        if (houseObject.Ressource[3].Hygiene < House3Limit)
                        {
                            HgAmount = GetRandomRessourceCount(2, houseObject.Ressource[0].Hygiene);
                            //Debug.Log("1: HighTrade - HG");
                        }
                        else
                        {
                            HgAmount = GetRandomRessourceCount(0, 2);
                            //Debug.Log("1: LowTrade - Hg");
                        }
                        HygienText[2].text = HgAmount.ToString();
                        DTpText[2].gameObject.SetActive(false);
                        DHgText[2].gameObject.SetActive(true);
                        DFdText[2].gameObject.SetActive(false);
                        break;

                    case 3:
                        if (houseObject.Ressource[3].Hygiene < House3Limit)
                        {
                            FoodAmount = GetRandomRessourceCount(2, houseObject.Ressource[0].Food);
                            //Debug.Log("1: HighTrade - Food");
                        }
                        else
                        {
                            FoodAmount = GetRandomRessourceCount(0, 2);
                            //Debug.Log("1: LowTrade - Food");
                        }
                        FoodText[2].text = FoodAmount.ToString();
                        DTpText[2].gameObject.SetActive(true);
                        DHgText[2].gameObject.SetActive(false);
                        DFdText[2].gameObject.SetActive(true);
                        break;

                    default:
                        break;
                }

                Trader3[0] = TpAmount;
                Trader3[1] = HgAmount;
                Trader3[2] = FoodAmount;

                break;

            case 4:
                randTradeRequest = Random.Range(1, 3);

                switch (randTradeRequest)
                {
                    case 1:
                        if (houseObject.Ressource[4].Toiletpaper < House4Limit)
                        {
                            TpAmount = GetRandomRessourceCount(2, houseObject.Ressource[0].Toiletpaper);
                            //Debug.Log("1: HighTrade - Tp");
                        }
                        else
                        {
                            TpAmount = GetRandomRessourceCount(0, 2);
                            //Debug.Log("1: LowTrade - Tp");
                        }
                        ToiletpaperText[3].text = TpAmount.ToString();
                        DTpText[3].gameObject.SetActive(true);
                        DHgText[3].gameObject.SetActive(false);
                        DFdText[3].gameObject.SetActive(false);
                        break;

                    case 2:
                        if (houseObject.Ressource[4].Hygiene < House4Limit)
                        {
                            HgAmount = GetRandomRessourceCount(2, houseObject.Ressource[0].Hygiene);
                            //Debug.Log("1: HighTrade - HG");
                        }
                        else
                        {
                            HgAmount = GetRandomRessourceCount(0, 2);
                            //Debug.Log("1: LowTrade - Hg");
                        }
                        HygienText[3].text = HgAmount.ToString();
                        DTpText[3].gameObject.SetActive(false);
                        DHgText[3].gameObject.SetActive(true);
                        DFdText[3].gameObject.SetActive(false);
                        break;

                    case 3:
                        if (houseObject.Ressource[4].Hygiene < House4Limit)
                        {
                            FoodAmount = GetRandomRessourceCount(2, houseObject.Ressource[0].Food);
                            //Debug.Log("1: HighTrade - Food");
                        }
                        else
                        {
                            FoodAmount = GetRandomRessourceCount(0, 2);
                            //Debug.Log("1: LowTrade - Food");
                        }
                        FoodText[3].text = FoodAmount.ToString();
                        DTpText[3].gameObject.SetActive(false);
                        DHgText[3].gameObject.SetActive(false);
                        DFdText[3].gameObject.SetActive(true);
                        break;

                    default:
                        break;
                }

                Trader4[0] = TpAmount;
                Trader4[1] = HgAmount;
                Trader4[2] = FoodAmount;

                break;

            default:
                break;
        }
    }
    #endregion


    // define if Hous is still there
    public void CheckForLostHouse(int Num)
    {
        if (houseObject.Ressource[Num].Toiletpaper <= 0 &&
        houseObject.Ressource[Num].Hygiene <= 0 &&
        houseObject.Ressource[Num].Food <= 0)
        {
            DesTpText[Num].gameObject.SetActive(false);
            DesHgText[Num].gameObject.SetActive(false);
            DesFoodText[Num].gameObject.SetActive(false);

            ButtonText[Num].gameObject.SetActive(false);

            houseObject.Ressource[Num].Dead = true;

            if (Num == 0)
            {
                SceneManager.LoadScene("Endscreen", LoadSceneMode.Single);
                Debug.Log("GAME OVER!");
            }
        }
        else
            return;
    }

    // check if game ends
    public void CheckWeekEnd()
    {
        if (week == 2)
        {
            SceneManager.LoadScene("EndScreen", LoadSceneMode.Single);
        }
        else
        {
            return;
        }
    }

    // getting random amount of ressources
    int GetRandomRessourceCount(int startNeed, int RessourceCount)
    {
        int Ressource = Random.Range(startNeed, RessourceCount);

        return Ressource;
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("EndScreen", LoadSceneMode.Single);
    }
}
