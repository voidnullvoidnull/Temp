using System.Linq;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class VoidRender : MonoBehaviour {

    public static VoidRender sharedInstance;
    public List<VoidLayer> layers = new List<VoidLayer>();
    public bool isRunning = false;

    public void StartRender(){
        if (!isRunning)
            Camera.onPostRender += Draw;
        else{
            GL.Flush();
            Camera.onPostRender -= Draw;
        }
        isRunning = !isRunning;
    }

    public void Register(string layer, GraphicRaw subject){
        VoidLayer l = layers.First(c => c.name==layer);
        l.Register(subject);
    }

    public void Remove(string layer, GraphicRaw subject){
        VoidLayer l = layers.First(c => c.name==layer);
        l.UnRegister(subject);
    }

    void Awake(){
        sharedInstance = this;
    }

    void Draw(Camera cam){
        GL.PushMatrix();
        foreach (VoidLayer layer in layers){
            layer.material.SetPass(0);
            GL.Begin(GL.QUADS);

            foreach (GraphicRaw obj in layer.layerObjects) 
                { 
                foreach(int p in obj.quads) 
                    {
                    GL.Vertex(obj.points[p]);
                    }
            }
            GL.End();
        }
        GL.PopMatrix();
    }

    void OnDisable(){
        if (isRunning){
            GL.Flush();
            Camera.onPostRender -= Draw;
        }
    }
}
