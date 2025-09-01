using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x02000557 RID: 1367
	internal class DateTimeFacetsChecker : FacetsChecker
	{
		// Token: 0x0600367D RID: 13949 RVA: 0x0012F5C4 File Offset: 0x0012D7C4
		internal override Exception CheckValueFacets(object value, XmlSchemaDatatype datatype)
		{
			DateTime value2 = datatype.ValueConverter.ToDateTime(value);
			return this.CheckValueFacets(value2, datatype);
		}

		// Token: 0x0600367E RID: 13950 RVA: 0x0012F5E8 File Offset: 0x0012D7E8
		internal override Exception CheckValueFacets(DateTime value, XmlSchemaDatatype datatype)
		{
			RestrictionFacets restriction = datatype.Restriction;
			RestrictionFlags restrictionFlags = (restriction != null) ? restriction.Flags : ((RestrictionFlags)0);
			if ((restrictionFlags & RestrictionFlags.MaxInclusive) != (RestrictionFlags)0 && datatype.Compare(value, (DateTime)restriction.MaxInclusive) > 0)
			{
				return new XmlSchemaException("The MaxInclusive constraint failed.", string.Empty);
			}
			if ((restrictionFlags & RestrictionFlags.MaxExclusive) != (RestrictionFlags)0 && datatype.Compare(value, (DateTime)restriction.MaxExclusive) >= 0)
			{
				return new XmlSchemaException("The MaxExclusive constraint failed.", string.Empty);
			}
			if ((restrictionFlags & RestrictionFlags.MinInclusive) != (RestrictionFlags)0 && datatype.Compare(value, (DateTime)restriction.MinInclusive) < 0)
			{
				return new XmlSchemaException("The MinInclusive constraint failed.", string.Empty);
			}
			if ((restrictionFlags & RestrictionFlags.MinExclusive) != (RestrictionFlags)0 && datatype.Compare(value, (DateTime)restriction.MinExclusive) <= 0)
			{
				return new XmlSchemaException("The MinExclusive constraint failed.", string.Empty);
			}
			if ((restrictionFlags & RestrictionFlags.Enumeration) != (RestrictionFlags)0 && !this.MatchEnumeration(value, restriction.Enumeration, datatype))
			{
				return new XmlSchemaException("The Enumeration constraint failed.", string.Empty);
			}
			return null;
		}

		// Token: 0x0600367F RID: 13951 RVA: 0x0012F70D File Offset: 0x0012D90D
		internal override bool MatchEnumeration(object value, ArrayList enumeration, XmlSchemaDatatype datatype)
		{
			return this.MatchEnumeration(datatype.ValueConverter.ToDateTime(value), enumeration, datatype);
		}

		// Token: 0x06003680 RID: 13952 RVA: 0x0012F724 File Offset: 0x0012D924
		private bool MatchEnumeration(DateTime value, ArrayList enumeration, XmlSchemaDatatype datatype)
		{
			for (int i = 0; i < enumeration.Count; i++)
			{
				if (datatype.Compare(value, (DateTime)enumeration[i]) == 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003681 RID: 13953 RVA: 0x0012F445 File Offset: 0x0012D645
		public DateTimeFacetsChecker()
		{
		}
	}
}
