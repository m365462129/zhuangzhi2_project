using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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

    private Transform parentRoot;
    private float showWindowAnimTime = 3f;
    private Dictionary<WindowType, GameObject> Dic = new Dictionary<WindowType, GameObject>();

    public void ShowWindow(WindowType type)
    {
        GameObject WindowGo = null;
        if (!Dic.ContainsKey(type))
        {
            string path = "UIWindow/" + type.ToString();
            Object obj = Resources.Load<Object>(path);
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
                ShowWindow(type);
                return;
            }
        }
        SetWeiZhi_ZuoXia(WindowGo.transform);
        EnterAnim(WindowGo.transform);
    }

    public void HideWindow(WindowType type)
    {
        if (Dic.ContainsKey(type))
        {
            GameObject go = Dic[type];
            OutAnim(go.transform);
        }
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UIManager.Instacne.ShowWindow(WindowType.UIType0);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            UIManager.Instacne.HideWindow(WindowType.UIType0);
        }
#endif

    }

    public void DestroyGo(GameObject _go)
    {
        if(_go!=null) Destroy(_go);
    }


    private void SetWeiZhi_ZuoXia(Transform _transform)
    {
        _transform.localPosition = new Vector3(-1000, -1000, 1000);
        _transform.localEulerAngles = new Vector3(0, 80, -50);
    }

    private void EnterAnim(Transform _transform)
    {
        _transform.DOLocalMove(new Vector3(0, 0, 0), showWindowAnimTime);//.SetEase(Ease.InSine)
        _transform.DOLocalRotate(new Vector3(0, 0, 0), showWindowAnimTime);
    }

    private void OutAnim(Transform _transform)
    {
        _transform.DOLocalMove(new Vector3(1000, 1000, 1000), showWindowAnimTime);
        _transform.DOLocalRotate(new Vector3(0, 80, 50), showWindowAnimTime).OnComplete(delegate() 
        {
            DestroyGo(_transform.gameObject);
        });
    }




}


public enum WindowType
{
    UIType0 = 0,
    UIType1,
    UIType2,
    UIType3,

    Max,
}
