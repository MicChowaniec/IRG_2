using System.Collections.Generic;
using UnityEngine;
using System;

public class VisionSystem : MonoBehaviour
{
    public LayerMask layer;
    public float range;

    public HashSet<int> ListOfDiscoveredFields = new();
    public bool human;
    public Player owner;

    private PlayerManager _playerManager;
    private Collider[] _colliderBuffer = new Collider[50]; // Adjust size as needed

    private void OnEnable()
    {
        _playerManager = FindAnyObjectByType<PlayerManager>();
        PlayerManager.PlayersInstantiated += ScanForVisible;
        StarlingSkillScript.SetNest += ScanForVisible;

        if (gameObject.activeInHierarchy)
        {
            ScanForVisible();
        }
    }

    private void OnDisable()
    {
        PlayerManager.PlayersInstantiated -= ScanForVisible;
        StarlingSkillScript.SetNest -= ScanForVisible;
    }

    public void ScanForVisible()
    {
        if (_playerManager == null) return;

        if (TryGetComponent<PlayerScript>(out PlayerScript playerScript))
        {
            human = playerScript.player.human;
        }

        int hitCount = Physics.OverlapSphereNonAlloc(transform.position, range, _colliderBuffer, layer);

        for (int i = 0; i < hitCount; i++)
        {
            GameObject go = _colliderBuffer[i].gameObject;
            CheckRecursively(go);

            if (go.TryGetComponent<TileScript>(out TileScript ts))
            {
                int idToAdd = ts.TSO.id;
                if (ListOfDiscoveredFields.Add(idToAdd))
                {
                    if (owner != null)
                    {
                        _playerManager.GetGameObjectFromSO(owner)
                            ?.GetComponent<VisionSystem>()
                            ?.ListOfDiscoveredFields.Add(idToAdd);
                    }
                }
            }
        }
    }

    private void CheckRecursively(GameObject obj)
    {
        Queue<Transform> queue = new Queue<Transform>();
        queue.Enqueue(obj.transform);

        while (queue.Count > 0)
        {
            Transform current = queue.Dequeue();

            if (human == true || (owner != null && owner.human == true))
            {
                current.gameObject.layer = 6;
            }

            foreach (Transform child in current)
            {
                queue.Enqueue(child);
            }
        }
    }
}