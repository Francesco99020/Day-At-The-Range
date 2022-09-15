using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    List<InputEntry> entries = new List<InputEntry>();

    public void AddUserToList(string name, int score)
    {
        entries.Add(new InputEntry(name, score));
    }
}
