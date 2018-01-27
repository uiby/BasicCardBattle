using UnityEngine;

[CreateAssetMenu(menuName="Prametar", fileName = "ParameterTable")]
public class ParameterTable : ScriptableObject {
    [SerializeField, Range(1, 100)] public int lifeScale;
    [SerializeField, Range(1, 50)] public int attackScale;
    [SerializeField, Range(1, 30)] public int defenseScale;
}