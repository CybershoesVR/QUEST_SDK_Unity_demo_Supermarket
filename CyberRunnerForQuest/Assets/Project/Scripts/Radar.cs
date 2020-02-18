using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{
    private Transform currentBomb;


    private void Start()
    {
        currentBomb = FindObjectOfType<Bomb>()?.transform;
    }

    private void FixedUpdate()
    {
        if (currentBomb != null)
        {
            //AIM
            Vector3 aimPos = currentBomb.position - transform.position;
            aimPos.y = 0;
            aimPos = aimPos.normalized;

            float angle = Mathf.Atan2(aimPos.z, aimPos.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, transform.up);
        }
    }
}
