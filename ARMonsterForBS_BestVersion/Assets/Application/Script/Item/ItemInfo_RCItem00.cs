using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemInfo_RCItem00 : MonoBehaviour {
 
    public Text itemname;

    public Text itemLessCount;

    public Image itemImage;

    public System.Action<string> callback_clickItem;

    public void SetInfo(string itemName, Sprite itemSprite, int _itemLessCount)
    {
        itemname.text = itemName;
        itemImage.sprite = itemSprite;
        itemLessCount.text =  "剩余 x" + _itemLessCount +"张";
    }

    public void ClickItem(Transform itemName)
    {
        if(callback_clickItem!=null)
        {
            callback_clickItem(itemName.name);
        }
    }
}
