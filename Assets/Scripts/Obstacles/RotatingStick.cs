using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingStick : MonoBehaviour
{
    GameObject player;
    Rigidbody playerRb;
    RotatorStick rotatorStick;
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rotatorStick = GameObject.FindGameObjectWithTag("Rotator").GetComponent<RotatorStick>();
        playerRb = player.GetComponent<Rigidbody>();
    }
    void OnCollisionEnter(Collision collision)
    {
        {
            if (collision.gameObject.tag == "Player" && Mathf.Sign(rotatorStick.rotatorRotateSpeed) == 1)
            {
                playerRb.AddForce((gameObject.transform.right) * 500f);
                Debug.Log("saðdan vurdu");
            }
            else if (collision.gameObject.tag == "Player" && Mathf.Sign(rotatorStick.rotatorRotateSpeed) == -1)
            {
                playerRb.AddForce((-gameObject.transform.right) * 500f);
                Debug.Log("soldan vurdu");
            }
        }
    }
}