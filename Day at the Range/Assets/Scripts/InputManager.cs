using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    List<InputEntry> entries = new List<InputEntry>();

    public void AddUserToList(string name, int score)
    {
        entries.Add(new InputEntry(name, score));
    }
}
