using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
public class NetworkController : MonoBehaviour {
    public string networkAdress3 = "http://47.99.45.109:8081/ConfigTxt/";
    public string networkAdress4 = "http://47.99.45.109:8081/";
    public string networkAdress2 = "http://47.99.45.109:8081/api/";
    private void Awake()
    { 
        AndaDataManager.Instance.networkController = this;
    }

    #region 登录
    public void Login(System.Action<bool> callback, string name)
    {
        var _wForm = new WWWForm();
        _wForm.AddField("acc", name);
        _wForm.AddField("pwd", "000000");
        string path = "http://47.99.45.109:8081/api/Login/login";
        //string path = "http://localhost:57789/api/Login/Login";
        StartCoroutine(ExcuteLogin(path, _wForm, callback));
    }

    private IEnumerator ExcuteLogin(string _url, WWWForm _wForm, System.Action<bool> callBack)
    {
        AndaUIManager.Instance.OpenWaitBoard(true);
        WWW postData = new WWW(_url, _wForm);
        yield return postData;

        if (postData.error != null)
        {
            Debug.Log(postData.error);
        }
        else
        {
            var data = JsonMapper.ToObject<PlayerLogin>(postData.text);
            Debug.Log(postData.text);
            if (data.code == "200")
            {
                AndaDataManager.Instance.SetUserData(data.BusinessData,data.token);
                //AndaDataManager.Instance.SetUserData(data.PlayerData);
                //AndaDataManager.Instance.SetUserToken(data.token);
            }
            callBack(data.code == "200");
        }
        AndaUIManager.Instance.OpenWaitBoard(false);
    }

    #endregion


    #region 从服务获取配置文件

    public void GetBaseConfig(System.Action<ConfigBase> callback)
    {
        StartCoroutine(ExcuteGetBaseConfig(callback));
    }

    private IEnumerator ExcuteGetBaseConfig(System.Action<ConfigBase> callback)
    {
        AndaUIManager.Instance.OpenWaitBoard(true);
        WWW configStr = new WWW(networkAdress3 + "ConfigBase.txt");
        yield return configStr;
        AndaUIManager.Instance.OpenWaitBoard(false);
        if (string.IsNullOrEmpty(configStr.error))
        {
            callback(JsonMapper.ToObject<ConfigBase>(configStr.text));
            //Debug.Log("configStr.text" + configStr.text);
        }
        else
        {
            // Debug.Log("configStr.text is null" + configStr.text);
        }
      
    }


    public void GetConfigFils(List<ConfigFile> files, System.Action<List<LocalConfigFile>> callback)
    {
        StartCoroutine(ExcuteGetConfigfiles(files, callback));
    }

    private IEnumerator ExcuteGetConfigfiles(List<ConfigFile> files, System.Action<List<LocalConfigFile>> callback)
    {
        AndaUIManager.Instance.OpenWaitBoard(true);
        int count = files.Count;
        List<LocalConfigFile> localConfigs = new List<LocalConfigFile>();
        for (int i = 0; i < count; i++)
        {
            string _name = files[i].name;
            int version = files[i].lastWriteTime;
            WWW link = new WWW(networkAdress3 + _name);
            yield return link;
            if (string.IsNullOrEmpty(link.error))
            {
                localConfigs.Add(new LocalConfigFile
                {
                    name = _name,
                    lastWriteTime = version,
                    config = link.text
                });
            }
        }
        AndaUIManager.Instance.OpenWaitBoard(false);
        if (localConfigs.Count != 0)
        {
            callback(localConfigs);
        }
      //  AndaUIManager.Instance.OpenWaitBoard(false);
    }

    #endregion

    #region 获取周围的数据
    public void GetCurrentLocationRangeOtherData(List<double> location, System.Action<List<PlayerStrongholdAttribute>, List<BusinessStrongholdAttribute>> callback)
    {
        var _wForm = new WWWForm();

        _wForm.AddField("positionx", location[1].ToString());
        _wForm.AddField("positiony", location[0].ToString());

        StartCoroutine(GetCurrentLocationRangeOtherData("http://47.99.45.109:8081/api/Region/GetRegion", _wForm, callback));
    }

    private IEnumerator GetCurrentLocationRangeOtherData(string _url, WWWForm _wForm, System.Action<List<PlayerStrongholdAttribute>, List<BusinessStrongholdAttribute>> callback)
    {
        AndaUIManager.Instance.OpenWaitBoard(true);
        WWW postData = new WWW(_url, _wForm);
        yield return postData;

        AndaUIManager.Instance.OpenWaitBoard(false);
        if (postData.error != null)
        {
            Debug.Log(postData.error);
        }
        else
        {

            var data = JsonMapper.ToObject<Region>(postData.text);

            List<PlayerStrongHoldGrowUpAttribute> tmpPlayerStronghold = data.resRegion.PlayerStrongHoldlist;


            List<BusinessStrongholdGrowUpAttribute> tmpBusinessStronghold = data.resRegion.BusinessStrongHoldlist;

            List<PlayerStrongholdAttribute> playerStrongholds = new List<PlayerStrongholdAttribute>();
            List<BusinessStrongholdAttribute> businessStrongholds = new List<BusinessStrongholdAttribute>();

            foreach (var go in tmpPlayerStronghold)
            {
                PlayerStrongholdAttribute psa = ConvertTool.ConvertToPlayerstrongholdAttribute(go);
                //Debug.Log("go.strongholdGloryValue" + go.strongholdGloryValue);
                playerStrongholds.Add(psa);
            }

            foreach (var go in tmpBusinessStronghold)
            {
                BusinessStrongholdAttribute bsa = ConvertTool.ConvertToBussinessStrongholdAttribute(go);
                bsa.strongholdID = 30005;
                businessStrongholds.Add(bsa);
            }
            callback(playerStrongholds, businessStrongholds);

        }
    }
    #endregion


    #region 放置据点

    public void UploadSetplayerstronghold(int HoldId, string nickName, string locationName, double positionX, double positionY, System.Action<BusinessStrongholdAttribute> callback)
    {
        var _wForm = new WWWForm();
        _wForm.AddField("token", AndaDataManager.Instance.mainData.token);
        _wForm.AddField("HoldId", HoldId);
        _wForm.AddField("NickName", nickName);
        _wForm.AddField("LocationName", locationName);
        Debug.Log("positionX" + positionX);
        Debug.Log("positionY" + positionY);
        //经纬度 顺序  这里跟服务上的相反
        _wForm.AddField("PositionX", positionY.ToString());
        _wForm.AddField("PositionY", positionX.ToString());

        //--
        string path = networkAdress2 + "StrongHold/AddBusinessStrongHold";
        StartCoroutine(ExcuteUploadPlayerstornghold(path, _wForm, callback));
    }

    private IEnumerator ExcuteUploadPlayerstornghold(string _url, WWWForm _wForm, System.Action<BusinessStrongholdAttribute> callback)
    {
        AndaUIManager.Instance.OpenWaitBoard(true);
        Debug.Log("uploading");
        WWW postData = new WWW(_url, _wForm);
        yield return postData;
        AndaUIManager.Instance.OpenWaitBoard(false);
        Debug.Log("finish");
        if (postData.error != null)
        {
            Debug.Log(postData.error);
        }
        else
        {
            BusinessStrongHoldInfo data = JsonMapper.ToObject<BusinessStrongHoldInfo>(postData.text);
            BusinessStrongholdAttribute bss = ConvertTool.ConvertToBussinessStrongholdAttribute(data.BusinessStrongHold);
            AndaDataManager.Instance.mainData.AddBussinessStronghold(bss);
            callback(bss);
        }
    }

    #endregion

    #region 上传编辑的玩家/商家信息
    public void CallServerUploadUserInformation(byte[] imgByte, string nickname, string description,System.Action<bool>  callback)
    {
       
         var _wForm = new WWWForm();
        _wForm.AddField("token", AndaDataManager.Instance.mainData.token);
       // string josn =  ConvertTool.bytesToString(imgByte); // LitJson.JsonMapper.ToJson(imgByte);
       // Debug.Log("josn" + josn);
        _wForm.AddField("headImage", ConvertTool.bytesToString(imgByte));
        _wForm.AddField("name", nickname);
        _wForm.AddField("autograph", description);
        string path = networkAdress2 + "UserInfo/EditInfo";

        StartCoroutine(ExcuteUploadUserInformation(path, _wForm,callback));
    }

    private IEnumerator ExcuteUploadUserInformation(string _url, WWWForm _wForm, System.Action<bool> callback)
    {
        AndaUIManager.Instance.OpenWaitBoard(true);
        WWW postData = new WWW(_url, _wForm);
        yield return postData;
        Debug.Log("PostData" + postData.text);
        Result result = JsonMapper.ToObject<Result>(postData.text);
        AndaUIManager.Instance.OpenWaitBoard(false);
        if (postData.error != null)
        {
            Debug.Log(postData.error);
        }
        else
        {
            callback(result.code == "200");
        }
       
    }
    #endregion


    #region 上传编辑的奖励
    public void CallServerUploadReward(BussinessRewardStruct data, List<SonCoupon> sonCoupons, System.Action<BusinessCouponRequest> callback)
    {

        var _wForm = new WWWForm();
        _wForm.AddField("token", AndaDataManager.Instance.mainData.token);

        _wForm.AddField("code", "");
        if (data.image != null)
            _wForm.AddField("image", data.image);
        _wForm.AddField("title", data.title);
        _wForm.AddField("description", data.description);
        _wForm.AddField("status", data.status);
        _wForm.AddField("starttime", data.starttime);
        _wForm.AddField("endtime", data.endtime);
        _wForm.AddField("continueTime", data.continueTime);
        _wForm.AddField("porIsUpdate", data.porIsUpdate);
        _wForm.AddField("createTime", data.createTime);
        _wForm.AddField("DropRate", data.rewardDropRate);
        _wForm.AddField("DropCount", data.rewardDropCount);
        _wForm.AddField("totalCount", data.totalCount);
        _wForm.AddField("type", data.type);
        string json = "";
        List<SonCoupon> sonCouponList = sonCoupons;
        if (sonCoupons != null)
        {
            json = JsonMapper.ToJson(sonCouponList);
        }
        _wForm.AddField("SonCoupon", json);


        //string path = "http://localhost:57789/api/BusinessCoupon/Add";
        string path = networkAdress2 + "BusinessCoupon/Add";
        StartCoroutine(ExcuteCallServerUploadReward(path, _wForm, callback));
    }

    private IEnumerator ExcuteCallServerUploadReward(string _url, WWWForm _wForm, System.Action<BusinessCouponRequest> callback)
    {
        AndaUIManager.Instance.OpenWaitBoard(true);
        WWW postData = new WWW(_url, _wForm);
        yield return postData;
        #if UNITY_EDITOR
        Debug.Log("PostData" + postData.text);
        #endif
        AndaUIManager.Instance.OpenWaitBoard(false);
        if (postData.error != null)
        {
            Debug.Log(postData.error);
        }
        else
        {
            BusinessCouponRequest result = JsonMapper.ToObject<BusinessCouponRequest>(postData.text);
            AndaDataManager.Instance.mainData.AddBussinessRewar(result.data);
            callback(result);
        }

    }

    #region 向服务器重新编辑奖励券

    public void CallServerEditReward(BussinessRewardStruct data, List<SonCoupon> sonCoupons, System.Action<BusinessCouponRequest> callback)
    {

        var _wForm = new WWWForm();
        Debug.Log("dataCouponIndex:" +data.businesscouponIndex );
        _wForm.AddField("token", AndaDataManager.Instance.mainData.token);
        _wForm.AddField("couponIndex", data.businesscouponIndex);
        _wForm.AddField("code", "");
        if (data.image != null)
            _wForm.AddField("image", data.image);
        _wForm.AddField("title", data.title);
        _wForm.AddField("description", data.description);
        _wForm.AddField("status", data.status);
        _wForm.AddField("starttime", data.starttime);
        _wForm.AddField("endtime", data.endtime);
        _wForm.AddField("continueTime", data.continueTime);
        _wForm.AddField("porIsUpdate", data.porIsUpdate);
        _wForm.AddField("createTime", data.createTime);
        _wForm.AddField("DropRate", data.rewardDropRate);
        _wForm.AddField("DropCount", data.rewardDropCount);
        _wForm.AddField("totalCount", data.totalCount);
        _wForm.AddField("type", data.type);
        string json = "";
        List<SonCoupon> sonCouponList = sonCoupons;
        if (sonCoupons != null)
        {
            json = JsonMapper.ToJson(sonCouponList);
        }
        _wForm.AddField("SonCoupon", json);
        //string path = "http://localhost:57789/api/BusinessCoupon/Edit";
        string path = networkAdress2 + "BusinessCoupon/Edit";
        StartCoroutine(ExcuteCallServerEditReward(path, _wForm, callback));
    }

    private IEnumerator ExcuteCallServerEditReward(string _url, WWWForm _wForm, System.Action<BusinessCouponRequest> callback)
    {
        AndaUIManager.Instance.OpenWaitBoard(true);
        WWW postData = new WWW(_url, _wForm);
        yield return postData;
        Debug.Log("PostData" + postData.text);
        AndaUIManager.Instance.OpenWaitBoard(false);
        if (postData.error != null)
        {
            Debug.Log(postData.error);
        }
        else
        {
            BusinessCouponRequest result = JsonMapper.ToObject<BusinessCouponRequest>(postData.text);
            AndaDataManager.Instance.mainData.UpdateBussinessCouponState(result.data);
            callback(result);
        }

    }
    #endregion


    #region 向 服务起上传 票据

    public void CallServerUpperShelf(int index, System.Action<int> callback)
    {

        var _wForm = new WWWForm();
        _wForm.AddField("token", AndaDataManager.Instance.mainData.token);
        _wForm.AddField("couponIndex", index);
        //string path = "http://localhost:57789/api/BusinessCoupon/UpperShelf";
        string path = networkAdress2 + "BusinessCoupon/UpperShelf";
        StartCoroutine(ExcuteCallServerUpperShelf(index,path, _wForm, callback));
    }

    private IEnumerator ExcuteCallServerUpperShelf(int itemIndex,string _url, WWWForm _wForm, System.Action<int> callback)
    {
        AndaUIManager.Instance.OpenWaitBoard(true); 
        WWW postData = new WWW(_url, _wForm);
        yield return postData;
        #if UNITY_EDITOR
        Debug.Log("PostData" + postData.text);
        #endif
        AndaUIManager.Instance.OpenWaitBoard(false);
        if (postData.error != null)
        {
            AndaUIManager.Instance.PlayTipsForChoose("上架失败，请检查网络", 1, "好的", "", null, null);

        }
        else
        {
            BusinessCouponRequest result = JsonMapper.ToObject<BusinessCouponRequest>(postData.text);

            if (result.code == "200")
            {
                AndaDataManager.Instance.mainData.UpdateBussinessCouponStateToUploadSell(itemIndex);

                callback(itemIndex);
            }else
            {
                AndaUIManager.Instance.PlayTipsForChoose("上架失败，请检查网络", 1, "好的", "", null, null);

            }


        }
    }

    #endregion

    #region 作废票据
    public void CallServerCancel(int index, System.Action<int> callback)
    {

        var _wForm = new WWWForm();
        _wForm.AddField("token", AndaDataManager.Instance.mainData.token);
        _wForm.AddField("couponIndex", index);
        //string path = "http://localhost:57789/api/BusinessCoupon/Cancel";
        string path = networkAdress2 + "BusinessCoupon/Cancel";
        StartCoroutine(ExcuteCallServerCancel(index, path, _wForm, callback));
    }

    private IEnumerator ExcuteCallServerCancel(int couponIndex, string _url, WWWForm _wForm, System.Action<int> callback)
    {
        AndaUIManager.Instance.OpenWaitBoard(true);
        WWW postData = new WWW(_url, _wForm);
        yield return postData;
        Debug.Log("PostData" + postData.text);
        AndaUIManager.Instance.OpenWaitBoard(false);
        if (postData.error != null)
        {
            Debug.Log(postData.error);
            AndaUIManager.Instance.PlayTipsForChoose("作废失败，请检查网络", 1, "好的", "", null, null);

        }
        else
        {

            BusinessCouponRequest result = JsonMapper.ToObject<BusinessCouponRequest>(postData.text);
            if(result.code == "200")
            {
                AndaDataManager.Instance.mainData.UpdateBussinessCouponStateDownSell(couponIndex);
                callback(couponIndex);
            }
            else
            {
                AndaUIManager.Instance.PlayTipsForChoose("作废失败，请检查网络", 1 ,"好的" , "", null ,null);
            }
           
        }
    }
    #endregion
    #endregion

    #region 上传广告数据

    public void CallServerUploadAds(int bsIndex, AdsStruct adsStruct , System.Action<AdsStruct> action)
    {
        StartCoroutine(ExcuteCallServerUploadAds(bsIndex,adsStruct, action));
    }

    private IEnumerator ExcuteCallServerUploadAds (int bsIndex, AdsStruct adsStruct ,System.Action<AdsStruct> action)
    {

        yield return null;
        switch(adsStruct.type)
        {
            case "text":
                break;
            case "texture":
                // tmpData000 应该为服务回传的路径名字 , 并且数据库中保存这个路径名字
                string token = "000";//"" AndaDataManager.Instance.mainData.playerData.userIndex.ToString()+ bsIndex.ToString() + adsStruct.itemIndex.ToString();
                string key =  AndaDataManager.userAdsKey + token;
                //上传成功之后，本地也会保存一份图片信 key值就是 基础key + 回传的tmpData000
                Debug.Log("Content" + adsStruct.content);

                PlayerPrefs.SetString(key, adsStruct.content);
                adsStruct.content = key;//广告内容替换成图片的路径,然后回传

                Debug.Log("key" + key);
                break;
            case "video":
                break;
        }

        action(adsStruct);
    }

    #endregion

    #region 往商家据点里放入奖励
    public void CallServerInsertStrongholdReward(int strongholdIndex, int rewardIndex, System.Action<bool,int> callback)
    {
        var _wForm = new WWWForm();
        _wForm.AddField("token", AndaDataManager.Instance.mainData.token);
        _wForm.AddField("strongholdIndex", strongholdIndex);
        _wForm.AddField("couponIndex", rewardIndex);
        string path = networkAdress2 + "BusinessCoupon/StrongholdAddCoupon";
        StartCoroutine(ExcuteCallServerInsertStrongholdReward(path , _wForm, strongholdIndex,rewardIndex, callback));
    }

    private IEnumerator ExcuteCallServerInsertStrongholdReward(string _url, WWWForm _wForm,int bsIndex, int rewardIndex, System.Action<bool,int> callback)
    {
        AndaUIManager.Instance.OpenWaitBoard(true);
        WWW postData = new WWW(_url, _wForm);
        yield return postData;
        Debug.Log("PostData" + postData.text);

        AndaUIManager.Instance.OpenWaitBoard(false);
        if(postData.error!=null)
        {
            Debug.Log(postData.error);
        }
        else
        {
            BusinessCouponRequest result = JsonMapper.ToObject<BusinessCouponRequest>(postData.text);
            //-- 更新本地数据
            //据点里的奖励数据更新
            AndaDataManager.Instance.mainData.UpdateAddStrongholdReward(bsIndex, rewardIndex);
            Debug.Log("Here Run Times");
            callback(result.code == "200", rewardIndex);
        }
      

    }
    #endregion

    #region 商家据点移除奖励
    public void CallServerRemoveStrongholdReward(int strongholdIndex, int rewardIndex, System.Action<bool,int> callback)
    {
        var _wForm = new WWWForm();
        _wForm.AddField("token", AndaDataManager.Instance.mainData.token);
        _wForm.AddField("strongholdIndex", strongholdIndex);
        _wForm.AddField("couponIndex", rewardIndex);
        string path = networkAdress2 + "BusinessCoupon/StrongholdDelCoupon";
        StartCoroutine(ExcuteCallServerRemoveStrongholdReward(path, _wForm, rewardIndex,strongholdIndex, callback));
    }

    private IEnumerator ExcuteCallServerRemoveStrongholdReward(string _url, WWWForm _wForm,int rewardIndex, int shIndex, System.Action<bool,int> callback)
    {
        AndaUIManager.Instance.OpenWaitBoard(true);
        WWW postData = new WWW(_url, _wForm);
        yield return postData;
        Debug.Log("PostData" + postData.text);
        AndaUIManager.Instance.OpenWaitBoard(false);
        if (postData.error != null)
        {
            Debug.Log(postData.error);
        }
        else
        {
            BusinessCouponRequest result = JsonMapper.ToObject<BusinessCouponRequest>(postData.text);
            AndaDataManager.Instance.mainData.UpdateReduceStrongholdReward(shIndex, rewardIndex);
            callback(result.code == "200" , rewardIndex);
        }


    }
    #endregion

    #region 据点改名字

    public void CallServerUploadStrongholdNickname(int shIndex,string nickName,System.Action<bool> callback)
    {
        var _wForm = new WWWForm();
        _wForm.AddField("token", AndaDataManager.Instance.mainData.token);
        _wForm.AddField("index", shIndex);
        _wForm.AddField("name", nickName);
        string path = networkAdress2 + "StrongHold/EditName";
        StartCoroutine(ExcuteCallServerUploadStrongholdNick(path, _wForm, shIndex, nickName, callback));
    }

    private IEnumerator ExcuteCallServerUploadStrongholdNick(string _url, WWWForm _wForm, int shIndex,string nickName , System.Action<bool> callback)
    {
        AndaUIManager.Instance.OpenWaitBoard(true);
        WWW postData = new WWW(_url, _wForm);
        yield return postData;
      
        AndaUIManager.Instance.OpenWaitBoard(false);
        if (postData.error != null)
        {
            Debug.Log(postData.error);
        }
        else
        {
            Result result = JsonMapper.ToObject<Result>(postData.text);
            AndaDataManager.Instance.mainData.UpdateStrongholdNickName(shIndex, nickName);
            callback(result.code == "200");
        }
    }
    #endregion
    #region 向服务器所以广告的图片

    public void CallServerGetUserAdsImg(string _key, System.Action<Sprite> callback)
    {
        string key = _key;
        string path = networkAdress4 + _key;
        StartCoroutine(ExcuteCallServerGetUserAdsImg(key,path, callback));
    }

    private IEnumerator ExcuteCallServerGetUserAdsImg(string key, string path, System.Action<Sprite> callback)
    {
        AndaUIManager.Instance.OpenWaitBoard(true);
        WWW wWW = new WWW(path);
        yield return wWW;

        AndaUIManager.Instance.OpenWaitBoard(false);
        if (string.IsNullOrEmpty(wWW.error))
        {
            Texture2D texture2D = ConvertTool.ConvertToTexture2d(wWW.texture);
            Sprite sprite = ConvertTool.ConvertToSpriteWithTexture2d(texture2D);
            byte[] b = texture2D.EncodeToPNG();
            string t = ConvertTool.bytesToString(b);
            PlayerPrefs.SetString(key, t);
            callback(sprite);
        }
        else
        {
            AndaUIManager.Instance.PlayTips(wWW.error);
        }

    }

    #endregion
    #region 向服务器要头像数据
    public void CallServerGetImagePor(string address ,System.Action<Sprite> callback)
    {
        string st = networkAdress4 + address;
        StartCoroutine(ExcuteCallServerGetImagePor(address,st, callback));
    }

    private IEnumerator ExcuteCallServerGetImagePor(string porAdrress,string adress,System.Action<Sprite> callback)
    {
        AndaUIManager.Instance.OpenWaitBoard(true);
        WWW wWW = new WWW(adress);
        yield return wWW;
        AndaUIManager.Instance.OpenWaitBoard(false);
        if(string.IsNullOrEmpty(wWW.error))
        {
            //byte[] b = wWW.texture.EncodeToPNG();
         
            Texture2D texture2D = ConvertTool.ConvertToTexture2d(wWW.texture);
            Sprite sprite = ConvertTool.ConvertToSpriteWithTexture2d(texture2D);
            byte[] b = texture2D.EncodeToPNG();
            string t = ConvertTool.bytesToString(b);
            PlayerPrefs.SetString("SH_" + porAdrress, t);
           
            callback(sprite);
        }else
        {
            AndaUIManager.Instance.PlayTips(wWW.error);
        }

    }


    #endregion

    #region 向服务器索要奖励图片数据
    public void CallServerGetRewardImagePor(string address, System.Action<Sprite> callback)
    {
        string st = networkAdress4 + address;
        StartCoroutine(ExcuteCallServerGetRewardImagePor(address,st, callback));
    }

    private IEnumerator ExcuteCallServerGetRewardImagePor(string proPaht, string adress, System.Action<Sprite> callback)
    {
        AndaUIManager.Instance.OpenWaitBoard(true);
        WWW wWW = new WWW(adress);
        yield return wWW;
        AndaUIManager.Instance.OpenWaitBoard(false);
        if (string.IsNullOrEmpty(wWW.error))
        {
            byte[] b = wWW.texture.EncodeToPNG();
            string t = ConvertTool.bytesToString(b);
            PlayerPrefs.SetString("RW_"+ proPaht, t);
            Texture2D texture2D = ConvertTool.ConvertToTexture2d(wWW.texture);
            Sprite sprite = ConvertTool.ConvertToSpriteWithTexture2d(texture2D);
            callback(sprite);
        }
        else
        {
            AndaUIManager.Instance.PlayTips(wWW.error);
        }

    }


    #endregion


    #region 玩家优惠卷审核
    public void GetPlayerCoupon(System.Action<PlayerCouponsRequest> callback)
    {
        var _wForm = new WWWForm();
        _wForm.AddField("token", AndaDataManager.Instance.mainData.token);
        string path = networkAdress2 + "PlayerCoupon/BusinessGetPlayerCoupons";
        //string path = "http://localhost:57789/api/PlayerCoupon/BusinessGetPlayerCoupons";
        StartCoroutine(ExcuteGetPlayerCoupon(path, _wForm, callback));
    }

    private IEnumerator ExcuteGetPlayerCoupon(string _url, WWWForm _wForm, System.Action<PlayerCouponsRequest> callback)
    {
        AndaUIManager.Instance.OpenWaitBoard(true);
        WWW postData = new WWW(_url, _wForm);
        yield return postData;

        AndaUIManager.Instance.OpenWaitBoard(false);
        if (postData.error != null)
        {
            Debug.Log(postData.error);
        }
        else
        {
            PlayerCouponsRequest result = JsonMapper.ToObject<PlayerCouponsRequest>(postData.text);
            Debug.Log(postData.text);
            callback(result);
        }
    }

    public void CheackCoupon(int applyIndex, System.Action<PlayerCouponRequest> callback)
    {
        var _wForm = new WWWForm();
        _wForm.AddField("token", AndaDataManager.Instance.mainData.token);
        _wForm.AddField("applyIndex", applyIndex);
        string path = networkAdress2 + "PlayerCoupon/Cheack";
        //string path = "http://localhost:57789/api/PlayerCoupon/Cheack";
        StartCoroutine(ExcuteCheackCoupon(path, _wForm, callback));
    }
    private IEnumerator ExcuteCheackCoupon(string _url, WWWForm _wForm, System.Action<PlayerCouponRequest> callback)
    {
        AndaUIManager.Instance.OpenWaitBoard(true);
        WWW postData = new WWW(_url, _wForm);
        yield return postData;

        AndaUIManager.Instance.OpenWaitBoard(false);
        if (postData.error != null)
        {
            Debug.Log(postData.error);
        }
        else
        {
            PlayerCouponRequest result = JsonMapper.ToObject<PlayerCouponRequest>(postData.text);
            Debug.Log(postData.text);
            callback(result);
        }
    }

    public void QRCheackCoupon(string QR, System.Action<PlayerCouponRequest> callback)
    {
        var _wForm = new WWWForm();
        if (AndaDataManager.Instance.mainData == null) { callback(null); return; }
        _wForm.AddField("token", AndaDataManager.Instance.mainData.token);
        _wForm.AddField("QR", QR);

        string path = networkAdress2 + "PlayerCoupon/QRCheackByPost";
        //string path = "http://localhost:57789/api/PlayerCoupon/QRCheackByPost";
        StartCoroutine(ExcuteQRCheackCoupon(path, _wForm, callback));
    }
    private IEnumerator ExcuteQRCheackCoupon(string _url, WWWForm _wForm, System.Action<PlayerCouponRequest> callback)
    {
        AndaUIManager.Instance.OpenWaitBoard(true);
        WWW postData = new WWW(_url, _wForm);
        yield return postData;

        AndaUIManager.Instance.OpenWaitBoard(false);
        if (postData.error != null)
        {
            Debug.Log(postData.error);
        }
        else
        {
            PlayerCouponRequest result = JsonMapper.ToObject<PlayerCouponRequest>(postData.text);
            Debug.Log(postData.text);
            callback(result);
        }
    }

    public void FailCoupon(int applyIndex, System.Action<PlayerCouponRequest> callback)
    {
        var _wForm = new WWWForm();
        _wForm.AddField("token", AndaDataManager.Instance.mainData.token);
        _wForm.AddField("applyIndex", applyIndex);
        string path = networkAdress2 + "PlayerCoupon/Fail";
        StartCoroutine(ExcuteFailCoupon(path, _wForm, callback));
    }

    private IEnumerator ExcuteFailCoupon(string _url, WWWForm _wForm, System.Action<PlayerCouponRequest> callback)
    {
        AndaUIManager.Instance.OpenWaitBoard(true);
        WWW postData = new WWW(_url, _wForm);
        yield return postData;

        AndaUIManager.Instance.OpenWaitBoard(false);
        if (postData.error != null)
        {
            Debug.Log(postData.error);
        }
        else
        {
            PlayerCouponRequest result = JsonMapper.ToObject<PlayerCouponRequest>(postData.text);
            Debug.Log(postData.text);
            callback(result);
        }
    }
    #endregion

    #region 服务器消息获取
    public void GetServerMessage(System.Action<ServerMessageRequest> callback)
    {
        var _wForm = new WWWForm();
        if (AndaDataManager.Instance.mainData == null)
            return;
        _wForm.AddField("token", AndaDataManager.Instance.mainData.token);
        string path = networkAdress2 + "ServerMessage/Get";
        //string path = "http://localhost:57789/api/ServerMessage/Get";
        StartCoroutine(ExcuteGetServerMessage(path, _wForm, callback));
    }

    private IEnumerator ExcuteGetServerMessage(string _url, WWWForm _wForm, System.Action<ServerMessageRequest> callback)
    {
        WWW postData = new WWW(_url, _wForm);
        yield return postData;

        if (postData.error != null)
        {
            Debug.Log(postData.error);
        }
        else
        {
            ServerMessageRequest result = JsonMapper.ToObject<ServerMessageRequest>(postData.text);
            Debug.Log(postData.text);
            callback(result);
        }
    }
    #endregion

    #region 购买验证

    public void VerifyAppleBuy(string appleReceipt)
    {
        var _wForm = new WWWForm();
        if (AndaDataManager.Instance.mainData == null)
            return;
        _wForm.AddField("token", AndaDataManager.Instance.mainData.token);
        _wForm.AddField("appleReceipt", appleReceipt);
        string path = networkAdress2 + "Mall/IosBuy";
        //string path = "http://localhost:57789/api/ServerMessage/Get";
        StartCoroutine(ExcuteVerifyAppleBuy(path, _wForm));
    }

    private IEnumerator ExcuteVerifyAppleBuy(string _url, WWWForm _wForm)
    {
        AndaUIManager.Instance.OpenWaitBoard(true);
        WWW postData = new WWW(_url, _wForm);
        yield return postData;
        AndaUIManager.Instance.OpenWaitBoard(false);
        if (postData.error != null)
        {
            Debug.Log(postData.error);
        }
        else
        {
            Result result = JsonMapper.ToObject< Result >(postData.text);
            Debug.Log("AndaSaid: "  + result.detail );
        }
    }

    #endregion
}
