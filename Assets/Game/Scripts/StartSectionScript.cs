using proceduralGeneration;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSectionScript : MonoBehaviour
{
    [SerializeField] float destructionDelay = 10f;

    private void Start()
    {
        RiverGeneration.instance.playerInput.applyPressed.AddListener(StartDestruction);
    }
    public void StartDestruction()
    {
         Destroy(gameObject, destructionDelay);

    }
}
