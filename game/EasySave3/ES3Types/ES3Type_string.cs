using System;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x0200004F RID: 79
	[Preserve]
	public class ES3Type_string : ES3Type
	{
		// Token: 0x060002B7 RID: 695 RVA: 0x0000A67F File Offset: 0x0000887F
		public ES3Type_string() : base(typeof(string))
		{
			this.isPrimitive = true;
			ES3Type_string.Instance = this;
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000A69E File Offset: 0x0000889E
		public override void Write(object obj, ES3Writer writer)
		{
			writer.WritePrimitive((string)obj);
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000A6AC File Offset: 0x000088AC
		public override object Read<T>(ES3Reader reader)
		{
			return reader.Read_string();
		}

		// Token: 0x040000A4 RID: 164
		public static ES3Type Instance;
	}
}
