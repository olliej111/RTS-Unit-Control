using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionBox : MonoBehaviour
{
    private Vector3 midPoint;
    public Texture boxTexture;
    private RectTransform SelectionBoxRect;

    // Start is called before the first frame update
    void Start()
    {
        SelectionBoxRect = GetComponent<RectTransform>();
        DrawBox(new Vector3(0, 0, 0), new Vector3(0, 0, 0));
    }

    public void DrawBox(Vector3 firstMouseDownPoint, Vector3 mousePos)
    {
        //Debug.Log("start point " + firstMouseDownPoint + " last point " + mousePos);
        var vertSize = Mathf.Abs(mousePos.x - firstMouseDownPoint.x);
        var horSize = Mathf.Abs(mousePos.y - firstMouseDownPoint.y);
        midPoint = new Vector2(firstMouseDownPoint.x + ((mousePos.x - firstMouseDownPoint.x) / 2), firstMouseDownPoint.y + ((mousePos.y - firstMouseDownPoint.y) / 2));
        //Debug.Log("mid point " + midPoint);
        SelectionBoxRect.anchoredPosition = midPoint;
        SelectionBoxRect.sizeDelta = new Vector2(vertSize, horSize);
        //Debug.Log("size of box " + (new Vector2(vertSize, horSize)));
    }
}
