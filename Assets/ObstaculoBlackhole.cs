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

    void OnTriggerStay(Collider col)
    {
        if (gm.useSlowTime) { Time.timeScale = 0.5f; }
        gm.TeleportBlackhole(secondBlackhole.transform.position, secondBlackhole.transform.right);
    }
}
