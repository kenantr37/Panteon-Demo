using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfDonutStick : MonoBehaviour
{
    [SerializeField] float stickSpeed;
    Rigidbody stickRb;

    void Awake()
    {
        stickRb = gameObject.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        DonutMovement();
    }
    void DonutMovement()
    {
        if (gameObject.transform.localPosition.x >= 0.130f)
        {
            AddForceToStick(-1);
        }
        if (gameObject.transform.localPosition.x <= -0.095f)
        {
            AddForceToStick(1);
        }
    }
    void AddForceToStick(int direction)
    {
        stickRb.AddForce(direction * stickSpeed * 10 * Time.deltaTime, 0, 0);
    }
}