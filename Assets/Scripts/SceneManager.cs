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
/// Менеджер сцены, осуществляющий инициацию загрузки объектов, контролирующий список загруженных объектов и их отражений в геометрической модели
/// </summary>
[RequireComponent(typeof(VoidRender))]
public class SceneManager : MonoBehaviour {

    /// <summary>
    /// Текущий модуль рендеринга
    /// </summary>
    public VoidRender render;
    /// <summary>
    /// Текущая загруженная модель
    /// </summary>
    public Model model;
    /// <summary>
    /// Активный экземпляр менеджера сцены
    /// </summary>
    public static SceneManager sharedInstance;

    public string json;

    void Start() {
        sharedInstance = this;
        render = GetComponent<VoidRender>();
        ModelObjectBase.parent = this.gameObject;
        ModelObjectBase.manager = this;
        }

    /// <summary>
    /// Инициирует загрузку модели и начало распознавания объектов
    /// </summary>
    public void LoadModel() {
        StartCoroutine(Load());
        }

    public IEnumerator Load() {
        // WWW www = new WWW("https://raw.githubusercontent.com/voidnullvoidnull/Temp/master/Assets/StreamingAssets/model.JSON");
        // yield return www;

        model = JsonConvert.DeserializeObject<Model>(json);
        model.GenerateObjects();
        render.StartRender();
        yield return new WaitForEndOfFrame();
    }

    public IEnumerator LoadModel(string name, Action<GraphicRaw> callback) {

        WWW www = new WWW("www.voidnull.ru/data/"+name+".obj");
        yield return www;
        var mesh = ParseMesh(www.text);
        callback(mesh);
    }

    public static GraphicRaw ParseMesh(string data) {
        var lines = data.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
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
            int p0 = int.Parse(values[1])-1;
            int p1 = int.Parse(values[2])-1;
            int p2 = int.Parse(values[3])-1;
            int p3 = int.Parse(values[4])-1;
            quads.AddRange(new[] { p0, p1, p2, p3 });
        }
        return new GraphicRaw { points=points.ToArray(), quads=quads.ToArray() };
    }


}
