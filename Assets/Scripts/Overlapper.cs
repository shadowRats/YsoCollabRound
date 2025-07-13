using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overlapper : Character
{
    protected readonly List<Collider2D> on = new();

    protected readonly float speed = 2;

    protected virtual void Update()
    {
        if (on.Count > 0)
        {
            foreach (Collider2D c in on)
            {
                if (c.CompareTag("Overlapper"))
                {
                    if (transform.position.y < c.transform.position.y)
                    {
                        if (transform.position.z >= c.transform.position.z)
                        {
                            c.transform.position += Vector3.forward * 0.1f;

                            print(gameObject.name + " put " + c.gameObject.name + " behind");
                        }
                    }
                    else
                    {
                        if (transform.position.z <= c.transform.position.z)
                        {
                            c.transform.position -= Vector3.forward * 0.1f;
                            print(gameObject.name + " put " + c.gameObject.name + " forward");
                        }
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
