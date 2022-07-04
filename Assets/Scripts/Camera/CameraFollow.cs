using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Vector3 distanceFromPlayer;
    PlayerCollisionHandler playerCollisionHandler;
    void Awake()
    {
        playerCollisionHandler = GameObject.Find("Boy").GetComponent<PlayerCollisionHandler>();
    }
    void LateUpdate()
    {
        transform.position = player.transform.position + distanceFromPlayer;
        ChangeCameraPosition();
    }
    void ChangeCameraPosition()
    {
        if (playerCollisionHandler.changeCameraPosition)
        {
            Vector3 newDistanceFromPlayer = new Vector3(0, Mathf.Lerp(distanceFromPlayer.y, 5.03999996f, Time.deltaTime * 1f), -3.3499999f);
            distanceFromPlayer = newDistanceFromPlayer;
        }
    }
}
