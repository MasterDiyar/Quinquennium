using System.Collections.Generic;
using Godot;
using Quinquennium.Scripts.Unit;

namespace Quinquennium.Scripts.UI;

public partial class PickedBox : Control
{
    [Export] public GridContainer UnitGrid;
    [Export] public int MaxUnits = 30;
    public List<UnitBase> Units = [];

    public override void _Ready()
    {
        for (int i = 0; i < MaxUnits; i++)
        {
            var btn = new Button() {
                Flat = true,
                CustomMinimumSize = Vector2.One * 32,
                Visible = false
            };
            UnitGrid.AddChild(btn);
        }
    }

    public void SetUnits(List<UnitBase> units)
    {
        Clear();
        Units = units;
        var nodes = UnitGrid.GetChildren();
        int displayCount = Mathf.Min(units.Count, nodes.Count);
        for (var i = 0; i < displayCount; i++)
        {
            var node = nodes[i];
            if (node is not Button btn) continue; 
            btn.Visible = true;
            btn.Icon = units[i].BaseResource.Icon;
        }
    }
    
    void Clear()
    {
        foreach (var node in UnitGrid.GetChildren())
            if (node is Button btn) {
                btn.Visible = false;
                btn.Icon = null;
            }
        Units.Clear();
    }
}