using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opponent : MonoBehaviour
{
    [SerializeField] float opponentMoveForwardSpeed = .5f;
    [SerializeField] List<Transform> objectsToEscape;
    [SerializeField] float horizontalDistanceBetweenNextObject;

    GameObject ground;
    Rigidbody opponentRb;
    float groundWeightSize;

    void Start()
    {
        groundWeightSize = GameObject.FindGameObjectWithTag("Ground").GetComponent<BoxCollider>().size.z;
        ground = GameObject.FindGameObjectWithTag("Ground");
        opponentRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        OpponentMoveForward();
        OpponentMoveLeftRight();
        OpponentKeepStayingOnTheWay();

    }
    void OpponentMoveForward()
    {
        Vector3 opponentForward = Vector3.forward * opponentMoveForwardSpeed * Time.deltaTime;
        transform.position = transform.position + opponentForward;
        transform.rotation = Quaternion.Euler(Vector3.forward);
    }
    void OpponentMoveLeftRight()
    {
        foreach (Transform everyObject in objectsToEscape)
        {
            float verticalDistanceBetweenNextObject = Mathf.Abs(transform.position.z - everyObject.position.z);
            horizontalDistanceBetweenNextObject = Mathf.Abs(transform.localPosition.x - everyObject.position.x);
            Debug.Log("yatay mesafe" + horizontalDistanceBetweenNextObject);

            if (verticalDistanceBetweenNextObject <= .2f)
            {
                if (horizontalDistanceBetweenNextObject <= 0)
                {
                    transform.Translate(Vector3.left * Time.deltaTime * .2f);
                }
                else
                {
                    transform.Translate(Vector3.right * Time.deltaTime * .2f);
                }
            }
        }
    }
    void OpponentKeepStayingOnTheWay()
    {
        if (transform.position.x <= -groundWeightSize)
        {
            transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, transform.position.y, transform.position.z),
                Vector3.zero, Time.deltaTime * .5f);
        }
        //else if (transform.position.x >= groundWeightSize)
        //{
        //    transform.position = Vector3.MoveTowards(new Vector3(groundWeightSize, transform.position.y, transform.position.z),
        //        Vector3.zero, Time.deltaTime * .5f);
        //}
        //else
        //{
        //    transform.position = Vector3.MoveTowards(new Vector3(groundWeightSize, transform.position.y, transform.position.z),
        //        Vector3.zero, Time.deltaTime * .5f);
        //}
    }
}
