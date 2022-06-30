using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    [Header("Horizontal Object Movements")]
    [SerializeField] float horizontalObstacleSpeed = 3f;
    [SerializeField] float horizontalObstacleRange = .5f;
    [SerializeField] Vector3 objectLocation;
    [SerializeField] Vector3 objectMovementDirection;
    void Update()
    {
        HoriztontalMove();
    }
    void HoriztontalMove()
    {
        float horizontal = Mathf.Sin(Time.time * horizontalObstacleSpeed) * horizontalObstacleRange;

        transform.localPosition = objectLocation + objectMovementDirection * horizontal;

    }
}