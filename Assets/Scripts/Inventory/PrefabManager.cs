using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    public static PrefabManager Instance; // prefablar� tek yerde tuttum ilerde fazla silah eklersek �nventor� kar��t�rmas�n diye
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
