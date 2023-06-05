using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAiHelper : MonoBehaviour
{
    public List<AiEnemy> aienemy;

    private void Update()
    {
        aienemy[0].Ai(this);
    }
}
