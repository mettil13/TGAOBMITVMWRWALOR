using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace proceduralGeneration
{
    public class RiverGeneration : MonoBehaviour
    {
        public static RiverGeneration instance;

        [System.Serializable] public struct ElementToGenerate
        {
            [SerializeField] public GeneratedElement prefab;
        }

        [Header("Generation prefs")]
        [SerializeField] private List<ElementToGenerate> elementsToGenerate;
        [SerializeField] private GameObject playerPref;

        [Header("Element generated")]
        [SerializeField] public GameObject playerObj;
        [SerializeField] public BubbleBuoiancy playerBuoiancy;
        [SerializeField] public PlayerInputController playerInput;
        [SerializeField] public PlayerDatas playerDatas;
        // first inserted = 0,
        // second inserted = 1,
        // latest inserted = last,
        // etc...
        [SerializeField] private List<GeneratedElement> generatedElements;

        [Header("Properties")]
        [SerializeField] private Transform riverContainer;
        [SerializeField] private byte minNumberOfElementGenerated = 3;
        [SerializeField] private byte maxNumberOfElementGenerated = 6;
        [SerializeField] private float timeToCheckElementToRemove = 0.5f;
        [SerializeField] private float timeToRemovePreviousElement = 3f;
        [SerializeField] public GeneratedElement playerPosition;
        [SerializeField] public GeneratedElementTriggerForPivots lastTrigger;
        [SerializeField] public Transform playerNearestPoint;
        //PlayerInputController
        //PlayerDatas

        public GeneratedElement lastElement => generatedElements[0];
        public GeneratedElement lastInsertedElement => generatedElements[generatedElements.Count - 1];

        private float timeCounterToCheckElementToRemove = 0;

        private void Awake()
        {
            instance = this;
            GenerateNextElement();

            // generate player

            playerObj = GameObject.Instantiate(playerPref);
            playerBuoiancy = playerObj.GetComponent<BubbleBuoiancy>();
            playerInput = playerObj.GetComponent<PlayerInputController>();
            playerDatas = playerObj.GetComponent<PlayerDatas>();
            playerObj.transform.position = Vector3.zero + Vector3.up * 10;
        }
        private void Update()
        {
            timeCounterToCheckElementToRemove += Time.deltaTime;
            if (timeCounterToCheckElementToRemove >= timeToCheckElementToRemove)
            {
                timeCounterToCheckElementToRemove = 0;
                if (generatedElements.Count <= minNumberOfElementGenerated) GenerateNextElement();
                if (playerPosition != generatedElements[0]) StartCoroutine(RemoveElementBeforePlayer(generatedElements[0]));
            }

            if (playerPosition != null)
            {
                playerPosition.CustomUpdate();
            }
            if (lastTrigger != null)
            {
                lastTrigger.CustomUpdate();
            }
        }
        public void GenerateNextElement()
        {
            // random generation of the element
            GeneratedElement element =
                GameObject.Instantiate(elementsToGenerate[
                    Random.Range(0, elementsToGenerate.Count - 1)
                    ].prefab.gameObject).GetComponent<GeneratedElement>();
            element.transform.parent = riverContainer;
            
            if (generatedElements.Count == 0)
            {
                element.transform.position = Vector3.zero;
                generatedElements.Add(element);
                playerPosition = element;
                return;
            }

            element.transform.position = lastInsertedElement.endPivot.position;
            element.transform.eulerAngles = lastInsertedElement.endPivot.eulerAngles + Vector3.up * -90;
            generatedElements.Add(element);
        }
        public void RemoveElement(GeneratedElement elementToRemove, bool destroyElement)
        {
            if (!generatedElements.Contains(elementToRemove)) return;
            generatedElements.Remove(elementToRemove);
            if (destroyElement) GameObject.Destroy(elementToRemove.gameObject);
        }
       
        public void RemoveLastElement()
        {
            if (generatedElements.Count <= minNumberOfElementGenerated) return;
            if (generatedElements[0] == playerPosition) return;
            RemoveElement(generatedElements[0], true);
        }
        public void SetPlayerPosition(GeneratedElement position)
        {
            playerPosition = position;
        }
        private IEnumerator RemoveElementBeforePlayer(GeneratedElement elementToRemove)
        {
            RemoveElement(elementToRemove, false);
            yield return new WaitForSecondsRealtime(timeToRemovePreviousElement);
            //RemoveElement(elementToRemove);
            if (elementsToGenerate == null) yield break;
            GameObject.Destroy(elementToRemove.gameObject);
        }
    }
}
