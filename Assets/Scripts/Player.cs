using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Vector2 rawInput;
    float fireInput;
    [SerializeField] float moveSpeed = 10f;

    [SerializeField] GameObject laserPrefab;
    [SerializeField] float laserSpeed = 50f;
    [SerializeField] float projectileFiringPeriod = 0.5f;

    Coroutine firingCoroutine;


    // SET THE PLAYER MOVEMENT BOUNDARY VARIABLES
    Vector2 minBounds;
    Vector2 maxBounds;
    [SerializeField] float paddingLeft;
    [SerializeField] float paddingRight;
    [SerializeField] float paddingTop;
    [SerializeField] float paddingBottom;

    
    void Start() 
    {
        InitBounds();
    }
       
    void Update()
    {
        Move();
    }

    void Move()
    {
        Vector2 delta = rawInput * moveSpeed * Time.deltaTime;
        Vector2 newPos = new Vector2();
        newPos.x = Mathf.Clamp(transform.position.x + delta.x, minBounds.x + paddingLeft, maxBounds.x - paddingRight);
        newPos.y = Mathf.Clamp(transform.position.y + delta.y, minBounds.y + paddingBottom, maxBounds.y - paddingTop);
        transform.position = newPos;;
    }

    void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
        //Debug.Log(rawInput);
    }

    void OnFire(InputValue value)
    {
        if ( value.isPressed ) 
        {
            firingCoroutine =  StartCoroutine(FireContinuously());
        }
        if ( !value.isPressed )
        {
            StopCoroutine(firingCoroutine);
        }
    }
     IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserSpeed);
            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }

    void InitBounds()
    {
        Camera mainCamera = Camera.main;
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0,0));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1,1));
    }

}
