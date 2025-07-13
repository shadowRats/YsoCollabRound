using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    protected Animator animator;
    protected SpriteRenderer sr;
    protected Sprite idle;


    protected virtual void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        idle = sr.sprite;

        animator = GetComponent<Animator>();
    }
}
