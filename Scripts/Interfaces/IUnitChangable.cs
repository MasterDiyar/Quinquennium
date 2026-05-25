namespace Quinquennium.Scripts.Interfaces;

public interface IUnitChangable
{
    UnitResource BaseResource { get; set; }
    UnitResource UpgradeResource { get; set; }
}