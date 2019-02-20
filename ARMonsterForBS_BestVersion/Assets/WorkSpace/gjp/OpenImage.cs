using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class OpenImage : MonoBehaviour {

    public Image image;
    public GameObject CutObject;

    //此方法是外部调用插件内部的方法

    private System.Action<Texture2D> callbackSprite;

    public void RegisterCallback(System.Action<Texture2D> callback)
    {
        callbackSprite = callback;
    }
    public void OpenPonePhoto(Button openButton )
    {
     
        TakePhoto(openButton);
    }


    public void TakePhoto(Button openButton,int maxSize = -1)
    {
        openButton.enabled = false;
        //调用插件自带接口，拉取相册，内部有区分平台
        NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
        {
            Debug.Log("Image path: " + path);
            if (path != null)
            {
                // 此Action为选取图片后的回调，返回一个Texture2D 
                AndaUIManager.Instance.OpenWaitBoard(true);
                Texture2D texture = NativeGallery.LoadImageAtPath(path, maxSize);
                if (texture == null)
                {
                    Debug.Log("Couldn't load texture from " + path);
                    return;
                }
                float bl = 1;
                if (image.rectTransform.sizeDelta.x > image.rectTransform.sizeDelta.y && image.rectTransform.sizeDelta.x > 800)
                {
                    bl = 800 / image.rectTransform.sizeDelta.x;
                    var newtexture = Change(image.sprite.texture, 800, Convert.ToInt32(bl * image.rectTransform.sizeDelta.y));
                    image.sprite = Sprite.Create(newtexture, new Rect(0, 0, newtexture.width, newtexture.height), new Vector2(0, 0));
                    image.SetNativeSize();
                }
                else if (image.rectTransform.sizeDelta.x <= image.rectTransform.sizeDelta.y && image.rectTransform.sizeDelta.y > 800)
                {
                    bl = 800 / image.rectTransform.sizeDelta.y;
                    var newtexture = Change(image.sprite.texture, Convert.ToInt32(bl * image.rectTransform.sizeDelta.x), 800);
                    image.sprite = Sprite.Create(newtexture, new Rect(0, 0, newtexture.width, newtexture.height), new Vector2(0, 0));
                    image.SetNativeSize();
                    //image.sprite.texture.Resize(Convert.ToInt32(bl * image.rectTransform.sizeDelta.x), 800);
                }
                else
                {
                    image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0));
                    // result.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0));
                    image.transform.localPosition = Vector3.one;
                    image.SetNativeSize();
                }
                var imageWidth = image.rectTransform.sizeDelta.x * image.transform.localScale.x;
                var imageHeight = image.rectTransform.sizeDelta.y * image.transform.localScale.y;

                if (imageWidth < imageHeight && imageWidth != 300)
                {
                    var bl2 = 300 / imageWidth;
                    image.transform.localScale = image.transform.localScale * bl2;
                    image.transform.localPosition = Vector3.zero;
                    imageWidth = image.rectTransform.sizeDelta.x * image.transform.localScale.x;
                    imageHeight = image.rectTransform.sizeDelta.y * image.transform.localScale.y;
                }
                else if (imageWidth >= imageHeight && imageHeight != 300)
                {
                    var bl2 = 300 / imageHeight;
                    image.transform.localScale = image.transform.localScale * bl2;
                    image.transform.localPosition = Vector3.zero;
                    imageWidth = image.rectTransform.sizeDelta.x * image.transform.localScale.x;
                    imageHeight = image.rectTransform.sizeDelta.y * image.transform.localScale.y;
                }
                Debug.Log(texture.name);
                AndaUIManager.Instance.OpenWaitBoard(false);

            }
            else {
                gameObject.SetActive(false);
            }

        }, "选择图片", "image/png", maxSize);
        Debug.Log("Permission result: " + permission);
        openButton.enabled = true;
    }

    public void Save()
    {
        CutPhoto();

        image.transform.localPosition = new Vector3(0, 0, 0);
        CutObject.transform.localPosition = new Vector3(0, 0, 0);
        image.transform.localScale = Vector3.one;
        gameObject.SetActive(false);
      //  StartCoroutine(SavePhoto());
    }

    /// <summary>
    /// 保存图片
    /// </summary>
    public IEnumerator SavePhoto()
    {
        yield return new WaitForEndOfFrame();
        //Debug.Log( "Permission result: " + NativeGallery.SaveImageToGallery(result.sprite.texture, "GalleryTest", "My img {0}.png" ) );
       
       
        // To avoid memory leaks	
        //Destroy(ss);
    }

    public int rectx = 0;
    public int recty = 0;
    public int rectWidth = 0;
    public int rectHeight = 0;

    public void CutPhoto()
    {
        var imagewidth = image.rectTransform.sizeDelta.x;
        var imageheight = image.rectTransform.sizeDelta.y;

        var truex = imagewidth * image.transform.localScale.x;
        var truey = imageheight * image.transform.localScale.y;
        //if (truey < 300)
        //{
        //    truey = 300;
        //    image.transform.localPosition = transform.localPosition = Vector3.zero;
        //}
        //if (truex < 300)
        //{
        //    truex = 300;
        //    image.transform.localPosition = transform.localPosition = Vector3.zero;
        //}
        var newtexture = Change(image.sprite.texture, Convert.ToInt32(truex), Convert.ToInt32(truey));

        image.sprite = Sprite.Create(newtexture, new Rect(0, 0, newtexture.width, newtexture.height), new Vector2(0, 0));
        image.SetNativeSize();
        image.transform.localScale = Vector3.one;
       // var texture = 
        if (callbackSprite != null) callbackSprite(Cut(image.sprite.texture));
       // Destroy(texture);
    }

    Texture2D Cut(Texture texture)
    {
        var cutWidth = 256;
        var cutHeight = 256;

        var x = CutObject.transform.localPosition.x - image.transform.localPosition.x;
        var y = CutObject.transform.localPosition.y - image.transform.localPosition.y;
      

        int imagewidth = (int)image.rectTransform.sizeDelta.x ;
        int imageheight = (int)image.rectTransform.sizeDelta.y ;


        rectx = Convert.ToInt32((imagewidth / 2 - (cutWidth / 2)) + x);
        if (imagewidth <= cutWidth)
            rectx = 0;
#if UNITY_ANDROID
        recty = Convert.ToInt32((imageheight / 2 - (cutHeight / 2)) + y);
        if (imageheight <= cutHeight)
            recty = 0;
#endif
#if UNITY_EDITOR
        recty = Convert.ToInt32((imageheight / 2 - (cutHeight / 2)) - y);
        if (imageheight <= cutHeight)
            recty = 0;
#endif
#if UNITY_IPHONE
     recty = Convert.ToInt32((imageheight / 2 - (cutWidth / 2)) + y);
#endif
#if UNITY_STANDALONE_WIN
         recty = Convert.ToInt32((imageheight / 2 - (cutWidth / 2)) - y);
#endif


        rectWidth = (int)cutWidth;
        rectHeight = (int)cutHeight;

        if (rectx+cutWidth >= imagewidth)
        {
            if(rectx>= imagewidth)
            {
                Debug.Log("截图区域不在图片上");
                return null;
            }
            rectWidth = rectWidth- (imagewidth -rectx) ;
        }
        if (recty + cutHeight >= imageheight)
        {
            if (rectx >= imagewidth)
            {
                Debug.Log("截图区域不在图片上");
                return null;
            }
            rectWidth = rectWidth - (imageheight - recty);
        }

        if (rectx < 0)
        {
            rectWidth -= rectx;
            if (rectWidth <= 0)
            {
                Debug.Log("截图区域不在图片上");
                return null;
            }
            rectx = 0;
        }
        if (recty < 0)
        {
            rectHeight += recty;
            if (rectHeight <= 0)
            {
                Debug.Log("截图区域不在图片上");
                return null;
            }
            recty = 0;
        }
        var rect = new Rect(rectx, recty, rectWidth, rectHeight);
        Debug.Log(rect);
        Texture2D texture2D = new Texture2D((int)cutWidth, (int)cutHeight, TextureFormat.RGBA32, false);
        RenderTexture renderTexture = RenderTexture.GetTemporary((int)imagewidth, (int)imageheight, 32);
        Graphics.Blit(texture, renderTexture);
        RenderTexture.active = renderTexture;
        texture2D.ReadPixels(rect, 0, 0);
        texture2D.Apply();
        RenderTexture.ReleaseTemporary(renderTexture);
        return texture2D;
    }



    Texture2D Change(Texture2D source, int targetWidth, int targetHeight)
    {
        

        Texture2D result = new Texture2D(targetWidth, targetHeight, source.format, false);

        var rect = new Rect(0, 0, source.width, source.height);
        Texture2D texture2D = new Texture2D((int)source.width, (int)source.height, TextureFormat.RGBA32, false);
        RenderTexture renderTexture = RenderTexture.GetTemporary((int)source.width, (int)source.height, 32);
        Graphics.Blit(source, renderTexture);
        RenderTexture.active = renderTexture;
        texture2D.ReadPixels(rect, 0, 0);
        texture2D.Apply();
     
        float incX = (1.0f / (float)targetWidth);
        float incY = (1.0f / (float)targetHeight);

        for (int i = 0; i < result.height; ++i)
        {
            for (int j = 0; j < result.width; ++j)
            {
                Color newColor = texture2D.GetPixelBilinear((float)j / (float)result.width, (float)i / (float)result.height);
                result.SetPixel(j, i, newColor);
            }
        }
        RenderTexture.ReleaseTemporary(renderTexture);
        result.Apply();
        return result;
    }
}
