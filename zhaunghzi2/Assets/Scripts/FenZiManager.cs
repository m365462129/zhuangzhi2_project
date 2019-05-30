using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
    public int curWindowIndex = -1;
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
                //if (targetDic.ContainsKey(target.Id))
                //{
                //    Debug.LogError("已经包含：" + target.transform.name);
                //}
                //else
                //{
                //    Debug.LogError("新增：" + target.Id);
                //    targetDic.Add(target.Id, target);
                //}
            }
        }

        targetList = targetList.OrderBy(a => a.Id).ToList(); ;



        for (int i = 0; i < targetList.Count; i++)
        {
            if (targetList[i].Id == 0)
            {
                SetCameraPos(targetList[0].CameraPosition);
            }
            
        }


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
            curWindowIndex++;
            //if (curWindowIndex >= targetDic.Count)
            //{
            //    curWindowIndex = 0;
            //}
            //if (targetDic.ContainsKey(curWindowIndex))
            //{
            //    Target tmp = targetDic[curWindowIndex];
            //    ThirdPersonCamera.Instance.SetTargetPos(tmp.CameraPosition);
            //}
            if (curWindowIndex >= targetList.Count)
            {
                curWindowIndex = 0;
            }
            for (int i = 0; i < targetList.Count; i++)
            {
                if (targetList[i].Id == curWindowIndex)
                {
                    SetCameraPos(targetList[i].CameraPosition);
                }
            }
        }
    }


    public void SetCameraPos(Vector3 pos)
    {
        ThirdPersonCamera.Instance.SetTargetPos(pos);
    }


}
