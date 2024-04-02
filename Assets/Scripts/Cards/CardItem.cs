using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardItem : MonoBehaviour
{
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private List<Sprite> _backgroundSprites;
    [SerializeField] private FavoriteButtonController _favoriteButtonController;
    [SerializeField] private Button _favoriteButton;
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _jobText;
    [SerializeField] private List<Tag> _tags;
    [SerializeField] private Button _showButton;
    [SerializeField] private Button _deleteButton;
    private int _cardIndex;
    public Action<int> Show;
    public Action<int> Delete;
    public Action FavoriteChange;

    private void OnEnable()
    {
        _showButton.onClick.AddListener(OnShowButtonClick);
        _deleteButton.onClick.AddListener(OnDeleteButtonClick);
    }

    private void OnDisable()
    {
        _showButton.onClick.RemoveAllListeners();
        _deleteButton.onClick.RemoveAllListeners();
    }

    public void Init(int index)
    {
        _favoriteButton.onClick.RemoveAllListeners();
        var cards = SaveSystem.LoadData<CardSaveData>();
        _cardIndex = index;
        _backgroundImage.sprite = _backgroundSprites[cards.Cards[_cardIndex].DesignIndex];
        _nameText.text = cards.Cards[_cardIndex].Name + " " + cards.Cards[_cardIndex].Surname;
        _jobText.text = cards.Cards[_cardIndex].Job;
        foreach (var item in _tags)
        {
            item.gameObject.SetActive(false);
        }
        for (int i = 0; i < cards.Cards[_cardIndex].Tags.Count; i++)
        {
            _tags[i].SetTagName(cards.Cards[_cardIndex].Tags[i]);
            _tags[i].gameObject.SetActive(true);
        }
        _favoriteButtonController.SetIndex(_cardIndex);
        _favoriteButton.onClick.AddListener(OnFavoriteButtonClick);
    }

    public void OnFavoriteButtonClick()
    {
        _favoriteButtonController.TryAddToFavorite();
        FavoriteChange?.Invoke();
    }

    public void OnDeleteButtonClick()
    {
        Delete?.Invoke(_cardIndex);
    }

    public void OnShowButtonClick()
    {
        Show?.Invoke(_cardIndex);
    }

}
