using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

namespace proceduralGeneration
{
    public class GeneratedElement : MonoBehaviour
    {
        [SerializeField] public Transform startPivot;
        [SerializeField] public Transform endPivot;
        [SerializeField] private BoxCollider playerTrigger;
        [SerializeField] private GeneratedElementTrigger trigger;

        [SerializeField] public Transform[] pathPoints;

        private void Awake()
        {
            trigger.elementConnected = this;
        }
        public void CustomUpdate()
        {
            // custom update
            BubbleBuoiancy bubble = RiverGeneration.instance.playerBuoiancy;
            
            byte nearestPointIndex = 0;
            Transform nearestPoint = pathPoints[nearestPointIndex];
            float minDistance = Vector3.Distance(nearestPoint.position, bubble.transform.position);

            byte c = 0;
            while (c < pathPoints.Length)
            {
                float currentDistance = Vector3.Distance(pathPoints[c].position, bubble.transform.position);
                if (currentDistance < minDistance)
                {
                    minDistance = currentDistance;
                    nearestPointIndex = c;
                    nearestPoint = pathPoints[nearestPointIndex];
                }
                c++;
            }

            if (c < pathPoints.Length - 1)
            {
                bubble.flowDirection = pathPoints[c + 1].position - pathPoints[c].position;
            }
        }
    }
}