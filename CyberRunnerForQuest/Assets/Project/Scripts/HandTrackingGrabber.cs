using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;
using TMPro;


public class HandTrackingGrabber : OVRGrabber
{
    [SerializeField] float pinchTreshold = 0.7f;
    //[SerializeField] float thumbTreshold = 0.4f;
    //[SerializeField] Renderer grabVisualizer;
    [SerializeField] bool isRight = true;
    [SerializeField] TextMeshProUGUI grabEndVelText;

    //[SerializeField] Color gripColor;

    //Color baseColor;

    private OVRHand hand;


    protected override void Start()
    {
        base.Start();
        hand = GetComponent<OVRHand>();
        //baseColor = grabVisualizer.material.color;
    }

    public override void Update()
    {
        base.Update();
        CheckIndexPinch();
        //grabVisualizer.material.color = new Color(1, 1, 1, 0.5f);
    }

    void CheckIndexPinch()
    {
        //float[] pinchStrength = new float[4];
        //pinchStrength[0] = hand.GetFingerPinchStrength(OVRHand.HandFinger.Index);
        //pinchStrength[1] = hand.GetFingerPinchStrength(OVRHand.HandFinger.Middle);
        //pinchStrength[2] = hand.GetFingerPinchStrength(OVRHand.HandFinger.Ring);
        //pinchStrength[3] = hand.GetFingerPinchStrength(OVRHand.HandFinger.Pinky);

        float[] pinchStrength = new float[1];
        pinchStrength[0] = hand.GetFingerPinchStrength(OVRHand.HandFinger.Index);

        //float thumbStrength = hand.GetFingerPinchStrength(OVRHand.HandFinger.Thumb);

        float pinchMax = 0;

        for (int i = 0; i < pinchStrength.Length; i++)
        {
            if (pinchStrength[i] > pinchMax)
            {
                pinchMax = pinchStrength[i];
            }
        }

        bool isGrabbing = pinchMax > pinchTreshold && hand.GetFingerConfidence(OVRHand.HandFinger.Index) == OVRHand.TrackingConfidence.High;

        //if (isGrabbing)
        //{
        //    grabVisualizer.material.color = gripColor;
        //}
        //else
        //{
        //    grabVisualizer.material.color = baseColor;
        //}

        if (!m_grabbedObj && isGrabbing && m_grabCandidates.Count > 0)
        {
            GrabBegin();
        }
        else if (m_grabbedObj && !isGrabbing)
        {
            GrabEnd();
        }
    }

    protected override void GrabEnd()
    {
        if (m_grabbedObj)
        {
            Vector3 linearVelocity = (transform.position - m_lastPos) / Time.fixedDeltaTime;
            Vector3 angularVelocity = (transform.eulerAngles - m_lastRot.eulerAngles) / Time.fixedDeltaTime;

            string rightText = isRight ? "R" : "L";
            grabEndVelText.text = $"{rightText}: {linearVelocity}";

            GrabbableRelease(linearVelocity, angularVelocity);
        }

        GrabVolumeEnable(true);
    }

    protected override void OffhandGrabbed(OVRGrabbable grabbable)
    {
        base.OffhandGrabbed(grabbable);
        GrabVolumeEnable(true);
    }
}
