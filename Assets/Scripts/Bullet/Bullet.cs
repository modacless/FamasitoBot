using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D selfRigidbody2D;
    public float lifeTime;
    private float startTime;
    public float speed;
    public Vector2 direction;

    private void Start()
    {
        startTime = Time.time;
    }
    private void Update()
    {
        if (Time.time > startTime + lifeTime) Destroy(transform.gameObject);
    }
    private void FixedUpdate()
    {
        selfRigidbody2D.velocity = direction * speed;
    }



}
