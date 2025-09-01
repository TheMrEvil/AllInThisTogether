using System;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000057 RID: 87
	[Preserve]
	public class ES3Type_ushort : ES3Type
	{
		// Token: 0x060002C7 RID: 711 RVA: 0x0000A7EA File Offset: 0x000089EA
		public ES3Type_ushort() : base(typeof(ushort))
		{
			this.isPrimitive = true;
			ES3Type_ushort.Instance = this;
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000A809 File Offset: 0x00008A09
		public override void Write(object obj, ES3Writer writer)
		{
			writer.WritePrimitive((ushort)obj);
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000A817 File Offset: 0x00008A17
		public override object Read<T>(ES3Reader reader)
		{
			return (T)((object)reader.Read_ushort());
		}

		// Token: 0x040000AC RID: 172
		public static ES3Type Instance;
	}
}
