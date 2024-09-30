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

    public float pushForce = 5000.0f;                // Force applied to push AI cars on collision
   /* public float collisionDisturbanceTime = 2.0f;*/ // How long the AI is disturbed after collision
    /*public float speedReductionFactor = 0.8f; */    // Slowdown factor after a collision
   /* public float speedRecoveryTime = 3.0f; */       // Time to recover speed after a collision

    /*public bool isDisturbed = false; */            // To track if AI is disturbed
 /*   public float disturbanceTimer = 0f;          // Timer for how long AI is disturbed
    public Vector3 disturbanceDirection;*/         // Direction to disturb the AI
    public Rigidbody rb;
    public Transform COM;
    // AI car's Rigidbody component
    /*    public float originalSpeed;                  // Store original speed before the collision
        public float currentSpeed;*/                   // Current speed of the AI

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        /*   originalSpeed = GetComponent<RCC_CarControllerV3>().maxspeed; // Store AI car's original speed
           currentSpeed = originalSpeed;*/
        Time.timeScale = 0.4f;
        Invoke("Jump", 0.25f);
    }

    void Jump()
    {
        rb.AddForce(transform.forward * 5000, ForceMode.Impulse);
        COM.localPosition = Vector3.zero;
        Invoke("time", 0.25f);
    }

    void time()
    {
        COM.localPosition = Vector3.zero;
        Time.timeScale = 1.0f;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("AI")|| collision.gameObject.CompareTag("Player"))
        {
            
            // Apply physical push to the other AI car
            Rigidbody otherRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 pushDirection = collision.contacts[0].normal;
            otherRigidbody.AddForce(-pushDirection * pushForce, ForceMode.Impulse);
            Debug.Log("Add Force");
        }
    }
}
