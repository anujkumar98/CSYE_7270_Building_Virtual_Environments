using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System;
using UnityEngine.SceneManagement;
using System.Threading;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private float movementX;
    private float movementY;
    private float speed = 5; 
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    private float jumpForce = 2.5f;

    private String collectable_tag = "PickUp";
    private int collected_count;
    private int count_collectable_total;

    private float elapsedTime = 0f;

    public TextMeshProUGUI timeText;
    private bool isGameWon = false; 
    void Start()
    {
        collected_count = 0;
        rb = GetComponent <Rigidbody>(); 
        winTextObject.SetActive(false);
        count_collectable_total = GameObject.FindGameObjectsWithTag(collectable_tag).Length;
    }

    // Update is called once per frame
    void OnMove (InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x; 
        movementY = movementVector.y;  
    }
    void SetCountText() 
    {
       countText.text =  "Count: " + collected_count.ToString();
       if (collected_count >= count_collectable_total)
       {
           winTextObject.SetActive(true);
           Destroy(GameObject.FindGameObjectWithTag("Enemy"));
           isGameWon = true; 
       }
    }
    private void FixedUpdate() 
    {
        Vector3 movement = new Vector3 (movementX, 0.0f, movementY);
        rb.AddForce(movement * speed); 
        if (!isGameWon){
            elapsedTime += Time.deltaTime;
            timeText.text =  "Time: " + Math.Round(elapsedTime,2).ToString() + " sec";
        }
        
    }
    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("PickUp")) 
        {
            other.gameObject.SetActive(false);
            collected_count = collected_count + 1;
            SetCountText();
        }

        if (other.gameObject.CompareTag("Danger")) 
        {
            speed = speed + 2;
        }
        
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }

    void OnJump(InputValue jumpValue)
    {
        if (IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
    if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            winTextObject.gameObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
        }

    }

}
