using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "RotatingPlatform")
        {
            transform.parent = collision.transform;
        }
        if (collision.gameObject.tag == "Ground")
        {
            transform.parent = null;
        }
    }
}