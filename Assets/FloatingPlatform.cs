using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///Student name : Junho Kim
///Student ID : 101136986
///Source file name : FloatingPlatform.cs
///Date last modified : December 17, 2020
///Program description : To control floating platforms
///Revision history : December 17, 2020 : Added the functions of moving up and down 
/// </summary>

public class FloatingPlatform : MonoBehaviour
{
    public float yPosition01;
    public float yPosition02;
    public float moveSpeed;
    bool moveDown;

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > yPosition01)
            moveDown = true;
        if (transform.position.y < yPosition02)
            moveDown = false;

        if (moveDown)
            transform.position = new Vector2(transform.position.x, transform.position.y - moveSpeed * Time.deltaTime);
        else
            transform.position = new Vector2(transform.position.x, transform.position.y + moveSpeed * Time.deltaTime);
    }
}