using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexRenderer
{
    public float halfsize;
    public Vector3 CenterPoint;
    public Vector3 LeftCorner, LeftUpConer, LeftLowConer;
    public Vector3 RightCorner, RightUpCorner, RightLowCorner;


    public HexRenderer(Vector3 CenterPoint, float halfsize)
    {
        this.CenterPoint = CenterPoint;
        this.halfsize = halfsize;

        LeftCorner = CenterPoint + new Vector3(-1, 0, 0) * halfsize;
        LeftUpConer = CenterPoint + new Vector3(-1, 0, 0) * halfsize * 0.5f + new Vector3(0, 0, 0.87f);
        LeftLowConer = CenterPoint + new Vector3(-1, 0, 0) * halfsize * 0.5f + new Vector3(0, 0, -0.87f);


        RightCorner = CenterPoint + new Vector3(1, 0, 0) * halfsize;
        RightUpCorner = CenterPoint + new Vector3(1, 0, 0) * halfsize * 0.5f + new Vector3(0, 0, 0.87f);
        RightLowCorner = CenterPoint + new Vector3(1, 0, 0) * halfsize * 0.5f + new Vector3(0, 0, -0.87f);


    }
}
