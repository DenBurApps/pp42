using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardEditor : MonoBehaviour
{
    [SerializeField] private Navigator _navigator;
    [SerializeField] private CardDisplayer _cardDisplayer;
    [SerializeField] private GameObject _editorCanvas;
    [SerializeField] private GameObject _displayerCanvas;
    [SerializeField] private AvatarChooser _avatarChooser;
    [SerializeField] private AvatarDisplayer _avatarDisplayer;
    [SerializeField] private TMP_InputField _nameField;
    [SerializeField] private TMP_InputField _surnameField;
    [SerializeField] private TMP_InputField _jobField;
    [SerializeField] private TMP_InputField _phoneField;
    [SerializeField] private TMP_InputField _emailField;
    [SerializeField] private TMP_InputField _telegramField;
    [SerializeField] private List<LinkInput> _linkInputs;
    [SerializeField] private GameObject _addLink;
    [SerializeField] private List<TMP_InputField> _tags;
    [SerializeField] private GameObject _addTag;
    [SerializeField] private List<GameObject> _selectedDesign;
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private Button _closeButton;
    public Action Saved;
    private int _linksCount = 0;
    private int _tagsCount = 0;
    private int _currentDesign;
    private bool _hasData = false;
    private int _dataIndex = 0;

    public void CreateCard()
    {
        ResetEditor();
        _closeButton.onClick.AddListener(CloseCardEditor);
    }

    public void ResetEditor()
    {
        _scrollRect.verticalNormalizedPosition = 1;
        _linksCount = 0;
        _tagsCount = 0;
        _hasData = false;
        foreach (var item in _linkInputs)
        {
            item.gameObject.SetActive(false);
            item.ResetLink();
        }
        AddLink();
        foreach (var item in _tags)
        {
            item.gameObject.SetActive(false);
            item.text = "Empty tag";
        }
        _addLink.SetActive(true);
        _addTag.SetActive(true);
        _avatarDisplayer.ChangeAvatar(0);
        _nameField.text = "";
        _surnameField.text = "";
        _jobField.text = "";
        _emailField.text = "";
        _telegramField.text = "";
        SetDesign(0);
        _closeButton.onClick.RemoveAllListeners();
    }

    public void ReturnToDisplay()
    {
        _editorCanvas.SetActive(false);
        ResetEditor();
        _cardDisplayer.DispayCard(_dataIndex);
        _displayerCanvas.SetActive(true);
    }

    public void CloseCardEditor()
    {
        _navigator.ShowCards();
        ResetEditor();
    }

    public void EditCard(int index)
    {
        _scrollRect.verticalNormalizedPosition = 1;
        _hasData = true;
        _dataIndex = index;
        var card = SaveSystem.LoadData<CardSaveData>().Cards[_dataIndex];
        _linksCount = card.Links.Count;
        if (_linksCount >= 3)
        {
            _addLink.SetActive(false);
        }
        else
        {
            _addLink.SetActive(true);
        }
        _tagsCount = card.Tags.Count;
        if (_tagsCount >= 3)
        {
            _addTag.SetActive(false);
        }
        else
        {
            _addTag.SetActive(true);
        }
        foreach (var item in _linkInputs)
        {
            item.gameObject.SetActive(false);
            item.ResetLink();
        }
        for (int i = 0; i < _linksCount; i++)
        {
            _linkInputs[i].SetLinkName(card.Links[i].Name);
            _linkInputs[i].SetLink(card.Links[i].Url);
            _linkInputs[i].gameObject.SetActive(true);
        }
        foreach (var item in _tags)
        {
            item.gameObject.SetActive(false);
            item.text = "Empty tag";
        }
        for (int i = 0; i < _tagsCount; i++)
        {
            _tags[i].text = card.Tags[i];
            _tags[i].gameObject.SetActive(true);
        }
        _avatarDisplayer.ChangeAvatar(card.AvatarIndex);
        _nameField.text = card.Name;
        _surnameField.text = card.Surname;
        _jobField.text = card.Job;
        _emailField.text = card.Email;
        _telegramField.text = card.Telegram;
        SetDesign(card.DesignIndex);
        _closeButton.onClick.AddListener(ReturnToDisplay);
    }

    public void ChangeAvatar()
    {
        _avatarChooser.gameObject.SetActive(true);
        _avatarChooser.SelectAvatar(_avatarDisplayer.GetAvatarIndex());
    }

    public void SetFirstLink(string url)
    {
        _linkInputs[0].SetLink(url);
    }

    public void AddLink()
    {
        _linkInputs[_linksCount].gameObject.SetActive(true);
        _linksCount++;
        if(_linksCount >= 3)
        {
            _addLink.SetActive(false);
        }
        else
        {
            _addLink.SetActive(true);
        }
    }
    
    public void AddTag()
    {
        _tags[_tagsCount].gameObject.SetActive(true);
        _tags[_tagsCount].text = "Empty tag";
        _tagsCount++;
        if(_tagsCount >= 3)
        {
            _addTag.SetActive(false);
        }
        else
        {
            _addTag.SetActive(true);
        }
    }

    public void DeleteTag(int index)
    {
        _tags[index].gameObject.SetActive(false);
        _tags[index].text = "Empty tag";
        _tagsCount--;
        _addTag.SetActive(true);
    }

    public void SetDesign(int index)
    {
        foreach (var item in _selectedDesign)
        {
            item.SetActive(false);
        }
        _currentDesign = index;
        _selectedDesign[_currentDesign].SetActive(true);
    }

    public void SaveCard()
    {
        var cards = SaveSystem.LoadData<CardSaveData>();
        if (_hasData)
        {
            if (_nameField.text == "")
            {
                cards.Cards[_dataIndex].Name = "Name";
            }
            else
            {
                cards.Cards[_dataIndex].Name = _nameField.text;
            }
            if (_surnameField.text == "")
            {
                cards.Cards[_dataIndex].Surname = "Surname";
            }
            else
            {
                cards.Cards[_dataIndex].Surname = _surnameField.text;
            }
            if (_jobField.text == "")
            {
                cards.Cards[_dataIndex].Job = "Empty job title";
            }
            else
            {
                cards.Cards[_dataIndex].Job = _jobField.text;
            }
            cards.Cards[_dataIndex].Phone = _phoneField.text;
            if (_emailField.text == "")
            {
                cards.Cards[_dataIndex].Email = "empty@email";
            }
            else
            {
                cards.Cards[_dataIndex].Email = _emailField.text;
            }
            cards.Cards[_dataIndex].Telegram = _telegramField.text;
            cards.Cards[_dataIndex].Links.Clear();
            if (_linksCount > 0)
            {
                for (int i = 0; i < _linksCount; i++)
                {
                    if (_linkInputs[i].GetLink() != "" && _linkInputs[i].GetLinkName() != "")
                    {
                        LinkData link = new LinkData();
                        link.Name = _linkInputs[i].GetLinkName();
                        link.Url = _linkInputs[i].GetLink();
                        cards.Cards[_dataIndex].Links.Add(link);
                    }
                }
                if (cards.Cards[_dataIndex].Links.Count <= 0)
                {
                    LinkData link = new LinkData();
                    link.Name = "Empty link name";
                    link.Url = "link.empty";
                    cards.Cards[_dataIndex].Links.Add(link);
                }
            }
            cards.Cards[_dataIndex].Tags.Clear();
            if (_tagsCount > 0)
            {
                for (int i = 0; i < _tagsCount; i++)
                {
                    cards.Cards[_dataIndex].Tags.Add(_tags[i].text);
                }
            }
            cards.Cards[_dataIndex].DesignIndex = _currentDesign;
            cards.Cards[_dataIndex].AvatarIndex = _avatarDisplayer.GetAvatarIndex();
        }
        else
        {
            CardData card = new CardData();
            card.IsFavorite = false;
            if(_nameField.text == "")
            {
                card.Name = "Name";
            }
            else
            {
                card.Name = _nameField.text;
            }
            if(_surnameField.text == "")
            {
                card.Surname = "Surname";
            }
            else
            {
                card.Surname = _surnameField.text;
            }
            if(_jobField.text == "") 
            {
                card.Job = "Empty job title";
            }
            else
            {
                card.Job = _jobField.text;
            }
            card.Phone = _phoneField.text;
            if (_emailField.text == "")
            {
                card.Email = "empty@email";
            }
            else
            {
                card.Email = _emailField.text;
            }
            card.Telegram = _telegramField.text;
            card.Links.Clear();
            if(_linksCount > 0)
            {
                for (int i = 0; i < _linksCount; i++)
                {
                    if(_linkInputs[i].GetLink() != "" && _linkInputs[i].GetLinkName() != "")
                    {
                        LinkData link = new LinkData();
                        link.Name = _linkInputs[i].GetLinkName();
                        link.Url = _linkInputs[i].GetLink();
                        card.Links.Add(link);
                    }
                }
                if(card.Links.Count <= 0)
                {
                    LinkData link = new LinkData();
                    link.Name = "Empty link name";
                    link.Url = "link.empty";
                    card.Links.Add(link);
                }
            }
            card.Tags.Clear();
            if(_tagsCount > 0)
            {
                for (int i = 0; i < _tagsCount; i++)
                {
                    card.Tags.Add(_tags[i].text);
                }
            }
            card.DesignIndex = _currentDesign;
            card.AvatarIndex = _avatarDisplayer.GetAvatarIndex();
            cards.Cards.Add(card);
            _dataIndex = cards.Cards.Count - 1;
            _hasData = true;
        }
        SaveSystem.SaveData(cards);
        Saved?.Invoke();

    }
}
