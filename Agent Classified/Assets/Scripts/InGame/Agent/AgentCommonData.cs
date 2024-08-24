using UnityEngine;

public class AgentCommonData
{
    public Transform Agent { get; set; }
    public Crosshair Crosshair { get; set; }
    public WeaponHandler WeaponHandler { get; set; }
    private static readonly AgentCommonData instance = new AgentCommonData();

    static AgentCommonData()
    {
    }

    private AgentCommonData()
    {
    }

    public static AgentCommonData Instance => instance;
}