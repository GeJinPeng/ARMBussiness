using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemInfo_SmallBSCoupon : MonoBehaviour {
    public GameObject newItem;
    public GameObject imageGroup;
    public Text c_name;
    public Text c_description;
    public Text c_fullCount;//cur/full
    public Text c_dropCount;
    public Text c_dropRate;
    public Image c_image;

    public System.Action callback_clickItem;

    public System.Action<BussinessRewardStruct,int> callback_selectItem;

    public BussinessRewardStruct data;
    public int itemIndex;
    public void SetInfo(BussinessRewardStruct dagta)
    {
        if(dagta == null) 
        {
            newItem.gameObject.SetActive(true);
            imageGroup.gameObject.SetActive(false);
            return;
        }
        newItem.gameObject.SetActive(false);
        imageGroup.gameObject.SetActive(true);
        data =dagta;
        c_name.text = dagta.title;
        c_description.text = dagta.description;
        c_fullCount.text =( dagta.totalCount - dagta.changeCount) + "/" + dagta.totalCount;
        c_dropCount.text =dagta.fallenCount.ToString();
        c_dropRate.text = dagta.rewardDropRate +"%";

        AndaDataManager.Instance.GetUserImg(AndaDataManager.Instance.mainData.playerData.headImg,(result)=>
        {
            c_image.sprite = result;
        });
         
    }

    public void ClickItem()
    {
        if(callback_clickItem!=null)
        {
            callback_clickItem();
        }
        if(callback_selectItem!=null)
        {
            callback_selectItem(data,itemIndex);
        }
    }
}
