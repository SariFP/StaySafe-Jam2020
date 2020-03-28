using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HouseRessources : MonoBehaviour
{
    public HouseObject houseObject;
    public int HouseNumber;

    private int TpCount;
    private int HgCount;
    private int FoodCount;

    private int minResources = 2;
    private int startRessources = 4;

    void Awake()
    {
        TpCount = Random.Range(minResources, startRessources);
        HgCount = Random.Range(minResources, startRessources);
        FoodCount = Random.Range(minResources, startRessources);
    }

    void Start()
    {
        UpdateRessources(TpCount, HgCount, FoodCount, false);
    }

    public void UpdateRessources(int Tp, int Hg, int Fd, bool dead)
    {
        houseObject.Ressource[HouseNumber].houseNumber = HouseNumber;
        houseObject.Ressource[HouseNumber].Toiletpaper = Tp;
        houseObject.Ressource[HouseNumber].Hygiene = Hg;
        houseObject.Ressource[HouseNumber].Food = Fd;
        houseObject.Ressource[HouseNumber].Dead = dead;
    }

}
