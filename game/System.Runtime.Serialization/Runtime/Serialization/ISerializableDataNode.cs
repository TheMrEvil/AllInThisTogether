using System;
using System.Collections.Generic;

namespace System.Runtime.Serialization
{
	// Token: 0x020000DB RID: 219
	internal class ISerializableDataNode : DataNode<object>
	{
		// Token: 0x06000C81 RID: 3201 RVA: 0x00032F91 File Offset: 0x00031191
		internal ISerializableDataNode()
		{
			this.dataType = Globals.TypeOfISerializableDataNode;
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000C82 RID: 3202 RVA: 0x00032FA4 File Offset: 0x000311A4
		// (set) Token: 0x06000C83 RID: 3203 RVA: 0x00032FAC File Offset: 0x000311AC
		internal string FactoryTypeName
		{
			get
			{
				return this.factoryTypeName;
			}
			set
			{
				this.factoryTypeName = value;
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000C84 RID: 3204 RVA: 0x00032FB5 File Offset: 0x000311B5
		// (set) Token: 0x06000C85 RID: 3205 RVA: 0x00032FBD File Offset: 0x000311BD
		internal string FactoryTypeNamespace
		{
			get
			{
				return this.factoryTypeNamespace;
			}
			set
			{
				this.factoryTypeNamespace = value;
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000C86 RID: 3206 RVA: 0x00032FC6 File Offset: 0x000311C6
		// (set) Token: 0x06000C87 RID: 3207 RVA: 0x00032FCE File Offset: 0x000311CE
		internal IList<ISerializableDataMember> Members
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

		// Token: 0x06000C88 RID: 3208 RVA: 0x00032FD7 File Offset: 0x000311D7
		public override void GetData(ElementData element)
		{
			base.GetData(element);
			if (this.FactoryTypeName != null)
			{
				base.AddQualifiedNameAttribute(element, "z", "FactoryType", "http://schemas.microsoft.com/2003/10/Serialization/", this.FactoryTypeName, this.FactoryTypeNamespace);
			}
		}

		// Token: 0x06000C89 RID: 3209 RVA: 0x0003300C File Offset: 0x0003120C
		public override void Clear()
		{
			base.Clear();
			this.members = null;
			this.factoryTypeName = (this.factoryTypeNamespace = null);
		}

		// Token: 0x0400052E RID: 1326
		private string factoryTypeName;

		// Token: 0x0400052F RID: 1327
		private string factoryTypeNamespace;

		// Token: 0x04000530 RID: 1328
		private IList<ISerializableDataMember> members;
	}
}
