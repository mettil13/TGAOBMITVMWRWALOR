using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollDamageCollider : MonoBehaviour
{
    public PlayerDatas playerDatas;
    [SerializeField] List<SkinnedMeshRenderer> meshRenderers = new List<SkinnedMeshRenderer>();
    [SerializeField] float importance = 1;
    [SerializeField] float partPercentage = 0;
    public float Importance => importance;
    public float PartPercentage => partPercentage;
    private bool isImmune = true;

    private void Start() {
        StartCoroutine(SetImmunity(1f));
    }
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.layer == 3) return;
        if (isImmune) return;

        float impact = Mathf.Abs(collision.relativeVelocity.x) + Mathf.Abs(collision.relativeVelocity.y) + Mathf.Abs(collision.relativeVelocity.z)/2;
        if (impact <= 20f) return;

        StartCoroutine(SetImmunity(.3f));

        playerDatas.TakeDamage((int)(impact*importance));

        partPercentage = Mathf.Clamp(partPercentage + impact, 0, 100);


        DOTween.Kill(this.gameObject);
        ChangeColor(partPercentage/100);

    }

    private void ChangeColor(float _damagePercentage) {

        foreach (var renderer in meshRenderers) {
            Sequence mySequence = DOTween.Sequence();

            Tweener ToRed = DOVirtual.Float(renderer.material.GetFloat("_DamageColorPercentage"), 1f, 0.2f,
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
