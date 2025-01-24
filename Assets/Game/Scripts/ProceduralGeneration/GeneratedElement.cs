using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace proceduralGeneration
{
    public class GeneratedElement : MonoBehaviour
    {
        [SerializeField] public Transform startPivot;
        [SerializeField] public Transform endPivot;
        [SerializeField] private BoxCollider playerTrigger;
        [SerializeField] private GeneratedElementTrigger trigger;

        private void Awake()
        {
            trigger.elementConnected = this;
        }
    }
}