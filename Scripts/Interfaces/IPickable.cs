using Godot;

namespace Quinquennium.Scripts.Interfaces;

public interface IPickable
{
    void OnSelect();
    void OnDeselect();
    void MoveTo (Vector2 targetPosition);
    bool IsSelectable { get; }
    
}