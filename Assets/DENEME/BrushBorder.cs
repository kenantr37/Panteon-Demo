using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrushBorder : MonoBehaviour
{
    Brush brush;
    [SerializeField] float xBorderForWall = 3.754f;
    [SerializeField] float yBorderForWall = 3.754f;

    void Awake()
    {
        brush = FindObjectOfType<Brush>();
    }
    void LateUpdate()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -xBorderForWall, xBorderForWall),
            transform.position.y, Mathf.Clamp(transform.position.z, -yBorderForWall, yBorderForWall));

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