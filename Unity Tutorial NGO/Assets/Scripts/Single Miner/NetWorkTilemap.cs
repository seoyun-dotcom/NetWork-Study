using UnityEngine;
using UnityEngine.Tilemaps;

public class NetWorkTilemap : MonoBehaviour
{
    [SerializeField] private GameObject[] minerals;

    private Tilemap tilemap;

    private void Awake()
    {
        tilemap = GetComponent<Tilemap>();
    }

    public void RemoveTile(Vector3 hitPos)
    {
        Vector3Int cellPos = tilemap.WorldToCell(hitPos);

        tilemap.SetTile(cellPos, null);

        // Item Drop
        int ranItemDrop = Random.Range(0, 101);
        if (ranItemDrop >= 70)
        {
            int ranIndex = Random.Range(0, minerals.Length);
            Instantiate(minerals[ranIndex], cellPos, Quaternion.identity);
        }
    }
}
