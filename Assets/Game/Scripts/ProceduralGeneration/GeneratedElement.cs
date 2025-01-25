using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

namespace proceduralGeneration
{
    public class GeneratedElement : MonoBehaviour
    {
        [System.Serializable] public struct ObjectToGenerate
        {
            [SerializeField] bool doNotGenerate;
            [SerializeField] GameObject prefab;
            [SerializeField] float maxDisplacement;
            [SerializeField] Vector3 offset;
            [SerializeField] Vector2 minMaxScaleFactor;

            public void GenerateElement(Transform elementFather, Vector3 worldPoint, Vector3 direction, float displacementMultiplier, float sizeMultiplier)
            {
                if (doNotGenerate) return;

                Vector3 positionDirection = Quaternion.AngleAxis(90, Vector3.up) * direction;
                GameObject generatedObj = GameObject.Instantiate(prefab, elementFather);
                generatedObj.transform.position = worldPoint + offset;

                float displacement = Random.Range(-maxDisplacement, maxDisplacement);
                generatedObj.transform.position += positionDirection.normalized * displacement * displacementMultiplier;
                float scaleFactor = Random.Range(minMaxScaleFactor.x, minMaxScaleFactor.y);
                generatedObj.transform.localScale = Vector3.one * scaleFactor * sizeMultiplier;
            }
        }

        [SerializeField] public Transform startPivot;
        [SerializeField] public Transform endPivot;
        [SerializeField] private BoxCollider playerTrigger;
        [SerializeField] private GeneratedElementTrigger trigger;
        [SerializeField] private GeneratedElementTriggerForPivots pivots;
        [SerializeField] private float displacementMultiplier;
        [SerializeField] private float sizeMultiplier;
        [SerializeField] private ObjectToGenerate[] objectsToGenerate;

        private void Awake()
        {
            trigger.elementConnected = this;
            Vector3 direction = Vector3.forward;
            byte c = 0;
            while (c < pivots.pathPoints.Length)
            {
                if (c <  pivots.pathPoints.Length - 1)
                {
                    direction = pivots.pathPoints[c + 1].position - pivots.pathPoints[c].position;
                }
                Vector3 generationPosition = pivots.pathPoints[c].position;
                byte randomObject = ((byte)Random.Range(0, objectsToGenerate.Length));
                objectsToGenerate[randomObject].GenerateElement(transform, generationPosition, direction, displacementMultiplier, sizeMultiplier);
                c++;
            }
        }
        public void CustomUpdate()
        {
            // custom update
            //BubbleBuoiancy bubble = RiverGeneration.instance.playerBuoiancy;


            //byte nearestPointIndex = 0;
            //float minDistance =
            //    Vector3.Distance(pathPoints[nearestPointIndex].transform.position, bubble.transform.position);

            //byte c = 0;
            //while (c < pathPoints.Length)
            //{
            //    float currentDistance = Vector3.Distance(pathPoints[c].position, bubble.transform.position);
            //    if (currentDistance < minDistance)
            //    {
            //        minDistance = currentDistance;
            //        nearestPointIndex = c;
            //    }
            //    c++;
            //}
            //Debug.LogError("E' entrato nearest " + pathPoints[nearestPointIndex].name + " , " + pathPoints[nearestPointIndex].transform.position);

            //RiverGeneration.instance.playerNearestPoint = pathPoints[nearestPointIndex];

            //if (nearestPointIndex < pathPoints.Length - 1)
            //{
            //    bubble.flowDirection = pathPoints[nearestPointIndex + 1].position - pathPoints[nearestPointIndex].position;
            //}
        }
    }
}