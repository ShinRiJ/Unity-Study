using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    [Serializable]
    public class Item
    {
        public Int32 Id;
        public String Name;
        public Sprite Image ;   
    }

    [SerializeField] public List<Item> Items;
}
