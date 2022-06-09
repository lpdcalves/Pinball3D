using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatilhoPinball : MonoBehaviour
{
    private GameObject ball;

    public int MinImpulseForce = 20;
    public int MaxImpulseForce = 70;
    public float SecondsToFullCharge = 2.5f;
    private float curr_ImpulseForce;

    public Color emptyColor;
    public Color fullColor;

    private float timer = 0;
    bool canShoot = true;

    private AudioSource soundEffect;
    private MeshRenderer my_meshrenderer;

    // Start is called before the first frame update
    void Start()
    {
        curr_ImpulseForce = MinImpulseForce;
        my_meshrenderer = GetComponent<MeshRenderer>();
        soundEffect = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            timer += Time.deltaTime;

            Color lerpedColor = Color.Lerp(emptyColor, fullColor, timer/SecondsToFullCharge);

            my_meshrenderer.material.color = lerpedColor;
            my_meshrenderer.material.SetColor("_EmissionColor", lerpedColor);

            if (curr_ImpulseForce < MaxImpulseForce)
            {
                float ChargeIncreaseRate = Time.deltaTime / SecondsToFullCharge;
                curr_ImpulseForce += (MaxImpulseForce - MinImpulseForce) * ChargeIncreaseRate;
            }
        }
        
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (canShoot)
            {
                ball = GameObject.FindGameObjectWithTag("Player");
                ball.GetComponent<Rigidbody>().AddForce(transform.up * curr_ImpulseForce, ForceMode.Impulse);
            }
            soundEffect.volume = curr_ImpulseForce / MaxImpulseForce;
            soundEffect.Play();
            curr_ImpulseForce = MinImpulseForce;
            my_meshrenderer.material.color = emptyColor;
            my_meshrenderer.material.SetColor("_EmissionColor", emptyColor);
            timer = 0;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        canShoot = true;
    }

    void OnCollisionExit(Collision collision)
    {
        canShoot = false;
    }
}
