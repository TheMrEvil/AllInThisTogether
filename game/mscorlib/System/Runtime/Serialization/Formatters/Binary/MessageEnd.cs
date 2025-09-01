using System;
using System.Diagnostics;
using System.IO;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020006A5 RID: 1701
	internal sealed class MessageEnd : IStreamable
	{
		// Token: 0x06003E77 RID: 15991 RVA: 0x0000259F File Offset: 0x0000079F
		internal MessageEnd()
		{
		}

		// Token: 0x06003E78 RID: 15992 RVA: 0x000D79C0 File Offset: 0x000D5BC0
		public void Write(__BinaryWriter sout)
		{
			sout.WriteByte(11);
		}

		// Token: 0x06003E79 RID: 15993 RVA: 0x00004BF9 File Offset: 0x00002DF9
		[SecurityCritical]
		public void Read(__BinaryParser input)
		{
		}

		// Token: 0x06003E7A RID: 15994 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public void Dump()
		{
		}

		// Token: 0x06003E7B RID: 15995 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public void Dump(Stream sout)
		{
		}

		// Token: 0x06003E7C RID: 15996 RVA: 0x000D79CA File Offset: 0x000D5BCA
		[Conditional("_LOGGING")]
		private void DumpInternal(Stream sout)
		{
			if (BCLDebug.CheckEnabled("BINARY") && sout != null && sout.CanSeek)
			{
				long length = sout.Length;
			}
		}
	}
}
