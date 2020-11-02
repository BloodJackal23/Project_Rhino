using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerControls : MonoBehaviour
{
    Rigidbody2D rb2d;
    float inputX;
    [SerializeField] float speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        inputX = GetHorInput();
    }

    private void FixedUpdate()
    {
        Move(inputX);
    }

    float GetHorInput()
    {
        float input = Input.GetAxisRaw("Horizontal");
        return input;
    }

    void Move(float _inputX)
    {
        if(Mathf.Abs(rb2d.velocity.x) < speed)
        {
            rb2d.velocity += new Vector2(speed * _inputX, rb2d.velocity.y);
        }
    }
}
