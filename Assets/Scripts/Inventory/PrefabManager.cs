using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    public static PrefabManager Instance; // prefablarý tek yerde tuttum ilerde fazla silah eklersek ýnventorü karýþtýrmasýn diye
    public void Awake()
    {
        if (Instance ==null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public GameObject Assault;
    public GameObject Pistol;
    public GameObject Pistol1;
}
