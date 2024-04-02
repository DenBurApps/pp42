using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertShower : MonoBehaviour
{
    [SerializeField] private GameObject _alertCanvas;
    [SerializeField] private GameObject _deleteAlert;
    [SerializeField] private GameObject _saveAlert;
    private GameObject _currentAlert;

    public void HideAlert()
    {
        _currentAlert.SetActive(false);
        _alertCanvas.SetActive(false);
    }

    public void ShowDeleteAlert()
    {
        _alertCanvas.SetActive(true);
        _deleteAlert.SetActive(true);
        _currentAlert = _deleteAlert;
    }

    public void ShowSaveAlert()
    {
        Sequence anim = DOTween.Sequence();
        anim.Append(_saveAlert.transform.DOScale(1, 0.7f).SetEase(Ease.OutBounce))
            .Append(_saveAlert.transform.DOScale(0, 0.7f).SetEase(Ease.InBounce).SetDelay(2f));
        anim.SetLink(gameObject);
        anim.Play();
    }

}
