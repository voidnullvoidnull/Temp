using System;
using System.Linq;
using System.Text;
using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

/// <summary>
/// Класс осуществляющий загрузку и первичную обработку данных модели.
/// </summary>
public static class DataManager {


    public static Model LoadModel() {
        using (var fs = File.OpenText(Application.streamingAssetsPath + "/model.json")) {
            Model model = JsonConvert.DeserializeObject<Model>(fs.ReadToEnd());
            return model;
            }
        }

    public static GraphicRaw LoadMesh(string name) {
        using (var fs = File.OpenText(Application.streamingAssetsPath + "/" + name + ".obj")) {
            string text = fs.ReadToEnd();
            var lines = text.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            var vertString = lines.Where(c => c.StartsWith("v"));
            var quadString = lines.Where(c => c.StartsWith("f"));

            List<Vector3> points = new List<Vector3>();
            foreach (string s in vertString) {
                var values = s.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                float x = float.Parse(values[1]);
                float y = float.Parse(values[2]);
                float z = float.Parse(values[3]);
                points.Add(new Vector3(x, y, z));
                }

            List<int> quads = new List<int>();
            foreach (string s in quadString) {
                var values = s.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                int p0 = int.Parse(values[1])  -1;
                int p1 = int.Parse(values[2]) - 1;
                int p2 = int.Parse(values[3]) - 1;
                int p3 = int.Parse(values[4]) - 1;
                quads.AddRange(new[] { p0, p1, p2, p3 });
                }
            return new GraphicRaw { points = points.ToArray(), quads = quads.ToArray() };
            }
        }
    }


