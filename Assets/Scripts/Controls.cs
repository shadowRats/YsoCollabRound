using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    readonly KeyCode[] movement = { KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.UpArrow, KeyCode.DownArrow };
    readonly Vector3[] directions = { Vector3.left, Vector3.right, Vector3.up, Vector3.down };

    readonly float speed = 3;

    Animator animator;
    SpriteRenderer sr;
    Sprite idle;


    readonly List<Collider2D> on = new();

    private void Start()
    {
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        idle = sr.sprite;

    }

    private void OnDisable()
    {
        if (animator.enabled)
        {
            animator.enabled = false;
            sr.sprite = idle;
        }
    }

    void Update()
    {

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

            if (on[^1].CompareTag("Overlapper"))
            {


                if (transform.position.y - transform.lossyScale.y / 2 < on[^1].transform.position.y - on[^1].transform.lossyScale.y / 2)
                {
                    if (transform.position.z >= on[^1].transform.position.z)
                    {
                        transform.position -= Vector3.forward * 0.1f;
                    }
                }
                else
                {
                    if (transform.position.z <= on[^1].transform.position.z)
                    {
                        transform.position += Vector3.forward * 0.1f;
                    }
                }
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        on.Add(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        on.Remove(collision);
    }
}
