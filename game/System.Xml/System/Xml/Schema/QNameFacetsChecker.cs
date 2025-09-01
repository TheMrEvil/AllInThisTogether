using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x02000559 RID: 1369
	internal class QNameFacetsChecker : FacetsChecker
	{
		// Token: 0x0600368A RID: 13962 RVA: 0x0012F9D0 File Offset: 0x0012DBD0
		internal override Exception CheckValueFacets(object value, XmlSchemaDatatype datatype)
		{
			XmlQualifiedName value2 = (XmlQualifiedName)datatype.ValueConverter.ChangeType(value, typeof(XmlQualifiedName));
			return this.CheckValueFacets(value2, datatype);
		}

		// Token: 0x0600368B RID: 13963 RVA: 0x0012FA04 File Offset: 0x0012DC04
		internal override Exception CheckValueFacets(XmlQualifiedName value, XmlSchemaDatatype datatype)
		{
			RestrictionFacets restriction = datatype.Restriction;
			RestrictionFlags restrictionFlags = (restriction != null) ? restriction.Flags : ((RestrictionFlags)0);
			if (restrictionFlags != (RestrictionFlags)0)
			{
				int length = value.ToString().Length;
				if ((restrictionFlags & RestrictionFlags.Length) != (RestrictionFlags)0 && restriction.Length != length)
				{
					return new XmlSchemaException("The actual length is not equal to the specified length.", string.Empty);
				}
				if ((restrictionFlags & RestrictionFlags.MinLength) != (RestrictionFlags)0 && length < restriction.MinLength)
				{
					return new XmlSchemaException("The actual length is less than the MinLength value.", string.Empty);
				}
				if ((restrictionFlags & RestrictionFlags.MaxLength) != (RestrictionFlags)0 && restriction.MaxLength < length)
				{
					return new XmlSchemaException("The actual length is greater than the MaxLength value.", string.Empty);
				}
				if ((restrictionFlags & RestrictionFlags.Enumeration) != (RestrictionFlags)0 && !this.MatchEnumeration(value, restriction.Enumeration))
				{
					return new XmlSchemaException("The Enumeration constraint failed.", string.Empty);
				}
			}
			return null;
		}

		// Token: 0x0600368C RID: 13964 RVA: 0x0012FAB7 File Offset: 0x0012DCB7
		internal override bool MatchEnumeration(object value, ArrayList enumeration, XmlSchemaDatatype datatype)
		{
			return this.MatchEnumeration((XmlQualifiedName)datatype.ValueConverter.ChangeType(value, typeof(XmlQualifiedName)), enumeration);
		}

		// Token: 0x0600368D RID: 13965 RVA: 0x0012FADC File Offset: 0x0012DCDC
		private bool MatchEnumeration(XmlQualifiedName value, ArrayList enumeration)
		{
			for (int i = 0; i < enumeration.Count; i++)
			{
				if (value.Equals((XmlQualifiedName)enumeration[i]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600368E RID: 13966 RVA: 0x0012F445 File Offset: 0x0012D645
		public QNameFacetsChecker()
		{
		}
	}
}
