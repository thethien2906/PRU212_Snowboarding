using System.Collections.Generic;
using UnityEngine;

public class MapSpawnerController : MonoBehaviour
{
    public GameObject[] mapPrefabs;  // Danh sách các prefab map có thể spawn
    public Transform firstMap;       // Map đầu tiên có sẵn trong Scene
    public int maxMaps = 3;          // Số lượng map tối đa tồn tại

    [Header("Surface Speed Settings")]
    public float minSpeed = 5f;
    public float maxSpeed = 20f;
    public float speedChangeRate = 0.5f; // Tốc độ thay đổi mỗi giây
    public bool speedIncreaseOverTime = true;

    private List<GameObject> spawnedMaps = new List<GameObject>();
    private float currentSpeed;

    void Start()
    {
        if (firstMap == null)
        {
            Debug.LogError("⚠️ Bạn chưa gán Map đầu tiên vào firstMap!");
            return;
        }

        spawnedMaps.Add(firstMap.gameObject);
        currentSpeed = minSpeed;

        // Áp dụng tốc độ ban đầu cho map đầu tiên
        ApplySurfaceSpeed(firstMap.gameObject, currentSpeed);

        // Tạo đủ số lượng map ban đầu
        for (int i = 1; i < maxMaps; i++)
        {
            SpawnMap(spawnedMaps[spawnedMaps.Count - 1]);
        }
    }

    void Update()
    {
        AdjustSurfaceSpeed();
    }

    void SpawnMap(GameObject previousMap)
    {
        if (mapPrefabs.Length == 0)
        {
            Debug.LogError("⚠️ Không có prefab nào để spawn!");
            return;
        }

        GameObject newMap = Instantiate(mapPrefabs[Random.Range(0, mapPrefabs.Length)]);
        Transform previousEndPos = previousMap.transform.Find("EndPos");
        Transform newStartPos = newMap.transform.Find("StartPos");

        if (previousEndPos == null || newStartPos == null)
        {
            Debug.LogError($"⚠️ Thiếu StartPos hoặc EndPos trong prefab {newMap.name}!");
            Destroy(newMap);
            return;
        }

        Vector3 offset = newStartPos.position - newMap.transform.position;
        newMap.transform.position = previousEndPos.position - offset;

        spawnedMaps.Add(newMap);

        ApplySurfaceSpeed(newMap, currentSpeed);

        if (spawnedMaps.Count > maxMaps)
        {
            Destroy(spawnedMaps[0]);
            spawnedMaps.RemoveAt(0);
        }
    }

    public void CheckAndSpawnNextMap(Transform player)
    {
        if (spawnedMaps.Count < 2) return;

        GameObject lastMap = spawnedMaps[spawnedMaps.Count - 1];

        Transform lastStartPos = lastMap.transform.Find("StartPos");
        if (lastStartPos == null)
        {
            Debug.LogError($"⚠️ {lastMap.name} thiếu StartPos!");
            return;
        }

        if (player.position.x > lastStartPos.position.x)
        {
            SpawnMap(spawnedMaps[spawnedMaps.Count - 1]);
        }
    }

    void ApplySurfaceSpeed(GameObject map, float speed)
    {
        SurfaceEffector2D surfaceEffector = map.GetComponentInChildren<SurfaceEffector2D>();
        if (surfaceEffector != null)
        {
            surfaceEffector.speed = speed;
        }
    }

    void AdjustSurfaceSpeed()
    {
        if (speedIncreaseOverTime)
        {
            currentSpeed += speedChangeRate * Time.deltaTime;
            if (currentSpeed > maxSpeed)
                currentSpeed = maxSpeed;
        }
        else
        {
            currentSpeed -= speedChangeRate * Time.deltaTime;
            if (currentSpeed < minSpeed)
                currentSpeed = minSpeed;
        }

        // Cập nhật tốc độ cho tất cả các map hiện tại
        foreach (GameObject map in spawnedMaps)
        {
            ApplySurfaceSpeed(map, currentSpeed);
        }
    }
}
