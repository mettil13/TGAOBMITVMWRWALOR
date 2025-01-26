using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDatas : MonoBehaviour
{
    List<RagdollDamageCollider> bodyParts = new List<RagdollDamageCollider>();

    [SerializeField] float damagePercentage = 0;
    private float convertedParts = 0;

    public TextMeshProUGUI uiPercentage;
    public TMP_Text uiPercentagetmptext;
    [SerializeField] int fontSmall;
    [SerializeField] int fontBig;

    [SerializeField] AnimationCurve curveFont;

    public UnityEvent<string, float> damageTaken = new UnityEvent<string, float>();

    void Start()
    {
        DOTween.SetTweensCapacity(200,100);
        bodyParts = GetComponentsInChildren<RagdollDamageCollider>().ToList<RagdollDamageCollider>();
        foreach(var part in bodyParts) {
            convertedParts += part.Importance;
            part.playerDatas = this;
        }
        proceduralGeneration.RiverGeneration.instance.playerInput.applyPressed.AddListener(DeactivateInvincibility);
    }

    public void DeactivateInvincibility() {
        foreach(var part in bodyParts) {
            part.StartCoroutine(part.SetImmunity(.3f));
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
        if (uiPercentage == null) return;
        int showedPercentage = (int)damagePercentage;
        uiPercentage.text = showedPercentage.ToString() + "%";

        DOTween.Kill(this.gameObject);
        TweenColor();
        TweenFontSize();
    }

    private void TweenColor()
    {
        Sequence myColorSequence = DOTween.Sequence();
        Tweener ToRed = uiPercentagetmptext.DOColor(Color.red, 0.2f).SetEase(Ease.OutQuint);
        Tweener ToWhite = uiPercentagetmptext.DOColor(Color.white, 0.4f).SetEase(Ease.OutQuint);


        myColorSequence.Append(ToRed);
        myColorSequence.Append(ToWhite);

    }

    private void TweenFontSize() {
        Sequence mySizeSequence = DOTween.Sequence();

        Tweener ToBig = DOVirtual.Int(fontSmall, fontBig, 0.2f,
            (f) => uiPercentage.fontSize = f
            ).SetEase(Ease.OutQuint);

        Tweener ToSmall = DOVirtual.Int(fontBig, fontSmall, 0.2f,
            (f) => uiPercentage.fontSize = f
            ).SetEase(Ease.OutQuint);

        mySizeSequence.Append(ToBig);
        mySizeSequence.Append(ToSmall);
    }

    public void TakeDamage(int damage, RagdollDamageCollider bodyPart) {
        float convertedDamage = damage / convertedParts;
        //if (convertedDamage < 1) return;
        convertedDamage = Mathf.Clamp(convertedDamage, 1, 5);
        damagePercentage = Mathf.Clamp(damagePercentage + convertedDamage, 0,100);
        UpdateUIText();
        damageTaken.Invoke(bodyPart.gameObject.name, convertedDamage);
        if(damagePercentage >= 100) {
            uiPercentagetmptext.color = Color.red;
            foreach (var part in bodyParts) {
                part.SetPartDead();

            }
            PagesManager.instance.OpenPage(Pages.Lose.ToString());
        }
    }
}

