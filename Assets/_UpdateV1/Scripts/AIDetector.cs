using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDetector : MonoBehaviour
{
    /*// Start is called before the first frame update
    public Transform parentTrans;
    void Start()
    {
        *//*rb = GetComponent<Rigidbody>();
        parentTrans = GetComponentInParent<Transform>();*//*
        parentTrans = GetComponent<Transform>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Assist")
        {
            Rigidbody rb = other.gameObject.GetComponentInParent<Rigidbody>();
            rb.AddForce((parentTrans.right * 5000f));
        }
    }*/

    public float pushForce = 5.0f;                // Force applied to push AI cars on collision
    public float collisionDisturbanceTime = 2.0f; // How long the AI is disturbed after collision
    public float speedReductionFactor = 0.8f;     // Slowdown factor after a collision
    public float speedRecoveryTime = 3.0f;        // Time to recover speed after a collision

    public bool isDisturbed = false;             // To track if AI is disturbed
    public float disturbanceTimer = 0f;          // Timer for how long AI is disturbed
    public Vector3 disturbanceDirection;         // Direction to disturb the AI
    public Rigidbody rb;                         // AI car's Rigidbody component
    public float originalSpeed;                  // Store original speed before the collision
    public float currentSpeed;                   // Current speed of the AI

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalSpeed = GetComponent<RCC_CarControllerV3>().maxspeed; // Store AI car's original speed
        currentSpeed = originalSpeed;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("AI"))
        {
            
            // Apply physical push to the other AI car
            Rigidbody otherRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            otherRigidbody.GetComponent<RCC_AICarController>().enabled = false;
            otherRigidbody.GetComponent<RCC_CarControllerV3>().enabled = false;
            Vector3 pushDirection = collision.contacts[0].normal;
            otherRigidbody.AddForce(-pushDirection * pushForce, ForceMode.Impulse);

            // Start disturbance behavior
            isDisturbed = true;
            disturbanceTimer = collisionDisturbanceTime;

            // Determine a random direction for the disturbance
            disturbanceDirection = Random.insideUnitSphere;
            disturbanceDirection.y = 0; // Keep disturbance horizontal (ignore y-axis)

            // Reduce AI's speed temporarily
            currentSpeed *= speedReductionFactor;
            GetComponent<RCC_CarControllerV3>().speed = currentSpeed;
        }
    }

    void Update()
    {
        if (isDisturbed)
        {
            disturbanceTimer -= Time.deltaTime;

            // Apply a temporary disturbance in steering
            float disturbanceStrength = disturbanceDirection.x * 0.5f; // You can adjust the multiplier to control the amount of steering disturbance
            GetComponent<RCC_CarControllerV3>().orgSteerAngle = (disturbanceStrength);

            GetComponent<RCC_CarControllerV3>().throttleInput = 0f;
            GetComponent<RCC_CarControllerV3>().brakeInput = 1f;

            // Gradually recover AI's speed over time
            currentSpeed = Mathf.Lerp(currentSpeed, originalSpeed, Time.deltaTime / speedRecoveryTime);
            GetComponent<RCC_CarControllerV3>().speed = currentSpeed;

            // End disturbance after the timer expires
            if (disturbanceTimer <= 0)
            {
                isDisturbed = false;
                disturbanceDirection = Vector3.zero;
                GetComponent<RCC_CarControllerV3>().maxspeed = originalSpeed; // Restore original speed

                GetComponent<RCC_CarControllerV3>().throttleInput = 0f;
                GetComponent<RCC_CarControllerV3>().brakeInput = 1f;

                GetComponent<RCC_AICarController>().enabled = true;
                GetComponent<RCC_CarControllerV3>().enabled = true;
            }
        }
    }

}
