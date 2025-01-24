using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDatas : MonoBehaviour
{
    [SerializeField] float damagePercentage = 0;
    public List<RagdollDamageCollider> bodyParts = new List<RagdollDamageCollider>();
    private float convertedParts = 0;

    public bool PercentageIsRight = true;
    

    void Start()
    {
        foreach(var part in bodyParts) {
            convertedParts += part.Importance;
        }
    }

    private void Update() {
        CalculatePercentage();
    }

    public void CalculatePercentage() {
        if (PercentageIsRight) return;
        damagePercentage = 0;
        foreach (var part in bodyParts) {
            damagePercentage += part.Importance * part.DamagePercentage;
        }
        damagePercentage = Mathf.Clamp(damagePercentage / convertedParts, 0, 100);
        PercentageIsRight = true;
    }
}

