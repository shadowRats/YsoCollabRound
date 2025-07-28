using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    protected Animator animator;
    protected SpriteRenderer sr;
    protected Sprite idle;

    protected readonly List<Collider2D> on = new();

    protected virtual void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        idle = sr.sprite;

        animator = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        if (on.Count > 0)
        {
            Vector3 pos = transform.position;
            Vector3 front = Vector3.zero, back = Vector3.zero;


            for (int i = 0; i < on.Count; i++)
            {
                if (on[i].CompareTag("Overlapper"))
                {
                    if (on[i].transform.position.y > pos.y)
                    {
                        if (back == Vector3.zero || on[i].transform.position.y < back.y)
                        {
                            back = on[i].transform.position;
                        }
                    }
                    else if (on[i].transform.position.y < pos.y)
                    {
                        if (front == Vector3.zero || on[i].transform.position.y > front.y)
                        {
                            front = on[i].transform.position;
                        }
                    }

                }

            }

            Vector3 newPos = pos;

            if (back == Vector3.zero)
            {
                if (pos.z <= front.z)
                {
                    newPos = new Vector3(pos.x, pos.y, front.z + 0.1f);
                }
            }
            else if (front == Vector3.zero)
            {
                if (pos.z >= back.z)
                {
                    newPos = new Vector3(pos.x, pos.y, back.z - 0.1f);
                }
            }
            else
            {
                newPos = new Vector3(pos.x, pos.y, front.z + (back.z - front.z) / 2);
            }

            if (pos != newPos)
            {
                transform.position = newPos;
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
