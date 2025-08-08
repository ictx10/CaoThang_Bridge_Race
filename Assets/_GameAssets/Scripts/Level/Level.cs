using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{

    /*
     T?o thêm m?t bi?n ?? tham chi?u ??n navmeshdata c?a level ??y
     */
    public Transform startPoint;
    public Transform finishPoint;
    public int botAmount;
    public Stage[] stages;

    public void OnInit()
    {
        for (int i = 0; i < stages.Length; i++)
        {
            stages[i].OnInit();
        }
    }
}
