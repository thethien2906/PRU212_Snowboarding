using System.Collections.Generic;
using UnityEngine;

public class MapSpawnerController : MonoBehaviour
{
    public GameObject[] mapPrefabs;  // Danh sách các prefab map có thể spawn
    public Transform firstMap;       // Map đầu tiên có sẵn trong Scene
    public int maxMaps = 3;          // Số lượng map tối thiểu duy trì

    private List<GameObject> spawnedMaps = new List<GameObject>();

    void Start()
    {
        if (firstMap == null)
        {
            Debug.LogError("⚠️ Bạn chưa gán Map đầu tiên vào firstMap!");
            return;
        }

        spawnedMaps.Add(firstMap.gameObject); // Thêm map đầu tiên vào danh sách

        // Spawn đủ 3 map ban đầu
        for (int i = 1; i < maxMaps; i++)
        {
            SpawnMap(spawnedMaps[spawnedMaps.Count - 1]);
        }
    }

    void SpawnMap(GameObject previousMap)
    {
        GameObject newMap = Instantiate(mapPrefabs[Random.Range(0, mapPrefabs.Length)]);

        Transform previousEndPos = previousMap.transform.Find("EndPos");
        Transform newStartPos = newMap.transform.Find("StartPos");

        if (previousEndPos == null || newStartPos == null)
        {
            Debug.LogError("⚠️ StartPos hoặc EndPos bị thiếu trong prefab!");
            return;
        }

        // Điều chỉnh vị trí để map mới nối vào map trước
        Vector3 offset = newStartPos.position - newMap.transform.position;
        newMap.transform.position = previousEndPos.position - offset;

        spawnedMaps.Add(newMap);

        // Nếu số map vượt quá maxMaps, xóa map cũ nhất
        if (spawnedMaps.Count > maxMaps)
        {
            Destroy(spawnedMaps[0]);
            spawnedMaps.RemoveAt(0);
        }

        Debug.Log($"✅ Spawn map {newMap.name} tại {newMap.transform.position}");
    }

    public void CheckAndSpawnNextMap(Transform player)
    {
        if (spawnedMaps.Count >= 2)
        {
            GameObject thirdMap = spawnedMaps[2];
            if (player.position.x > thirdMap.transform.Find("StartPos").position.x)
            {
                SpawnMap(spawnedMaps[spawnedMaps.Count - 2]);
            }
        }
    }
}
