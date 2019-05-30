using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    public static CameraManager Instance;
    private void Awake()
    {
        Instance = this;
        SetTarget(new Vector3(-1.26f,15.9f,-62.2f),new Vector3 (16.4f,0f,0f));
    }

    public float smooth = 10;
    public Vector3 TargetPosition;
    public Vector3 TargetRotation;

    public void SetTarget(Vector3 _TargetPosition, Vector3 _TargetRotation)
    {
        TargetPosition = _TargetPosition;
        TargetRotation = _TargetRotation;
    }

    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SetTarget(new Vector3(-3.5f, 12, -61f), new Vector3(5.7f, 12f, 0));
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            SetTarget(new Vector3(-16f, 15f, -27f), new Vector3(13f, 47f, 0));
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            SetTarget(new Vector3(-20f, 16f, -47f), new Vector3(0, 10f, 0));
        }

        transform.position = Vector3.Lerp(transform.position, TargetPosition, Time.deltaTime * smooth);
        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, TargetRotation, Time.deltaTime * smooth);
    }

}
