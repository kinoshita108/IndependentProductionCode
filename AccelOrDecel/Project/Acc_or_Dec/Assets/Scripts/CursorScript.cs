using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorScript : MonoBehaviour
{
    public Texture2D AimCursor;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.SetCursor(AimCursor, new Vector2(55, 55), CursorMode.Auto);
    }
}
