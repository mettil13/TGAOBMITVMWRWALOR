using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace proceduralGeneration
{
    public class GeneratedElementTriggerForPivots : MonoBehaviour
    {
        [SerializeField] public Transform[] pathPoints;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                RiverGeneration.instance.lastTrigger = this;
            }   
        }
        public void CustomUpdate()
        {
            // custom update
            BubbleBuoiancy bubble = RiverGeneration.instance.playerBuoiancy;


            byte nearestPointIndex = 0;
            float minDistance =
                Vector3.Distance(pathPoints[nearestPointIndex].transform.position, bubble.transform.position);

            byte c = 0;
            while (c < pathPoints.Length)
            {
                float currentDistance = Vector3.Distance(pathPoints[c].position, bubble.transform.position);
                if (currentDistance < minDistance)
                {
                    minDistance = currentDistance;
                    nearestPointIndex = c;
                }
                c++;
            }
            //Debug.LogError("E' entrato nearest " + pathPoints[nearestPointIndex].name + " , " + pathPoints[nearestPointIndex].transform.position);

            RiverGeneration.instance.playerNearestPoint = pathPoints[nearestPointIndex];

            if (nearestPointIndex < pathPoints.Length - 1)
            {
                bubble.flowDirection = pathPoints[nearestPointIndex + 1].position - pathPoints[nearestPointIndex].position;
            }
        }
    }
}