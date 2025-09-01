using System;
using System.Globalization;

namespace System.Runtime.Serialization
{
	// Token: 0x020000D7 RID: 215
	internal class DataNode<T> : IDataNode
	{
		// Token: 0x06000C54 RID: 3156 RVA: 0x00032C23 File Offset: 0x00030E23
		internal DataNode()
		{
			this.dataType = typeof(T);
			this.isFinalValue = true;
		}

		// Token: 0x06000C55 RID: 3157 RVA: 0x00032C4D File Offset: 0x00030E4D
		internal DataNode(T value) : this()
		{
			this.value = value;
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000C56 RID: 3158 RVA: 0x00032C5C File Offset: 0x00030E5C
		public Type DataType
		{
			get
			{
				return this.dataType;
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000C57 RID: 3159 RVA: 0x00032C64 File Offset: 0x00030E64
		// (set) Token: 0x06000C58 RID: 3160 RVA: 0x00032C71 File Offset: 0x00030E71
		public object Value
		{
			get
			{
				return this.value;
			}
			set
			{
				this.value = (T)((object)value);
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000C59 RID: 3161 RVA: 0x00032C7F File Offset: 0x00030E7F
		// (set) Token: 0x06000C5A RID: 3162 RVA: 0x00032C87 File Offset: 0x00030E87
		bool IDataNode.IsFinalValue
		{
			get
			{
				return this.isFinalValue;
			}
			set
			{
				this.isFinalValue = value;
			}
		}

		// Token: 0x06000C5B RID: 3163 RVA: 0x00032C90 File Offset: 0x00030E90
		public T GetValue()
		{
			return this.value;
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000C5C RID: 3164 RVA: 0x00032C98 File Offset: 0x00030E98
		// (set) Token: 0x06000C5D RID: 3165 RVA: 0x00032CA0 File Offset: 0x00030EA0
		public string DataContractName
		{
			get
			{
				return this.dataContractName;
			}
			set
			{
				this.dataContractName = value;
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000C5E RID: 3166 RVA: 0x00032CA9 File Offset: 0x00030EA9
		// (set) Token: 0x06000C5F RID: 3167 RVA: 0x00032CB1 File Offset: 0x00030EB1
		public string DataContractNamespace
		{
			get
			{
				return this.dataContractNamespace;
			}
			set
			{
				this.dataContractNamespace = value;
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000C60 RID: 3168 RVA: 0x00032CBA File Offset: 0x00030EBA
		// (set) Token: 0x06000C61 RID: 3169 RVA: 0x00032CC2 File Offset: 0x00030EC2
		public string ClrTypeName
		{
			get
			{
				return this.clrTypeName;
			}
			set
			{
				this.clrTypeName = value;
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000C62 RID: 3170 RVA: 0x00032CCB File Offset: 0x00030ECB
		// (set) Token: 0x06000C63 RID: 3171 RVA: 0x00032CD3 File Offset: 0x00030ED3
		public string ClrAssemblyName
		{
			get
			{
				return this.clrAssemblyName;
			}
			set
			{
				this.clrAssemblyName = value;
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000C64 RID: 3172 RVA: 0x00032CDC File Offset: 0x00030EDC
		public bool PreservesReferences
		{
			get
			{
				return this.Id != Globals.NewObjectId;
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000C65 RID: 3173 RVA: 0x00032CEE File Offset: 0x00030EEE
		// (set) Token: 0x06000C66 RID: 3174 RVA: 0x00032CF6 File Offset: 0x00030EF6
		public string Id
		{
			get
			{
				return this.id;
			}
			set
			{
				this.id = value;
			}
		}

		// Token: 0x06000C67 RID: 3175 RVA: 0x00032D00 File Offset: 0x00030F00
		public virtual void GetData(ElementData element)
		{
			element.dataNode = this;
			element.attributeCount = 0;
			element.childElementIndex = 0;
			if (this.DataContractName != null)
			{
				this.AddQualifiedNameAttribute(element, "i", "type", "http://www.w3.org/2001/XMLSchema-instance", this.DataContractName, this.DataContractNamespace);
			}
			if (this.ClrTypeName != null)
			{
				element.AddAttribute("z", "http://schemas.microsoft.com/2003/10/Serialization/", "Type", this.ClrTypeName);
			}
			if (this.ClrAssemblyName != null)
			{
				element.AddAttribute("z", "http://schemas.microsoft.com/2003/10/Serialization/", "Assembly", this.ClrAssemblyName);
			}
		}

		// Token: 0x06000C68 RID: 3176 RVA: 0x00032D94 File Offset: 0x00030F94
		public virtual void Clear()
		{
			this.clrTypeName = (this.clrAssemblyName = null);
		}

		// Token: 0x06000C69 RID: 3177 RVA: 0x00032DB4 File Offset: 0x00030FB4
		internal void AddQualifiedNameAttribute(ElementData element, string elementPrefix, string elementName, string elementNs, string valueName, string valueNs)
		{
			string prefix = ExtensionDataReader.GetPrefix(valueNs);
			element.AddAttribute(elementPrefix, elementNs, elementName, string.Format(CultureInfo.InvariantCulture, "{0}:{1}", prefix, valueName));
			bool flag = false;
			if (element.attributes != null)
			{
				for (int i = 0; i < element.attributes.Length; i++)
				{
					AttributeData attributeData = element.attributes[i];
					if (attributeData != null && attributeData.prefix == "xmlns" && attributeData.localName == prefix)
					{
						flag = true;
						break;
					}
				}
			}
			if (!flag)
			{
				element.AddAttribute("xmlns", "http://www.w3.org/2000/xmlns/", prefix, valueNs);
			}
		}

		// Token: 0x0400051E RID: 1310
		protected Type dataType;

		// Token: 0x0400051F RID: 1311
		private T value;

		// Token: 0x04000520 RID: 1312
		private string dataContractName;

		// Token: 0x04000521 RID: 1313
		private string dataContractNamespace;

		// Token: 0x04000522 RID: 1314
		private string clrTypeName;

		// Token: 0x04000523 RID: 1315
		private string clrAssemblyName;

		// Token: 0x04000524 RID: 1316
		private string id = Globals.NewObjectId;

		// Token: 0x04000525 RID: 1317
		private bool isFinalValue;
	}
}
