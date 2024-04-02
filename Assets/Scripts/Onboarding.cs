using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Onboarding : MonoBehaviour
{
    [SerializeField] private List<GameObject> _steps;
    [SerializeField] private GameObject _loading;
    private int _currentIndex = 0;

    private void Start()
    {
        _currentIndex = 0;
        foreach (var item in _steps)
        {
            item.SetActive(false);
        }
        _steps[_currentIndex].SetActive(true);
        ShowLoading();
    }

    public void ShowLoading()
    {
        _loading.transform.DOLocalRotate(new Vector3(0, 0, -360), 1, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear).SetLoops(3).SetLink(gameObject).OnComplete(() => 
        {
            ShowNextStep();
        });
    }

    public void ShowNextStep()
    {
        _currentIndex++;
        if (_currentIndex < _steps.Count)
        {
            foreach (var item in _steps)
            {
                item.SetActive(false);
            }
            _steps[_currentIndex].SetActive(true);
        }
        else
        {
            PlayerPrefs.SetInt("Onboarding", 1);
            SceneManager.LoadScene("MainScene");
        }
    }
    
}
