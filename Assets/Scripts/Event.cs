using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Event : Character
{
    [SerializeField]
    string line;

    [SerializeField]
    Transform portrait;
    
    static Text text;

    Transform leader;

    Controls controls;

    protected override void Update()
    {
        base.Update();

        Vector2 distance = leader.position - transform.position;

        if (distance.magnitude > 0.5)
        {
            if (animator != null && !animator.enabled)
            {
                animator.enabled = true;
            }

            transform.position += controls.speed * Time.deltaTime * new Vector3(distance.x, distance.y, 0);
        }
        else if (animator != null && animator.enabled)
        {
            animator.enabled = false;
            sr.sprite = idle;
        }
    }

    public IEnumerator OnCaught(Controls co, Transform t)
    {
        leader = t;

        controls = co;

        if (line != "")
        {

            if (text == null)
            {
                text = portrait.parent.GetComponentInChildren<Text>();
            }

            controls.enabled = false;
            portrait.gameObject.SetActive(true);
            portrait.parent.gameObject.SetActive(true);

            foreach (char c in line)
            {
                text.text += c;
                yield return new WaitForSeconds(0.05f);
            }

            yield return new WaitUntil(Confirmed);

            text.text = "";
            portrait.gameObject.SetActive(false);
            portrait.parent.gameObject.SetActive(false);
            controls.enabled = true;
        }

        Destroy(GetComponent<Beforer>());
        enabled = true;
    }

    bool Confirmed()
    {
        return Input.GetKeyDown(KeyCode.Z);
    }
}
