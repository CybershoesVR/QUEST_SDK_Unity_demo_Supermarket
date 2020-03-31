using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{
    private static Transform currentBomb;
    private Transform pointer;
    private float maxZscale = 2;
    //int damping = 2;

    private void Start()
    {
        pointer = transform.GetChild(0);
    }

    public static void SetCurrentBomb(Transform bomb)
    {
        currentBomb = FindObjectOfType<Bomb>()?.transform;
    }

    private static float MapClamped(float input, float input_start, float input_end, float output_start, float output_end)
    {
        float output = Map(input, input_start, input_end, output_start, output_end);
        output = Mathf.Max(output, output_start);
        output = Mathf.Min(output, output_end);
        return output;
    }

    private static float Map(float input, float input_start, float input_end, float output_start, float output_end)
    {
        return output_start + ((output_end - output_start) / (input_end - input_start)) * (input - input_start);
    }

    private void FixedUpdate()
    {
        if (currentBomb != null)
        {
            //AIM
            transform.LookAt(currentBomb);
            float distance2bomb = (currentBomb.position - transform.position).magnitude;
            //  Debug.Log(distance2bomb);
            pointer.localScale = new Vector3(1, 1, MapClamped(distance2bomb, 0, 20, 0, maxZscale));
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
