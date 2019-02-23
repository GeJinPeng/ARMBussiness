using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class MainData  {

    public BusinessData playerData ;
    public string token;
    public Sprite userImage;
    public List<BusinessStrongholdAttribute> businessStrongholdAttributes;
    public List<LD_Objs> strongholdDrawingList = new List<LD_Objs>();
    public List<LD_Objs> strongholdRewardCardList = new List<LD_Objs>();
    public List<BussinessRewardStruct> bussinessReward;   
    public List<BusinessSD_Pag4U> freeMonsterCard ;
    public List<BusinessSD_Pag4U> monsterCard;
    public List<BusinessSD_Pag4U> bussinessPag =new List<BusinessSD_Pag4U>();
    public Sprite imgPor;


    public System.Action<BusinessStrongholdAttribute> UpdateStrongholdDataEvent;
    public void SetUserInformation(BusinessData _playerData , string _token)
    {
        token =_token;
        playerData = _playerData;
        bussinessReward = playerData.businessCoupons;
        bussinessPag = playerData.businessSD_Pag4Us;
        int count = bussinessReward.Count;
        for(int i = 0; i <count; i ++)
        {
            Debug.Log("CouponState" + bussinessReward[i].status);
        }


        BuildBussinessStrongholdAttrubte();
        BuildStrongholdDawingList();
        SetMonsterCard();
       
        GetImg();
    }

    public void BuildBossListConfig()
    {

    }

    #region 计算出可以使用的monster Card

    public void SetMonsterCard()
    {
        int count = playerData.businessSD_Pag4Us.Count;
        for(int i = 0 ; i < count; i++ )
        {
            if(playerData.businessSD_Pag4Us[i].commodityID.Substring(0,5) == "ms_bs")
            {
                if(monsterCard == null) monsterCard = new List<BusinessSD_Pag4U>();
                monsterCard.Add(playerData.businessSD_Pag4Us[i]);
                if(freeMonsterCard == null) freeMonsterCard = new List<BusinessSD_Pag4U>();
                freeMonsterCard.Add(playerData.businessSD_Pag4Us[i]);
            }
        }
        
    }


    #endregion

    #region 计算出可以使用的怪兽卡

    public List<BusinessSD_Pag4U> GetFreeMonster()
    {
        if(monsterCard == null || monsterCard.Count == 0) return null;
        int count = freeMonsterCard.Count;
        int shCount = businessStrongholdAttributes.Count;
        for (int i = 0 ;i < count; i++)
        {
            for(int j = 0 ; j < shCount; j++)
            {
                if(businessStrongholdAttributes[j].monsterCardID == freeMonsterCard[i].commodityID)
                {
                    freeMonsterCard[i].objectCount -=1;
                }
            }
        }

        return freeMonsterCard;
    }

    #endregion

    #region 向服务器所要头像数据

    private void GetImg()
    {
        AndaDataManager.Instance.CallServerGetUserImg(playerData.headImg, CallBackGetImg);
    }

    private void CallBackGetImg(Sprite _value)
    {
        imgPor = _value;
    }

    #endregion
    #region 构造据点数据
    public void BuildBussinessStrongholdAttrubte()
    {
        int count = playerData.strongholdList.Count;
        for(int i = 0 ; i < count; i++)
        {
            if(businessStrongholdAttributes == null)
            {
                businessStrongholdAttributes = new List<BusinessStrongholdAttribute>();
            }
            BusinessStrongholdAttribute _b = ConvertTool.ConvertToBussinessStrongholdAttribute(playerData.strongholdList[i]);
            businessStrongholdAttributes.Add(_b);
    
        }
    }
    #endregion
    #region 添加据点
    public void AddBussinessStronghold(BusinessStrongholdAttribute business)
    {
        if(businessStrongholdAttributes == null)businessStrongholdAttributes = new List<BusinessStrongholdAttribute>(); 
        businessStrongholdAttributes.Add(business);
    }
    #endregion

    #region 添加奖励券

    public void AddBussinessRewar(BussinessRewardStruct br )
    {
        if(bussinessReward == null) bussinessReward = new List<BussinessRewardStruct>();
        bussinessReward.Add(br);
    }

    #endregion

    #region 构造物品数据
    public void BuildStrongholdDawingList()
    {
        int count = playerData.playerObjects.Count;
        for(int i = 0; i < count; i++)
        {
            int idType = AndaDataManager.Instance.GetObjTypeID(playerData.playerObjects[i].objectID);
            Debug.Log("idType" +idType);
            if(idType == 42010)
            {
                LD_Objs lD_Objs = ConvertTool.ConvertToLd_objs(playerData.playerObjects[i]);
                strongholdDrawingList.Add(lD_Objs);
            }else if(idType == 43000)
            {
                LD_Objs lD_Objs = ConvertTool.ConvertToLd_objs(playerData.playerObjects[i]);
                if(playerData.playerObjects[i].objectID == 43004)
                {
                    strongholdRewardCardList.Add(lD_Objs);
                }
            }
        }
    }
    #endregion

    #region 添加和减少

    public void AddCommodity(BusinessSD_Pag4U item)
    {
        BusinessSD_Pag4U bs = bussinessPag.FirstOrDefault(s=>s.objectIndex == item.objectIndex);
        if(bs == null)
        {
            bussinessPag.Add(item);
        }else
        {
            bs.objectCount += item.objectCount;
        }
    }

    public void ReducCommodity(BusinessSD_Pag4U item)
    {
        BusinessSD_Pag4U bs = bussinessPag.FirstOrDefault(s => s.objectIndex == item.objectIndex);
        if (bs == null)
        {
            return;
        }
        else
        {

            bs.objectCount -= item.objectCount;
            if (bs.objectCount<0) bs.objectCount=0;
        }
    }

    public void ReduceComodityForID(string id)
    {
        bussinessPag.FirstOrDefault(s => s.commodityID == id).objectCount-=1;
    }
     

    #endregion

    #region 放置怪兽卡成功  要减去一张 同时更新据点数据

    public void ReduceFreemonsterCard(string monsterID)
    {
        if(freeMonsterCard!=null && freeMonsterCard.Count!=0)
        {
            freeMonsterCard.FirstOrDefault(s=>s.commodityID == monsterID).objectCount-=1;
        }
    }

    public void AddFreemonsterCard(string monsterID)
    {
        freeMonsterCard.FirstOrDefault(s=>s.commodityID == monsterID).objectCount +=1;
    }

    public void UpdateStrongholdMonsterCard(int shIndex, string monsterID)
    {
        string lastMonsterCardID = businessStrongholdAttributes.FirstOrDefault(s => s.strongholdIndex == shIndex).monsterCardID;
        if(!string.IsNullOrEmpty(lastMonsterCardID))
        {
            AddFreemonsterCard(lastMonsterCardID);
        }
        ReduceFreemonsterCard(monsterID);
        businessStrongholdAttributes.FirstOrDefault(s => s.strongholdIndex == shIndex).monsterCardID = monsterID;
    }

    #endregion

    #region 获取据点数据

    public BusinessStrongholdAttribute GetAtrongholdAttributes(int _index)
    {
        return businessStrongholdAttributes.FirstOrDefault(s=>s.strongholdIndex == _index);
    }

    public void GetAllStrongholdAttributes()
    {

    }

    public List<LD_Objs> GetMineStrongholdDrawing()
    {
        return strongholdDrawingList;
    }

    public int GetCommodityCountForSH_BS_00()
    {
        if(bussinessPag == null || bussinessPag.Count == 0)return 0;
        BusinessSD_Pag4U pag4U = bussinessPag.FirstOrDefault(s=>s.commodityID == "sh_bs_00");
        if(pag4U == null || pag4U.objectCount == 0)return 0;
        else return pag4U.objectCount;
    }
    #endregion

    #region 获取奖励数据

    public BussinessRewardStruct GetRewardData(int _index)
    {
        if(bussinessReward == null)return null;
        return bussinessReward.FirstOrDefault(s=>s.businesscouponIndex == _index);
    }



    #endregion


    #region 获取可以使用的奖励券

    public List<BussinessRewardStruct> GetCanusingRewardData()
    {
        List<BussinessRewardStruct> list  = null;
        int count = bussinessReward.Count;
        for(int i = 0 ; i <count;i ++)
        {
            if(bussinessReward[i].status == 0)
            {
                if(list == null) list = new List<BussinessRewardStruct>();
                list.Add(bussinessReward[i]);
            }
        }

        return list;//会有空值的情况，请一定要注意判空
    }

    #endregion


    #region 更新据点奖励信息

    public void UpdateAddStrongholdReward(int shIndex,int rewardIndex)
    {
        BusinessStrongholdAttribute bs = businessStrongholdAttributes.FirstOrDefault(s => s.strongholdIndex == shIndex);
        if(bs.coupons == null) 
        {
            bs.coupons = new List<int>();
            bs.coupons.Add(rewardIndex);
        }else
        {
            bs.coupons[0] = rewardIndex;
        }


        Debug.Log("AfterAdd" + bs.coupons.Count);
        //businessStrongholdAttributes.FirstOrDefault(s=>s.strongholdIndex == shIndex).coupons.Add(rewardIndex);
    }

    public void UpdateReduceStrongholdReward(int shIndex,int rewardIndex)
    {
        businessStrongholdAttributes.FirstOrDefault(s => s.strongholdIndex == shIndex).coupons.Remove(rewardIndex);
    }

    public void AddBussinessCoupon(BussinessRewardStruct _brs)
    {
        playerData.businessCoupons.Add(_brs);
    }

    public void UpdateStrongholdNickName(int shIndex , string nickName)
    {
        BusinessStrongholdAttribute bsa =  businessStrongholdAttributes.FirstOrDefault(s => s.strongholdIndex == shIndex);
        bsa.strongholdNickName = nickName;
        if (UpdateStrongholdDataEvent!=null)
        {
            UpdateStrongholdDataEvent(bsa);
        }
    }

    public void UpdateStrongholdDescpriton(int shIndex, string des)
    {
        BusinessStrongholdAttribute bsa = businessStrongholdAttributes.FirstOrDefault(s => s.strongholdIndex == shIndex);
        bsa.description = des;
        if (UpdateStrongholdDataEvent != null)
        {
            UpdateStrongholdDataEvent(bsa);
        }
    }

    #region 更新票据
    public void UpdateBussinessCouponState(BussinessRewardStruct _brs)
    {
        BussinessRewardStruct brs = 
            playerData.businessCoupons.FirstOrDefault(
                s=> s.businesscouponIndex == _brs.businesscouponIndex);
        if(brs == null) return;
        brs = _brs;
    }

    public void UpdateBussinessCouponStateToUploadSell(int brsIndex )
    {
        BussinessRewardStruct brs =
            playerData.businessCoupons.FirstOrDefault(
                s => s.businesscouponIndex == brsIndex);
        if (brs == null) return;
        brs.status = 0;
    }
    /// <summary>
    /// 下架
    /// </summary>
    /// <param name="brsIndex">Brs index.</param>
    public void UpdateBussinessCouponStateDownSell(int brsIndex)
    {
        BussinessRewardStruct brs =
            playerData.businessCoupons.FirstOrDefault(
                s => s.businesscouponIndex == brsIndex);
        if (brs == null) return;
        brs.status = 2;
    }
    #endregion

    #endregion


    #region 更新广告

    public void UpdateStrongholdAds(AdsStruct adsStruct ,int shIndex)
    {
        BusinessStrongholdAttribute bsa = businessStrongholdAttributes.FirstOrDefault(s=>s.strongholdIndex == shIndex);
        if(bsa.adsInfos == null || bsa.adsInfos.Count == 0)
        {
            bsa.adsInfos = new List<AdsStruct>();
            bsa.adsInfos.Add(adsStruct);
        }else
        {
            if(bsa.adsInfos.Count -1 < adsStruct.itemIndex)
            {
                bsa.adsInfos.Add(adsStruct);
            }
            else
            {
                bsa.adsInfos[adsStruct.itemIndex] = adsStruct;
            }
        }
    }


    #endregion
}
