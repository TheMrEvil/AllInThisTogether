using System;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x0200004D RID: 77
	[Preserve]
	public class ES3Type_short : ES3Type
	{
		// Token: 0x060002B3 RID: 691 RVA: 0x0000A61E File Offset: 0x0000881E
		public ES3Type_short() : base(typeof(short))
		{
			this.isPrimitive = true;
			ES3Type_short.Instance = this;
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000A63D File Offset: 0x0000883D
		public override void Write(object obj, ES3Writer writer)
		{
			writer.WritePrimitive((short)obj);
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000A64B File Offset: 0x0000884B
		public override object Read<T>(ES3Reader reader)
		{
			return (T)((object)reader.Read_short());
		}

		// Token: 0x040000A2 RID: 162
		public static ES3Type Instance;
	}
}
