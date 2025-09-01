using System;
using System.IO;

namespace Mono.CompilerServices.SymbolWriter
{
	// Token: 0x0200030C RID: 780
	internal sealed class MyBinaryWriter : BinaryWriter
	{
		// Token: 0x060024C4 RID: 9412 RVA: 0x000AFC5A File Offset: 0x000ADE5A
		public MyBinaryWriter(Stream stream) : base(stream)
		{
		}

		// Token: 0x060024C5 RID: 9413 RVA: 0x000AFC63 File Offset: 0x000ADE63
		public void WriteLeb128(int value)
		{
			base.Write7BitEncodedInt(value);
		}
	}
}
