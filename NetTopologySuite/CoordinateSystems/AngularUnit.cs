// This code has lifted from ProjNet project code base, and the namespaces 
// updated to fit into NetTopologySuit. This is an interim measure, so that 
// ProjNet can be removed from Sharpmap. This code is to be refactor / written
//  to use the DotSpiatial project library.

// Copyright 2005, 2006 - Morten Nielsen (www.iter.dk)
//
// This file is part of Proj.Net.
// Proj.Net is free software; you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
// 
// Proj.Net is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.

// You should have received a copy of the GNU Lesser General Public License
// along with Proj.Net; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA 


using System;
using System.Globalization;
using System.Text;
using GeoAPI.Coordinates;
using GeoAPI.CoordinateSystems;

namespace NetTopologySuite.CoordinateSystems
{
    /// <summary>
    /// Definition of angular units.
    /// </summary>
    public class AngularUnit : Info, IAngularUnit
    {
        private readonly Double _radiansPerUnit;

        /// <summary>
        /// Initializes a new instance of a angular unit
        /// </summary>
        /// <param name="radiansPerUnit">Radians per unit</param>
        protected internal AngularUnit(Double radiansPerUnit)
            : this(radiansPerUnit, String.Empty, String.Empty, String.Empty, String.Empty,
                   String.Empty, String.Empty) {}

        /// <summary>
        /// Initializes a new instance of a angular unit
        /// </summary>
        /// <param name="radiansPerUnit">Radians per unit</param>
        /// <param name="name">Name</param>
        /// <param name="authority">Authority name</param>
        /// <param name="authorityCode">Authority-specific identification code.</param>
        /// <param name="alias">Alias</param>
        /// <param name="abbreviation">Abbreviation</param>
        /// <param name="remarks">Provider-supplied remarks</param>
        protected internal AngularUnit(Double radiansPerUnit, String name, String authority,
                                       String authorityCode, String alias, String abbreviation, 
                                       String remarks)
            : base(name, authority, authorityCode, alias, abbreviation, remarks)
        {
            _radiansPerUnit = radiansPerUnit;
        }

        #region Predifined units

        /// <summary>
        /// The angular degrees are π/180 = 0.017453292519943295769236907684886 radians
        /// </summary>
        public static AngularUnit Degrees
        {
            get
            {
                return new AngularUnit(0.017453292519943295769236907684886,
                                       "degree", "EPSG", "9102", "deg", String.Empty, "=π/180 radians");
            }
        }

        /// <summary>
        /// SI standard unit
        /// </summary>
        public static AngularUnit Radian
        {
            get
            {
                return new AngularUnit(
                    1, "radian", "EPSG", "9101", "rad", String.Empty,
                    "SI standard unit.");
            }
        }

        /// <summary>
        /// π / 200 = 0.015707963267948966192313216916398 radians
        /// </summary>
        public static AngularUnit Grad
        {
            get
            {
                return new AngularUnit(0.015707963267948966192313216916398,
                                       "grad", "EPSG", "9105", "gr", String.Empty, "=π/200 radians.");
            }
        }

        /// <summary>
        /// π / 200 = 0.015707963267948966192313216916398 radians
        /// </summary>		
        public static AngularUnit Gon
        {
            get
            {
                return
                    new AngularUnit(0.015707963267948966192313216916398,
                                    "gon", "EPSG", "9106", "g", String.Empty, "=π/200 radians.");
            }
        }

        #endregion

        #region IAngularUnit Members

        /// <summary>
        /// Gets or sets the number of radians per <see cref="AngularUnit"/>.
        /// </summary>
        public Double RadiansPerUnit
        {
            get { return _radiansPerUnit; }
        }

        /// <summary>
        /// Returns the Well-Known Text for this object
        /// as defined in the simple features specification.
        /// </summary>
        public override String Wkt
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat(CultureInfo.InvariantCulture.NumberFormat, "UNIT[\"{0}\", {1}", Name, RadiansPerUnit);

                if (!String.IsNullOrEmpty(Authority) && !String.IsNullOrEmpty(AuthorityCode))
                {
                    sb.AppendFormat(", AUTHORITY[\"{0}\", \"{1}\"]", Authority, AuthorityCode);
                }

                sb.Append("]");
                return sb.ToString();
            }
        }

        /// <summary>
        /// Gets an XML representation of this object.
        /// </summary>
        public override String Xml
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture.NumberFormat,
                                     "<CS_AngularUnit RadiansPerUnit=\"{0}\">{1}</CS_AngularUnit>",
                                     RadiansPerUnit, InfoXml);
            }
        }

        #endregion

        /// <summary>
        /// Checks whether the values of this instance is equal to the values of another instance.
        /// Only parameters used for coordinate system are used for comparison.
        /// Name, abbreviation, authority, alias and remarks are ignored in the comparison.
        /// </summary>
        public override Boolean EqualParams(IInfo other)
        {
            AngularUnit otherUnit = other as AngularUnit;

            if (ReferenceEquals(otherUnit, null))
            {
                return false;
            }

            return Tolerance.Global.Equal(otherUnit.RadiansPerUnit, RadiansPerUnit);
        }
    }
}