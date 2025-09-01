using System;

namespace System.Xml.Schema
{
	// Token: 0x0200053A RID: 1338
	internal class Datatype_NOTATION : Datatype_anySimpleType
	{
		// Token: 0x0600359E RID: 13726 RVA: 0x0012B60C File Offset: 0x0012980C
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return XmlMiscConverter.Create(schemaType);
		}

		// Token: 0x170009DF RID: 2527
		// (get) Token: 0x0600359F RID: 13727 RVA: 0x0012BB82 File Offset: 0x00129D82
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return DatatypeImplementation.qnameFacetsChecker;
			}
		}

		// Token: 0x170009E0 RID: 2528
		// (get) Token: 0x060035A0 RID: 13728 RVA: 0x0012BCD1 File Offset: 0x00129ED1
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Notation;
			}
		}

		// Token: 0x170009E1 RID: 2529
		// (get) Token: 0x060035A1 RID: 13729 RVA: 0x000678D5 File Offset: 0x00065AD5
		public override XmlTokenizedType TokenizedType
		{
			get
			{
				return XmlTokenizedType.NOTATION;
			}
		}

		// Token: 0x170009E2 RID: 2530
		// (get) Token: 0x060035A2 RID: 13730 RVA: 0x0012ACD1 File Offset: 0x00128ED1
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return RestrictionFlags.Length | RestrictionFlags.MinLength | RestrictionFlags.MaxLength | RestrictionFlags.Pattern | RestrictionFlags.Enumeration | RestrictionFlags.WhiteSpace;
			}
		}

		// Token: 0x170009E3 RID: 2531
		// (get) Token: 0x060035A3 RID: 13731 RVA: 0x0012BCD5 File Offset: 0x00129ED5
		public override Type ValueType
		{
			get
			{
				return Datatype_NOTATION.atomicValueType;
			}
		}

		// Token: 0x170009E4 RID: 2532
		// (get) Token: 0x060035A4 RID: 13732 RVA: 0x0012BCDC File Offset: 0x00129EDC
		internal override Type ListValueType
		{
			get
			{
				return Datatype_NOTATION.listValueType;
			}
		}

		// Token: 0x170009E5 RID: 2533
		// (get) Token: 0x060035A5 RID: 13733 RVA: 0x00066748 File Offset: 0x00064948
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Collapse;
			}
		}

		// Token: 0x060035A6 RID: 13734 RVA: 0x0012BCE4 File Offset: 0x00129EE4
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			if (s == null || s.Length == 0)
			{
				return new XmlSchemaException("The attribute value cannot be empty.", string.Empty);
			}
			Exception ex = DatatypeImplementation.qnameFacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				XmlQualifiedName xmlQualifiedName = null;
				try
				{
					string text;
					xmlQualifiedName = XmlQualifiedName.Parse(s, nsmgr, out text);
				}
				catch (ArgumentException ex)
				{
					return ex;
				}
				catch (XmlException ex)
				{
					return ex;
				}
				ex = DatatypeImplementation.qnameFacetsChecker.CheckValueFacets(xmlQualifiedName, this);
				if (ex == null)
				{
					typedValue = xmlQualifiedName;
					return null;
				}
			}
			return ex;
		}

		// Token: 0x060035A7 RID: 13735 RVA: 0x0012BD68 File Offset: 0x00129F68
		internal override void VerifySchemaValid(XmlSchemaObjectTable notations, XmlSchemaObject caller)
		{
			for (Datatype_NOTATION datatype_NOTATION = this; datatype_NOTATION != null; datatype_NOTATION = (Datatype_NOTATION)datatype_NOTATION.Base)
			{
				if (datatype_NOTATION.Restriction != null && (datatype_NOTATION.Restriction.Flags & RestrictionFlags.Enumeration) != (RestrictionFlags)0)
				{
					for (int i = 0; i < datatype_NOTATION.Restriction.Enumeration.Count; i++)
					{
						XmlQualifiedName name = (XmlQualifiedName)datatype_NOTATION.Restriction.Enumeration[i];
						if (!notations.Contains(name))
						{
							throw new XmlSchemaException("NOTATION cannot be used directly in a schema; only data types derived from NOTATION by specifying an enumeration value can be used in a schema. All enumeration facet values must match the name of a notation declared in the current schema.", caller);
						}
					}
					return;
				}
			}
			throw new XmlSchemaException("NOTATION cannot be used directly in a schema; only data types derived from NOTATION by specifying an enumeration value can be used in a schema. All enumeration facet values must match the name of a notation declared in the current schema.", caller);
		}

		// Token: 0x060035A8 RID: 13736 RVA: 0x0012B310 File Offset: 0x00129510
		public Datatype_NOTATION()
		{
		}

		// Token: 0x060035A9 RID: 13737 RVA: 0x0012BDF3 File Offset: 0x00129FF3
		// Note: this type is marked as 'beforefieldinit'.
		static Datatype_NOTATION()
		{
		}

		// Token: 0x040027A4 RID: 10148
		private static readonly Type atomicValueType = typeof(XmlQualifiedName);

		// Token: 0x040027A5 RID: 10149
		private static readonly Type listValueType = typeof(XmlQualifiedName[]);
	}
}
