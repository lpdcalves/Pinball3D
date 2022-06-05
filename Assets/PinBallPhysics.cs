using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinBallPhysics : MonoBehaviour
{
    public float FallMultiplier = 1.5f;
    private Rigidbody my_rigidbody;
    
    public AudioSource rollingSource;
    public AudioSource hitSource;

    // Start is called before the first frame update
    void Start()
    {
        my_rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rollingSource.volume = Mathf.Clamp(my_rigidbody.velocity.magnitude / 40f, 0.1f, 0.3f);
        rollingSource.pitch = Mathf.Clamp(my_rigidbody.velocity.magnitude / 15f, 0.2f, 2f);
    }

    private void FixedUpdate()
    {
        my_rigidbody.velocity += new Vector3(0,1,1) * Physics.gravity.y * FallMultiplier * Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Walls")
        {
            foreach (ContactPoint contact in collision.contacts)
            {
                float angle = Vector3.Angle(contact.normal, my_rigidbody.velocity.normalized);
                float volume = 0f;
                if (angle > 60)
                    volume = 0f;
                else
                    volume = Mathf.Clamp(-(angle - 90f) / 90f, 0, 0.6f);
                hitSource.volume = volume;
                hitSource.Play();
            }
        }
    }
}
