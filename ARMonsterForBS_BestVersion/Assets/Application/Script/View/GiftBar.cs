using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GiftBar : MonoBehaviour {

    private const float cellHeight = 500;

    private Vector2 cellSize;

    private bool isFading = false;

    public GridLayoutGroup gridLayoutGroup;

    private List<itemInfo_GiftBar> itemInfo_GiftBars;

    public void SetInfo(List<MallCommodityStructure> data)
    {

        int count = data.Count;
        for(int i = 0 ; i < count; i++)
        {


            GameObject item = Resources.Load<GameObject>("Prefab/CommodityItemInfo");
            item = Instantiate(item);
            item.transform.parent = gridLayoutGroup.transform;
            item.transform.localScale = Vector3.one;
            item.transform.localPosition = Vector3.zero;

            itemInfo_GiftBar info = item.GetComponent<itemInfo_GiftBar>();

            CommodityStructure commodityStructure = GetGameConfigData.GetCommodityStructureItem(data[i].commodityID);

            Sprite sprite = Resources.Load<Sprite>("Sprite/Commodity/" + commodityStructure.commodityID);

            info.SetInfo(sprite, commodityStructure.commodityName + " x" + data[i].count, commodityStructure.description);
            if(itemInfo_GiftBars == null) itemInfo_GiftBars = new List<itemInfo_GiftBar>();
            itemInfo_GiftBars.Add(info);
        }

        cellSize = Vector2.zero;
        gridLayoutGroup.cellSize = cellSize;
        isFading = true;
        StartCoroutine(FadeIn());

    }

    private IEnumerator FadeIn()
    {
       //scrollRect.verticalNormalizedPosition = 1;

        float t = 0;
        gridLayoutGroup.gameObject.SetActive(true);

        while (t < 1 && isFading)
        {
            t += Time.deltaTime * 4f;

            cellSize.y = Mathf.Lerp(0, cellHeight, t);

            gridLayoutGroup.cellSize = cellSize;

            yield return null;
        }
        isFading = false;
    }

    public void ClickClose()
    {
        isFading = false;
        Destroy(gameObject);
    }

}
