using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Vector3 distanceFromPlayer;
    void LateUpdate()
    {
        transform.position = player.transform.position + distanceFromPlayer;
    }
}
