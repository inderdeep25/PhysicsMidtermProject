using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsController : MonoBehaviour
{


    [SerializeField] private Rigidbody m_rb;

    public float speed = 8f;
    private float m_turnSpeed = 100f;

    void ForcesMovement()
    {
        float forwardSpeed = Input.GetAxis("Vertical") * speed;
        // float strafeSpeed = Input.GetAxis("Horizontal") * speed;
        Vector3 forwardAcceleration = forwardSpeed * transform.forward;
        //Vector3 strafeforwarAcceleration = strafeSpeed * transform.right;

        //m_rb.AddForce(forwardAcceleration + strafeforwarAcceleration, ForceMode.Acceleration);
        m_rb.AddForce(forwardAcceleration, ForceMode.Acceleration);
        transform.Rotate(0, Input.GetAxis("Horizontal") * m_turnSpeed * Time.deltaTime, 0);
    }

    void FixedUpdate()
    {
        ForcesMovement();
    }



}