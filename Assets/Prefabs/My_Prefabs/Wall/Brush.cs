using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Brush : MonoBehaviour
{
    [SerializeField] GameObject brush;
    [SerializeField] float brushSize;
    [SerializeField] TextMeshProUGUI rayX, rayY;
    public bool canPaint;

    Vector3 firstPosition;
    Vector3 secondPosition;
    Vector3 isCursorMoved;
    RaycastHit hit;
    public int counter = 0;
    void Start()
    {
        canPaint = true;
    }
    void Update()
    {

        if (Input.GetMouseButton(0))
        {
            firstPosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            secondPosition = Input.mousePosition;
            isCursorMoved = secondPosition - firstPosition;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (isCursorMoved != firstPosition)
            {
                if (Physics.Raycast(ray, out hit))
                {
                    if (Input.mousePosition.x > 350 && Input.mousePosition.x < 950 && canPaint)
                    {
                        var go = Instantiate(brush, hit.point - transform.position, Quaternion.identity);
                        go.transform.localScale = Vector3.one * brushSize;

                    }
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