using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField] GameObject wall;
    PlayerMovement playerMovement;

    public bool changeCameraPosition;
    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("RotatingPlatformRight") || collision.gameObject.CompareTag("RotatingPlatformLeft"))
        {
            transform.parent = collision.transform;
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            transform.parent = null;
        }
        if (collision.gameObject.tag == "Holl")
        {
            transform.parent = null;
            playerMovement.PlayerDeadChecker = true;
        }
        if (collision.gameObject.tag == "Finish")
        {
            playerMovement.paintTime = true;

            playerMovement.PlayerStopRunning = true;
            wall.gameObject.SetActive(true);
            Vector3 center = new Vector3(0, transform.position.y, transform.position.z);
            transform.position = center;

            playerMovement.PlayerMoveForwardSpeed = 0;
            playerMovement.MouseSpeed = 0;
            playerMovement.MobilScreenSpeed = 0;
            StartCoroutine(FinishLineWait());

            playerMovement.PlayerRb.isKinematic = true;
            changeCameraPosition = true;
        }
        if (collision.gameObject.CompareTag("RotatorRight"))
        {
            playerMovement.PlayerRb.AddForce(Vector3.left * 400);
        }
        if (collision.gameObject.CompareTag("RotatorLeft"))
        {
            playerMovement.PlayerRb.AddForce(Vector3.right * 400);
        }
        if (collision.gameObject.CompareTag("MandatoryObstacles"))
        {
            playerMovement.PlayerDeadChecker = true;
        }
    }
    IEnumerator FinishLineWait()
    {
        Debug.Log(Mathf.RoundToInt(Time.time));
        yield return new WaitForSeconds(3);

        playerMovement.FinishLineArrived = true;
        playerMovement.paintedWallRatioText.gameObject.SetActive(true);
    }
}