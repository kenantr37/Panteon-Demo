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
        if (collision.gameObject.CompareTag("RotatingPlatformRight"))
        {
            transform.parent = collision.gameObject.transform;
            opponentRb.velocity = (Vector3.right * 150f * Time.deltaTime);
        }
        if (collision.gameObject.CompareTag("RotatingPlatformLeft"))
        {
            transform.parent = collision.gameObject.transform;
            opponentRb.velocity = (Vector3.left * 150f * Time.deltaTime);
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            transform.parent = null;
        }
        if (collision.gameObject.CompareTag("RotatorRight"))
        {
            opponent.OpponentRb.AddTorque(Vector3.left * 1000);
            Debug.Log("deðdi");
        }
        if (collision.gameObject.CompareTag("RotatorLeft"))
        {
            opponent.OpponentRb.AddTorque(Vector3.right * 1000);
            Debug.Log("deðdi");
        }
    }
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("RotatingPlatformRight"))
        {
            opponentRb.velocity = Vector3.right * 113f * Time.deltaTime;
        }
        if (collision.gameObject.CompareTag("RotatingPlatformLeft"))
        {
            opponentRb.velocity = Vector3.left * 113f * Time.deltaTime;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("FirstWayPointChecker"))
        {
            opponent.OpponentFollowNavMeshEnable = false;
            opponent.OpponentNavMesh.enabled = false;

            opponent.OpponentWayPointActive = true;
            opponent.OppponentMoveForwardSpeed = 1.5f;
        }
        if (other.gameObject.CompareTag("NavigatorActivator"))
        {
            opponent.OpponentFollowNavMeshEnable = true;
            opponent.OpponentNavMesh.enabled = true;

            opponent.OpponentWayPointActive = false;
            opponent.OppponentMoveForwardSpeed = 0;
        }

        if (other.gameObject.CompareTag("OpponentFinish"))
        {
            opponent.OpponentRb.isKinematic = true;
            opponent.OpponentNavMesh.enabled = false;
            opponent.OpponentStopAnim = true;
        }
    }
}