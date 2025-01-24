using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RagdollDamageCollider : MonoBehaviour
{
    public PlayerDatas playerDatas;
    [SerializeField] List<SkinnedMeshRenderer> meshRenderers = new List<SkinnedMeshRenderer>();
    [SerializeField] float damagePercentage = 0;
    public float importance = 1;


    public float DamagePercentage => damagePercentage;
    public float Importance => importance;

    [SerializeField] float impulseProva;



    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision) {
        float impulse = Mathf.Abs(collision.impulse.x) + Mathf.Abs(collision.impulse.y) + Mathf.Abs(collision.impulse.z);
        impulseProva = impulse;
        if (impulse < 0) return; //Da decidere la soglia via testing

        damagePercentage = Mathf.Clamp(damagePercentage + impulse, 0, 100);
        playerDatas.PercentageIsRight = false;

        StopAllCoroutines();
        StartCoroutine(TurnRed(damagePercentage));

    }

    private IEnumerator TurnRed(float _damagePercentage) {

        //foreach (SkinnedMeshRenderer renderer in meshRenderers) {
        //    Tweener ToRed = DOVirtual.Float(0f, 1f, 2f, SetValue).SetEase(Ease.InOutQuad);
        //    renderer.material.GetFloat()
        //}

        yield return null;
    }
}
