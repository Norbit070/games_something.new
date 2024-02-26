using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthViewsController : MonoBehaviour
{
    private const float MinViewportPosition = -0.1f;
    private const float MaxViewportPosition = 1.1f;
    [SerializeField] private CharacterHealthView _enemyHealthViewPrefab;
    [SerializeField] private Transform _enemyHealthViewsContainer;
    [SerializeField] private Vector3 _deltaHealthViewPosition = new Vector3(0, 2.2f, 0);
    private Dictionary<CharacterHealth, CharacterHealthView> _enemyHealthViewPairs = new Dictionary<CharacterHealth, CharacterHealthView>();
    private Camera _mainCamera;
    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _mainCamera = Camera.main;
        CharacterHealth[] enemyHealths = FindObjectsOfType<EnemyHealth>();
        for (int i = 0; i < enemyHealths.Length; i++)
        {
            CreateEnemyHealthView(enemyHealths[i]);
        }
    }
    private void CreateEnemyHealthView(CharacterHealth health)
    {
        CharacterHealthView characterHealthView = Instantiate(_enemyHealthViewPrefab, _enemyHealthViewsContainer);
        SetHealthViewScreenPosition(characterHealthView, health.transform.position);
        characterHealthView.Init(health);
        _enemyHealthViewPairs.Add(health, characterHealthView);
        health.OnDieWithObject += DestroyEnemyHealthView;
    }
    private void DestroyEnemyHealthView(CharacterHealth health)
    {
        CharacterHealthView view = _enemyHealthViewPairs[health];
        _enemyHealthViewPairs.Remove(health);
        Destroy(view.gameObject);
        health.OnDieWithObject -= DestroyEnemyHealthView;
    }
    private void Update()
    {
        RefreshViewsPositions();
    }

    private void RefreshViewsPositions()
    {
        foreach (var pair in _enemyHealthViewPairs)
        {
            Vector3 enemyPosition = pair.Key.transform.position + _deltaHealthViewPosition;
            if (!CheckPositionVisible(enemyPosition))
            {
                continue;
            }
            SetHealthViewScreenPosition(pair.Value, enemyPosition);
        }
    }
    private bool CheckPositionVisible(Vector3 position)
    {
        Vector3 viewportPosition = _mainCamera.WorldToViewportPoint(position);
        if (viewportPosition.x < MinViewportPosition || viewportPosition.x > MaxViewportPosition
                                                     || viewportPosition.y < MinViewportPosition || viewportPosition.y > MaxViewportPosition)
        {
            return false;
        }
        return true;
    }
    private void SetHealthViewScreenPosition(CharacterHealthView view,Vector3 worldPosition)
    {
        view.transform.position = _mainCamera.WorldToScreenPoint(worldPosition);
    }


}
