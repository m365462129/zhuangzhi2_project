using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WholeRotateManager : MonoBehaviour
{
    public static WholeRotateManager Instance;
    private void Awake()
    {
        Instance = this;
        TargetRotation = transform.localEulerAngles;
    }

    public float smooth = 5f;
    private Vector3 TargetRotation;

    public void SetTargetRotation()
    {
        TargetRotation.y = TargetRotation.y + 90;
    }

    void LateUpdate()
    {
        //transform.position = Vector3.Lerp(transform.position, TargetPosition, Time.deltaTime * smooth);
        //Vector3 vvv = Vector3.zero;
        //transform.localEulerAngles = Vector3.SmoothDamp(transform.localEulerAngles, TargetRotation,ref vvv, Time.deltaTime * smooth);

        Vector3 v = Vector3.Lerp(transform.localEulerAngles, TargetRotation, Time.deltaTime * smooth);
        transform.localEulerAngles = v;
        if (v.y >= 360)
        {
            v.y = v.y - 360;
            transform.localEulerAngles = v;
            TargetRotation.y = TargetRotation.y - 360;
        }
    }
}
