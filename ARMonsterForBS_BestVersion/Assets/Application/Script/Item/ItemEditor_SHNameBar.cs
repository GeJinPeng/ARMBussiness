using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemEditor_SHNameBar : ViewBasic {


    public string type ;
    public InputField descriptionEditorInput;
    public InputField nameEditorInput;
    public GameObject SaveBtn;
    private bool unSave = false;

    public System.Action<string> callbcak;

    private int shIndex;


    public void SetInfo(int _shIndex)
    {

        IsYou(true);

        shIndex =_shIndex;


        if (type == "nickName")
        {
            GetComponent<Animator>().Play("NameFadeIn");
            nameEditorInput.gameObject.SetActive(true);
        }
        else if (type == "description")
        {
            GetComponent<Animator>().Play("DescriptionFadeIn");
            descriptionEditorInput.gameObject.SetActive(true);
        }
    }

    public void Change()
    {
        if(type == "description" &&  descriptionEditorInput.text!= "")
        {
            unSave = true;
            SaveBtn.gameObject.SetActive(true);

            return;
        }

        if(type == "nickName" && nameEditorInput.text!="")
        {
            unSave = true;
            SaveBtn.gameObject.SetActive(true);


        }
    }


    public void ClickSave()
    {
        if(type == "nickName")
        {
            AndaDataManager.Instance.networkController.CallServerEditroStrongholdNickName(shIndex, nameEditorInput.text, CallBackFinishUploadContent);

        }else if(type == "description")
        {
            AndaDataManager.Instance.networkController.CallServerEditorStrongholdDescription(shIndex, descriptionEditorInput.text, CallBackFinishUploadContent);

        }
    }

    private void CallBackFinishUploadContent(int sh, string content)
    {
        unSave = false;
        if (callbcak!=null)
        {
            callbcak(content);
        }
        Out();
    }

    private Vector3 startMousePose;
    public void FixedUpdate()
    {
        if (isMe)
        {
            if (Input.GetMouseButtonDown(0))
            {
                startMousePose = Input.mousePosition;
            }

            if (Input.GetMouseButtonUp(0))
            {
               

                if (Input.mousePosition.x - startMousePose.x > 500)
                {
                    if (unSave)
                    {
                        isMe = false;
                        AndaUIManager.Instance.PlayTipsForChoose("你尚未保存，请先保存再退出!", 0, "保存", "执意退出", ClickSave, Out);
                        return;
                    }
                    Out();
                }
            }
        }
    }




    private void Out()
    {
        if (type == "nickName")
        {
            GetComponent<Animator>().Play("NameFadeOut");
        }
        else if (type == "description")
        {
            GetComponent<Animator>().Play("DescriptionFadeOut");
        }

        Invoke("DestoryView", 0.8f);

    }

    private void DestoryView()
    {
        lastView.IsYou(true);
        Destroy(gameObject);
    }
}
