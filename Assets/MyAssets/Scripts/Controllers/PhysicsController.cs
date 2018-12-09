using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsController : MonoBehaviour
{


    [SerializeField] private Rigidbody m_rb;

    public float speed = 8f;
    private readonly float m_turnSpeed = 100f;
    private float forwardSpeed;

    void ForcesMovement()
    {
        forwardSpeed = Input.GetAxis("Vertical") * speed;
        Vector3 forwardAcceleration = forwardSpeed * transform.forward;

        m_rb.AddForce(forwardAcceleration, ForceMode.Acceleration);
        transform.Rotate(0, Input.GetAxis("Horizontal") * m_turnSpeed * Time.deltaTime, 0);
    }

    void FixedUpdate()
    {
        ForcesMovement();
    }



}