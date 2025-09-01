using System;

namespace IKVM.Reflection.Emit
{
	// Token: 0x020000E6 RID: 230
	public sealed class LocalBuilder : LocalVariableInfo
	{
		// Token: 0x06000A61 RID: 2657 RVA: 0x00024104 File Offset: 0x00022304
		internal LocalBuilder(Type localType, int index, bool pinned) : base(index, localType, pinned)
		{
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x0002410F File Offset: 0x0002230F
		internal LocalBuilder(Type localType, int index, bool pinned, CustomModifiers customModifiers) : base(index, localType, pinned, customModifiers)
		{
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x0002411C File Offset: 0x0002231C
		public void SetLocalSymInfo(string name)
		{
			this.name = name;
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x00024125 File Offset: 0x00022325
		public void SetLocalSymInfo(string name, int startOffset, int endOffset)
		{
			this.name = name;
			this.startOffset = startOffset;
			this.endOffset = endOffset;
		}

		// Token: 0x0400049B RID: 1179
		internal string name;

		// Token: 0x0400049C RID: 1180
		internal int startOffset;

		// Token: 0x0400049D RID: 1181
		internal int endOffset;
	}
}
