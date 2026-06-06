using Godot;
using System;
using System.Globalization;
using Quinquennium.Scripts.Unit;

public partial class HPshower : Label
{
    [Export] public float BaseSpeed { get; set; } = 15.0f;
    [Export] public float MinLifeTime { get; set; } = 0.5f;
    [Export] public float MaxLifeTime { get; set; } = 1.5f;

    private float _moveAngle;
    private float _lifeTime;
    private float _moveSpeed;
    private bool _isTurnOn;
    
    private float _currentDamage; 
    
    private UnitBase _parentUnit;

    public override void _Ready()
    {
        if (GetParent() is UnitBase unit) {
            _parentUnit = unit;
            _parentUnit.OnTakeDamage += OnTakeDamage;
        }
        
        Visible = false;
    }

    public override void _ExitTree()
    {
        if (_parentUnit != null) {
            _parentUnit.OnTakeDamage -= OnTakeDamage;
        }
    }

    private void OnTakeDamage(float damage)
    {
        if (_isTurnOn) {
            _currentDamage += damage;
            Text = _currentDamage.ToString(CultureInfo.InvariantCulture);
            if (_lifeTime < MinLifeTime)
                _lifeTime = MinLifeTime; 
            
        }else {
            _currentDamage = damage;
            Position = Vector2.Zero;
            Text = _currentDamage.ToString(CultureInfo.InvariantCulture);
            
            _moveAngle = GD.Randf() * Mathf.Tau;
            _lifeTime = (float)GD.RandRange(MinLifeTime, MaxLifeTime);
            _moveSpeed = (GD.Randf() + 0.5f) * BaseSpeed;
            
            _isTurnOn = true;
            Visible = true;
        }
    }

    public override void _Process(double delta)
    {
        if (!_isTurnOn) return;

        float fDelta = (float)delta;
        _lifeTime -= fDelta;
        
        Position += Vector2.FromAngle(_moveAngle) * _moveSpeed * fDelta;

        if (!(_lifeTime <= 0)) return;
        _isTurnOn = false;
        Visible = false;
        _currentDamage = 0;
    }
}