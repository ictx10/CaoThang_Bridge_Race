using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay : UICanvas
{
    public void SettingBtn()
    {
        UIManager.Instance.OpenUI<Settings>();  
    }

}
