using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class VoidRender : MonoBehaviour {

    public bool isRunning = false;
    public List<VoidLayer> layers = new List<VoidLayer>();

    public void Draw(Camera cam) {
        foreach (VoidLayer layer in layers) {
            if (layer.isVisible) {
                layer.material.SetPass(0);

                foreach (VoidTopology obj in layer.objects) {
                    GL.PushMatrix();
                    //GL.MultMatrix(obj.matrix);
                    GL.Begin(GL.QUADS);
                    foreach (int p in obj.quads) {
                        GL.Vertex(obj.points[p]);
                    }
                    GL.End();
                    GL.PopMatrix();
                }
            }
        }
    }

    public void BeginDraw() {
        isRunning=!isRunning;

        if (isRunning) {
            GL.Flush();
            Camera.onPostRender += Draw;
        }
        else {
            GL.Flush();
            Camera.onPostRender -= Draw;
        }
    }

    public void OnDisable() {
        GL.Flush();
        Camera.onPostRender-=Draw;
    }

    public void OnDestroy() {
        GL.Flush();
        Camera.onPostRender-=Draw;
    }
}
