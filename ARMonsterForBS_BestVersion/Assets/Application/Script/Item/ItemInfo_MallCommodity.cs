using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemInfo_MallCommodity : MonoBehaviour {

    public Text commodityName;
    public Text commodityDescription;
    public Image commodityImage;
    public Dropdown dropdown;
    public Image dropdownArrow;
    public GameObject giftBtn;

    private CommodityStructure commodityStructure;
    private List<PayWay> payWays;

    private MallCommodityStructure mallCommodityStructure;
    public void SetInfo(MallCommodityStructure data)
    {
        mallCommodityStructure = data;
        payWays = new List<PayWay> ();

        List<Dropdown.OptionData> optionDatas =new List<Dropdown.OptionData>();

        commodityStructure = GetGameConfigData.GetCommodityStructureItem(data.commodityID);
    
        if(data.commodityName == null)
        {
            commodityName.text = commodityStructure.commodityName + " x" + data.count;

        }else
        {
            commodityName.text = data.commodityName;
        }

        commodityDescription.text = data.description;

        commodityImage.sprite = Resources.Load<Sprite>("Sprite/Commodity/" + data.commodityID);

        commodityImage.SetNativeSize();

        int c = data.priceType.Count;
        for(int i = 0;i < c; i ++)
        {
            if(data.priceType[i]!=0)
            {
               
                payWays.Add(new PayWay
                {
                    payType = i,
                    payPrice = data.price[i]
                });


                optionDatas.Add( new Dropdown.OptionData
                {
                    text = data.price[i].ToString(),
                    image = Resources.Load<Sprite>("Sprite/Currency/currency" + i )
                });

               
            }

        }

        dropdown.options = optionDatas;
        dropdown.value  = 0;
        ChangePayWay(0);

        giftBtn.gameObject.SetActive(data.child!=null&&data.child.Count!=0);
    }


    public void ChangePayWay(int index)
    {

        dropdown.captionText.text = payWays[dropdown.value].payPrice.ToString();
        dropdownArrow.sprite = dropdown.options[dropdown.value].image;// Resources.Load<Sprite>("Sprite/Commodity/currency" + payWays[index].payType);
    }

    public void ClickOpenGfitBar()
    {
        GameObject bar = Resources.Load<GameObject>("Prefab/GiftBar");
        bar= Instantiate(bar);
        AndaUIManager.Instance.SetIntoCanvas(bar.transform);
        GiftBar giftBar = bar.GetComponent<GiftBar>();
        giftBar.SetInfo(mallCommodityStructure.child);
    }

    public void ClickPay()
    {
       // AndaDataManager.Instance.networkController.VerifyAppleBuy("json");

        //AndaDataManager.Instance.mainContoller.Purchase("commodityID" + mallCommodityStructure.commodityIndex);
   
        AndaDataManager.Instance.networkController.CallServerBuyCommodity(mallCommodityStructure.commodityID,100, CallBackFinishBuy);

    }

    private void CallBackFinishBuy(BusinessSD_Pag4U businessSD_Pag4U)
    {
        AndaUIManager.Instance.PlayTipsForBuySuccess();
    }



}


