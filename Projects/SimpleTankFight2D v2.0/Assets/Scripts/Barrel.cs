using UnityEngine;
using System;

[CreateAssetMenu(fileName ="New Barrel", menuName = "Items/Barrel" )]
public class Barrel : ScriptableObject
{
    public String title;
    public String description;
    public Sprite sprite;
    public Int32 radius; 
}
