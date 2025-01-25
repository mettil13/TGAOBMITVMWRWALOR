using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class PlayerDatas : MonoBehaviour
{
    List<RagdollDamageCollider> bodyParts = new List<RagdollDamageCollider>();

    [SerializeField] float damagePercentage = 0;
    private float convertedParts = 0;

    public bool PercentageIsRight = true;
    public TextMeshProUGUI uiPercentage;

    void Start()
    {
        DOTween.SetTweensCapacity(200,100);
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
        PercentageIsRight = true;

        damagePercentage = 0;
        foreach (var part in bodyParts) {
            damagePercentage += part.Importance * part.DamagePercentage;
        }
        damagePercentage = Mathf.Clamp(damagePercentage / convertedParts, 0, 100);
        if (damagePercentage >= 95)
            damagePercentage = 100;

        UpdateUIText();

        if(damagePercentage >= 95) {
            PagesManager.instance.OpenPage(Pages.Loose.ToString());
        }
    }

    private void UpdateUIText() {
        int showedPercentage = (int)damagePercentage;
        uiPercentage.text = showedPercentage.ToString() + "%";
        //effetti grafici carini
        
    }
}

