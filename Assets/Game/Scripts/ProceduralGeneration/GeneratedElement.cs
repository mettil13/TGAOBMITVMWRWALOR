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

        [SerializeField] public Transform[] pathPoints;

        private void Awake()
        {
            trigger.elementConnected = this;
        }



        public void CustomUpdate()
        {
            // custom update
            BubbleBuoiancy bubble = RiverGeneration.instance.playerBubble;
            
            Transform nearestPoint = pathPoints[0];
            float minDistance = Vector3.Distance(nearestPoint.position, bubble.transform.position);


            byte c = 0;
            foreach (Transform point in pathPoints)
            {
                float currentDistance = Vector3.Distance(point.position, bubble.transform.position);
                if (currentDistance < minDistance)
                {
                    minDistance = currentDistance;
                    nearestPoint = point;
                }
            }

            //bubble.flowDirection = 



            
            //bubble.flowDirection
        }
    }
}