using System;

namespace System.Xml.Schema
{
	// Token: 0x0200056B RID: 1387
	internal sealed class SchemaAttDef : SchemaDeclBase, IDtdDefaultAttributeInfo, IDtdAttributeInfo
	{
		// Token: 0x06003726 RID: 14118 RVA: 0x001385F8 File Offset: 0x001367F8
		public SchemaAttDef(XmlQualifiedName name, string prefix) : base(name, prefix)
		{
		}

		// Token: 0x06003727 RID: 14119 RVA: 0x00138602 File Offset: 0x00136802
		public SchemaAttDef(XmlQualifiedName name) : base(name, null)
		{
		}

		// Token: 0x06003728 RID: 14120 RVA: 0x0013860C File Offset: 0x0013680C
		private SchemaAttDef()
		{
		}

		// Token: 0x17000A32 RID: 2610
		// (get) Token: 0x06003729 RID: 14121 RVA: 0x00138614 File Offset: 0x00136814
		string IDtdAttributeInfo.Prefix
		{
			get
			{
				return this.Prefix;
			}
		}

		// Token: 0x17000A33 RID: 2611
		// (get) Token: 0x0600372A RID: 14122 RVA: 0x0013861C File Offset: 0x0013681C
		string IDtdAttributeInfo.LocalName
		{
			get
			{
				return this.Name.Name;
			}
		}

		// Token: 0x17000A34 RID: 2612
		// (get) Token: 0x0600372B RID: 14123 RVA: 0x00138629 File Offset: 0x00136829
		int IDtdAttributeInfo.LineNumber
		{
			get
			{
				return this.LineNumber;
			}
		}

		// Token: 0x17000A35 RID: 2613
		// (get) Token: 0x0600372C RID: 14124 RVA: 0x00138631 File Offset: 0x00136831
		int IDtdAttributeInfo.LinePosition
		{
			get
			{
				return this.LinePosition;
			}
		}

		// Token: 0x17000A36 RID: 2614
		// (get) Token: 0x0600372D RID: 14125 RVA: 0x00138639 File Offset: 0x00136839
		bool IDtdAttributeInfo.IsNonCDataType
		{
			get
			{
				return this.TokenizedType > XmlTokenizedType.CDATA;
			}
		}

		// Token: 0x17000A37 RID: 2615
		// (get) Token: 0x0600372E RID: 14126 RVA: 0x00138644 File Offset: 0x00136844
		bool IDtdAttributeInfo.IsDeclaredInExternal
		{
			get
			{
				return this.IsDeclaredInExternal;
			}
		}

		// Token: 0x17000A38 RID: 2616
		// (get) Token: 0x0600372F RID: 14127 RVA: 0x0013864C File Offset: 0x0013684C
		bool IDtdAttributeInfo.IsXmlAttribute
		{
			get
			{
				return this.Reserved > SchemaAttDef.Reserve.None;
			}
		}

		// Token: 0x17000A39 RID: 2617
		// (get) Token: 0x06003730 RID: 14128 RVA: 0x00138657 File Offset: 0x00136857
		string IDtdDefaultAttributeInfo.DefaultValueExpanded
		{
			get
			{
				return this.DefaultValueExpanded;
			}
		}

		// Token: 0x17000A3A RID: 2618
		// (get) Token: 0x06003731 RID: 14129 RVA: 0x0013865F File Offset: 0x0013685F
		object IDtdDefaultAttributeInfo.DefaultValueTyped
		{
			get
			{
				return this.DefaultValueTyped;
			}
		}

		// Token: 0x17000A3B RID: 2619
		// (get) Token: 0x06003732 RID: 14130 RVA: 0x00138667 File Offset: 0x00136867
		int IDtdDefaultAttributeInfo.ValueLineNumber
		{
			get
			{
				return this.ValueLineNumber;
			}
		}

		// Token: 0x17000A3C RID: 2620
		// (get) Token: 0x06003733 RID: 14131 RVA: 0x0013866F File Offset: 0x0013686F
		int IDtdDefaultAttributeInfo.ValueLinePosition
		{
			get
			{
				return this.ValueLinePosition;
			}
		}

		// Token: 0x17000A3D RID: 2621
		// (get) Token: 0x06003734 RID: 14132 RVA: 0x00138677 File Offset: 0x00136877
		// (set) Token: 0x06003735 RID: 14133 RVA: 0x0013867F File Offset: 0x0013687F
		internal int LinePosition
		{
			get
			{
				return this.linePos;
			}
			set
			{
				this.linePos = value;
			}
		}

		// Token: 0x17000A3E RID: 2622
		// (get) Token: 0x06003736 RID: 14134 RVA: 0x00138688 File Offset: 0x00136888
		// (set) Token: 0x06003737 RID: 14135 RVA: 0x00138690 File Offset: 0x00136890
		internal int LineNumber
		{
			get
			{
				return this.lineNum;
			}
			set
			{
				this.lineNum = value;
			}
		}

		// Token: 0x17000A3F RID: 2623
		// (get) Token: 0x06003738 RID: 14136 RVA: 0x00138699 File Offset: 0x00136899
		// (set) Token: 0x06003739 RID: 14137 RVA: 0x001386A1 File Offset: 0x001368A1
		internal int ValueLinePosition
		{
			get
			{
				return this.valueLinePos;
			}
			set
			{
				this.valueLinePos = value;
			}
		}

		// Token: 0x17000A40 RID: 2624
		// (get) Token: 0x0600373A RID: 14138 RVA: 0x001386AA File Offset: 0x001368AA
		// (set) Token: 0x0600373B RID: 14139 RVA: 0x001386B2 File Offset: 0x001368B2
		internal int ValueLineNumber
		{
			get
			{
				return this.valueLineNum;
			}
			set
			{
				this.valueLineNum = value;
			}
		}

		// Token: 0x17000A41 RID: 2625
		// (get) Token: 0x0600373C RID: 14140 RVA: 0x001386BB File Offset: 0x001368BB
		// (set) Token: 0x0600373D RID: 14141 RVA: 0x001386D1 File Offset: 0x001368D1
		internal string DefaultValueExpanded
		{
			get
			{
				if (this.defExpanded == null)
				{
					return string.Empty;
				}
				return this.defExpanded;
			}
			set
			{
				this.defExpanded = value;
			}
		}

		// Token: 0x17000A42 RID: 2626
		// (get) Token: 0x0600373E RID: 14142 RVA: 0x001386DA File Offset: 0x001368DA
		// (set) Token: 0x0600373F RID: 14143 RVA: 0x001386E7 File Offset: 0x001368E7
		internal XmlTokenizedType TokenizedType
		{
			get
			{
				return base.Datatype.TokenizedType;
			}
			set
			{
				base.Datatype = XmlSchemaDatatype.FromXmlTokenizedType(value);
			}
		}

		// Token: 0x17000A43 RID: 2627
		// (get) Token: 0x06003740 RID: 14144 RVA: 0x001386F5 File Offset: 0x001368F5
		// (set) Token: 0x06003741 RID: 14145 RVA: 0x001386FD File Offset: 0x001368FD
		internal SchemaAttDef.Reserve Reserved
		{
			get
			{
				return this.reserved;
			}
			set
			{
				this.reserved = value;
			}
		}

		// Token: 0x17000A44 RID: 2628
		// (get) Token: 0x06003742 RID: 14146 RVA: 0x00138706 File Offset: 0x00136906
		internal bool DefaultValueChecked
		{
			get
			{
				return this.defaultValueChecked;
			}
		}

		// Token: 0x17000A45 RID: 2629
		// (get) Token: 0x06003743 RID: 14147 RVA: 0x0013870E File Offset: 0x0013690E
		// (set) Token: 0x06003744 RID: 14148 RVA: 0x00138716 File Offset: 0x00136916
		internal bool HasEntityRef
		{
			get
			{
				return this.hasEntityRef;
			}
			set
			{
				this.hasEntityRef = value;
			}
		}

		// Token: 0x17000A46 RID: 2630
		// (get) Token: 0x06003745 RID: 14149 RVA: 0x0013871F File Offset: 0x0013691F
		// (set) Token: 0x06003746 RID: 14150 RVA: 0x00138727 File Offset: 0x00136927
		internal XmlSchemaAttribute SchemaAttribute
		{
			get
			{
				return this.schemaAttribute;
			}
			set
			{
				this.schemaAttribute = value;
			}
		}

		// Token: 0x06003747 RID: 14151 RVA: 0x00138730 File Offset: 0x00136930
		internal void CheckXmlSpace(IValidationEventHandling validationEventHandling)
		{
			if (this.datatype.TokenizedType == XmlTokenizedType.ENUMERATION && this.values != null && this.values.Count <= 2)
			{
				string a = this.values[0].ToString();
				if (this.values.Count == 2)
				{
					string a2 = this.values[1].ToString();
					if ((a == "default" || a2 == "default") && (a == "preserve" || a2 == "preserve"))
					{
						return;
					}
				}
				else if (a == "default" || a == "preserve")
				{
					return;
				}
			}
			validationEventHandling.SendEvent(new XmlSchemaException("Invalid xml:space syntax.", string.Empty), XmlSeverityType.Error);
		}

		// Token: 0x06003748 RID: 14152 RVA: 0x00138803 File Offset: 0x00136A03
		internal SchemaAttDef Clone()
		{
			return (SchemaAttDef)base.MemberwiseClone();
		}

		// Token: 0x06003749 RID: 14153 RVA: 0x00138810 File Offset: 0x00136A10
		// Note: this type is marked as 'beforefieldinit'.
		static SchemaAttDef()
		{
		}

		// Token: 0x04002873 RID: 10355
		private string defExpanded;

		// Token: 0x04002874 RID: 10356
		private int lineNum;

		// Token: 0x04002875 RID: 10357
		private int linePos;

		// Token: 0x04002876 RID: 10358
		private int valueLineNum;

		// Token: 0x04002877 RID: 10359
		private int valueLinePos;

		// Token: 0x04002878 RID: 10360
		private SchemaAttDef.Reserve reserved;

		// Token: 0x04002879 RID: 10361
		private bool defaultValueChecked;

		// Token: 0x0400287A RID: 10362
		private bool hasEntityRef;

		// Token: 0x0400287B RID: 10363
		private XmlSchemaAttribute schemaAttribute;

		// Token: 0x0400287C RID: 10364
		public static readonly SchemaAttDef Empty = new SchemaAttDef();

		// Token: 0x0200056C RID: 1388
		internal enum Reserve
		{
			// Token: 0x0400287E RID: 10366
			None,
			// Token: 0x0400287F RID: 10367
			XmlSpace,
			// Token: 0x04002880 RID: 10368
			XmlLang
		}
	}
}
