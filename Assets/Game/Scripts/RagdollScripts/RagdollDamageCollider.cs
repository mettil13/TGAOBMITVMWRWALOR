using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollDamageCollider : MonoBehaviour
{
    public PlayerDatas playerDatas;
    [SerializeField] List<SkinnedMeshRenderer> meshRenderers = new List<SkinnedMeshRenderer>();
    [SerializeField] float importance = 1;
    [SerializeField] float damagePercentage = 0;
    public float Importance => importance;
    public float DamagePercentage => damagePercentage;
    private bool isImmune = true;

    [SerializeField] float impactProva;



    private void Start() {
        StartCoroutine(SetImmunity(1.5f));
    }
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.layer == 3) return;
        if (isImmune) return;
        if (damagePercentage == 100) return;
        StartCoroutine(SetImmunity(.3f));

        //provare entrambi i metodi e scegliere il migliore
        float impact = Mathf.Abs(collision.relativeVelocity.x) + Mathf.Abs(collision.relativeVelocity.y) + Mathf.Abs(collision.relativeVelocity.z)/3;
        //float impact = Mathf.Abs(collision.impulse.x) + Mathf.Abs(collision.impulse.y) + Mathf.Abs(collision.impulse.z);
        if (impact <= 15f) return; //Da decidere la soglia via testing
        impactProva = impact; //Da togliere dopo testing

        damagePercentage = Mathf.Clamp(damagePercentage + impact, 0, 100);
        if (damagePercentage >= 95)
            damagePercentage = 100;
        playerDatas.PercentageIsRight = false;

        DOTween.Kill(this.gameObject);
        ChangeColor(damagePercentage/100);

    }

    private void ChangeColor(float _damagePercentage) {

        foreach (var renderer in meshRenderers) {
            Sequence mySequence = DOTween.Sequence();

            Tweener ToRed = DOVirtual.Float(renderer.material.GetFloat("_DamageColorPercentage"), 1f, 0.3f,
                (f) => renderer.material.SetFloat("_DamageColorPercentage", f)
                ).SetEase(Ease.OutQuint);

            Tweener ToColor = DOVirtual.Float(1f, _damagePercentage, 0.5f,
                (f) => renderer.material.SetFloat("_DamageColorPercentage", f)
                ).SetEase(Ease.OutQuint);

            mySequence.Append(ToRed);
            mySequence.Append(ToColor);

        }

    }

    private IEnumerator SetImmunity(float time) {
        isImmune = true;
        yield return new WaitForSeconds(time);
        isImmune = false;
    }
}
