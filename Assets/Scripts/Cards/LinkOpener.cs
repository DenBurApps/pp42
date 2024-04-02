using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LinkOpener : MonoBehaviour
{
    [SerializeField] private Button _openLinkButton;
    [SerializeField] private TMP_Text _linkName;
    private string _currentLink;

    private void OnDisable()
    {
        _openLinkButton.onClick.RemoveAllListeners();
    }

    public void SetLink(string name, string url)
    {
        _linkName.text = name;
        if (!url.Contains("http"))
        {
            _currentLink = "https://" + url;
        }
        else
        {
            _currentLink = url;
        }
        _openLinkButton.onClick.AddListener(OpenLink);
    }

    public void OpenLink()
    {
        Application.OpenURL(_currentLink);
    }
}
