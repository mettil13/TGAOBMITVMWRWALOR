using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialCinematicCageController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        proceduralGeneration.RiverGeneration.instance.playerInput.applyPressed.AddListener(DeactivateInitialCinematicCage);
    }

    private void DeactivateInitialCinematicCage() {
        gameObject.SetActive(false);
    }
    
}
