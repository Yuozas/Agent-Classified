using UnityEngine;

public sealed class ItemCatalog
{
    public BuyableScriptable[] buyables = Resources.LoadAll<BuyableScriptable>("Buyables");
    public WeaponScriptable[] weapons = Resources.LoadAll<WeaponScriptable>("Weapons");
    public GameObject[] weaponsPrefabs = Resources.LoadAll<GameObject>("Prefabs/Weapons");
    private static readonly ItemCatalog instance = new ItemCatalog();

    static ItemCatalog()
    {
    }

    private ItemCatalog()
    {
    }

    public static ItemCatalog Instance => instance;
}