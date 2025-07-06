using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner : Overlapper
{
    Vector3 direction;
    float duration, time;
    readonly float durMin = 1, durMax = 2, dirMin = -5, dirMax = 5, border = 4.5f;

    protected override void Update()
    {
        base.Update();

        if (Mathf.Abs(transform.position.x) > border || Mathf.Abs(transform.position.y) > border)
        {
            direction = -direction;
        }

        if (time > duration)
        {
            direction = new Vector3(Random.Range(dirMin, dirMax), Random.Range(dirMin, dirMax), 0);
            time = 0;
            duration = Random.Range(durMin, durMax);
        }

        transform.position += speed * Time.deltaTime * direction.normalized;
        time += Time.deltaTime;
    }
}
