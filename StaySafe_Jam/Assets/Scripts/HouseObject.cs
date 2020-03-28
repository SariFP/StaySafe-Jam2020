using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/HouseObject", order = 1)]
public class HouseObject : ScriptableObject
{
    [Serializable]
    public struct Ressources
    {
        public int houseNumber;
        public int Toiletpaper;
        public int Hygiene;
        public int Food;

        public bool Dead;
    }

    public Ressources[] Ressource;
}
