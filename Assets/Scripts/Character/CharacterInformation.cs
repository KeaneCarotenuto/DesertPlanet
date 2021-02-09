using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInformation : MonoBehaviour
{
    public CharacterClass characterClass;
    private CharacterVisuals characterVisuals;

    //BaseStats
    public string className;
    public string weaponType;
    public string armourType;
    public string specialAbility;
    public string defensiveAbility;

    //Visuals
    public Sprite front;
    public Sprite back;
    public Sprite portrait;

    void Start()
    {
        //Stats
        className = characterClass.name;
        weaponType = characterClass.weaponType;
        armourType = characterClass.armourType;
        specialAbility = characterClass.specialAbility;
        defensiveAbility = characterClass.defensiveAbility;
        //Visuals
        front = characterClass.front;
        back = characterClass.back;
        portrait = characterClass.portrait;

        characterVisuals = GetComponent<CharacterVisuals>();

        characterVisuals.UpdateVisuals(front, back, portrait);

    } 
}
