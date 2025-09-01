using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x0200055D RID: 1373
	internal class UnionFacetsChecker : FacetsChecker
	{
		// Token: 0x06003698 RID: 13976 RVA: 0x0012FD10 File Offset: 0x0012DF10
		internal override Exception CheckValueFacets(object value, XmlSchemaDatatype datatype)
		{
			RestrictionFacets restriction = datatype.Restriction;
			if ((((restriction != null && restriction.Flags != (RestrictionFlags)0) ? 1 : 0) & 16) != 0 && !this.MatchEnumeration(value, restriction.Enumeration, datatype))
			{
				return new XmlSchemaException("The Enumeration constraint failed.", string.Empty);
			}
			return null;
		}

		// Token: 0x06003699 RID: 13977 RVA: 0x0012FD58 File Offset: 0x0012DF58
		internal override bool MatchEnumeration(object value, ArrayList enumeration, XmlSchemaDatatype datatype)
		{
			for (int i = 0; i < enumeration.Count; i++)
			{
				if (datatype.Compare(value, enumeration[i]) == 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600369A RID: 13978 RVA: 0x0012F445 File Offset: 0x0012D645
		public UnionFacetsChecker()
		{
		}
	}
}
