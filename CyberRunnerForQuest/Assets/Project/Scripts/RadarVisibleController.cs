using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarVisibleController : MonoBehaviour
{
    private OVRHand hand;
    private GameObject radar;

    private void Start()
    {
        hand = GetComponent<OVRHand>();
        radar = transform.GetChild(0).gameObject;
    }

    void Update()
    {
        if (hand.IsDataHighConfidence && !radar.activeSelf)
        {
            radar.SetActive(true);
        }
        else if (!hand.IsDataHighConfidence && radar.activeSelf)
        {
            radar.SetActive(false);
        }
    }
}
