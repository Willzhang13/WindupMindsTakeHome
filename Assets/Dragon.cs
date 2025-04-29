using UnityEngine;

public class Dragon : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float stoppingDistance = 0.5f;

    [Header("Retrieve Target")]
    [SerializeField] private Transform mouthSlot;

    [Header("Animation")]
    [SerializeField] private Animator animator;

    private IRetrievable targetItem;
    private bool isRetrieving = false;

    private void OnEnable()
    {
        ObjectControlHandler.OnItemThrown += OnItemThrown;
    }

    private void OnDisable()
    {
        ObjectControlHandler.OnItemThrown -= OnItemThrown;
    }

    private void OnItemThrown(IRetrievable item)
    {
        targetItem = item;
        isRetrieving = true;
    }

    private void Update()
    {
        if (isRetrieving && targetItem != null)
        {
            Transform itemTransform = targetItem.GetTransform();
            float distance = Vector3.Distance(transform.position, itemTransform.position);

            if (distance > stoppingDistance)
            {
                Vector3 dir = (itemTransform.position - transform.position).normalized;
                transform.position += dir * moveSpeed * Time.deltaTime;
                transform.LookAt(itemTransform);
                animator.SetBool("isMoving", true);
            }
            else
            {
                animator.SetBool("isMoving", false);
                targetItem.OnRetrieve(mouthSlot);
                targetItem = null;
                isRetrieving = false;
            }
        }
    }
}
