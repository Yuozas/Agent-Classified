using UnityEngine;

[CreateAssetMenu(fileName = "SpawnableObject", menuName = "ScriptableObjects/SpawnableObject", order = 1)]
public class SpawnableObjectScriptable : ScriptableObject
{
    [Header("Components")]
    public GameObject prefab;

    [Header("Prefab description")]
    public string prefabName;

    [Header("Mortality")]
    public bool mortal;// mortal - true, immortal - false
    public bool despawnable;
    public float despawnTime;//not related if not despawnable
    public int health;//not related if immortal

    [Header("Spawn")]
    public float spawnCooldown;
    public int spawnChance;
    public int currentlySpawned;
    public GameObject[] spawnableSlots;//Array size == max spawnable at same time;
    public bool onCooldown;
    public Addon.RangeInt spawnRange;

    [Header("Drop")]
    public int coin;
}