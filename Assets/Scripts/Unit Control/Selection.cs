using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Selection : MonoBehaviour
{
    public UnityEvent sendMeYourScreenPos;
    public GameObject eventManagerGameObject;
    private EventManager eventManagerScript;
    public GameObject unitController;
    private void Awake()
    {
        eventManagerScript = eventManagerGameObject.GetComponent<EventManager>();
    }
    public void addMeAsListener(GameObject unit)
    {
        //Debug.Log("adding listener " + unit);
        eventManagerScript.moveToMouseClick.AddListener(unit.GetComponent<movementController>().setDestination);
    }
    public void removeMeAsListener(GameObject unit)
    {
        eventManagerScript.moveToMouseClick.RemoveListener(unit.GetComponent<movementController>().setDestination);
    }
}
