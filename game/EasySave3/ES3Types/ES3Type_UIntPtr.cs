using System;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000053 RID: 83
	[Preserve]
	public class ES3Type_UIntPtr : ES3Type
	{
		// Token: 0x060002BF RID: 703 RVA: 0x0000A732 File Offset: 0x00008932
		public ES3Type_UIntPtr() : base(typeof(UIntPtr))
		{
			this.isPrimitive = true;
			ES3Type_UIntPtr.Instance = this;
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000A751 File Offset: 0x00008951
		public override void Write(object obj, ES3Writer writer)
		{
			writer.WritePrimitive((ulong)obj);
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000A75F File Offset: 0x0000895F
		public override object Read<T>(ES3Reader reader)
		{
			return reader.Read_ulong();
		}

		// Token: 0x040000A8 RID: 168
		public static ES3Type Instance;
	}
}
