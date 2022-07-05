using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OpponentCollisionHandler : MonoBehaviour
{
    Opponent opponent;
    Rigidbody opponentRb;

    void Start()
    {
        opponent = GetComponent<Opponent>();
        opponentRb = GetComponent<Rigidbody>();
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "MandatoryObstacles" || collision.gameObject.tag == "Holl")
        {
            opponent.OppoonentStartingPosition = true;
        }
        if (collision.gameObject.CompareTag("Finish"))
        {
            opponent.OpponentRb.isKinematic = true;
        }
        if (collision.gameObject.CompareTag("RotatingPlatformRight"))
        {
            transform.parent = collision.gameObject.transform;
            opponentRb.velocity = (Vector3.right * 100f * Time.deltaTime);
        }
        if (collision.gameObject.CompareTag("RotatingPlatformLeft"))
        {
            transform.parent = collision.gameObject.transform;
            opponentRb.velocity = (Vector3.left * 100f * Time.deltaTime);
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            transform.parent = null;
            opponent.OpponentNavMeshEnable = true;
            opponent.OpponentNavMesh.enabled = true;

        }
    }
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("RotatingPlatformRight"))
        {
            opponentRb.velocity = Vector3.right * 95f * Time.deltaTime;
        }
        if (collision.gameObject.CompareTag("RotatingPlatformLeft"))
        {
            opponentRb.velocity = Vector3.left * 95f * Time.deltaTime;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("FirstWayPointChecker"))
        {
            opponent.OpponentNavMeshEnable = false;
            opponent.OpponentNavMesh.enabled = false;
        }
    }
}