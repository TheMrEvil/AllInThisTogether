using System;
using System.Runtime.Serialization;

namespace IKVM.Reflection
{
	// Token: 0x02000040 RID: 64
	[Serializable]
	public sealed class MissingAssemblyException : InvalidOperationException
	{
		// Token: 0x06000298 RID: 664 RVA: 0x00009B1C File Offset: 0x00007D1C
		internal MissingAssemblyException(MissingAssembly assembly) : base("Assembly '" + assembly.FullName + "' is a missing assembly and does not support the requested operation.")
		{
			this.assembly = assembly;
		}

		// Token: 0x06000299 RID: 665 RVA: 0x00009B40 File Offset: 0x00007D40
		private MissingAssemblyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x0600029A RID: 666 RVA: 0x00009B4A File Offset: 0x00007D4A
		public Assembly Assembly
		{
			get
			{
				return this.assembly;
			}
		}

		// Token: 0x0400016C RID: 364
		[NonSerialized]
		private readonly MissingAssembly assembly;
	}
}
