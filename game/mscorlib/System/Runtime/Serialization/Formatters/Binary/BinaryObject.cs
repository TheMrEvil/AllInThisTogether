using System;
using System.Diagnostics;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000698 RID: 1688
	internal sealed class BinaryObject : IStreamable
	{
		// Token: 0x06003E27 RID: 15911 RVA: 0x0000259F File Offset: 0x0000079F
		internal BinaryObject()
		{
		}

		// Token: 0x06003E28 RID: 15912 RVA: 0x000D63B6 File Offset: 0x000D45B6
		internal void Set(int objectId, int mapId)
		{
			this.objectId = objectId;
			this.mapId = mapId;
		}

		// Token: 0x06003E29 RID: 15913 RVA: 0x000D63C6 File Offset: 0x000D45C6
		public void Write(__BinaryWriter sout)
		{
			sout.WriteByte(1);
			sout.WriteInt32(this.objectId);
			sout.WriteInt32(this.mapId);
		}

		// Token: 0x06003E2A RID: 15914 RVA: 0x000D63E7 File Offset: 0x000D45E7
		[SecurityCritical]
		public void Read(__BinaryParser input)
		{
			this.objectId = input.ReadInt32();
			this.mapId = input.ReadInt32();
		}

		// Token: 0x06003E2B RID: 15915 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public void Dump()
		{
		}

		// Token: 0x06003E2C RID: 15916 RVA: 0x000D6165 File Offset: 0x000D4365
		[Conditional("_LOGGING")]
		private void DumpInternal()
		{
			BCLDebug.CheckEnabled("BINARY");
		}

		// Token: 0x04002841 RID: 10305
		internal int objectId;

		// Token: 0x04002842 RID: 10306
		internal int mapId;
	}
}
