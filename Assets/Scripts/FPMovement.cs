using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPMovement : MonoBehaviour
{
    private Transform characterTransform;
    public float Speed;
    public float JumpHeight;
    public float Gravity;
    private Rigidbody characterRigidbody;
    private bool isGrounded = true;
    // private int Ground;
    
    private void Start()
    {
        characterTransform = transform;
        characterRigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // isGrounded = Physics.CheckSphere(characterTransform.position, 20.0f, 0);
        if (isGrounded){
            var tmp_Horizontal = Input.GetAxis("Horizontal");
            var tmp_Vertical = Input.GetAxis("Vertical");
            
            var tmp_CurrentDirection = new Vector3(tmp_Horizontal, 0, tmp_Vertical);
            tmp_CurrentDirection = characterTransform.TransformDirection(tmp_CurrentDirection);
            tmp_CurrentDirection *= Speed;
            
            var tmp_CurrentVelocity = characterRigidbody.velocity;
            var tmp_VelocityChange = tmp_CurrentDirection - tmp_CurrentVelocity;
            tmp_VelocityChange.y = 0;
            
            characterRigidbody.AddForce(tmp_VelocityChange, ForceMode.VelocityChange);
            
            if (Input.GetButtonDown("Jump"))
            {
                characterRigidbody.velocity = new Vector3(tmp_CurrentVelocity.x, CalculateJumpHeightSpeed(), tmp_CurrentVelocity.z);
            }
        }
        characterRigidbody.AddForce(new Vector3(0, -Gravity * characterRigidbody.mass, 0));
        // Debug.Log("isGrounded:" + isGrounded);
    }
    
    private float CalculateJumpHeightSpeed()
    {
        return Mathf.Sqrt(2 * Gravity * JumpHeight);
    }
    
    private void OnCollisionStay(Collision collisionInfo)
    {
        isGrounded = true;
    }
    
    private void OnCollisionExit(Collision collisionInfo)
    {
        // foreach (ContactPoint contact in collisionInfo.contacts)
        // {
        //     Debug.DrawRay(contact.point, contact.normal * 10, Color.white);
        // }
        // Debug.Log("OnCollisionExit");
        isGrounded = false;
    }


    // void OnTriggerStay(Collider other)
    // {
    //     Debug.Log(other.tag);
    //     if (other.tag == "Ground")
    //     {
    //         isGrounded = true;
    //         Debug.Log("Grounded");
    //     }
    //     else
    //     {
    //         isGrounded = false;
    //         Debug.Log("Not Grounded!");
    //     }
    // }
}
