using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
//using UnityEngine.EventSystems;

[System.Serializable]
public class floatEvent : UnityEvent<float> { }

[System.Serializable]
public class floatTwoEvent : UnityEvent<float, float> { }

//[System.Serializable]
public class vector3Event : UnityEvent<Vector3> { }

[System.Serializable]
public class vector3TwoEvent : UnityEvent<Vector3, Vector3> { }

[System.Serializable]
public class vector3FourEvent : UnityEvent<Vector3, Vector3, Vector3, Vector3> { }

[System.Serializable]
public class UnityGameObjectEvent : UnityEvent<GameObject> { }

[System.Serializable]
public class vector3BoolEvent : UnityEvent<Vector3, Vector3, bool> { }


public class EventManager : MonoBehaviour
{
    
    //private Selection selection = new Selection();
    public GameObject unitController;
    private Selection unitControllerScript;
    private bool isThisFirstMouseClick = true;
    private bool shiftDown = false;
    public GameObject selectionBox;
    private Vector3 firstMouseDownPoint;
    private Vector3 lastMouseDownPoint;
    public Camera cam;
    public LayerMask layermask;

    public floatEvent HorizontalInput;
    public floatEvent VerticalInput;
    public floatEvent ZoomInput;
    public floatTwoEvent RotateInput;

    public vector3Event moveToMouseClick = new vector3Event();
    public vector3TwoEvent drawUISelectionBox;
    public vector3BoolEvent sendMeYourScreenPos;

    // Start is called before the first frame update
    private void Awake()
    {
        unitControllerScript = unitController.GetComponent<Selection>();
    }

    void Start()
    {
    }
    void Update()
    {

        if (Input.GetAxis("Horizontal") != 0)
        {
            HorizontalInput.Invoke(Input.GetAxis("Horizontal"));
        }
        if (Input.GetAxis("Vertical") != 0)
        {
            VerticalInput.Invoke(Input.GetAxis("Vertical"));
        }
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            ZoomInput.Invoke(Input.GetAxis("Mouse ScrollWheel"));
        }
        
        if (Input.GetMouseButton(2))
        {
            RotateInput.Invoke(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        }
        /*
        if (Input.GetKey("q"))
        {
            RotateInput.Invoke(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        }
        */
        if (Input.GetMouseButton(1) && moveToMouseClick != null)
        {
            moveToMouseClick.Invoke(calculateTerrainMousePos(Input.mousePosition));
        }

        if (Input.GetMouseButton(0))
        {
            if (isThisFirstMouseClick)
            {
                firstMouseDownPoint = Input.mousePosition;
                isThisFirstMouseClick = false;
            }
            drawUISelectionBox.Invoke(firstMouseDownPoint, Input.mousePosition);
        }
        if (Input.GetMouseButtonUp(0))
        {
            lastMouseDownPoint = Input.mousePosition;
            sendMeYourScreenPos.Invoke(firstMouseDownPoint, lastMouseDownPoint, shiftDown);
            isThisFirstMouseClick = true;
            firstMouseDownPoint = Vector3.zero;
            drawUISelectionBox.Invoke(firstMouseDownPoint, firstMouseDownPoint);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            //Debug.Log("shift down");
            shiftDown = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            //Debug.Log("shift up");
            shiftDown = false;
        }
    }
    Vector3 calculateTerrainMousePos(Vector3 screenPos)
    {

        RaycastHit hit;

        Ray ray = cam.ScreenPointToRay(screenPos);
        Vector3 terrainMousePosition = new Vector3();
        if (Physics.Raycast(ray.origin, ray.direction, out hit, 1000))
        {
            terrainMousePosition = hit.point;
        }
        return terrainMousePosition;
    }
}
