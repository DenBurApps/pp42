using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.iOS;

public class Settings : MonoBehaviour
{
    [SerializeField] private GameObject _privacyCanvas;
    [SerializeField] private GameObject _termsCanvas;
    [SerializeField] private GameObject _settingsCanvas;
    [SerializeField] private string _email;

    public void ShowPrivacy()
    {
        _settingsCanvas.SetActive(false);
        _privacyCanvas.SetActive(true);
    }

    public void ShowTerms()
    {
        _settingsCanvas.SetActive(false);
        _termsCanvas.SetActive(true);
    }

    public void RateUs()
    {
        Device.RequestStoreReview();
    }

    public void ContactUs()
    {
        Application.OpenURL("mailto:" + _email + "?subject=Mail to developer");
    }
}
