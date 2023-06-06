using UnityEngine;

[CreateAssetMenu(menuName = "AiEnemy/Retreat")]
public class Retreat : AiEnemy
{
    public string TargetTag;
    public float RetreatDistance = 2f;
    public float RetreatRadius = 5f;

    public override void Ai(EnemyAiHelper aiHelper)
    {
        GameObject target = GameObject.FindGameObjectWithTag(TargetTag);
        if (target)
        {
            Vector2 targetPosition = target.transform.position;
            Vector2 currentPosition = aiHelper.gameObject.transform.position;
            float distanceToTarget = Vector2.Distance(currentPosition, targetPosition);

            if (distanceToTarget <= RetreatRadius)
            {
                Vector2 moveDirection = (currentPosition - targetPosition).normalized;
                Vector2 retreatPosition = currentPosition + moveDirection * RetreatDistance;

                var movement = aiHelper.gameObject.GetComponent<Movement>();
                if (movement)
                {
                    movement.MoveTowardTarget(retreatPosition);
                }
            }
        }
    }
}