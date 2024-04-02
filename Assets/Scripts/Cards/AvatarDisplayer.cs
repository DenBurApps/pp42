using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvatarDisplayer : MonoBehaviour
{
    [SerializeField] private List<Sprite> _sprites;
    [SerializeField] private Image _avatar;
    private int _currentAvatarIndex = 0;

    public void ChangeAvatar(int index)
    {
        _currentAvatarIndex = index;
        _avatar.sprite = _sprites[_currentAvatarIndex];
    }

    public int GetAvatarIndex()
    {
        return _currentAvatarIndex;
    }
}
