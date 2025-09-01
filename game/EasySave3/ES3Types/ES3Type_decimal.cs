using System;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x0200003B RID: 59
	[Preserve]
	public class ES3Type_decimal : ES3Type
	{
		// Token: 0x0600028C RID: 652 RVA: 0x00009DCB File Offset: 0x00007FCB
		public ES3Type_decimal() : base(typeof(decimal))
		{
			this.isPrimitive = true;
			ES3Type_decimal.Instance = this;
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00009DEA File Offset: 0x00007FEA
		public override void Write(object obj, ES3Writer writer)
		{
			writer.WritePrimitive((decimal)obj);
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00009DF8 File Offset: 0x00007FF8
		public override object Read<T>(ES3Reader reader)
		{
			return (T)((object)reader.Read_decimal());
		}

		// Token: 0x0400008F RID: 143
		public static ES3Type Instance;
	}
}
