using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fail : UICanvas
{
    public void RetryBtn()
    {
        LevelManager.Instance.OnRetry();
        Close();
    }
}
