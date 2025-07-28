using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : Character
{
    readonly KeyCode[] movement = { KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.UpArrow, KeyCode.DownArrow };
    readonly Vector3[] directions = { Vector3.left, Vector3.right, Vector3.up, Vector3.down };

    [SerializeField]
    Camera cam;

    readonly List<Transform> followers = new();

    public static float border;
    public readonly float speed = 2;

    protected override void Awake()
    {
        base.Awake();

        border = cam.orthographicSize - 0.5f;

        Screen.SetResolution(1280, 1280, false);

        if (Screen.height < 1280)
        {
            Screen.SetResolution(Screen.height, Screen.height, false);
        }
    }

    private void OnDisable()
    {
        if (animator.enabled)
        {
            animator.enabled = false;
            sr.sprite = idle;
        }
    }

    protected override void Update()
    {
        base.Update();

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

        if (Mathf.Abs(transform.position.x) > border)
        {
            //transform.position = new Vector3(border * (Mathf.Abs(transform.position.x) / transform.position.x), transform.position.y, 0);
        }
        if (Mathf.Abs(transform.position.y) > border)
        {
            //transform.position = new Vector3(transform.position.x, border * (Mathf.Abs(transform.position.y) / transform.position.y), 0);
        }

        if (none)
        {
            if (animator.enabled)
            {
                animator.enabled = false;
                sr.sprite = idle;
            }
        }

        if (on.Count > 0 && Input.GetKeyDown(KeyCode.Z))
        {
            for (int i = 1; i <= on.Count; i++)
            {
                bool b = true;

                foreach (Transform t in followers)
                {
                    if (t == on[^i].transform)
                    {
                        b = false;
                        break;
                    }
                }

                if (b)
                {
                    Event e = on[^i].transform.GetComponent<Event>();
                    if (e != null)
                    {
                        Transform t = transform;

                        if (followers.Count > 0)
                        {
                            t = followers[^1];
                        }

                        StartCoroutine(e.OnCaught(this, t));
                        followers.Add(on[^i].transform);
                        break;
                    }

                }

            }

        }

    }

}
