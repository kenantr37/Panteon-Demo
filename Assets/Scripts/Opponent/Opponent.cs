using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opponent : MonoBehaviour
{
    //Opponent movement Process
    [SerializeField] float opponentMoveForwardSpeed = .5f;
    [SerializeField] List<Transform> objectsToEscape;

    //wayPoint Process
    [SerializeField] List<Transform> wayPoints;
    [SerializeField] int wayPointIndex = 0;

    public float calculateHypotenuse;

    //Spaning to starting position of the opponent Process
    [SerializeField] bool _opponentMoveToStartChecker;
    Vector3 _opponentStartingPosition;
    public bool OppoonentStartingPosition { get { return _opponentMoveToStartChecker; } set { _opponentMoveToStartChecker = value; } }

    //Opponent Ranking System
    PlayerMovement playerMovement;
    [SerializeField] bool playerGecti, opponentGecti;
    [SerializeField] int max;
    void Awake()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }
    void Start()
    {
        _opponentStartingPosition = transform.position;
    }
    void Update()
    {
        OpponentMoveForward();
        OpponentMoveLeftRight();
        MoveToStart();
    }
    void OpponentMoveForward()
    {
        int lastWayPointIndex = wayPointIndex;
        transform.position = Vector3.MoveTowards(transform.position, wayPoints[wayPointIndex].position, opponentMoveForwardSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(Vector3.forward);

        if (Vector3.Distance(transform.position, wayPoints[wayPointIndex].position) <= 3f)
        {
            lastWayPointIndex = wayPointIndex;
            wayPointIndex++;

            if (wayPointIndex == wayPoints.Count)
            {
                wayPointIndex = lastWayPointIndex;
                //BURAYA KARAKTERÝ DURDURACAK BÝR ÞEYLER YAZ
            }
        }
    }
    void OpponentMoveLeftRight()
    {
        foreach (Transform everyObject in objectsToEscape)
        {
            float verticalDistanceBetweenNextObject = Mathf.Abs(transform.position.z - everyObject.position.z);
            float horizontalDistanceBetweenNextObject = Mathf.Abs(transform.position.x - everyObject.position.x);

            if (verticalDistanceBetweenNextObject <= 1)
            {
                Debug.DrawLine(transform.position, everyObject.position, Color.green);
                calculateHypotenuse = Mathf.Sqrt(Mathf.Pow(horizontalDistanceBetweenNextObject, 2) + Mathf.Pow(verticalDistanceBetweenNextObject, 2));

                if (calculateHypotenuse <= .5f && everyObject.position.x <= transform.position.x)
                {
                    Debug.DrawLine(transform.position, everyObject.position, Color.red);
                    transform.Translate(Vector3.right * Time.deltaTime * 10);
                }
                else if (calculateHypotenuse <= .5f && everyObject.position.x > transform.position.x)
                {
                    Debug.DrawLine(transform.position, everyObject.position, Color.red);
                    transform.Translate(Vector3.left * Time.deltaTime * 10);
                }
            }
        }
    }
    void MoveToStart()
    {
        if (_opponentMoveToStartChecker)
        {
            transform.position = _opponentStartingPosition;
            _opponentMoveToStartChecker = false;
        }
    }
}