using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cybershoes;

public class DebugCanvas : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI xText;
    [SerializeField] TextMeshProUGUI yText;
    [SerializeField] TextMeshProUGUI jumpText;
    [SerializeField] TextMeshProUGUI lastBTUpdateText;
    [SerializeField] OVRHand leftHand;
    [SerializeField] OVRHand rightHand;
    [SerializeField] TextMeshProUGUI pinchText;

    /* //FOR UPDATE DETECTION
    float counter;
    float counterMax = 100;
    bool lastInput = false;
    */

    private void Update()
    {
        Vector2 input = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.Gamepad);
        SetX(input.x);
        SetY(input.y);

        //jumpText.text = $"Pinch R: {rightHand.GetFingerPinchStrength(OVRHand.HandFinger.Index):0.00}";
        //jumpText.text = $"button2: {OVRInput.Get(OVRInput.Button.Two, OVRInput.Controller.Gamepad)}";
        jumpText.text = $"Jump: {OVRInput.Get(OVRInput.Button.One, OVRInput.Controller.Gamepad)}";
        lastBTUpdateText.text = $"BT Timeout: {CybershoesInput.lastBTupdateTook:0.000}";
    }

    void SetX(float x)
    {
        xText.text = $"X: {x:0.00}";
    }

    void SetY(float y)
    {
        yText.text = $"Y: {y:0.00}";
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
