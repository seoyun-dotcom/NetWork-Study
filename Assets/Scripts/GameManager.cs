using Mirror;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    public static GameManager Instance;

    [SerializeField] private Transform coin;

    // SyncVar : 대상이 변경될 때 알려주는 기능
    [SyncVar(hook = nameof(OnCoinPositionChanged))] // 값이 변경될 때 이벤트 실행
    public Vector3 coinPosition;

    void Awake()
    {
        Instance = this;
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
        MoveCoin();
    }

    [Server]
    public void MoveCoin()
    {
        float ranX = Random.Range(-20f, 20f);
        float ranY = Random.Range(-10f, 10f);

        coinPosition = new Vector3(ranX, ranY, 0);
    }

    private void OnCoinPositionChanged(Vector3 prevPos, Vector3 newPos)
    {
        coin.position = newPos;
    }
}