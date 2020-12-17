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
///                                       Added the functions of shirinking the size and back to original size 
/// </summary>

public class FloatingPlatform : MonoBehaviour
{
    #region Variables
    [Header("Floating Positions")]
    public float yPosition01;
    public float yPosition02;
    public float moveSpeed;
    bool moveDown;

    [Header("Floating Time")]
    public float fixedTime = 2.0f;

    [Header("Sounds")]
    public AudioSource audioSource;
    public AudioClip audioClip_Shrinking;
    public AudioClip audioClip_BackToOriginal;

    #endregion
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        // set boolean according to the y positions
        if (transform.position.y > yPosition01)
            moveDown = true;
        if (transform.position.y < yPosition02)
            moveDown = false;

        // actual moving platforms 
        if (moveDown)
            transform.position = new Vector2(transform.position.x, transform.position.y - moveSpeed * Time.deltaTime);
        else
            transform.position = new Vector2(transform.position.x, transform.position.y + moveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StopAllCoroutines();

            //Start shrink
            StartCoroutine(ShrinkCoroutine());

            //Play Sound
            audioSource.clip = audioClip_Shrinking;
            audioSource.Play();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StopAllCoroutines();

            //Start go back to original size
            StartCoroutine(BackToOriginalCoroutine());

            //Play Sound
            audioSource.clip = audioClip_BackToOriginal;
            audioSource.Play();

        }
    }

    IEnumerator ShrinkCoroutine()
    {
        float shirinkingTime = 0.0f;
        Vector3 platformVector = transform.localScale;

        // until shiriktime is over fixed time.
        while (shirinkingTime < fixedTime)
        {
            shirinkingTime += Time.deltaTime;
            platformVector.x = Mathf.Lerp(platformVector.x, 0.0f, shirinkingTime / fixedTime);
            transform.localScale = new Vector2(platformVector.x, platformVector.y);

            yield return null;
        }

        // make disappear
        transform.localScale = new Vector3(0.0f, 0.0f,0.0f);
    }

    IEnumerator BackToOriginalCoroutine()
    {
        float shirinkingTime = 0;
        Vector3 platformVector = transform.localScale;

        // until shiriktime is over fixed time.
        while (shirinkingTime < fixedTime)
        {
            shirinkingTime += Time.deltaTime;
            platformVector.x = Mathf.Lerp(platformVector.x, 1.0f, shirinkingTime / fixedTime);
            transform.localScale = new Vector2(platformVector.x, platformVector.y);

            yield return null;
        }

        // back to original size
        transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }

}