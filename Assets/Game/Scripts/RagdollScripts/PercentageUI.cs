using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using proceduralGeneration;
using TMPro;

public class PercentageUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RiverGeneration.instance.playerDatas.uiPercentage = this.GetComponent<TextMeshProUGUI>();
    }

}
