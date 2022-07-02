using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentCollisionHandler : MonoBehaviour
{
    Opponent opponent;
    void Awake()
    {
        opponent = GetComponent<Opponent>();
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "RotatingPlatform")
        {
            Debug.Log("�st�nde");
            transform.parent = collision.gameObject.transform;
            opponent.OpponentOnTheRotatingPlatform = true;
        }
        if (collision.gameObject.tag == "Ground")
        {
            transform.parent = null;
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "RotatingPlatform")
        {
            Debug.Log("��kt�");
            opponent.OpponentOnTheRotatingPlatform = false;
        }
    }
}
