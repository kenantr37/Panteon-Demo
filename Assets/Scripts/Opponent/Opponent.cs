using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opponent : MonoBehaviour
{
    [SerializeField] float opponentMoveForwardSpeed = .5f;
    [SerializeField] List<Transform> objectsToEscape;
    [SerializeField] float calculateHypotenuse;

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
        //OpponentKeepStayingOnTheWay();

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
            float horizontalDistanceBetweenNextObject = Mathf.Abs(transform.localPosition.x - everyObject.position.x);
            //Debug.Log("yatay mesafe" + horizontalDistanceBetweenNextObject);

            if (verticalDistanceBetweenNextObject <= .3f)
            {
                //Debug.DrawLine(transform.position, everyObject.position, Color.green);
                calculateHypotenuse = Mathf.Sqrt(Mathf.Pow(horizontalDistanceBetweenNextObject, 2) + Mathf.Pow(verticalDistanceBetweenNextObject, 2));

                if (calculateHypotenuse <= .3f && everyObject.position.x <= transform.localPosition.x)
                {
                    Debug.Log("saðdan geçti");
                    transform.Translate(Vector3.right * Time.deltaTime * .5f);
                    continue;
                }
                else if (calculateHypotenuse <= .3f && everyObject.position.x > transform.localPosition.x)
                {
                    transform.Translate(Vector3.left * Time.deltaTime * .3f);
                    continue;
                    Debug.Log("soldan geçti");

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
