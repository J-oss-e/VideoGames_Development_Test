using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public static class InputHelper
{
    // Returns -1, 0 or 1 for left/right (A/D or arrow keys)
    public static float Horizontal()
    {
    //New input system
#if ENABLE_INPUT_SYSTEM
        var kb = Keyboard.current;
        if (kb == null) return 0f; //No keyboard connected
        float x = 0f;
        if (kb.aKey.isPressed || kb.leftArrowKey.isPressed) x -= 1f;
        if (kb.dKey.isPressed || kb.rightArrowKey.isPressed) x += 1f;
        return x;
#else   
    //Old input system
        return Input.GetAxisRaw("Horizontal");
#endif
    }
}
