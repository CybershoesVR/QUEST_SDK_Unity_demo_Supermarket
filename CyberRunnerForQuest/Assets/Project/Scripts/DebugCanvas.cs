using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugCanvas : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI xText;
    [SerializeField] TextMeshProUGUI yText;
    [SerializeField] TextMeshProUGUI jumpText;


    private void Update()
    {
        Vector2 input = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);

        SetX(input.x);
        SetY(input.y);
        SetJump(OVRInput.Get(OVRInput.Button.One, OVRInput.Controller.Gamepad)
            || OVRInput.Get(OVRInput.Button.One, OVRInput.Controller.Touch));
    }

    void SetX(float x)
    {
        xText.text = $"X: {x:0.00}";
    }

    void SetY(float y)
    {
        yText.text = $"Y: {y:0.00}";
    }

    void SetJump(bool jump)
    {
        jumpText.text = $"Jump: {jump}";
    }
}
