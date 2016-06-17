using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Vectrosity;

public class GravGun : MonoBehaviour {

    public Material material;
    public float step = 0.1f;
    public int stepCount = 100;
    public float scale = 0.5f;

    List<Vector3> path = new List<Vector3>();
    

	void Start () {
        Vector3 pos = transform.position;

        for (int i = 0; i < stepCount; i++)
        {
            pos +=(transform.forward * step * i);

            Ray ray = new Ray(pos, Vector3.down);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                pos += GravityManager.GetPotential(pos, 1, hit.point, 1f) * scale;
                
            }
            ray = new Ray(pos, Vector3.up);
            if (Physics.Raycast(ray, out hit))
            {
                pos += GravityManager.GetPotential(pos, 1, hit.point, 1f) * scale;
            }

            path.Add(pos);
        }

        VectorLine line = new VectorLine("path", new Vector3[501], material, 2, LineType.Continuous);
        line.MakeSpline(path.ToArray(), 500, false);
        line.Draw3DAuto();
	}
}
