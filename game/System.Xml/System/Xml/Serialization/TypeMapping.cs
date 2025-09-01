using System;

namespace System.Xml.Serialization
{
	// Token: 0x02000287 RID: 647
	internal abstract class TypeMapping : Mapping
	{
		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06001861 RID: 6241 RVA: 0x0008E8E0 File Offset: 0x0008CAE0
		// (set) Token: 0x06001862 RID: 6242 RVA: 0x0008E8E8 File Offset: 0x0008CAE8
		internal bool ReferencedByTopLevelElement
		{
			get
			{
				return this.referencedByTopLevelElement;
			}
			set
			{
				this.referencedByTopLevelElement = value;
			}
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06001863 RID: 6243 RVA: 0x0008E8F1 File Offset: 0x0008CAF1
		// (set) Token: 0x06001864 RID: 6244 RVA: 0x0008E903 File Offset: 0x0008CB03
		internal bool ReferencedByElement
		{
			get
			{
				return this.referencedByElement || this.referencedByTopLevelElement;
			}
			set
			{
				this.referencedByElement = value;
			}
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06001865 RID: 6245 RVA: 0x0008E90C File Offset: 0x0008CB0C
		// (set) Token: 0x06001866 RID: 6246 RVA: 0x0008E914 File Offset: 0x0008CB14
		internal string Namespace
		{
			get
			{
				return this.typeNs;
			}
			set
			{
				this.typeNs = value;
			}
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06001867 RID: 6247 RVA: 0x0008E91D File Offset: 0x0008CB1D
		// (set) Token: 0x06001868 RID: 6248 RVA: 0x0008E925 File Offset: 0x0008CB25
		internal string TypeName
		{
			get
			{
				return this.typeName;
			}
			set
			{
				this.typeName = value;
			}
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06001869 RID: 6249 RVA: 0x0008E92E File Offset: 0x0008CB2E
		// (set) Token: 0x0600186A RID: 6250 RVA: 0x0008E936 File Offset: 0x0008CB36
		internal TypeDesc TypeDesc
		{
			get
			{
				return this.typeDesc;
			}
			set
			{
				this.typeDesc = value;
			}
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x0600186B RID: 6251 RVA: 0x0008E93F File Offset: 0x0008CB3F
		// (set) Token: 0x0600186C RID: 6252 RVA: 0x0008E947 File Offset: 0x0008CB47
		internal bool IncludeInSchema
		{
			get
			{
				return this.includeInSchema;
			}
			set
			{
				this.includeInSchema = value;
			}
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x0600186D RID: 6253 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		// (set) Token: 0x0600186E RID: 6254 RVA: 0x0000B528 File Offset: 0x00009728
		internal virtual bool IsList
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x0600186F RID: 6255 RVA: 0x0008E950 File Offset: 0x0008CB50
		// (set) Token: 0x06001870 RID: 6256 RVA: 0x0008E958 File Offset: 0x0008CB58
		internal bool IsReference
		{
			get
			{
				return this.reference;
			}
			set
			{
				this.reference = value;
			}
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06001871 RID: 6257 RVA: 0x0008E961 File Offset: 0x0008CB61
		internal bool IsAnonymousType
		{
			get
			{
				return this.typeName == null || this.typeName.Length == 0;
			}
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06001872 RID: 6258 RVA: 0x0008E97B File Offset: 0x0008CB7B
		internal virtual string DefaultElementName
		{
			get
			{
				if (!this.IsAnonymousType)
				{
					return this.typeName;
				}
				return XmlConvert.EncodeLocalName(this.typeDesc.Name);
			}
		}

		// Token: 0x06001873 RID: 6259 RVA: 0x0008E99C File Offset: 0x0008CB9C
		protected TypeMapping()
		{
		}

		// Token: 0x040018C2 RID: 6338
		private TypeDesc typeDesc;

		// Token: 0x040018C3 RID: 6339
		private string typeNs;

		// Token: 0x040018C4 RID: 6340
		private string typeName;

		// Token: 0x040018C5 RID: 6341
		private bool referencedByElement;

		// Token: 0x040018C6 RID: 6342
		private bool referencedByTopLevelElement;

		// Token: 0x040018C7 RID: 6343
		private bool includeInSchema = true;

		// Token: 0x040018C8 RID: 6344
		private bool reference;
	}
}
