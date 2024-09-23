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
    public float moveSpeed = 10;

    void Update()
    {
        transform.position = transform.position + velocity * Time.deltaTime;
        PlayerMovement();
    }

    void PlayerMovement()
    {
        float upthing = Input.GetAxisRaw("Vertical");
        if (upthing > 0)
        {
            velocity += transform.up * moveSpeed  * Time.deltaTime;
        }
        if (upthing < 0)
        {
            velocity -= transform.up * moveSpeed * Time.deltaTime;
        }
        float sideThing = Input.GetAxisRaw("Horizontal");
        if (sideThing > 0)
        {
            velocity += transform.right * moveSpeed * Time.deltaTime;
        }
        if (sideThing < 0)
        {
            velocity -= transform.right * moveSpeed * Time.deltaTime;
        }
    }

}
