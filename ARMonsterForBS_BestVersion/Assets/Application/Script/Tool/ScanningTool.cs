using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZXing;

public class ScanningTool: MonoBehaviour
{
    public Color32[] data;
    private bool IsScanning=false;
    public RawImage cameraTexture;
    public Text txt;
    private WebCamTexture webCameraTexture;
    private BarcodeReader barcodeReader;
    private float timer = 0;
    private int width;


    public void OnEnable()
    {
        Scanning();
    }

    IEnumerator ScanningCoroutine()
    {
        barcodeReader = new BarcodeReader();
        yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
        try
        {
            if (Application.HasUserAuthorization(UserAuthorization.WebCam))
            {
                WebCamDevice[] devices = WebCamTexture.devices;
                string devicename = devices[0].name;
                webCameraTexture = new WebCamTexture(devicename,800 , 480);
                cameraTexture.gameObject.SetActive(true);
                cameraTexture.texture = webCameraTexture;
                webCameraTexture.Play();
                IsScanning = true;
            }
        }
        catch (Exception ex)
        {
            txt.text = ex.Message;
            Debug.Log(ex.Message);
        }
    }

    public void Scanning()
    {
        if (!IsScanning)
        {
            StartCoroutine("ScanningCoroutine");
        }
        else {
            IsScanning = false;
            cameraTexture.gameObject.SetActive(false);
        }
    }
    public void StopScanning(PlayerCouponRequest info)
    {
        cameraTexture.gameObject.SetActive(false);
        webCameraTexture.Stop();
       
        if (info != null)
        {
            if (info.code == "200")
            {
                AndaUIManager.Instance.PlayTips("扫描成功");
                txt.text = "扫描成功";
            }
            else
            {
                AndaUIManager.Instance.PlayTips("扫描失败");
                txt.text = info.detail;
            }
        }
        Destroy(gameObject);
    }
    void ScreenChange()//屏幕横竖屏切换
    {
        if (cameraTexture == null)
            return;

        if (width == Screen.width)
            return;
        width = Screen.width;

        if (width > Screen.height)
        {
            cameraTexture.transform.localEulerAngles = Vector3.zero;
        }
        else
        {
            cameraTexture.transform.localEulerAngles = new Vector3(0, 0, -90);
            cameraTexture.rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);
        }
    }
    void FixedUpdate()
    {
        if (IsScanning)
        {
            timer += Time.deltaTime;

            if (timer > 0.5f && !isChecking) //0.5秒扫描一次
            {
                isChecking= true;
                data = webCameraTexture.GetPixels32();
                DecodeQR(webCameraTexture.width, webCameraTexture.height);
                timer = 0;
            }
           // ScreenChange();
        }
    }

    private bool isChecking = false;

   /* IEnumerator ScanQRcode()
    {
        data = webCameraTexture.GetPixels32();
        DecodeQR(webCameraTexture.width, webCameraTexture.height);
        yield return new WaitForEndOfFrame();
    }*/

    private void DecodeQR(int width, int height)
    {
        var br = barcodeReader.Decode(data, width, height);

        if(br == null)
        {
           
        }else
        {
            if(string.IsNullOrEmpty(br.Text))
            {

            }
            else
            {
                if (br.Text != txt.text)
                {
                    IsScanning = false;

                    AndaDataManager.Instance.QRCheackCoupon(br.Text, StopScanning);
                }
            }
        }

        isChecking = false; 
    }
}
