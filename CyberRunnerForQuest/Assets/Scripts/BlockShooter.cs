using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVR;

public class BlockShooter : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] float speed = 500;
    [SerializeField] Transform spawnPoint;
    [SerializeField] OVRInput.Axis1D trigger;

    private bool lastPressed = false;


    void Update()
    {
        if (OVRInput.Get(trigger) > 0 && !lastPressed)
        {
            lastPressed = true;

            GameObject bullet = Instantiate(projectile, spawnPoint.position, spawnPoint.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(spawnPoint.forward * speed);
        }
        else if (OVRInput.Get(trigger) <= 0)
        {
            lastPressed = false;
        }
    }
}
