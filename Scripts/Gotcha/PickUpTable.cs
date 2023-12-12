using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PickUpTable
{
    public CharacterSO Character;
    public int weight;
    public Enums.CharacterGrade Grade;
    public PickUpTable(PickUpTable pickUp)
    {
        this.Character = pickUp.Character;
        this.Grade = pickUp.Grade;
        this.weight = pickUp.weight;
    }
}
