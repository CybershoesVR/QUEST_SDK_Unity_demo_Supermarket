using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVR;

public class BlockShooter : MonoBehaviour
{
    [SerializeField] GameObject[] projectiles;
    [SerializeField] float speed = 500;
    [SerializeField] Transform spawnPoint;
    [SerializeField] OVRInput.Axis1D trigger;

    private bool lastPressed = false;


    void Update()
    {
        if (OVRInput.Get(trigger) > 0 && !lastPressed)
        {
            lastPressed = true;

            int projectileindex = Random.Range(0, projectiles.Length);

            GameObject bullet = Instantiate(projectiles[projectileindex], spawnPoint.position, spawnPoint.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(spawnPoint.forward * speed);
        }
        else if (OVRInput.Get(trigger) <= 0)
        {
            lastPressed = false;
        }
    }
}
