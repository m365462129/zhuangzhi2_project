using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instacne
    {
        get { return _instance; }
    }
    private void Awake()
    {
        _instance = this;
        parentRoot = this.transform;
    }

    public Ease ease;

    private Transform parentRoot;
    private float showWindowAnimTime = 4f;
    private Dictionary<WindowType, GameObject> Dic = new Dictionary<WindowType, GameObject>();

    public void ShowWindow(WindowType type,Action OnCompleteCallback=null)
    {
        GameObject WindowGo = null;
        if (!Dic.ContainsKey(type))
        {
            string path = "UIWindow/" + type.ToString();
            UnityEngine.Object obj = Resources.Load<UnityEngine.Object>(path);
            if (obj == null)
            {
                Debug.LogError("error:path=" + path);
                return;
            }
            WindowGo = GameObject.Instantiate(obj) as GameObject;
            WindowGo.transform.parent = parentRoot;
            WindowGo.transform.localScale = Vector3.one;
            WindowGo.transform.localPosition = Vector3.zero;
            WindowGo.transform.localEulerAngles = Vector3.zero;
            WindowGo.SetActive(true);
            Dic.Add(type, WindowGo);
        }
        else
        {
            WindowGo = Dic[type];
            if (WindowGo == null)
            {
                Dic.Remove(type);
                ShowWindow(type, OnCompleteCallback);
                return;
            }
        }
        MyGameManager.Instance.isCanSwipe = false; 
        SetWeiZhi_ZuoXia(WindowGo.transform);
        EnterAnim(WindowGo.transform, OnCompleteCallback);
    }

    public void HideWindow(WindowType type)
    {
        if (Dic.ContainsKey(type))
        {
            GameObject go = Dic[type];
            OutAnim(go.transform);
        }
    }

//    private void Update()
//    {
//#if UNITY_EDITOR
//        if (Input.GetKeyDown(KeyCode.Alpha1))
//        {
//            ShowAndHideWindow(WindowType.UI_0);
//        }
//#endif
//    }

    public void ShowAndHideWindow(WindowType type)
    {
        UIManager.Instacne.ShowWindow(type);
        StartCoroutine(Delay(showWindowAnimTime, delegate ()
        {
            HideWindow(type);
        }));
    }

    public void DestroyGo(GameObject _go)
    {
        if(_go!=null) Destroy(_go);
    }


    private void SetWeiZhi_ZuoXia(Transform _transform)
    {
        _transform.localPosition = new Vector3(-600, -600, 1000);
        //_transform.localEulerAngles = new Vector3(0, 50, -20);
        _transform.localEulerAngles = new Vector3(-90, -90, -20);
    }

    private void EnterAnim(Transform _transform,Action OnCompleteCallback = null)
    {
        _transform.DOLocalRotate(new Vector3(-20, 20,0), showWindowAnimTime);
        //List<Ease> tmpList = new List<Ease>() { Ease.OutCirc,Ease.OutBack,Ease.OutBounce,Ease.OutQuad };
        //Ease tempease = tmpList[UnityEngine.Random.Range(0, tmpList.Count - 1)];
        _transform.DOLocalMove(new Vector3(0, 0, 0), showWindowAnimTime).SetEase(Ease.OutCirc).OnComplete(delegate ()
        {
            if (OnCompleteCallback != null) OnCompleteCallback();
        });
    }

    IEnumerator Delay(float DelayTime,Action callback)
    {
        yield return new WaitForSeconds(DelayTime);
        if (callback != null) callback();
    }

    private void OutAnim(Transform _transform)
    {
        _transform.DOLocalRotate(new Vector3(90, 90, -10), showWindowAnimTime);
        _transform.DOLocalMove(new Vector3(1000, 1000, 1000), showWindowAnimTime).SetEase(Ease.InSine).OnComplete(delegate ()
        {
            DestroyGo(_transform.gameObject);
            MyGameManager.Instance.isCanSwipe = true;
        });
    }




}


public enum WindowType
{
    UI_0 = 0,
    UI_1,
    UI_2,
    UI_3,

    Max,
}
