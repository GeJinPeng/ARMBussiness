using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragEvent : MonoBehaviour
{
    private Vector3 lastmousePos =Vector3.zero;
    private Vector3 newmousePos = Vector3.zero;


    private Touch oldTouch1;  //上次触摸点1(手指1)

    private Touch oldTouch2;  //上次触摸点2(手指2)


    private float oldDistance;
    private float newDistance;
    private bool isScale;

    private bool IsTouch = false;

    void Update()
    {
        //没有触摸，就是触摸点为0
        if (Input.touchCount <= 0 )
        {
            if (IsTouch)
                OnDrag();
            IsTouch = false;
            lastmousePos = Vector3.zero;
            newmousePos = Vector3.zero;
            isScale = false;
            return;
        }
        IsTouch = true;
        Touch newTouch1 = Input.GetTouch(0);
        if (Input.touchCount == 1)
        {
            isScale = false;
            if (lastmousePos == Vector3.zero)
            {
                lastmousePos = newTouch1.position;
            }
            newmousePos = newTouch1.position;
            if (lastmousePos != newmousePos)
            {
                var x = lastmousePos.x - newmousePos.x;
                var y = lastmousePos.y - newmousePos.y;
                this.transform.localPosition -= new Vector3(x, y, 0);
            }
            lastmousePos = newmousePos;
            return;
        }
        //多点触摸, 放大缩小
     

        Touch newTouch2 = Input.GetTouch(1);

        //第2点刚开始接触屏幕, 只记录，不做处理

        if (!isScale)
        {

            oldTouch2 = newTouch2;

            oldTouch1 = newTouch1;

            isScale = true;
            oldDistance = Vector2.Distance(oldTouch1.position, oldTouch2.position);
            newDistance = Vector2.Distance(oldTouch1.position, oldTouch2.position);
            return;
        }

        //计算老的两点距离和新的两点间距离，变大要放大模型，变小要缩放模型

         oldDistance = Vector2.Distance(oldTouch1.position, oldTouch2.position);

         newDistance = Vector2.Distance(newTouch1.position, newTouch2.position);

        //两个距离之差，为正表示放大手势， 为负表示缩小手势

        float offset = newDistance - oldDistance;

        //放大因子， 一个像素按 0.01倍来算(100可调整)

        float scaleFactor = offset / 100f;

        Vector3 localScale = transform.localScale;

        Vector3 scale = new Vector3(localScale.x + scaleFactor,

            localScale.y + scaleFactor,

            localScale.z + scaleFactor);

        //在什么情况下进行缩放

        if (scale.x >= 0.05f && scale.y >= 0.05f && scale.z >= 0.05f)
        {
            transform.localScale = scale;
        }

        //记住最新的触摸点，下次使用

        oldTouch1 = newTouch1;

        oldTouch2 = newTouch2;

    }

    public void OnDrag()
    {
        isScale = false;
        lastmousePos = Vector3.zero;
        newmousePos= Vector3.zero;
        var imageWidth = GetComponent<Image>().rectTransform.sizeDelta.x * transform.localScale.x;
        var imageHeight= GetComponent<Image>().rectTransform.sizeDelta.y * transform.localScale.y;
        if (imageWidth < imageHeight && imageWidth < 300)
        {
            var bl = 300 / imageWidth;
            transform.localScale = transform.localScale * bl;
            transform.localPosition = Vector3.zero;
            imageWidth = GetComponent<Image>().rectTransform.sizeDelta.x * transform.localScale.x;
            imageHeight = GetComponent<Image>().rectTransform.sizeDelta.y * transform.localScale.y;
        }

        if (imageWidth >= imageHeight && imageHeight < 300)
        {
            var bl = 300 / imageHeight;
            transform.localScale = transform.localScale * bl;
            transform.localPosition = Vector3.zero;
            imageWidth = GetComponent<Image>().rectTransform.sizeDelta.x * transform.localScale.x;
            imageHeight = GetComponent<Image>().rectTransform.sizeDelta.y * transform.localScale.y;
        }
        if (transform.localPosition.x > (imageWidth / 2 - 130))
        {
            transform.localPosition = new Vector3(imageWidth / 2 - 130, transform.localPosition.y, transform.localPosition.z);
        }
        else if (transform.localPosition.x < -(imageWidth / 2 - 130))
        {
            transform.localPosition = new Vector3(-(imageWidth / 2 - 130), transform.localPosition.y, transform.localPosition.z);
        }
        if (transform.localPosition.y > (imageHeight / 2 - 130))
        {
            transform.localPosition = new Vector3(transform.localPosition.x, (imageHeight / 2 - 130), transform.localPosition.z);
        } 
        else if (transform.localPosition.y < -(imageHeight / 2 - 130))
        {
            transform.localPosition = new Vector3(transform.localPosition.x, -(imageHeight / 2 - 130), transform.localPosition.z);
        }

    }

    public void SetOneScale()
    {
        transform.localScale = Vector3.one;
        transform.localPosition = Vector3.zero;
    }
}

