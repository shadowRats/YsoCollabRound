using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Event : MonoBehaviour
{
    readonly string line = "Kozupii wants to kill";

    [SerializeField]
    GameObject diaBox;
    Text text;

    Controls controls;

    bool spoken = false;

    void OnEnable()
    {
        if (controls == null)
        {
            controls = FindObjectOfType<Controls>();
            text = diaBox.GetComponentInChildren<Text>();
        }


        controls.enabled = false;
        diaBox.SetActive(true);
        StartCoroutine(Speak());
    }

    void Update()
    {
        if (spoken && Input.GetKeyDown(KeyCode.Z))
        {
            spoken = false;
            text.text = "";
            diaBox.SetActive(false);
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
