using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : MonoBehaviour
{
    public float rotationSpeed = 1000f;

    private Animator animator;

    public float jumpForce = 8f;

    private bool isJumping = false;

    private Rigidbody rig;

    [SerializeField] private Camera cam;

    public float moveSpeed = 5f;

    
    
    void Start()
    {
        animator = GetComponent<Animator>();
        rig = GetComponent<Rigidbody>();

    }

    
    void Update()
    {
        CameraMove();
        Rotate();

        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            isJumping = true;
            rig.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void FixedUpdate()
    {

        
        if (Input.GetKey(KeyCode.W))
        {
            animator.SetBool("IsWalkingForward", true);

            bool isRunning = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

            if (isRunning)
            {
                animator.SetBool("IsRunning", true);
            }
            else
            {
                animator.SetBool("IsRunning", false);
            }
        }
        else
        {
            animator.SetBool("IsRunning", false);
            animator.SetBool("IsWalkingForward", false);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            animator.SetBool("IsJumping", true);
        }
        else
        {
            animator.SetBool("IsJumping", false);
        }

        if (Input.GetKey(KeyCode.X))
        {
            animator.SetBool("IsCrouchedWalking", true);
        }
        else
        {
            animator.SetBool("IsCrouchedWalking", false);
        }

        if (Input.GetKey(KeyCode.C))
        {
            animator.SetBool("IsSliding", true);
        }
        else
        {
            animator.SetBool("IsSliding", false);
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            animator.SetBool("IsTumbling", true);
        }
        else
        {
            animator.SetBool("IsTumbling", false);
        }

        if (Input.GetKey(KeyCode.A))
        {
            animator.SetBool("IsLeft", true);
        }
        else
        {
            animator.SetBool("IsLeft", false);
        }

        if (Input.GetKey(KeyCode.D))
        {
            animator.SetBool("IsRight", true);
        }
        else
        {
            animator.SetBool("IsRight", false);
        }

        if (Input.GetKey(KeyCode.S))
        {
            animator.SetBool("IsWalkingBackward", true);
        }
        else
        {
            animator.SetBool("IsWalkingBackward", false);
        }
    }

    void CameraMove()
    {
        
        Vector3 cameraForward = cam.transform.forward;
        Vector3 cameraRight = cam.transform.right;
        cameraForward.y = 0f;
        cameraRight.y = 0f;
        cameraForward.Normalize();
        cameraRight.Normalize();

        float x = Input.GetAxis("Horizontal") * moveSpeed;
        float z = Input.GetAxis("Vertical") * moveSpeed;

        Vector3 moveDirection = cameraForward * z + cameraRight * x;
        rig.velocity = new Vector3(moveDirection.x, rig.velocity.y, moveDirection.z);

    }

    void Rotate()
    {
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up, mouseX);
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }
}
