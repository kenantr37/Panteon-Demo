using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Brush : MonoBehaviour
{
    [SerializeField] GameObject brush;
    [SerializeField] Vector3 brushDistanceBetweenWall;
    [SerializeField] float brushSize;

    Vector3 firstPosition;
    Vector3 secondPosition;
    Vector3 isCursorMoved;

    PlayerMovement playerMovement;
    GameManager gameManager;

    Vector3 lastTouch, firstTouch;
    void Awake()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        gameManager = FindObjectOfType<GameManager>();

    }
    void Update()
    {
        SwerveMouse();
        SwerveMobil();
    }
    void SwerveMouse()
    {
        if (playerMovement.FinishLineArrived && !gameManager.isGameOver)
        {
            if (Input.GetMouseButton(0))
            {
                firstPosition = Input.mousePosition;
            }
            if (Input.GetMouseButton(0))
            {
                secondPosition = Input.mousePosition;
                isCursorMoved = secondPosition - firstPosition;


                if (isCursorMoved != firstPosition)
                {

                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Wall") || hit.collider.CompareTag("Percentage") || hit.collider.CompareTag("Brush"))
                    {
                        var draw = Instantiate(brush, hit.point + transform.parent.localPosition, transform.parent.rotation, transform.parent);
                        draw.transform.localScale = Vector3.one * brushSize;
                    }
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                firstPosition = Vector3.zero;
                secondPosition = Vector3.zero;
            }
        }
    }
    void SwerveMobil()
    {
        if (playerMovement.FinishLineArrived && !gameManager.isGameOver)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                firstTouch.x = touch.position.x;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                lastTouch.x = touch.position.x;
                Vector3 distanceFirstLastPosition = lastTouch - firstTouch;
                transform.Translate(distanceFirstLastPosition.x * playerMovement.MobilScreenSpeed * Time.deltaTime
                    , 0, 0);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                lastTouch = Vector3.zero;
                firstTouch = Vector3.zero;
            }

            if (Input.GetMouseButton(0))
            {
                firstPosition = Input.mousePosition;
            }
            if (Input.GetMouseButton(0))
            {
                secondPosition = Input.mousePosition;
                isCursorMoved = secondPosition - firstPosition;


                if (isCursorMoved != firstPosition)
                {

                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Wall") || hit.collider.CompareTag("Percentage") || hit.collider.CompareTag("Brush"))
                    {
                        var draw = Instantiate(brush, hit.point + transform.parent.localPosition, transform.parent.rotation, transform.parent);
                        draw.transform.localScale = Vector3.one * brushSize;
                    }
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                firstPosition = Vector3.zero;
                secondPosition = Vector3.zero;
            }
        }
    }
}
