using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OpponentCollisionHandler : MonoBehaviour
{
    Opponent opponent;
    void Start()
    {
        opponent = GetComponent<Opponent>();
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "MandatoryObstacles" || collision.gameObject.tag == "Holl" || collision.gameObject.tag == "RotationStick")
        {
            opponent.OppoonentStartingPosition = true;
        }
        if (collision.gameObject.CompareTag("Finish"))
        {
            opponent.OpponentRb.isKinematic = true;
        }
    }
}