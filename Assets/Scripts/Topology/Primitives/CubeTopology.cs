using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class CubeTopology : VoidTopology  {

    public CubeTopology(float width, float height, Vector3 position, Vector3 rotation) {
        Create(width, height);
        SetTransform(position, rotation, Vector3.one);
    }

    public void Create(Vector3 b0, Vector3 b1, Vector3 b2, Vector3 b3, Vector3 t0, Vector3 t1, Vector3 t2, Vector3 t3) {
        List<Vector3> pnts = new List<Vector3> { b0,b1,b2,b3,t0,t1,t2,t3 };
        List<int> qds = new List<int> {
            0,1,2,3,
            0,1,5,4,
            4,5,6,7,
            7,6,2,3,
            0,4,7,3,
            1,5,6,2,
        };
        points = pnts.ToArray();
        quads = qds.ToArray();
    }

    public void Create(Vector3 down, Vector3 size) {
        Vector3 b0, b1, b2, b3, t0, t1, t2, t3;
        size=size/2;
        b0=new Vector3(down.x-size.x, down.y, down.z-size.z);
        b1=new Vector3(down.x-size.x, down.y, down.z+size.z);
        b2=new Vector3(down.x+size.x, down.y, down.z+size.z);
        b3=new Vector3(down.x+size.x, down.y, down.z-size.z);

        t0=new Vector3(down.x-size.x, down.y+size.y, down.z-size.z);
        t1=new Vector3(down.x-size.x, down.y+size.y, down.z+size.z);
        t2=new Vector3(down.x+size.x, down.y+size.y, down.z+size.z);
        t3=new Vector3(down.x+size.x, down.y+size.y, down.z-size.z);

        Create(b0, b1, b2, b3, t0, t1, t2, t3);

    }

    public void Create(float width, float height) {
        Vector3 size = new Vector3(width, height, width);
        Create(Vector3.zero, size);
    }

}
