using System;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000051 RID: 81
	[Preserve]
	public class ES3Type_uint : ES3Type
	{
		// Token: 0x060002BB RID: 699 RVA: 0x0000A6D1 File Offset: 0x000088D1
		public ES3Type_uint() : base(typeof(uint))
		{
			this.isPrimitive = true;
			ES3Type_uint.Instance = this;
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000A6F0 File Offset: 0x000088F0
		public override void Write(object obj, ES3Writer writer)
		{
			writer.WritePrimitive((uint)obj);
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000A6FE File Offset: 0x000088FE
		public override object Read<T>(ES3Reader reader)
		{
			return (T)((object)reader.Read_uint());
		}

		// Token: 0x040000A6 RID: 166
		public static ES3Type Instance;
	}
}
