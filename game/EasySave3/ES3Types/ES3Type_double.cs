using System;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x0200003D RID: 61
	[Preserve]
	public class ES3Type_double : ES3Type
	{
		// Token: 0x06000290 RID: 656 RVA: 0x00009E2C File Offset: 0x0000802C
		public ES3Type_double() : base(typeof(double))
		{
			this.isPrimitive = true;
			ES3Type_double.Instance = this;
		}

		// Token: 0x06000291 RID: 657 RVA: 0x00009E4B File Offset: 0x0000804B
		public override void Write(object obj, ES3Writer writer)
		{
			writer.WritePrimitive((double)obj);
		}

		// Token: 0x06000292 RID: 658 RVA: 0x00009E59 File Offset: 0x00008059
		public override object Read<T>(ES3Reader reader)
		{
			return (T)((object)reader.Read_double());
		}

		// Token: 0x04000091 RID: 145
		public static ES3Type Instance;
	}
}
