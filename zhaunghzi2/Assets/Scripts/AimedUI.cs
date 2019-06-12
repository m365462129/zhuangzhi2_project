using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimedUI : MonoBehaviour
{
    CanvasGroup canvasGroup;
    float al = 0;
    bool isAdd = false;
    public float Speed = 1f;
    private void Awake()
    {
        canvasGroup = this.GetComponent<CanvasGroup>();
        al = canvasGroup.alpha;
    }

    void Update()
    {
        if (isAdd)
        {
            al += Time.deltaTime * Speed;
            if (al >= 1f) isAdd = false;
        }
        else
        {
            al -= Time.deltaTime * Speed;
            if (al <= 0f) isAdd = true;
        }
        canvasGroup.alpha = al;
    }
}
