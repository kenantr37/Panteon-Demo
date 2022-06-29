using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float playerSpeed = 1f;
    float startPos;

    void Start()
    {
        float startPos = Input.mousePosition.x;
    }
    void Update()
    {
        MoveForward();
        SwerveMechanic();
    }
    void MoveForward()
    {
        //if you'll use Rigidbody, don't forget to look at Time.fixedDeltaTime
        Vector3 playerForward = Vector3.forward * playerSpeed * Time.deltaTime;
        transform.position = transform.position + playerForward;
    }
    void SwerveMechanic()
    {
        if (Input.GetMouseButtonDown(0))
        {
            float endPos = Input.mousePosition.x - startPos;
            Debug.Log(startPos + " : " + endPos);
        }
    }
}
