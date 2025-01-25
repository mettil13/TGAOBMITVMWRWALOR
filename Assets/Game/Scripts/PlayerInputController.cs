using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    public float verticalMagnitude = 1;
    public float horizontalMagnitude = 1;

    public UnityEvent quitPressed = new UnityEvent();
    private Rigidbody rb;
    private BubbleBuoiancy bubbleBuoiancy;
    //RiverGeneration

    private void Start() {
        rb = GetComponent<Rigidbody>();
        bubbleBuoiancy = GetComponent<BubbleBuoiancy>();
    }

    private float cachedVertical = 0;
    public void OnVertical(InputValue value) {
        //Debug.Log("Vertical " + value.Get<float>());
        cachedVertical = value.Get<float>();
    }

    private float cachedHorizontal = 0;
    public void OnHorizontal(InputValue value) {
        //Debug.Log("Horizontal " + value.Get<float>());
        cachedHorizontal = value.Get<float>();
    }

    public void Update() {
        Vector3 force = new Vector3(cachedHorizontal * horizontalMagnitude, 0, cachedVertical * verticalMagnitude); // force to apply if the flow direction is forward
        Quaternion rotation = Quaternion.FromToRotation(Vector3.forward, bubbleBuoiancy.flowDirection.normalized); // rotation between forward and the actual flow direction
        Vector3 forceInFlowDirection = rotation * force;

        rb.AddForce(forceInFlowDirection);
    }

    public void OnQuit() {
        //Debug.Log("Quit");
        quitPressed.Invoke();
    }

    private void OnCollisionStay(Collision collision) {
        if(collision.gameObject.layer != LayerMask.NameToLayer("Ground")) {
            return;
        }

        Debug.Log("COLLISION");
        float angle = Vector3.Angle(proceduralGeneration.RiverGeneration.instance.playerNearestPoint.transform.position, collision.GetContact(0).point);
        rb.velocity = rb.velocity.magnitude * angle * Vector3.forward;
        Debug.DrawLine(rb.position, rb.velocity, Color.red);
        Debug.DrawLine(rb.position, proceduralGeneration.RiverGeneration.instance.playerNearestPoint.transform.position, Color.green);
        Debug.Break();
    }

}
