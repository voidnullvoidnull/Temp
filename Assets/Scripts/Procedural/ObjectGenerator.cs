using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using Vectrosity;

public class ObjectGenerator : MonoBehaviour {

    public LoadJson loader;
    LineManager lineManager;
    List<VectorLine> lines = new List<VectorLine>();

    public Material defaultMaterial;
    public Material lineMaterial;

    public int segmentation = 16;
    public int width = 3;

    int progress = 0;
    bool loading = false;


    void Update()
    {
        if (loading)
        {
            if (progress == loader.model.modelObjects.Count)
            {
                loading = false;
                progress = 0;
            }
            else
            {
                StartCoroutine(Generate());
            }
        }
    }

    IEnumerator Generate()
    {
     JsonObject element = loader.model.modelObjects[progress];
         GameObject go = new GameObject(element.name + element.ID.ToString());
         go.transform.SetParent(transform);
         ParseType(go, element);
     progress++;
     yield return null;
    }

    public void ParseType(GameObject go, JsonObject json)
    {
        string type = json.objectType;
        

        switch (type)
        {
            case "Pipe":
                go.transform.position = json.coordinates.StartPos();
                go.transform.rotation = Quaternion.LookRotation(json.coordinates.EndPos() - go.transform.position);
                go.isStatic = true;
                MeshRenderer rend = go.AddComponent<MeshRenderer>();
                rend.sharedMaterial = defaultMaterial;

                MeshFilter filter = go.AddComponent<MeshFilter>();
                filter.sharedMesh = MeshTools.PipeWithoutRotation((json.coordinates.EndPos()-go.transform.position).magnitude, float.Parse(json.properties[0].value), segmentation);
                go.AddComponent<MeshCollider>();

                VectorLine start = new VectorLine("startCircle", new Vector3[segmentation+1], lineMaterial, width, LineType.Continuous);
                lines.Add(start);
                start.matrix = go.transform.localToWorldMatrix;
                start.MakeCircle(Vector3.zero, float.Parse(json.properties[0].value), segmentation);
                start.Draw3DAuto();

                VectorLine end = new VectorLine("endCircle", new Vector3[segmentation+1], lineMaterial, width, LineType.Continuous);
                lines.Add(end);
                end.matrix = go.transform.localToWorldMatrix;
                end.MakeCircle(new Vector3(0,0, (json.coordinates.EndPos() - go.transform.position).magnitude), float.Parse(json.properties[0].value), segmentation);
                end.Draw3DAuto();

                VectorLine line = new VectorLine("endCircle", new Vector3[] { Vector3.zero, new Vector3(0, 0, (json.coordinates.EndPos() - go.transform.position).magnitude)}, lineMaterial, width, LineType.Discrete);
                lines.Add(line);
                line.matrix = go.transform.localToWorldMatrix;
                line.Draw3DAuto();
                break;

            case "Manometr":

                go.transform.position = json.coordinates.StartPos();

                break;

            case "JsonObject":

                go.transform.position = json.coordinates.StartPos();

                break;

            default: break;
        }
    }

    void Clear()
    {
        lineManager = FindObjectOfType<LineManager>();

        for (int l = 0; l < lines.Count; l++)
        {
            lineManager.DisableLine(lines[l], 0);
        }
        lines.Clear();

        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    void OnGUI()
    {
        Rect load = new Rect(10, 10, 300, 50);
        if (GUI.Button(load, "LOAD") && !loading)
        {
            Clear();
            LoadJson.CreateModel();
            //LoadJson.LoadModel();
            StartCoroutine(Generate());
            loading = true;
        }
    }
}
