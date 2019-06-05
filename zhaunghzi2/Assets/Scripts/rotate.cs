using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour {
    public bool IsRange = true;
    public bool IsOnlyY = false;
    public float rotateX = 2f;
    public float rotateY = 2f;
    public float rotateZ = 2f;

    private float coefficient = 0;
    private void Start()
    {
        if (IsRange)
        {
            coefficient = Random.Range(0.3f, 1f);//随机，每个的转速都不一样
            rotateX *= coefficient;
            rotateY *= coefficient;
            rotateZ *= coefficient;
        }
    }

    public void LateUpdate()
    {
        if (IsOnlyY)
        {
            Vector3 v3 = transform.localEulerAngles;
            v3.y += rotateY * Time.deltaTime;
            transform.localEulerAngles = v3;
        }
        else
        {
            transform.Rotate(rotateX * Time.deltaTime, rotateY * Time.deltaTime, rotateZ * Time.deltaTime);
        }
        
    }
}
