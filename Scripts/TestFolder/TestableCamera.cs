using Godot;
using System;

public partial class TestableCamera : Camera2D
{
    [Export] public bool IsManualMove = true;
    [Export] public bool IsLocked = false;
    [Export] public float Speed = 100f;
    [Export] public float Margin = 30f; 
    [Export] public float Acceleration = 3.0f; 

    public override void _Process(double delta)
    {
       float dt = (float)delta;
       
       float zoomDelta = Input.GetAxis("q", "e"); 
       if (zoomDelta != 0) {
          float zoomSpeed = 2.0f,
                targetZoom = Zoom.X + zoomDelta * zoomSpeed * dt;
          Zoom = new Vector2(Mathf.Clamp(targetZoom, 0.5f, 5.0f), Mathf.Clamp(targetZoom, 0.5f, 5.0f));
       }
       if (IsLocked) return;
       if (IsManualMove) ManualMove(dt);
       else CornerMove(dt);
    }

    void ManualMove(float delta)
    {
       Vector2 vector = Input.GetVector("a", "d", "w", "s").Normalized();
       Position += vector * delta * Speed;
    }

    void CornerMove(float delta)
    {
       Vector2 viewSize = GetViewportRect().Size,
               mousePos = GetViewport().GetMousePosition(),
               moveDir = Vector2.Zero;
       
       float speedMultiplier = 1.0f;

       if (mousePos.X < Margin) {
           moveDir.X = -1;
           speedMultiplier = 1.0f + (1.0f - mousePos.X / Margin) * Acceleration;
       }else if (mousePos.X > viewSize.X - Margin) {
           moveDir.X = 1;
           speedMultiplier = 1.0f + (1.0f - (viewSize.X - mousePos.X) / Margin) * Acceleration;
       }

       if (mousePos.Y < Margin) {
           moveDir.Y = -1;
           speedMultiplier = Mathf.Max(speedMultiplier, 1.0f + (1.0f - mousePos.Y / Margin) * Acceleration);
       }else if (mousePos.Y > viewSize.Y - Margin) {
           moveDir.Y = 1;
           speedMultiplier = Mathf.Max(speedMultiplier, 1.0f + (1.0f - (viewSize.Y - mousePos.Y) / Margin) * Acceleration);
       }

       Position += moveDir * Speed * speedMultiplier * delta;
    }
}