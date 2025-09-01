using System;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000045 RID: 69
	[Preserve]
	public class ES3Type_int : ES3Type
	{
		// Token: 0x060002A3 RID: 675 RVA: 0x0000A490 File Offset: 0x00008690
		public ES3Type_int() : base(typeof(int))
		{
			this.isPrimitive = true;
			ES3Type_int.Instance = this;
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000A4AF File Offset: 0x000086AF
		public override void Write(object obj, ES3Writer writer)
		{
			writer.WritePrimitive((int)obj);
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000A4BD File Offset: 0x000086BD
		public override object Read<T>(ES3Reader reader)
		{
			return (T)((object)reader.Read_int());
		}

		// Token: 0x0400009A RID: 154
		public static ES3Type Instance;
	}
}
