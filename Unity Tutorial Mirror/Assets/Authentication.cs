﻿using System;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

public class Authentication : MonoBehaviour
{
    async void Start()
    {
        try
        {
            await UnityServices.InitializeAsync();
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            Debug.Log("Logged into Unity, player ID: " + AuthenticationService.Instance.PlayerId);
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }
}
