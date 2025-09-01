using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000099 RID: 153
	[Preserve]
	[ES3Properties(new string[]
	{
		"colorKeys",
		"alphaKeys",
		"mode"
	})]
	public class ES3Type_LayerMask : ES3Type
	{
		// Token: 0x0600037C RID: 892 RVA: 0x00011B4D File Offset: 0x0000FD4D
		public ES3Type_LayerMask() : base(typeof(LayerMask))
		{
			ES3Type_LayerMask.Instance = this;
		}

		// Token: 0x0600037D RID: 893 RVA: 0x00011B68 File Offset: 0x0000FD68
		public override void Write(object obj, ES3Writer writer)
		{
			writer.WriteProperty("value", ((LayerMask)obj).value, ES3Type_int.Instance);
		}

		// Token: 0x0600037E RID: 894 RVA: 0x00011B98 File Offset: 0x0000FD98
		public override object Read<T>(ES3Reader reader)
		{
			LayerMask layerMask = default(LayerMask);
			string a;
			while ((a = reader.ReadPropertyName()) != null)
			{
				if (a == "value")
				{
					layerMask = reader.Read<int>(ES3Type_int.Instance);
				}
				else
				{
					reader.Skip();
				}
			}
			return layerMask;
		}

		// Token: 0x040000EC RID: 236
		public static ES3Type Instance;
	}
}
