using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] CharacterController2D characterController;
    Vector2 moveInput;
    [SerializeField] float speed = 10;
    bool jump = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        moveInput = GetInput();
        if(moveInput.y > 0)
        {
            jump = true;
        }
    }

    private void FixedUpdate()
    {
        characterController.Move(moveInput.x * speed * Time.fixedDeltaTime, false, jump);
        jump = false;
    }

    Vector2 GetInput()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        input.Normalize();
        return input;
    }
}
