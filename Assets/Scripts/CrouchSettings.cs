using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchSettings : MonoBehaviour
{
    public static bool isToggleCrouchEnabled = false;

    public void SetToggleCrouch(bool value)
    {
        isToggleCrouchEnabled = value;
    }
}