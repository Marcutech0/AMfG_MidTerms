using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CannonController : MonoBehaviour
{
    private Vector2 CannonPosition;
    private Vector2 MousePosition;
    private Vector2 direction;

    public GameObject CannonBall;
    public float Power;
    public Transform FirePoint;

    public GameObject point;
    public GameObject[] points;
    public int numberofPoints;
    public float spaceBetweenPoints;

    void Start()
    {
        points = new GameObject[numberofPoints];
        for (int i = 0; i < numberofPoints; i++)
        {
            points[i] = Instantiate(point, FirePoint.position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        CannonRotate();

        if(Input.GetMouseButtonDown(0)) 
        {
            FireCannon();
        }

        Vector2 PointPosition(float p)
        {
            Vector2 position = ((Vector2)FirePoint.position + direction.normalized * Power * p) + 0.5f * Physics2D.gravity * (p * p);
            return position;
        }

        for (int i = 0; i < numberofPoints; i++)
        {
            points[i].transform.position = PointPosition(i * spaceBetweenPoints);
        }
    }

    private void CannonRotate()
    {
        CannonPosition = transform.position;
        MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = MousePosition - CannonPosition;
        transform.right = direction;
    }

    private void FireCannon()
    {
        GameObject _CannonBall = Instantiate(CannonBall, FirePoint.position, FirePoint.rotation);
        _CannonBall.GetComponent<Rigidbody2D>().velocity = transform.right * Power;
    }
}
