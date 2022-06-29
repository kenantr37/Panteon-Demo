using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float playerSpeed = 0.1f;
    void Update()
    {
        SwerveMechanic();
    }
    void SwerveMechanic()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 horizontal = Input.mousePosition;
            horizontal = horizontal + transform.position;
            Debug.Log(horizontal + "mouse'a basýlýyor þuanda");
            transform.position = transform.position + new Vector3(horizontal.x * Time.deltaTime * playerSpeed, transform.position.y, transform.position.z);
        }
    }
}
