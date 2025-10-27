using Unity.VisualScripting;
using UnityEngine;

public class GamesLossScript : MonoBehaviour
{
    private void Start()
    {
        if (!Cursor.visible)
        {
            Cursor.visible = true;
        }
    }
}
