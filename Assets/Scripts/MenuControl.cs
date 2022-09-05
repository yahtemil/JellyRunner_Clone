#if UNITY_EDITOR
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuControl : MonoBehaviour
{
    [MenuItem("GameObject/Obstacles/DikenliMazgal")]
    static void AddDikenliMazgal()
    {
        Instantiate(Resources.Load<GameObject>("Obstacles/DikenliMazgal Variant"));
    }

    [MenuItem("GameObject/Obstacles/MazgalAlevli")]
    static void AddMazgalAlevli()
    {
        Instantiate(Resources.Load<GameObject>("Obstacles/MazgalAlevli Variant"));
    }

    [MenuItem("GameObject/Obstacles/FanLefToRight")]
    static void AddFanLefToRight()
    {
        Instantiate(Resources.Load<GameObject>("Obstacles/FanLefToRight Variant"));
    }


    [MenuItem("GameObject/Obstacles/Keser")]
    static void AddKeser()
    {
        Instantiate(Resources.Load<GameObject>("Obstacles/Keser Variant"));
    }

    [MenuItem("GameObject/Obstacles/Knife")]
    static void AddKnife()
    {
        Instantiate(Resources.Load<GameObject>("Obstacles/Knife Variant"));
    }

    [MenuItem("GameObject/Obstacles/Lava")]
    static void AddLava()
    {
        Instantiate(Resources.Load<GameObject>("Obstacles/Lava Variant"));
    }

    [MenuItem("GameObject/Obstacles/PervaneliRampa")]
    static void AddPervaneliRampa()
    {
        Instantiate(Resources.Load<GameObject>("Obstacles/PervaneliRampa Variant"));
    }

    [MenuItem("GameObject/Obstacles/Razor")]
    static void AddRazor()
    {
        Instantiate(Resources.Load<GameObject>("Obstacles/razor Variant"));
    }

    [MenuItem("GameObject/Obstacles/UnderProp")]
    static void AddUnderProp()
    {
        Instantiate(Resources.Load<GameObject>("Obstacles/UnderProp Variant"));
    }

    [MenuItem("GameObject/Obstacles/UnderProp-Sadece Dikenli")]
    static void AddUnderProSadeceDikenli()
    {
        Instantiate(Resources.Load<GameObject>("Obstacles/underprop-dikkenli Variant"));
    }

    [MenuItem("GameObject/Obstacles/Wall")]
    static void AddWall()
    {
        Instantiate(Resources.Load<GameObject>("Obstacles/Wall Variant"));
    }

    [MenuItem("GameObject/Collectables/Coin")]
    static void AddCoin()
    {
        Instantiate(Resources.Load<GameObject>("Collectables/CollectableCoin Variant"));
    }

    [MenuItem("GameObject/Collectables/Blob")]
    static void AddBlob()
    {
        Instantiate(Resources.Load<GameObject>("Collectables/CollectableSmallBlob Variant"));
    }
}
#endif
