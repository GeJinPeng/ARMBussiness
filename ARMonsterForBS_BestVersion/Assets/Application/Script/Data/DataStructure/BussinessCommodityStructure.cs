using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BussinessMallStructure
{
    public List<MallCommodityStructure> data {get;set;}
}

public class MallCommodityStructure
{ 
    /// <summary>
    /// 商品序号
    /// </summary>
    /// <value>The index of the commodity.</value>
    public int commodityIndex {get;set;}
    /// <summary>
    /// 对应的物件ID
    /// </summary>
    /// <value>The commodity identifier.</value>
    public string commodityID {get;set;}
    /// <summary>
    /// 像有些 套餐 或者 福袋，他们由多个物品组成，名字就需要另外取，。这个值为空的情况下，一定是单品，否则会报错
    /// </summary>
    /// <value>The name of the commodity.</value>
    public string commodityName{get;set;}
    /// <summary>
    /// 这个商品提供的物件数量
    /// </summary>
    /// <value>The count.</value>
    public int count {get;set;}
    /// <summary>
    /// 到期时间，，永久的就填0
    /// </summary>
    /// <value>The end time.</value>
    public long endTime{get;set;}
    /// <summary>
    /// 商品描述
    /// </summary>
    /// <value>The description.</value>
    public string description{get;set;}
    /// <summary>
    /// 当前商品的状态 0= 上架  1= 下架
    /// </summary>
    /// <value>The state.</value>
    public int state {get;set;}
    /// <summary>
    /// 支付货币种类,不支持改币种填 0,支持填 1 【星币 ，星石，赤金 ，真实货币 】 【0，0，0，0】
    /// </summary>
    /// <value>The type of the price.</value>
    public List<int> priceType {get;set;}
    /// <summary>
    /// 价格 相对应序号底下填 价格 【0，0，0，1】 = 》 【0，0，0，1 （RMB） 】
    /// </summary>
    /// <value>The price.</value>
    public List<int> price {get;set;}
    /// <summary>
    /// 购买这个物件 附带的其他的物件，可以无限扩
    /// </summary>
    /// <value>The child.</value>
    public List<MallCommodityStructure> child {get;set;}
}
public class PayWay
{
    public int payType { get; set; }
    public int payPrice { get; set; }
}


public class MallCommodityTypeStructure
{
    public string type { get; set; }
    public string typeName { get; set; }
    /// <summary>
    /// 商店里物件的游标
    /// </summary>
    /// <value>The index of the item.</value>
    public List<int> itemIndex { get; set; }
}

//注意 物品和商品的区别。

public class Commoditiestructure
{
    public List<CommodityStructure> data {get;set;}
}



public class CommodityStructure
{
    public string commodityID { get; set; }

    public string commodityName {get;set;}

    public string description { get; set; }
}

public class CommodityTypeStructure
{
    public string typte{get;set;}
    public string cnName{get;set;}
    public List<string> idList{get;set;}
}


public class BussinessSHRootConfig
{
    /// <summary>
    /// 对应的物件ID
    /// </summary>
    /// <value>The identifier.</value>
    public string id {get;set;}

    /// <summary>
    /// 据点的等级
    /// </summary>
    /// <value>The level.</value>
    public int level {get;set;}

    /// <summary>
    /// 拥有的怪广为 数量
    /// </summary>
    /// <value>The ads count.</value>
    public int adsCount{get;set;}
    /// <summary>
    /// 广告的的等级 0 = 文字， 1  = 图像 2 = 视频
    /// </summary>
    /// <value>The ads level.</value>
    public int adsLevel{get;set;}

    /// <summary>
    /// 可放怪兽的最大数量，暂时都是1个
    /// </summary>
    /// <value>The monster count.</value>
    public int monsterCount {get;set; }
    /// <summary>
    /// 什么级别的怪兽可以放在这个据点上 ， monsterLevel = 0 小于等于0级的怪兽可放， monsterLEVel =1 ，代表1级及一下的怪兽可以放
    /// </summary>
    /// <value>The monster level.</value>
    public int monsterLevel{get;set;}
    /// <summary>
    /// 暂时无用
    /// </summary>
    /// <value>The coupon level.</value>
    public int couponLevel{get;set;}

    public int hotValue{get;set;}
}

public class MonsterLevelBox
{
    public int level {get;set;}
    public string title { get; set; }
    public List<string> data {get;set;}
}

public class AdsLevelBox
{
    public int level{get;set;}

    public string key {get;set;}

    public string title {get;set;}

    public List<string> root {get;set;}

    
}

public class AdsStruct
{
    /// <summary>
    /// 第几条广告
    /// </summary>
    /// <value>The index of the item.</value>
    public int itemIndex{get;set;}
    /// <summary>
    /// 是什么类型的广告
    /// </summary>
    /// <value>The type.</value>
    public string type{get;set;}
    /// <summary>
    /// 广告内容是啥
    /// </summary>
    /// <value>The content.</value>
    public string content{get;set;}
}


