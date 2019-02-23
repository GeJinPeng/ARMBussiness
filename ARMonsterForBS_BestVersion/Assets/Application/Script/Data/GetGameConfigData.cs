using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using LitJson;
public static class GetGameConfigData  {

	
    private static BussinessMallStructure _bssinessMall = null;
    public static BussinessMallStructure BussineMallData
    {
        get
        {
            if(_bssinessMall == null)
            {
                _bssinessMall = new BussinessMallStructure
                {
                    data = new List<MallCommodityStructure>
                    {
                        //萌新
                        new MallCommodityStructure
                        {
                            commodityIndex = 0,
                            commodityID = "sh_bs_00",
                            count = 1,
                            endTime = 0,
                            description = "获得1张萌新据点建造图纸，可以立即使用建造一个萌新据点",
                            state = 0,
                            priceType = new List<int>{0,0,0,1},
                            price = new List<int>{0,0,0,1},
                        },
                        new MallCommodityStructure
                        {
                            commodityIndex = 1,
                            commodityID = "sh_bs_00",
                            count = 3,
                            endTime = 0,
                            description = "获得3张萌新据点建造图纸，可以立即使用建造一个萌新据点",
                            state = 0,
                            priceType = new List<int>{0,0,0,1},
                            price = new List<int>{0,0,0,3},
                        },
                        new MallCommodityStructure
                        {
                            commodityIndex = 2,
                            commodityID = "sh_bs_00",
                            count = 14,
                            endTime = 0,
                            description = "获得14张萌新据点建造图纸，可以立即使用建造一个萌新据点",
                            state = 0,
                            priceType = new List<int>{0,0,0,1},
                            price = new List<int>{0,0,0,12},
                        },
                        new MallCommodityStructure
                        {
                            commodityIndex = 3,
                            commodityID = "sh_bs_00",
                            count = 20,
                            endTime = 0,
                            description = "获得20张萌新据点建造图纸，可以立即使用建造一个萌新据点",
                            state = 0,
                            priceType = new List<int>{0,0,0,1},
                            price = new List<int>{0,0,0,18},
                        },
                        //初级
                        new MallCommodityStructure
                        {
                            commodityIndex = 4,
                            commodityID = "sh_bs_01",
                            count = 4,
                            endTime = 0,
                            description = "获得4张初级据点建造图纸，可以立即使用建造一个初级据点",
                            state = 0,
                            priceType = new List<int>{0,0,0,1},
                            price = new List<int>{0,0,0,6},
                        },

                        new MallCommodityStructure
                        {
                            commodityIndex = 5,
                            commodityID = "sh_bs_01",
                            count = 14,
                            endTime = 0,
                            description = "获得14张初级据点建造图纸，可以立即使用建造一个初级据点",
                            state = 0,
                            priceType = new List<int>{0,0,0,1},
                            price = new List<int>{0,0,0,12},
                        },
                        //中级
                        new MallCommodityStructure
                        {
                            commodityIndex = 6,
                            commodityID = "sh_bs_02",
                            count = 10,
                            endTime = 0,
                            description = "获得10张中级据点建造图纸，可以立即使用建造一个中级据点",
                            state = 0,
                            priceType = new List<int>{0,0,0,1},
                            price = new List<int>{0,0,0,12},
                        },

                        new MallCommodityStructure
                        {
                            commodityIndex = 7,
                            commodityID = "sh_bs_02",
                            count = 14,
                            endTime = 0,
                            description = "获得14张中级据点建造图纸，可以立即使用建造一个中级据点",
                            state = 0,
                            priceType = new List<int>{0,0,0,1},
                            price = new List<int>{0,0,0,18},
                        },
                        //高级
                        new MallCommodityStructure
                        {
                            commodityIndex = 8,
                            commodityID = "sh_bs_03",
                            count = 11,
                            endTime = 0,
                            description = "获得11张高级据点建造图纸，可以立即使用建造一个高级据点",
                            state = 0,
                            priceType = new List<int>{0,0,0,1},
                            price = new List<int>{0,0,0,25},
                        },
                        //璀璨
                         new MallCommodityStructure
                        {
                            commodityIndex = 9,
                            commodityID = "sh_bs_04",
                            count = 1,
                            endTime = 0,
                            description = "获得1张璀璨据点建造图纸，可以立即使用建造一个璀璨据点",
                            state = 0,
                            priceType = new List<int>{0,0,0,1},
                            price = new List<int>{0,0,0,30},
                        },


                         new MallCommodityStructure
                        {
                            commodityIndex = 10,
                            commodityID = "sh_bs_04",
                            count = 3,
                            endTime = 0,
                            description = "获得3张璀璨据点建造图纸，可以立即使用建造一个璀璨据点",
                            state = 0,
                            priceType = new List<int>{0,0,0,1},
                            price = new List<int>{0,0,0,88},
                        },

                         new MallCommodityStructure
                        {
                            commodityIndex = 11,
                            commodityID = "rc_bs_00",
                            count = 12,
                            endTime = 0,
                            description = "获得12张初级奖励编辑券，编辑简单的奖励",
                            state = 0,
                            priceType = new List<int>{0,0,0,1},
                            price = new List<int>{0,0,0,6},
                        },
                         new MallCommodityStructure
                        {
                            commodityIndex = 12,
                            commodityID = "rc_bs_01",
                            count = 1,
                            endTime = 0,
                            description = "获得1张高级奖励编辑券，编辑复合奖励券",
                            state = 0,
                            priceType = new List<int>{0,0,0,1},
                            price = new List<int>{0,0,0,1},
                        },
                        new MallCommodityStructure
                        {
                            commodityIndex = 13,
                            commodityID = "rc_bs_01",
                            count = 7,
                            endTime = 0,
                            description = "获得7张高级奖励编辑券，编辑复合奖励券",
                            state = 0,
                            priceType = new List<int>{0,0,0,1},
                            price = new List<int>{0,0,0,6},
                        },

                         new MallCommodityStructure
                        {
                            commodityIndex = 14,
                            commodityID = "ms_bs_1004",
                            count = 1,
                            endTime = 0,
                            description = "获得一张初级 毕宿 星宿卡，适用于所有据点",
                            state = 0,
                            priceType = new List<int>{1,0,0,1},
                            price = new List<int>{18000,0,0,3},
                        },
                         new MallCommodityStructure
                        {
                            commodityIndex = 15,
                            commodityID = "ms_bs_1005",
                            count = 1,
                            endTime = 0,
                            description = "获得一张初级 柳宿 星宿卡，适用于所有据点",
                            state = 0,
                            priceType = new List<int>{1,0,0,1},
                            price = new List<int>{18000,0,0,3},
                        },
                         new MallCommodityStructure
                        {
                            commodityIndex = 16,
                            commodityID = "ms_bs_1007",
                            count = 1,
                            endTime = 0,
                            description = "获得一张初级 角宿 星宿卡，适用于所有据点",
                            state = 0,
                            priceType = new List<int>{1,0,0,1},
                            price = new List<int>{18000,0,0,3},
                        },
                         
                        new MallCommodityStructure
                        {
                            commodityIndex = 17,
                            commodityID = "ms_bs_1010",
                            count = 1,
                            endTime = 0,
                            description = "获得一张初级 亢宿 星宿卡，适用于所有据点",
                            state = 0,
                            priceType = new List<int>{1,0,0,1},
                            price = new List<int>{18000,0,0,3},
                        },

                        new MallCommodityStructure
                        {
                            commodityIndex = 18,
                            commodityID = "ms_bs_1006",
                            count = 1,
                            endTime = 0,
                            description = "获得一张高级 氐宿 星宿卡，适用于中级及以上所有据点",
                            state = 0,
                            priceType = new List<int>{0,0,0,1},
                            price = new List<int>{0,0,0,6},
                        },

                         new MallCommodityStructure
                        {
                            commodityIndex = 19,
                            commodityID = "ms_bs_1011",
                            count = 1,
                            endTime = 0,
                            description = "获得一张高级 尾宿 星宿卡，适用于中级及以上所有据点",
                            state = 0,
                            priceType = new List<int>{1,0,0,1},
                            price = new List<int>{36000,0,0,6},
                        },

                         new MallCommodityStructure
                        {
                            commodityIndex = 20,
                            commodityID = "ms_bs_2001",
                            count = 1,
                            endTime = 0,
                            description = "获得一张高级 烛光 E星卡，适用于中级及以上所有据点",
                            state = 0,
                            priceType = new List<int>{0,0,0,1},
                            price = new List<int>{0,0,0,6},
                        },


                        new MallCommodityStructure
                        {
                            commodityIndex = 21,
                            commodityID = "sh_bs_00",
                            commodityName = "萌新小福袋1号",
                            count = 6,
                            endTime = 0,
                            description = "萌新建造图纸福袋，6张萌新建造图纸，还有丰富的赠品哦！点击查看",
                            state = 0,
                            priceType = new List<int>{0,0,0,1},
                            price = new List<int>{0,0,0,6},
                            child = new List<MallCommodityStructure>
                            {
                                new MallCommodityStructure
                                {
                                    commodityID = "ms_bs_sj00",
                                    count = 3,
                                },
                                new MallCommodityStructure
                                {
                                    commodityID = "rc_bs_00",
                                    count = 3,
                                }
                            }
                        },

                        new MallCommodityStructure
                        {
                            commodityIndex = 22,
                            commodityID = "sh_bs_00",
                            commodityName = "萌新小福袋2号",
                            count = 12,
                            endTime = 0,
                            description = "萌新建造图纸福袋，12张萌新建造图纸，还有更加丰富的赠品哦！点击查看",
                            state = 0,
                            priceType = new List<int>{0,0,0,1},
                            price = new List<int>{0,0,0,12},
                            child = new List<MallCommodityStructure>
                            {
                                new MallCommodityStructure
                                {
                                    commodityID = "ms_bs_sj00",
                                    count = 2,
                                },
                                 new MallCommodityStructure
                                {
                                    commodityID = "ms_bs_sj01",
                                    count = 1,
                                },
                                new MallCommodityStructure
                                {
                                    commodityID = "rc_bs_00",
                                    count = 2,
                                },
                                new MallCommodityStructure
                                {
                                    commodityID = "rc_bs_01",
                                    count = 1,
                                }
                            }
                        },

                        new MallCommodityStructure
                        {
                            commodityIndex = 23,
                            commodityID = "sh_bs_00",
                            commodityName = "萌新小福袋3号",
                            count = 12,
                            endTime = 0,
                            description = "萌新建造图纸福袋，20 张萌新建造图纸，还有更加更加更加丰富的赠品哦！点击查看",
                            state = 0,
                            priceType = new List<int>{0,0,0,1},
                            price = new List<int>{0,0,0,18},
                            child = new List<MallCommodityStructure>
                            {
                                 new MallCommodityStructure
                                {
                                    commodityID = "ms_bs_sj01",
                                    count = 5,
                                },
                            }
                        },

                        new MallCommodityStructure
                        {
                            commodityIndex = 24,
                            commodityID = "sh_bs_01",
                            commodityName = "初级小福袋",
                            count = 4,
                            endTime = 0,
                            description = "获得 4 张初级建造图纸，还有更加丰富的赠品哦！点击查看",
                            state = 0,
                            priceType = new List<int>{0,0,0,1},
                            price = new List<int>{0,0,0,6},
                            child = new List<MallCommodityStructure>
                            {
                                new MallCommodityStructure
                                {
                                    commodityID = "ms_bs_sj01",
                                    count = 1,
                                },
                                new MallCommodityStructure
                                {
                                    commodityID = "ms_bs_sj00",
                                    count = 2,
                                },
                                 new MallCommodityStructure
                                {
                                    commodityID = "rc_bs_01",
                                    count = 3,
                                },
                            }
                        },

                        new MallCommodityStructure
                        {
                            commodityIndex = 25,
                            commodityID = "sh_bs_02",
                            commodityName = "中级小福袋",
                            count = 6,
                            endTime = 0,
                            description = "获得 6 张中级建造图纸，还有更加更加丰富的赠品哦！点击查看",
                            state = 0,
                            priceType = new List<int>{0,0,0,1},
                            price = new List<int>{0,0,0,12},
                            child = new List<MallCommodityStructure>
                            {
                                new MallCommodityStructure
                                {
                                    commodityID = "ms_bs_sj01",
                                    count = 2,
                                },
                                new MallCommodityStructure
                                {
                                    commodityID = "ms_bs_sj00",
                                    count = 1,
                                },
                                new MallCommodityStructure
                                {
                                    commodityID = "rc_bs_01",
                                    count = 3,
                                },
                            }
                        },

                        new MallCommodityStructure
                        {
                            commodityIndex = 26,
                            commodityID = "sh_bs_03",
                            commodityName = "高级福袋1号",
                            count = 6,
                            endTime = 0,
                            description = "获得 6 张高级建造图纸，还有更加更加丰富的赠品哦！点击查看",
                            state = 0,
                            priceType = new List<int>{0,0,0,1},
                            price = new List<int>{0,0,0,18},
                            child = new List<MallCommodityStructure>
                            {
                                new MallCommodityStructure
                                {
                                    commodityID = "ms_bs_sj01",
                                    count = 3,
                                },
                                new MallCommodityStructure
                                {
                                    commodityID = "rc_bs_01",
                                    count = 3,
                                },
                            }
                        },

                        new MallCommodityStructure
                        {
                            commodityIndex = 27,
                            commodityID = "sh_bs_03",
                            commodityName = "高级福袋2号",
                            count = 12,
                            endTime = 0,
                            description = "获得 12 张高级建造图纸，还有更加更加丰富的赠品哦！点击查看",
                            state = 0,
                            priceType = new List<int>{0,0,0,1},
                            price = new List<int>{0,0,0,30},
                            child = new List<MallCommodityStructure>
                            {
                                new MallCommodityStructure
                                {
                                    commodityID = "ms_bs_sj01",
                                    count = 1,
                                },
                                new MallCommodityStructure
                                {
                                    commodityID = "rc_bs_01",
                                    count = 3,
                                },
                                 new MallCommodityStructure
                                {
                                    commodityID = "sh_bs_02",
                                    count = 2,
                                },
                            }
                        },

                        new MallCommodityStructure
                        {
                            commodityIndex = 28,
                            commodityID = "sh_bs_04",
                            commodityName = "璀璨大福袋",
                            count = 5,
                            endTime = 0,
                            description = "获得 5 张璀璨建造图纸，还有更加更加丰富的赠品哦！点击查看",
                            state = 0,
                            priceType = new List<int>{0,0,0,1},
                            price = new List<int>{0,0,0,108},
                            child = new List<MallCommodityStructure>
                            {
                                new MallCommodityStructure
                                {
                                    commodityID = "ms_bs_sj01",
                                    count = 2,
                                },
                                new MallCommodityStructure
                                {
                                    commodityID = "rc_bs_01",
                                    count = 2,
                                },
                                new MallCommodityStructure
                                {
                                    commodityID = "sh_bs_03",
                                    count = 1,
                                },
                            }
                        },


                         new MallCommodityStructure
                        {
                            commodityIndex = 29,
                            commodityID = "sh_bs_lvup_all_03",
                            commodityName = "财升万座_高级",
                            count = 1,
                            endTime = 0,
                            description = "获得1张 高级据点提升卡 ,可以将名下所有的据点提升至高级 ，还有更加更加丰富的赠品哦！点击查看",
                            state = 0,
                            priceType = new List<int>{0,0,0,1},
                            price = new List<int>{0,0,0,698},
                            child = new List<MallCommodityStructure>
                            {
                                new MallCommodityStructure
                                {
                                    commodityID = "dz_bs_prop00",
                                    count = 1,
                                },
                            }
                        },

                         new MallCommodityStructure
                        {
                            commodityIndex = 30,
                            commodityID = "sh_bs_lvup_all_04",
                            commodityName = "财升万座_璀璨",
                            count = 1,
                            endTime = 0,
                            description = "获得1张 璀璨据点提升卡 ,可以将名下所有的据点提升至璀璨 ，还有更加更加丰富的赠品哦！点击查看",
                            state = 0,
                            priceType = new List<int>{0,0,0,1},
                            price = new List<int>{0,0,0,3998},
                            child = new List<MallCommodityStructure>
                            {
                                new MallCommodityStructure
                                {
                                    commodityID = "dz_bs_prop01",
                                    count = 1,
                                },
                            }
                        },

                         new MallCommodityStructure
                        {
                            commodityIndex = 31,
                            commodityID = "sh_bs_lvup_all_04",
                            commodityName = "财升万座_璀璨VIP",
                            count = 1,
                            endTime = 0,
                            description = "获得1张 璀璨据点提升卡 ,可以将名下所有的据点提升至璀璨 ，还有更加更加丰富的赠品哦！点击查看",
                            state = 0,
                            priceType = new List<int>{0,0,0,1},
                            price = new List<int>{0,0,0,6498},
                            child = new List<MallCommodityStructure>
                            {
                                new MallCommodityStructure
                                {
                                    commodityID = "dz_bs_prop02",
                                    count = 1,
                                },
                                new MallCommodityStructure
                                {
                                    commodityID = "dz_bs_monster00",
                                    count = 1,
                                },
                            }
                        },

                    }
                };
                string mallJson = JsonMapper.ToJson(_bssinessMall);
                Debug.Log("AndaSaid:商城的商品配置文件表(会更新）Json=>>>>>>" + mallJson);

            }

           


            return _bssinessMall;
        }
    }



    #region 商品数据

    private static Commoditiestructure _commoditise =null;

    public static Commoditiestructure Commodities
    {
        get
        {
            if(_commoditise == null)
            {
                _commoditise = new Commoditiestructure
                {
                    data = new List<CommodityStructure>
                    {
                        new CommodityStructure
                        {
                            commodityID = "sh_bs_00",
                            commodityName = "萌新建造图纸",
                            description = "建造一个基础热度为10的萌新商家据点，提供1个数量的初级怪兽卡槽，1个文本格式的广告位。"
                        },
                        new CommodityStructure
                        {
                            commodityID = "sh_bs_01",
                            commodityName = "初级建造图纸",
                            description = "建造一个基础热度为15的初级商家据点，提供1个数量的初级怪兽卡槽，1个静态图像格式的广告位。"
                        },
                        new CommodityStructure
                        {
                            commodityID = "sh_bs_02",
                            commodityName = "中级建造图纸",
                            description = "建造一个基础热度为20的中级商家据点，提供2个数量的高级怪兽卡槽，1个动态视频的广告位。"
                        },
                        new CommodityStructure
                        {
                            commodityID = "sh_bs_03",
                            commodityName = "高级建造图纸",
                            description = "建造一个基础热度为30的高级商家据点，提供2个数量的高级怪兽卡槽，2个动态视频的广告位。"
                        },
                        new CommodityStructure
                        {
                            commodityID = "sh_bs_04",
                            commodityName = "璀璨建造图纸",
                            description = "建造一个基础热度为80的璀璨商家据点，提供2个数量的高级怪兽卡槽，3个动态视频的广告位。"
                        },

                        new CommodityStructure
                        {
                            commodityID = "sh_bs_lvup_all_01",
                            commodityName = "初级据点提升卡",
                            description = "将名下所有的据点等级提升为初级",
                        },
                        new CommodityStructure
                        {
                            commodityID = "sh_bs_lvup_all_02",
                            commodityName = "中级级据点提升卡",
                            description = "将名下所有的据点等级提升为中级",
                        },
                        new CommodityStructure
                        {
                            commodityID = "sh_bs_lvup_all_03",
                            commodityName = "高级据点提升卡",
                            description = "将名下所有的据点等级提升为高级",
                        },
                        new CommodityStructure
                        {
                            commodityID = "sh_bs_lvup_all_04",
                            commodityName = "璀璨据点提升卡",
                            description = "将名下所有的据点等级提升为璀璨",
                        },
                     
                        new CommodityStructure
                        {
                            commodityID = "rc_bs_00",
                            commodityName = "初级奖励编辑券",
                            description = "可以编辑简单的奖励券，但不能合成",
                        },
                        new CommodityStructure
                        {
                            commodityID = "rc_bs_01",
                            commodityName = "高级奖励编辑券",
                            description = "可以编辑复合奖励券，可以设置为稀有奖励券",
                        },

                        new CommodityStructure
                        {
                            commodityID = "ms_bs_sj00",
                            commodityName = "初级随机卡",
                            description = "使用后会随机获得一站初级星宿卡哦",
                        },

                        new CommodityStructure
                        {
                            commodityID = "ms_bs_sj01",
                            commodityName = "高级随机卡",
                            description = "使用后会随机获得一站高级星宿卡哦度",
                        },

                        new CommodityStructure
                        {
                            commodityID = "ms_bs_1004",
                            commodityName = "初级毕宿卡",
                            description = "适用于所有等级的据点，并且提供30点热度",
                        },
                        new CommodityStructure
                        {
                            commodityID = "ms_bs_1005",
                            commodityName = "初级柳宿卡",
                            description = "适用于所有等级的据点，并且提供30点热度",
                        },
                        new CommodityStructure
                        {
                            commodityID = "ms_bs_1007",
                            commodityName = "初级角宿卡",
                            description = "适用于所有等级的据点，并且提供30点热度",
                        },
                        new CommodityStructure
                        {
                            commodityID = "ms_bs_1010",
                            commodityName = "初级亢宿卡",
                            description = "适用于所有等级的据点，并且提供30点热度",
                        },
                        new CommodityStructure
                        {
                            commodityID = "ms_bs_1006",
                            commodityName = "高级氐宿卡",
                            description = "适用于中级及以上的据点并且提供30点热度",
                        },
                        new CommodityStructure
                        {
                            commodityID = "ms_bs_1011",
                            commodityName = "高级尾宿卡",
                            description = "适用于中级及以上的据点并且提供30点热度",
                        },
                        new CommodityStructure
                        {
                            commodityID = "ms_bs_2001",
                            commodityName = "高级烛光卡",
                            description = "适用于中级及以上的据点并且提供30点热度",
                        },

                        new CommodityStructure
                        {
                            commodityID = "dz_bs_prop00",
                            commodityName = "道具定制卡_VIP",
                            description = "定制1种游戏道具，提供专业的设计支持！",
                        },

                         new CommodityStructure
                        {
                            commodityID = "dz_bs_prop01",
                            commodityName = "道具定制卡_VVIP",
                            description = "定制2种游戏道具，提供专业的设计支持！",
                        },
                        new CommodityStructure
                        {
                            commodityID = "dz_bs_prop02",
                            commodityName = "道具定制卡_VVVIP",
                            description = "定制 3 种游戏道具，提供专业的设计支持！",

                        },
                         new CommodityStructure
                        {
                            commodityID = "dz_bs_monster00",
                            commodityName = "道具定制卡_SVIP",
                            description = "定制 1 个高级游戏角色！提供专业的设计支持！打造您的独特品牌效应",
                        },
                    }
                };
                string mallJson = JsonMapper.ToJson(_commoditise);
                Debug.Log("AndaSaid:游戏物件配置表(还会更新）：Json=>>>>>>" + mallJson);
            }



            return _commoditise;
        }
    }


    #endregion

    #region MallCommodityType

    public static List<MallCommodityTypeStructure> _mallCommodityType = null;

    public static List<MallCommodityTypeStructure> GetMallCommodityType
    {
        get
        {
            if(_mallCommodityType == null)
            {
                _mallCommodityType = new List<MallCommodityTypeStructure>()
                {
                    new MallCommodityTypeStructure
                    {
                        type = "fd",
                        typeName = "福袋呀",
                        itemIndex = new List<int>
                        {
                           21,22,23,24,25,26,27,28
                        }
                    },
                    new MallCommodityTypeStructure
                    {
                        type = "dz",
                        typeName = "专属定制",
                        itemIndex = new List<int>
                        {
                           29,30,31
                        }
                    },
                    new MallCommodityTypeStructure
                    {
                        type = "sh",
                        typeName = "建造图纸",
                        itemIndex = new List<int> 
                        {
                            0,1,2,3,4,5,6,7,8,9,10
                        }
                    },
                    new MallCommodityTypeStructure
                    {
                        type = "ms",
                        typeName = "星宿卡",
                        itemIndex = new List<int>
                        {
                           14,15,16,17,18,19,20
                        }
                    },
                    new MallCommodityTypeStructure
                    {
                        type = "rc",
                        typeName = "奖励编辑券",
                        itemIndex = new List<int>
                        {
                          11,12,13
                        }
                    },
                };
                string mallJson = JsonMapper.ToJson(_mallCommodityType);
                Debug.Log("AndaSaid:商城界面用到的分类配置：Json=>>>>>>" + mallJson);
            }

           

            return _mallCommodityType;
        }
    }

    #endregion

    #region 物品分类

    private static List<CommodityTypeStructure> _commodityTypeStructure =null;
    public static List<CommodityTypeStructure> commodityTypeStructure
    {
        get
        {
            if(_commodityTypeStructure == null)
            {
                _commodityTypeStructure = new List<CommodityTypeStructure>
                {
                    new CommodityTypeStructure
                    { 
                        typte = "rc",
                        cnName = "优惠券",
                        idList = new   List<string> 
                        {
                            "rc_bs_00",
                            "rc_bs_01"
                        }
                    },
                    new CommodityTypeStructure
                    {
                        typte = "ms",
                        cnName = "怪兽卡",
                        idList = new   List<string>
                        {
                            "ms_bs_1004",
                           "ms_bs_1005",
                            "ms_bs_1006",
                            "ms_bs_1007",
                            "ms_bs_1010",
                            "ms_bs_1011",
                            "ms_bs_2001",
                        }
                    },
                    new CommodityTypeStructure
                    {
                        typte = "sh",
                        cnName = "建造图纸",
                        idList = new   List<string>
                        {
                            "sh_bs_00",
                            "sh_bs_01",
                            "sh_bs_02",
                            "sh_bs_03",
                            "sh_bs_04"
                        }
                    },
                    new CommodityTypeStructure
                    {
                        typte = "dz",
                        cnName = "定制",
                        idList = new   List<string>
                        {
                            "dz_bs_prop00",
                            "dz_bs_prop01",
                            "dz_bs_prop02",
                            "dz_bs_monster00",
                        }
                    },
                };
              
            }

            return _commodityTypeStructure;
        }

    }
    #endregion

    #region 怪兽级分类
    private static List<MonsterLevelBox> _monsterLevelBox =null;
    public static  List<MonsterLevelBox> monsterLevelBox 
    {
        get
        {
            if(_monsterLevelBox == null)
            {
                _monsterLevelBox = new List<MonsterLevelBox>
                {
                    new MonsterLevelBox
                    {
                        level = 0,
                        title = "初级星宿卡",
                        hotValue = 30,
                        data = new List<string>
                        {
                            "ms_bs_1004","ms_bs_1005","ms_bs_1007",
                            "ms_bs_1010"
                        }
                    },
                    new MonsterLevelBox
                    {
                        level =1,
                        title = "高级星宿卡",
                        hotValue =50,
                        data = new List<string>
                        {
                            "ms_bs_1006","ms_bs_1011",
                            "ms_bs_2001"
                        }
                    }
                };

                #if UNITY_EDITOR
                Debug.Log("怪兽的等级分类:" + JsonMapper.ToJson(_monsterLevelBox));
                #endif
            }
            return _monsterLevelBox;
        }
    }
    #endregion

    #region 据点权限配置

    private static List<BussinessSHRootConfig> _bussinessSHRootConfigs = null;
    public static List<BussinessSHRootConfig> bussinessSHRootConfigs
    {
        get
        {
            if(_bussinessSHRootConfigs == null)
            {
                _bussinessSHRootConfigs = new List<BussinessSHRootConfig>
                {
                    new BussinessSHRootConfig
                    {
                        id = "sh_bs_00",
                        level = 0,
                        adsCount = 1,
                        adsLevel = 0,
                        monsterCount = 1,
                        monsterLevel =  0,
                        couponLevel = 1 ,
                        hotValue = 10,
                    }
                    ,
                     new BussinessSHRootConfig
                    {
                        id = "sh_bs_01",
                        level = 1,
                        adsCount = 1,
                        adsLevel = 1,
                        monsterCount = 1,
                        monsterLevel =  0,
                        couponLevel = 1 ,
                        hotValue = 15,

                    },
                    new BussinessSHRootConfig
                    {
                        id = "sh_bs_02",
                        level = 2 ,
                        adsCount = 2,
                        adsLevel = 2,
                        monsterCount = 1,
                        monsterLevel =  1,
                        couponLevel = 1 ,
                        hotValue = 20,

                    },
                    new BussinessSHRootConfig
                    {
                        id = "sh_bs_03",
                        level = 3 ,
                        adsCount = 3 ,
                        adsLevel = 2,
                        monsterCount = 1,
                        monsterLevel = 1,
                        couponLevel = 1 ,
                        hotValue = 30,

                    },
                     new BussinessSHRootConfig
                    {
                        id = "sh_bs_04",
                        level = 4 ,
                        adsCount = 4 ,
                        adsLevel = 2,
                        monsterCount = 1,
                        monsterLevel = 1,
                        couponLevel = 1 ,
                        hotValue = 30,

                    }
                };

                #if UNITY_EDITOR
                Debug.Log("据点权限 ：" +  JsonMapper.ToJson(_bussinessSHRootConfigs));
                #endif

              
      
            }

            return _bussinessSHRootConfigs;
        }
    }
    #endregion


    #region 广告等级权限配置
    private static List<AdsLevelBox> _adsLevelBox = null;
    public  static List<AdsLevelBox> adsLevelBox 
    {
        get
        {
            if(_adsLevelBox == null)
            {
                _adsLevelBox = new List<AdsLevelBox>
                { 
                    new AdsLevelBox
                    {
                        level = 0 ,
                        key = "text",
                        title = "萌新广告",
                        root = new List<string> { "text" },
                    },
                    new AdsLevelBox
                    {
                        level = 1,
                        key ="texture",
                        title = "高级广告",
                        root = new List<string> { "text" ,"texture"},
                    },
                    new AdsLevelBox
                    {
                        level = 2,
                        key ="video",
                        title = "璀璨广告",
                        root = new List<string> { "text" ,"texture","video"},
                    }
                };

#if UNITY_EDITOR

                Debug.Log("广告位的权限：" + JsonMapper.ToJson(_adsLevelBox));

#endif
            }

            return  _adsLevelBox;
        }
    }
    #endregion

    #region 广告图片上传规则
    private static AdsRule _adsRule = null;
    public static AdsRule adsRule 
    {
        get {
            if(_adsRule == null)
            {
                _adsRule =new AdsRule
                {
                    widthLimit = 256,
                    heightLimit = 454,
                };
            }
            return _adsRule;
        }
    }
    #endregion

  
    public static BussinessSHRootConfig GetBussinessSHRootConfigItem(string id)
    {
        return bussinessSHRootConfigs.FirstOrDefault(s=>s.id == id);
    }
    /// <summary>
    /// 根据据点等级查询据点的所有权限配置
    /// </summary>
    /// <returns>The bussiness SHR oot config item.</returns>
    /// <param name="shLevel">Sh level.</param>
    public static BussinessSHRootConfig GetBussinessSHRootConfigItem(int shLevel)
    {
        return bussinessSHRootConfigs.FirstOrDefault(s => s.level == shLevel);
    }

    public static AdsLevelBox GetAdsLevelBoxItem(int level)
    {
        return adsLevelBox.FirstOrDefault(s=>s.level == level);
    }

    public static MonsterLevelBox GetMonsterLevelBox(int level)
    {
        return monsterLevelBox.FirstOrDefault(s=>s.level == level);
    }

    public static MonsterLevelBox GetMonsterLevelBoxByMonsterID(string monsterID)
    {
        int count = monsterLevelBox.Count;
        for(int i = 0 ;i < count;i++)
        {
            if(monsterLevelBox[i].data.Contains(monsterID))
            {
                return monsterLevelBox[i];
            }
        }

        return null;//一般不是在这里，如果有，一定是配置文件问题。或者传入的ID 有误
    }
    /// <summary>
    /// 通过id 查找物件配置表中的物件
    /// </summary>
    /// <returns>The commodity structure item.</returns>
    /// <param name="id">Identifier.</param>
    public static CommodityStructure GetCommodityStructureItem(string id)
    {
        return Commodities.data.FirstOrDefault(s=>s.commodityID == id);
    }
    /// <summary>
    /// 通过 游标 查找 商店中的物件
    /// </summary>
    /// <returns>The mall commodity structure with index.</returns>
    /// <param name="index">Index.</param>
    public static MallCommodityStructure GetMallCommodityStructureWithIndex(int index)
    {
        return BussineMallData.data.FirstOrDefault(s=>s.commodityIndex == index);
    }
    /// <summary>
    /// 通过 id 查找商店中的物件
    /// </summary>
    /// <returns>The mall commodity structure with identifier.</returns>
    /// <param name="id">Identifier.</param>
    public static MallCommodityStructure GetMallCommodityStructureWithID(string id)
    {
        return BussineMallData.data.FirstOrDefault(s => s.commodityID == id);
    }

    public static List<CommodityStructure> SortRCFormCommodityConfig()
    {
        List<CommodityStructure> cs = new List<CommodityStructure>();
        //获取配置文件里所有的票据编道具
        int count = Commodities.data.Count;
        List<string> ids = commodityTypeStructure.FirstOrDefault(s=>s.typte == "rc").idList;
        int count2 = ids.Count;
        for (int i = 0; i < count; i++)
        {
            for (int j = 0; j < count2; j++)
            {
                if(Commodities.data[i].commodityID == ids[j])
                {
                    cs.Add(Commodities.data[i]);
                }
            }
        }

        return cs;
    }



    //test // 假的数据

    private static List<BusinessSD_Pag4U> _userPackages = null;
    public static List<BusinessSD_Pag4U> userPackages
    {
        get
        {
            if(_userPackages == null)
            {
                _userPackages = new List<BusinessSD_Pag4U>
                {
                    new BusinessSD_Pag4U
                    {
                        hostIndex = AndaDataManager.Instance.mainData.playerData.userIndex,
                        commodityID = "rc_bs_00",
                        objectCount = 50,
                        objectIndex = 1,
                    }
                };
            }
            return _userPackages;
        }
    }


}

public class UserPagekageData
{
    public string id{get;set;}
    public int count {get;set;}
}
