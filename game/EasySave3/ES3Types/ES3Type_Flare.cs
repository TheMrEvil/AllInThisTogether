using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000088 RID: 136
	[Preserve]
	[ES3Properties(new string[]
	{
		"hideFlags"
	})]
	public class ES3Type_Flare : ES3Type
	{
		// Token: 0x0600034B RID: 843 RVA: 0x00010892 File Offset: 0x0000EA92
		public ES3Type_Flare() : base(typeof(Flare))
		{
			ES3Type_Flare.Instance = this;
		}

		// Token: 0x0600034C RID: 844 RVA: 0x000108AC File Offset: 0x0000EAAC
		public override void Write(object obj, ES3Writer writer)
		{
			Flare flare = (Flare)obj;
			writer.WriteProperty("hideFlags", flare.hideFlags);
		}

		// Token: 0x0600034D RID: 845 RVA: 0x000108D8 File Offset: 0x0000EAD8
		public override object Read<T>(ES3Reader reader)
		{
			Flare flare = new Flare();
			this.ReadInto<T>(reader, flare);
			return flare;
		}

		// Token: 0x0600034E RID: 846 RVA: 0x000108F4 File Offset: 0x0000EAF4
		public override void ReadInto<T>(ES3Reader reader, object obj)
		{
			Flare flare = (Flare)obj;
			string a;
			while ((a = reader.ReadPropertyName()) != null)
			{
				if (a == "hideFlags")
				{
					flare.hideFlags = reader.Read<HideFlags>();
				}
				else
				{
					reader.Skip();
				}
			}
		}

		// Token: 0x040000D8 RID: 216
		public static ES3Type Instance;
	}
}
