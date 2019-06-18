using HedgehogTeam.EasyTouch;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MyGameManager : MonoBehaviour
{
    private static MyGameManager _instance;
    public static MyGameManager Instance
    {
        get { return _instance; }
    }

    public bool isCanSwipe = true;
    public GameObject TipUI;
    public GameObject AimedUI;
    public GameObject disappear;



    private void Awake()
    {
        _instance = this;
    }

    private void OnEnable()
    {
        EasyTouch.On_SwipeEnd += On_SwipeEnd;
    }

    private void OnDisable()
    {
        EasyTouch.On_SwipeEnd -= On_SwipeEnd;
    }

    private void On_SwipeEnd(Gesture gesture)
    {
        if (!isCanSwipe) return;

        switch (gesture.swipe)
        {
            case EasyTouch.SwipeDirection.Left:
            case EasyTouch.SwipeDirection.Up:
            case EasyTouch.SwipeDirection.UpLeft:
            case EasyTouch.SwipeDirection.DownLeft:
                WholeRotateManager.Instance.SetTargetRotation(true);
                break;
            case EasyTouch.SwipeDirection.Right:
            case EasyTouch.SwipeDirection.Down:
            case EasyTouch.SwipeDirection.DownRight:
            case EasyTouch.SwipeDirection.UpRight:
                WholeRotateManager.Instance.SetTargetRotation(false);
                break;
        }

    }


    public IEnumerator DelayAction(float delayTime,Action action)
    {
        yield return new WaitForSeconds(delayTime);
        if (action!= null) action();
    }

    public UnityEngine.UI.RawImage rawImage;
    public Ease ease;
    public void LoadMoive(int indexMovie = 1)
    {
        //Debug.LogError("111111");
        isCanSwipe = false;
        rawImage.gameObject.SetActive(true);
        MovieTexture movieTexture = Resources.Load("Dideo_"+ indexMovie) as MovieTexture;
        rawImage.transform.localScale = Vector3.one;
        rawImage.texture = movieTexture;
        movieTexture.Stop();
        movieTexture.Play();

        SetState_TipUI(false);

        StartCoroutine(DelayAction(10f, delegate ()
        {
            isCanSwipe = true;
            movieTexture.Stop();


            
        }));



        disappear.gameObject.SetActive(false);
        StartCoroutine(DelayAction(9.5f, delegate ()
        {
            rawImage.transform.DOScaleY(0f, 0.3f).OnComplete(delegate() 
            {
                rawImage.gameObject.SetActive(false);
            });

            StartCoroutine(DelayAction(0.2f, delegate ()
             {
                 disappear.gameObject.SetActive(true);
                 disappear.transform.DOScale(0.1f,0.3f).SetEase(ease).OnComplete(delegate() 
                 {
                     disappear.gameObject.SetActive(false);
                     disappear.transform.localScale = Vector3.one;
                 });
             }));

        }));
    }

    IEnumerator dddd()
    {
        yield return new WaitForSeconds(9f);
        rawImage.transform.DOScaleY(0, 0.5f);
        //yield return new WaitForSeconds(0.2f);
        //
    }





    public void SetState_TipUI(bool isShow)
    {
        TipUI.SetActive(isShow);
    }

    public void SetState_AimedUI(bool isShow)
    {
        AimedUI.SetActive(isShow);
    }



    private int HuaDongType = 1;//0插值滑动    1固定滑动
    public bool IsChaZhiHuaDong()
    {
        return isCanSwipe && HuaDongType == 0;
    }

    public bool IsGuDingHuaDong()
    {
        return isCanSwipe && HuaDongType == 1;
    }



    //private RaycastHit hit;
    //private Ray ray;
    //private Vector3 PointRay;
    //Transform LastHit = null;
    //private void OnGUI()
    //{
    //    if (isCanSwipe)
    //    {
    //        Vector3 v = new Vector3(Screen.width / 2, Screen.height / 2, 100f);
    //        ray = Camera.main.ScreenPointToRay(v);
    //        if (Physics.Raycast(ray, out hit))
    //        {
    //            if (hit.transform.tag == "Target")
    //            {
    //                if (LastHit != hit.transform)
    //                {
    //                    LastHit = hit.transform;
    //                    int tmpIndex = -1;
    //                    int.TryParse(hit.transform.name, out tmpIndex);
    //                    if (tmpIndex >= 0) LoadMoive(tmpIndex);
    //                }
    //            }
    //        }
    //        else
    //        {
    //            if (hit.transform == null) LastHit = null;
    //        }
    //    }
    //}
}

public enum HuaDongType
{
    ChaZhi = 0,
    GuDing,
}
