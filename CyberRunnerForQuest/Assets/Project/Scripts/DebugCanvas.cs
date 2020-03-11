using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugCanvas : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI xText;
    [SerializeField] TextMeshProUGUI yText;
    [SerializeField] TextMeshProUGUI jumpText;
    [SerializeField] OVRHand leftHand;
    [SerializeField] OVRHand rightHand;
    [SerializeField] TextMeshProUGUI pinchText;


    private void Update()
    {
        Vector2 input = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.Gamepad);

        SetX(input.x);
        SetY(input.y);
        //SetJump(OVRInput.Get(OVRInput.Button.One, OVRInput.Controller.Gamepad));
        //float strengthLeft = GetLeftGrip();
        //float strengthRight = GetRightGrip();
        //pinchText.text = $"Grip: L {strengthLeft:0.0} R {strengthRight:0.0}";
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

    float GetLeftGrip()
    {
        float[] pinchStrength = new float[4];
        pinchStrength[0] = leftHand.GetFingerPinchStrength(OVRHand.HandFinger.Index);
        pinchStrength[1] = leftHand.GetFingerPinchStrength(OVRHand.HandFinger.Middle);
        pinchStrength[2] = leftHand.GetFingerPinchStrength(OVRHand.HandFinger.Ring);
        pinchStrength[3] = leftHand.GetFingerPinchStrength(OVRHand.HandFinger.Pinky);

        float thumbStrength = leftHand.GetFingerPinchStrength(OVRHand.HandFinger.Thumb);

        float pinchMax = 0;

        for (int i = 0; i < pinchStrength.Length; i++)
        {
            if (pinchStrength[i] > pinchMax)
            {
                pinchMax = pinchStrength[i];
            }
        }

        return pinchMax;
    }

    float GetRightGrip()
    {
        float[] pinchStrength = new float[4];
        pinchStrength[0] = rightHand.GetFingerPinchStrength(OVRHand.HandFinger.Index);
        pinchStrength[1] = rightHand.GetFingerPinchStrength(OVRHand.HandFinger.Middle);
        pinchStrength[2] = rightHand.GetFingerPinchStrength(OVRHand.HandFinger.Ring);
        pinchStrength[3] = rightHand.GetFingerPinchStrength(OVRHand.HandFinger.Pinky);

        float thumbStrength = rightHand.GetFingerPinchStrength(OVRHand.HandFinger.Thumb);

        float pinchMax = 0;

        for (int i = 0; i < pinchStrength.Length; i++)
        {
            if (pinchStrength[i] > pinchMax)
            {
                pinchMax = pinchStrength[i];
            }
        }

        return pinchMax;
    }
}
