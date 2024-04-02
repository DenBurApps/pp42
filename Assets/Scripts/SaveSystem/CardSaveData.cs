using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class CardSaveData : SaveData
{
    public List<CardData> Cards { get; set; }

    public CardSaveData(List<CardData> cards)
    {
        Cards = cards;
    }
}
