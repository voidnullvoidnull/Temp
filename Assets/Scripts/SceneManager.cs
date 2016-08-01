using UnityEngine;
using System.Collections;


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
        model = DataManager.LoadModel();
        model.GenerateObjects();
        render.StartRender();
        }
    }
