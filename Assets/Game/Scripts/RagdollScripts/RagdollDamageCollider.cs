using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RagdollDamageCollider : MonoBehaviour
{
    [SerializeField] PlayerDatas playerDatas;
    //[SerializeField] Collider colliderBodyPart;
    [SerializeField] List<SkinnedMeshRenderer> meshRenderers = new List<SkinnedMeshRenderer>();
    [SerializeField] float damagePercentage = 0;
    public float importance = 1;

    public float DamagePercentage => damagePercentage;
    public float Importance => importance;

    [SerializeField] float impulseProva;

    void Awake()
    {
        playerDatas = GetComponentInParent<PlayerDatas>();
        playerDatas.bodyParts.Add(this);
        //colliderBodyPart = GetComponent<Collider>();
    }

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

        //Coroutine per cambiare il colore del materiale da damagepercentage a 100, e poi fino a newDamagePercentage
        //foreach (var renderer in meshRenderers) {
        //    renderer.material.color = Color.red; //lo facciamo bene quando è pronto il materiale
        //}
        yield return null;
    }
}
