using UnityEngine;

public class SpawnPoints : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints;

    public int Length => _spawnPoints.Length;

    public void GetPoint(int index, out Vector3 position, out Vector3 rotation)
    {
        if(index >= _spawnPoints.Length)
        {
            position = Vector3.zero;
            rotation = Vector3.zero;
            return;
        }
        position = _spawnPoints[index].position;
        rotation = _spawnPoints[index].eulerAngles;
    }

}
