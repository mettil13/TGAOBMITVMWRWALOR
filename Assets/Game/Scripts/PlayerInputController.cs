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

    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    public void OnVertical(InputValue value) {
        Debug.Log("Vertical " + value.Get<float>());
        rb.AddForce(new Vector3(value.Get<float>() * verticalMagnitude, 0, 0));
    }

    public void OnHorizontal(InputValue value) {
        Debug.Log("Horizontal " + value.Get<float>());
        rb.AddForce(new Vector3(value.Get<float>() * horizontalMagnitude, 0, 0));
    }

    public void OnQuit() {
        //Debug.Log("Quit");
        quitPressed.Invoke();
    }

}
