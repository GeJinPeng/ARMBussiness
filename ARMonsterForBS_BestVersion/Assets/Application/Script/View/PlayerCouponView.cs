using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCouponView : ViewBasic
{
    public GameObject main;


    public GameObject ScrollContent;
    public List<GameObject> ItemList;
    public int Type = 0; // -1全部 0未使用1已提交2已审核成功3已审核审核失败4已过期5已作废

    public Dropdown dropdown;

    public int selectTypeIndex = 0;//当前面板显示类别

    public Sprite sprite0;
    public Sprite sprite1;
    public Sprite sprite2;
    public Sprite sprite3;

    public Animator animator;
    public void Init()
    {
        if (AndaPlayerCouponManager.Instance.playerCouponView == null)
            AndaPlayerCouponManager.Instance.playerCouponView = this;
        InstanceContentPanel();
    }


    public void ShowMain()
    {
        Init();
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
        Destroy(gameObject);
    }
    public void InstanceContentPanel()
    {
        AndaPlayerCouponManager.Instance.SetPlayerCoupon();
    }
    public void SetContentPanel(List<PlayerCoupon> list)
    {
        if (ItemList == null)
            ItemList = new List<GameObject>();
        if (list == null)
            return;
        int count = list.Count;
        if (ItemList != null)
        {
            foreach (var m in ItemList)
            {
                Destroy(m);
            }
            ItemList.Clear();
        }
        for (int i = 0; i < count; i++)
        {
            GameObject item = Instantiate(AndaDataManager.Instance.GetItemInfoPrefab("PlayerCouponItem"));
            item.transform.parent = ScrollContent.transform;
            item.transform.localScale = Vector3.one;
            item.transform.localPosition = Vector3.zero;
            item.GetComponent<ItemInfo_PlayerCoupon>().SetInfo(this,  list[i]);
            ItemList.Add(item);
        }
    }
    public void Cheack()
    {
        AndaPlayerCouponManager.Instance.Cheack();
    }
    public void Fail()
    {
        AndaPlayerCouponManager.Instance.Fail();
    }
    public void ChangeType(int type)
    {

        if (ItemList == null)
            return;
        foreach (var m in ItemList)
        {
            if (m.GetComponent<ItemInfo_PlayerCoupon>().playerCoupon.status == type)
            {
                m.SetActive(true);
            }
            else
            {
                m.SetActive(false);
            }
        }
    }


    public void RightSelect()
    {
        selectTypeIndex--;
        if (selectTypeIndex < 0)
            selectTypeIndex = 3;
        dropdown.value=selectTypeIndex;
    }
    public void LeftSelect()
    {
        selectTypeIndex++;
        if (selectTypeIndex >3)
            selectTypeIndex = 0;
        dropdown.value = selectTypeIndex;
    }

    public void DropSelect()
    {
        dropdown.OnPointerClick(null);
    }
}
