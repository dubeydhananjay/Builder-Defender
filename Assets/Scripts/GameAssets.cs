using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    static GameAssets instance;
    public static GameAssets Instance
    {
        get
        {
            if (instance == null)
                instance = Resources.Load<GameAssets>("GameAssets");
            return instance;
        }
    }

    public ArrowProjectile pfArrowProjectile;
    public BuildingConstruction pfBuildingConstruction;
    public Enemy pfEnemy;
    public Transform pfEnemyDieParticles;
    public Transform pfBuildingDestroyedParticles;
    public Transform pfBuildingPlacedParticles;
}
