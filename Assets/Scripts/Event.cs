using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Event : Overlapper
{
    [SerializeField]
    string line;

    [SerializeField]
    Transform sprite;
    static Text text;

    static Controls controls;
    Transform leader;

    protected override void Update()
    {
        base.Update();

        Vector3 distance = leader.position - transform.position;

        if (distance.magnitude > 0.5)
        {
            transform.position += speed * Time.deltaTime * distance;
        }
    }

    public IEnumerator OnCaught(Controls co, Transform t)
    {
        if (controls == null)
        {
            controls = co;
            text = sprite.parent.GetComponentInChildren<Text>();
        }

        leader = t;

        controls.enabled = false;
        sprite.gameObject.SetActive(true);
        sprite.parent.gameObject.SetActive(true);

        foreach (char c in line)
        {
            text.text += c;
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitUntil(Confirmed);

        text.text = "";
        sprite.gameObject.SetActive(false);
        sprite.parent.gameObject.SetActive(false);
        controls.enabled = true;
        enabled = true;
    }

    bool Confirmed()
    {
        return Input.GetKeyDown(KeyCode.Z);
    }
}
