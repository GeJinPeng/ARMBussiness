using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class itemInfo_GiftBar : MonoBehaviour {

    public Text commodityName;
    public Text commodityDescription;
    public Image commodityImage;


    public void SetInfo(Sprite sprite , string name, string description)
    {
        commodityName.text = name;
        commodityDescription.text = description;
        commodityImage.sprite = sprite;

        commodityImage.SetNativeSize();
    }
}
