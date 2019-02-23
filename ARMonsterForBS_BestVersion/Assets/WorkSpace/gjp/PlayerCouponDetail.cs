using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCouponDetail : MonoBehaviour {

    public PlayerCoupon selectPlayerCoupon;

    public Image headImage;

    public Text businessName;
    public Text playerCouponName;
    public Text describe;
    public Text time;

    public Text addressInfo;
    public Button confirmButton;

    public Text tips;

    public Transform content;

    public bool IsShow = false;

    public Animator animator;

    public GameObject operation;

    public Text uptime;
    public void setInfo(PlayerCoupon _playerCoupon)
    {
        gameObject.SetActive(true);
        selectPlayerCoupon = _playerCoupon;

        businessName.text = selectPlayerCoupon.coupon.businessname;
        playerCouponName.text = selectPlayerCoupon.coupon.title;
        describe.text = selectPlayerCoupon.coupon.description;
        Debug.Log(selectPlayerCoupon.coupon.endtime);
        if (selectPlayerCoupon.coupon.endtime == 0)
            time.text = "有效期：永久有效";
        else
            time.text = "有效期：" + ConvertTool.UnixTimestampToDateTime(selectPlayerCoupon.coupon.endtime).ToShortDateString();

        if (selectPlayerCoupon.status == 1)
        {
            uptime.text="申请时间："+ ConvertTool.UnixTimestampToDateTime(selectPlayerCoupon.createTime).ToLongDateString();
        }
        addressInfo.text = selectPlayerCoupon.code;
        tips.text = selectPlayerCoupon.coupon.tips;
        AndaDataManager.Instance.GetUserImg(_playerCoupon.coupon.image , SetImage);
        ShowButtonText(selectPlayerCoupon.status);
    }
    public void ShowButtonText(int _index)
    {
        if (_index != 1)
        {
            confirmButton.transform.GetChild(0).gameObject.SetActive(false);
            confirmButton.transform.GetChild(1).gameObject.SetActive(false);
            confirmButton.transform.GetChild(2).gameObject.SetActive(false);
            confirmButton.transform.GetChild(3).gameObject.SetActive(false);
            confirmButton.transform.GetChild(_index).gameObject.SetActive(true);
            confirmButton.gameObject.SetActive(true);
            operation.SetActive(false);
        }
        else
        {
            confirmButton.gameObject.SetActive(false);
            operation.SetActive(true);
        }
    }

    public void SetImage(Sprite _sp)
    {
        headImage.sprite = _sp;
    }

    public void Open(PlayerCoupon _playerCoupon)
    {
        setInfo(_playerCoupon);
        Show();
    }

    public void Show()
    {
        animator.Play("FadeIn");
        gameObject.SetActive(true);
    }
    public void SetShow()
    {
        IsShow = true;
    }
    public void SetClose()
    {
        gameObject.SetActive(false);
        content.transform.position = new Vector3(content.transform.position.x, 0, content.transform.position.z);
        Destroy(gameObject);
    }
    public void Close()
    {
        IsShow = false;
        animator.Play("FadeOut");
    }
    public void Scroll(Vector2 v2)
    {
        if (IsShow)
        {
            if (content.transform.position.y < 20)
            {
                Close();
            }
        }
    }

    public void Cheack()
    {
        AndaPlayerCouponManager.Instance.Cheack(selectPlayerCoupon.applyIndex);
    }

    public void Fail()
    {
        AndaPlayerCouponManager.Instance.Fail(selectPlayerCoupon.applyIndex);
    }
}
