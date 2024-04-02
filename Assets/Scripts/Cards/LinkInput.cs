using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LinkInput : MonoBehaviour
{
    [SerializeField] private TMP_InputField _linkName;
    [SerializeField] private TMP_InputField _link;

    public string GetLinkName()
    {
        return _linkName.text;
    }

    public string GetLink()
    {
        return _link.text;
    }

    public void SetLinkName(string name)
    {
        _linkName.text = name;
    }

    public void SetLink(string link)
    {
        _link.text = link;
    }

    public void ResetLink()
    {
        _linkName.text = "";
        _link.text = "";
    }
}
