using System;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000047 RID: 71
	[Preserve]
	public class ES3Type_IntPtr : ES3Type
	{
		// Token: 0x060002A7 RID: 679 RVA: 0x0000A4F1 File Offset: 0x000086F1
		public ES3Type_IntPtr() : base(typeof(IntPtr))
		{
			this.isPrimitive = true;
			ES3Type_IntPtr.Instance = this;
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000A510 File Offset: 0x00008710
		public override void Write(object obj, ES3Writer writer)
		{
			writer.WritePrimitive((long)((IntPtr)obj));
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000A523 File Offset: 0x00008723
		public override object Read<T>(ES3Reader reader)
		{
			return (T)((object)((IntPtr)reader.Read_long()));
		}

		// Token: 0x0400009C RID: 156
		public static ES3Type Instance;
	}
}
