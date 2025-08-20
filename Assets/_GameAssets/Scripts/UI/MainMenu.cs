using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : UICanvas
{
    public void PlayBtn()
    {
        LevelManager.Instance.OnStartGame();

        UIManager.Instance.OpenUI<Gameplay>();
        Close();
    }
}
