using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloqueadorCanaleta : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerExit(Collider other)
    {
        Collider col = GetComponent<Collider>();
        col.isTrigger = false;
    }
}
