using UnityEngine;
using System.Collections;
using System;

public class Topology : ModelObjectBase {

    int id;
    public string name;
    public string displayType;
    public string modelName;
    public Vector3 position;
    public Vector3 rotation;
    public Vector3 size;
    public GraphicRaw topology;

    public static void CreateObject(ModelObject obj) {

        GameObject go = new GameObject(obj.id.ToString());
        Topology custom = go.AddComponent<Topology>();
        custom.id = obj.id;
        custom.name = obj.name;
        custom.position = Point3D.FromJson(obj.values["position"].ToString()).GetVector();
        custom.rotation = Point3D.FromJson(obj.values["rotation"].ToString()).GetVector();
        custom.size = Point3D.FromJson(obj.values["size"].ToString()).GetVector();
        custom.displayType = obj.values["displayType"].ToString();
        custom.modelName = obj.values["meshName"].ToString();
        go.transform.SetParent(parent.transform);
        }

    void Start() {
        Recalculate();
        }

    public void Recalculate() {
        StartCoroutine(SceneManager.sharedInstance.LoadModel(modelName, Callback));
        }

    public void Callback(GraphicRaw mesh) {
        topology=mesh;
        topology.SetTransform(position, rotation, size);
        VoidRender.sharedInstance.Register("seams", topology);
    }

    }
