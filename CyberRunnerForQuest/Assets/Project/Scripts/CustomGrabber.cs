using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGrabber : MonoBehaviour
{
    [SerializeField] Transform grabPoint;
    [SerializeField] float grabRadius;
    [SerializeField] LayerMask grabMask;
    [SerializeField] OVRInput.Axis1D grabButton;
    [SerializeField] CustomGrabber otherHand;

    private bool lastPressed = false;
    private Transform currentGrabObj;
    private Rigidbody currentGrabRB;
    private Vector3 lastPos;


    void Update()
    {
        if (OVRInput.Get(grabButton) > 0 && !lastPressed)
        {
            lastPressed = true;

            Collider[] grabCol = Physics.OverlapSphere(grabPoint.position, grabRadius, grabMask);

            if (grabCol.Length > 0)
            {
                currentGrabObj = grabCol[0].transform;
                currentGrabRB = currentGrabObj.GetComponent<Rigidbody>();

                currentGrabRB.isKinematic = true;
                currentGrabObj.parent = grabPoint;

                if (otherHand.GetCurrentGrabObject() == currentGrabObj)
                {
                    otherHand.Snatch();

                    Bomb b = currentGrabObj.GetComponent<Bomb>();

                    if (b)
                    {
                        b.Defuse();
                    }
                }
            }
        }
        else if (OVRInput.Get(grabButton) <= 0 && lastPressed)
        {
            lastPressed = false;

            if (currentGrabObj != null)
            {
                currentGrabObj.parent = null;
                currentGrabRB.isKinematic = false;
                currentGrabRB = null;
                currentGrabObj = null;
            }
        }
    }

    public Transform GetCurrentGrabObject()
    {
        return currentGrabObj;
    }

    public void Snatch()
    {
        currentGrabRB = null;
        currentGrabObj = null;
    }

    private void OnDrawGizmosSelected()
    {
        if (grabPoint != null && grabRadius > 0)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(grabPoint.position, grabRadius);
        }
    }
}
