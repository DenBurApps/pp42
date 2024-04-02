using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplayer : MonoBehaviour
{
    [SerializeField] private Navigator _navigator;
    [SerializeField] private CardEditor _cardEditor;
    [SerializeField] private AvatarDisplayer _avatarDisplayer;
    [SerializeField] private List<Image> _designImages;
    [SerializeField] private List<Sprite> _designSprites;
    [SerializeField] private GameObject _editorCanvas;
    [SerializeField] private GameObject _displayerCanvas;
    [SerializeField] private TMP_Text _nameAndSurnameText;
    [SerializeField] private TMP_Text _jobText;
    [SerializeField] private TMP_Text _phoneText;
    [SerializeField] private TMP_Text _emailText;
    [SerializeField] private TMP_Text _telegramText;
    [SerializeField] private List<LinkOpener> _linkOpeners;
    [SerializeField] private List<Tag> _tags;
    private int _dataIndex = 0;

    public void DispayCard(int index)
    {
        _dataIndex = index;
        var card = SaveSystem.LoadData<CardSaveData>().Cards[_dataIndex];
        _nameAndSurnameText.text = card.Name + " " + card.Surname;
        _avatarDisplayer.ChangeAvatar(card.AvatarIndex);
        _jobText.text = card.Job;
        _phoneText.text = card.Phone;
        _emailText.text = card.Email;
        _telegramText.text = card.Telegram;
        foreach (var item in _designImages)
        {
            item.sprite = _designSprites[card.DesignIndex];
        }
        foreach (var item in _linkOpeners)
        {
            item.gameObject.SetActive(false);
        }
        for (int i = 0; i < card.Links.Count; i++)
        {
            _linkOpeners[i].SetLink(card.Links[i].Name, card.Links[i].Url);
            _linkOpeners[i].gameObject.SetActive(true);
        }
        foreach (var item in _tags)
        {
            item.gameObject.SetActive(false);
        }
        for (int i = 0; i < card.Tags.Count; i++)
        {
            _tags[i].SetTagName(card.Tags[i]);
            _tags[i].gameObject.SetActive(true);
        }
    }

    public void EditCard()
    {
        CloseDisplay();
        _editorCanvas.SetActive(true);
        _cardEditor.EditCard(_dataIndex);
    }

    public void CloseDisplay()
    {
        _navigator.ShowCards();
        _nameAndSurnameText.text = "";
        _jobText.text = "";
        _phoneText.text = "";
        _emailText.text = "";
        _telegramText.text = "";
        foreach (var item in _linkOpeners)
        {
            item.gameObject.SetActive(false);
        }
        foreach (var item in _tags)
        {
            item.gameObject.SetActive(false);
        }
    }
}
