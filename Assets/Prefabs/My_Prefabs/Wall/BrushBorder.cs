using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrushBorder : MonoBehaviour
{
    Brush brush;

    [SerializeField] float xBorderForWall = 3.754f;
    [SerializeField] float yBorderForWall = 3.754f;
    PlayerMovement playerMovement;

    void Awake()
    {
        brush = FindObjectOfType<Brush>();
        playerMovement = FindObjectOfType<PlayerMovement>();

    }
    void LateUpdate()
    {

        if (playerMovement.FinishLineArrived)
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -xBorderForWall, xBorderForWall),
            Mathf.Clamp(transform.position.y, -yBorderForWall, yBorderForWall),
            transform.parent.position.z - .1f);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            brush.canPaint = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            brush.canPaint = false;
        }
    }
}