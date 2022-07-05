using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingStick : MonoBehaviour
{
    [SerializeField] [Header("Rotator of the Stick")] GameObject rotatorStick;

    GameObject player;
    GameObject opponent;
    Rigidbody playerRb;
    Rigidbody opponentRb;
    RotatorStick rotatorStickScript;

    void Awake()
    {
        rotatorStickScript = rotatorStick.GetComponent<RotatorStick>();

        player = GameObject.FindGameObjectWithTag("Player");
        opponent = GameObject.FindGameObjectWithTag("Opponent");
        playerRb = player.GetComponent<Rigidbody>();
        opponentRb = opponent.GetComponent<Rigidbody>();
    }
    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            if (Mathf.Sign(rotatorStickScript.rotatorRotateSpeed) == 1)
            {
                playerRb.AddForce((gameObject.transform.right) * 150f);
                Debug.Log("saðdan vurdu");
            }
            if (Mathf.Sign(rotatorStickScript.rotatorRotateSpeed) == -1)
            {
                playerRb.AddForce((-gameObject.transform.right) * 150f);
                Debug.Log("soldan vurdu");
            }
        }

        if (collision.gameObject.CompareTag("Opponent"))
            if (Mathf.Sign(rotatorStickScript.rotatorRotateSpeed) == 1)
            {
                opponentRb.AddForce((gameObject.transform.right) * 150f);
                Debug.Log("saðdan vurdu");
            }
        if (Mathf.Sign(rotatorStickScript.rotatorRotateSpeed) == -1)
        {
            opponentRb.AddForce((-gameObject.transform.right) * 150f);
            Debug.Log("soldan vurdu");
        }
    }
}