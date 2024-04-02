using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarChooser : MonoBehaviour
{
    [SerializeField] private List<AvatarItem> _avatarItems;
    [SerializeField] private AvatarDisplayer _avatarDisplayer;
    private int _currentIndex;

    private void OnEnable()
    {
        foreach (var item in _avatarItems)
        {
            item.Select += SelectAvatar;
        }
    }

    private void OnDisable()
    {
        foreach (var item in _avatarItems)
        {
            item.Select -= SelectAvatar;
        }
    }

    public void HideChooseAvater()
    {
        gameObject.SetActive(false);
    }

    public void SelectAvatar(int index)
    {
        foreach (var item in _avatarItems)
        {
            item.DisableOutline();
        }
        _avatarItems[index].EnableOutline();
        _currentIndex = index;
    }

    public void SaveAvatar()
    {
        _avatarDisplayer.ChangeAvatar(_currentIndex);
        HideChooseAvater();
    }
}
