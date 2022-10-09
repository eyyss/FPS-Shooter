using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="GunData")]
[System.Serializable]
public class Item :ScriptableObject
{
    public string ItemName;
    public ItemType Type;
    public float damage;

    [Header("Magazine")]
    public int ammoInGun;
    public int ammoInPocket;
    public float addableAmmo;
    [Header("Prefab")]
    public GameObject HandItemPrefab;
    public GameObject PlaneItemPrefab;
}
public enum ItemType
{
    Pistol,
    Rifle
}
