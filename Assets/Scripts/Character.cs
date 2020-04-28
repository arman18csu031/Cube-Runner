using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float Speed = 5.0f;
    public float jumpForce = 5.0F;
    public Rigidbody2D rigibody;

    private bool CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.5F);
        if (colliders.Length > 1)
            return true;
        else
            return false;
    }
    public void FreezeZ()
    {
        rigibody.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    private void Jump()
    {
        rigibody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }
    private void Attraction()
    {
        rigibody.AddForce(-transform.up * jumpForce, ForceMode2D.Impulse);
    }
    private void Run()
    {
        Vector3 direction = transform.right;
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, Speed * Time.deltaTime);
    }
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (CheckGround()))
            Jump();
        if (Input.GetKeyDown(KeyCode.DownArrow))
            Attraction();
        Run();	
	}
}
