using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public void Bar()
    {
        Renderer bar = gameObject.GetComponent<Renderer>();

        if (bar.enabled)
        {
            bar.enabled = false;
        }
        else
        {
            bar.enabled = true;
        }

    }

    public void Line()
    {
        LineRenderer line = gameObject.GetComponent<LineRenderer>();

        if (line.enabled)
        {
            line.enabled = false;
        }

        else
        {
            line.enabled = true;
        }
    }
}