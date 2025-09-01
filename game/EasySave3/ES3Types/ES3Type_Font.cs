using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x0200008A RID: 138
	[Preserve]
	[ES3Properties(new string[]
	{
		"material",
		"name"
	})]
	public class ES3Type_Font : ES3UnityObjectType
	{
		// Token: 0x06000350 RID: 848 RVA: 0x00010952 File Offset: 0x0000EB52
		public ES3Type_Font() : base(typeof(Font))
		{
			ES3Type_Font.Instance = this;
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0001096C File Offset: 0x0000EB6C
		protected override void WriteUnityObject(object obj, ES3Writer writer)
		{
			Font font = (Font)obj;
			writer.WriteProperty("name", font.name, ES3Type_string.Instance);
			writer.WriteProperty("material", font.material);
		}

		// Token: 0x06000352 RID: 850 RVA: 0x000109A8 File Offset: 0x0000EBA8
		protected override void ReadUnityObject<T>(ES3Reader reader, object obj)
		{
			Font font = (Font)obj;
			string a;
			while ((a = reader.ReadPropertyName()) != null)
			{
				if (a == "material")
				{
					font.material = reader.Read<Material>(ES3Type_Material.Instance);
				}
				else
				{
					reader.Skip();
				}
			}
		}

		// Token: 0x06000353 RID: 851 RVA: 0x000109F0 File Offset: 0x0000EBF0
		protected override object ReadUnityObject<T>(ES3Reader reader)
		{
			Font font = new Font(reader.ReadProperty<string>(ES3Type_string.Instance));
			this.ReadObject<T>(reader, font);
			return font;
		}

		// Token: 0x040000DA RID: 218
		public static ES3Type Instance;
	}
}
