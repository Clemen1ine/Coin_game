using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAiHelper : MonoBehaviour
{
    public List<AiEnemy> aiEnemies;
    private int currentBehaviorIndex;
    private bool isRetreating;

    private void Start()
    {
        currentBehaviorIndex = 0;
        isRetreating = false;
    }

    private void Update()
    {
        if (!isRetreating)
        {
            aiEnemies[currentBehaviorIndex].Ai(this);
        }
    }

    public void SwitchAi()
    {
        if (!isRetreating)
        {
            StartCoroutine(SwitchAiCoroutine());
        }
    }

    private IEnumerator SwitchAiCoroutine()
    {
        // Switch to Retreat behavior
        currentBehaviorIndex = 1; // 1 is the index of the Retreat behavior
        aiEnemies[currentBehaviorIndex].Ai(this);
        isRetreating = true;

        yield return new WaitForSeconds(0.1f); // Duration of the Retreat behavior

        // Switch back to Chase behavior
        currentBehaviorIndex = 0; // 0 is the index of the Chase behavior
        aiEnemies[currentBehaviorIndex].Ai(this);
        isRetreating = false;
    }
}