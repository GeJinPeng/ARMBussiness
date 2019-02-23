using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemInfo_MonsterCar : MonoBehaviour {

   
    public Text tips;

    public Image monsterPor;

    public Text description;

    public Text itemName;

    public Text hotValue;

    public Text countText;

    public System.Action callbcak_clickItem;

    public System.Action<string,int> callback_selectItem;

    public string commodity_id;
    public int count;
    public int itemIndex;
    public void SetInfo(string commodityID)
    {
        if(string.IsNullOrEmpty(commodityID))
        {
            monsterPor.gameObject.SetActive(false);
            if (tips != null) tips.gameObject.SetActive(true);
            return;
        }
        monsterPor.gameObject.SetActive(true);
        if(tips!=null) tips.gameObject.SetActive(false);

        commodity_id = commodityID;
        CommodityStructure commodityStructure = GetGameConfigData.GetCommodityStructureItem(commodityID);
        monsterPor.sprite = AndaDataManager.Instance.GetCommoditySprite(commodityStructure.commodityID);
        description.text = commodityStructure.description;
        itemName.text = commodityStructure.commodityName;
        hotValue.text = GetGameConfigData.GetMonsterLevelBoxByMonsterID(commodityID).hotValue.ToString();
     
       
    }

    public void SetCount(int _count)
    {
        count = _count;
        countText.gameObject.SetActive(true);
        countText.text = "x" +_count.ToString();
    }

    public void ReduceCount()
    {
        count-=1;
        SetCount(count);
    }
    public void AddCount()
    {
        count += 1;
        SetCount(count);
    }




    public void DragItem()
    {

    }

    public void ClickItem()
    {
        if(callbcak_clickItem!=null)
        {
            callbcak_clickItem();
        }

        if(callback_selectItem!=null)
        {
            callback_selectItem(commodity_id,itemIndex);
        }
    }
}
