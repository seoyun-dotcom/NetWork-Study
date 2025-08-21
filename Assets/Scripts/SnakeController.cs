using Mirror;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : NetworkBehaviour
{
    [SerializeField] private GameObject tailPrefab;

    //SyncVar : 대상이 변경되면 동기화해주는 기능
    [SyncVar]
    private Transform coin;

    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float turnSpeed = 120f;
    [SerializeField] private float lerpSpeed = 7f;

    //List : 추가 / 삽입/ 삭제 할 수 있는 자료구조
    //SyncList : 추가 / 삽입/ 삭제 할 때 동기화해주는 기능
    private SyncList<Transform> tails = new SyncList<Transform>();

    [Server]
    public override void OnStartServer()
    {
        coin = GameObject.FindGameObjectWithTag("Coin").transform;
    }
    
    void Update()
    {
        if(isLocalPlayer)
            MoveHead();
    }

    void LateUpdate()
    {
        MoveTail();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            AddTail();
            MoveCoin();
        }
    }

    private void MoveHead()
    {
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);

        float h = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.forward * h * -turnSpeed * Time.deltaTime);
    }

    [Server]//서버에서만 호출되는 함수
    private void MoveCoin()
    {
        if (coin == null)
            return;

        float ranX = Random.Range(-20f, 20f);
        float ranY = Random.Range(-10f, 10f);

        coin.position = new Vector3(ranX, ranY, 0);
    }

    [Server]
    private void AddTail()
    {
        GameObject newTail = Instantiate(tailPrefab);
        newTail.transform.position = transform.position;

        NetworkServer.Spawn(newTail, connectionToClient);

        tails.Add(newTail.transform);
    }

    private void MoveTail()
    {
        Transform target = transform;

        foreach (var tail in tails)
        {
            if (tail == null)
                continue;

            tail.position = Vector3.Lerp(tail.position, target.position, lerpSpeed * Time.deltaTime);
            tail.rotation = Quaternion.Lerp(tail.rotation, target.rotation, lerpSpeed * Time.deltaTime);

            target = tail;
        }
    }
}