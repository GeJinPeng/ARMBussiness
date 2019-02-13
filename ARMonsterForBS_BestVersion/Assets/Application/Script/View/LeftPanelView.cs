using System.Collections;
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
        businessCouponView.ShowMain();
    }


    public void ShowPlayerCouponView()
    {
        Close();
        playerCouponView.ShowMain();
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
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
