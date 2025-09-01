using System;
using System.Runtime.Serialization;

namespace IKVM.Reflection
{
	// Token: 0x02000041 RID: 65
	[Serializable]
	public sealed class MissingModuleException : InvalidOperationException
	{
		// Token: 0x0600029B RID: 667 RVA: 0x00009B52 File Offset: 0x00007D52
		internal MissingModuleException(MissingModule module) : base("Module from missing assembly '" + module.Assembly.FullName + "' does not support the requested operation.")
		{
			this.module = module;
		}

		// Token: 0x0600029C RID: 668 RVA: 0x00009B40 File Offset: 0x00007D40
		private MissingModuleException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x0600029D RID: 669 RVA: 0x00009B7B File Offset: 0x00007D7B
		public Module Module
		{
			get
			{
				return this.module;
			}
		}

		// Token: 0x0400016D RID: 365
		[NonSerialized]
		private readonly MissingModule module;
	}
}
