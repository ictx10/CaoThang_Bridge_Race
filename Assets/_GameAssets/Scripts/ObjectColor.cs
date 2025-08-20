using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectColor : GameUnit
{
    public ColorType colorType; 

    [SerializeField] private Renderer renderer;
    [SerializeField] private ColorData colorData;

    public void ChangeColor(ColorType colorType)
    {
        this.colorType = colorType;
        renderer.material = colorData.GetColorMat(colorType);
    }

    public override void OnDespawn()
    {
    }

    public override void OnInit()
    {
    }
}
