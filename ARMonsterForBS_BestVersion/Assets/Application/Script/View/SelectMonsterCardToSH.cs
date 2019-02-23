using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
public class SelectMonsterCardToSH : ViewBasic {
 
    public Transform mainCardPoint;
    public Transform grid;
    public Text tips;
    public Text selectTips;
    public Text centerTips;
    private BusinessStrongholdAttribute bsa;
    private ItemInfo_MonsterCar main_MonsterCard;
    private List<ItemInfo_MonsterCar> free_list ;
    public System.Action callback_UpdateMonsterCard;
    private bool unSave =false;
    public void SetInfo(BusinessStrongholdAttribute _bsa)
    {
        IsYou(true);

        bsa = _bsa;

        if(string.IsNullOrEmpty(bsa.monsterCardID))
        {
            centerTips.gameObject.SetActive(false);
            tips.enabled=true;
        }
        else
        {
            centerTips.gameObject.SetActive(true);
            tips.enabled = false;
            main_MonsterCard = BuildM_Item(bsa.monsterCardID, mainCardPoint);
        }

        BuildFreeMonsterItem();
    }


    private void BuildFreeMonsterItem()
    {
        List<BusinessSD_Pag4U>free = AndaDataManager.Instance.mainData.GetFreeMonster();
        if(free == null || free.Count == 0)
        {
            selectTips.text = "您暂时没有多余的星宿卡可以替换";
            return ;

        }

        selectTips.text = "请从下方选取您想要替换的星宿卡";

        int count = free.Count;



        for(int i = 0 ; i <count; i++)
        {
            if (free_list == null) free_list = new List<ItemInfo_MonsterCar>();
            ItemInfo_MonsterCar tt = BuildM_Item(free[i].commodityID, grid.transform);
            tt.SetCount(free[i].objectCount);
            tt.itemIndex = i;
            tt.callback_selectItem = SetMonsterCard;
            free_list.Add(tt);
        }
    }

    private ItemInfo_MonsterCar BuildM_Item(string iid,Transform parentsPoint)
    {
        GameObject item = AndaDataManager.Instance.InstantiateItem(AndaDataManager.ItemInfo_MonsterCard);
        AndaDataManager.Instance.SetInto(item.transform, parentsPoint);
        ItemInfo_MonsterCar t  = item.GetComponent<ItemInfo_MonsterCar>();
        t.SetInfo(iid);
        return t;
    }

    public void SetMonsterCard(string commodity,int itemIndex)
    {
        unSave = true;
        AndaDataManager.Instance.networkController.CallServerSetMonsterCard(bsa.strongholdIndex,commodity,(resutl,result2)=>
        {
            unSave = false;

            if(free_list[itemIndex].count - 1 <= 0)
            {
                ItemInfo_MonsterCar rus = free_list[itemIndex];
                free_list.RemoveAt(itemIndex);
                Destroy(rus.gameObject);
            }else
            {
                free_list[itemIndex].ReduceCount();
            }

            if (main_MonsterCard != null)
            {
                Destroy(main_MonsterCard.gameObject);

                ItemInfo_MonsterCar t = free_list.FirstOrDefault(s=>s.commodity_id == main_MonsterCard.commodity_id);
                if(t == null)
                {
                    ItemInfo_MonsterCar tmp = BuildM_Item(main_MonsterCard.commodity_id, grid);
                    tmp.callback_selectItem = SetMonsterCard;
                    free_list.Add(tmp);
                    tmp.itemIndex = free_list.Count - 1;
                    tmp.SetCount(1);
                }
                else
                {
                    t.AddCount();
                }

                if(callback_UpdateMonsterCard!=null)
                {
                    callback_UpdateMonsterCard();
                }
            }

            main_MonsterCard = BuildM_Item(bsa.monsterCardID, mainCardPoint);

        });
    }

    public void CallBackFinishSaveInfo()
    {
        isMe = true;
        unSave = false;
    }

    private Vector3 startMousePose;
    public void FixedUpdate()
    {
        if (gameObject.activeSelf)
        {
            if (isMe)
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
                        if(unSave)
                        {
                            isMe = false;
                            AndaUIManager.Instance.PlayTips("信息尚未保存完!");
                            return;
                        }
                        lastView.IsYou(true);

                        Destroy(gameObject);
                    }
                }
            }
        }
    }

}
