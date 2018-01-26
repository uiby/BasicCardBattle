using UnityEngine;

[CreateAssetMenu(menuName="Prametar", fileName = "ParameterTable")]
public class ParameterTable : ScriptableObject {
    [SerializeField, Range(10, 50)] public int lifeScale;
    [SerializeField, Range(10, 50)] public int attackScale;
    [SerializeField, Range(10, 50)] public int defenseScale;
}