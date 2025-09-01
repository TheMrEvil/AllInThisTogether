using System;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000035 RID: 53
	[Preserve]
	public class ES3Type_byte : ES3Type
	{
		// Token: 0x0600027E RID: 638 RVA: 0x00009C63 File Offset: 0x00007E63
		public ES3Type_byte() : base(typeof(byte))
		{
			this.isPrimitive = true;
			ES3Type_byte.Instance = this;
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00009C82 File Offset: 0x00007E82
		public override void Write(object obj, ES3Writer writer)
		{
			writer.WritePrimitive((byte)obj);
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00009C90 File Offset: 0x00007E90
		public override object Read<T>(ES3Reader reader)
		{
			return (T)((object)reader.Read_byte());
		}

		// Token: 0x04000089 RID: 137
		public static ES3Type Instance;
	}
}
