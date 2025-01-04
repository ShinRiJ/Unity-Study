using System;
using System.Collections;

using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Inventory))]
public class InventoryEditor : Editor
{
    private Inventory _inventory;

    private void OnEnable()
    {
        _inventory = (Inventory)target;
    }

    public override void OnInspectorGUI()
    {
        if(_inventory.Items.Count > 0)
        {

        }
        else
        {
            EditorGUILayout.LabelField("Инвентарь пустой");
        }

        if (GUILayout.Button("Добавить предмет", GUILayout.Width(300), GUILayout.Height(30)))
            _inventory.Items.Add(new Inventory.Item());
            

    }
}
