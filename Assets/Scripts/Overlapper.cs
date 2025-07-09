using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overlapper : MonoBehaviour
{

    protected readonly float speed = 2;
    protected readonly List<Collider2D> on = new();

    protected static float border;

    protected virtual void Update()
    {
        if (on.Count > 0)
        {
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
