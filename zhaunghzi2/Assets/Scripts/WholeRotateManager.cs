using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WholeRotateManager : MonoBehaviour
{
    public static WholeRotateManager Instance;

    MyGameManager MyGameManagerInstance;
    private void Awake()
    {
        Instance = this;
        TargetRotation = transform.localEulerAngles;//26.442,250.05，0
        MyGameManagerInstance = MyGameManager.Instance;
    }

    public float smooth = 5f;
    private Vector3 TargetRotation;
    public void SetTargetRotation(bool isLeft)
    {
        //if (MyGameManagerInstance.IsChaZhiHuaDong())
        //{
        //    if (isLeft)
        //    {
        //        TargetRotation.y = TargetRotation.y + 90;
        //    }
        //    else
        //    {
        //        TargetRotation.y = TargetRotation.y - 90;
        //    }
        //}
    }


    float x;
    float y;
    public float yMinLimit = -50;
    public float yMaxLimit = 50;

    float LastSmoothTime = 0;
    void LateUpdate()
    {
        return;
        if (MyGameManagerInstance.IsChaZhiHuaDong()) //插值滑动
        {
            //Vector3 v = Vector3.Lerp(transform.localEulerAngles, TargetRotation, Time.deltaTime * smooth);
            //transform.localEulerAngles = v;
            //if (v.y >= 360)
            //{
            //    v.y = v.y - 360;
            //    transform.localEulerAngles = v;
            //    TargetRotation.y = TargetRotation.y - 360;
            //}
            //else if (v.y <= 0)
            //{
            //    v.y = v.y + 360;
            //    transform.localEulerAngles = v;
            //    TargetRotation.y = TargetRotation.y + 360;
            //}
        }
        else if (MyGameManagerInstance.IsGuDingHuaDong()) //鼠标滑动
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 vv = transform.eulerAngles;

                if (vv.y==0)
                {
                    vv.z = 0;
                    transform.eulerAngles = vv;
                    x = transform.eulerAngles.y;
                    y = transform.eulerAngles.x;
                } 
            }

            if (Input.GetMouseButton(0))
            {
                Vector3 v3 = transform.eulerAngles;
                v3.z = 0;
                transform.eulerAngles = v3;

                //Vector2 v2 = Input.GetTouch(0).deltaPosition;
                //Debug.LogError("===v2="+v2);
#if UNITY_EDITOR

#else

#endif

                x -= Input.GetAxis("Mouse X") * smooth;
                if (x >= 360)
                    x -= 360;
                else if (x<=0)
                    x += 360;

                y -= Input.GetAxis("Mouse Y") * smooth;
                //y = ClampAngle(y, yMinLimit, yMaxLimit);
                if (y >= 360)
                    y -= 360;
                else if (y <= 0)
                    y += 360;


                v3.z = 0;
                v3.x = y;
                v3.y = x;



                transform.eulerAngles = v3;
                //Debug.LogError("==v3="+ v3);
                LastSmoothTime = 0;
                MyGameManager.Instance.SetState_TipUI(false);
            }
            
        }
    }

    public void SetState_ChangJing(bool isShow)
    {
        gameObject.SetActive(isShow);
    }

    public void EasyTouchOn_Swipe(Vector2 v2)
    {
        Vector3 v3 = transform.eulerAngles;

        x -= v2.x * smooth;
        if (x >= 360)
            x -= 360;
        else if (x <= 0)
            x += 360;

        y -= v2.y * smooth;
        if (y >= 360)
            y -= 360;
        else if (y <= 0)
            y += 360;

        v3.z = 0;
        v3.x = y;
        v3.y = x;
        transform.eulerAngles = v3;
        LastSmoothTime = 0;
        MyGameManagerInstance.SetState_TipUI(false);
    }

    private void Update()
    {
        if (MyGameManagerInstance.isCanSwipe)
        {
            LastSmoothTime += Time.deltaTime;
            if (LastSmoothTime >= 10f)
            {
                LastSmoothTime = 0;
                MyGameManagerInstance.SetState_AimedUI(true);
                MyGameManagerInstance.SetState_TipUI(true);
            }
        }
    }

    public List<GameObject> HideList = new List<GameObject>();
    public void Hide(bool isHide)
    {
        for (int i = 0; i < HideList.Count; i++)
        {
            if (HideList[i] != null)
            {
                HideList[i].SetActive(isHide);
            }
        }
    }
}
