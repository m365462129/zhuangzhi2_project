using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour {

    public float rotateX = 10f;
    public float rotateY = 10f;
    public float rotateZ = 10f;

    private float coefficient = 0;
    private void Start()
    {
        coefficient = Random.Range(0.3f,1f);//随机，每个的转速都不一样
        rotateX *=  coefficient;
        rotateY *= coefficient;
        rotateZ *= coefficient;
    }

    public void LateUpdate()
    {
        transform.Rotate(rotateX*Time.deltaTime, rotateY * Time.deltaTime, rotateZ * Time.deltaTime);
    }
}
