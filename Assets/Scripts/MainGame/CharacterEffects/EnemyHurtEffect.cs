using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurtEffect : MonoBehaviour
{
    [SerializeField] private float fadeTime = 0.25f;
    [SerializeField] private Color fadeColor;
    private SpriteRenderer sr;
    private Color initialColor;
    private int tweenId = -1;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        initialColor = sr.color;
    }

    public void PlayEffect()
    {
        LeanTween.cancel(gameObject,tweenId);
        sr.color = initialColor;
        tweenId = LeanTween.color(gameObject, fadeColor, fadeTime).setLoopPingPong(1).id;
    }
}
