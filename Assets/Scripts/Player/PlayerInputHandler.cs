using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerInputHandler : MonoBehaviour
{
    private PlayerController _playerController;

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        InputManager.Instance.OnClickEvent += OnClick;
    }

    private void OnClick(Vector2 pos)
    {
        _playerController.SetFollowTarget(InputManager.Instance.GetClickWorldPos());
    }

    private void OnDestroy()
    {
        InputManager.Instance.OnClickEvent -= OnClick;
    }
}
