using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement Process")]
    [SerializeField] float _playerMoveForwardSpeed = 1f;
    [SerializeField] float _mouseSpeed = 2.5f;
    [SerializeField] float _mobilScreenSpeed = 0.005f;
    [SerializeField] Vector3 swerveFirstPosition, swerveLastPosition, distanceFirstLastPosition;

    [Header("Wall Process")]
    [SerializeField] Renderer wall;
    [SerializeField] GameObject wallScaleSize;
    [SerializeField] float paintedWallRatio;
    float transparencyRation;

    [Header("Finish Line Checker")]
    [Tooltip("Is player on the finish way before the paint-wall ?")]
    [SerializeField] bool _finishLineArrived;

    [Header("Player Ranking")]
    [SerializeField] int _playerCurrentRank;
    [SerializeField] int playerFirstRank;
    Opponent[] opponents;

    public bool FinishLineArrived { get { return _finishLineArrived; } set { _finishLineArrived = value; } }
    public float PlayerMoveForwardSpeed { get { return _playerMoveForwardSpeed; } set { _playerMoveForwardSpeed = value; } }
    public float MouseSpeed { get { return _mouseSpeed; } set { _mouseSpeed = value; } }
    public float MobilScreenSpeed { get { return _mobilScreenSpeed; } set { _mobilScreenSpeed = value; } }
    public int PlayerCurrentRank { get { return _playerCurrentRank; } set { _playerCurrentRank = value; } }

    void Awake()
    {
        opponents = FindObjectsOfType<Opponent>();
    }
    void Start()
    {
        float startPos = Input.mousePosition.x;
        _finishLineArrived = false;
        transparencyRation = 0f;

        PlayerFirsRank();
        Debug.Log("starting rank : " + playerFirstRank);
    }

    void Update()
    {
        if (FinishLineArrived)
        {
            PaintTheWallMouse();
            PaintTheWallMobil();
        }
        else
        {
            MoveForward();
            SwerveMechanicMouse(_mouseSpeed);
            SwerveMechanicMobil(_mobilScreenSpeed);
        }
    }
    void MoveForward()
    {
        Vector3 playerForward = Vector3.forward * _playerMoveForwardSpeed * Time.deltaTime;
        transform.position = transform.position + playerForward;
        transform.rotation = Quaternion.Euler(Vector3.forward);
    }
    void SwerveMechanicMouse(float mouseSpeed)
    {
        if (Input.GetMouseButtonDown(0))
        {
            swerveFirstPosition.x = Input.mousePosition.x;
        }
        else if (Input.GetMouseButton(0))
        {
            swerveLastPosition.x = Input.mousePosition.x;
            distanceFirstLastPosition = swerveLastPosition - swerveFirstPosition;
            transform.Translate(distanceFirstLastPosition.x * Time.deltaTime * mouseSpeed * Time.deltaTime
                , 0, 0);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            swerveFirstPosition = Vector3.zero;
            swerveLastPosition = Vector3.zero;
        }
    }
    void SwerveMechanicMobil(float mobilScreenSpeed)
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                swerveFirstPosition.x = touch.position.x;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                swerveLastPosition.x = touch.position.x;
                distanceFirstLastPosition = swerveLastPosition - swerveFirstPosition;
                transform.Translate(distanceFirstLastPosition.x * mobilScreenSpeed * Time.deltaTime
                    , 0, 0);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                swerveLastPosition = Vector3.zero;
                swerveLastPosition = Vector3.zero;
            }
        }
    }
    void PaintTheWallMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            swerveFirstPosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            swerveLastPosition = Input.mousePosition;
            if (swerveLastPosition != swerveFirstPosition)
            {
                wall.material.color = new Color(1, 0, 0, Mathf.Lerp(0f, 1f, transparencyRation));
                transparencyRation += 0.0005f;
                paintedWallRatio = Mathf.RoundToInt(wall.material.color.a * 100);

                wallScaleSize.transform.parent.localScale = new Vector3(1, Mathf.Lerp(0, 1, transparencyRation), 1);
                Debug.Log("You've just painted %" + paintedWallRatio + " of the wall!");
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            swerveFirstPosition = Vector3.zero;
            swerveLastPosition = Vector3.zero;
        }
    }
    void PaintTheWallMobil()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                swerveFirstPosition.x = touch.position.x;
            }
            if (touch.phase == TouchPhase.Moved)
            {
                if (swerveLastPosition != swerveFirstPosition)
                {
                    wall.material.color = new Color(1, 0, 0, Mathf.Lerp(0f, 1f, transparencyRation));
                    transparencyRation += 0.0005f;
                    paintedWallRatio = Mathf.RoundToInt(wall.material.color.a * 100);

                    wallScaleSize.transform.parent.localScale = new Vector3(1, Mathf.Lerp(0, 1, transparencyRation), 1);
                    Debug.Log("You've just painted %" + paintedWallRatio + " of the wall!");
                }
            }
            if (touch.phase == TouchPhase.Ended)
            {
                swerveLastPosition = Vector3.zero;
                swerveLastPosition = Vector3.zero;
            }
        }
    }
    void PlayerFirsRank()
    {
        playerFirstRank = 1;

        foreach (Opponent opponent in opponents)
        {
            if (transform.position.z <= opponent.transform.position.z)
            {
                playerFirstRank++;
            }
        }
        _playerCurrentRank = playerFirstRank;
    }

}