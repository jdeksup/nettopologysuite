#pragma warning disable 1591
namespace GeoAPI.Geometries
{
    public static class ILinearRingExtension
    {
        /// <summary>
        /// Gets a value indicating whether this ring is oriented counter-clockwise.
        /// </summary>
        public static bool IsCCW(this ILinearRing linearRing)
        {
            return NetTopologySuite.Algorithm.CGAlgorithms.IsCCW(linearRing.CoordinateSequence);
        }
    }
}
