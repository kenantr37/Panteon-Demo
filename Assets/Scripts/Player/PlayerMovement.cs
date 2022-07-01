using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float _playerMoveForwardSpeed = 1f;
    [SerializeField] float _swerveSpeed = 2.5f;
    [SerializeField] Vector3 swerveFirstPosition, swerveLastPosition, distanceFirstLastPosition;

    [SerializeField] Renderer wall;
    [SerializeField] float paintedWallRatio;
    float transparencyRation;

    [SerializeField] bool _finishLineArrived;
    public bool FinishLineArrived { get { return _finishLineArrived; } set { _finishLineArrived = value; } }
    public float PlayerMoveForwardSpeed { get { return _playerMoveForwardSpeed; } set { _playerMoveForwardSpeed = value; } }
    public float SwerveSpee { get { return _swerveSpeed; } set { _swerveSpeed = value; } }

    void Start()
    {
        float startPos = Input.mousePosition.x;
        _finishLineArrived = false;
        transparencyRation = 0f;
    }
    void Update()
    {
        if (FinishLineArrived)
        {
            PaintTheWall();
        }
        else
        {
            MoveForward();
            SwerveMechanicMouse();
            SwerveMechanicMobil();
        }
    }
    void MoveForward()
    {
        Vector3 playerForward = Vector3.forward * _playerMoveForwardSpeed * Time.deltaTime;
        transform.position = transform.position + playerForward;
        transform.rotation = Quaternion.Euler(Vector3.forward);
    }
    void SwerveMechanicMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            swerveFirstPosition.x = Input.mousePosition.x;
        }
        else if (Input.GetMouseButton(0))
        {
            swerveLastPosition.x = Input.mousePosition.x;
            distanceFirstLastPosition = swerveLastPosition - swerveFirstPosition;
            transform.Translate(distanceFirstLastPosition.x * Time.deltaTime * _swerveSpeed * Time.deltaTime
                , 0, 0);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            swerveFirstPosition = Vector3.zero;
            swerveLastPosition = Vector3.zero;
        }
    }
    void SwerveMechanicMobil()
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
                transform.Translate(distanceFirstLastPosition.x * _swerveSpeed * Time.deltaTime
                    , 0, 0);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                swerveLastPosition = Vector3.zero;
                swerveLastPosition = Vector3.zero;
            }
        }
    }
    void PaintTheWall()
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
                transparencyRation += 0.001f;
                paintedWallRatio = Mathf.RoundToInt(wall.material.color.a * 100);
                Debug.Log(transparencyRation + "<->" + wall.material.color.a);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            swerveFirstPosition = Vector3.zero;
            swerveLastPosition = Vector3.zero;
        }
    }
}
