using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OperationPanelByBC : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{


    public Vector2 first;
    public Vector2 second;

    public BusinessCouponView businessCouponView;
    public void OnPointerDown(PointerEventData eventData)
    {
        first = new Vector2(eventData.position.x, eventData.position.y);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //鼠标点击A对象，抬起鼠标时响应
        //无论鼠标在何处抬起（即不在A对象中）
        //都会在A对象中响应此事件
        //注：响应此事件的前提是A对象必须响应过OnPointerDown事件

        second = new Vector2(eventData.position.x, eventData.position.y);

        var disX = second.x - first.x;
        var disY = second.y - first.y;

        if (Mathf.Abs(disX) > Mathf.Abs(disY))
        {
            if (disX > 150)
            {
                Debug.Log(1);
                businessCouponView.RightSelect();
                return;
            }
            else if (disX < -150)
            {
                Debug.Log(2);
                businessCouponView.LeftSelect();
                return;
            }
        }
        else
        {
            if (disY < -150)
            {
                Debug.Log(3);
                businessCouponView.CloseMain();
                return;
            }
        }
        businessCouponView.DropSelect();
    }
}
