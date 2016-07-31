using UnityEngine;
using System.Collections;
using System;

public class TopologyCube : MonoBehaviour, IRenderObject {


    public Vector3 p0, p1, p2, p3, p4, p5, p6, p7;

    public VoidTopology topology;

    void Start() {
        Recalculate();
        VoidRender render = FindObjectOfType<VoidRender>();
        render.layers[0].Register(ref topology);
    }

    public void Recalculate() {
        var top1 = PrimitiveCube.GetTopology(transform.localToWorldMatrix,p0, p1, p2, p3, p4, p5, p6, p7);
        Vector3 shift = new Vector3(2, 2, 2);
        var top2 = PrimitiveCube.GetTopology(transform.localToWorldMatrix, p0 +shift, p1+shift, p2+shift, p3+shift, p4+shift, p5+shift, p6+shift, p7+shift);
        topology = top1+top2;
    }
}
