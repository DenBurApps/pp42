using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardsShower : MonoBehaviour
{
    [SerializeField] private AlertShower _alertShower;
    [SerializeField] private FavoritesShower _favoritesShower;
    [SerializeField] private CardEditor _cardsEditor;
    [SerializeField] private CardDisplayer _cardDisplayer;
    [SerializeField] private GameObject _displayCanvas;
    [SerializeField] private GameObject _cardsCanvas;
    [SerializeField] private GameObject _cardEditorCanvas;
    [SerializeField] private ContentSizeFitter _contentSizeFitter;
    [SerializeField] private VerticalLayoutGroup _verticalLayoutGroup;
    [SerializeField] private List<CardItem> _cardItems;
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private GameObject _noItemsAlert;
    [SerializeField] private GameObject _searchObj;
    [SerializeField] private TMP_InputField _search;
    private int _currentIndex = 0;

    private void OnEnable()
    {
        foreach (var item in _cardItems)
        {
            item.Show += ShowCard;
            item.Delete += OnDeleteCard;
        }
        _cardsEditor.Saved += OnSaved;
        _search.onValueChanged.AddListener(SearchByNameAndSurname);
    }

    private void OnDisable()
    {
        foreach (var item in _cardItems)
        {
            item.Show -= ShowCard;
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
        if(cards.Cards.Count <= 0)
        {
            return;
        }
        int j = 0;
        for (int i = cards.Cards.Count - 1; i >= 0; i--)
        {
            string name = cards.Cards[i].Name + " " + cards.Cards[i].Surname;
            if (name.Contains(text))
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

    public void CreateCard()
    {
        _cardEditorCanvas.SetActive(true);
        _cardsEditor.CreateCard();
    }

    public void CreateCardByQR(string url)
    {
        _cardEditorCanvas.SetActive(true);
        _cardsEditor.CreateCard();
        _cardsEditor.SetFirstLink(url);
    }

    private void ShowCard(int index)
    {
        _cardDisplayer.DispayCard(index);
        _cardsCanvas.SetActive(false);
        _displayCanvas.SetActive(true);
    }

    private void OnSaved()
    {
        ShowCards();
        _alertShower.ShowSaveAlert();
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
        _noItemsAlert.SetActive(false);
        _searchObj.gameObject.SetActive(true);
        _scrollRect.gameObject.SetActive(true);
        int j = 0;
        for (int i = cards.Cards.Count - 1; i >= 0; i--)
        {
            _cardItems[j].Init(i);
            _cardItems[j].gameObject.SetActive(true);
            j++;
        }
        UpdateLayouts();
    }

    public void SetCurrentIndex(int index)
    {
        _currentIndex = index;
    }

    public void OnDeleteCard(int checkListIndex)
    {
        _alertShower.ShowDeleteAlert();
        _currentIndex = checkListIndex;
    }

    public void DeleteCard()
    {
        var cards = SaveSystem.LoadData<CardSaveData>();
        cards.Cards.RemoveAt(_currentIndex);
        SaveSystem.SaveData(cards);
        ShowCards();
        _favoritesShower.ShowCards();
    }
}
