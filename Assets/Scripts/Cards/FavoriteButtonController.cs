using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FavoriteButtonController : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private List<Sprite> _sprites;
    private int _index = 0;

    public void SetIndex(int index)
    {
        _index = index;
        UpdateFavoriteSprite();
    }

    public void UpdateFavoriteSprite()
    {
        var cards = SaveSystem.LoadData<CardSaveData>();
        if (cards.Cards[_index].IsFavorite)
        {
            _image.sprite = _sprites[1];
        }
        else
        {
            _image.sprite = _sprites[0];
        }
    }

    public void TryAddToFavorite()
    {
        var cards = SaveSystem.LoadData<CardSaveData>();
        if (cards.Cards[_index].IsFavorite)
        {
            cards.Cards[_index].IsFavorite = false;
        }
        else
        {
            cards.Cards[_index].IsFavorite = true;
        }
        SaveSystem.SaveData(cards);
        UpdateFavoriteSprite();
    }
}
