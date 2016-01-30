using UnityEngine;
using System.Collections;

public class IceEnemy : EnemyRegular {

    void Start()
    {
        EnemyState = EnvironmentChanger.Environment.Ice;
    }
}
