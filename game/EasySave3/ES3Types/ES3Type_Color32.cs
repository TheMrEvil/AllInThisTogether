using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000082 RID: 130
	[Preserve]
	[ES3Properties(new string[]
	{
		"r",
		"g",
		"b",
		"a"
	})]
	public class ES3Type_Color32 : ES3Type
	{
		// Token: 0x06000336 RID: 822 RVA: 0x00010245 File Offset: 0x0000E445
		public ES3Type_Color32() : base(typeof(Color32))
		{
			ES3Type_Color32.Instance = this;
		}

		// Token: 0x06000337 RID: 823 RVA: 0x00010260 File Offset: 0x0000E460
		public override void Write(object obj, ES3Writer writer)
		{
			Color32 color = (Color32)obj;
			writer.WriteProperty("r", color.r, ES3Type_byte.Instance);
			writer.WriteProperty("g", color.g, ES3Type_byte.Instance);
			writer.WriteProperty("b", color.b, ES3Type_byte.Instance);
			writer.WriteProperty("a", color.a, ES3Type_byte.Instance);
		}

		// Token: 0x06000338 RID: 824 RVA: 0x000102E0 File Offset: 0x0000E4E0
		public override object Read<T>(ES3Reader reader)
		{
			return new Color32(reader.ReadProperty<byte>(ES3Type_byte.Instance), reader.ReadProperty<byte>(ES3Type_byte.Instance), reader.ReadProperty<byte>(ES3Type_byte.Instance), reader.ReadProperty<byte>(ES3Type_byte.Instance));
		}

		// Token: 0x06000339 RID: 825 RVA: 0x00010318 File Offset: 0x0000E518
		public static bool Equals(Color32 a, Color32 b)
		{
			return a.r == b.r && a.g == b.g && a.b == b.b && a.a == b.a;
		}

		// Token: 0x040000D2 RID: 210
		public static ES3Type Instance;
	}
}
