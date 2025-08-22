using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreManager : NetworkBehaviour
{
    public static ScoreManager instance;

    [SerializeField] private TextMeshProUGUI scoreTextUI;
    private NetworkVariable<int> globalScore = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    private void Awake()
    {
        instance = this;
    }
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        globalScore.OnValueChanged += OnScoreChanged;
    }

    void OnScoreChanged(int prevValue, int newValue)
    {
        scoreTextUI.text = newValue.ToString();
    }

    public void AddScore()
    {
        if(!IsServer)
            return;

        globalScore.Value++;
    }

}
