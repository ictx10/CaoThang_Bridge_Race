using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : ObjectColor
{
    [HideInInspector] public Stage stage;
    public void OnDespawm()
    {
        stage.RemoveBrick(this);
    }

}
