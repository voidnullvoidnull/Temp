using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {
    public VoidTopology topology = new VoidTopology();

    void Start() {
        BandTopology band = new BandTopology(1,2, Vector3.zero, new Vector3(0,1,5), Vector3.zero, Vector3.zero, 8);
        band.AddCircle(2, new Vector3(0, 0, 10), new Vector3(0,30,0));
        band.AddCircle(1, new Vector3(1, 3, 15), new Vector3(0, 45, 0));
        topology=band;
        FindObjectOfType<VoidRender>().layers[0].Register(ref topology);
    }
}
