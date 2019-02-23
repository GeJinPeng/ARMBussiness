using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using System;
using ZXing;
using ZXing.QrCode;
using System.Security.Cryptography;
using System.Text;

public static class ConvertTool  {

    public static BusinessStrongholdAttribute ConvertToBussinessStrongholdAttribute(BusinessStrongholdGrowUpAttribute bsga)
    {
        BusinessStrongholdAttribute tmp = new BusinessStrongholdAttribute();
        tmp.hostIndex = bsga.hostIndex;
        tmp.strongholdIndex =bsga.strongholdIndex;
        tmp.strongholdNickName = bsga.strongholdNickName;
        tmp.hostType =  bsga.hostType;
        tmp.strongholdID = bsga.strongholdID;
        tmp.strongholdGloryValue = bsga.strongholdGloryValue;
        tmp.hostNickName = AndaDataManager.Instance.mainData.playerData.userName;
        tmp.coupons = bsga.coupons;
        tmp.fightMonsterListIndex = bsga.strongholdFightMonsterList;
        tmp.strongholdPosition = bsga.strongholdPosition;
        tmp.adsInfos = bsga.adsInfos;
        tmp.description = bsga.description;
        tmp.monsterCardID = bsga.bossID;
        return tmp;
    }

    public static PlayerStrongholdAttribute ConvertToPlayerstrongholdAttribute(PlayerStrongHoldGrowUpAttribute info)
    {
        PlayerStrongholdAttribute playerStrongholdAttribute = new PlayerStrongholdAttribute();
        playerStrongholdAttribute.strongholdIndex = info.strongholdIndex;
        playerStrongholdAttribute.medalLevel = info.playerStrongholdMedalLevel;
        playerStrongholdAttribute.strongholdNickName = info.strongholdNickName;
        playerStrongholdAttribute.strongholdPosition = info.strongholdPosition;
        playerStrongholdAttribute.coupons = info.coupons;
        return playerStrongholdAttribute;

    }

    public static LD_Objs ConvertToLd_objs(SD_Pag4U sD_Pag4U)
    {
        LD_Objs lD_Objs = new LD_Objs();
        lD_Objs.objID = sD_Pag4U.objectID;
        lD_Objs.objectType = AndaDataManager.Instance.GetObjTypeID(lD_Objs.objID);
        lD_Objs.objSmallID = lD_Objs.objID - lD_Objs.objectType;
        CD_ObjAttr cD_ObjAttr = AndaDataManager.Instance.objectsList.FirstOrDefault(s=>s.objectID == lD_Objs.objectType);
        lD_Objs.objIndex = sD_Pag4U.objectIndex;
        lD_Objs.objName = cD_ObjAttr.objectName[lD_Objs.objSmallID];
        lD_Objs.lessCount = sD_Pag4U.objectCount;
        lD_Objs.giveValue = sD_Pag4U.objectValue;
        lD_Objs.objDescription = cD_ObjAttr.objectDescription[lD_Objs.objSmallID];
        return lD_Objs;
    }



    public static Texture2D ConvertToTexture2d(Texture2D value)
    {
        Texture2D newT2d = new Texture2D(value.height, value.height);
        newT2d.SetPixels(value.GetPixels());
        newT2d.Apply(true);
        newT2d.filterMode = FilterMode.Trilinear;
        return newT2d;
    }

    public static Sprite ConvertToSpriteWithTexture2d(Texture2D texture2)
    {
        return  Sprite.Create(texture2, new Rect(0, 0, texture2.width, texture2.height),
                                         new Vector2(0.5f, 0.5f));
    }

    /// <summary>
    /// 本地图片文件转Base64字符串
    /// </summary>
    /// <param name="imagepath">本地文件路径</param>
    /// <returns>Base64String</returns>
    public static string bytesToString(byte[] bts)
    {
        return Convert.ToBase64String(bts);
    }

    public static byte[] StringToBytes(string str)
    {
        //return System.Text.Encoding.Default.GetBytes(str);
       return Convert.FromBase64String(str);
        //Base64 Decoded
       // byte[] decoded = System.baseBase64.getDecoder().decode(encoded);
    }


    public static double[] BD09ToGCJ02(double lat, double lon)
    {
        double x = lon - 0.0065, y = lat - 0.006;
        double z = Math.Sqrt(x * x + y * y) - 0.00002 * Math.Sin(y * x_pi);
        double theta = Math.Atan2(y, x) - 0.000003 * Math.Cos(x * x_pi);
        double tempLon = z * Math.Cos(theta);
        double tempLat = z * Math.Sin(theta);
        double[] gps = { tempLat, tempLon };
        return gps;
    }

    public static double[] GCJ02ToWGS84(double lat, double lon)
    {
        double[] gps = transform(lat, lon);
        double lontitude = lon * 2 - gps[1];
        double latitude = lat * 2 - gps[0];
        return new double[] { latitude, lontitude };
    }









    public static double pi = 3.1415926535897932384626;
    public static double x_pi = 3.14159265358979324 * 3000.0 / 180.0;
    public static double a = 6378245.0;
    public static double ee = 0.00669342162296594323;

    public static double transformLat(double x, double y)
    {
        double ret = -100.0 + 2.0 * x + 3.0 * y + 0.2 * y * y + 0.1 * x * y
                         + 0.2 * Math.Sqrt(Math.Abs(x));
        ret += (20.0 * Math.Sin(6.0 * x * pi) + 20.0 * Math.Sin(2.0 * x * pi)) * 2.0 / 3.0;
        ret += (20.0 * Math.Sin(y * pi) + 40.0 * Math.Sin(y / 3.0 * pi)) * 2.0 / 3.0;
        ret += (160.0 * Math.Sin(y / 12.0 * pi) + 320 * Math.Sin(y * pi / 30.0)) * 2.0 / 3.0;
        return ret;
    }

    public static double transformLon(double x, double y)
    {
        double ret = 300.0 + x + 2.0 * y + 0.1 * x * x + 0.1 * x * y + 0.1
                        * Math.Sqrt(Math.Abs(x));
        ret += (20.0 * Math.Sin(6.0 * x * pi) + 20.0 * Math.Sin(2.0 * x * pi)) * 2.0 / 3.0;
        ret += (20.0 * Math.Sin(x * pi) + 40.0 * Math.Sin(x / 3.0 * pi)) * 2.0 / 3.0;
        ret += (150.0 * Math.Sin(x / 12.0 * pi) + 300.0 * Math.Sin(x / 30.0
                                                                   * pi)) * 2.0 / 3.0;
        return ret;
    }
    public static double[] transform(double lat, double lon)
    {
        if (outOfChina(lat, lon))
        {
            return new double[] { lat, lon };
        }
        double dLat = transformLat(lon - 105.0, lat - 35.0);
        double dLon = transformLon(lon - 105.0, lat - 35.0);
        double radLat = lat / 180.0 * pi;
        double magic = Math.Sin(radLat);
        magic = 1 - ee * magic * magic;
        double sqrtMagic = Math.Sqrt(magic);
        dLat = (dLat * 180.0) / ((a * (1 - ee)) / (magic * sqrtMagic) * pi);
        dLon = (dLon * 180.0) / (a / sqrtMagic * Math.Cos(radLat) * pi);
        double mgLat = lat + dLat;
        double mgLon = lon + dLon;
        return new double[] { mgLat, mgLon };
    }
    public static bool outOfChina(double lat, double lon)
    {
        if (lon < 72.004 || lon > 137.8347)
            return true;
        if (lat < 0.8293 || lat > 55.8271)
            return true;
        return false;
    }


    #region 时间戳管理
    /// <summary>
    /// 日期转换成unix时间戳
    /// </summary>
    /// <returns></returns>
    public static int GetTimestamp()
    {
        DateTime dateTime = DateTime.Now;
        var start = new DateTime(1970, 1, 1, 0, 0, 0, dateTime.Kind);
        return Convert.ToInt32((dateTime - start.AddHours(8)).TotalSeconds);
    }
    /// <summary>
    /// 日期转换成unix时间戳
    /// </summary>
    /// <returns></returns>
    public static int GetTimestamp(DateTime dateTime)
    {
        var start = new DateTime(1970, 1, 1, 0, 0, 0, dateTime.Kind);
        return Convert.ToInt32((dateTime - start.AddHours(8)).TotalSeconds);
    }
    /// <summary>
    /// unix时间戳转换成日期
    /// </summary>
    /// <param name="timestamp">时间戳(秒)</param>
    /// <returns></returns>
    public static DateTime UnixTimestampToDateTime(long timestamp)
    {
        DateTime target = DateTime.Now;
        var start = new DateTime(1970, 1, 1, 8, 0, 0, target.Kind);
        return start.AddSeconds(timestamp);
    }
    #endregion


    public class AESEncryptor
    {
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="AESKey"></param>
        /// <returns></returns>
        public static string Encrypt(string plainText, string AESKey)
        {
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {

                byte[] inputByteArray = Encoding.UTF8.GetBytes(plainText);
                des.Key = ASCIIEncoding.ASCII.GetBytes(AESKey.Substring(0, 8));
                des.IV = ASCIIEncoding.ASCII.GetBytes(AESKey.Substring(0, 8));
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    cs.Close();
                }
                string str = Convert.ToBase64String(ms.ToArray());
                ms.Close();
                return str;
            }
        }
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="showText"></param>
        /// <param name="AESKey"></param>
        /// <returns></returns>
        public static string Decrypt(string showText, string AESKey)
        {
            byte[] inputByteArray = Convert.FromBase64String(showText);
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                des.Key = ASCIIEncoding.ASCII.GetBytes(AESKey.Substring(0, 8));
                des.IV = ASCIIEncoding.ASCII.GetBytes(AESKey.Substring(0, 8));
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    cs.Close();
                }
                string str = Encoding.UTF8.GetString(ms.ToArray());
                ms.Close();
                return str;
            }
        }
    }
}

public class QRcodeDrawTool
{


    //定义方法生成二维码   
    private static Color32[] Encode(string textForEncoding, int width, int height)
    {
        var writer = new BarcodeWriter
        {
            Format = BarcodeFormat.QR_CODE,
            Options = new QrCodeEncodingOptions
            {
                Height = height,
                Width = width
            }
        };
        return writer.Write(textForEncoding);
    }
    public static Texture2D ShowCode(string textForEncoding)
    {
        Texture2D encoded = new Texture2D(256, 256);
        if (textForEncoding != null)
        {            //二维码写入图片          

            var color32 = Encode(textForEncoding, encoded.width, encoded.height);
            encoded.SetPixels32(color32);
            encoded.Apply();

            //重新赋值一张图，计算大小,避免白色边框过大
            Texture2D encoded1;
            encoded1 = new Texture2D(190, 190);
            //创建目标图片大小          
            encoded1.SetPixels(encoded.GetPixels(32, 32, 190, 190));
            encoded1.Apply();
            return encoded1;
        }
        else
        {
            return null;
        }
    }

}
