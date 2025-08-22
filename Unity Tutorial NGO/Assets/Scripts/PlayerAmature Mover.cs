using StarterAssets;
using Unity.Cinemachine;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAmatureMover : NetworkBehaviour
{
    [SerializeField] private CharacterController cc;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private StarterAssetsInputs starterAsset;
    [SerializeField] private ThirdPersonController controller;
    [SerializeField] private Transform playerRoot;

    [SerializeField] private GameObject bombPrefab;

    private void Awake()
    {
        cc.enabled = false;
        playerInput.enabled = false;
        starterAsset.enabled = false;
        controller.enabled = false;

    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();


        if(IsOwner)
        {
            cc.enabled = true;
            playerInput.enabled = true;
            starterAsset.enabled = true;
            controller.enabled = true;

            var cinemachine = GameObject.Find("PlayerFollowCamera").GetComponent<CinemachineCamera>();
            cinemachine.Target.TrackingTarget = playerRoot;
        }
    }
    private void Update()
    {
        if (!IsOwner)
            return;

        if(Input.GetKeyDown(KeyCode.Return))//return : enter
        {
            AddScoreServerRpc();
        }
        else
        {
            ThrowBombServerRpc();
        }
    }

    [ServerRpc]
    void ThrowBombServerRpc()
    {
        Instantiate(bombPrefab, transform.position,Quaternion.identity);
    }

    [ServerRpc]
    void AddScoreServerRpc()
    {
        ScoreManager.instance.AddScore();
    }

    //[ClientRpc]
    //void AddScoreLogClientRpc(int newValue)
    //{
    //    Debug.Log($"누군가 점수를 획득했습니다. 현재 점수: {newValue}");
    //}
}
