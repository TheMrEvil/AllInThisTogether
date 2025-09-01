using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x0200055B RID: 1371
	internal class BinaryFacetsChecker : FacetsChecker
	{
		// Token: 0x06003690 RID: 13968 RVA: 0x0012FB14 File Offset: 0x0012DD14
		internal override Exception CheckValueFacets(object value, XmlSchemaDatatype datatype)
		{
			byte[] value2 = (byte[])value;
			return this.CheckValueFacets(value2, datatype);
		}

		// Token: 0x06003691 RID: 13969 RVA: 0x0012FB30 File Offset: 0x0012DD30
		internal override Exception CheckValueFacets(byte[] value, XmlSchemaDatatype datatype)
		{
			RestrictionFacets restriction = datatype.Restriction;
			int num = value.Length;
			RestrictionFlags restrictionFlags = (restriction != null) ? restriction.Flags : ((RestrictionFlags)0);
			if (restrictionFlags != (RestrictionFlags)0)
			{
				if ((restrictionFlags & RestrictionFlags.Length) != (RestrictionFlags)0 && restriction.Length != num)
				{
					return new XmlSchemaException("The actual length is not equal to the specified length.", string.Empty);
				}
				if ((restrictionFlags & RestrictionFlags.MinLength) != (RestrictionFlags)0 && num < restriction.MinLength)
				{
					return new XmlSchemaException("The actual length is less than the MinLength value.", string.Empty);
				}
				if ((restrictionFlags & RestrictionFlags.MaxLength) != (RestrictionFlags)0 && restriction.MaxLength < num)
				{
					return new XmlSchemaException("The actual length is greater than the MaxLength value.", string.Empty);
				}
				if ((restrictionFlags & RestrictionFlags.Enumeration) != (RestrictionFlags)0 && !this.MatchEnumeration(value, restriction.Enumeration, datatype))
				{
					return new XmlSchemaException("The Enumeration constraint failed.", string.Empty);
				}
			}
			return null;
		}

		// Token: 0x06003692 RID: 13970 RVA: 0x0012FBDC File Offset: 0x0012DDDC
		internal override bool MatchEnumeration(object value, ArrayList enumeration, XmlSchemaDatatype datatype)
		{
			return this.MatchEnumeration((byte[])value, enumeration, datatype);
		}

		// Token: 0x06003693 RID: 13971 RVA: 0x0012FBEC File Offset: 0x0012DDEC
		private bool MatchEnumeration(byte[] value, ArrayList enumeration, XmlSchemaDatatype datatype)
		{
			for (int i = 0; i < enumeration.Count; i++)
			{
				if (datatype.Compare(value, (byte[])enumeration[i]) == 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003694 RID: 13972 RVA: 0x0012F445 File Offset: 0x0012D645
		public BinaryFacetsChecker()
		{
		}
	}
}
