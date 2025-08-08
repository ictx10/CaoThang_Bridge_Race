using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectColor : MonoBehaviour
{
    public ColorType colorType; 

    [SerializeField] private MeshRenderer renderer;
    [SerializeField] private ColorData colorData;

    public void ChangeColor(ColorType colorType)
    {
        this.colorType = colorType;
        renderer.material = colorData.GetColorMat(colorType);
    }

}
