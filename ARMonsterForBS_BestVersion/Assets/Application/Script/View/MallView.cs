using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
public class MallView : MonoBehaviour  {

    public ScrollRect scrollRect;
    ContentSizeFitter contentSizeFitter;
    public GridLayoutGroup gridLayoutGroup;
    public Text title;
    public GameObject left;
    public GameObject right;

    private const int cellHeight = 500;
    private const string mallCommodityItem = "MallCommodityInfo";
    private List<string> mallType = new List<string>
    {
        "fd","dz","sh" , "ms" ,"rc"
    };
    private List<ItemInfo_MallCommodity> items;
    private Vector2 cellSize;
    private bool isFading =false;
    private int currentIndex;
    private float interval;
    private MallCommodityTypeStructure mt ;
    private void Start()
    {
        SetInfo();
    }

  
    private void BuildItem( )
    {
        if (items == null)
        {
            items = new List<ItemInfo_MallCommodity>();
            int count = mt.itemIndex.Count;
            for(int i = 0 ; i < count; i++)
            {
                GameObject it = Resources.Load<GameObject>("Prefab/" + mallCommodityItem);
                it = Instantiate(it);
                ItemInfo_MallCommodity itMallCo = it.GetComponent<ItemInfo_MallCommodity>();
                itMallCo.SetInfo(GetGameConfigData.GetMallCommodityStructureWithIndex(mt.itemIndex[i]));

                itMallCo.transform.parent = gridLayoutGroup.transform;
                itMallCo.transform.localScale = Vector3.one;
                itMallCo.transform.localPosition = Vector3.zero;

                items.Add(itMallCo);
            }
        }else
        {
            if(items.Count <= mt.itemIndex.Count)
            {
                int count = mt.itemIndex.Count;
                for(int i = 0 ; i< count; i++)
                {
                    if(i < items.Count )
                    {
                        items[i].gameObject.SetActive(true);
                        items[i].SetInfo(GetGameConfigData.GetMallCommodityStructureWithIndex(mt.itemIndex[i]));
                    }
                    else
                    {
                        GameObject it = Resources.Load<GameObject>("Prefab/" + mallCommodityItem);
                        it = Instantiate(it);
                        ItemInfo_MallCommodity itMallCo = it.GetComponent<ItemInfo_MallCommodity>();
                        itMallCo.SetInfo(GetGameConfigData.GetMallCommodityStructureWithIndex(mt.itemIndex[i]));

                        itMallCo.transform.parent = gridLayoutGroup.transform;
                        itMallCo.transform.localScale = Vector3.one;
                        itMallCo.transform.localPosition = Vector3.zero;

                        items.Add(itMallCo);
                    }
                }
            }else
            {
                int count = items.Count;
                for(int i = 0; i < count; i++)
                {
                    if( i < mt.itemIndex.Count)
                    {
                        items[i].gameObject.SetActive(true);
                        items[i].SetInfo(GetGameConfigData.GetMallCommodityStructureWithIndex(mt.itemIndex[i]));
                    }else
                    {
                        items[i].gameObject.SetActive(false);
                    }
                }
            }
        }
       
        StartCoroutine(FadeIn());

    }

    private IEnumerator FadeIn()
    {
        scrollRect.verticalNormalizedPosition = 1;

        float t = 0 ;
        gridLayoutGroup.gameObject.SetActive(true);

        while (t < 1 && isFading)
        {
            t += Time.deltaTime *4f;

            cellSize.y = Mathf.Lerp(0,cellHeight,t);

            gridLayoutGroup.cellSize = cellSize;

            yield return null;
        }
        isFading = false;
    }


    private void RemoveCurrentItems()
    {
        if(items!=null)
        {
            int count  = items.Count;
            for(int i = 0 ; i < count;i++)
            {
                Destroy(items[i]);
            }
        }
    }

    public void ClickRight()
    {
        if(isFading)return;
        currentIndex+=1;
        currentIndex = Mathf.Clamp(currentIndex,0,mallType.Count-1);
        SetInfo();
    }

    public void ClickLeft()
    {
        if (isFading) return;
        currentIndex -= 1;
        currentIndex = Mathf.Clamp(currentIndex, 0, mallType.Count - 1);
        SetInfo();
    }


    private void SetInfo()
    {
        mt = GetGameConfigData.GetMallCommodityType.FirstOrDefault(s => s.type == mallType[currentIndex]);
        title.text = mt.typeName;
        right.gameObject.SetActive(currentIndex < mallType.Count - 1);
        left.gameObject.SetActive(currentIndex > 0 );
        cellSize = Vector2.zero;
        gridLayoutGroup.cellSize = cellSize;
        gridLayoutGroup.gameObject.SetActive(false);
       
        if (interval.Equals(0))
        {
            StartCoroutine(WaitForInterval());
        }else
        {
            interval = 0;
        }

    }

    private IEnumerator WaitForInterval()
    {
        while(interval < 0.25f)
        {
            interval += Time.deltaTime;
            yield return null; 
        }

       //isFading = false;
        isFading = true;

        interval = 0;

        BuildItem();
    }


}
