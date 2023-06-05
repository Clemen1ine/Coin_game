using UnityEngine;

public abstract class AiEnemy :ScriptableObject
{
    public abstract void Ai(EnemyAiHelper aiHelper);
}
