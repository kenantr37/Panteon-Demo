using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerCollisionHandler : MonoBehaviour
{
    PlayerMovement playerMovement;
    float center = 0;
    Vector3 centerOfTheGame;
    public bool changeCameraPosition;

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("RotatingPlatform"))
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
            Vector3 center = new Vector3(0, transform.position.y, transform.position.z);
            transform.position = center;

            playerMovement.PlayerMoveForwardSpeed = 0;
            playerMovement.MouseSpeed = 0;
            playerMovement.MobilScreenSpeed = 0;
            StartCoroutine(FinishLineWait());

            playerMovement.PlayerRb.isKinematic = true;
            changeCameraPosition = true;
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