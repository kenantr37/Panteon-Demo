using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    [SerializeField] float rotatingSpeed;
    void Update()
    {
        RotateToZAxis();
    }
    void RotateToZAxis()
    {
        transform.Rotate(Vector3.forward * rotatingSpeed * Time.deltaTime);
    }
}