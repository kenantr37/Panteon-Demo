using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Brush : MonoBehaviour
{
    [SerializeField] GameObject brush;
    [SerializeField] Vector3 brushDistanceBetweenWall;
    [SerializeField] float brushSize;
    public bool canPaint;

    Vector3 firstPosition;
    Vector3 secondPosition;
    Vector3 isCursorMoved;

    Vector3 lastPosition;
    Vector3 currentPosition;
    PlayerMovement playerMovement;

    void Awake()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }
    void Start()
    {
        canPaint = true;
    }
    void FixedUpdate()
    {

        if (playerMovement.FinishLineArrived)
        {
            if (Input.GetMouseButton(0))
            {
                firstPosition = Input.mousePosition;
                lastPosition = transform.position;

            }
            if (Input.GetMouseButton(0))
            {
                secondPosition = Input.mousePosition;
                isCursorMoved = secondPosition - firstPosition;
                currentPosition = transform.position;


                if (isCursorMoved != firstPosition)
                {

                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Wall") || hit.collider.CompareTag("Percentage"))
                    {

                        var draw = Instantiate(brush, hit.point + transform.parent.localPosition * 500f, transform.parent.rotation, transform.parent);
                        draw.transform.localScale = Vector3.one * brushSize;


                        lastPosition = currentPosition;
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
