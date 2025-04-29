using UnityEngine;

public interface IRetrievable
{
    Transform GetTransform();
    void OnThrow(Transform context);
    void OnRetrieve(Transform receiver);
}