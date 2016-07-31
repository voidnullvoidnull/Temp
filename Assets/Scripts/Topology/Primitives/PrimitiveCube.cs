using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class PrimitiveCube  {

    public static VoidTopology GetTopology(Matrix4x4 matrix,Vector3 b0, Vector3 b1, Vector3 b2, Vector3 b3, Vector3 t0, Vector3 t1, Vector3 t2, Vector3 t3) {
        List<Vector3> points = new List<Vector3> { b0,b1,b2,b3,t0,t1,t2,t3 };
        List<int> quads = new List<int> {
            0,1,2,3,
            0,1,5,4,
            4,5,6,7,
            7,6,2,3,
            0,4,7,3,
            1,5,6,2,
        };

        return new VoidTopology { points=points.ToArray(), quads=quads.ToArray(), matrix=matrix };
    }


}
