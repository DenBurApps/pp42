using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class CardData 
{
    public bool IsFavorite { get; set; }
    public string Name { get; set; }   
    public string Surname { get; set; }
    public string Job { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Telegram { get; set; }
    public List<LinkData> Links { get; set; } = new List<LinkData>();
    public List<string> Tags { get; set; } = new List<string>();
    public int DesignIndex { get; set; }
    public int AvatarIndex { get; set; }

}
