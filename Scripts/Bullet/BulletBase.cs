using Godot;
using System;

public partial class BulletBase : Area2D
{
    private float _speed;
    private float _lifeTime;
    
    private PackedScene _onDieEffect;

    public void Init(BulletResource resource)
    {
        if (resource == null) return;

        _speed = resource.Speed;
        _lifeTime = resource.LifeTime;
        _onDieEffect = resource.OnDie;

        if (resource.TextureScene != null) {
            Node textureNode = resource.TextureScene.Instantiate();
            AddChild(textureNode);
        }

        if (resource.OnFly != null) {
            Node flyEffect = resource.OnFly.Instantiate();
            AddChild(flyEffect);
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        float fDelta = (float)delta;

        Position += Transform.X * _speed * fDelta;

        _lifeTime -= fDelta;
        if (_lifeTime <= 0)
            Die();
        
    }

    public void Die()
    {
        if (_onDieEffect != null) {
            Node2D effect = _onDieEffect.Instantiate<Node2D>();
            
            GetTree().CurrentScene.AddChild(effect);
            effect.GlobalPosition = GlobalPosition;
            
        }

        QueueFree();
    }
}