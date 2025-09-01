using System;
using System.IO;

namespace Mono.CompilerServices.SymbolWriter
{
	// Token: 0x0200030D RID: 781
	internal class MyBinaryReader : BinaryReader
	{
		// Token: 0x060024C6 RID: 9414 RVA: 0x000AFC6C File Offset: 0x000ADE6C
		public MyBinaryReader(Stream stream) : base(stream)
		{
		}

		// Token: 0x060024C7 RID: 9415 RVA: 0x000AFC75 File Offset: 0x000ADE75
		public int ReadLeb128()
		{
			return base.Read7BitEncodedInt();
		}

		// Token: 0x060024C8 RID: 9416 RVA: 0x000AFC80 File Offset: 0x000ADE80
		public string ReadString(int offset)
		{
			long position = this.BaseStream.Position;
			this.BaseStream.Position = (long)offset;
			string result = this.ReadString();
			this.BaseStream.Position = position;
			return result;
		}
	}
}
