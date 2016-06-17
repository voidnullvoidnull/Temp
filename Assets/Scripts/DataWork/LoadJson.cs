using System;
using System.Text;
using System.IO;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class LoadJson : MonoBehaviour {


    public Model model;

    public static void CreateModel()
    {
        Model model = new Model();
        model.modelName = "Sample";

        float prevX = 0;
        float prevY = 0;
        float prevZ = 0;

        for (int i = 0; i < 100; i++)
        {         
            Pipe pipe = new Pipe(model.modelObjects.Count);
            pipe.coordinates.X = prevX;
            pipe.coordinates.Y = prevY;
            pipe.coordinates.Z = prevZ;

            prevX = pipe.coordinates.endX = Random.Range(-30, 30);
            prevY = pipe.coordinates.endY = Random.Range(-30, 30);
            prevZ = pipe.coordinates.endZ = Random.Range(-30, 30);

            pipe.properties[0].value = 1.ToString();
            model.modelObjects.Add(pipe);
        }

        GameObject.FindObjectOfType<LoadJson>().model = model;

        string jsonString = JsonUtility.ToJson(model, true);

        using (StreamWriter writer = File.CreateText(System.IO.Path.Combine(Application.streamingAssetsPath, "model.json")))
        {
            writer.Write(jsonString);
            writer.Close();
        }
    }

    public static void LoadModel()
    {
        string fromJson = File.ReadAllText(System.IO.Path.Combine(Application.streamingAssetsPath, "model.json"));
        Model loadedModel = JsonUtility.FromJson<Model>(fromJson);
        GameObject.FindObjectOfType<LoadJson>().model = loadedModel;
    }

}
