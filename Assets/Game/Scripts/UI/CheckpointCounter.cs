using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CheckpointCounter : MonoBehaviour
{
    static public CheckpointCounter instance;
    public TextMeshProUGUI textCounter;
    private int counter = 0;

    private void Awake() {
        if(instance == null) {
            instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }

    public void AddCheckpoint() {
        counter++;
        if (textCounter != null) {
            textCounter.text = counter.ToString();
        }
    }
}
