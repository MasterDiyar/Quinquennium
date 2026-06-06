using Godot;
namespace Quinquennium.Scripts.Interfaces;

public interface IInteractable
{
    void OnLeftClick(Vector2 mousePosition, Vector2 interactPosition);
    void OnRightClick(Vector2 mousePosition, Vector2 interactPosition);
}