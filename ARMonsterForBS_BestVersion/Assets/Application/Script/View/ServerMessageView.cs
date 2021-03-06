﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ServerMessageView : MonoBehaviour {


    public GameObject main;

    public GameObject ScrollContent;


    public Text content;

    public List<GameObject> ItemList;
    public Text messageCout;
    public float standardTime = 30f;
    public int Status = 0; //0初始化 1开始 2停止

    public ItemInfo_ServerMessage selectServerItem;

    public List<ServerMessage> SMMData;


    public GameObject confirmButton;

    public void Awake()
    {
        AndaMessageManager.Instance.serverMessageView = this;
        Status = -1;
    }
    // Use this for initialization
    void Start()
    {
        InstanceContentPanel();
    }

    // Update is called once per frame
    void Update()
    {
        if (Status == 0)
        {
            Status = 1;
            StartCoroutine("TimeChange");
        }
    }
    IEnumerator TimeChange()
    {
        while (Status == 1)
        {
            Debug.Log("申请消息");
            AndaMessageManager.Instance.GetServerMessage();
            messageCout.text = AndaMessageManager.Instance.GetSMMData().Count(o=>o.receiveTime==0).ToString();
            yield return new WaitForSeconds(standardTime);
        }
    }
    public void Stop()
    {
        if (Status == 1)
        {
            Status = 2;
            StopCoroutine("TimeChange");
        }
    }
    public void Begin()
    {
        if (Status == 1)
        {
            return;
        }
        if (Status == 2)
            StopCoroutine("TimeChange");
        Status = 0;
    }
    public void ShowMain()
    {
        if (main.activeSelf)//当面吧打开时 关闭
        {
            CloseMain();
            return;
        }
        Begin();
        main.SetActive(true);
        if (ItemList.Count == 0)
            return;
        ItemList[ItemList.Count-1].GetComponent<ItemInfo_ServerMessage>().OnPointerClick(null);
    }
    public void CloseMain()
    {
        main.SetActive(false);
    }
    public void InstanceContentPanel()
    {
        SetContentPanel(AndaMessageManager.Instance.GetSMMData());
    }

    public void SetContentPanel(List<ServerMessage> list)
    {
        if (ItemList == null)
            ItemList = new List<GameObject>();
        if (list == null)
            return;
        int count = list.Count;
        if (ItemList!=null)
        {
            foreach (var m in ItemList)
            {
                if (m != null)
                    m.transform.parent = null;
            }
            ItemList.Clear();
        }
        for (int i = 0; i < count; i++)
        {
            GameObject item = AndaDataManager.Instance.GetItemInfoPrefab("ServerItem");
            item = Instantiate(item);
            item.transform.parent = ScrollContent.transform;
            item.transform.localScale = Vector3.one;
            item.transform.localPosition = Vector3.zero;
            item.transform.SetAsFirstSibling();
            item.GetComponent<ItemInfo_ServerMessage>().SetInfo(this,  list[i]);
            ItemList.Add(item);
        }
    }
    public void CloseOtherItem()
    {
        if (selectServerItem != null)
            selectServerItem.CloseContent();
    }
    public void AddContentPanel(List<ServerMessage> list)
    {
        if (ItemList == null)
            ItemList = new List<GameObject>();
        if (list == null)
            return;
        int count = list.Count;
      
        for (int i = 0; i < count; i++)
        {
            GameObject item = AndaDataManager.Instance.GetItemInfoPrefab("ServerItem");
            item = Instantiate(item);
            item.transform.parent = ScrollContent.transform;
            item.transform.localScale = Vector3.one;
            item.transform.localPosition = Vector3.zero;
            item.transform.SetAsFirstSibling();
            item.GetComponent<ItemInfo_ServerMessage>().SetInfo(this,  list[i]);
            ItemList.Add(item);
        }
    }

    public void ClearHistory()
    {
        AndaMessageManager.Instance.ClearHistory();
    }

    public void Confirm()
    {
        //目前该功能仅用于邮箱中快捷确认玩家优惠卷
        if (selectServerItem.info.objectList.Count != 0)
        {
            foreach (var m in selectServerItem.info.objectList)
            {
                if (m.type == 3)
                {
                    if (m.status == 1)
                    {
                        AndaPlayerCouponManager.Instance.Cheack(m.index);
                        m.status = 2;
                        AndaMessageManager.Instance.SaveData();
                        confirmButton.SetActive(false);
                        confirmButton.GetComponent<Button>().enabled = false;
                        confirmButton.transform.GetChild(0).GetComponent<Text>().text = "已处理";
                        break;
                    }
                }
            }
        }
    }
}
