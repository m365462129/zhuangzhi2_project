using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
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

    private void OnEnable()
    {
        al = 1f;
        canvasGroup.alpha = al;
        isAdd = false;
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
