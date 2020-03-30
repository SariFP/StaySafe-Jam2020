using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManagment : MonoBehaviour
{
    [Space(5)]
    [Header("FamilyStories")]
    public Image[] FamilyStory;

    private TradeSystem tradeSys;

    private void Start()
    {
        tradeSys = GetComponent<TradeSystem>();
    }

    public void FamilyStoryButton(int Num)
    {
        tradeSys.FamilyStoryCanvas.gameObject.SetActive(true);
        tradeSys.QuestionCanvas.gameObject.SetActive(false);
        tradeSys.TradeView.gameObject.SetActive(false);
        tradeSys.NeighborOverview.gameObject.SetActive(false);

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
        //tradeSys.StartCoroutine(PlaySoundOnce(tradeSys.ClickSound));
        StartCoroutine(tradeSys.PlaySoundOnce(tradeSys.ClickSound));
    }

    // change to neighbor overview canvas
    public void NeighborOverviewCanvas(bool change)
    {
        if (change == true)
        {
            tradeSys.QuestionCanvas.gameObject.SetActive(false);
            tradeSys.TradeView.gameObject.SetActive(false);
            tradeSys.NeighborOverview.gameObject.SetActive(true);
            tradeSys.FamilyStoryCanvas.gameObject.SetActive(false);
        }
        else if (change == false)
        {
            tradeSys.QuestionCanvas.gameObject.SetActive(true);
            tradeSys.TradeView.gameObject.SetActive(false);
            tradeSys.NeighborOverview.gameObject.SetActive(false);
            tradeSys.FamilyStoryCanvas.gameObject.SetActive(false);
        }
        //StartCoroutine(PlaySoundOnce(ClickSound));
        StartCoroutine(tradeSys.PlaySoundOnce(tradeSys.ClickSound));
    }

    public void QuestionTrade(bool trade)
    {
        //StartCoroutine(PlaySoundOnce(ClickSound));
        StartCoroutine(tradeSys.PlaySoundOnce(tradeSys.ClickSound));
        if (trade == true)
        {
            tradeSys.TradeRoutine();
        }
        if (trade == false)
        {
            tradeSys.tradePerDay--;
            StartCoroutine(tradeSys.FadeScreenToggle(false));
            
            tradeSys.ChangeDaytime();
        }
    }

    // change to trade view canvas
    public void ChangeToTrade(bool trade)
    {
        if (trade)
        {
            tradeSys.QuestionCanvas.gameObject.SetActive(false);
            tradeSys.TradeView.gameObject.SetActive(true);
            tradeSys.NeighborOverview.gameObject.SetActive(false);
        }
        else if (!trade)
        {
            tradeSys.QuestionCanvas.gameObject.SetActive(true);
            tradeSys.TradeView.gameObject.SetActive(false);
            tradeSys.NeighborOverview.gameObject.SetActive(false);
        }
    }


    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);

        StartCoroutine(tradeSys.PlaySoundOnce(tradeSys.ClickSound));
    }

    public void BackToQuestionCanvas()
    {
        tradeSys.QuestionCanvas.gameObject.SetActive(true);
        tradeSys.TradeView.gameObject.SetActive(false);
        tradeSys.NeighborOverview.gameObject.SetActive(false);

        StartCoroutine(tradeSys.PlaySoundOnce(tradeSys.ClickSound));
    }
}
