using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private Transform _level;

    private Vector2 _levelSize;
    
    private void Awake()
    {
        _levelSize = _level.transform.localScale;
    }

    public Vector3 GetRandomPositionOnLevel()
    {
        var x = Random.Range(0, _levelSize.x);
        var y = Random.Range(0, _levelSize.y);
        
        return new Vector3(x, y, 0);
    }
}
