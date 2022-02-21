using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Vector2 rawInput;

    [SerializeField] float moveSpeed = 10f;
       
    void Update()
    {
        Vector3 delta = Move();
        transform.position += delta;
    }

    Vector3 Move()
    {
        return rawInput * moveSpeed * Time.deltaTime;
    }

    void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
        Debug.Log(rawInput);
    }

}
