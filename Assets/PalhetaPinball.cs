using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalhetaPinball : MonoBehaviour
{
    public float RestingAngle = 0f;
    public float PressedAngle = 35f;
    public float SpringStrength = 10000f;
    public float SpringDamper = 150;

    public float strengthModifier = 1f;
    public float maxHitStrength = 50f;

    AudioSource soundEffect;
    Rigidbody my_rb;
    KeyCode ActivatingKey;
    HingeJoint hinge;

    void Start()
    {
        soundEffect = GetComponent<AudioSource>();
        my_rb = GetComponent<Rigidbody>();
        hinge = GetComponent<HingeJoint>();
        hinge.useSpring = true;
        if(gameObject.name == "PalhetaE")
        {
            ActivatingKey = KeyCode.A;
        }
        else if (gameObject.name == "PalhetaD")
        {
            ActivatingKey = KeyCode.D;
        }
    }

    void Update()
    {
        JointSpring spring = new JointSpring();
        spring.spring = SpringStrength;
        spring.damper = SpringDamper;

        if (Input.GetKeyDown(ActivatingKey))
        {
            soundEffect.Play();
        }
        if (Input.GetKey(ActivatingKey))
        {
            spring.targetPosition = PressedAngle;
        }
        else
        {
            spring.targetPosition = RestingAngle;
        }
        hinge.spring = spring;
        hinge.useLimits = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if (Input.GetKey(ActivatingKey))
            {
                Rigidbody ball_rb = collision.rigidbody;
                float dist = (hinge.connectedAnchor - contact.point).magnitude;
                float hit_str = Mathf.Clamp(my_rb.velocity.magnitude * strengthModifier * dist, 0, maxHitStrength);
                ball_rb.AddForce(-contact.normal * hit_str , ForceMode.Impulse);
                
            }
        }
    }
}
