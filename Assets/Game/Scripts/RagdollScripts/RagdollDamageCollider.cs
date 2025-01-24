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

    [SerializeField] float impulseProva;



    private void Start() {
        StartCoroutine(SetImmunity(2f));
    }
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision) {
        if (isImmune) return;
        StartCoroutine(SetImmunity(.3f));

        float impulse = Mathf.Abs(collision.impulse.x) + Mathf.Abs(collision.impulse.y) + Mathf.Abs(collision.impulse.z);
        if (impulse <= 0f) return; //Da decidere la soglia via testing
        impulseProva = impulse; //Da togliere dopo testing

        damagePercentage = Mathf.Clamp(damagePercentage + impulse, 0, 100);
        playerDatas.PercentageIsRight = false;

        DOTween.Kill(this);
        ChangeColor(damagePercentage/100);

    }

    private void ChangeColor(float _damagePercentage) {

        foreach (var renderer in meshRenderers) {
            Sequence mySequence = DOTween.Sequence();

            Tweener ToRed = DOVirtual.Float(renderer.material.GetFloat("_DamageColorPercentage"), 1f, 0.3f,
                (f) => renderer.material.SetFloat("_DamageColorPercentage", f)
                ).SetEase(Ease.InOutQuad);

            Tweener ToColor = DOVirtual.Float(1f, _damagePercentage, 1f,
                (f) => renderer.material.SetFloat("_DamageColorPercentage", f)
                ).SetEase(Ease.InOutQuad);

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
