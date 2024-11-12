using TrakHound.Blazor.Diagrams.Core.Geometry;

namespace TrakHound.Blazor.Diagrams.Core.Models.Base;

public interface IHasBounds
{
    public Rectangle? GetBounds();
}