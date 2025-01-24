using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{

    public void OnVertical(InputValue value) {
        Debug.Log("Vertical " + value.Get<float>());
    }

    public void OnHorizontal(InputValue value) {
        Debug.Log("Horizontal " + value.Get<float>());
    }

    public void OnQuit() {
        Debug.Log("Quit");
    }

}
