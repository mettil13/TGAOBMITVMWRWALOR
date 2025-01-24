using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerDatas : MonoBehaviour
{
    List<RagdollDamageCollider> bodyParts = new List<RagdollDamageCollider>();

    [SerializeField] float damagePercentage = 0;
    private float convertedParts = 0;

    public bool PercentageIsRight = true;
    

    void Start()
    {
        bodyParts = GetComponentsInChildren<RagdollDamageCollider>().ToList<RagdollDamageCollider>();
        foreach(var part in bodyParts) {
            convertedParts += part.Importance;
            part.playerDatas = this;
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

