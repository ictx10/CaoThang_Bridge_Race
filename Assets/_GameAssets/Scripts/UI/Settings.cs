using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : UICanvas
{
    public override void Open()
    {
        Time.timeScale = 0;
        base.Open();
    }
    public override void Close()
    {
        Time.timeScale = 1;
        base.Close();
    }
    public void ContinueBtn()
    {
        Close();
    }
    public void RetryBtn()
    {
        LevelManager.Instance.OnRetry();
        Close();
    }
}
