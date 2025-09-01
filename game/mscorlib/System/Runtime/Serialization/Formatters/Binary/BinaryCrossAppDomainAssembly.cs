using System;
using System.Diagnostics;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000697 RID: 1687
	internal sealed class BinaryCrossAppDomainAssembly : IStreamable
	{
		// Token: 0x06003E22 RID: 15906 RVA: 0x0000259F File Offset: 0x0000079F
		internal BinaryCrossAppDomainAssembly()
		{
		}

		// Token: 0x06003E23 RID: 15907 RVA: 0x000D637A File Offset: 0x000D457A
		public void Write(__BinaryWriter sout)
		{
			sout.WriteByte(20);
			sout.WriteInt32(this.assemId);
			sout.WriteInt32(this.assemblyIndex);
		}

		// Token: 0x06003E24 RID: 15908 RVA: 0x000D639C File Offset: 0x000D459C
		[SecurityCritical]
		public void Read(__BinaryParser input)
		{
			this.assemId = input.ReadInt32();
			this.assemblyIndex = input.ReadInt32();
		}

		// Token: 0x06003E25 RID: 15909 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public void Dump()
		{
		}

		// Token: 0x06003E26 RID: 15910 RVA: 0x000D6165 File Offset: 0x000D4365
		[Conditional("_LOGGING")]
		private void DumpInternal()
		{
			BCLDebug.CheckEnabled("BINARY");
		}

		// Token: 0x0400283F RID: 10303
		internal int assemId;

		// Token: 0x04002840 RID: 10304
		internal int assemblyIndex;
	}
}
