using UnityEngine;

[CreateAssetMenu(menuName = "AiEnemy/Chase")]
public class Chase : AiEnemy
{
    public string TargetTag;
    public override void Ai(EnemyAiHelper aiHelper)
    {
        GameObject target = GameObject.FindGameObjectWithTag(TargetTag);
        if (target)
        {
            var movement = aiHelper.gameObject.GetComponent<Movement>();
            if (movement)
            {
                movement.MoveTowardTarget(target.transform.position);
            }
        }
    }
}