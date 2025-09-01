using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.Threading;

namespace System.Xml.Schema
{
	// Token: 0x02000558 RID: 1368
	internal class StringFacetsChecker : FacetsChecker
	{
		// Token: 0x17000A1D RID: 2589
		// (get) Token: 0x06003682 RID: 13954 RVA: 0x0012F764 File Offset: 0x0012D964
		private static Regex LanguagePattern
		{
			get
			{
				if (StringFacetsChecker.languagePattern == null)
				{
					Regex value = new Regex("^([a-zA-Z]{1,8})(-[a-zA-Z0-9]{1,8})*$", RegexOptions.None);
					Interlocked.CompareExchange<Regex>(ref StringFacetsChecker.languagePattern, value, null);
				}
				return StringFacetsChecker.languagePattern;
			}
		}

		// Token: 0x06003683 RID: 13955 RVA: 0x0012F798 File Offset: 0x0012D998
		internal override Exception CheckValueFacets(object value, XmlSchemaDatatype datatype)
		{
			string value2 = datatype.ValueConverter.ToString(value);
			return this.CheckValueFacets(value2, datatype, true);
		}

		// Token: 0x06003684 RID: 13956 RVA: 0x0012F7BB File Offset: 0x0012D9BB
		internal override Exception CheckValueFacets(string value, XmlSchemaDatatype datatype)
		{
			return this.CheckValueFacets(value, datatype, true);
		}

		// Token: 0x06003685 RID: 13957 RVA: 0x0012F7C8 File Offset: 0x0012D9C8
		internal Exception CheckValueFacets(string value, XmlSchemaDatatype datatype, bool verifyUri)
		{
			int length = value.Length;
			RestrictionFacets restriction = datatype.Restriction;
			RestrictionFlags restrictionFlags = (restriction != null) ? restriction.Flags : ((RestrictionFlags)0);
			Exception ex = this.CheckBuiltInFacets(value, datatype.TypeCode, verifyUri);
			if (ex != null)
			{
				return ex;
			}
			if (restrictionFlags != (RestrictionFlags)0)
			{
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
				if ((restrictionFlags & RestrictionFlags.Enumeration) != (RestrictionFlags)0 && !this.MatchEnumeration(value, restriction.Enumeration, datatype))
				{
					return new XmlSchemaException("The Enumeration constraint failed.", string.Empty);
				}
			}
			return null;
		}

		// Token: 0x06003686 RID: 13958 RVA: 0x0012F88B File Offset: 0x0012DA8B
		internal override bool MatchEnumeration(object value, ArrayList enumeration, XmlSchemaDatatype datatype)
		{
			return this.MatchEnumeration(datatype.ValueConverter.ToString(value), enumeration, datatype);
		}

		// Token: 0x06003687 RID: 13959 RVA: 0x0012F8A4 File Offset: 0x0012DAA4
		private bool MatchEnumeration(string value, ArrayList enumeration, XmlSchemaDatatype datatype)
		{
			if (datatype.TypeCode == XmlTypeCode.AnyUri)
			{
				for (int i = 0; i < enumeration.Count; i++)
				{
					if (value.Equals(((Uri)enumeration[i]).OriginalString))
					{
						return true;
					}
				}
			}
			else
			{
				for (int j = 0; j < enumeration.Count; j++)
				{
					if (value.Equals((string)enumeration[j]))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06003688 RID: 13960 RVA: 0x0012F914 File Offset: 0x0012DB14
		private Exception CheckBuiltInFacets(string s, XmlTypeCode typeCode, bool verifyUri)
		{
			Exception result = null;
			switch (typeCode)
			{
			case XmlTypeCode.AnyUri:
				if (verifyUri)
				{
					Uri uri;
					result = XmlConvert.TryToUri(s, out uri);
				}
				break;
			case XmlTypeCode.NormalizedString:
				result = XmlConvert.TryVerifyNormalizedString(s);
				break;
			case XmlTypeCode.Token:
				result = XmlConvert.TryVerifyTOKEN(s);
				break;
			case XmlTypeCode.Language:
				if (s == null || s.Length == 0)
				{
					return new XmlSchemaException("The attribute value cannot be empty.", string.Empty);
				}
				if (!StringFacetsChecker.LanguagePattern.IsMatch(s))
				{
					return new XmlSchemaException("'{0}' is an invalid language identifier.", string.Empty);
				}
				break;
			case XmlTypeCode.NmToken:
				result = XmlConvert.TryVerifyNMTOKEN(s);
				break;
			case XmlTypeCode.Name:
				result = XmlConvert.TryVerifyName(s);
				break;
			case XmlTypeCode.NCName:
			case XmlTypeCode.Id:
			case XmlTypeCode.Idref:
			case XmlTypeCode.Entity:
				result = XmlConvert.TryVerifyNCName(s);
				break;
			}
			return result;
		}

		// Token: 0x06003689 RID: 13961 RVA: 0x0012F445 File Offset: 0x0012D645
		public StringFacetsChecker()
		{
		}

		// Token: 0x040027E1 RID: 10209
		private static Regex languagePattern;
	}
}
