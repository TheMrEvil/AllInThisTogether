using System;

namespace System.Xml.Serialization
{
	// Token: 0x0200028D RID: 653
	internal class StructMapping : TypeMapping, INameScope
	{
		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x0600188F RID: 6287 RVA: 0x0008EB04 File Offset: 0x0008CD04
		// (set) Token: 0x06001890 RID: 6288 RVA: 0x0008EB0C File Offset: 0x0008CD0C
		internal StructMapping BaseMapping
		{
			get
			{
				return this.baseMapping;
			}
			set
			{
				this.baseMapping = value;
				if (!base.IsAnonymousType && this.baseMapping != null)
				{
					this.nextDerivedMapping = this.baseMapping.derivedMappings;
					this.baseMapping.derivedMappings = this;
				}
				if (value.isSequence && !this.isSequence)
				{
					this.isSequence = true;
					if (this.baseMapping.IsSequence)
					{
						for (StructMapping structMapping = this.derivedMappings; structMapping != null; structMapping = structMapping.NextDerivedMapping)
						{
							structMapping.SetSequence();
						}
					}
				}
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06001891 RID: 6289 RVA: 0x0008EB8A File Offset: 0x0008CD8A
		internal StructMapping DerivedMappings
		{
			get
			{
				return this.derivedMappings;
			}
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06001892 RID: 6290 RVA: 0x0008EB92 File Offset: 0x0008CD92
		internal bool IsFullyInitialized
		{
			get
			{
				return this.baseMapping != null && this.Members != null;
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x06001893 RID: 6291 RVA: 0x0008EBA7 File Offset: 0x0008CDA7
		internal NameTable LocalElements
		{
			get
			{
				if (this.elements == null)
				{
					this.elements = new NameTable();
				}
				return this.elements;
			}
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06001894 RID: 6292 RVA: 0x0008EBC2 File Offset: 0x0008CDC2
		internal NameTable LocalAttributes
		{
			get
			{
				if (this.attributes == null)
				{
					this.attributes = new NameTable();
				}
				return this.attributes;
			}
		}

		// Token: 0x17000497 RID: 1175
		object INameScope.this[string name, string ns]
		{
			get
			{
				object obj = this.LocalElements[name, ns];
				if (obj != null)
				{
					return obj;
				}
				if (this.baseMapping != null)
				{
					return ((INameScope)this.baseMapping)[name, ns];
				}
				return null;
			}
			set
			{
				this.LocalElements[name, ns] = value;
			}
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06001897 RID: 6295 RVA: 0x0008EC27 File Offset: 0x0008CE27
		internal StructMapping NextDerivedMapping
		{
			get
			{
				return this.nextDerivedMapping;
			}
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06001898 RID: 6296 RVA: 0x0008EC2F File Offset: 0x0008CE2F
		internal bool HasSimpleContent
		{
			get
			{
				return this.hasSimpleContent;
			}
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06001899 RID: 6297 RVA: 0x0008EC38 File Offset: 0x0008CE38
		internal bool HasXmlnsMember
		{
			get
			{
				for (StructMapping structMapping = this; structMapping != null; structMapping = structMapping.BaseMapping)
				{
					if (structMapping.XmlnsMember != null)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x0600189A RID: 6298 RVA: 0x0008EC5E File Offset: 0x0008CE5E
		// (set) Token: 0x0600189B RID: 6299 RVA: 0x0008EC66 File Offset: 0x0008CE66
		internal MemberMapping[] Members
		{
			get
			{
				return this.members;
			}
			set
			{
				this.members = value;
			}
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x0600189C RID: 6300 RVA: 0x0008EC6F File Offset: 0x0008CE6F
		// (set) Token: 0x0600189D RID: 6301 RVA: 0x0008EC77 File Offset: 0x0008CE77
		internal MemberMapping XmlnsMember
		{
			get
			{
				return this.xmlnsMember;
			}
			set
			{
				this.xmlnsMember = value;
			}
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x0600189E RID: 6302 RVA: 0x0008EC80 File Offset: 0x0008CE80
		// (set) Token: 0x0600189F RID: 6303 RVA: 0x0008EC88 File Offset: 0x0008CE88
		internal bool IsOpenModel
		{
			get
			{
				return this.openModel;
			}
			set
			{
				this.openModel = value;
			}
		}

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x060018A0 RID: 6304 RVA: 0x0008EC91 File Offset: 0x0008CE91
		// (set) Token: 0x060018A1 RID: 6305 RVA: 0x0008ECAC File Offset: 0x0008CEAC
		internal CodeIdentifiers Scope
		{
			get
			{
				if (this.scope == null)
				{
					this.scope = new CodeIdentifiers();
				}
				return this.scope;
			}
			set
			{
				this.scope = value;
			}
		}

		// Token: 0x060018A2 RID: 6306 RVA: 0x0008ECB8 File Offset: 0x0008CEB8
		internal MemberMapping FindDeclaringMapping(MemberMapping member, out StructMapping declaringMapping, string parent)
		{
			declaringMapping = null;
			if (this.BaseMapping != null)
			{
				MemberMapping memberMapping = this.BaseMapping.FindDeclaringMapping(member, out declaringMapping, parent);
				if (memberMapping != null)
				{
					return memberMapping;
				}
			}
			if (this.members == null)
			{
				return null;
			}
			int i = 0;
			while (i < this.members.Length)
			{
				if (this.members[i].Name == member.Name)
				{
					if (this.members[i].TypeDesc != member.TypeDesc)
					{
						throw new InvalidOperationException(Res.GetString("Member {0}.{1} of type {2} hides base class member {3}.{4} of type {5}. Use XmlElementAttribute or XmlAttributeAttribute to specify a new name.", new object[]
						{
							parent,
							member.Name,
							member.TypeDesc.FullName,
							base.TypeName,
							this.members[i].Name,
							this.members[i].TypeDesc.FullName
						}));
					}
					if (!this.members[i].Match(member))
					{
						throw new InvalidOperationException(Res.GetString("Member '{0}.{1}' hides inherited member '{2}.{3}', but has different custom attributes.", new object[]
						{
							parent,
							member.Name,
							base.TypeName,
							this.members[i].Name
						}));
					}
					declaringMapping = this;
					return this.members[i];
				}
				else
				{
					i++;
				}
			}
			return null;
		}

		// Token: 0x060018A3 RID: 6307 RVA: 0x0008EDF4 File Offset: 0x0008CFF4
		internal bool Declares(MemberMapping member, string parent)
		{
			StructMapping structMapping;
			return this.FindDeclaringMapping(member, out structMapping, parent) != null;
		}

		// Token: 0x060018A4 RID: 6308 RVA: 0x0008EE10 File Offset: 0x0008D010
		internal void SetContentModel(TextAccessor text, bool hasElements)
		{
			if (this.BaseMapping == null || this.BaseMapping.TypeDesc.IsRoot)
			{
				this.hasSimpleContent = (!hasElements && text != null && !text.Mapping.IsList);
			}
			else if (this.BaseMapping.HasSimpleContent)
			{
				if (text != null || hasElements)
				{
					throw new InvalidOperationException(Res.GetString("Cannot serialize object of type '{0}'. Base type '{1}' has simpleContent and can only be extended by adding XmlAttribute elements. Please consider changing XmlText member of the base class to string array.", new object[]
					{
						base.TypeDesc.FullName,
						this.BaseMapping.TypeDesc.FullName
					}));
				}
				this.hasSimpleContent = true;
			}
			else
			{
				this.hasSimpleContent = false;
			}
			if (!this.hasSimpleContent && text != null && !text.Mapping.TypeDesc.CanBeTextValue)
			{
				throw new InvalidOperationException(Res.GetString("Cannot serialize object of type '{0}'. Consider changing type of XmlText member '{0}.{1}' from {2} to string or string array.", new object[]
				{
					base.TypeDesc.FullName,
					text.Name,
					text.Mapping.TypeDesc.FullName
				}));
			}
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x060018A5 RID: 6309 RVA: 0x0008EF0F File Offset: 0x0008D10F
		internal bool HasElements
		{
			get
			{
				return this.elements != null && this.elements.Values.Count > 0;
			}
		}

		// Token: 0x060018A6 RID: 6310 RVA: 0x0008EF30 File Offset: 0x0008D130
		internal bool HasExplicitSequence()
		{
			if (this.members != null)
			{
				for (int i = 0; i < this.members.Length; i++)
				{
					if (this.members[i].IsParticle && this.members[i].IsSequence)
					{
						return true;
					}
				}
			}
			return this.baseMapping != null && this.baseMapping.HasExplicitSequence();
		}

		// Token: 0x060018A7 RID: 6311 RVA: 0x0008EF90 File Offset: 0x0008D190
		internal void SetSequence()
		{
			if (base.TypeDesc.IsRoot)
			{
				return;
			}
			StructMapping structMapping = this;
			while (!structMapping.BaseMapping.IsSequence && structMapping.BaseMapping != null && !structMapping.BaseMapping.TypeDesc.IsRoot)
			{
				structMapping = structMapping.BaseMapping;
			}
			structMapping.IsSequence = true;
			for (StructMapping structMapping2 = structMapping.DerivedMappings; structMapping2 != null; structMapping2 = structMapping2.NextDerivedMapping)
			{
				structMapping2.SetSequence();
			}
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x060018A8 RID: 6312 RVA: 0x0008EFFD File Offset: 0x0008D1FD
		// (set) Token: 0x060018A9 RID: 6313 RVA: 0x0008F017 File Offset: 0x0008D217
		internal bool IsSequence
		{
			get
			{
				return this.isSequence && !base.TypeDesc.IsRoot;
			}
			set
			{
				this.isSequence = value;
			}
		}

		// Token: 0x060018AA RID: 6314 RVA: 0x0008E9BC File Offset: 0x0008CBBC
		public StructMapping()
		{
		}

		// Token: 0x040018D4 RID: 6356
		private MemberMapping[] members;

		// Token: 0x040018D5 RID: 6357
		private StructMapping baseMapping;

		// Token: 0x040018D6 RID: 6358
		private StructMapping derivedMappings;

		// Token: 0x040018D7 RID: 6359
		private StructMapping nextDerivedMapping;

		// Token: 0x040018D8 RID: 6360
		private MemberMapping xmlnsMember;

		// Token: 0x040018D9 RID: 6361
		private bool hasSimpleContent;

		// Token: 0x040018DA RID: 6362
		private bool openModel;

		// Token: 0x040018DB RID: 6363
		private bool isSequence;

		// Token: 0x040018DC RID: 6364
		private NameTable elements;

		// Token: 0x040018DD RID: 6365
		private NameTable attributes;

		// Token: 0x040018DE RID: 6366
		private CodeIdentifiers scope;
	}
}
