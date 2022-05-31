using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaculoPin : MonoBehaviour
{
    public GameMaster gm;
    public GameObject pinHead;
    public int pinScore = 100;
    public bool useBounce = true;
    public float bounceStrength = 100f;
    private float blinkTimer = 0.2f;
    private bool blinkColor = false;
    private float timer = 0;
    private Color my_color;

    void Start()
    {
        gm = FindObjectOfType<GameMaster>();
        my_color = GetComponent<MeshRenderer>().material.color;
    }

    void Update()
    {
        if(blinkColor)
        {
            timer += Time.deltaTime;
            //pinHead.GetComponent<MeshRenderer>().material.color = Color.white;
            Color lerpedColor = Color.Lerp(Color.white, my_color, timer / blinkTimer);
            pinHead.GetComponent<MeshRenderer>().material.color = lerpedColor;
            if(timer >= blinkTimer)
            {
                blinkColor = false;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        blinkColor = true;
        timer = 0;
        if (useBounce)
        {
            foreach (ContactPoint contact in collision.contacts)
            {
                Rigidbody ball_rb = collision.rigidbody;
                ball_rb.AddForce(-contact.normal * bounceStrength, ForceMode.Impulse);
            }
        }
        gm.AddScore(pinScore);
    }
    void OnTriggerEnter(Collider col)
    {
        blinkColor = true;
        timer = 0;
        gm.AddScore(pinScore);
    }
}
