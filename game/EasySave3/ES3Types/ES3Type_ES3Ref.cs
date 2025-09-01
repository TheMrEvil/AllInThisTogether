using System;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000040 RID: 64
	[Preserve]
	public class ES3Type_ES3Ref : ES3Type
	{
		// Token: 0x06000297 RID: 663 RVA: 0x0000A374 File Offset: 0x00008574
		public ES3Type_ES3Ref() : base(typeof(long))
		{
			this.isPrimitive = true;
			ES3Type_ES3Ref.Instance = this;
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000A394 File Offset: 0x00008594
		public override void Write(object obj, ES3Writer writer)
		{
			writer.WritePrimitive(((long)obj).ToString());
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000A3B5 File Offset: 0x000085B5
		public override object Read<T>(ES3Reader reader)
		{
			return (T)((object)new ES3Ref(reader.Read_ref()));
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000A3CC File Offset: 0x000085CC
		// Note: this type is marked as 'beforefieldinit'.
		static ES3Type_ES3Ref()
		{
		}

		// Token: 0x04000095 RID: 149
		public static ES3Type Instance = new ES3Type_ES3Ref();
	}
}
