using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ImageController : MonoBehaviour, IPointerEnterHandler
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        transform.parent.GetComponent<CanvasGroup>().alpha = 1;
        Debug.Log("Cursor Entering " + name + " GameObject");
        //GetComponentInParent<CanvasGroup>().alpha = 1;
    }
    /*
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        transform.parent.GetComponent<CanvasGroup>().alpha = 0;
        Debug.Log("Cursor Exiting " + name + " GameObject");
        //GetComponentInParent<CanvasGroup>().alpha = 0;
    }
    */
    // Update is called once per frame
    void Update()
    {
        
    }
}
