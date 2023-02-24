using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DragCamera : MonoBehaviour
{
    public static DragCamera instance;

    public bool isDragSomething = false;
    public float dragSpeed;
    Vector3 lastMousePosition;
    private Camera cam;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        // is on stageXX
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName.Length < 6)
            return;
        string sceneName5 = sceneName.Substring(0, 5);
        if (sceneName5 != "Stage")
            return;

        if (!isDragSomething)
            MouseInputs();
    }

    void MouseInputs()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 delta = Input.mousePosition - lastMousePosition;

            MoveCamera(delta.x, delta.y);

            lastMousePosition = Input.mousePosition;
        }
    }

    void MoveCamera(float x, float y)
    {
        Vector3 pos = cam.gameObject.transform.position;
        pos.x -= x * dragSpeed * Time.deltaTime;
        pos.y -= y * dragSpeed * Time.deltaTime;
        cam.transform.position = pos;
    }

    // scene load complete
    void sceneLoaded(Scene scene, LoadSceneMode mode)
    {
        cam = Camera.main;
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += sceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= sceneLoaded;
    }
}
