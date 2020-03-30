using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class TradeSystem : MonoBehaviour
{
    public HouseObject houseObject;
    private ButtonManagment buttonManager;

    [Space(5)]
    [Header("Backgrounds")]
    public GameObject LevelSkeleton;
    public GameObject DayBackground;
    public GameObject EveningBackground;

    [Space(5)]
    [Header("DeliveryText")]
    public GameObject DeliverText;

    [Space(5)]
    [Header("Animations")]
    public Animator FadeScreen;

    [Space(5)]
    [Header("DeliveryVideo")]
    public RawImage VideoRawImage;
    public VideoPlayer videoPlayer;
    public AudioSource videoAudio;
    private AudioSource mainAudio;

    [Space(5)]
    [Header("Sounds")]
    public AudioClip DeliverySound;
    public AudioClip ClickSound;
    public AudioClip DayBeginSound;
    public AudioClip DayEndSound;

    [Space(5)]
    [Header("Canvas Panel")]
    public Canvas HandyCanvas;
    public Canvas QuestionCanvas;
    public Canvas TradeView;
    public Canvas NeighborOverview;
    public Canvas FamilyStoryCanvas;

    [Space(5)]
    [Header("FairTradeButton")]
    public Sprite ToilettpaperIcon;
    public Sprite HygieneIcon;
    public Sprite FoodIcon;
    public Image[] FairTradeButton;

    [Space(5)]
    [Header("Text Fields")]
    public TMP_Text DayTimeText;
    public TMP_Text DayText;
    [Header("Description")]
    public TMP_Text[] DesTpText;
    public TMP_Text[] DesHgText;
    public TMP_Text[] DesFoodText;
    [Header("AddAmount")]
    public TMP_Text[] ToiletpaperText;
    public TMP_Text[] HygieneText;
    public TMP_Text[] FoodText;
    [Space(5)]
    //[Header("Button")]
    //public TMP_Text[] ButtonText;


    [Space(5)]
    [Header("House Limits")]
    public int House1Limit = 2;
    public int House2Limit = 2;
    public int House3Limit = 2;
    public int House4Limit = 2;

    //private bool DoTrade = false;
    public int tradePerDay = 3;
    private int weekdays = 1;
    private int week = 0;
    private int deadCount = 0;
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
        buttonManager = GetComponent<ButtonManagment>();
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
        DayBackground.SetActive(true);
        EveningBackground.SetActive(true);
        mainAudio = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioSource>();
        HandyCanvas.gameObject.SetActive(false);
        DeliverText.SetActive(false);
        ChangeDay(0);
        ChangeDaytime();

        for (int i = 0; i <= houseObject.Ressource.Length; i++)
        {
            HouseState(i);
        }
    }

    // gameplay dayroutine
    public void TradeRoutine()
    {
        buttonManager.ChangeToTrade(true);

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
 
    public void TradeTp(int Num)
    {
        switch (Num)
        {
            case 1:
                houseObject.Ressource[1].Toiletpaper += Trader1[0]; //TpAmount
                houseObject.Ressource[0].Toiletpaper -= Trader1[0];
                //PlayerGiveRessources(Trader1[0], 0, 0);

                if (FairTradeButton[1].sprite == HygieneIcon)
                {
                    houseObject.Ressource[0].Hygiene += 1;
                    houseObject.Ressource[1].Hygiene -= 1;
                }
                else if (FairTradeButton[1].sprite == FoodIcon)
                {
                    houseObject.Ressource[0].Food += 1;
                    houseObject.Ressource[1].Food -= 1;
                }

                break;

            case 2:
                houseObject.Ressource[2].Toiletpaper += Trader2[0]; //TpAmount
                houseObject.Ressource[0].Toiletpaper -= Trader2[0];
                //PlayerGiveRessources(Trader2[0], 0, 0);

                if (FairTradeButton[2].sprite == HygieneIcon)
                {
                    houseObject.Ressource[0].Hygiene += 1;
                    houseObject.Ressource[2].Hygiene -= 1;
                }
                else if (FairTradeButton[2].sprite == FoodIcon)
                {
                    houseObject.Ressource[0].Food += 1;
                    houseObject.Ressource[2].Food -= 1;
                }

                break;

            case 3:
                houseObject.Ressource[3].Toiletpaper += Trader3[0]; //TpAmount
                houseObject.Ressource[0].Toiletpaper -= Trader3[0];
                //PlayerGiveRessources(Trader3[0], 0, 0);

                if (FairTradeButton[3].sprite == HygieneIcon)
                {
                    houseObject.Ressource[0].Hygiene += 1;
                    houseObject.Ressource[3].Hygiene -= 1;
                }
                else if (FairTradeButton[3].sprite == FoodIcon)
                {
                    houseObject.Ressource[0].Food += 1;
                    houseObject.Ressource[3].Food -= 1;
                }

                break;

            case 4:
                houseObject.Ressource[4].Toiletpaper += Trader4[0]; //TpAmount
                houseObject.Ressource[0].Toiletpaper -= Trader4[0];
                //PlayerGiveRessources(Trader4[0], 0, 0);

                if (FairTradeButton[4].sprite == HygieneIcon)
                {
                    houseObject.Ressource[0].Hygiene += 1;
                    houseObject.Ressource[4].Hygiene -= 1;
                }
                else if (FairTradeButton[4].sprite == FoodIcon)
                {
                    houseObject.Ressource[0].Food += 1;
                    houseObject.Ressource[4].Food -= 1;
                }

                break;

            default:
                break;
        }
        UpdateRessourceDescriptionText(0, houseObject.Ressource[0].Toiletpaper, houseObject.Ressource[0].Hygiene, houseObject.Ressource[0].Food);
        UpdateRessourceDescriptionText(Num, houseObject.Ressource[Num].Toiletpaper, houseObject.Ressource[Num].Hygiene, houseObject.Ressource[Num].Food);

        //StartCoroutine(PlaySoundOnce(ClickSound));
        StartCoroutine(PlaySoundOnce(ClickSound));

        // restartTrade - Different Time
        StartCoroutine(FadeScreenToggle(false));
        tradePerDay--;
        buttonManager.ChangeToTrade(false);
        ChangeDaytime();
    }

    public void TradeHg(int Num)
    {
        switch (Num)
        {
            case 1:
                houseObject.Ressource[1].Hygiene += Trader1[1]; //HgAmount
                houseObject.Ressource[0].Hygiene -= Trader1[1];
                //PlayerGiveRessources(0, Trader1[1], 0);

                if (FairTradeButton[0].sprite == ToilettpaperIcon)
                {
                    houseObject.Ressource[0].Toiletpaper += 1;
                    houseObject.Ressource[1].Toiletpaper -= 1;
                }
                else if (FairTradeButton[0].sprite == FoodIcon)
                {
                    houseObject.Ressource[0].Food += 1;
                    houseObject.Ressource[1].Food -= 1;
                }

                break;

            case 2:
                houseObject.Ressource[2].Hygiene += Trader2[1]; //HgAmount
                houseObject.Ressource[0].Hygiene -= Trader2[1];

                //PlayerGiveRessources(0, Trader2[1], 0);

                if (FairTradeButton[1].sprite == ToilettpaperIcon)
                {
                    houseObject.Ressource[0].Toiletpaper += 1;
                    houseObject.Ressource[2].Toiletpaper -= 1;
                }
                else if (FairTradeButton[1].sprite == FoodIcon)
                {
                    houseObject.Ressource[0].Food += 1;
                    houseObject.Ressource[2].Food -= 1;
                }

                break;

            case 3:
                houseObject.Ressource[3].Hygiene += Trader3[1]; //HgAmount
                houseObject.Ressource[0].Hygiene -= Trader3[1];
                //PlayerGiveRessources(0, Trader3[1], 0);

                if (FairTradeButton[2].sprite == ToilettpaperIcon)
                {
                    houseObject.Ressource[0].Toiletpaper += 1;
                    houseObject.Ressource[3].Toiletpaper -= 1;
                }
                else if (FairTradeButton[2].sprite == FoodIcon)
                {
                    houseObject.Ressource[0].Food += 1;
                    houseObject.Ressource[3].Food -= 1;
                }

                break;

            case 4:
                houseObject.Ressource[4].Hygiene += Trader4[1]; //HgAmount
                houseObject.Ressource[0].Hygiene -= Trader4[1];
                //PlayerGiveRessources(0, Trader4[1], 0);

                if (FairTradeButton[3].sprite == ToilettpaperIcon)
                {
                    houseObject.Ressource[0].Toiletpaper += 1;
                    houseObject.Ressource[4].Toiletpaper -= 1;
                }
                else if (FairTradeButton[3].sprite == FoodIcon)
                {
                    houseObject.Ressource[0].Food += 1;
                    houseObject.Ressource[4].Food -= 1;
                }

                break;

            default:
                break;
        }
        UpdateRessourceDescriptionText(0, houseObject.Ressource[0].Toiletpaper, houseObject.Ressource[0].Hygiene, houseObject.Ressource[0].Food);
        UpdateRessourceDescriptionText(Num, houseObject.Ressource[Num].Toiletpaper, houseObject.Ressource[Num].Hygiene, houseObject.Ressource[Num].Food);

        //StartCoroutine(PlaySoundOnce(ClickSound));
        StartCoroutine(PlaySoundOnce(ClickSound));

        // restartTrade - Different Time
        StartCoroutine(FadeScreenToggle(false));
        tradePerDay--;
        buttonManager.ChangeToTrade(false);
        ChangeDaytime();
    }

    public void TradeFood(int Num)
    {
        switch (Num)
        {
            case 1:
                houseObject.Ressource[1].Food += Trader1[2]; //FdAmount
                houseObject.Ressource[0].Food -= Trader1[2];
                //PlayerGiveRessources(0, 0, Trader1[2]);

                if (FairTradeButton[0].sprite == ToilettpaperIcon)
                {
                    houseObject.Ressource[0].Toiletpaper += 1;
                    houseObject.Ressource[1].Toiletpaper -= 1;
                }
                else if (FairTradeButton[0].sprite == HygieneIcon)
                {
                    houseObject.Ressource[0].Hygiene += 1;
                    houseObject.Ressource[1].Hygiene -= 1;
                }

                break;

            case 2:
                houseObject.Ressource[2].Food += Trader2[2]; //FdAmount
                houseObject.Ressource[0].Food -= Trader2[2];
                //PlayerGiveRessources(0, 0, Trader2[2]);

                if (FairTradeButton[1].sprite == ToilettpaperIcon)
                {
                    houseObject.Ressource[0].Toiletpaper += 1;
                    houseObject.Ressource[2].Toiletpaper -= 1;
                }
                else if (FairTradeButton[1].sprite == HygieneIcon)
                {
                    houseObject.Ressource[0].Hygiene += 1;
                    houseObject.Ressource[2].Hygiene -= 1;
                }

                break;

            case 3:
                houseObject.Ressource[3].Food += Trader3[2]; //FdAmount
                houseObject.Ressource[0].Food -= Trader3[2];
                //PlayerGiveRessources(0, 0, Trader3[2]);

                if (FairTradeButton[2].sprite == ToilettpaperIcon)
                {
                    houseObject.Ressource[0].Toiletpaper += 1;
                    houseObject.Ressource[3].Toiletpaper -= 1;
                }
                else if (FairTradeButton[2].sprite == HygieneIcon)
                {
                    houseObject.Ressource[0].Hygiene += 1;
                    houseObject.Ressource[3].Hygiene -= 1;
                }

                break;

            case 4:
                houseObject.Ressource[4].Food += Trader4[2]; //FdAmount
                houseObject.Ressource[0].Food -= Trader4[2];
                //PlayerGiveRessources(0, 0, Trader4[2]);

                if (FairTradeButton[3].sprite == ToilettpaperIcon)
                {
                    houseObject.Ressource[0].Toiletpaper += 1;
                    houseObject.Ressource[4].Toiletpaper -= 1;
                }
                else if (FairTradeButton[3].sprite == HygieneIcon)
                {
                    houseObject.Ressource[0].Hygiene += 1;
                    houseObject.Ressource[4].Hygiene -= 1;
                }

                break;

            default:
                break;
        }
        UpdateRessourceDescriptionText(0, houseObject.Ressource[0].Toiletpaper, houseObject.Ressource[0].Hygiene, houseObject.Ressource[0].Food);
        UpdateRessourceDescriptionText(Num, houseObject.Ressource[Num].Toiletpaper, houseObject.Ressource[Num].Hygiene, houseObject.Ressource[Num].Food);

        //StartCoroutine(PlaySoundOnce(ClickSound));
        StartCoroutine(PlaySoundOnce(ClickSound));

        // restartTrade - Different Time
        StartCoroutine(FadeScreenToggle(false));
        tradePerDay--;
        buttonManager.ChangeToTrade(false);
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

    #endregion


    #region RessourceManagment
    // Reduce Ressources at end of the day
    public void RessourceReduce()
    {
        for (int i = 0; i < houseObject.Ressource.Length; i++)
        {
            if (i > 0)
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

    //public void PlayerGiveRessources(int Tp, int Hg, int Fd)
    //{
    //    houseObject.Ressource[0].Toiletpaper -= Tp;
    //    houseObject.Ressource[0].Hygiene -= Hg;
    //    houseObject.Ressource[0].Food -= Fd;

    //    UpdateRessourceDescriptionText(0, Tp, Hg, Fd);
    //}

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

        Debug.Log("LoadVideo");
        StartCoroutine(StartVideo());
        //StartCoroutine(PlaySoundOnce(DeliverySound));

        //if (weekdays == 0)
        //{
        //    StartCoroutine(ShowCustomText("The delivery service makes their round in your area every Monday. As you’re the only one not immediately at risk, the responsibility to distribute the resources rests on you."));
        //}
        //else
        //    StartCoroutine(ShowDeliveryText());
    }

    //IEnumerator ShowDeliveryText()
    //{
    //    int randText = Random.Range(0, 2);

    //    switch (randText)
    //    {
    //        case 1:
    //            DeliverText.SetActive(true);
    //            DeliverText.GetComponentInChildren<TMP_Text>().text = "Your weekly delivery has arrived.";
    //            break;
    //        case 2:
    //            DeliverText.SetActive(true);
    //            DeliverText.GetComponentInChildren<TMP_Text>().text = "Najor has placed your weekly delivery on your porch.It has been automatically added to your inventory.";
    //            break;
    //        default:
    //            break;
    //    }

    //    yield return new WaitForSeconds(10f);
    //    DeliverText.SetActive(false);
    //}

    // changeDay -> animation
    public void ChangeDay(int day)
    {
        if (day != 0)
            RessourceReduce();
        weekdays += day;

        StartCoroutine(PlaySoundOnce(DayBeginSound));

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
            StartCoroutine(FadeScreenToggle(true));
        
    }


    // House stats Text ?
    //public void CheckHouseStats()
    //{
    //    if (houseObject.Ressource[1].Toiletpaper < House1Limit ||
    //        houseObject.Ressource[1].Hygiene < House1Limit) ;
    //}

    //change dayTime (picture/animation) -> Sound?
    public void ChangeDaytime()
    {
        if (tradePerDay == 3)
        {
            DayTimeText.text = "Morning";
            DayBackground.SetActive(true);
            EveningBackground.SetActive(false);
            StartCoroutine(FadeScreenToggle(true));

            // change image/Gif
        }
        else if (tradePerDay == 2)
        {
            DayTimeText.text = "Afternoon";
            DayBackground.SetActive(true);
            EveningBackground.SetActive(false);
            StartCoroutine(FadeScreenToggle(true));

            // change image/Gif
        }
        else if (tradePerDay == 1)
        {
            DayTimeText.text = "Evening";
            DayBackground.SetActive(false);
            EveningBackground.SetActive(true);
            StartCoroutine(ShowCustomText("It’s getting late. I should go to bed soon."));
            StartCoroutine(FadeScreenToggle(true));
            StartCoroutine(PlaySoundOnce(DayEndSound));

            // change image/Gif
        }
        else
        {
            StartCoroutine(FadeScreenToggle(false));
            ChangeDay(1);
            DayTimeText.text = "Morning";
            DayBackground.SetActive(true);
            EveningBackground.SetActive(false);
        }
    }
    #endregion


    #region Sounds&FadeScreen
    public IEnumerator FadeScreenToggle(bool fadeIn)
    {
        if (fadeIn)
        {
            FadeScreen.gameObject.SetActive(true);
            FadeScreen.SetBool("FadeOut", false);
            FadeScreen.SetBool("FadeIn", true);

            QuestionCanvas.gameObject.SetActive(true);
            TradeView.gameObject.SetActive(false);
            NeighborOverview.gameObject.SetActive(false);
            FamilyStoryCanvas.gameObject.SetActive(false);

            yield return new WaitForSeconds(5);
            FadeScreen.SetBool("FadeIn", false);
            FadeScreen.gameObject.SetActive(false);

            if (!videoPlayer.isPlaying)
            {
                HandyCanvas.gameObject.SetActive(true);
            }
        }
        else if (!fadeIn)
        {
            FadeScreen.gameObject.SetActive(true);
            FadeScreen.SetBool("FadeIn", false);
            FadeScreen.SetBool("FadeOut", true);

            QuestionCanvas.gameObject.SetActive(false);
            TradeView.gameObject.SetActive(false);
            NeighborOverview.gameObject.SetActive(false);
            FamilyStoryCanvas.gameObject.SetActive(false);

            yield return new WaitForSeconds(5);
            FadeScreen.SetBool("FadeOut", false);
            FadeScreen.gameObject.SetActive(false);
            HandyCanvas.gameObject.SetActive(false);
        }
    }

    // plays sounds once
    public IEnumerator PlaySoundOnce(AudioClip clipSrc)
    {
        AudioSource audioSrc = GetComponent<AudioSource>();

        if (audioSrc.isPlaying)
        {
            yield return new WaitForSeconds(audioSrc.clip.length);
            audioSrc.PlayOneShot(clipSrc);
        }
        else if (!audioSrc.isPlaying)
            audioSrc.PlayOneShot(clipSrc);
    }

    #endregion


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

    // defines if Player gets Hg or Fd back when Tp is needed
    public void TradeHygieneFood(int houseNum)
    {
        int random = Random.Range(0, 2);

        if (houseObject.Ressource[houseNum].Hygiene > houseObject.Ressource[houseNum].Food ||
            houseObject.Ressource[houseNum].Hygiene == houseObject.Ressource[houseNum].Food && random == 0)
        {
            FairTradeButton[houseNum-1].sprite = HygieneIcon;
        }
        else if (houseObject.Ressource[houseNum].Food > houseObject.Ressource[houseNum].Hygiene ||
            houseObject.Ressource[houseNum].Hygiene == houseObject.Ressource[houseNum].Food && random == 1)
        {
            FairTradeButton[houseNum-1].sprite = FoodIcon;
        }
    }

    // defines if Player gets Tp or Fd back when Hg is needed
    public void TradeToiletpaperFood(int houseNum)
    {
        int random = Random.Range(0, 2);

        if (houseObject.Ressource[houseNum].Toiletpaper > houseObject.Ressource[houseNum].Food ||
            houseObject.Ressource[houseNum].Toiletpaper == houseObject.Ressource[houseNum].Food && random == 0)
        {
            FairTradeButton[houseNum-1].sprite = ToilettpaperIcon;
        }
        else if (houseObject.Ressource[houseNum].Food > houseObject.Ressource[houseNum].Toiletpaper ||
            houseObject.Ressource[houseNum].Toiletpaper == houseObject.Ressource[houseNum].Food && random == 1)
        {
            FairTradeButton[houseNum-1].sprite = FoodIcon;
        }
    }

    // defines if Player gets Tp or Hg back when Fd is needed
    public void TradeToiletpaperHygiene(int houseNum)
    {
        int random = Random.Range(0, 2);

        if (houseObject.Ressource[houseNum].Toiletpaper > houseObject.Ressource[houseNum].Hygiene ||
            houseObject.Ressource[houseNum].Toiletpaper == houseObject.Ressource[houseNum].Hygiene && random == 0)
        {
            FairTradeButton[houseNum-1].sprite = ToilettpaperIcon;
        }
        else if (houseObject.Ressource[houseNum].Hygiene > houseObject.Ressource[houseNum].Toiletpaper ||
            houseObject.Ressource[houseNum].Toiletpaper == houseObject.Ressource[houseNum].Hygiene && random == 1)
        {
            FairTradeButton[houseNum-1].sprite = HygieneIcon;
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

                        TradeHygieneFood(1);

                        ToiletpaperText[0].text = TpAmount.ToString();
                        ToiletpaperText[0].gameObject.SetActive(true);
                        HygieneText[0].gameObject.SetActive(false);
                        FoodText[0].gameObject.SetActive(false);
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

                        TradeToiletpaperFood(1);

                        HygieneText[0].text = HgAmount.ToString();
                        ToiletpaperText[0].gameObject.SetActive(false);
                        HygieneText[0].gameObject.SetActive(true);
                        FoodText[0].gameObject.SetActive(false);
                        break;

                    case 3:
                        if (houseObject.Ressource[1].Food < House1Limit)
                        {
                            FoodAmount = GetRandomRessourceCount(2, houseObject.Ressource[0].Food);
                            //Debug.Log("1: HighTrade - Food");
                        }
                        else
                        {
                            FoodAmount = GetRandomRessourceCount(0, 2);
                            //Debug.Log("1: LowTrade - Food");
                        }

                        TradeToiletpaperHygiene(1);

                        FoodText[0].text = FoodAmount.ToString();
                        ToiletpaperText[0].gameObject.SetActive(false);
                        HygieneText[0].gameObject.SetActive(false);
                        FoodText[0].gameObject.SetActive(true);
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

                        TradeHygieneFood(2);

                        ToiletpaperText[1].text = TpAmount.ToString();
                        ToiletpaperText[1].gameObject.SetActive(true);
                        HygieneText[1].gameObject.SetActive(false);
                        FoodText[1].gameObject.SetActive(false);
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

                        TradeToiletpaperFood(2);

                        this.HygieneText[1].text = HgAmount.ToString();
                        ToiletpaperText[1].gameObject.SetActive(false);
                        HygieneText[1].gameObject.SetActive(true);
                        FoodText[1].gameObject.SetActive(false);
                        break;

                    case 3:
                        if (houseObject.Ressource[2].Food < House2Limit)
                        {
                            FoodAmount = GetRandomRessourceCount(2, houseObject.Ressource[0].Food);
                            //Debug.Log("1: HighTrade - Food");
                        }
                        else
                        {
                            FoodAmount = GetRandomRessourceCount(0, 2);
                            //Debug.Log("1: LowTrade - Food");
                        }

                        TradeToiletpaperHygiene(2);

                        FoodText[1].text = FoodAmount.ToString();
                        ToiletpaperText[1].gameObject.SetActive(false);
                        HygieneText[1].gameObject.SetActive(false);
                        FoodText[1].gameObject.SetActive(true);
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

                        TradeHygieneFood(3);

                        ToiletpaperText[2].text = TpAmount.ToString();
                        ToiletpaperText[2].gameObject.SetActive(true);
                        HygieneText[2].gameObject.SetActive(false);
                        FoodText[2].gameObject.SetActive(false);
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

                        TradeToiletpaperFood(3);

                        HygieneText[2].text = HgAmount.ToString();
                        ToiletpaperText[2].gameObject.SetActive(false);
                        HygieneText[2].gameObject.SetActive(true);
                        FoodText[2].gameObject.SetActive(false);
                        break;

                    case 3:
                        if (houseObject.Ressource[3].Food < House3Limit)
                        {
                            FoodAmount = GetRandomRessourceCount(2, houseObject.Ressource[0].Food);
                            //Debug.Log("1: HighTrade - Food");
                        }
                        else
                        {
                            FoodAmount = GetRandomRessourceCount(0, 2);
                            //Debug.Log("1: LowTrade - Food");
                        }

                        TradeToiletpaperHygiene(3);

                        FoodText[2].text = FoodAmount.ToString();
                        ToiletpaperText[2].gameObject.SetActive(true);
                        HygieneText[2].gameObject.SetActive(false);
                        FoodText[2].gameObject.SetActive(true);
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

                        TradeHygieneFood(4);

                        ToiletpaperText[3].text = TpAmount.ToString();
                        ToiletpaperText[3].gameObject.SetActive(true);
                        HygieneText[3].gameObject.SetActive(false);
                        FoodText[3].gameObject.SetActive(false);
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

                        TradeToiletpaperFood(4);

                        HygieneText[3].text = HgAmount.ToString();
                        ToiletpaperText[3].gameObject.SetActive(false);
                        HygieneText[3].gameObject.SetActive(true);
                        FoodText[3].gameObject.SetActive(false);
                        break;

                    case 3:
                        if (houseObject.Ressource[4].Food < House4Limit)
                        {
                            FoodAmount = GetRandomRessourceCount(2, houseObject.Ressource[0].Food);
                            //Debug.Log("1: HighTrade - Food");
                        }
                        else
                        {
                            FoodAmount = GetRandomRessourceCount(0, 2);
                            //Debug.Log("1: LowTrade - Food");
                        }

                        TradeToiletpaperHygiene(4);

                        FoodText[3].text = FoodAmount.ToString();
                        ToiletpaperText[3].gameObject.SetActive(false);
                        HygieneText[3].gameObject.SetActive(false);
                        FoodText[3].gameObject.SetActive(true);
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

            //ButtonText[Num].gameObject.SetActive(false);

            houseObject.Ressource[Num].Dead = true;

            deadCount++;

            if (houseObject.Ressource[1].Dead)
            {
                StartCoroutine(ShowCustomText("Waltraud Residence ran out of resources."));
            }
            else if (houseObject.Ressource[2].Dead)
            {
                StartCoroutine(ShowCustomText("Kröger Residence ran out of resources."));
            }
            else if (houseObject.Ressource[3].Dead)
            {
                StartCoroutine(ShowCustomText(" Roi Residence ran out of resources."));
            }
            else if (houseObject.Ressource[4].Dead)
            {
                StartCoroutine(ShowCustomText("Von Butz Residence ran out of resources."));
            }

            if (deadCount >= 4)
            {
                Debug.Log("GAME OVER!");
                SceneManager.LoadScene("Endscreen", LoadSceneMode.Single);
            }

            if (Num == 0)
            {
                Debug.Log("GAME OVER!");
                SceneManager.LoadScene("Endscreen", LoadSceneMode.Single);
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

    IEnumerator ShowCustomText(string text)
    {
        DeliverText.SetActive(true);
        DeliverText.GetComponentInChildren<TMP_Text>().text = text;
        yield return new WaitForSeconds(8f);
        DeliverText.SetActive(false);
    }

    IEnumerator StartVideo()
    {
        HandyCanvas.gameObject.SetActive(false);
        VideoRawImage.gameObject.SetActive(true);
        mainAudio.Stop();
        videoPlayer.Prepare();
        WaitForSeconds wait = new WaitForSeconds(1);
        while (!videoPlayer.isPrepared)
        {
            yield return wait;
            break;
        }
        VideoRawImage.texture = videoPlayer.texture;
        videoPlayer.Play();
        videoAudio.Play();

        yield return new WaitForSeconds(26);
        mainAudio.Play();
        VideoRawImage.gameObject.SetActive(false);
        HandyCanvas.gameObject.SetActive(true);
    }
}
