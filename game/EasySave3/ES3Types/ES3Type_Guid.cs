using System;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000094 RID: 148
	[Preserve]
	[ES3Properties(new string[]
	{
		"value"
	})]
	public class ES3Type_Guid : ES3Type
	{
		// Token: 0x06000370 RID: 880 RVA: 0x00011869 File Offset: 0x0000FA69
		public ES3Type_Guid() : base(typeof(Guid))
		{
			ES3Type_Guid.Instance = this;
		}

		// Token: 0x06000371 RID: 881 RVA: 0x00011884 File Offset: 0x0000FA84
		public override void Write(object obj, ES3Writer writer)
		{
			writer.WriteProperty("value", ((Guid)obj).ToString(), ES3Type_string.Instance);
		}

		// Token: 0x06000372 RID: 882 RVA: 0x000118B5 File Offset: 0x0000FAB5
		public override object Read<T>(ES3Reader reader)
		{
			return Guid.Parse(reader.ReadProperty<string>(ES3Type_string.Instance));
		}

		// Token: 0x040000E7 RID: 231
		public static ES3Type Instance;
	}
}
