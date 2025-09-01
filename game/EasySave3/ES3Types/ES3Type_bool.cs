using System;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000033 RID: 51
	[Preserve]
	public class ES3Type_bool : ES3Type
	{
		// Token: 0x0600027A RID: 634 RVA: 0x00009C02 File Offset: 0x00007E02
		public ES3Type_bool() : base(typeof(bool))
		{
			this.isPrimitive = true;
			ES3Type_bool.Instance = this;
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00009C21 File Offset: 0x00007E21
		public override void Write(object obj, ES3Writer writer)
		{
			writer.WritePrimitive((bool)obj);
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00009C2F File Offset: 0x00007E2F
		public override object Read<T>(ES3Reader reader)
		{
			return (T)((object)reader.Read_bool());
		}

		// Token: 0x04000087 RID: 135
		public static ES3Type Instance;
	}
}
