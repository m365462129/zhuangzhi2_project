using HedgehogTeam.EasyTouch;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGameManager : MonoBehaviour
{
    private static MyGameManager _instance;
    public static MyGameManager Instance
    {
        get { return _instance; }
    }

    public bool isCanSwipe = true;
    private int curUIIndex = -1;//ui的Index
    private int curSheXiangJiIndex = 0;//摄像机的index

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
        Debug.LogError("gesture.swipe=" + gesture.swipe);
        if (!isCanSwipe) return;

        curUIIndex++;
        if (curUIIndex >= (int)WindowType.Max) curUIIndex = 0;
        LoadMoive(curUIIndex);
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
               
                //StartCoroutine(DelayAction(0f, delegate () 
                //{
                //    UIManager.Instacne.ShowAndHideWindow((WindowType)curUIIndex);
                //}));
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
    public void LoadMoive(int indexMovie = 1)
    {
        Debug.LogError("111111");
        rawImage.gameObject.SetActive(true);
        MovieTexture movieTexture = Resources.Load("Dideo_"+ indexMovie) as MovieTexture;
        rawImage.texture = movieTexture;
        movieTexture.Stop();
        movieTexture.Play();
    }
}
