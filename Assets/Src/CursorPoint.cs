using UnityEngine;

public class CursorPoint : MonoBehaviour
{
    [SerializeField] private Texture2D cursorTexture;
    Vector2 cursorCenter = new Vector2(16, 16);
    private void Start() 
    {
        Cursor.SetCursor(cursorTexture, cursorCenter, CursorMode.Auto);
    }
}
