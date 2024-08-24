using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    #region Components

    [Header("Components")]
    [SerializeField]
    private SpawnableObjectScriptable[] spawnableObjects;

    [SerializeField]
    private BoxCollider2D[] spawners;

    #endregion Components

    private void Start()
    {
        for (int i = 0; i < spawnableObjects.Length; i++)
        {
            spawnableObjects[i].onCooldown.SetFalse();
            spawnableObjects[i].currentlySpawned = 0;
        }
    }

    private void Update() => SpawnControl();

    private void SpawnControl()
    {
        for (int i = 0; i < spawnableObjects.Length; i++)
        {

            if (spawnableObjects[i].spawnRange.InRange(GameData.Instance.ScoreHandler.newRunScore))
            {
                if (!spawnableObjects[i].onCooldown)
                {
                    if (spawnableObjects[i].currentlySpawned < spawnableObjects[i].spawnableSlots.Length)
                    {
                        spawnableObjects[i].onCooldown.SetTrue();
                        if (spawnableObjects[i].spawnChance >= Random.Range(1, 100))
                            Spawn(i);
                        else
                            ChanceUnsuccessful(i);

                    }
                }
            }
        }
    }

    private void ChanceUnsuccessful(int i) => StartCoroutine(CooldownPrefab(i));

    private void Spawn(int i)
    {
        for (int j = 0; j < spawnableObjects[i].spawnableSlots.Length; j++)
        {
            if (spawnableObjects[i].spawnableSlots[j].NotNull())
                continue;
            SetSpawnableData(i, j);
            HandleSpawnConsequences(i, j);
            break;
        }
    }

    private void SetSpawnableData(int i, int j)
    {
        spawnableObjects[i].currentlySpawned++;
        spawnableObjects[i].spawnableSlots[j] = Instantiate(spawnableObjects[i].prefab, SpawnLocation, Quaternion.identity);
        if(spawnableObjects[i].mortal)
            spawnableObjects[i].spawnableSlots[j].GetComponent<DamageEnemyCommunication>().Initialize(this, i, j);
        if (!spawnableObjects[i].prefabName.IsNullOrEmpty())
            spawnableObjects[i].spawnableSlots[j].GetComponent<IMobName>().Name = spawnableObjects[i].prefabName;
    }

    private void HandleSpawnConsequences(int i, int j)
    {
        StartCoroutine(CooldownPrefab(i));
        if (spawnableObjects[i].despawnable)
            StartCoroutine(DespawnPrefab(i, j));
    }

    private IEnumerator DespawnPrefab(int spawnableObjectIndex, int objectIndex)
    {
        float time = Time.time;
        while (Time.time - time < spawnableObjects[spawnableObjectIndex].despawnTime)
        {
            if (spawnableObjects[spawnableObjectIndex].spawnableSlots[objectIndex].IsNull())
                break;
            yield return new WaitForEndOfFrame();
        }
        if (spawnableObjects[spawnableObjectIndex].spawnableSlots[objectIndex].NotNull())
            KillObject(spawnableObjectIndex, objectIndex);
    }
    public GameObject GetSpawnedObject(int objectIndex, int slotIndex) => spawnableObjects[objectIndex].spawnableSlots[slotIndex];
    public void Drop(int spawnableObjectIndex, int objectIndex)
    {
        if (spawnableObjects[spawnableObjectIndex].coin > 0)
            GameData.Instance.MonetaryHandler.DropCoin(spawnableObjects[spawnableObjectIndex].coin, spawnableObjects[spawnableObjectIndex].spawnableSlots[objectIndex].transform.position);
    }

    public void KillObject(int spawnableObjectIndex, int objectIndex)
    {
        if (spawnableObjects[spawnableObjectIndex].spawnableSlots[objectIndex].NotNull())
        {
            spawnableObjects[spawnableObjectIndex].currentlySpawned--;
            Destroy(spawnableObjects[spawnableObjectIndex].spawnableSlots[objectIndex]);
            spawnableObjects[spawnableObjectIndex].spawnableSlots[objectIndex] = null;
        }
    }

    public int GetHealth(int spawnableObjectIndex) => spawnableObjects[spawnableObjectIndex].health;

    private IEnumerator CooldownPrefab(int spawnableObjectIndex)
    {
        yield return new WaitForSeconds(spawnableObjects[spawnableObjectIndex].spawnCooldown);
        spawnableObjects[spawnableObjectIndex].onCooldown.SetFalse();
    }

    private Vector3 SpawnLocation => spawners.RandomElement().bounds.RandomPointInBounds();
}