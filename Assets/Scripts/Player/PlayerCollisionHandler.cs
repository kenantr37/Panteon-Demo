using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerCollisionHandler : MonoBehaviour
{
    PlayerMovement playerMovement;
    float moveToCenter = 0;
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
            playerMovement.PlayerMoveForwardSpeed = 0;
            playerMovement.MouseSpeed = 0;
            playerMovement.MobilScreenSpeed = 0;
            StartCoroutine(FinishLineWait());

            playerMovement.PlayerRb.isKinematic = true;
            changeCameraPosition = true;
        }
    }
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Finish")
        {
            moveToCenter = Mathf.Lerp(0, 1, .5f);
            centerOfTheGame = new Vector3(0, 0, transform.localPosition.z);
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, centerOfTheGame, moveToCenter * Time.deltaTime);
            Debug.Log(moveToCenter);
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