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
        TargetRotation = transform.localEulerAngles;
        MyGameManagerInstance = MyGameManager.Instance;
    }

    public float smooth = 5f;

    private Vector3 TargetRotation;

    public void SetTargetRotation(bool isLeft)
    {
        if (MyGameManagerInstance.IsChaZhiHuaDong())
        {
            if (isLeft)
            {
                TargetRotation.y = TargetRotation.y + 90;
            }
            else
            {
                TargetRotation.y = TargetRotation.y - 90;
            }
        }
    }


    float x;
    float y;
    public float yMinLimit = -50;
    public float yMaxLimit = 50;

    float LastSmoothTime = 0;
    void LateUpdate()
    {
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
                x = transform.eulerAngles.y;
                y = transform.eulerAngles.x;
            }

            if (Input.GetMouseButton(0))
            {
                x -= Input.GetAxis("Mouse X") * smooth;
                y -= Input.GetAxis("Mouse Y") * smooth;
                y = ClampAngle(y, yMinLimit,yMaxLimit);
                Vector3 v3 = transform.eulerAngles;
                v3.x = y;
                v3.y = x;
                transform.eulerAngles = v3;
                LastSmoothTime = 0;
                MyGameManager.Instance.SetState_TipUI(false);
            }
            
        }
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


    float ClampAngle(float angle)
    {
        if (angle > 180f)
            angle -= 360f;

        return angle;
    }

    float ClampAngle(float angle, float min, float max)
    {
        if (angle > 180f)
            angle -= 360f;

        return Mathf.Clamp(angle, min, max);
    }


}
