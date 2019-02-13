using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Internal;
using System.Runtime.InteropServices;
public class MainContoller : MonoBehaviour {

    public BaseController checkConfigCtrl;
    public BaseController loginCtrl;
    public BaseController mapCtrl; 
    public BaseController mallCtrl;
    private BaseController lastController;

    public ViewBasic loginView;
    public ViewBasic mapView;
    public ViewBasic mallView;
    private ViewBasic lastView;

    private void Awake()
    {
        AndaDataManager.Instance.mainContoller = this;
        checkConfigCtrl.SetMainController(this);    
        loginCtrl.SetMainController(this);
        mapCtrl.SetMainController(this);
        mallCtrl.SetMainController(this);

        loginView.baseController = loginCtrl;
        mapView.baseController = mapCtrl;
    }

    private void Start()
    {
        SwitchCtrl("checkConfigCtrl"); 
    }

    public void SwitchCtrl(string ctrlName)
    {
        StartCtrl(ctrlName);
    }

    private void StartCtrl(string ctrlName)
    {
        if(lastController!=null) lastController.EndCtrl();

        switch(ctrlName)
        {
            case "checkConfigCtrl" :
                checkConfigCtrl.StartCtrl();
                lastController = checkConfigCtrl;
                break;
            case "loginCtrl":
                loginCtrl.StartCtrl();
                lastController = loginCtrl;
                //loginView.StartView();
                //lastView = loginView;
                break;
            case "mapCtrl":
                mapCtrl.StartCtrl();
                lastController = mapCtrl;
                //mapView.StartView();
                //lastView = mapView;
                break;
            case "mallCtrl":
                mallCtrl.StartCtrl();
                lastController = mallCtrl;
                //mallView.StartView();
                //lastView = mallView;1 
                break;
        }
    }




    //IOS通信 ，发送想要购买的物件ID

    [DllImport("__Internal")]
    public static extern void _Purchase(string id);

    public void Purchase(string id)
    {
        _Purchase(id);
    }

    public void IOSCallBack_Purchase(string json)
    {
        //回调，让服务器去验证一下，然后发放物品

    }
}
