using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FavoritesShower : MonoBehaviour
{
    [SerializeField] private AlertShower _alertShower;
    [SerializeField] private CardEditor _cardsEditor;
    [SerializeField] private CardsShower _cardsShower;
    [SerializeField] private CardDisplayer _cardDisplayer;
    [SerializeField] private GameObject _displayCanvas;
    [SerializeField] private GameObject _favCanvas;
    [SerializeField] private GameObject _cardEditorCanvas;
    [SerializeField] private ContentSizeFitter _contentSizeFitter;
    [SerializeField] private VerticalLayoutGroup _verticalLayoutGroup;
    [SerializeField] private List<CardItem> _cardItems;
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private GameObject _noItemsAlert;
    [SerializeField] private GameObject _searchObj;
    [SerializeField] private TMP_InputField _search;

    private void OnEnable()
    {
        foreach (var item in _cardItems)
        {
            item.Show += ShowCard;
            item.FavoriteChange += ShowCards;
            item.Delete += OnDeleteCard;
        }
        _cardsEditor.Saved += OnSaved;
        _search.onValueChanged.AddListener(SearchByNameAndSurname);
        ShowCards();
    }

    private void OnDisable()
    {
        foreach (var item in _cardItems)
        {
            item.Show -= ShowCard;
            item.FavoriteChange -= ShowCards;
            item.Delete -= OnDeleteCard;
        }
        _cardsEditor.Saved -= OnSaved;
    }

    private void Start()
    {
        ShowCards();
    }

    public void SearchByNameAndSurname(string text)
    {
        var cards = SaveSystem.LoadData<CardSaveData>();
        if (cards.Cards.Count <= 0)
        {
            return;
        }
        int j = 0;
        for (int i = cards.Cards.Count - 1; i >= 0; i--)
        {
            string name = cards.Cards[i].Name + " " + cards.Cards[i].Surname;
            if (name.Contains(text) && cards.Cards[i].IsFavorite)
            {
                _cardItems[j].gameObject.SetActive(true);
            }
            else
            {
                _cardItems[j].gameObject.SetActive(false);
            }
            j++;
        }
        UpdateLayouts();
    }

    private void ShowCard(int index)
    {
        _cardDisplayer.DispayCard(index);
        _favCanvas.SetActive(false);
        _displayCanvas.SetActive(true);
    }

    private void OnSaved()
    {
        ShowCards();
    }

    public void UpdateLayouts()
    {
        Canvas.ForceUpdateCanvases();
        _verticalLayoutGroup.CalculateLayoutInputVertical();
        _contentSizeFitter.SetLayoutVertical();
        _scrollRect.verticalNormalizedPosition = 1;
    }

    public void ShowCards()
    {
        _cardsShower.ShowCards();
        foreach (var item in _cardItems)
        {
            item.gameObject.SetActive(false);
        }
        var cards = SaveSystem.LoadData<CardSaveData>();
        if (cards.Cards.Count <= 0)
        {
            _searchObj.gameObject.SetActive(false);
            _scrollRect.gameObject.SetActive(false);
            _noItemsAlert.gameObject.SetActive(true);
            return;
        }
        int favoritesCount = 0;
        for (int i = 0; i < cards.Cards.Count; i++)
        {
            if (cards.Cards[i].IsFavorite)
            {
                favoritesCount++;
            }
        }
        if(favoritesCount <= 0)
        {
            _searchObj.gameObject.SetActive(false);
            _scrollRect.gameObject.SetActive(false);
            _noItemsAlert.gameObject.SetActive(true);
            return;
        }
        _noItemsAlert.SetActive(false);
        _searchObj.gameObject.SetActive(true);
        _scrollRect.gameObject.SetActive(true);
        int j = 0;
        for (int i = cards.Cards.Count - 1; i >= 0; i--)
        {
            if (cards.Cards[i].IsFavorite)
            {
                _cardItems[j].Init(i);
                _cardItems[j].gameObject.SetActive(true);
                j++;
            }
        }
        UpdateLayouts();
    }

    public void OnDeleteCard(int checkListIndex)
    {
        Debug.Log(checkListIndex);
        _alertShower.ShowDeleteAlert();
        _cardsShower.SetCurrentIndex(checkListIndex);
    }
}
