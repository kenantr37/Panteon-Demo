using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfDonutStick : MonoBehaviour
{
    [SerializeField] float stickSpeed = 15f;
    Rigidbody stickRb;

    void Awake()
    {
        stickRb = GetComponent<Rigidbody>();
    }
    void Start()
    {
        StartCoroutine(StickHorizontalMove());
    }

    IEnumerator StickHorizontalMove()
    {

        if (gameObject.transform.localPosition.x >= 0.130f)
        {
            stickRb.velocity = (stickSpeed * Time.deltaTime * Vector3.left);
        }
        if (gameObject.transform.localPosition.x <= -0.1f)
        {
            stickRb.velocity = (stickSpeed * Time.deltaTime * Vector3.right);
        }
        yield return new WaitForSeconds(1);
    }
}