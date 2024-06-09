using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Cannon : MonoBehaviour
{
    public Vector2 Cannonposition, Mouseposition, direction;
    public GameObject Bullet, Ppoint;
    public GameObject[] points;
    public int no_of_points;
    public bool Trajectory;
    public float FForce, spacebetweenpoints;
    public Transform Point;


    // Start is called before the first frame update
    void Start()
    {
        points = new GameObject[no_of_points];
        for (int i = 0; i < no_of_points; i++)
        {
            points[i] = Instantiate(Ppoint, Point.position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
        Cannonrotate();

        Vector2 Ppointposition(float p)
        {
            Vector2 position = ((Vector2)Point.position + direction.normalized * FForce * p) + 0.5f * Physics2D.gravity * (p * p);
            return position;
        }
        for (int i = 0; i < no_of_points; i++)
        {
            points[i].transform.position = Ppointposition(i * spacebetweenpoints);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Trajectory != true)
            {
                Trajectory = true;
                Ppoint.SetActive(false);
            }
            else
            {
                Trajectory = false;
                Ppoint.SetActive(true);
            }
                
        }
    }
    public void Cannonrotate()
    {
        Cannonposition = transform.position;
        Mouseposition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = Mouseposition - Cannonposition;
        transform.right = direction;
    }
    public void Fire()
    {
        GameObject _Bullet = Instantiate(Bullet, Point.position, Point.rotation);
        _Bullet.GetComponent<Rigidbody2D>().velocity = transform.right * FForce;
    }
}
