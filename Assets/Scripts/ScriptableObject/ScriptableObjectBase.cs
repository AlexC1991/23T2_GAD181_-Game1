using UnityEngine;


namespace AlexzanderCowell
{

[CreateAssetMenu(fileName = "Position Scriptable Object", menuName = "ScriptableObjects/Position Scriptable Object")]
    public class ScriptableObjectBase : ScriptableObject
    {
        public Transform spawnToThisPosition;
    }
}
