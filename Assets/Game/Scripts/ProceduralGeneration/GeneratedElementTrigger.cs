using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace proceduralGeneration
{
    public class GeneratedElementTrigger : MonoBehaviour
    {
        [HideInInspector] public GeneratedElement elementConnected;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                RiverGeneration.instance.SetPlayerPosition(elementConnected);
            }   
        }
    }
}