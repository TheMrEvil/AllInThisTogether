using System;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000043 RID: 67
	[Preserve]
	public class ES3Type_float : ES3Type
	{
		// Token: 0x0600029F RID: 671 RVA: 0x0000A42F File Offset: 0x0000862F
		public ES3Type_float() : base(typeof(float))
		{
			this.isPrimitive = true;
			ES3Type_float.Instance = this;
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000A44E File Offset: 0x0000864E
		public override void Write(object obj, ES3Writer writer)
		{
			writer.WritePrimitive((float)obj);
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000A45C File Offset: 0x0000865C
		public override object Read<T>(ES3Reader reader)
		{
			return (T)((object)reader.Read_float());
		}

		// Token: 0x04000098 RID: 152
		public static ES3Type Instance;
	}
}
