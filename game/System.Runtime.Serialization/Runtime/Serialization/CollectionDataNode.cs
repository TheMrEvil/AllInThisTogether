using System;
using System.Collections.Generic;
using System.Globalization;

namespace System.Runtime.Serialization
{
	// Token: 0x020000D9 RID: 217
	internal class CollectionDataNode : DataNode<Array>
	{
		// Token: 0x06000C6E RID: 3182 RVA: 0x00032E7B File Offset: 0x0003107B
		internal CollectionDataNode()
		{
			this.dataType = Globals.TypeOfCollectionDataNode;
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000C6F RID: 3183 RVA: 0x00032E95 File Offset: 0x00031095
		// (set) Token: 0x06000C70 RID: 3184 RVA: 0x00032E9D File Offset: 0x0003109D
		internal IList<IDataNode> Items
		{
			get
			{
				return this.items;
			}
			set
			{
				this.items = value;
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000C71 RID: 3185 RVA: 0x00032EA6 File Offset: 0x000310A6
		// (set) Token: 0x06000C72 RID: 3186 RVA: 0x00032EAE File Offset: 0x000310AE
		internal string ItemName
		{
			get
			{
				return this.itemName;
			}
			set
			{
				this.itemName = value;
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000C73 RID: 3187 RVA: 0x00032EB7 File Offset: 0x000310B7
		// (set) Token: 0x06000C74 RID: 3188 RVA: 0x00032EBF File Offset: 0x000310BF
		internal string ItemNamespace
		{
			get
			{
				return this.itemNamespace;
			}
			set
			{
				this.itemNamespace = value;
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000C75 RID: 3189 RVA: 0x00032EC8 File Offset: 0x000310C8
		// (set) Token: 0x06000C76 RID: 3190 RVA: 0x00032ED0 File Offset: 0x000310D0
		internal int Size
		{
			get
			{
				return this.size;
			}
			set
			{
				this.size = value;
			}
		}

		// Token: 0x06000C77 RID: 3191 RVA: 0x00032EDC File Offset: 0x000310DC
		public override void GetData(ElementData element)
		{
			base.GetData(element);
			element.AddAttribute("z", "http://schemas.microsoft.com/2003/10/Serialization/", "Size", this.Size.ToString(NumberFormatInfo.InvariantInfo));
		}

		// Token: 0x06000C78 RID: 3192 RVA: 0x00032F18 File Offset: 0x00031118
		public override void Clear()
		{
			base.Clear();
			this.items = null;
			this.size = -1;
		}

		// Token: 0x04000527 RID: 1319
		private IList<IDataNode> items;

		// Token: 0x04000528 RID: 1320
		private string itemName;

		// Token: 0x04000529 RID: 1321
		private string itemNamespace;

		// Token: 0x0400052A RID: 1322
		private int size = -1;
	}
}
