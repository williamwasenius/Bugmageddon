using UnityEngine;
using UnityEngine.UI;

public class CrosshairFollow : MonoBehaviour
{
    [SerializeField] private RectTransform crosshair;
    //[SerializeField] private RectTransform circle;

    void Start()
    {
        Cursor.visible = false; 
    }

    void Update()
    {
        Vector2 mousePos = Input.mousePosition;
        crosshair.position = mousePos;
       // circle.position = mousePos;
    }
}
