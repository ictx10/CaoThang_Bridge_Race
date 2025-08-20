using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory : UICanvas
{
    public void RetryBtn()
    {
        LevelManager.Instance.OnRetry();
        Close();
    }
    public void NextBtn()
    {
        LevelManager.Instance.OnNextLevel();
        Close();
    }
}
