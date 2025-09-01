using System;
using System.Collections.Generic;

namespace System.Runtime.Serialization
{
	// Token: 0x020000D8 RID: 216
	internal class ClassDataNode : DataNode<object>
	{
		// Token: 0x06000C6A RID: 3178 RVA: 0x00032E48 File Offset: 0x00031048
		internal ClassDataNode()
		{
			this.dataType = Globals.TypeOfClassDataNode;
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000C6B RID: 3179 RVA: 0x00032E5B File Offset: 0x0003105B
		// (set) Token: 0x06000C6C RID: 3180 RVA: 0x00032E63 File Offset: 0x00031063
		internal IList<ExtensionDataMember> Members
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

		// Token: 0x06000C6D RID: 3181 RVA: 0x00032E6C File Offset: 0x0003106C
		public override void Clear()
		{
			base.Clear();
			this.members = null;
		}

		// Token: 0x04000526 RID: 1318
		private IList<ExtensionDataMember> members;
	}
}
