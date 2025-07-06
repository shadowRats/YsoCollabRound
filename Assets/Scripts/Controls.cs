using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : Overlapper
{
    readonly KeyCode[] movement = { KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.UpArrow, KeyCode.DownArrow };
    readonly Vector3[] directions = { Vector3.left, Vector3.right, Vector3.up, Vector3.down };

    Animator animator;
    SpriteRenderer sr;
    Sprite idle;


    private void Start()
    {
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        idle = sr.sprite;

        Screen.SetResolution(1000, 1000, false);

        if (Screen.height < 1000)
        {
            Screen.SetResolution(Screen.height, Screen.height, false);
        }
    }

    private void OnDisable()
    {
        if (animator.enabled)
        {
            animator.enabled = false;
            sr.sprite = idle;
        }
    }

    protected override void Update()
    {
        base.Update();

        bool none = true;
        for (int i = 0; i < movement.Length; i++)
        {
            if (Input.GetKey(movement[i]))
            {
                if (!animator.enabled)
                {
                    animator.enabled = true;
                }

                transform.position += speed * Time.deltaTime * directions[i];
                none = false;
            }
        }

        if (none)
        {
            if (animator.enabled)
            {
                animator.enabled = false;
                sr.sprite = idle;
            }
        }

        if (on.Count > 0)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Event e = on[^1].transform.GetComponent<Event>();
                if (e != null && !e.enabled)
                {
                    e.enabled = true;
                }
            }

        }

    }

}
