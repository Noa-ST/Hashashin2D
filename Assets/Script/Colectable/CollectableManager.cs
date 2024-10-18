using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CollectableItem
{
    [Range(0f, 1f)]
    public float spawnRate;
    public int amount;
    public Collectable collectablePb;

}

public class CollectableManager : Singleton<CollectableManager>
{
    [SerializeField] private CollectableItem[] _items;

    public void Spawn (Vector3 position)
    {
        if (_items == null && _items.Length <= 0) return;
        float spawnRateChecking = Random.value;

        for (int i = 0; i < _items.Length; i++)
        {
            var item = _items[i];
            if (item == null || item.spawnRate < spawnRateChecking) continue;

            CreateCollectable(position, item);
        }
    }

    private void CreateCollectable(Vector3 position, CollectableItem item)
    {
        if (item == null) return;
        for (int i = 0; i < item.amount; i ++)
        {
            Instantiate(item.collectablePb, position, Quaternion.identity);
        }
    }
}
