using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Xml.Schema
{
	// Token: 0x02000573 RID: 1395
	internal sealed class SchemaElementDecl : SchemaDeclBase, IDtdAttributeListInfo
	{
		// Token: 0x060037D0 RID: 14288 RVA: 0x0013FDE8 File Offset: 0x0013DFE8
		internal SchemaElementDecl()
		{
		}

		// Token: 0x060037D1 RID: 14289 RVA: 0x0013FE06 File Offset: 0x0013E006
		internal SchemaElementDecl(XmlSchemaDatatype dtype)
		{
			base.Datatype = dtype;
			this.contentValidator = ContentValidator.TextOnly;
		}

		// Token: 0x060037D2 RID: 14290 RVA: 0x0013FE36 File Offset: 0x0013E036
		internal SchemaElementDecl(XmlQualifiedName name, string prefix) : base(name, prefix)
		{
		}

		// Token: 0x060037D3 RID: 14291 RVA: 0x0013FE56 File Offset: 0x0013E056
		internal static SchemaElementDecl CreateAnyTypeElementDecl()
		{
			return new SchemaElementDecl
			{
				Datatype = DatatypeImplementation.AnySimpleType.Datatype
			};
		}

		// Token: 0x17000A53 RID: 2643
		// (get) Token: 0x060037D4 RID: 14292 RVA: 0x00138614 File Offset: 0x00136814
		string IDtdAttributeListInfo.Prefix
		{
			get
			{
				return this.Prefix;
			}
		}

		// Token: 0x17000A54 RID: 2644
		// (get) Token: 0x060037D5 RID: 14293 RVA: 0x0013861C File Offset: 0x0013681C
		string IDtdAttributeListInfo.LocalName
		{
			get
			{
				return this.Name.Name;
			}
		}

		// Token: 0x17000A55 RID: 2645
		// (get) Token: 0x060037D6 RID: 14294 RVA: 0x0013FE6D File Offset: 0x0013E06D
		bool IDtdAttributeListInfo.HasNonCDataAttributes
		{
			get
			{
				return this.hasNonCDataAttribute;
			}
		}

		// Token: 0x060037D7 RID: 14295 RVA: 0x0013FE78 File Offset: 0x0013E078
		IDtdAttributeInfo IDtdAttributeListInfo.LookupAttribute(string prefix, string localName)
		{
			XmlQualifiedName key = new XmlQualifiedName(localName, prefix);
			SchemaAttDef result;
			if (this.attdefs.TryGetValue(key, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x060037D8 RID: 14296 RVA: 0x0013FEA0 File Offset: 0x0013E0A0
		IEnumerable<IDtdDefaultAttributeInfo> IDtdAttributeListInfo.LookupDefaultAttributes()
		{
			return this.defaultAttdefs;
		}

		// Token: 0x060037D9 RID: 14297 RVA: 0x0013FEA8 File Offset: 0x0013E0A8
		IDtdAttributeInfo IDtdAttributeListInfo.LookupIdAttribute()
		{
			foreach (SchemaAttDef schemaAttDef in this.attdefs.Values)
			{
				if (schemaAttDef.TokenizedType == XmlTokenizedType.ID)
				{
					return schemaAttDef;
				}
			}
			return null;
		}

		// Token: 0x17000A56 RID: 2646
		// (get) Token: 0x060037DA RID: 14298 RVA: 0x0013FF0C File Offset: 0x0013E10C
		// (set) Token: 0x060037DB RID: 14299 RVA: 0x0013FF14 File Offset: 0x0013E114
		internal bool IsIdDeclared
		{
			get
			{
				return this.isIdDeclared;
			}
			set
			{
				this.isIdDeclared = value;
			}
		}

		// Token: 0x17000A57 RID: 2647
		// (get) Token: 0x060037DC RID: 14300 RVA: 0x0013FE6D File Offset: 0x0013E06D
		// (set) Token: 0x060037DD RID: 14301 RVA: 0x0013FF1D File Offset: 0x0013E11D
		internal bool HasNonCDataAttribute
		{
			get
			{
				return this.hasNonCDataAttribute;
			}
			set
			{
				this.hasNonCDataAttribute = value;
			}
		}

		// Token: 0x060037DE RID: 14302 RVA: 0x0013FF26 File Offset: 0x0013E126
		internal SchemaElementDecl Clone()
		{
			return (SchemaElementDecl)base.MemberwiseClone();
		}

		// Token: 0x17000A58 RID: 2648
		// (get) Token: 0x060037DF RID: 14303 RVA: 0x0013FF33 File Offset: 0x0013E133
		// (set) Token: 0x060037E0 RID: 14304 RVA: 0x0013FF3B File Offset: 0x0013E13B
		internal bool IsAbstract
		{
			get
			{
				return this.isAbstract;
			}
			set
			{
				this.isAbstract = value;
			}
		}

		// Token: 0x17000A59 RID: 2649
		// (get) Token: 0x060037E1 RID: 14305 RVA: 0x0013FF44 File Offset: 0x0013E144
		// (set) Token: 0x060037E2 RID: 14306 RVA: 0x0013FF4C File Offset: 0x0013E14C
		internal bool IsNillable
		{
			get
			{
				return this.isNillable;
			}
			set
			{
				this.isNillable = value;
			}
		}

		// Token: 0x17000A5A RID: 2650
		// (get) Token: 0x060037E3 RID: 14307 RVA: 0x0013FF55 File Offset: 0x0013E155
		// (set) Token: 0x060037E4 RID: 14308 RVA: 0x0013FF5D File Offset: 0x0013E15D
		internal XmlSchemaDerivationMethod Block
		{
			get
			{
				return this.block;
			}
			set
			{
				this.block = value;
			}
		}

		// Token: 0x17000A5B RID: 2651
		// (get) Token: 0x060037E5 RID: 14309 RVA: 0x0013FF66 File Offset: 0x0013E166
		// (set) Token: 0x060037E6 RID: 14310 RVA: 0x0013FF6E File Offset: 0x0013E16E
		internal bool IsNotationDeclared
		{
			get
			{
				return this.isNotationDeclared;
			}
			set
			{
				this.isNotationDeclared = value;
			}
		}

		// Token: 0x17000A5C RID: 2652
		// (get) Token: 0x060037E7 RID: 14311 RVA: 0x0013FF77 File Offset: 0x0013E177
		internal bool HasDefaultAttribute
		{
			get
			{
				return this.defaultAttdefs != null;
			}
		}

		// Token: 0x17000A5D RID: 2653
		// (get) Token: 0x060037E8 RID: 14312 RVA: 0x0013FF82 File Offset: 0x0013E182
		// (set) Token: 0x060037E9 RID: 14313 RVA: 0x0013FF8A File Offset: 0x0013E18A
		internal bool HasRequiredAttribute
		{
			get
			{
				return this.hasRequiredAttribute;
			}
			set
			{
				this.hasRequiredAttribute = value;
			}
		}

		// Token: 0x17000A5E RID: 2654
		// (get) Token: 0x060037EA RID: 14314 RVA: 0x0013FF93 File Offset: 0x0013E193
		// (set) Token: 0x060037EB RID: 14315 RVA: 0x0013FF9B File Offset: 0x0013E19B
		internal ContentValidator ContentValidator
		{
			get
			{
				return this.contentValidator;
			}
			set
			{
				this.contentValidator = value;
			}
		}

		// Token: 0x17000A5F RID: 2655
		// (get) Token: 0x060037EC RID: 14316 RVA: 0x0013FFA4 File Offset: 0x0013E1A4
		// (set) Token: 0x060037ED RID: 14317 RVA: 0x0013FFAC File Offset: 0x0013E1AC
		internal XmlSchemaAnyAttribute AnyAttribute
		{
			get
			{
				return this.anyAttribute;
			}
			set
			{
				this.anyAttribute = value;
			}
		}

		// Token: 0x17000A60 RID: 2656
		// (get) Token: 0x060037EE RID: 14318 RVA: 0x0013FFB5 File Offset: 0x0013E1B5
		// (set) Token: 0x060037EF RID: 14319 RVA: 0x0013FFBD File Offset: 0x0013E1BD
		internal CompiledIdentityConstraint[] Constraints
		{
			get
			{
				return this.constraints;
			}
			set
			{
				this.constraints = value;
			}
		}

		// Token: 0x17000A61 RID: 2657
		// (get) Token: 0x060037F0 RID: 14320 RVA: 0x0013FFC6 File Offset: 0x0013E1C6
		// (set) Token: 0x060037F1 RID: 14321 RVA: 0x0013FFCE File Offset: 0x0013E1CE
		internal XmlSchemaElement SchemaElement
		{
			get
			{
				return this.schemaElement;
			}
			set
			{
				this.schemaElement = value;
			}
		}

		// Token: 0x060037F2 RID: 14322 RVA: 0x0013FFD8 File Offset: 0x0013E1D8
		internal void AddAttDef(SchemaAttDef attdef)
		{
			this.attdefs.Add(attdef.Name, attdef);
			if (attdef.Presence == SchemaDeclBase.Use.Required || attdef.Presence == SchemaDeclBase.Use.RequiredFixed)
			{
				this.hasRequiredAttribute = true;
			}
			if (attdef.Presence == SchemaDeclBase.Use.Default || attdef.Presence == SchemaDeclBase.Use.Fixed)
			{
				if (this.defaultAttdefs == null)
				{
					this.defaultAttdefs = new List<IDtdDefaultAttributeInfo>();
				}
				this.defaultAttdefs.Add(attdef);
			}
		}

		// Token: 0x060037F3 RID: 14323 RVA: 0x00140040 File Offset: 0x0013E240
		internal SchemaAttDef GetAttDef(XmlQualifiedName qname)
		{
			SchemaAttDef result;
			if (this.attdefs.TryGetValue(qname, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x17000A62 RID: 2658
		// (get) Token: 0x060037F4 RID: 14324 RVA: 0x0013FEA0 File Offset: 0x0013E0A0
		internal IList<IDtdDefaultAttributeInfo> DefaultAttDefs
		{
			get
			{
				return this.defaultAttdefs;
			}
		}

		// Token: 0x17000A63 RID: 2659
		// (get) Token: 0x060037F5 RID: 14325 RVA: 0x00140060 File Offset: 0x0013E260
		internal Dictionary<XmlQualifiedName, SchemaAttDef> AttDefs
		{
			get
			{
				return this.attdefs;
			}
		}

		// Token: 0x17000A64 RID: 2660
		// (get) Token: 0x060037F6 RID: 14326 RVA: 0x00140068 File Offset: 0x0013E268
		internal Dictionary<XmlQualifiedName, XmlQualifiedName> ProhibitedAttributes
		{
			get
			{
				return this.prohibitedAttributes;
			}
		}

		// Token: 0x060037F7 RID: 14327 RVA: 0x00140070 File Offset: 0x0013E270
		internal void CheckAttributes(Hashtable presence, bool standalone)
		{
			foreach (SchemaAttDef schemaAttDef in this.attdefs.Values)
			{
				if (presence[schemaAttDef.Name] == null)
				{
					if (schemaAttDef.Presence == SchemaDeclBase.Use.Required)
					{
						throw new XmlSchemaException("The required attribute '{0}' is missing.", schemaAttDef.Name.ToString());
					}
					if (standalone && schemaAttDef.IsDeclaredInExternal && (schemaAttDef.Presence == SchemaDeclBase.Use.Default || schemaAttDef.Presence == SchemaDeclBase.Use.Fixed))
					{
						throw new XmlSchemaException("The standalone document declaration must have a value of 'no'.", string.Empty);
					}
				}
			}
		}

		// Token: 0x060037F8 RID: 14328 RVA: 0x0014011C File Offset: 0x0013E31C
		// Note: this type is marked as 'beforefieldinit'.
		static SchemaElementDecl()
		{
		}

		// Token: 0x040028AC RID: 10412
		private Dictionary<XmlQualifiedName, SchemaAttDef> attdefs = new Dictionary<XmlQualifiedName, SchemaAttDef>();

		// Token: 0x040028AD RID: 10413
		private List<IDtdDefaultAttributeInfo> defaultAttdefs;

		// Token: 0x040028AE RID: 10414
		private bool isIdDeclared;

		// Token: 0x040028AF RID: 10415
		private bool hasNonCDataAttribute;

		// Token: 0x040028B0 RID: 10416
		private bool isAbstract;

		// Token: 0x040028B1 RID: 10417
		private bool isNillable;

		// Token: 0x040028B2 RID: 10418
		private bool hasRequiredAttribute;

		// Token: 0x040028B3 RID: 10419
		private bool isNotationDeclared;

		// Token: 0x040028B4 RID: 10420
		private Dictionary<XmlQualifiedName, XmlQualifiedName> prohibitedAttributes = new Dictionary<XmlQualifiedName, XmlQualifiedName>();

		// Token: 0x040028B5 RID: 10421
		private ContentValidator contentValidator;

		// Token: 0x040028B6 RID: 10422
		private XmlSchemaAnyAttribute anyAttribute;

		// Token: 0x040028B7 RID: 10423
		private XmlSchemaDerivationMethod block;

		// Token: 0x040028B8 RID: 10424
		private CompiledIdentityConstraint[] constraints;

		// Token: 0x040028B9 RID: 10425
		private XmlSchemaElement schemaElement;

		// Token: 0x040028BA RID: 10426
		internal static readonly SchemaElementDecl Empty = new SchemaElementDecl();
	}
}
