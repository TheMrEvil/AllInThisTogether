using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x02000555 RID: 1365
	internal class Numeric2FacetsChecker : FacetsChecker
	{
		// Token: 0x06003672 RID: 13938 RVA: 0x0012F2D0 File Offset: 0x0012D4D0
		internal override Exception CheckValueFacets(object value, XmlSchemaDatatype datatype)
		{
			double value2 = datatype.ValueConverter.ToDouble(value);
			return this.CheckValueFacets(value2, datatype);
		}

		// Token: 0x06003673 RID: 13939 RVA: 0x0012F2F4 File Offset: 0x0012D4F4
		internal override Exception CheckValueFacets(double value, XmlSchemaDatatype datatype)
		{
			RestrictionFacets restriction = datatype.Restriction;
			RestrictionFlags restrictionFlags = (restriction != null) ? restriction.Flags : ((RestrictionFlags)0);
			XmlValueConverter valueConverter = datatype.ValueConverter;
			if ((restrictionFlags & RestrictionFlags.MaxInclusive) != (RestrictionFlags)0 && value > valueConverter.ToDouble(restriction.MaxInclusive))
			{
				return new XmlSchemaException("The MaxInclusive constraint failed.", string.Empty);
			}
			if ((restrictionFlags & RestrictionFlags.MaxExclusive) != (RestrictionFlags)0 && value >= valueConverter.ToDouble(restriction.MaxExclusive))
			{
				return new XmlSchemaException("The MaxExclusive constraint failed.", string.Empty);
			}
			if ((restrictionFlags & RestrictionFlags.MinInclusive) != (RestrictionFlags)0 && value < valueConverter.ToDouble(restriction.MinInclusive))
			{
				return new XmlSchemaException("The MinInclusive constraint failed.", string.Empty);
			}
			if ((restrictionFlags & RestrictionFlags.MinExclusive) != (RestrictionFlags)0 && value <= valueConverter.ToDouble(restriction.MinExclusive))
			{
				return new XmlSchemaException("The MinExclusive constraint failed.", string.Empty);
			}
			if ((restrictionFlags & RestrictionFlags.Enumeration) != (RestrictionFlags)0 && !this.MatchEnumeration(value, restriction.Enumeration, valueConverter))
			{
				return new XmlSchemaException("The Enumeration constraint failed.", string.Empty);
			}
			return null;
		}

		// Token: 0x06003674 RID: 13940 RVA: 0x0012F3E0 File Offset: 0x0012D5E0
		internal override Exception CheckValueFacets(float value, XmlSchemaDatatype datatype)
		{
			double value2 = (double)value;
			return this.CheckValueFacets(value2, datatype);
		}

		// Token: 0x06003675 RID: 13941 RVA: 0x0012F3F8 File Offset: 0x0012D5F8
		internal override bool MatchEnumeration(object value, ArrayList enumeration, XmlSchemaDatatype datatype)
		{
			return this.MatchEnumeration(datatype.ValueConverter.ToDouble(value), enumeration, datatype.ValueConverter);
		}

		// Token: 0x06003676 RID: 13942 RVA: 0x0012F414 File Offset: 0x0012D614
		private bool MatchEnumeration(double value, ArrayList enumeration, XmlValueConverter valueConverter)
		{
			for (int i = 0; i < enumeration.Count; i++)
			{
				if (value == valueConverter.ToDouble(enumeration[i]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003677 RID: 13943 RVA: 0x0012F445 File Offset: 0x0012D645
		public Numeric2FacetsChecker()
		{
		}
	}
}
