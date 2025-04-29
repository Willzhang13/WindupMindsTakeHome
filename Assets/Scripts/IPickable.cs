using UnityEngine;

public interface IPickable
{
    void OnPickUp();
    void OnUse(Transform contextActor);
    Sprite Icon { get; }
}
