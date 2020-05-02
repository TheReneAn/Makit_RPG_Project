using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    [TextArea(1, 2)] // lines amount
    public string[] sentences; // context
    public string[] names;     // dialogue name
    public Sprite[] sprites;   // portrait
}
