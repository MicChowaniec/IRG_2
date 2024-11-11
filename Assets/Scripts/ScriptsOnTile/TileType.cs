using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Tile_Type", menuName = "Tile Type", order = 0)]
public class TileType : ScriptableObject
{
    public TileTypesEnum type;
    public bool passable;
    public bool rootable;
    public GameObjectTypeEnum gameObjectType;

}



