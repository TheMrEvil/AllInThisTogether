using System;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000049 RID: 73
	[Preserve]
	public class ES3Type_long : ES3Type
	{
		// Token: 0x060002AB RID: 683 RVA: 0x0000A55C File Offset: 0x0000875C
		public ES3Type_long() : base(typeof(long))
		{
			this.isPrimitive = true;
			ES3Type_long.Instance = this;
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0000A57B File Offset: 0x0000877B
		public override void Write(object obj, ES3Writer writer)
		{
			writer.WritePrimitive((long)obj);
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0000A589 File Offset: 0x00008789
		public override object Read<T>(ES3Reader reader)
		{
			return (T)((object)reader.Read_long());
		}

		// Token: 0x0400009E RID: 158
		public static ES3Type Instance;
	}
}
