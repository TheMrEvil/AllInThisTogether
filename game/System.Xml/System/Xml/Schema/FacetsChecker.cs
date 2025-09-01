using System;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;

namespace System.Xml.Schema
{
	// Token: 0x02000551 RID: 1361
	internal abstract class FacetsChecker
	{
		// Token: 0x0600363A RID: 13882 RVA: 0x0012D6E8 File Offset: 0x0012B8E8
		internal virtual Exception CheckLexicalFacets(ref string parseString, XmlSchemaDatatype datatype)
		{
			this.CheckWhitespaceFacets(ref parseString, datatype);
			return this.CheckPatternFacets(datatype.Restriction, parseString);
		}

		// Token: 0x0600363B RID: 13883 RVA: 0x0001DA42 File Offset: 0x0001BC42
		internal virtual Exception CheckValueFacets(object value, XmlSchemaDatatype datatype)
		{
			return null;
		}

		// Token: 0x0600363C RID: 13884 RVA: 0x0001DA42 File Offset: 0x0001BC42
		internal virtual Exception CheckValueFacets(decimal value, XmlSchemaDatatype datatype)
		{
			return null;
		}

		// Token: 0x0600363D RID: 13885 RVA: 0x0001DA42 File Offset: 0x0001BC42
		internal virtual Exception CheckValueFacets(long value, XmlSchemaDatatype datatype)
		{
			return null;
		}

		// Token: 0x0600363E RID: 13886 RVA: 0x0001DA42 File Offset: 0x0001BC42
		internal virtual Exception CheckValueFacets(int value, XmlSchemaDatatype datatype)
		{
			return null;
		}

		// Token: 0x0600363F RID: 13887 RVA: 0x0001DA42 File Offset: 0x0001BC42
		internal virtual Exception CheckValueFacets(short value, XmlSchemaDatatype datatype)
		{
			return null;
		}

		// Token: 0x06003640 RID: 13888 RVA: 0x0001DA42 File Offset: 0x0001BC42
		internal virtual Exception CheckValueFacets(byte value, XmlSchemaDatatype datatype)
		{
			return null;
		}

		// Token: 0x06003641 RID: 13889 RVA: 0x0001DA42 File Offset: 0x0001BC42
		internal virtual Exception CheckValueFacets(DateTime value, XmlSchemaDatatype datatype)
		{
			return null;
		}

		// Token: 0x06003642 RID: 13890 RVA: 0x0001DA42 File Offset: 0x0001BC42
		internal virtual Exception CheckValueFacets(double value, XmlSchemaDatatype datatype)
		{
			return null;
		}

		// Token: 0x06003643 RID: 13891 RVA: 0x0001DA42 File Offset: 0x0001BC42
		internal virtual Exception CheckValueFacets(float value, XmlSchemaDatatype datatype)
		{
			return null;
		}

		// Token: 0x06003644 RID: 13892 RVA: 0x0001DA42 File Offset: 0x0001BC42
		internal virtual Exception CheckValueFacets(string value, XmlSchemaDatatype datatype)
		{
			return null;
		}

		// Token: 0x06003645 RID: 13893 RVA: 0x0001DA42 File Offset: 0x0001BC42
		internal virtual Exception CheckValueFacets(byte[] value, XmlSchemaDatatype datatype)
		{
			return null;
		}

		// Token: 0x06003646 RID: 13894 RVA: 0x0001DA42 File Offset: 0x0001BC42
		internal virtual Exception CheckValueFacets(TimeSpan value, XmlSchemaDatatype datatype)
		{
			return null;
		}

		// Token: 0x06003647 RID: 13895 RVA: 0x0001DA42 File Offset: 0x0001BC42
		internal virtual Exception CheckValueFacets(XmlQualifiedName value, XmlSchemaDatatype datatype)
		{
			return null;
		}

		// Token: 0x06003648 RID: 13896 RVA: 0x0012D700 File Offset: 0x0012B900
		internal void CheckWhitespaceFacets(ref string s, XmlSchemaDatatype datatype)
		{
			RestrictionFacets restriction = datatype.Restriction;
			XmlSchemaDatatypeVariety variety = datatype.Variety;
			if (variety != XmlSchemaDatatypeVariety.Atomic)
			{
				if (variety == XmlSchemaDatatypeVariety.List)
				{
					s = s.Trim();
					return;
				}
			}
			else
			{
				if (datatype.BuiltInWhitespaceFacet == XmlSchemaWhiteSpace.Collapse)
				{
					s = XmlComplianceUtil.NonCDataNormalize(s);
					return;
				}
				if (datatype.BuiltInWhitespaceFacet == XmlSchemaWhiteSpace.Replace)
				{
					s = XmlComplianceUtil.CDataNormalize(s);
					return;
				}
				if (restriction != null && (restriction.Flags & RestrictionFlags.WhiteSpace) != (RestrictionFlags)0)
				{
					if (restriction.WhiteSpace == XmlSchemaWhiteSpace.Replace)
					{
						s = XmlComplianceUtil.CDataNormalize(s);
						return;
					}
					if (restriction.WhiteSpace == XmlSchemaWhiteSpace.Collapse)
					{
						s = XmlComplianceUtil.NonCDataNormalize(s);
					}
				}
			}
		}

		// Token: 0x06003649 RID: 13897 RVA: 0x0012D788 File Offset: 0x0012B988
		internal Exception CheckPatternFacets(RestrictionFacets restriction, string value)
		{
			if (restriction != null && (restriction.Flags & RestrictionFlags.Pattern) != (RestrictionFlags)0)
			{
				for (int i = 0; i < restriction.Patterns.Count; i++)
				{
					if (!((Regex)restriction.Patterns[i]).IsMatch(value))
					{
						return new XmlSchemaException("The Pattern constraint failed.", string.Empty);
					}
				}
			}
			return null;
		}

		// Token: 0x0600364A RID: 13898 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		internal virtual bool MatchEnumeration(object value, ArrayList enumeration, XmlSchemaDatatype datatype)
		{
			return false;
		}

		// Token: 0x0600364B RID: 13899 RVA: 0x0012D7E4 File Offset: 0x0012B9E4
		internal virtual RestrictionFacets ConstructRestriction(DatatypeImplementation datatype, XmlSchemaObjectCollection facets, XmlNameTable nameTable)
		{
			RestrictionFacets restrictionFacets = new RestrictionFacets();
			FacetsChecker.FacetsCompiler facetsCompiler = new FacetsChecker.FacetsCompiler(datatype, restrictionFacets);
			for (int i = 0; i < facets.Count; i++)
			{
				XmlSchemaFacet xmlSchemaFacet = (XmlSchemaFacet)facets[i];
				if (xmlSchemaFacet.Value == null)
				{
					throw new XmlSchemaException("The 'value' attribute must be present in facet.", xmlSchemaFacet);
				}
				IXmlNamespaceResolver nsmgr = new SchemaNamespaceManager(xmlSchemaFacet);
				switch (xmlSchemaFacet.FacetType)
				{
				case FacetType.Length:
					facetsCompiler.CompileLengthFacet(xmlSchemaFacet);
					break;
				case FacetType.MinLength:
					facetsCompiler.CompileMinLengthFacet(xmlSchemaFacet);
					break;
				case FacetType.MaxLength:
					facetsCompiler.CompileMaxLengthFacet(xmlSchemaFacet);
					break;
				case FacetType.Pattern:
					facetsCompiler.CompilePatternFacet(xmlSchemaFacet as XmlSchemaPatternFacet);
					break;
				case FacetType.Whitespace:
					facetsCompiler.CompileWhitespaceFacet(xmlSchemaFacet);
					break;
				case FacetType.Enumeration:
					facetsCompiler.CompileEnumerationFacet(xmlSchemaFacet, nsmgr, nameTable);
					break;
				case FacetType.MinExclusive:
					facetsCompiler.CompileMinExclusiveFacet(xmlSchemaFacet);
					break;
				case FacetType.MinInclusive:
					facetsCompiler.CompileMinInclusiveFacet(xmlSchemaFacet);
					break;
				case FacetType.MaxExclusive:
					facetsCompiler.CompileMaxExclusiveFacet(xmlSchemaFacet);
					break;
				case FacetType.MaxInclusive:
					facetsCompiler.CompileMaxInclusiveFacet(xmlSchemaFacet);
					break;
				case FacetType.TotalDigits:
					facetsCompiler.CompileTotalDigitsFacet(xmlSchemaFacet);
					break;
				case FacetType.FractionDigits:
					facetsCompiler.CompileFractionDigitsFacet(xmlSchemaFacet);
					break;
				default:
					throw new XmlSchemaException("This is an unknown facet.", xmlSchemaFacet);
				}
			}
			facetsCompiler.FinishFacetCompile();
			facetsCompiler.CompileFacetCombinations();
			return restrictionFacets;
		}

		// Token: 0x0600364C RID: 13900 RVA: 0x0012D924 File Offset: 0x0012BB24
		internal static decimal Power(int x, int y)
		{
			decimal num = 1m;
			decimal d = x;
			if (y > 28)
			{
				return decimal.MaxValue;
			}
			for (int i = 0; i < y; i++)
			{
				num *= d;
			}
			return num;
		}

		// Token: 0x0600364D RID: 13901 RVA: 0x0000216B File Offset: 0x0000036B
		protected FacetsChecker()
		{
		}

		// Token: 0x02000552 RID: 1362
		private struct FacetsCompiler
		{
			// Token: 0x0600364E RID: 13902 RVA: 0x0012D964 File Offset: 0x0012BB64
			public FacetsCompiler(DatatypeImplementation baseDatatype, RestrictionFacets restriction)
			{
				this.firstPattern = true;
				this.regStr = null;
				this.pattern_facet = null;
				this.datatype = baseDatatype;
				this.derivedRestriction = restriction;
				this.baseFlags = ((this.datatype.Restriction != null) ? this.datatype.Restriction.Flags : ((RestrictionFlags)0));
				this.baseFixedFlags = ((this.datatype.Restriction != null) ? this.datatype.Restriction.FixedFlags : ((RestrictionFlags)0));
				this.validRestrictionFlags = this.datatype.ValidRestrictionFlags;
				this.nonNegativeInt = DatatypeImplementation.GetSimpleTypeFromTypeCode(XmlTypeCode.NonNegativeInteger).Datatype;
				this.builtInEnum = ((!(this.datatype is Datatype_union) && !(this.datatype is Datatype_List)) ? this.datatype.TypeCode : XmlTypeCode.None);
				this.builtInType = ((this.builtInEnum > XmlTypeCode.None) ? DatatypeImplementation.GetSimpleTypeFromTypeCode(this.builtInEnum).Datatype : this.datatype);
			}

			// Token: 0x0600364F RID: 13903 RVA: 0x0012DA58 File Offset: 0x0012BC58
			internal void CompileLengthFacet(XmlSchemaFacet facet)
			{
				this.CheckProhibitedFlag(facet, RestrictionFlags.Length, "The length constraining facet is prohibited for '{0}'.");
				this.CheckDupFlag(facet, RestrictionFlags.Length, "This is a duplicate Length constraining facet.");
				this.derivedRestriction.Length = XmlBaseConverter.DecimalToInt32((decimal)this.ParseFacetValue(this.nonNegativeInt, facet, "The Length constraining facet is invalid - {0}", null, null));
				if ((this.baseFixedFlags & RestrictionFlags.Length) != (RestrictionFlags)0 && !this.datatype.IsEqual(this.datatype.Restriction.Length, this.derivedRestriction.Length))
				{
					throw new XmlSchemaException("Values that are declared as {fixed} in a base type can not be changed in a derived type.", facet);
				}
				if ((this.baseFlags & RestrictionFlags.Length) != (RestrictionFlags)0 && this.datatype.Restriction.Length < this.derivedRestriction.Length)
				{
					throw new XmlSchemaException("It is an error if 'length' is among the members of {facets} of {base type definition} and {value} is greater than the {value} of the parent 'length'.", facet);
				}
				if ((this.baseFlags & RestrictionFlags.MinLength) != (RestrictionFlags)0 && this.datatype.Restriction.MinLength > this.derivedRestriction.Length)
				{
					throw new XmlSchemaException("It is an error for both 'length' and either 'minLength' or 'maxLength' to be members of {facets}, unless they are specified in different derivation steps. In which case the following must be true: the {value} of 'minLength' <= the {value} of 'length' <= the {value} of 'maxLength'.", facet);
				}
				if ((this.baseFlags & RestrictionFlags.MaxLength) != (RestrictionFlags)0 && this.datatype.Restriction.MaxLength < this.derivedRestriction.Length)
				{
					throw new XmlSchemaException("It is an error for both 'length' and either 'minLength' or 'maxLength' to be members of {facets}, unless they are specified in different derivation steps. In which case the following must be true: the {value} of 'minLength' <= the {value} of 'length' <= the {value} of 'maxLength'.", facet);
				}
				this.SetFlag(facet, RestrictionFlags.Length);
			}

			// Token: 0x06003650 RID: 13904 RVA: 0x0012DB94 File Offset: 0x0012BD94
			internal void CompileMinLengthFacet(XmlSchemaFacet facet)
			{
				this.CheckProhibitedFlag(facet, RestrictionFlags.MinLength, "The MinLength constraining facet is prohibited for '{0}'.");
				this.CheckDupFlag(facet, RestrictionFlags.MinLength, "This is a duplicate MinLength constraining facet.");
				this.derivedRestriction.MinLength = XmlBaseConverter.DecimalToInt32((decimal)this.ParseFacetValue(this.nonNegativeInt, facet, "The MinLength constraining facet is invalid - {0}", null, null));
				if ((this.baseFixedFlags & RestrictionFlags.MinLength) != (RestrictionFlags)0 && !this.datatype.IsEqual(this.datatype.Restriction.MinLength, this.derivedRestriction.MinLength))
				{
					throw new XmlSchemaException("Values that are declared as {fixed} in a base type can not be changed in a derived type.", facet);
				}
				if ((this.baseFlags & RestrictionFlags.MinLength) != (RestrictionFlags)0 && this.datatype.Restriction.MinLength > this.derivedRestriction.MinLength)
				{
					throw new XmlSchemaException("It is an error if 'minLength' is among the members of {facets} of {base type definition} and {value} is less than the {value} of the parent 'minLength'.", facet);
				}
				if ((this.baseFlags & RestrictionFlags.Length) != (RestrictionFlags)0 && this.datatype.Restriction.Length < this.derivedRestriction.MinLength)
				{
					throw new XmlSchemaException("It is an error for both 'length' and either 'minLength' or 'maxLength' to be members of {facets}, unless they are specified in different derivation steps. In which case the following must be true: the {value} of 'minLength' <= the {value} of 'length' <= the {value} of 'maxLength'.", facet);
				}
				this.SetFlag(facet, RestrictionFlags.MinLength);
			}

			// Token: 0x06003651 RID: 13905 RVA: 0x0012DC9C File Offset: 0x0012BE9C
			internal void CompileMaxLengthFacet(XmlSchemaFacet facet)
			{
				this.CheckProhibitedFlag(facet, RestrictionFlags.MaxLength, "The MaxLength constraining facet is prohibited for '{0}'.");
				this.CheckDupFlag(facet, RestrictionFlags.MaxLength, "This is a duplicate MaxLength constraining facet.");
				this.derivedRestriction.MaxLength = XmlBaseConverter.DecimalToInt32((decimal)this.ParseFacetValue(this.nonNegativeInt, facet, "The MaxLength constraining facet is invalid - {0}", null, null));
				if ((this.baseFixedFlags & RestrictionFlags.MaxLength) != (RestrictionFlags)0 && !this.datatype.IsEqual(this.datatype.Restriction.MaxLength, this.derivedRestriction.MaxLength))
				{
					throw new XmlSchemaException("Values that are declared as {fixed} in a base type can not be changed in a derived type.", facet);
				}
				if ((this.baseFlags & RestrictionFlags.MaxLength) != (RestrictionFlags)0 && this.datatype.Restriction.MaxLength < this.derivedRestriction.MaxLength)
				{
					throw new XmlSchemaException("It is an error if 'maxLength' is among the members of {facets} of {base type definition} and {value} is greater than the {value} of the parent 'maxLength'.", facet);
				}
				if ((this.baseFlags & RestrictionFlags.Length) != (RestrictionFlags)0 && this.datatype.Restriction.Length > this.derivedRestriction.MaxLength)
				{
					throw new XmlSchemaException("It is an error for both 'length' and either 'minLength' or 'maxLength' to be members of {facets}, unless they are specified in different derivation steps. In which case the following must be true: the {value} of 'minLength' <= the {value} of 'length' <= the {value} of 'maxLength'.", facet);
				}
				this.SetFlag(facet, RestrictionFlags.MaxLength);
			}

			// Token: 0x06003652 RID: 13906 RVA: 0x0012DDA4 File Offset: 0x0012BFA4
			internal void CompilePatternFacet(XmlSchemaPatternFacet facet)
			{
				this.CheckProhibitedFlag(facet, RestrictionFlags.Pattern, "The Pattern constraining facet is prohibited for '{0}'.");
				if (this.firstPattern)
				{
					this.regStr = new StringBuilder();
					this.regStr.Append("(");
					this.regStr.Append(facet.Value);
					this.pattern_facet = facet;
					this.firstPattern = false;
				}
				else
				{
					this.regStr.Append(")|(");
					this.regStr.Append(facet.Value);
				}
				this.SetFlag(facet, RestrictionFlags.Pattern);
			}

			// Token: 0x06003653 RID: 13907 RVA: 0x0012DE30 File Offset: 0x0012C030
			internal void CompileEnumerationFacet(XmlSchemaFacet facet, IXmlNamespaceResolver nsmgr, XmlNameTable nameTable)
			{
				this.CheckProhibitedFlag(facet, RestrictionFlags.Enumeration, "The Enumeration constraining facet is prohibited for '{0}'.");
				if (this.derivedRestriction.Enumeration == null)
				{
					this.derivedRestriction.Enumeration = new ArrayList();
				}
				this.derivedRestriction.Enumeration.Add(this.ParseFacetValue(this.datatype, facet, "The Enumeration constraining facet is invalid - {0}", nsmgr, nameTable));
				this.SetFlag(facet, RestrictionFlags.Enumeration);
			}

			// Token: 0x06003654 RID: 13908 RVA: 0x0012DE98 File Offset: 0x0012C098
			internal void CompileWhitespaceFacet(XmlSchemaFacet facet)
			{
				this.CheckProhibitedFlag(facet, RestrictionFlags.WhiteSpace, "The WhiteSpace constraining facet is prohibited for '{0}'.");
				this.CheckDupFlag(facet, RestrictionFlags.WhiteSpace, "This is a duplicate WhiteSpace constraining facet.");
				if (facet.Value == "preserve")
				{
					this.derivedRestriction.WhiteSpace = XmlSchemaWhiteSpace.Preserve;
				}
				else if (facet.Value == "replace")
				{
					this.derivedRestriction.WhiteSpace = XmlSchemaWhiteSpace.Replace;
				}
				else
				{
					if (!(facet.Value == "collapse"))
					{
						throw new XmlSchemaException("The white space character, '{0}', is invalid.", facet.Value, facet);
					}
					this.derivedRestriction.WhiteSpace = XmlSchemaWhiteSpace.Collapse;
				}
				if ((this.baseFixedFlags & RestrictionFlags.WhiteSpace) != (RestrictionFlags)0 && !this.datatype.IsEqual(this.datatype.Restriction.WhiteSpace, this.derivedRestriction.WhiteSpace))
				{
					throw new XmlSchemaException("Values that are declared as {fixed} in a base type can not be changed in a derived type.", facet);
				}
				XmlSchemaWhiteSpace xmlSchemaWhiteSpace;
				if ((this.baseFlags & RestrictionFlags.WhiteSpace) != (RestrictionFlags)0)
				{
					xmlSchemaWhiteSpace = this.datatype.Restriction.WhiteSpace;
				}
				else
				{
					xmlSchemaWhiteSpace = this.datatype.BuiltInWhitespaceFacet;
				}
				if (xmlSchemaWhiteSpace == XmlSchemaWhiteSpace.Collapse && (this.derivedRestriction.WhiteSpace == XmlSchemaWhiteSpace.Replace || this.derivedRestriction.WhiteSpace == XmlSchemaWhiteSpace.Preserve))
				{
					throw new XmlSchemaException("It is an error if 'whiteSpace' is among the members of {facets} of {base type definition}, {value} is 'replace' or 'preserve', and the {value} of the parent 'whiteSpace' is 'collapse'.", facet);
				}
				if (xmlSchemaWhiteSpace == XmlSchemaWhiteSpace.Replace && this.derivedRestriction.WhiteSpace == XmlSchemaWhiteSpace.Preserve)
				{
					throw new XmlSchemaException("It is an error if 'whiteSpace' is among the members of {facets} of {base type definition}, {value} is 'preserve', and the {value} of the parent 'whiteSpace' is 'replace'.", facet);
				}
				this.SetFlag(facet, RestrictionFlags.WhiteSpace);
			}

			// Token: 0x06003655 RID: 13909 RVA: 0x0012DFF8 File Offset: 0x0012C1F8
			internal void CompileMaxInclusiveFacet(XmlSchemaFacet facet)
			{
				this.CheckProhibitedFlag(facet, RestrictionFlags.MaxInclusive, "The MaxInclusive constraining facet is prohibited for '{0}'.");
				this.CheckDupFlag(facet, RestrictionFlags.MaxInclusive, "This is a duplicate MaxInclusive constraining facet.");
				this.derivedRestriction.MaxInclusive = this.ParseFacetValue(this.builtInType, facet, "The MaxInclusive constraining facet is invalid - {0}", null, null);
				if ((this.baseFixedFlags & RestrictionFlags.MaxInclusive) != (RestrictionFlags)0 && !this.datatype.IsEqual(this.datatype.Restriction.MaxInclusive, this.derivedRestriction.MaxInclusive))
				{
					throw new XmlSchemaException("Values that are declared as {fixed} in a base type can not be changed in a derived type.", facet);
				}
				this.CheckValue(this.derivedRestriction.MaxInclusive, facet);
				this.SetFlag(facet, RestrictionFlags.MaxInclusive);
			}

			// Token: 0x06003656 RID: 13910 RVA: 0x0012E09C File Offset: 0x0012C29C
			internal void CompileMaxExclusiveFacet(XmlSchemaFacet facet)
			{
				this.CheckProhibitedFlag(facet, RestrictionFlags.MaxExclusive, "The MaxExclusive constraining facet is prohibited for '{0}'.");
				this.CheckDupFlag(facet, RestrictionFlags.MaxExclusive, "This is a duplicate MaxExclusive constraining facet.");
				this.derivedRestriction.MaxExclusive = this.ParseFacetValue(this.builtInType, facet, "The MaxExclusive constraining facet is invalid - {0}", null, null);
				if ((this.baseFixedFlags & RestrictionFlags.MaxExclusive) != (RestrictionFlags)0 && !this.datatype.IsEqual(this.datatype.Restriction.MaxExclusive, this.derivedRestriction.MaxExclusive))
				{
					throw new XmlSchemaException("Values that are declared as {fixed} in a base type can not be changed in a derived type.", facet);
				}
				this.CheckValue(this.derivedRestriction.MaxExclusive, facet);
				this.SetFlag(facet, RestrictionFlags.MaxExclusive);
			}

			// Token: 0x06003657 RID: 13911 RVA: 0x0012E14C File Offset: 0x0012C34C
			internal void CompileMinInclusiveFacet(XmlSchemaFacet facet)
			{
				this.CheckProhibitedFlag(facet, RestrictionFlags.MinInclusive, "The MinInclusive constraining facet is prohibited for '{0}'.");
				this.CheckDupFlag(facet, RestrictionFlags.MinInclusive, "This is a duplicate MinInclusive constraining facet.");
				this.derivedRestriction.MinInclusive = this.ParseFacetValue(this.builtInType, facet, "The MinInclusive constraining facet is invalid - {0}", null, null);
				if ((this.baseFixedFlags & RestrictionFlags.MinInclusive) != (RestrictionFlags)0 && !this.datatype.IsEqual(this.datatype.Restriction.MinInclusive, this.derivedRestriction.MinInclusive))
				{
					throw new XmlSchemaException("Values that are declared as {fixed} in a base type can not be changed in a derived type.", facet);
				}
				this.CheckValue(this.derivedRestriction.MinInclusive, facet);
				this.SetFlag(facet, RestrictionFlags.MinInclusive);
			}

			// Token: 0x06003658 RID: 13912 RVA: 0x0012E1FC File Offset: 0x0012C3FC
			internal void CompileMinExclusiveFacet(XmlSchemaFacet facet)
			{
				this.CheckProhibitedFlag(facet, RestrictionFlags.MinExclusive, "The MinExclusive constraining facet is prohibited for '{0}'.");
				this.CheckDupFlag(facet, RestrictionFlags.MinExclusive, "This is a duplicate MinExclusive constraining facet.");
				this.derivedRestriction.MinExclusive = this.ParseFacetValue(this.builtInType, facet, "The MinExclusive constraining facet is invalid - {0}", null, null);
				if ((this.baseFixedFlags & RestrictionFlags.MinExclusive) != (RestrictionFlags)0 && !this.datatype.IsEqual(this.datatype.Restriction.MinExclusive, this.derivedRestriction.MinExclusive))
				{
					throw new XmlSchemaException("Values that are declared as {fixed} in a base type can not be changed in a derived type.", facet);
				}
				this.CheckValue(this.derivedRestriction.MinExclusive, facet);
				this.SetFlag(facet, RestrictionFlags.MinExclusive);
			}

			// Token: 0x06003659 RID: 13913 RVA: 0x0012E2AC File Offset: 0x0012C4AC
			internal void CompileTotalDigitsFacet(XmlSchemaFacet facet)
			{
				this.CheckProhibitedFlag(facet, RestrictionFlags.TotalDigits, "The TotalDigits constraining facet is prohibited for '{0}'.");
				this.CheckDupFlag(facet, RestrictionFlags.TotalDigits, "This is a duplicate TotalDigits constraining facet.");
				XmlSchemaDatatype xmlSchemaDatatype = DatatypeImplementation.GetSimpleTypeFromTypeCode(XmlTypeCode.PositiveInteger).Datatype;
				this.derivedRestriction.TotalDigits = XmlBaseConverter.DecimalToInt32((decimal)this.ParseFacetValue(xmlSchemaDatatype, facet, "The TotalDigits constraining facet is invalid - {0}", null, null));
				if ((this.baseFixedFlags & RestrictionFlags.TotalDigits) != (RestrictionFlags)0 && !this.datatype.IsEqual(this.datatype.Restriction.TotalDigits, this.derivedRestriction.TotalDigits))
				{
					throw new XmlSchemaException("Values that are declared as {fixed} in a base type can not be changed in a derived type.", facet);
				}
				if ((this.baseFlags & RestrictionFlags.TotalDigits) != (RestrictionFlags)0 && this.derivedRestriction.TotalDigits > this.datatype.Restriction.TotalDigits)
				{
					throw new XmlSchemaException("It is an error if the derived 'totalDigits' facet value is greater than the parent 'totalDigits' facet value.", string.Empty);
				}
				this.SetFlag(facet, RestrictionFlags.TotalDigits);
			}

			// Token: 0x0600365A RID: 13914 RVA: 0x0012E3A0 File Offset: 0x0012C5A0
			internal void CompileFractionDigitsFacet(XmlSchemaFacet facet)
			{
				this.CheckProhibitedFlag(facet, RestrictionFlags.FractionDigits, "The FractionDigits constraining facet is prohibited for '{0}'.");
				this.CheckDupFlag(facet, RestrictionFlags.FractionDigits, "This is a duplicate FractionDigits constraining facet.");
				this.derivedRestriction.FractionDigits = XmlBaseConverter.DecimalToInt32((decimal)this.ParseFacetValue(this.nonNegativeInt, facet, "The FractionDigits constraining facet is invalid - {0}", null, null));
				if (this.derivedRestriction.FractionDigits != 0 && this.datatype.TypeCode != XmlTypeCode.Decimal)
				{
					throw new XmlSchemaException("The FractionDigits constraining facet is invalid - {0}", Res.GetString("FractionDigits should be equal to 0 on types other then decimal."), facet);
				}
				if ((this.baseFlags & RestrictionFlags.FractionDigits) != (RestrictionFlags)0 && this.derivedRestriction.FractionDigits > this.datatype.Restriction.FractionDigits)
				{
					throw new XmlSchemaException("It is an error if the derived 'totalDigits' facet value is greater than the parent 'totalDigits' facet value.", string.Empty);
				}
				this.SetFlag(facet, RestrictionFlags.FractionDigits);
			}

			// Token: 0x0600365B RID: 13915 RVA: 0x0012E474 File Offset: 0x0012C674
			internal void FinishFacetCompile()
			{
				if (!this.firstPattern)
				{
					if (this.derivedRestriction.Patterns == null)
					{
						this.derivedRestriction.Patterns = new ArrayList();
					}
					try
					{
						this.regStr.Append(")");
						if (this.regStr.ToString().IndexOf('|') != -1)
						{
							this.regStr.Insert(0, "(");
							this.regStr.Append(")");
						}
						this.derivedRestriction.Patterns.Add(new Regex(FacetsChecker.FacetsCompiler.Preprocess(this.regStr.ToString()), RegexOptions.None));
					}
					catch (Exception ex)
					{
						throw new XmlSchemaException("The Pattern constraining facet is invalid - {0}", new string[]
						{
							ex.Message
						}, ex, this.pattern_facet.SourceUri, this.pattern_facet.LineNumber, this.pattern_facet.LinePosition, this.pattern_facet);
					}
				}
			}

			// Token: 0x0600365C RID: 13916 RVA: 0x0012E570 File Offset: 0x0012C770
			private void CheckValue(object value, XmlSchemaFacet facet)
			{
				RestrictionFacets restriction = this.datatype.Restriction;
				switch (facet.FacetType)
				{
				case FacetType.MinExclusive:
					if ((this.baseFlags & RestrictionFlags.MinExclusive) != (RestrictionFlags)0 && this.datatype.Compare(value, restriction.MinExclusive) < 0)
					{
						throw new XmlSchemaException("It is an error if the derived 'minExclusive' facet value is less than the parent 'minExclusive' facet value.", string.Empty);
					}
					if ((this.baseFlags & RestrictionFlags.MinInclusive) != (RestrictionFlags)0 && this.datatype.Compare(value, restriction.MinInclusive) < 0)
					{
						throw new XmlSchemaException("It is an error if the derived 'minExclusive' facet value is less than or equal to the parent 'minInclusive' facet value.", string.Empty);
					}
					if ((this.baseFlags & RestrictionFlags.MaxExclusive) != (RestrictionFlags)0 && this.datatype.Compare(value, restriction.MaxExclusive) >= 0)
					{
						throw new XmlSchemaException("It is an error if the derived 'minExclusive' facet value is greater than or equal to the parent 'maxExclusive' facet value.", string.Empty);
					}
					break;
				case FacetType.MinInclusive:
					if ((this.baseFlags & RestrictionFlags.MinInclusive) != (RestrictionFlags)0 && this.datatype.Compare(value, restriction.MinInclusive) < 0)
					{
						throw new XmlSchemaException("It is an error if the derived 'minInclusive' facet value is less than the parent 'minInclusive' facet value.", string.Empty);
					}
					if ((this.baseFlags & RestrictionFlags.MinExclusive) != (RestrictionFlags)0 && this.datatype.Compare(value, restriction.MinExclusive) < 0)
					{
						throw new XmlSchemaException("It is an error if the derived 'minInclusive' facet value is less than or equal to the parent 'minExclusive' facet value.", string.Empty);
					}
					if ((this.baseFlags & RestrictionFlags.MaxExclusive) != (RestrictionFlags)0 && this.datatype.Compare(value, restriction.MaxExclusive) >= 0)
					{
						throw new XmlSchemaException("It is an error if the derived 'minInclusive' facet value is greater than or equal to the parent 'maxExclusive' facet value.", string.Empty);
					}
					break;
				case FacetType.MaxExclusive:
					if ((this.baseFlags & RestrictionFlags.MaxExclusive) != (RestrictionFlags)0 && this.datatype.Compare(value, restriction.MaxExclusive) > 0)
					{
						throw new XmlSchemaException("It is an error if the derived 'maxExclusive' facet value is greater than the parent 'maxExclusive' facet value.", string.Empty);
					}
					if ((this.baseFlags & RestrictionFlags.MaxInclusive) != (RestrictionFlags)0 && this.datatype.Compare(value, restriction.MaxInclusive) > 0)
					{
						throw new XmlSchemaException("It is an error if the derived 'maxExclusive' facet value is greater than or equal to the parent 'maxInclusive' facet value.", string.Empty);
					}
					break;
				case FacetType.MaxInclusive:
					if ((this.baseFlags & RestrictionFlags.MaxInclusive) != (RestrictionFlags)0 && this.datatype.Compare(value, restriction.MaxInclusive) > 0)
					{
						throw new XmlSchemaException("It is an error if the derived 'maxInclusive' facet value is greater than the parent 'maxInclusive' facet value.", string.Empty);
					}
					if ((this.baseFlags & RestrictionFlags.MaxExclusive) != (RestrictionFlags)0 && this.datatype.Compare(value, restriction.MaxExclusive) >= 0)
					{
						throw new XmlSchemaException("It is an error if the derived 'maxInclusive' facet value is greater than or equal to the parent 'maxExclusive' facet value.", string.Empty);
					}
					break;
				default:
					return;
				}
			}

			// Token: 0x0600365D RID: 13917 RVA: 0x0012E7B4 File Offset: 0x0012C9B4
			internal void CompileFacetCombinations()
			{
				RestrictionFacets restriction = this.datatype.Restriction;
				if ((this.derivedRestriction.Flags & RestrictionFlags.MaxInclusive) != (RestrictionFlags)0 && (this.derivedRestriction.Flags & RestrictionFlags.MaxExclusive) != (RestrictionFlags)0)
				{
					throw new XmlSchemaException("'maxInclusive' and 'maxExclusive' cannot both be specified for the same data type.", string.Empty);
				}
				if ((this.derivedRestriction.Flags & RestrictionFlags.MinInclusive) != (RestrictionFlags)0 && (this.derivedRestriction.Flags & RestrictionFlags.MinExclusive) != (RestrictionFlags)0)
				{
					throw new XmlSchemaException("'minInclusive' and 'minExclusive' cannot both be specified for the same data type.", string.Empty);
				}
				if ((this.derivedRestriction.Flags & RestrictionFlags.Length) != (RestrictionFlags)0 && (this.derivedRestriction.Flags & (RestrictionFlags.MinLength | RestrictionFlags.MaxLength)) != (RestrictionFlags)0)
				{
					throw new XmlSchemaException("It is an error for both length and minLength or maxLength to be present.", string.Empty);
				}
				this.CopyFacetsFromBaseType();
				if ((this.derivedRestriction.Flags & RestrictionFlags.MinLength) != (RestrictionFlags)0 && (this.derivedRestriction.Flags & RestrictionFlags.MaxLength) != (RestrictionFlags)0 && this.derivedRestriction.MinLength > this.derivedRestriction.MaxLength)
				{
					throw new XmlSchemaException("MinLength is greater than MaxLength.", string.Empty);
				}
				if ((this.derivedRestriction.Flags & RestrictionFlags.MinInclusive) != (RestrictionFlags)0 && (this.derivedRestriction.Flags & RestrictionFlags.MaxInclusive) != (RestrictionFlags)0 && this.datatype.Compare(this.derivedRestriction.MinInclusive, this.derivedRestriction.MaxInclusive) > 0)
				{
					throw new XmlSchemaException("The value specified for 'minInclusive' cannot be greater than the value specified for 'maxInclusive' for the same data type.", string.Empty);
				}
				if ((this.derivedRestriction.Flags & RestrictionFlags.MinInclusive) != (RestrictionFlags)0 && (this.derivedRestriction.Flags & RestrictionFlags.MaxExclusive) != (RestrictionFlags)0 && this.datatype.Compare(this.derivedRestriction.MinInclusive, this.derivedRestriction.MaxExclusive) > 0)
				{
					throw new XmlSchemaException("The value specified for 'minInclusive' cannot be greater than the value specified for 'maxExclusive' for the same data type.", string.Empty);
				}
				if ((this.derivedRestriction.Flags & RestrictionFlags.MinExclusive) != (RestrictionFlags)0 && (this.derivedRestriction.Flags & RestrictionFlags.MaxExclusive) != (RestrictionFlags)0 && this.datatype.Compare(this.derivedRestriction.MinExclusive, this.derivedRestriction.MaxExclusive) > 0)
				{
					throw new XmlSchemaException("The value specified for 'minExclusive' cannot be greater than the value specified for 'maxExclusive' for the same data type.", string.Empty);
				}
				if ((this.derivedRestriction.Flags & RestrictionFlags.MinExclusive) != (RestrictionFlags)0 && (this.derivedRestriction.Flags & RestrictionFlags.MaxInclusive) != (RestrictionFlags)0 && this.datatype.Compare(this.derivedRestriction.MinExclusive, this.derivedRestriction.MaxInclusive) > 0)
				{
					throw new XmlSchemaException("The value specified for 'minExclusive' cannot be greater than the value specified for 'maxInclusive' for the same data type.", string.Empty);
				}
				if ((this.derivedRestriction.Flags & (RestrictionFlags.TotalDigits | RestrictionFlags.FractionDigits)) == (RestrictionFlags.TotalDigits | RestrictionFlags.FractionDigits) && this.derivedRestriction.FractionDigits > this.derivedRestriction.TotalDigits)
				{
					throw new XmlSchemaException("FractionDigits is greater than TotalDigits.", string.Empty);
				}
			}

			// Token: 0x0600365E RID: 13918 RVA: 0x0012EA54 File Offset: 0x0012CC54
			private void CopyFacetsFromBaseType()
			{
				RestrictionFacets restriction = this.datatype.Restriction;
				if ((this.derivedRestriction.Flags & RestrictionFlags.Length) == (RestrictionFlags)0 && (this.baseFlags & RestrictionFlags.Length) != (RestrictionFlags)0)
				{
					this.derivedRestriction.Length = restriction.Length;
					this.SetFlag(RestrictionFlags.Length);
				}
				if ((this.derivedRestriction.Flags & RestrictionFlags.MinLength) == (RestrictionFlags)0 && (this.baseFlags & RestrictionFlags.MinLength) != (RestrictionFlags)0)
				{
					this.derivedRestriction.MinLength = restriction.MinLength;
					this.SetFlag(RestrictionFlags.MinLength);
				}
				if ((this.derivedRestriction.Flags & RestrictionFlags.MaxLength) == (RestrictionFlags)0 && (this.baseFlags & RestrictionFlags.MaxLength) != (RestrictionFlags)0)
				{
					this.derivedRestriction.MaxLength = restriction.MaxLength;
					this.SetFlag(RestrictionFlags.MaxLength);
				}
				if ((this.baseFlags & RestrictionFlags.Pattern) != (RestrictionFlags)0)
				{
					if (this.derivedRestriction.Patterns == null)
					{
						this.derivedRestriction.Patterns = restriction.Patterns;
					}
					else
					{
						this.derivedRestriction.Patterns.AddRange(restriction.Patterns);
					}
					this.SetFlag(RestrictionFlags.Pattern);
				}
				if ((this.baseFlags & RestrictionFlags.Enumeration) != (RestrictionFlags)0)
				{
					if (this.derivedRestriction.Enumeration == null)
					{
						this.derivedRestriction.Enumeration = restriction.Enumeration;
					}
					this.SetFlag(RestrictionFlags.Enumeration);
				}
				if ((this.derivedRestriction.Flags & RestrictionFlags.WhiteSpace) == (RestrictionFlags)0 && (this.baseFlags & RestrictionFlags.WhiteSpace) != (RestrictionFlags)0)
				{
					this.derivedRestriction.WhiteSpace = restriction.WhiteSpace;
					this.SetFlag(RestrictionFlags.WhiteSpace);
				}
				if ((this.derivedRestriction.Flags & RestrictionFlags.MaxInclusive) == (RestrictionFlags)0 && (this.baseFlags & RestrictionFlags.MaxInclusive) != (RestrictionFlags)0)
				{
					this.derivedRestriction.MaxInclusive = restriction.MaxInclusive;
					this.SetFlag(RestrictionFlags.MaxInclusive);
				}
				if ((this.derivedRestriction.Flags & RestrictionFlags.MaxExclusive) == (RestrictionFlags)0 && (this.baseFlags & RestrictionFlags.MaxExclusive) != (RestrictionFlags)0)
				{
					this.derivedRestriction.MaxExclusive = restriction.MaxExclusive;
					this.SetFlag(RestrictionFlags.MaxExclusive);
				}
				if ((this.derivedRestriction.Flags & RestrictionFlags.MinInclusive) == (RestrictionFlags)0 && (this.baseFlags & RestrictionFlags.MinInclusive) != (RestrictionFlags)0)
				{
					this.derivedRestriction.MinInclusive = restriction.MinInclusive;
					this.SetFlag(RestrictionFlags.MinInclusive);
				}
				if ((this.derivedRestriction.Flags & RestrictionFlags.MinExclusive) == (RestrictionFlags)0 && (this.baseFlags & RestrictionFlags.MinExclusive) != (RestrictionFlags)0)
				{
					this.derivedRestriction.MinExclusive = restriction.MinExclusive;
					this.SetFlag(RestrictionFlags.MinExclusive);
				}
				if ((this.derivedRestriction.Flags & RestrictionFlags.TotalDigits) == (RestrictionFlags)0 && (this.baseFlags & RestrictionFlags.TotalDigits) != (RestrictionFlags)0)
				{
					this.derivedRestriction.TotalDigits = restriction.TotalDigits;
					this.SetFlag(RestrictionFlags.TotalDigits);
				}
				if ((this.derivedRestriction.Flags & RestrictionFlags.FractionDigits) == (RestrictionFlags)0 && (this.baseFlags & RestrictionFlags.FractionDigits) != (RestrictionFlags)0)
				{
					this.derivedRestriction.FractionDigits = restriction.FractionDigits;
					this.SetFlag(RestrictionFlags.FractionDigits);
				}
			}

			// Token: 0x0600365F RID: 13919 RVA: 0x0012ED14 File Offset: 0x0012CF14
			private object ParseFacetValue(XmlSchemaDatatype datatype, XmlSchemaFacet facet, string code, IXmlNamespaceResolver nsmgr, XmlNameTable nameTable)
			{
				object result;
				Exception ex = datatype.TryParseValue(facet.Value, nameTable, nsmgr, out result);
				if (ex == null)
				{
					return result;
				}
				throw new XmlSchemaException(code, new string[]
				{
					ex.Message
				}, ex, facet.SourceUri, facet.LineNumber, facet.LinePosition, facet);
			}

			// Token: 0x06003660 RID: 13920 RVA: 0x0012ED64 File Offset: 0x0012CF64
			private static string Preprocess(string pattern)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("^");
				char[] array = pattern.ToCharArray();
				int length = pattern.Length;
				int num = 0;
				for (int i = 0; i < length - 2; i++)
				{
					if (array[i] == '\\')
					{
						if (array[i + 1] == '\\')
						{
							i++;
						}
						else
						{
							char c = array[i + 1];
							for (int j = 0; j < FacetsChecker.FacetsCompiler.c_map.Length; j++)
							{
								if (FacetsChecker.FacetsCompiler.c_map[j].match == c)
								{
									if (num < i)
									{
										stringBuilder.Append(array, num, i - num);
									}
									stringBuilder.Append(FacetsChecker.FacetsCompiler.c_map[j].replacement);
									i++;
									num = i + 1;
									break;
								}
							}
						}
					}
				}
				if (num < length)
				{
					stringBuilder.Append(array, num, length - num);
				}
				stringBuilder.Append("$");
				return stringBuilder.ToString();
			}

			// Token: 0x06003661 RID: 13921 RVA: 0x0012EE51 File Offset: 0x0012D051
			private void CheckProhibitedFlag(XmlSchemaFacet facet, RestrictionFlags flag, string errorCode)
			{
				if ((this.validRestrictionFlags & flag) == (RestrictionFlags)0)
				{
					throw new XmlSchemaException(errorCode, this.datatype.TypeCodeString, facet);
				}
			}

			// Token: 0x06003662 RID: 13922 RVA: 0x0012EE70 File Offset: 0x0012D070
			private void CheckDupFlag(XmlSchemaFacet facet, RestrictionFlags flag, string errorCode)
			{
				if ((this.derivedRestriction.Flags & flag) != (RestrictionFlags)0)
				{
					throw new XmlSchemaException(errorCode, facet);
				}
			}

			// Token: 0x06003663 RID: 13923 RVA: 0x0012EE89 File Offset: 0x0012D089
			private void SetFlag(XmlSchemaFacet facet, RestrictionFlags flag)
			{
				this.derivedRestriction.Flags |= flag;
				if (facet.IsFixed)
				{
					this.derivedRestriction.FixedFlags |= flag;
				}
			}

			// Token: 0x06003664 RID: 13924 RVA: 0x0012EEB9 File Offset: 0x0012D0B9
			private void SetFlag(RestrictionFlags flag)
			{
				this.derivedRestriction.Flags |= flag;
				if ((this.baseFixedFlags & flag) != (RestrictionFlags)0)
				{
					this.derivedRestriction.FixedFlags |= flag;
				}
			}

			// Token: 0x06003665 RID: 13925 RVA: 0x0012EEEC File Offset: 0x0012D0EC
			// Note: this type is marked as 'beforefieldinit'.
			static FacetsCompiler()
			{
			}

			// Token: 0x040027D0 RID: 10192
			private DatatypeImplementation datatype;

			// Token: 0x040027D1 RID: 10193
			private RestrictionFacets derivedRestriction;

			// Token: 0x040027D2 RID: 10194
			private RestrictionFlags baseFlags;

			// Token: 0x040027D3 RID: 10195
			private RestrictionFlags baseFixedFlags;

			// Token: 0x040027D4 RID: 10196
			private RestrictionFlags validRestrictionFlags;

			// Token: 0x040027D5 RID: 10197
			private XmlSchemaDatatype nonNegativeInt;

			// Token: 0x040027D6 RID: 10198
			private XmlSchemaDatatype builtInType;

			// Token: 0x040027D7 RID: 10199
			private XmlTypeCode builtInEnum;

			// Token: 0x040027D8 RID: 10200
			private bool firstPattern;

			// Token: 0x040027D9 RID: 10201
			private StringBuilder regStr;

			// Token: 0x040027DA RID: 10202
			private XmlSchemaPatternFacet pattern_facet;

			// Token: 0x040027DB RID: 10203
			private static readonly FacetsChecker.FacetsCompiler.Map[] c_map = new FacetsChecker.FacetsCompiler.Map[]
			{
				new FacetsChecker.FacetsCompiler.Map('c', "\\p{_xmlC}"),
				new FacetsChecker.FacetsCompiler.Map('C', "\\P{_xmlC}"),
				new FacetsChecker.FacetsCompiler.Map('d', "\\p{_xmlD}"),
				new FacetsChecker.FacetsCompiler.Map('D', "\\P{_xmlD}"),
				new FacetsChecker.FacetsCompiler.Map('i', "\\p{_xmlI}"),
				new FacetsChecker.FacetsCompiler.Map('I', "\\P{_xmlI}"),
				new FacetsChecker.FacetsCompiler.Map('w', "\\p{_xmlW}"),
				new FacetsChecker.FacetsCompiler.Map('W', "\\P{_xmlW}")
			};

			// Token: 0x02000553 RID: 1363
			private struct Map
			{
				// Token: 0x06003666 RID: 13926 RVA: 0x0012EF9C File Offset: 0x0012D19C
				internal Map(char m, string r)
				{
					this.match = m;
					this.replacement = r;
				}

				// Token: 0x040027DC RID: 10204
				internal char match;

				// Token: 0x040027DD RID: 10205
				internal string replacement;
			}
		}
	}
}
