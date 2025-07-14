using UnityEngine;

[CreateAssetMenu(fileName = "TestDataSO", menuName = "Scriptable Objects/TestDataSO")]
public class TestDataSO : ScriptableObject
{
    public string characterName;
    public int health;
    public float moveSpeed;
    public bool isPlayer;
    public Vector3 initialPosition;
    public Color characterColor;
}
