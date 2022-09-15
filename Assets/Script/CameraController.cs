using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraController : MonoBehaviour
{
    public float maxZoom = 10f;
    public float minZoom = 5f;
    public float currentZoom = 7f;
    public float ZoomSensitive = 2f;
    public Transform target;
    
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        float scroll = Input.GetAxisRaw("Mouse ScrollWheel") * ZoomSensitive;
        if (scroll != 0)
        {
            currentZoom = Mathf.Clamp(currentZoom - scroll, minZoom, maxZoom);
        }
        transform.position = new Vector3(target.position[0], 0, -10);
        GetComponent<Camera>().orthographicSize = currentZoom;
    }
}
