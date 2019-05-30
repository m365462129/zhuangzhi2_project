using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SomeSetting : MonoBehaviour {

    private void Awake()
    {
        Application.targetFrameRate = -1;
    }
}
