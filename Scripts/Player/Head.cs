using Godot;
using System;

public partial class Head : Sprite2D
{
    [Export] public Godot.Collections.Array<Sprite2D> HeadTextures { get; set; } = [];
    [Export] private Sprite2D MainHead;

    [Export] public float BaseSpeed { get; set; } = 3.0f;

    [Export] public float ReturnSpeed { get; set; } = 15.0f;

    public override void _Process(double delta)
    {
        if (HeadTextures == null || HeadTextures.Count == 0) return;

        Vector2 dir = -Input.GetVector("a", "d", "w", "s");

        if (dir.LengthSquared() > 0) {
            for (int i = 0; i < HeadTextures.Count; i++) {
                if (HeadTextures[i] == null) continue;
                float layerSpeed = (i + 1) * BaseSpeed;
                HeadTextures[i].Position += dir * layerSpeed * (float)delta;
            }
        }else {
            foreach (var t in HeadTextures) {
                if (t == null) continue;
                t.Position = t.Position.Lerp(Vector2.Zero, ReturnSpeed * (float)delta);
            }
        }
    }
}