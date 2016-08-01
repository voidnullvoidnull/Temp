using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

/// <summary>
/// Контрлирует уращение и панорамирование камеры,
/// </summary>
public class CameraRotator : MonoBehaviour {

    public Camera cam;
    public float speed = 5;
    public float smoothSpeed = 10;
    public Transform pivot;
    float maxVertAngle = 70;
    float angleHor = 45;
    float angleVert = 20;
    float scale = 10;
    Vector3 pos = new Vector3();
    Quaternion rotation = new Quaternion();

    void Start() {
        cam = GetComponent<Camera>();
        cam.orthographicSize = scale;
        }

    void Update() {
        scale -= Input.GetAxis("Mouse ScrollWheel") * speed;
        scale = Mathf.Clamp(scale, 3, 100);

        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject()) {
            pos += transform.right * Input.GetAxis("Mouse X");
            pos += transform.up * Input.GetAxis("Mouse Y");
            }
        else if (Input.GetMouseButton(1) && !EventSystem.current.IsPointerOverGameObject()) {
            angleHor -= Input.GetAxis("Mouse X") * speed;
            angleVert += Input.GetAxis("Mouse Y") * speed;
            angleVert = Mathf.Clamp(angleVert, -maxVertAngle, maxVertAngle);
            }
        }

    void LateUpdate() {
        cam.orthographicSize = scale;
        rotation = Quaternion.Euler(angleVert, angleHor, 0);
        pivot.rotation = Quaternion.Lerp(pivot.rotation, rotation, Time.deltaTime * smoothSpeed);
        pivot.position = Vector3.Lerp(pivot.position, pos, Time.deltaTime * smoothSpeed);
        }

    public void SetPivot(Vector3 point) {
        pos = point;
        }
    }
