using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Class", menuName = "Character/Class")]
public class CharacterClass : ScriptableObject
{
    //Do base stats here instead
    //attack type (ranged, magic, slashing, piercing)
    //weapon type
    //base movement speed
    //base damage
    //special Ability
    //movement Ability
    public new string name = "New Class";
    public string weaponType;
    public string armourType;
    public string specialAbility;
    public string defensiveAbility;


    public Sprite front;
    public Sprite back;
    public Sprite portrait;    
}
