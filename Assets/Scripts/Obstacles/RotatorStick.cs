using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorStick : MonoBehaviour
{
    [SerializeField] public float rotatorRotateSpeed;
    void Update()
    {
        RotateRotatorStick();
    }
    void RotateRotatorStick()
    {
        transform.Rotate(Vector3.up * rotatorRotateSpeed * Time.deltaTime);
    }

}
