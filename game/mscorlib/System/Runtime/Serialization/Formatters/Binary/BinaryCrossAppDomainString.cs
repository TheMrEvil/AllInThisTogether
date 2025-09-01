using System;
using System.Diagnostics;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x0200069C RID: 1692
	internal sealed class BinaryCrossAppDomainString : IStreamable
	{
		// Token: 0x06003E42 RID: 15938 RVA: 0x0000259F File Offset: 0x0000079F
		internal BinaryCrossAppDomainString()
		{
		}

		// Token: 0x06003E43 RID: 15939 RVA: 0x000D6FDD File Offset: 0x000D51DD
		public void Write(__BinaryWriter sout)
		{
			sout.WriteByte(19);
			sout.WriteInt32(this.objectId);
			sout.WriteInt32(this.value);
		}

		// Token: 0x06003E44 RID: 15940 RVA: 0x000D6FFF File Offset: 0x000D51FF
		[SecurityCritical]
		public void Read(__BinaryParser input)
		{
			this.objectId = input.ReadInt32();
			this.value = input.ReadInt32();
		}

		// Token: 0x06003E45 RID: 15941 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public void Dump()
		{
		}

		// Token: 0x06003E46 RID: 15942 RVA: 0x000D6165 File Offset: 0x000D4365
		[Conditional("_LOGGING")]
		private void DumpInternal()
		{
			BCLDebug.CheckEnabled("BINARY");
		}

		// Token: 0x0400285E RID: 10334
		internal int objectId;

		// Token: 0x0400285F RID: 10335
		internal int value;
	}
}
