using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QRScanner : MonoBehaviour
{
    [SerializeField] private CardsShower _cardsShower;
    [SerializeField] private QRCodeDecodeController _qRCodeDecodeController;
    [SerializeField] private GameObject _rawImage;


    private void OnEnable()
    {
        _qRCodeDecodeController.onQRScanFinished += OnScanFinished;
    }

    private void OnDisable()
    {
        _qRCodeDecodeController.onQRScanFinished -= OnScanFinished;
    }

    public void StartScan()
    {
        ResetScan();
        _rawImage.SetActive(true);
        _qRCodeDecodeController.StartWork();
    }

    public void StopWork()
    {
        _qRCodeDecodeController.StopWork();
        _rawImage.SetActive(false);
    }

    public void ResetScan()
    {
        _qRCodeDecodeController.Reset();
        _rawImage.SetActive(false);
	}

    public void OnScanFinished(string dataText)
    {
        StopWork();
        _cardsShower.CreateCardByQR(dataText);
    }
}
