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
    public void AddItem(Item �tem,Transform tr)
    {
        // itemi listeye ekledim type �na g�re
        switch (�tem.Type)
        {
            case ItemType.Pistol:
                if (!isPistolSlot)
                {
                    Instance.InventoryItems.Add(�tem);
                    Instance.isPistolSlot = true;
                    Instantiate(�tem.HandItemPrefab,WeaponTransform);
                    WeaponHolder.Instance.SelectWeapon();
                    Destroy(tr.parent.gameObject);
                }
                break;
            case ItemType.Rifle:
                if (!isRifleSlot)
                {
                    Instance.InventoryItems.Add(�tem); // itemi ekle listeye
                    Instance.isRifleSlot = true; // t�fek slotunu dolu goster
                    Instantiate(�tem.HandItemPrefab,WeaponTransform); // eldeki silah� olustur
                    WeaponHolder.Instance.SelectWeapon(); // secili silah� ayarla
                    Destroy(tr.parent.gameObject);                     // itemin yerdeki objes�ini sildim
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
            int currentWeapon = WeaponHolder.Instance.Weapon�ndex; // �uanki silah�n indexsini ald�k
            ItemDrop(currentWeapon);
            WeaponHolder.Instance.animator.CrossFade("Default", .1f);//animatoru defaulta gonderdim
            InventoryItems.Remove(WeaponTransform.GetChild(currentWeapon).GetComponent<ItemHolder>().Item);// listeden itemi sildim
            Destroy(WeaponTransform.GetChild(currentWeapon).gameObject);// eldeki silah� sildim silah�n indexi ayn� zamanda weapontransfrom objesinin alt objesi
            if (WeaponHolder.Instance.Weapon�ndex >= 1) WeaponHolder.Instance.Weapon�ndex--; // silah� yere at�nca �uanki silah indexsini bir azaltt�m
            print("Silah at�ld�");                                   // e�er azaltmazsam olmayan bi silah indexsinde kal�rd�k

        }

    }
    public void ItemDrop(int CurrentWeapon)
    {
        switch (WeaponTransform.GetChild(CurrentWeapon).GetComponent<ItemHolder>().Item.name)// icindeki �tem datas�n�n isimlerine bakt�k
        {
            case "Pistol": // �temi atarken objeyi olusturdum rigidbody adfforce ile ileri ittim 
                GameObject pistol= Instantiate(PrefabManager.Instance.Pistol, transform.position, Quaternion.identity);
                ItemDropAction(pistol);
                isPistolSlot = false;//  pistol slotunu false yapt�mki tekrardan ba�ka tabanca alabilek
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
        obj.transform.Rotate(Random.Range(-30, 30), 0, 0);// dimdik yere d��mesin diye rotasyonuyla oynad�m
    }

}
