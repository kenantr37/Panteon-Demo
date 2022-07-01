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
            AddForceToStick(-1f);
        }
        if (gameObject.transform.localPosition.x <= -0.116f)
        {
            AddForceToStick(1f);
        }
    }
    void AddForceToStick(float direction)
    {
        stickRb.AddForce(direction * stickSpeed * 10 * Time.deltaTime, 0, 0);
    }
}