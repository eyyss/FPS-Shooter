using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    void Awake()
    {
        Instance = this;
    }
    [Header("Inventory")]
    public List<Item> InventoryItems;

    public Transform WeaponTransform;
    [Header("Pistol")]
    public bool isPistolSlot;
    [Header("Rifle")]
    public bool isRifleSlot;

    // item eklemek
    public void AddItem(Item ýtem,Transform tr)
    {
        // itemi listeye ekledim type ýna göre
        switch (ýtem.Type)
        {
            case ItemType.Pistol:
                if (!isPistolSlot)
                {
                    Instance.InventoryItems.Add(ýtem);
                    Instance.isPistolSlot = true;
                    Instantiate(ýtem.HandItemPrefab,WeaponTransform);
                    WeaponHolder.Instance.SelectWeapon();
                    Destroy(tr.parent.gameObject);
                }
                break;
            case ItemType.Rifle:
                if (!isRifleSlot)
                {
                    Instance.InventoryItems.Add(ýtem); // itemi ekle listeye
                    Instance.isRifleSlot = true; // tüfek slotunu dolu goster
                    Instantiate(ýtem.HandItemPrefab,WeaponTransform); // eldeki silahý olustur
                    WeaponHolder.Instance.SelectWeapon(); // secili silahý ayarla
                    Destroy(tr.parent.gameObject);                     // itemin yerdeki objesþini sildim
                }
                break;
            default:
                break;
        }


    }
    public void RemoveItem()
    {
        if (InventoryItems.Count > 0)
        {
            int currentWeapon = WeaponHolder.Instance.WeaponÝndex; // þuanki silahýn indexsini aldýk
            ItemDrop(currentWeapon);
            WeaponHolder.Instance.animator.CrossFade("Default", .1f);//animatoru defaulta gonderdim
            InventoryItems.Remove(WeaponTransform.GetChild(currentWeapon).GetComponent<ItemHolder>().Item);// listeden itemi sildim
            Destroy(WeaponTransform.GetChild(currentWeapon).gameObject);// eldeki silahý sildim silahýn indexi ayný zamanda weapontransfrom objesinin alt objesi
            if (WeaponHolder.Instance.WeaponÝndex >= 1) WeaponHolder.Instance.WeaponÝndex--; // silahý yere atýnca þuanki silah indexsini bir azalttým
            print("Silah atýldý");                                   // eðer azaltmazsam olmayan bi silah indexsinde kalýrdýk

        }

    }
    public void ItemDrop(int CurrentWeapon)
    {
        switch (WeaponTransform.GetChild(CurrentWeapon).GetComponent<ItemHolder>().Item.name)// icindeki ýtem datasýnýn isimlerine baktýk
        {
            case "Pistol": // ýtemi atarken objeyi olusturdum rigidbody adfforce ile ileri ittim 
                GameObject pistol= Instantiate(PrefabManager.Instance.Pistol, transform.position, Quaternion.identity);
                ItemDropAction(pistol);
                isPistolSlot = false;//  pistol slotunu false yaptýmki tekrardan baþka tabanca alabilek
                break;
            case "Rifle":
                GameObject assault = Instantiate(PrefabManager.Instance.Assault, transform.position, Quaternion.identity);
                ItemDropAction(assault);
                isRifleSlot = false;
                break;
            case "Pistol1":
                GameObject pistol1 = Instantiate(PrefabManager.Instance.Pistol1, transform.position, Quaternion.identity);
                ItemDropAction(pistol1);
                isPistolSlot = false;
                break;
            default:
                break;
        }
        WeaponHolder.Instance.SelectWeapon();// secili weaponu guncelledim

    }
    public void ItemDropAction(GameObject obj)
    {
        obj.GetComponent<Rigidbody>().AddForce(transform.forward * 3, ForceMode.Impulse);
        obj.transform.Rotate(Random.Range(-30, 30), 0, 0);// dimdik yere düþmesin diye rotasyonuyla oynadým
    }

}
