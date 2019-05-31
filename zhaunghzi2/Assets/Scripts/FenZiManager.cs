using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using HedgehogTeam.EasyTouch;

public class FenZiManager : MonoBehaviour
{
    private static FenZiManager _instance;
    public static FenZiManager Instance
    {
        get { return _instance; }
    }

    public GameObject PreTargetGo;
    public GameObject CurTargetGo;
    public List<Target> targetList = new List<Target>();
    public Dictionary<int, Target> targetDic = new Dictionary<int, Target>();

    public float AutoRotateTime = 5f;
    public float TotalTime = 0;
    private int curJingtuoIndex = 0;
    private void Awake()
    {
        _instance = this;

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform tmpTras = transform.GetChild(i);
            Target target = tmpTras.GetComponent<Target>();
            if (target != null)
            {
                targetList.Add(target);
            }
        }

        targetList = targetList.OrderBy(a => a.Id).ToList(); ;

    }



    public void SetCurTargetGo(GameObject _go)
    {
        if (PreTargetGo == CurTargetGo)
        {
            return;
        }
        PreTargetGo = CurTargetGo;
        CurTargetGo = _go;
        Debug.LogError("PreTargetGo=" + PreTargetGo.name);
        Debug.LogError("CurTargetGo=" + CurTargetGo.name);
    }


    private void Update()
    {
        TotalTime += Time.deltaTime;
        if (TotalTime >= AutoRotateTime)
        {
            Debug.LogError("====");
            TotalTime = 0;
            curJingtuoIndex++;
            if (curJingtuoIndex >2)
            {
                curJingtuoIndex = 0;
                
            }
            CameraManager.Instance.ToJingTuo(curJingtuoIndex);
        }
    }

    private void ResetTotalTime()
    {

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

        switch (gesture.swipe)
        {
            case EasyTouch.SwipeDirection.Left:
            case EasyTouch.SwipeDirection.Up:
            case EasyTouch.SwipeDirection.UpLeft:
            case EasyTouch.SwipeDirection.DownLeft:
                curJingtuoIndex--;
                if (curJingtuoIndex<0) curJingtuoIndex = 2;
                break;

            case EasyTouch.SwipeDirection.Right:
            case EasyTouch.SwipeDirection.Down:
            case EasyTouch.SwipeDirection.DownRight:
            case EasyTouch.SwipeDirection.UpRight:
                curJingtuoIndex++;
                if (curJingtuoIndex >2) curJingtuoIndex = 0;
                break;
        }
        TotalTime = 0;
        CameraManager.Instance.ToJingTuo(curJingtuoIndex);
    }



}
