using UnityEngine;

public static class InputHelper
{
    // Returns -1, 0 or 1 for left/right (A/D or arrow keys)
    public static float Horizontal()
    {
        return Input.GetAxisRaw("Horizontal");
    }
}
