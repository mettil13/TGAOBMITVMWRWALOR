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
                if (worldPoint.sqrMagnitude < 20 * 20) return;
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
        [SerializeField] public int difficultyMultiplier = 1;
        [SerializeField] private ObjectToGenerate[] objectsToGenerate;

        private void Start()
        {
            trigger.elementConnected = this;
            Vector3 direction = Vector3.forward;
            byte c = 0;
            while (c < pivots.pathPoints.Length - 1)
            {
                for(int i = 0; i < difficultyMultiplier; i++) {
                    if (c < pivots.pathPoints.Length - 1)
                    {
                        direction = InterpolatePosition(pivots.pathPoints[c].position, pivots.pathPoints[c + 1].position, (float)(i + 1) / difficultyMultiplier) - InterpolatePosition(pivots.pathPoints[c].position, pivots.pathPoints[c + 1].position, (float)i / difficultyMultiplier);
                    }
                    
                    Vector3 generationPosition = InterpolatePosition(pivots.pathPoints[c].position, pivots.pathPoints[c + 1].position, (float)i / difficultyMultiplier);
                    byte randomObject = ((byte)Random.Range(0, objectsToGenerate.Length));
                    objectsToGenerate[randomObject].GenerateElement(transform, generationPosition, direction, displacementMultiplier, sizeMultiplier);
                }

                c++;
            }
        }

        private Vector3 InterpolatePosition(Vector3 pos1, Vector3 pos2, float progress) { // progress must be a value between 0 and 1
            progress = Mathf.Clamp01(progress);
            return pos1 * (1 - progress) + pos2 * progress;
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