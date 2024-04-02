using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tag : MonoBehaviour
{
    [SerializeField] private TMP_Text _tagName;

    public void SetTagName(string name)
    {
        _tagName.text = name;
    }
}
