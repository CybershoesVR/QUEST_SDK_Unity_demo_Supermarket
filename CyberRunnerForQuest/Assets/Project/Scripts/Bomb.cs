using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : OVRGrabbable
{
    [SerializeField] float power = 7;
    [SerializeField] float detonationTime;    
    [SerializeField] float panicTime = 5;
    [SerializeField] float fuseEndPosY = 0;
    [SerializeField] Transform fuse;
    [SerializeField] ParticleSystem fireParticles;
    [SerializeField] ParticleSystem explosionParticles;
    [Space]
    [SerializeField] AudioClip diffuseClip;
    [SerializeField] AudioClip explosionClip;

    private float detonationStartTime;
    private bool ticking = true;
    private bool panic = false;
    private AudioSource src;
    private BombSpawner spawner;
    private Renderer[] rends;

    protected override void Start()
    {
        base.Start();

        src = GetComponent<AudioSource>();
        spawner = FindObjectOfType<BombSpawner>();
        detonationStartTime = Time.time;
        rends = GetComponentsInChildren<MeshRenderer>();

        Radar.SetCurrentBomb(transform);
    }

    void FixedUpdate()
    {
        if (ticking)
        {
            if (fuse)
            {
                fuse.localPosition = fuse.localPosition + (Vector3.forward * fuseEndPosY * (Time.fixedDeltaTime / detonationTime));
            }

            if (Time.time - detonationStartTime >= detonationTime)
            {
                ticking = false;

                Collider[] cols = GetComponents<Collider>();

                for (int i = 0; i < cols.Length; i++)
                {
                    cols[i].enabled = false;
                }

                GetComponent<Rigidbody>().isKinematic = true;

                src.Stop();
                src.loop = false;
                src.clip = explosionClip;
                src.Play();

                for (int i = 0; i < rends.Length; i++)
                {
                    rends[i].enabled = false;
                }

                explosionParticles.Play();

                Collider[] hits = Physics.OverlapSphere(transform.position, 5);

                for (int i = 0; i < hits.Length; i++)
                {
                    Rigidbody rb = hits[i].GetComponent<Rigidbody>();

                    if (rb != null)
                    {
                        rb.AddForce((hits[i].transform.position - transform.position)*power, ForceMode.Impulse);
                    }
                }

                spawner.SpawnNext();

                Destroy(gameObject,2);
            }
            else if (!panic && Time.time - detonationStartTime >= detonationTime - panicTime)
            {
                panic = true;
            }
        }
    }

    public override void GrabBegin(OVRGrabber hand, Collider grabPoint)
    {
        base.GrabBegin(hand, grabPoint);
        Defuse();
    }

    public void Defuse()
    {
        if (ticking)
        {
            fireParticles.Stop();
            ticking = false;
            spawner.SpawnNext();

            src.Stop();
            src.loop = false;
            src.clip = diffuseClip;
            src.Play();
        }
    }
}
