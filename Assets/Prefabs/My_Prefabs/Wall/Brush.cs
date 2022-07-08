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
    void Awake()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        gameManager = FindObjectOfType<GameManager>();

    }
    void Update()
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
}
