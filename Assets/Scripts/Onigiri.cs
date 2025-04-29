using UnityEngine;

public class Onigiri : MonoBehaviour, IPickable, IRetrievable
{
    [SerializeField] private Sprite icon;

    Sprite IPickable.Icon => icon;

    private bool isFetched;

    // Update is called once per frame
    void Update()
    {
        if (isFetched)
        {

        }
    }

    public void OnPickUp()
    {
        gameObject.SetActive(false);
    }

    public void OnUse(Transform contextActor)
    {
        gameObject.SetActive(true);
        OnThrow(contextActor);
    }

    public void OnThrow(Transform contextActor)
    {
        transform.position = contextActor.position;
        if (TryGetComponent<Rigidbody>(out var rb))
        {
            rb.isKinematic = false;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.AddForce(contextActor.forward * contextActor.GetComponent<ObjectControlHandler>().throwForce, ForceMode.Impulse);
        }
        Debug.Log("threw an onigiri!");
    }

    public void OnRetrieve(Transform receiver)
    {
        transform.position = receiver.position;
        if (TryGetComponent<Rigidbody>(out var rb))
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
        }

        Debug.Log("Onigiri retrieved!");
    }

    public Transform GetTransform()
    {
        return transform;
    }
}
