using Godot;

public interface IInteractable
{
    void Interact(IEntity interactor);
    bool CanInteract();

    void ShowInteractText();

    void HideInteractText();
}