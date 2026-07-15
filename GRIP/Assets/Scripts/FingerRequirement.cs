using System;
using UnityEngine;

[Serializable]
public class FingerRequirement
{
    public FingerType finger;

    [Range(0, 100)]
    public int contribution;
}