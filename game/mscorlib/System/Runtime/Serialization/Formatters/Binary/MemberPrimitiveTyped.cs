using System;
using System.Diagnostics;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x0200069E RID: 1694
	internal sealed class MemberPrimitiveTyped : IStreamable
	{
		// Token: 0x06003E4C RID: 15948 RVA: 0x0000259F File Offset: 0x0000079F
		internal MemberPrimitiveTyped()
		{
		}

		// Token: 0x06003E4D RID: 15949 RVA: 0x000D703D File Offset: 0x000D523D
		internal void Set(InternalPrimitiveTypeE primitiveTypeEnum, object value)
		{
			this.primitiveTypeEnum = primitiveTypeEnum;
			this.value = value;
		}

		// Token: 0x06003E4E RID: 15950 RVA: 0x000D704D File Offset: 0x000D524D
		public void Write(__BinaryWriter sout)
		{
			sout.WriteByte(8);
			sout.WriteByte((byte)this.primitiveTypeEnum);
			sout.WriteValue(this.primitiveTypeEnum, this.value);
		}

		// Token: 0x06003E4F RID: 15951 RVA: 0x000D7075 File Offset: 0x000D5275
		[SecurityCritical]
		public void Read(__BinaryParser input)
		{
			this.primitiveTypeEnum = (InternalPrimitiveTypeE)input.ReadByte();
			this.value = input.ReadValue(this.primitiveTypeEnum);
		}

		// Token: 0x06003E50 RID: 15952 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public void Dump()
		{
		}

		// Token: 0x06003E51 RID: 15953 RVA: 0x000D6165 File Offset: 0x000D4365
		[Conditional("_LOGGING")]
		private void DumpInternal()
		{
			BCLDebug.CheckEnabled("BINARY");
		}

		// Token: 0x04002861 RID: 10337
		internal InternalPrimitiveTypeE primitiveTypeEnum;

		// Token: 0x04002862 RID: 10338
		internal object value;
	}
}
