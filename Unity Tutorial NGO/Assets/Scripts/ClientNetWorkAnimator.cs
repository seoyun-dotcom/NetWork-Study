using Unity.Netcode.Components;
using UnityEngine;

public class ClientNetWorkAnimator : NetworkAnimator
{
    protected override bool OnIsServerAuthoritative()
    {
        return false;
    }
}
