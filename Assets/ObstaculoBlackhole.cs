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

    //void OnCollisionStay(Collision collision)
    //{
    //    foreach (ContactPoint contact in collision.contacts)
    //    {
    //        Time.timeScale = 0.2f;
    //        gm.TeleportBlackhole(secondBlackhole.transform.position, secondBlackhole.transform.right);
    //        //Vector3 dir = new Vector3(contact.normal.x, -contact.normal.y, contact.normal.z);
    //        //ball_rb.AddForce(dir * boostStrength, ForceMode.Impulse);
    //        //Debug.DrawRay(secondBlackhole.transform.position, dir, Color.green, 2f);
    //    }
    //}

    void OnTriggerStay(Collider col)
    {
        Time.timeScale = 0.8f;
        gm.TeleportBlackhole(secondBlackhole.transform.position, secondBlackhole.transform.right);
    }
}
