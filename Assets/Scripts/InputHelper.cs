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

    // Returns -1, 0 or 1 for down/up (S/W or arrow keys).
    public static float Vertical()
    {
#if ENABLE_INPUT_SYSTEM
        var kb = Keyboard.current;
        if (kb == null) return 0f;
        float y = 0f;
        if (kb.sKey.isPressed || kb.downArrowKey.isPressed) y -= 1f;
        if (kb.wKey.isPressed || kb.upArrowKey.isPressed) y += 1f;
        return y;
#else
        return Input.GetAxisRaw("Vertical");
#endif
    }

    // True on the frame the jump key (space) goes down.
    public static bool JumpPressed()
    {
#if ENABLE_INPUT_SYSTEM
        return Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame;
#else
        return Input.GetButtonDown("Jump");
#endif
    }

    // True on the frame the jump key is released.
    public static bool JumpReleased()
    {
#if ENABLE_INPUT_SYSTEM
        return Keyboard.current != null && Keyboard.current.spaceKey.wasReleasedThisFrame;
#else
        return Input.GetButtonUp("Jump");
#endif
    }

    // Mouse position in screen pixels.
    public static Vector2 MouseScreenPosition()
    {
#if ENABLE_INPUT_SYSTEM
        return Mouse.current != null ? Mouse.current.position.ReadValue() : Vector2.zero;
#else 
        return Input.mousePosition;
#endif
    }
}
