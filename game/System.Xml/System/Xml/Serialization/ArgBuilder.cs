using System;

namespace System.Xml.Serialization
{
	// Token: 0x0200026A RID: 618
	internal class ArgBuilder
	{
		// Token: 0x060017A5 RID: 6053 RVA: 0x0008B2B9 File Offset: 0x000894B9
		internal ArgBuilder(string name, int index, Type argType)
		{
			this.Name = name;
			this.Index = index;
			this.ArgType = argType;
		}

		// Token: 0x0400186B RID: 6251
		internal string Name;

		// Token: 0x0400186C RID: 6252
		internal int Index;

		// Token: 0x0400186D RID: 6253
		internal Type ArgType;
	}
}
