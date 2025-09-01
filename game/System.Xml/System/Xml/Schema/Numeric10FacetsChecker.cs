using System;
using System.Collections;
using System.Globalization;

namespace System.Xml.Schema
{
	// Token: 0x02000554 RID: 1364
	internal class Numeric10FacetsChecker : FacetsChecker
	{
		// Token: 0x06003667 RID: 13927 RVA: 0x0012EFAC File Offset: 0x0012D1AC
		internal Numeric10FacetsChecker(decimal minVal, decimal maxVal)
		{
			this.minValue = minVal;
			this.maxValue = maxVal;
		}

		// Token: 0x06003668 RID: 13928 RVA: 0x0012EFC4 File Offset: 0x0012D1C4
		internal override Exception CheckValueFacets(object value, XmlSchemaDatatype datatype)
		{
			decimal value2 = datatype.ValueConverter.ToDecimal(value);
			return this.CheckValueFacets(value2, datatype);
		}

		// Token: 0x06003669 RID: 13929 RVA: 0x0012EFE8 File Offset: 0x0012D1E8
		internal override Exception CheckValueFacets(decimal value, XmlSchemaDatatype datatype)
		{
			RestrictionFacets restriction = datatype.Restriction;
			RestrictionFlags restrictionFlags = (restriction != null) ? restriction.Flags : ((RestrictionFlags)0);
			XmlValueConverter valueConverter = datatype.ValueConverter;
			if (value > this.maxValue || value < this.minValue)
			{
				return new OverflowException(Res.GetString("Value '{0}' was either too large or too small for {1}.", new object[]
				{
					value.ToString(CultureInfo.InvariantCulture),
					datatype.TypeCodeString
				}));
			}
			if (restrictionFlags == (RestrictionFlags)0)
			{
				return null;
			}
			if ((restrictionFlags & RestrictionFlags.MaxInclusive) != (RestrictionFlags)0 && value > valueConverter.ToDecimal(restriction.MaxInclusive))
			{
				return new XmlSchemaException("The MaxInclusive constraint failed.", string.Empty);
			}
			if ((restrictionFlags & RestrictionFlags.MaxExclusive) != (RestrictionFlags)0 && value >= valueConverter.ToDecimal(restriction.MaxExclusive))
			{
				return new XmlSchemaException("The MaxExclusive constraint failed.", string.Empty);
			}
			if ((restrictionFlags & RestrictionFlags.MinInclusive) != (RestrictionFlags)0 && value < valueConverter.ToDecimal(restriction.MinInclusive))
			{
				return new XmlSchemaException("The MinInclusive constraint failed.", string.Empty);
			}
			if ((restrictionFlags & RestrictionFlags.MinExclusive) != (RestrictionFlags)0 && value <= valueConverter.ToDecimal(restriction.MinExclusive))
			{
				return new XmlSchemaException("The MinExclusive constraint failed.", string.Empty);
			}
			if ((restrictionFlags & RestrictionFlags.Enumeration) != (RestrictionFlags)0 && !this.MatchEnumeration(value, restriction.Enumeration, valueConverter))
			{
				return new XmlSchemaException("The Enumeration constraint failed.", string.Empty);
			}
			return this.CheckTotalAndFractionDigits(value, restriction.TotalDigits, restriction.FractionDigits, (restrictionFlags & RestrictionFlags.TotalDigits) > (RestrictionFlags)0, (restrictionFlags & RestrictionFlags.FractionDigits) > (RestrictionFlags)0);
		}

		// Token: 0x0600366A RID: 13930 RVA: 0x0012F160 File Offset: 0x0012D360
		internal override Exception CheckValueFacets(long value, XmlSchemaDatatype datatype)
		{
			decimal value2 = value;
			return this.CheckValueFacets(value2, datatype);
		}

		// Token: 0x0600366B RID: 13931 RVA: 0x0012F17C File Offset: 0x0012D37C
		internal override Exception CheckValueFacets(int value, XmlSchemaDatatype datatype)
		{
			decimal value2 = value;
			return this.CheckValueFacets(value2, datatype);
		}

		// Token: 0x0600366C RID: 13932 RVA: 0x0012F198 File Offset: 0x0012D398
		internal override Exception CheckValueFacets(short value, XmlSchemaDatatype datatype)
		{
			decimal value2 = value;
			return this.CheckValueFacets(value2, datatype);
		}

		// Token: 0x0600366D RID: 13933 RVA: 0x0012F1B4 File Offset: 0x0012D3B4
		internal override Exception CheckValueFacets(byte value, XmlSchemaDatatype datatype)
		{
			decimal value2 = value;
			return this.CheckValueFacets(value2, datatype);
		}

		// Token: 0x0600366E RID: 13934 RVA: 0x0012F1D0 File Offset: 0x0012D3D0
		internal override bool MatchEnumeration(object value, ArrayList enumeration, XmlSchemaDatatype datatype)
		{
			return this.MatchEnumeration(datatype.ValueConverter.ToDecimal(value), enumeration, datatype.ValueConverter);
		}

		// Token: 0x0600366F RID: 13935 RVA: 0x0012F1EC File Offset: 0x0012D3EC
		internal bool MatchEnumeration(decimal value, ArrayList enumeration, XmlValueConverter valueConverter)
		{
			for (int i = 0; i < enumeration.Count; i++)
			{
				if (value == valueConverter.ToDecimal(enumeration[i]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003670 RID: 13936 RVA: 0x0012F224 File Offset: 0x0012D424
		internal Exception CheckTotalAndFractionDigits(decimal value, int totalDigits, int fractionDigits, bool checkTotal, bool checkFraction)
		{
			decimal d = FacetsChecker.Power(10, totalDigits) - 1m;
			int num = 0;
			if (value < 0m)
			{
				value = decimal.Negate(value);
			}
			while (decimal.Truncate(value) != value)
			{
				value *= 10m;
				num++;
			}
			if (checkTotal && (value > d || num > totalDigits))
			{
				return new XmlSchemaException("The TotalDigits constraint failed.", string.Empty);
			}
			if (checkFraction && num > fractionDigits)
			{
				return new XmlSchemaException("The FractionDigits constraint failed.", string.Empty);
			}
			return null;
		}

		// Token: 0x06003671 RID: 13937 RVA: 0x0012F2B8 File Offset: 0x0012D4B8
		// Note: this type is marked as 'beforefieldinit'.
		static Numeric10FacetsChecker()
		{
		}

		// Token: 0x040027DE RID: 10206
		private static readonly char[] signs = new char[]
		{
			'+',
			'-'
		};

		// Token: 0x040027DF RID: 10207
		private decimal maxValue;

		// Token: 0x040027E0 RID: 10208
		private decimal minValue;
	}
}
