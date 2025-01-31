using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;

public class BubbleBuoiancy : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float waterHeight;
    [SerializeField] float waterDensity = 1f;
    [SerializeField] float waveFrequency = 0.5f;
    [SerializeField] float waveAmplitude = 0.2f;
    [SerializeField] float r = 1;
    [SerializeField] AudioClip SplashClip;

    public float flowIntensity;
    [SerializeField] public Vector3 flowDirection = Vector3.forward;
    bool isInWater;
    [SerializeField] public GameObject splashVFX;

    Vector3 previousFlowDirection;
    [SerializeField] float flowDirChangeSpeed;
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
            if (!isInWater)
            {
                GameObject vfx = Instantiate(splashVFX, transform.position + Vector3.down * r, Quaternion.identity);
                vfx.GetComponent<VisualEffect>().Play();
                Destroy(vfx, 5f);
                if (SceneManager.GetActiveScene().name != "MainMenu")
                    AudioManager.instance.PlaySound(SplashClip);
            }
            isInWater = true;
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
                    v = (4f / 3f * Mathf.PI * r * r * r) - CapVolume(h, r);
                    A = AreaCrossSection(r, h);
                }
                else
                {
                    A = Mathf.PI * r * r;
                    v = (4f / 3f * Mathf.PI * r * r * r);
                }
            }
        }
        else
        {
            isInWater = false;
        }

        Vector3 drag = Drag(waterDensity, rb.velocity, A);

        //Debug.Log(drag);

        float force = v * waterDensity * -Physics.gravity.y;

        previousFlowDirection = Vector3.Slerp(previousFlowDirection, flowDirection, Time.deltaTime * flowDirChangeSpeed);

        rb.AddForce(Vector3.up * force + drag + previousFlowDirection * flowIntensity * A, ForceMode.Force);
    }

    float AreaCrossSection(float r, float h)
    {
        return Mathf.PI * h * (2f * r - h);
    }

    Vector3 Drag(float p, Vector3 v, float A)
    {
        return -1f / 2f * p * A * v;
    }

    float CapVolume(float h, float r)
    {
        h = Mathf.Abs(h);
        return 1f / 3f * Mathf.PI * h * h * (3f / 2f * r - h);
    }
}
