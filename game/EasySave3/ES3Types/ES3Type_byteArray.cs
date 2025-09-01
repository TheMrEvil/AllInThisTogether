using System;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000036 RID: 54
	[Preserve]
	public class ES3Type_byteArray : ES3Type
	{
		// Token: 0x06000281 RID: 641 RVA: 0x00009CA7 File Offset: 0x00007EA7
		public ES3Type_byteArray() : base(typeof(byte[]))
		{
			this.isPrimitive = true;
			ES3Type_byteArray.Instance = this;
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00009CC6 File Offset: 0x00007EC6
		public override void Write(object obj, ES3Writer writer)
		{
			writer.WritePrimitive((byte[])obj);
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00009CD4 File Offset: 0x00007ED4
		public override object Read<T>(ES3Reader reader)
		{
			return (T)((object)reader.Read_byteArray());
		}

		// Token: 0x0400008A RID: 138
		public static ES3Type Instance;
	}
}
