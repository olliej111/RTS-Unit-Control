using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/* The content in this script is not used any more. 
The add listener functions are carried out in the script attached to the gameobject itself.
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
*/
