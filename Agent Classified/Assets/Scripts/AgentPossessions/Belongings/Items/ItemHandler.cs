using Newtonsoft.Json;
using System.IO;
using System.Linq;
using UnityEngine;

public class ItemHandler : MonoBehaviour
{
    private string weaponsFilePath;
    int[] weaponsOwnedId;
    private void Start()
    {
        weaponsFilePath = Application.dataPath + "/SaveData/Weapons.json"; 
        LoadFiles();
    }
    public void SaveFiles()
    {
        string json = JsonConvert.SerializeObject(weaponsOwnedId);
        File.WriteAllText(weaponsFilePath, json);
    }

    public void LoadFiles()
    {
        weaponsOwnedId = JsonConvert.DeserializeObject<int[]>(File.ReadAllText(weaponsFilePath));
        GameObject[] temp = new GameObject[weaponsOwnedId.Length];
        for(int i = 0; i < weaponsOwnedId.Length; i++)
            temp[i] = ItemCatalog.Instance.weaponsPrefabs.Where(w => w.name == weaponsOwnedId[i].ToString()).First();

        AgentCommonData.Instance.WeaponHandler.WeaponPrefabs = temp;

    }
}