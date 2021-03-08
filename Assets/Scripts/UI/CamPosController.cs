using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamPosController : MonoBehaviour
{
    public Camera cam;
    public float camSpeed;
    public float zoomSpeed;

    public float rotSpeed;
    float pitch = 90.0f;
    float yaw = 0.0f;

    //private float pitch = 0.0f;
    //private float yaw = 0.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void MoveCameraHor(float input)
    {
        transform.position += transform.right * input * Time.deltaTime * camSpeed;
        //Debug.Log("moving the camera hor by "+input);
    }
    
    public void MoveCameraVer(float input)
    {
        transform.position += transform.forward * input * Time.deltaTime * camSpeed;
        //Debug.Log("moving the camera vert by " + input);
    }
    
    public void ZoomCamera(float input)
    {
        transform.position += transform.forward * input * Time.deltaTime * zoomSpeed;
        //Debug.Log("zooming the camera by"+ input);
    }
    public void RotateCamera(float mouseX, float mouseY)
    {
        //Debug.Log("mouse pitch " + mouseY + " mouse yaw " + mouseX);
        pitch = Mathf.Clamp(pitch -= rotSpeed * mouseY, 0f, 90f);
        yaw += rotSpeed * mouseX;
        //Debug.Log("stored pitch " + pitch + " yaw " + yaw);
        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }
}
