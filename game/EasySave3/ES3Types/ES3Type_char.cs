using System;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000037 RID: 55
	[Preserve]
	public class ES3Type_char : ES3Type
	{
		// Token: 0x06000284 RID: 644 RVA: 0x00009CE6 File Offset: 0x00007EE6
		public ES3Type_char() : base(typeof(char))
		{
			this.isPrimitive = true;
			ES3Type_char.Instance = this;
		}

		// Token: 0x06000285 RID: 645 RVA: 0x00009D05 File Offset: 0x00007F05
		public override void Write(object obj, ES3Writer writer)
		{
			writer.WritePrimitive((char)obj);
		}

		// Token: 0x06000286 RID: 646 RVA: 0x00009D13 File Offset: 0x00007F13
		public override object Read<T>(ES3Reader reader)
		{
			return (T)((object)reader.Read_char());
		}

		// Token: 0x0400008B RID: 139
		public static ES3Type Instance;
	}
}
