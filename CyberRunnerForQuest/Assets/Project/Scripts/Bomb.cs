using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] float power = 7;
    [SerializeField] float detonationTime;
    [SerializeField] float blinkTime = 1;
    [SerializeField] float panicTime = 5;

    private float detonationStartTime;
    private float blinkStartTime;
    private bool ticking = true;
    private bool panic = false;
    private Renderer rend;

    private Color offColor = Color.white;


    void Start()
    {
        rend = GetComponent<Renderer>();
        detonationStartTime = Time.time;
        rend.material.color = Color.yellow;
    }

    void FixedUpdate()
    {
        if (ticking)
        {
            if (Time.time - detonationStartTime >= detonationTime)
            {
                rend.material.color = Color.red;
                ticking = false;

                Collider[] hits = Physics.OverlapSphere(transform.position, 5);

                for (int i = 0; i < hits.Length; i++)
                {
                    Rigidbody rb = hits[i].GetComponent<Rigidbody>();

                    if (rb != null)
                    {
                        rb.AddForce((hits[i].transform.position - transform.position)*power, ForceMode.Impulse);
                    }
                }
            }
            else if (Time.time - blinkStartTime >= blinkTime)
            {
                if (!panic && Time.time - detonationStartTime >= detonationTime - panicTime)
                {
                    panic = true;
                    blinkTime /= 2;
                }
               
                blinkStartTime = Time.time;

                Color cache = rend.material.color;
                rend.material.color = offColor;
                offColor = cache;
            }
        }
    }

    public void Defuse()
    {
        if (ticking)
        {
            rend.material.color = Color.green;
            ticking = false;
        }
    }
}
