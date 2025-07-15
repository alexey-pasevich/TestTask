using UnityEngine;
using System.Collections.Generic;

public enum CharacterClass { Warrior, Mage, Archer }

[CreateAssetMenu(fileName = "TestDataSO", menuName = "Scriptable Objects/TestDataSO")]
public class TestDataSO : ScriptableObject
{
    public string characterName;
    public int health;
    public float moveSpeed;
    public bool isPlayer;
    public Vector3 initialPosition;
    public Color characterColor;

    public GameObject prefab;  

    // Коллекции
    public int[] inventoryIDs = { 1, 2, 3 };
    public List<string> abilities = new List<string> { "Fireball", "Dash" };

    // Enum
    public CharacterClass characterClass = CharacterClass.Warrior;
}
