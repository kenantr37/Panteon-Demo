using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    PlayerMovement playerMovement;
    float moveToCenter = 0;
    Vector3 centerOfTheGame;
    Opponent opponent;

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        opponent = FindObjectOfType<Opponent>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "RotatingPlatform")
        {
            transform.parent = collision.transform;
        }
        if (collision.gameObject.tag == "Ground")
        {
            transform.parent = null;
        }
        if (collision.gameObject.tag == "Finish")
        {
            playerMovement.PlayerMoveForwardSpeed = 0;
            playerMovement.MouseSpeed = 0;
            playerMovement.MobilScreenSpeed = 0;
            StartCoroutine(FinishLineWait());
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
    }

}