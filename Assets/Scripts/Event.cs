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

    Transform leader;

    protected override void Update()
    {
        base.Update();

        Vector3 distance = leader.position - transform.position;

        if (distance.magnitude > 0.5)
        {
            if (animator != null && !animator.enabled)
            {
                animator.enabled = true;
            }

            transform.position += speed * Time.deltaTime * distance;
        }
        else if (animator != null && animator.enabled)
        {
            animator.enabled = false;
            sr.sprite = idle;
        }
    }

    public IEnumerator OnCaught(Controls controls, Transform t)
    {
        leader = t;

        if (line != "")
        {

            if (text == null)
            {
                text = sprite.parent.GetComponentInChildren<Text>();
            }

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
        }

        Beforer b = GetComponent<Beforer>();

        if (b != null)
        {
            Destroy(b);
        }

        enabled = true;
    }

    bool Confirmed()
    {
        return Input.GetKeyDown(KeyCode.Z);
    }
}
