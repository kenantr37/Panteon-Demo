using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingStick : MonoBehaviour
{
    [SerializeField] [Header("Rotator of the Stick")] GameObject rotatorStick;

    GameObject player;
    Rigidbody playerRb;
    RotatorStick rotatorStickScript;

    void Awake()
    {
        rotatorStickScript = rotatorStick.GetComponent<RotatorStick>();

        player = GameObject.FindGameObjectWithTag("Player");
        playerRb = player.GetComponent<Rigidbody>();
    }
    void OnCollisionEnter(Collision collision)
    {
        {
            if (collision.gameObject.tag == "Player" && Mathf.Sign(rotatorStickScript.rotatorRotateSpeed) == 1)
            {
                playerRb.AddForce((gameObject.transform.right) * 500f);
                Debug.Log("saðdan vurdu");
            }
            else if (collision.gameObject.tag == "Player" && Mathf.Sign(rotatorStickScript.rotatorRotateSpeed) == -1)
            {
                playerRb.AddForce((-gameObject.transform.right) * 500f);
                Debug.Log("soldan vurdu");
            }
        }
    }
}