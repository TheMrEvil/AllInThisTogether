using System;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x0200004B RID: 75
	[Preserve]
	public class ES3Type_sbyte : ES3Type
	{
		// Token: 0x060002AF RID: 687 RVA: 0x0000A5BD File Offset: 0x000087BD
		public ES3Type_sbyte() : base(typeof(sbyte))
		{
			this.isPrimitive = true;
			ES3Type_sbyte.Instance = this;
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000A5DC File Offset: 0x000087DC
		public override void Write(object obj, ES3Writer writer)
		{
			writer.WritePrimitive((sbyte)obj);
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000A5EA File Offset: 0x000087EA
		public override object Read<T>(ES3Reader reader)
		{
			return (T)((object)reader.Read_sbyte());
		}

		// Token: 0x040000A0 RID: 160
		public static ES3Type Instance;
	}
}
