using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{
    private static Transform currentBomb;

    //int damping = 2;


    public static void SetCurrentBomb(Transform bomb)
    {
        currentBomb = FindObjectOfType<Bomb>()?.transform;
    }

    private void FixedUpdate()
    {
        if (currentBomb != null)
        {
            //AIM
            transform.LookAt(currentBomb);

            //Vector3 lookPos = currentBomb.position - transform.position;
            //lookPos.y = 0;
            //Quaternion rotation = Quaternion.LookRotation(lookPos);

            //transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);


            //Vector3 aimPos = currentBomb.position - transform.position;
            //aimPos.y = 0;
            //aimPos = aimPos.normalized;

            //float angle = Mathf.Atan2(aimPos.z, aimPos.x) * Mathf.Rad2Deg;
            //transform.rotation = Quaternion.AngleAxis(angle, transform.parent.up);
        }
        else
        {
            transform.LookAt(transform.position + Vector3.up);
        }
    }
}
