using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;
using ZXing;

/// <summary>
/// Reads QRCode from standard camera (It does not use the ARFoundation ARKit camera)
/// </summary>
public class QRScanner : MonoBehaviour
{   
    [SerializeField] private RawImage _rawImageBackground;
    [SerializeField] private AspectRatioFitter _aspectRatioFitter;
    [SerializeField] private RectTransform _scanZone;
    [SerializeField] private Canvas UICanvas;

    private bool _isCameraAvailable;

    private WebCamTexture _cameraTexture;

    void Start()
    {
        
    }

    void Update()
    {
        if (UICanvas.enabled == true)
        {
            
        }
        else
        {
            ScanQR();
            UpdateCameraRender();
        }

    }

    public void InitCamera()
    {
        UICanvas.enabled = false;

        WebCamDevice[] devices = WebCamTexture.devices;

        if (devices.Length == 0)
        {
            _isCameraAvailable = false;
            return;
        }

        for (int i = 0; i < devices.Length; i++)
        {
            if (devices[i].isFrontFacing == false)
            {
                _cameraTexture = new WebCamTexture(devices[i].name, (int)_scanZone.rect.width, (int)_scanZone.rect.height);
            }
        }

        _cameraTexture.Play();
        _rawImageBackground.texture = _cameraTexture;
        _isCameraAvailable = true;

        ScanQR();
    }

    private void UpdateCameraRender()
    {
        if (_isCameraAvailable == false)
        {
            return;
        }

        float ratio = (float)_cameraTexture.width / (float) _cameraTexture.height;
        _aspectRatioFitter.aspectRatio = ratio;

        int orientation = -_cameraTexture.videoRotationAngle;
        _rawImageBackground.rectTransform.localEulerAngles = new Vector3(0, 0, orientation);
    }

    private void ScanQR()
    {
        try
        {
            IBarcodeReader barcodeReader = new BarcodeReader();
            Result result = barcodeReader.Decode(_cameraTexture.GetPixels32(), _cameraTexture.width, _cameraTexture.height);

            if (result != null)
            {
                Debug.Log(result.Text);
                if (result.Text == "AloitaAR")
                {
                    SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
                }
            }
            else
            {
                return;
            }
        }
        catch
        {
            Debug.Log("Failed in try/catch");
        }
    }
}