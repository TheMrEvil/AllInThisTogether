using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Xml.Schema
{
	// Token: 0x02000576 RID: 1398
	internal class SchemaInfo : IDtdInfo
	{
		// Token: 0x0600381E RID: 14366 RVA: 0x001402F4 File Offset: 0x0013E4F4
		internal SchemaInfo()
		{
			this.schemaType = SchemaType.None;
		}

		// Token: 0x17000A7D RID: 2685
		// (get) Token: 0x0600381F RID: 14367 RVA: 0x0014035B File Offset: 0x0013E55B
		// (set) Token: 0x06003820 RID: 14368 RVA: 0x00140363 File Offset: 0x0013E563
		public XmlQualifiedName DocTypeName
		{
			get
			{
				return this.docTypeName;
			}
			set
			{
				this.docTypeName = value;
			}
		}

		// Token: 0x17000A7E RID: 2686
		// (get) Token: 0x06003821 RID: 14369 RVA: 0x0014036C File Offset: 0x0013E56C
		// (set) Token: 0x06003822 RID: 14370 RVA: 0x00140374 File Offset: 0x0013E574
		internal string InternalDtdSubset
		{
			get
			{
				return this.internalDtdSubset;
			}
			set
			{
				this.internalDtdSubset = value;
			}
		}

		// Token: 0x17000A7F RID: 2687
		// (get) Token: 0x06003823 RID: 14371 RVA: 0x0014037D File Offset: 0x0013E57D
		internal Dictionary<XmlQualifiedName, SchemaElementDecl> ElementDecls
		{
			get
			{
				return this.elementDecls;
			}
		}

		// Token: 0x17000A80 RID: 2688
		// (get) Token: 0x06003824 RID: 14372 RVA: 0x00140385 File Offset: 0x0013E585
		internal Dictionary<XmlQualifiedName, SchemaElementDecl> UndeclaredElementDecls
		{
			get
			{
				return this.undeclaredElementDecls;
			}
		}

		// Token: 0x17000A81 RID: 2689
		// (get) Token: 0x06003825 RID: 14373 RVA: 0x0014038D File Offset: 0x0013E58D
		internal Dictionary<XmlQualifiedName, SchemaEntity> GeneralEntities
		{
			get
			{
				if (this.generalEntities == null)
				{
					this.generalEntities = new Dictionary<XmlQualifiedName, SchemaEntity>();
				}
				return this.generalEntities;
			}
		}

		// Token: 0x17000A82 RID: 2690
		// (get) Token: 0x06003826 RID: 14374 RVA: 0x001403A8 File Offset: 0x0013E5A8
		internal Dictionary<XmlQualifiedName, SchemaEntity> ParameterEntities
		{
			get
			{
				if (this.parameterEntities == null)
				{
					this.parameterEntities = new Dictionary<XmlQualifiedName, SchemaEntity>();
				}
				return this.parameterEntities;
			}
		}

		// Token: 0x17000A83 RID: 2691
		// (get) Token: 0x06003827 RID: 14375 RVA: 0x001403C3 File Offset: 0x0013E5C3
		// (set) Token: 0x06003828 RID: 14376 RVA: 0x001403CB File Offset: 0x0013E5CB
		internal SchemaType SchemaType
		{
			get
			{
				return this.schemaType;
			}
			set
			{
				this.schemaType = value;
			}
		}

		// Token: 0x17000A84 RID: 2692
		// (get) Token: 0x06003829 RID: 14377 RVA: 0x001403D4 File Offset: 0x0013E5D4
		internal Dictionary<string, bool> TargetNamespaces
		{
			get
			{
				return this.targetNamespaces;
			}
		}

		// Token: 0x17000A85 RID: 2693
		// (get) Token: 0x0600382A RID: 14378 RVA: 0x001403DC File Offset: 0x0013E5DC
		internal Dictionary<XmlQualifiedName, SchemaElementDecl> ElementDeclsByType
		{
			get
			{
				return this.elementDeclsByType;
			}
		}

		// Token: 0x17000A86 RID: 2694
		// (get) Token: 0x0600382B RID: 14379 RVA: 0x001403E4 File Offset: 0x0013E5E4
		internal Dictionary<XmlQualifiedName, SchemaAttDef> AttributeDecls
		{
			get
			{
				return this.attributeDecls;
			}
		}

		// Token: 0x17000A87 RID: 2695
		// (get) Token: 0x0600382C RID: 14380 RVA: 0x001403EC File Offset: 0x0013E5EC
		internal Dictionary<string, SchemaNotation> Notations
		{
			get
			{
				if (this.notations == null)
				{
					this.notations = new Dictionary<string, SchemaNotation>();
				}
				return this.notations;
			}
		}

		// Token: 0x17000A88 RID: 2696
		// (get) Token: 0x0600382D RID: 14381 RVA: 0x00140407 File Offset: 0x0013E607
		// (set) Token: 0x0600382E RID: 14382 RVA: 0x0014040F File Offset: 0x0013E60F
		internal int ErrorCount
		{
			get
			{
				return this.errorCount;
			}
			set
			{
				this.errorCount = value;
			}
		}

		// Token: 0x0600382F RID: 14383 RVA: 0x00140418 File Offset: 0x0013E618
		internal SchemaElementDecl GetElementDecl(XmlQualifiedName qname)
		{
			SchemaElementDecl result;
			if (this.elementDecls.TryGetValue(qname, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06003830 RID: 14384 RVA: 0x00140438 File Offset: 0x0013E638
		internal SchemaElementDecl GetTypeDecl(XmlQualifiedName qname)
		{
			SchemaElementDecl result;
			if (this.elementDeclsByType.TryGetValue(qname, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06003831 RID: 14385 RVA: 0x00140458 File Offset: 0x0013E658
		internal XmlSchemaElement GetElement(XmlQualifiedName qname)
		{
			SchemaElementDecl elementDecl = this.GetElementDecl(qname);
			if (elementDecl != null)
			{
				return elementDecl.SchemaElement;
			}
			return null;
		}

		// Token: 0x06003832 RID: 14386 RVA: 0x00140478 File Offset: 0x0013E678
		internal XmlSchemaAttribute GetAttribute(XmlQualifiedName qname)
		{
			SchemaAttDef schemaAttDef = this.attributeDecls[qname];
			if (schemaAttDef != null)
			{
				return schemaAttDef.SchemaAttribute;
			}
			return null;
		}

		// Token: 0x06003833 RID: 14387 RVA: 0x001404A0 File Offset: 0x0013E6A0
		internal XmlSchemaElement GetType(XmlQualifiedName qname)
		{
			SchemaElementDecl elementDecl = this.GetElementDecl(qname);
			if (elementDecl != null)
			{
				return elementDecl.SchemaElement;
			}
			return null;
		}

		// Token: 0x06003834 RID: 14388 RVA: 0x001404C0 File Offset: 0x0013E6C0
		internal bool HasSchema(string ns)
		{
			return this.targetNamespaces.ContainsKey(ns);
		}

		// Token: 0x06003835 RID: 14389 RVA: 0x001404C0 File Offset: 0x0013E6C0
		internal bool Contains(string ns)
		{
			return this.targetNamespaces.ContainsKey(ns);
		}

		// Token: 0x06003836 RID: 14390 RVA: 0x001404D0 File Offset: 0x0013E6D0
		internal SchemaAttDef GetAttributeXdr(SchemaElementDecl ed, XmlQualifiedName qname)
		{
			SchemaAttDef schemaAttDef = null;
			if (ed != null)
			{
				schemaAttDef = ed.GetAttDef(qname);
				if (schemaAttDef == null)
				{
					if (!ed.ContentValidator.IsOpen || qname.Namespace.Length == 0)
					{
						throw new XmlSchemaException("The '{0}' attribute is not declared.", qname.ToString());
					}
					if (!this.attributeDecls.TryGetValue(qname, out schemaAttDef) && this.targetNamespaces.ContainsKey(qname.Namespace))
					{
						throw new XmlSchemaException("The '{0}' attribute is not declared.", qname.ToString());
					}
				}
			}
			return schemaAttDef;
		}

		// Token: 0x06003837 RID: 14391 RVA: 0x00140550 File Offset: 0x0013E750
		internal SchemaAttDef GetAttributeXsd(SchemaElementDecl ed, XmlQualifiedName qname, XmlSchemaObject partialValidationType, out AttributeMatchState attributeMatchState)
		{
			SchemaAttDef schemaAttDef = null;
			attributeMatchState = AttributeMatchState.UndeclaredAttribute;
			if (ed != null)
			{
				schemaAttDef = ed.GetAttDef(qname);
				if (schemaAttDef != null)
				{
					attributeMatchState = AttributeMatchState.AttributeFound;
					return schemaAttDef;
				}
				XmlSchemaAnyAttribute anyAttribute = ed.AnyAttribute;
				if (anyAttribute != null)
				{
					if (!anyAttribute.NamespaceList.Allows(qname))
					{
						attributeMatchState = AttributeMatchState.ProhibitedAnyAttribute;
					}
					else if (anyAttribute.ProcessContentsCorrect != XmlSchemaContentProcessing.Skip)
					{
						if (this.attributeDecls.TryGetValue(qname, out schemaAttDef))
						{
							if (schemaAttDef.Datatype.TypeCode == XmlTypeCode.Id)
							{
								attributeMatchState = AttributeMatchState.AnyIdAttributeFound;
							}
							else
							{
								attributeMatchState = AttributeMatchState.AttributeFound;
							}
						}
						else if (anyAttribute.ProcessContentsCorrect == XmlSchemaContentProcessing.Lax)
						{
							attributeMatchState = AttributeMatchState.AnyAttributeLax;
						}
					}
					else
					{
						attributeMatchState = AttributeMatchState.AnyAttributeSkip;
					}
				}
				else if (ed.ProhibitedAttributes.ContainsKey(qname))
				{
					attributeMatchState = AttributeMatchState.ProhibitedAttribute;
				}
			}
			else if (partialValidationType != null)
			{
				XmlSchemaAttribute xmlSchemaAttribute = partialValidationType as XmlSchemaAttribute;
				if (xmlSchemaAttribute != null)
				{
					if (qname.Equals(xmlSchemaAttribute.QualifiedName))
					{
						schemaAttDef = xmlSchemaAttribute.AttDef;
						attributeMatchState = AttributeMatchState.AttributeFound;
					}
					else
					{
						attributeMatchState = AttributeMatchState.AttributeNameMismatch;
					}
				}
				else
				{
					attributeMatchState = AttributeMatchState.ValidateAttributeInvalidCall;
				}
			}
			else if (this.attributeDecls.TryGetValue(qname, out schemaAttDef))
			{
				attributeMatchState = AttributeMatchState.AttributeFound;
			}
			else
			{
				attributeMatchState = AttributeMatchState.UndeclaredElementAndAttribute;
			}
			return schemaAttDef;
		}

		// Token: 0x06003838 RID: 14392 RVA: 0x00140648 File Offset: 0x0013E848
		internal SchemaAttDef GetAttributeXsd(SchemaElementDecl ed, XmlQualifiedName qname, ref bool skip)
		{
			AttributeMatchState attributeMatchState;
			SchemaAttDef attributeXsd = this.GetAttributeXsd(ed, qname, null, out attributeMatchState);
			switch (attributeMatchState)
			{
			case AttributeMatchState.UndeclaredAttribute:
				throw new XmlSchemaException("The '{0}' attribute is not declared.", qname.ToString());
			case AttributeMatchState.AnyAttributeSkip:
				skip = true;
				break;
			case AttributeMatchState.ProhibitedAnyAttribute:
			case AttributeMatchState.ProhibitedAttribute:
				throw new XmlSchemaException("The '{0}' attribute is not allowed.", qname.ToString());
			}
			return attributeXsd;
		}

		// Token: 0x06003839 RID: 14393 RVA: 0x001406B0 File Offset: 0x0013E8B0
		internal void Add(SchemaInfo sinfo, ValidationEventHandler eventhandler)
		{
			if (this.schemaType == SchemaType.None)
			{
				this.schemaType = sinfo.SchemaType;
			}
			else if (this.schemaType != sinfo.SchemaType)
			{
				if (eventhandler != null)
				{
					eventhandler(this, new ValidationEventArgs(new XmlSchemaException("Different schema types cannot be mixed.", string.Empty)));
				}
				return;
			}
			foreach (string key in sinfo.TargetNamespaces.Keys)
			{
				if (!this.targetNamespaces.ContainsKey(key))
				{
					this.targetNamespaces.Add(key, true);
				}
			}
			foreach (KeyValuePair<XmlQualifiedName, SchemaElementDecl> keyValuePair in sinfo.elementDecls)
			{
				if (!this.elementDecls.ContainsKey(keyValuePair.Key))
				{
					this.elementDecls.Add(keyValuePair.Key, keyValuePair.Value);
				}
			}
			foreach (KeyValuePair<XmlQualifiedName, SchemaElementDecl> keyValuePair2 in sinfo.elementDeclsByType)
			{
				if (!this.elementDeclsByType.ContainsKey(keyValuePair2.Key))
				{
					this.elementDeclsByType.Add(keyValuePair2.Key, keyValuePair2.Value);
				}
			}
			foreach (SchemaAttDef schemaAttDef in sinfo.AttributeDecls.Values)
			{
				if (!this.attributeDecls.ContainsKey(schemaAttDef.Name))
				{
					this.attributeDecls.Add(schemaAttDef.Name, schemaAttDef);
				}
			}
			foreach (SchemaNotation schemaNotation in sinfo.Notations.Values)
			{
				if (!this.Notations.ContainsKey(schemaNotation.Name.Name))
				{
					this.Notations.Add(schemaNotation.Name.Name, schemaNotation);
				}
			}
		}

		// Token: 0x0600383A RID: 14394 RVA: 0x00140910 File Offset: 0x0013EB10
		internal void Finish()
		{
			Dictionary<XmlQualifiedName, SchemaElementDecl> dictionary = this.elementDecls;
			for (int i = 0; i < 2; i++)
			{
				foreach (SchemaElementDecl schemaElementDecl in dictionary.Values)
				{
					if (schemaElementDecl.HasNonCDataAttribute)
					{
						this.hasNonCDataAttributes = true;
					}
					if (schemaElementDecl.DefaultAttDefs != null)
					{
						this.hasDefaultAttributes = true;
					}
				}
				dictionary = this.undeclaredElementDecls;
			}
		}

		// Token: 0x17000A89 RID: 2697
		// (get) Token: 0x0600383B RID: 14395 RVA: 0x00140994 File Offset: 0x0013EB94
		bool IDtdInfo.HasDefaultAttributes
		{
			get
			{
				return this.hasDefaultAttributes;
			}
		}

		// Token: 0x17000A8A RID: 2698
		// (get) Token: 0x0600383C RID: 14396 RVA: 0x0014099C File Offset: 0x0013EB9C
		bool IDtdInfo.HasNonCDataAttributes
		{
			get
			{
				return this.hasNonCDataAttributes;
			}
		}

		// Token: 0x0600383D RID: 14397 RVA: 0x001409A4 File Offset: 0x0013EBA4
		IDtdAttributeListInfo IDtdInfo.LookupAttributeList(string prefix, string localName)
		{
			XmlQualifiedName key = new XmlQualifiedName(prefix, localName);
			SchemaElementDecl result;
			if (!this.elementDecls.TryGetValue(key, out result))
			{
				this.undeclaredElementDecls.TryGetValue(key, out result);
			}
			return result;
		}

		// Token: 0x0600383E RID: 14398 RVA: 0x001409D9 File Offset: 0x0013EBD9
		IEnumerable<IDtdAttributeListInfo> IDtdInfo.GetAttributeLists()
		{
			foreach (IDtdAttributeListInfo dtdAttributeListInfo in this.elementDecls.Values)
			{
				yield return dtdAttributeListInfo;
			}
			Dictionary<XmlQualifiedName, SchemaElementDecl>.ValueCollection.Enumerator enumerator = default(Dictionary<XmlQualifiedName, SchemaElementDecl>.ValueCollection.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x0600383F RID: 14399 RVA: 0x001409EC File Offset: 0x0013EBEC
		IDtdEntityInfo IDtdInfo.LookupEntity(string name)
		{
			if (this.generalEntities == null)
			{
				return null;
			}
			XmlQualifiedName key = new XmlQualifiedName(name);
			SchemaEntity result;
			if (this.generalEntities.TryGetValue(key, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x17000A8B RID: 2699
		// (get) Token: 0x06003840 RID: 14400 RVA: 0x0014035B File Offset: 0x0013E55B
		XmlQualifiedName IDtdInfo.Name
		{
			get
			{
				return this.docTypeName;
			}
		}

		// Token: 0x17000A8C RID: 2700
		// (get) Token: 0x06003841 RID: 14401 RVA: 0x0014036C File Offset: 0x0013E56C
		string IDtdInfo.InternalDtdSubset
		{
			get
			{
				return this.internalDtdSubset;
			}
		}

		// Token: 0x040028D3 RID: 10451
		private Dictionary<XmlQualifiedName, SchemaElementDecl> elementDecls = new Dictionary<XmlQualifiedName, SchemaElementDecl>();

		// Token: 0x040028D4 RID: 10452
		private Dictionary<XmlQualifiedName, SchemaElementDecl> undeclaredElementDecls = new Dictionary<XmlQualifiedName, SchemaElementDecl>();

		// Token: 0x040028D5 RID: 10453
		private Dictionary<XmlQualifiedName, SchemaEntity> generalEntities;

		// Token: 0x040028D6 RID: 10454
		private Dictionary<XmlQualifiedName, SchemaEntity> parameterEntities;

		// Token: 0x040028D7 RID: 10455
		private XmlQualifiedName docTypeName = XmlQualifiedName.Empty;

		// Token: 0x040028D8 RID: 10456
		private string internalDtdSubset = string.Empty;

		// Token: 0x040028D9 RID: 10457
		private bool hasNonCDataAttributes;

		// Token: 0x040028DA RID: 10458
		private bool hasDefaultAttributes;

		// Token: 0x040028DB RID: 10459
		private Dictionary<string, bool> targetNamespaces = new Dictionary<string, bool>();

		// Token: 0x040028DC RID: 10460
		private Dictionary<XmlQualifiedName, SchemaAttDef> attributeDecls = new Dictionary<XmlQualifiedName, SchemaAttDef>();

		// Token: 0x040028DD RID: 10461
		private int errorCount;

		// Token: 0x040028DE RID: 10462
		private SchemaType schemaType;

		// Token: 0x040028DF RID: 10463
		private Dictionary<XmlQualifiedName, SchemaElementDecl> elementDeclsByType = new Dictionary<XmlQualifiedName, SchemaElementDecl>();

		// Token: 0x040028E0 RID: 10464
		private Dictionary<string, SchemaNotation> notations;

		// Token: 0x02000577 RID: 1399
		[CompilerGenerated]
		private sealed class <System-Xml-IDtdInfo-GetAttributeLists>d__60 : IEnumerable<IDtdAttributeListInfo>, IEnumerable, IEnumerator<IDtdAttributeListInfo>, IDisposable, IEnumerator
		{
			// Token: 0x06003842 RID: 14402 RVA: 0x00140A1D File Offset: 0x0013EC1D
			[DebuggerHidden]
			public <System-Xml-IDtdInfo-GetAttributeLists>d__60(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x06003843 RID: 14403 RVA: 0x00140A38 File Offset: 0x0013EC38
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
				int num = this.<>1__state;
				if (num == -3 || num == 1)
				{
					try
					{
					}
					finally
					{
						this.<>m__Finally1();
					}
				}
			}

			// Token: 0x06003844 RID: 14404 RVA: 0x00140A70 File Offset: 0x0013EC70
			bool IEnumerator.MoveNext()
			{
				bool result;
				try
				{
					int num = this.<>1__state;
					SchemaInfo schemaInfo = this;
					if (num != 0)
					{
						if (num != 1)
						{
							return false;
						}
						this.<>1__state = -3;
					}
					else
					{
						this.<>1__state = -1;
						enumerator = schemaInfo.elementDecls.Values.GetEnumerator();
						this.<>1__state = -3;
					}
					if (!enumerator.MoveNext())
					{
						this.<>m__Finally1();
						enumerator = default(Dictionary<XmlQualifiedName, SchemaElementDecl>.ValueCollection.Enumerator);
						result = false;
					}
					else
					{
						IDtdAttributeListInfo dtdAttributeListInfo = enumerator.Current;
						this.<>2__current = dtdAttributeListInfo;
						this.<>1__state = 1;
						result = true;
					}
				}
				catch
				{
					this.System.IDisposable.Dispose();
					throw;
				}
				return result;
			}

			// Token: 0x06003845 RID: 14405 RVA: 0x00140B20 File Offset: 0x0013ED20
			private void <>m__Finally1()
			{
				this.<>1__state = -1;
				((IDisposable)enumerator).Dispose();
			}

			// Token: 0x17000A8D RID: 2701
			// (get) Token: 0x06003846 RID: 14406 RVA: 0x00140B3A File Offset: 0x0013ED3A
			IDtdAttributeListInfo IEnumerator<IDtdAttributeListInfo>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06003847 RID: 14407 RVA: 0x00005BD6 File Offset: 0x00003DD6
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000A8E RID: 2702
			// (get) Token: 0x06003848 RID: 14408 RVA: 0x00140B3A File Offset: 0x0013ED3A
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06003849 RID: 14409 RVA: 0x00140B44 File Offset: 0x0013ED44
			[DebuggerHidden]
			IEnumerator<IDtdAttributeListInfo> IEnumerable<IDtdAttributeListInfo>.GetEnumerator()
			{
				SchemaInfo.<System-Xml-IDtdInfo-GetAttributeLists>d__60 <System-Xml-IDtdInfo-GetAttributeLists>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<System-Xml-IDtdInfo-GetAttributeLists>d__ = this;
				}
				else
				{
					<System-Xml-IDtdInfo-GetAttributeLists>d__ = new SchemaInfo.<System-Xml-IDtdInfo-GetAttributeLists>d__60(0);
					<System-Xml-IDtdInfo-GetAttributeLists>d__.<>4__this = this;
				}
				return <System-Xml-IDtdInfo-GetAttributeLists>d__;
			}

			// Token: 0x0600384A RID: 14410 RVA: 0x00140B87 File Offset: 0x0013ED87
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.Xml.IDtdAttributeListInfo>.GetEnumerator();
			}

			// Token: 0x040028E1 RID: 10465
			private int <>1__state;

			// Token: 0x040028E2 RID: 10466
			private IDtdAttributeListInfo <>2__current;

			// Token: 0x040028E3 RID: 10467
			private int <>l__initialThreadId;

			// Token: 0x040028E4 RID: 10468
			public SchemaInfo <>4__this;

			// Token: 0x040028E5 RID: 10469
			private Dictionary<XmlQualifiedName, SchemaElementDecl>.ValueCollection.Enumerator <>7__wrap1;
		}
	}
}
