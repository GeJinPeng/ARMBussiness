using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class BusinessCouponView : ViewBasic {

    public GameObject main;
    public GameObject ScrollContent;
    public List<ItemInfo_BusinessCoupon> ItemList;
    public int Type = 0; // -1全部 0未使用1已提交2已审核成功3已审核审核失败4已过期5已作废

    public Dropdown dropdown;

    public int selectTypeIndex = 0;//当前面板显示类别

    public GameObject leftBtn;
    public GameObject rightBtn;
    public Text centerTitles;
    public Text leftTitle;
    public Text rightTitle;

    public Sprite sprite0;
    public Sprite sprite1;
    public Sprite sprite2;
    public Sprite sprite3;
    public Animator animator;

    public GiftEditionBar giftEditionBar;

    private int currentIndex = 0;

    private int realCount = 0;//当前有效显示数量；


    private int[] types = new  int [3]{-1,0,2};
    private string[] titles = new string[]
    {
        "待上架","已上架","作废"
    };
    private float waitTime ;//false;


    public void OnEnable()
    {
        Invoke("InvokeEnter", 0.5f);
    }

    private void InvokeEnter()
    {
        IsYou(true);
        ShowMain();
    }

    public void ShowMain()
    {
        SetContentPanel();
        if (main.activeSelf)
            CloseMain();
        else
        {
            gameObject.SetActive(true);
            main.SetActive(true);
            animator.Play("OpenMenu");
        }
    }

    public void CloseMain()
    {
        animator.Play("CloseMenu");
    }
    public void SetActiveTrue()
    {
        main.SetActive(true);
    }
    public void SetActiveFalse()
    {
        main.SetActive(false);
    }
   
    public void SetContentPanel()
    {
       
        if (AndaDataManager.Instance.mainData.bussinessReward == null)
            return;

        SetCurrentType();
    }

    public void SetCurrentType()
    {
        centerTitles.text = titles[currentIndex];
        CheckBtnState();
        ScrollContent.gameObject.SetActive(false);
        if (waitTime.Equals(0))
        {
            StartCoroutine(Wait());
        }else
        {
            waitTime = 0;
        }
    }

    public void ClickNextGroup()
    {
        currentIndex+=1;
        currentIndex = Mathf.Clamp(currentIndex,0,2);
        SetCurrentType();
    }

    public void ClickLastGroup()
    {
        currentIndex -= 1;
        currentIndex = Mathf.Clamp(currentIndex, 0, 2);
        SetCurrentType();
    }

    private void CheckBtnState()
    {
        if(currentIndex < types.Length - 1 )
        {
            rightTitle.text = titles[currentIndex+1];
            rightBtn.gameObject.SetActive(true);
        }else
        {
            rightBtn.gameObject.SetActive(false);
        }
          
        if(currentIndex > 0)
        {
            leftTitle.text = titles[currentIndex - 1];
            leftBtn.gameObject.SetActive(true);
        }
        else
        {
            leftBtn.gameObject.SetActive(false);
        }

         
         
    }

    private IEnumerator Wait()
    { 
        while(waitTime <0.25f)
        {
            waitTime+=Time.deltaTime;
            yield return null;
        }


        ScrollContent.gameObject.SetActive(true);
        waitTime = 0f;
        BuildItem();
    }

    private List<BussinessRewardStruct> SortItem()
    {
        List<BussinessRewardStruct> list = null;
        List<BussinessRewardStruct> tmp = AndaDataManager.Instance.mainData.bussinessReward;
        int count = tmp.Count;
        for (int i = count-1 ; i >= 0; i--)
        {
            if(tmp[i].status == types[currentIndex])
            {
                if(list == null) list = new List<BussinessRewardStruct>();
                list.Add(tmp[i]);
            }
        }

        return list;
    }

    private void ResetCoupon(int index, BussinessRewardStruct brs)
    {
        ItemList[index].gameObject.SetActive(true);
        ItemList[index].SetInfo(this, brs);
        SetEditorIdf(ItemList[index]);
    }
    private void UpdateCoupon()
    {

    }

    private void AddCoupon(BussinessRewardStruct brs)
    {
        GameObject item = AndaDataManager.Instance.InstantiateItem(AndaDataManager.BussinessCouponItem);
        AndaDataManager.Instance.SetInto(item.transform, ScrollContent.transform);
        ItemInfo_BusinessCoupon it = item.GetComponent<ItemInfo_BusinessCoupon>();
        it.SetInfo(this, brs);

        if(ItemList == null) ItemList = new List<ItemInfo_BusinessCoupon>();
        ItemList.Add(it);
        SetEditorIdf(it);
    }

    private void SetEditorIdf(ItemInfo_BusinessCoupon it)
    {
        it.ClickItem_ToEditor = null;
        it.ClickItem_UploadtoSell = null;
        it.ClickItem_DowntoSell = null;
        switch (types[currentIndex])
        {
            case -1:
                it.ClickItem_ToEditor = ClickRewardToEditor;
                it.ClickItem_UploadtoSell = CallbackUploadToSell;//移除
                break;
            case 0:
                it.ClickItem_DowntoSell = CallbackUploadToSell;//也是移除
                break;
            case 2:

                break;
        }
    }

    public void BuildItem()
    {
        List<BussinessRewardStruct> t = SortItem();
        if(t == null || t.Count == 0)return; 
        int count = t.Count;
        realCount = count;

        if (ItemList == null || ItemList.Count == 0)
        {
            for (int i = 0; i < count; i++)
            {
                AddCoupon(t[i]);
            }
        }
        else
        {
            //需要添加新的位置
            if(ItemList.Count < count)
            {
                for (int i = 0 ; i < count; i++)
                {
                    if(i < ItemList.Count)
                    {
                        ResetCoupon(i,t[i]);
                    }
                    else
                    {
                        AddCoupon(t[i]);
                    }
                }
            }else
            {
                //不用更添加新的位置，多出来的隐藏
                //不够的新增
                int ItemListCount = ItemList.Count;
                for (int i = 0; i < ItemListCount; i++)
                {
                    if (i < count)
                    {
                        ResetCoupon(i,t[i]);
                    }
                    else
                    {
                        ItemList[i].gameObject.SetActive(false);
                    }
                }
            }
        }
    }

    private void ClearItem()
    {
        if(ItemList!=null)
        {
            int count = ItemList.Count;
            for (int i = 0; i < count; i++)
            {
                Destroy(ItemList[i]);
            }
            ItemList.Clear();
        }
    }


    public void RefreshAddContentPanel(BussinessRewardStruct _coupon)
    {
        if(ItemList!=null && ItemList.Count!=0)
        {
            BussinessRewardStruct _tmp = ItemList[0].brs;
            if (realCount < ItemList.Count) //
            {

                ItemList[realCount].gameObject.SetActive(true);
                ItemList[realCount].SetInfo(this, _tmp);
                ItemList[0].SetInfo(this, _coupon);
                SetEditorIdf(ItemList[realCount]);

            }
            else
            {
                AddCoupon(_tmp);
                ItemList[0].SetInfo(this, _coupon);
            }


        }else
        {
            AddCoupon(_coupon);
        }
        realCount++;

    }

    public void RefreshEditContentPanel(BussinessRewardStruct _coupon)
    {
        if (ItemList != null)
        {
            foreach (var m in ItemList)
            {
                if (m.coupon.businesscouponIndex == _coupon.businesscouponIndex)
                {
                    //
                    int index = ItemList.IndexOf(m);
                    //重置
                    ResetCoupon(index,_coupon);
                    /*var info = m.GetComponent<ItemInfo_BusinessCoupon>().coupon;
                    info.title = _coupon.title;
                    info.businessname = _coupon.businessname;
                    info.changeCount = _coupon.changeCount;
                    info.code = _coupon.code;
                    info.continueTime = _coupon.continueTime;
                    info.createTime = _coupon.createTime;
                    info.description = _coupon.description;
                    info.endtime = _coupon.endtime;
                    info.failCount = _coupon.failCount;
                    info.fallenCount = _coupon.fallenCount;
                    info.image = _coupon.image;
                    info.porIsUpdate = _coupon.porIsUpdate;
                    info.rewardcomposeID = _coupon.rewardcomposeID;
                    info.rewardDropCount = _coupon.rewardDropCount;
                    info.rewardDropRate = _coupon.rewardDropRate;
                    info.starttime = _coupon.starttime;
                    info.status = _coupon.status;
                    info.strongholdIndex = _coupon.strongholdIndex;
                    info.tips = _coupon.tips;
                    info.type = _coupon.type;
                    info.totalCount = _coupon.totalCount;
                    info.userIndex = _coupon.userIndex;

                    m.GetComponent<ItemInfo_BusinessCoupon>().SetInfo(this, info);*/
                }
            }
        }
    }


    public void CallbackUploadToSell(int _brsIndex)
    {
        ItemInfo_BusinessCoupon itemInfo =
            ItemList.FirstOrDefault(s => s.brs.businesscouponIndex == _brsIndex);
        int indexOf = ItemList.IndexOf(itemInfo);
        ItemList.RemoveAt(indexOf);
        Destroy(itemInfo.gameObject);
    }

    


    public void Cheack()
    {
        AndaPlayerCouponManager.Instance.Cheack();
    }
    public void Fail()
    {
        AndaPlayerCouponManager.Instance.Fail();
    }
    /*public void ChangeType(int type)
    {
        if (ItemList == null)
            return;
        foreach (var m in ItemList)
        {
            if (m.GetComponent<ItemInfo_BusinessCoupon>().coupon.status == type)
            {
                m.SetActive(true);
            }
            else
            {
                m.SetActive(false);
            }
        }
    }*/


    public void RightSelect()
    {
        selectTypeIndex--;
        if (selectTypeIndex < 0)
            selectTypeIndex = 2;
        dropdown.value = selectTypeIndex;
    }
    public void LeftSelect()
    {
        selectTypeIndex++;
        if (selectTypeIndex > 2)
            selectTypeIndex = 0;
        dropdown.value = selectTypeIndex;
    }

    public void DropSelect()
    {
        dropdown.OnPointerClick(null);
    }


    public void ClickAdditionCoupon()
    {
        IsYou(false);
        GameObject view = AndaDataManager.Instance.InstantiateItem(AndaDataManager.AdditionEditorCouponView);
        AndaUIManager.Instance.SetIntoCanvas(view.transform);
        AdditionEditorCoupon bsv = view.GetComponent<AdditionEditorCoupon>();
        bsv.lastView = this;
        bsv.callback_addtionItem = RefreshAddContentPanel;
        bsv.StartView();

    }

    public void ClickRewardToEditor(BussinessRewardStruct brs)
    {
        IsYou(false);
        GameObject view = AndaDataManager.Instance.InstantiateItem(AndaDataManager.AdditionEditorCouponView);
        AndaUIManager.Instance.SetIntoCanvas(view.transform);
        AdditionEditorCoupon bsv =  view.GetComponent<AdditionEditorCoupon>();
        bsv.lastView = this;
        bsv.callbakc_updateItem = RefreshEditContentPanel;
        bsv.EditorCoupon(brs);
    }



    private Vector3 startMousePose;
    public void Update()
    {
        if(gameObject.activeSelf)
        {
            if(isMe)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    startMousePose = Input.mousePosition;
                }

                if (Input.GetMouseButtonUp(0))
                {
                    //float delta = Mathf.Abs(i)
                    if (Input.mousePosition.x - startMousePose.x > 500)
                    {

                        Destroy(gameObject);
                    }
                }
            }
        }
    }

}
