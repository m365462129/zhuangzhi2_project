using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{

    private void OnCollisionEnter(Collision hit)
    {
        //Debug.LogError("===="+ hit.transform.name);
        if (hit.transform.tag.Equals("Target"))
        {
            int tmpIndex = -1;
            int.TryParse(hit.transform.name, out tmpIndex);
            if (tmpIndex >= 0 && tmpIndex <= (int)WindowType.UI_Max)
            {
                MyGameManager.Instance.LoadMoive(tmpIndex);
            }
        }
    }

}
