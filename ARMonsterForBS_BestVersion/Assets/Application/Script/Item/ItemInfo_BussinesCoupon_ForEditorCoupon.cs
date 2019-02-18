using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemInfo_BussinesCoupon_ForEditorCoupon : MonoBehaviour {


    public Text couponName;
    public Text couponDescription;

    public Text dropRate;

    public Text dropAmount;

    public Text dropIntergate;

    public Image couponImage;

    public void SetInfo(string itemname , string des , int rate, int amount , int limit, string path)
    {
        couponName.text = itemname;
        couponDescription.text = des;
        dropRate.text = "掉落概率 " + rate + "%";

        dropAmount.text = "一次掉落最多 " + amount +" 张";

        dropIntergate.text = "目前剩余 " + limit +  "张"; 

        AndaDataManager.Instance.GetUserImg(path,SetImage);
    }

    private void SetImage(Sprite _sp)
    {
        couponImage.sprite = _sp;
    }

	
}
