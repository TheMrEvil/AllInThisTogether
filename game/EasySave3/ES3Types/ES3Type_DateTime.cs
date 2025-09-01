using System;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000039 RID: 57
	[Preserve]
	public class ES3Type_DateTime : ES3Type
	{
		// Token: 0x06000288 RID: 648 RVA: 0x00009D47 File Offset: 0x00007F47
		public ES3Type_DateTime() : base(typeof(DateTime))
		{
			ES3Type_DateTime.Instance = this;
		}

		// Token: 0x06000289 RID: 649 RVA: 0x00009D60 File Offset: 0x00007F60
		public override void Write(object obj, ES3Writer writer)
		{
			writer.WriteProperty("ticks", ((DateTime)obj).Ticks, ES3Type_long.Instance);
		}

		// Token: 0x0600028A RID: 650 RVA: 0x00009D90 File Offset: 0x00007F90
		public override object Read<T>(ES3Reader reader)
		{
			reader.ReadPropertyName();
			return new DateTime(reader.Read<long>(ES3Type_long.Instance));
		}

		// Token: 0x0400008D RID: 141
		public static ES3Type Instance;
	}
}
