using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaculoBlackhole : MonoBehaviour
{
    public GameMaster gm;
    public GameObject secondBlackhole;
    public float boostStrength = 20f;

    void Start()
    {
        gm = FindObjectOfType<GameMaster>();
    }


    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            Rigidbody ball_rb = collision.rigidbody;
            gm.TeleportBlackhole(secondBlackhole.transform.position);
            Vector3 dir = new Vector3(contact.normal.x, contact.normal.y, contact.normal.z);
            ball_rb.AddForce(dir * boostStrength, ForceMode.Impulse);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        gm.TeleportBlackhole(secondBlackhole.transform.position);
    }
}
