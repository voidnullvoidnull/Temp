using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Vectrosity;

public class LineRender : MonoBehaviour {

    public Material lineMaterial;
    Transform targ;
    Mesh selected;
    Box box;

    VectorLine selectedWire;
    
    public void OnPost(Camera cam)
    {
        if (selected!= null)
            {
            selectedWire.Draw3D();
            }
    }

    public void SetTarget(Transform target)
    {
        selected = target.GetComponent<MeshFilter>().sharedMesh;
        targ = target;
        selectedWire.matrix = targ.localToWorldMatrix;
        selectedWire.MakeWireframe(selected);

    }

    void OnEnable()
    {
        selectedWire = new VectorLine("Wire", new Vector3[] {}, lineMaterial, 2.5f, LineType.Discrete);
        selectedWire.drawDepth = 9999;
        selectedWire.maxWeldDistance = 100f;
        selectedWire.capLength = 100f;
        Camera.onPostRender += OnPost;
    }

    void OnDisable()
    {
        Camera.onPostRender -= OnPost;
    }
}



public class Box
{
    public Vector3[] points = new Vector3[8];
    public int[] edges = new int[24];

    public Box(Bounds bounds)
    {
        points[0] = new Vector3(bounds.min.x, bounds.min.y, bounds.min.z);
        points[1] = new Vector3(bounds.min.x, bounds.max.y, bounds.min.z);
        points[2] = new Vector3(bounds.max.x, bounds.max.y, bounds.min.z);
        points[3] = new Vector3(bounds.max.x, bounds.min.y, bounds.min.z);

        points[4] = new Vector3(bounds.min.x, bounds.min.y, bounds.max.z);
        points[5] = new Vector3(bounds.min.x, bounds.max.y, bounds.max.z);
        points[6] = new Vector3(bounds.max.x, bounds.max.y, bounds.max.z);
        points[7] = new Vector3(bounds.max.x, bounds.min.y, bounds.max.z);

        edges = new int[]
        {
            0,1,1,2,2,3,3,0,
            4,5,5,6,6,7,7,4,
            0,4,1,5,2,6,3,7
        };
    }
}
