using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag.CompareTo("Player") == 0) {
            CheckpointCounter.instance.AddCheckpoint();
        }
    }

}
