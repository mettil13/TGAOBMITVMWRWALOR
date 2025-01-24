using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleBuoiancy : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float waterHeight;
    [SerializeField] float waterDensity = 1f;
    [SerializeField] float waveFrequency = 0.5f;
    [SerializeField] float waveAmplitude = 0.2f;
    [SerializeField] float r = 1;

    public float flowIntensity;
    [NonSerialized] public Vector3 flowDirection = Vector3.forward;

    private void FixedUpdate()
    {
        float y = transform.position.y;
        float low = y - r;
        float high = y + r;
        float v = 0f;
        float A = 0f;
        float waterHeightWaved = waterHeight + (waveAmplitude * Mathf.Sin(Time.time * waveFrequency));

        if (low < waterHeightWaved) 
        {
            if (y > waterHeightWaved)
            {
                float h = waterHeightWaved - low;
                v = CapVolume(h, r);
                A = AreaCrossSection(r, h);

            }
            else
            {
                if (high > waterHeightWaved)
                {
                    float h = high - waterHeightWaved;
                    v =  (4f / 3f * Mathf.PI * r * r * r) - CapVolume(h, r);
                    A = AreaCrossSection(r, h);
                }
                else
                {
                    A = Mathf.PI * r * r;
                    v = (4f / 3f * Mathf.PI * r * r * r);
                }
            }
        }

        Vector3 drag = Drag(waterDensity, rb.velocity, A);

        //Debug.Log(drag);

        float force = v * waterDensity * - Physics.gravity.y;

        rb.AddForce(Vector3.up * force + drag + flowDirection * flowIntensity * A, ForceMode.Force);
    }

    float AreaCrossSection(float r, float h)
    {
        return Mathf.PI * h * (2f * r - h);
    }

    Vector3 Drag(float p, Vector3 v, float A)
    {
        return - 1f / 2f * p * A  * v;
    }

    float CapVolume (float h, float r)
    {
        h = Mathf.Abs(h);
        return 1f/3f * Mathf.PI* h * h * (3f / 2f * r - h);
    }
}
