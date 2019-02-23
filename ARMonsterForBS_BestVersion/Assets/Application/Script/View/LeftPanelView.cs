﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftPanelView : MonoBehaviour {


    public EditorAccountView editorAccountView;

    public PlayerCouponView playerCouponView;

    public BusinessCouponView businessCouponView;

    public StrongholdInfoBar strongholdInfoBar;

    public ServerMessageView serverMessageView;

    public Animator animator;
    public void Show()
    {
        gameObject.SetActive(true);
        animator.Play("ShowLeftPanel");
    }
    public void Close()
    {
        animator.Play("CloseLeftPanel");
    }
    public void SetActivityFalse()
    {
        gameObject.SetActive(false);
    }
    public void ShowEditorAccountView()
    {
        Close();
        editorAccountView.StartView();
    }

    public void ShowBusinessCouponView()
    {
        Close();

        GameObject view = AndaDataManager.Instance.InstantiateItem(AndaDataManager.BusinessCouponManagerView);
        AndaUIManager.Instance.SetIntoCanvas(view.transform);

      //  businessCouponView.ShowMain();
    }


    public void ShowPlayerCouponView()
    {
        Close();
        GameObject view = AndaDataManager.Instance.InstantiateItem(AndaDataManager.PlayerCouponrView);
        AndaUIManager.Instance.SetIntoCanvas(view.transform);
        view.GetComponent<PlayerCouponView>().ShowMain();
    }

    public void ShowStrongholdInfoBar()
    {
        Close();
        strongholdInfoBar.StartView();
    }

    public void ShowServerMessageView()
    {
        Close();
        serverMessageView.ShowMain();
    }

    public void OpenMall()
    {
        Close();
        GameObject mall= Resources.Load<GameObject>("Prefab/MallBar");
        mall = Instantiate(mall);
        AndaUIManager.Instance.SetIntoCanvas(mall.transform);
    }

    public void OpenAdditionEditorCouponView()
    {
        Close();
        GameObject view = AndaDataManager.Instance.InstantiateItem(AndaDataManager.AdditionEditorCouponView);
        AndaUIManager.Instance.SetIntoCanvas(view.transform);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


}
