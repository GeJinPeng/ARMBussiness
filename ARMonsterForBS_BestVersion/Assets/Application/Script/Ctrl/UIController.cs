using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIController : MonoBehaviour {

    public LoginView  loginView;
    public MapView mapView;

    public WaitBoard waitBoard;
    public OpenImage openImageBar;

    public LeftPanelView leftPanelView;

    public ScanningTool scanningTool;

    public GameObject background;

    private void Awake()
    {
        AndaUIManager.Instance.uIController = this;
    }


    public void OpenWatiBoard(bool isOpen)
    {
         Debug.Log("isOpen" +  isOpen);
         waitBoard.transform.SetAsLastSibling();
         waitBoard.gameObject.SetActive(isOpen);
    }


    public void OpenImageBar(Button btn, System.Action<Texture2D> callback)
    {
        openImageBar.gameObject.SetActive(true);
        openImageBar.transform.SetAsLastSibling();
        openImageBar.RegisterCallback(callback);
        openImageBar.OpenPonePhoto(btn);
    }

    public void OpenLeftPanel()
    {
        leftPanelView.Show();
    }

    public void OpenScannerView()
    {
        if(scanningTool==null)
        {

            GameObject view = AndaDataManager.Instance.InstantiateItem(AndaDataManager.ScannerViwer);
            AndaUIManager.Instance.SetIntoCanvas(view.transform);
            scanningTool= view.GetComponent<ScanningTool>();

        }
       

    }

    public void OpenBackground(bool isOpen)
    {
        background.gameObject.SetActive(isOpen);
    }


}
