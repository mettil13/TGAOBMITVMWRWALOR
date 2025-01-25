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

    public TextMeshProUGUI uiPercentage;

    [SerializeField] AnimationCurve curveFont;

    void Start()
    {
        DOTween.SetTweensCapacity(200,100);
        bodyParts = GetComponentsInChildren<RagdollDamageCollider>().ToList<RagdollDamageCollider>();
        foreach(var part in bodyParts) {
            convertedParts += part.Importance;
            part.playerDatas = this;
        }
    }


    //public void CalculatePercentage() {
    //    if (PercentageIsRight) return;
    //    PercentageIsRight = true;

    //    damagePercentage = 0;
    //    foreach (var part in bodyParts) {
    //        damagePercentage += part.Importance * part.PartPercentage;
    //    }
    //    damagePercentage = Mathf.Clamp(damagePercentage / convertedParts, 0, 100);
    //    if (damagePercentage >= 95)
    //        damagePercentage = 100;

    //    UpdateUIText();

    //    if (damagePercentage >= 95) {
    //        PagesManager.instance.OpenPage(Pages.Loose.ToString());
    //    }
    //}

    private void UpdateUIText() {
        int showedPercentage = (int)damagePercentage;
        uiPercentage.text = showedPercentage.ToString() + "%";

        TweenColor();
        TweenFontSize();
    }

    private void TweenColor() {
        //Sequence myColorSequence = DOTween.Sequence();
        //Tweener ToRed = uiPercentage.material.DOColor(Color.red, 0.2f).SetEase(Ease.OutQuint);
        //Tweener ToWhite = uiPercentage.material.DOColor(Color.white, 0.4f).SetEase(Ease.OutQuint);

        //myColorSequence.Append(ToRed);
        //myColorSequence.Append(ToWhite);

    }

    private void TweenFontSize() {

    }

    public void TakeDamage(int damage) {
        damagePercentage = Mathf.Clamp(damagePercentage + damage/convertedParts,0,100);
        UpdateUIText();
        if(damagePercentage >= 100) {
            PagesManager.instance.OpenPage(Pages.Lose.ToString());
        }
    }
}

