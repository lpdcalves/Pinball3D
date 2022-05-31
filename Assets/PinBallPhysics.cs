using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinBallPhysics : MonoBehaviour
{
    public float FallMultiplier = 1.5f;
    private Rigidbody my_rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        my_rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //if (my_rigidbody.velocity.y < 0)
        //{
        //    my_rigidbody.velocity += Vector3.up * Physics.gravity.y * FallMultiplier * Time.deltaTime;
        //}
        my_rigidbody.velocity += new Vector3(0,1,1) * Physics.gravity.y * FallMultiplier * Time.deltaTime;
    }
}
