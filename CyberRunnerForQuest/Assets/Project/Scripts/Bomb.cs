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
    [Space]
    [SerializeField] AudioClip panicTrack;
    [SerializeField] float panicVolume = 0.3f;

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

            if (panic || Time.time - detonationStartTime >= detonationTime - panicTime)
            {
                if (!panic)
                {
                    panic = true;
                    MusicPlayer.Instance.StartSubTrack(panicTrack,panicVolume);
                }

                if (Time.time - detonationStartTime >= detonationTime)
                {
                    ticking = false;
                    panic = false;

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

                    MusicPlayer.Instance.StopSubTrack();

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
                            rb.AddForce((hits[i].transform.position - transform.position) * power, ForceMode.Impulse);
                        }
                    }

                    FindObjectOfType<SupermarketDemo.PlayerController>().GameOver();

                    Destroy(gameObject, 2);
                }
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

            FindObjectOfType<SupermarketDemo.PlayerController>().AddDiffusedBomb();

            if (panic)
            {
                panic = false;
                MusicPlayer.Instance.StopSubTrack();
            }
        }
    }
}
