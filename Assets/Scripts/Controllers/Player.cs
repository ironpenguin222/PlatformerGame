using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Player : MonoBehaviour
{
    public List<Transform> asteroidTransforms;
    public Transform enemyTransform;
    public GameObject bombPrefab;
    public Transform bombsTransform;
    private Vector3 velocity = new Vector3(0.001f, 0f);
    public float accelerationtTime = 3;
    public float deceleration = 0.95f;
    public float maxSpeed = 30;
    public float acceleration;

    private void Start()
    {
        acceleration = maxSpeed / accelerationtTime;
    }
    void Update()
    {
        transform.position = transform.position + velocity * Time.deltaTime;
        PlayerMovement();
        Debug.Log(velocity.magnitude);
    }

    void PlayerMovement()
    {
        float upthing = Input.GetAxisRaw("Vertical");
        if (upthing > 0)
        {
            velocity += Vector3.up * acceleration  * Time.deltaTime;
        }
        if (upthing < 0)
        {
            velocity -= Vector3.up * acceleration * Time.deltaTime;
        }
        float sideThing = Input.GetAxisRaw("Horizontal");
        if (sideThing > 0)
        {
            velocity += Vector3.right * acceleration * Time.deltaTime;
        }
        if (sideThing < 0)
        {
            velocity -= Vector3.right * acceleration * Time.deltaTime;
        }

        if(velocity.magnitude > maxSpeed)
        {
            velocity = velocity.normalized * maxSpeed;
        }

        //if (Input.GetAxisRaw("Vertical") == 0)
        //{
        //   velocity *= deceleration;
        //}
        }
    

}
