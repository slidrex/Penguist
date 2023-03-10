using UnityEngine;

public static class PenguiFunctions
{
    public static Vector2 GetMousePosition() => Camera.main.ScreenToWorldPoint(Input.mousePosition);
}
