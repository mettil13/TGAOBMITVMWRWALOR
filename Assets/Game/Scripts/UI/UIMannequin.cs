using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMannequin : MonoBehaviour
{
    public Color damageColor;
    public Color normalColor;
    public List<Image> bodyParts;
    private Tweener[] bodyPartsTween;
    public float fadeDuration = 2;


    void Start()
    {
        proceduralGeneration.RiverGeneration.instance.playerDatas.damageTaken.AddListener(RegisterDamage);
        bodyPartsTween = new Tweener[bodyParts.Count];
        
    }

    public void RegisterDamage(string name, float amount) { // max amount is 5
        for(int i = 0; i < bodyParts.Count; i++) {
            if(bodyParts[i].name.ToLower() == name.ToLower()) {
                if(bodyPartsTween[i] != null) {
                    bodyPartsTween[i].Kill();
                }
                //Color newColor = normalColor * (1 - amount / 5) + damageColor * (amount / 5);
                Color newColor = damageColor;
                bodyParts[i].color = newColor;
                bodyPartsTween[i] = bodyParts[i].DOColor(normalColor, fadeDuration);
                
            }
        }

    }

}
