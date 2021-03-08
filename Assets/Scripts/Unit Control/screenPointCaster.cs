using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class screenPointCaster : MonoBehaviour
{
    private Camera cam;
    private GameObject eventManagerObject;
    private EventManager eventManagerScript;
    private movementController movementControllerScript;
    public Transform selectedBox;
    private Transform _mSelectedBox;
    private bool amSelected;
    public float clickDurationLimits;
    public LayerMask layerMask;
    //public float sizeOfBox;
    private void Awake()
    {
        cam = Camera.main;
        eventManagerObject = GameObject.FindGameObjectWithTag("EventManager");
        eventManagerScript = eventManagerObject.GetComponent<EventManager>();
        eventManagerScript.sendMeYourScreenPos.AddListener(sendScreenPos);
        movementControllerScript = GetComponent<movementController>();
    }
    public void sendScreenPos(Vector3 firstMouse, Vector3 releaseMouse, bool shiftDown)
    {
        bool imInTheBox;
        //Vector3 oneclickbox = Vector3.one;
        float lengthOfClick = Mathf.Abs(Vector3.Magnitude(firstMouse - releaseMouse));
        //Debug.Log("lengthOfClick " + lengthOfClick);
        if (lengthOfClick <= clickDurationLimits)
        {
            //Debug.Log("oneclick");
            //Debug.Log("the mouse pos " + firstMouse);
            Vector3 oneClickBoxLowerBounds = firstMouse - new Vector3(10, 10, 0);
            Vector3 oneClickBoxUpperBounds = firstMouse + new Vector3(10, 10, 0);
            //Debug.Log("lowerBounds " + oneClickBoxLowerBounds + " upperbounds " + oneClickBoxUpperBounds);
            imInTheBox = whatsInTheBox(oneClickBoxLowerBounds, oneClickBoxUpperBounds);
            if (imInTheBox)
            {
                
                //Debug.Log("im in the box");
                if (shiftDown)
                {
                    if (amSelected)
                    {
                        //Debug.Log("shift is down, im already selected");
                        deSelected();
                    }
                    else
                    {
                        //Debug.Log("shift is down, im not already selected");
                        selected();
                    }
                    return;
                }
                
                selected();
            }
            else
            {
                if (!shiftDown)
                {
                    deSelected();
                }
                else
                {
                    return;
                }

            }
            return;
        }
        else
        {
            //Debug.Log("drawing a box");
            imInTheBox = whatsInTheBox(firstMouse, releaseMouse);
            //Debug.Log("iminthebox now equals " + imInTheBox);
            if (!imInTheBox)
            {
                if (shiftDown)
                {
                }
                else
                {
                    deSelected();
                }
            }
            else
            {
                //Debug.Log("so I am being selected");
                selected();
            }
        }

    }
    private void selected()
    {
        eventManagerScript.moveToMouseClick.AddListener(movementControllerScript.setDestination);
        amSelected = true;
        if (_mSelectedBox == null)
        {
            _mSelectedBox = Instantiate(selectedBox, transform);
        }
    }
    private void deSelected()
    {
        eventManagerScript.moveToMouseClick.RemoveListener(movementControllerScript.setDestination);
        amSelected = false;
        if (_mSelectedBox != null)
        {
            DestroyImmediate(_mSelectedBox.gameObject);
        }
    }
    private bool whatsInTheBox(Vector3 firstMouse, Vector3 releaseMouse)
    {
        //Debug.Log("the bounds passed to me " + firstMouse + releaseMouse);
        //Debug.Log("Sending screen pos " + cam.WorldToScreenPoint(transform.position));
        Vector3 unitScreenPoint = cam.WorldToScreenPoint(transform.position);
        //Debug.Log("i am located " + unitScreenPoint);
        Vector3 absBoxSize = new Vector3();
        Vector3 midPoint = Vector3.Lerp(firstMouse, releaseMouse, 0.5f);
        absBoxSize.x = Mathf.Abs(releaseMouse.x - firstMouse.x);
        absBoxSize.y = Mathf.Abs(releaseMouse.y - firstMouse.y);
        Vector3 boxSizeUpperDisplaced = midPoint + absBoxSize / 2;
        Vector3 boxSizeLowerDisplaced = midPoint - absBoxSize / 2;
        Vector3 absUpperUnitLoc = unitScreenPoint + midPoint;
        if (unitScreenPoint.x >= boxSizeUpperDisplaced.x || unitScreenPoint.y >= boxSizeUpperDisplaced.y
                    || unitScreenPoint.x <= boxSizeLowerDisplaced.x || unitScreenPoint.y <= boxSizeLowerDisplaced.y)
        {
            //Debug.Log("im not in the box");
            return false;
        }
        else
        {
            //Debug.Log("i am in the box");
            return true;
        }
    }
}

