using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Event : MonoBehaviour
{
    [SerializeField]
    string line;

    [SerializeField]
    Transform sprite;
    Text text;

    Controls controls;

    bool spoken = false;

    void OnEnable()
    {
        if (controls == null)
        {
            controls = FindObjectOfType<Controls>();
            text = sprite.parent.GetComponentInChildren<Text>();
        }


        controls.enabled = false;
        sprite.gameObject.SetActive(true);
        sprite.parent.gameObject.SetActive(true);
        StartCoroutine(Speak());
    }

    void Update()
    {
        if (spoken && Input.GetKeyDown(KeyCode.Z))
        {
            spoken = false;
            text.text = "";
            sprite.gameObject.SetActive(false);
            sprite.parent.gameObject.SetActive(false);
            controls.enabled = true;
            enabled = false;
        }
    }

    IEnumerator Speak()
    {
        foreach (char c in line)
        {
            text.text += c;
            yield return new WaitForSeconds(0.05f);
        }

        spoken = true;
    }
}
