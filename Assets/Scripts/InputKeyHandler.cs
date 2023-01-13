using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class InputKeyHandler : MonoBehaviour
{
    void Update()
    {
        RespondToKeys();
    }

    private void RespondToKeys()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) { Application.Quit(); }
    }
}
