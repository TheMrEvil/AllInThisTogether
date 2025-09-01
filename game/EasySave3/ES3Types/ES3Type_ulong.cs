using System;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000055 RID: 85
	[Preserve]
	public class ES3Type_ulong : ES3Type
	{
		// Token: 0x060002C3 RID: 707 RVA: 0x0000A789 File Offset: 0x00008989
		public ES3Type_ulong() : base(typeof(ulong))
		{
			this.isPrimitive = true;
			ES3Type_ulong.Instance = this;
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000A7A8 File Offset: 0x000089A8
		public override void Write(object obj, ES3Writer writer)
		{
			writer.WritePrimitive((ulong)obj);
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000A7B6 File Offset: 0x000089B6
		public override object Read<T>(ES3Reader reader)
		{
			return (T)((object)reader.Read_ulong());
		}

		// Token: 0x040000AA RID: 170
		public static ES3Type Instance;
	}
}
