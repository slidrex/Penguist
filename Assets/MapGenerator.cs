using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private FrozenObject objectTemplate;
    public ushort ObjectCount;
    private int spawnedObjects;
    [SerializeField] private Transform spawnPointsContainer;
    [SerializeField] private PackFrozenObject[] NeccessaryObjects;
    [System.Serializable]
    private struct PackFrozenObject
    {
        public UnfrozenObject Entity;
        [Range(0, 1.0f)] public float PartialPackedIceChance;
    }
    private void Start()
    {
        GenerateMap();
    }
    private void GenerateMap()
    {
        for(int i = spawnedObjects; i < ObjectCount; i++)
        {
            int maxChildren = spawnPointsContainer.childCount;
            int randomPlace = Random.Range(0, maxChildren);
            Transform spawnPoint = spawnPointsContainer.GetChild(randomPlace);
            
            FrozenObject obj = Instantiate(objectTemplate, spawnPoint.transform.position, Quaternion.identity);
            DestroyImmediate(spawnPoint.gameObject);
            
            Sprite frozenObjSprite = null;
            UnfrozenObject unfrozenObject = null;
            bool partial = false;
            for(int j = 0; j < NeccessaryObjects.Length; j++)
            {
                if(NeccessaryObjects[j].Entity != null)
                {
                    frozenObjSprite = NeccessaryObjects[j].Entity.Renderer.sprite;
                    unfrozenObject = NeccessaryObjects[j].Entity;
                    NeccessaryObjects[j].Entity = null;
                    if(NeccessaryObjects[j].PartialPackedIceChance >= Random.Range(0, 1.0f))
                        partial = true;
                    break;
                }
            }
            obj.RenderFrozenObject(frozenObjSprite, unfrozenObject, partial);
        }
        
    }
}
