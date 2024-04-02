using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvatarItem : MonoBehaviour
{
    [SerializeField] private GameObject _selectOutline;
    [SerializeField] private int _index;
    private Button _button;
    public Action<int> Select;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnSelectAvatarClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveAllListeners();
    }

    public void DisableOutline()
    {
        _selectOutline.gameObject.SetActive(false);
    }

    public void EnableOutline()
    {
        _selectOutline.gameObject.SetActive(true);
    }

    public void OnSelectAvatarClick()
    {
        Select?.Invoke(_index);
    }
}
