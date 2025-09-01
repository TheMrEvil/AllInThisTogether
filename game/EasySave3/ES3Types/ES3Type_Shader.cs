using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x020000B2 RID: 178
	[Preserve]
	[ES3Properties(new string[]
	{
		"name",
		"maximumLOD"
	})]
	public class ES3Type_Shader : ES3Type
	{
		// Token: 0x060003C7 RID: 967 RVA: 0x000171B7 File Offset: 0x000153B7
		public ES3Type_Shader() : base(typeof(Shader))
		{
			ES3Type_Shader.Instance = this;
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x000171D0 File Offset: 0x000153D0
		public override void Write(object obj, ES3Writer writer)
		{
			Shader shader = (Shader)obj;
			writer.WriteProperty("name", shader.name, ES3Type_string.Instance);
			writer.WriteProperty("maximumLOD", shader.maximumLOD, ES3Type_int.Instance);
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x00017218 File Offset: 0x00015418
		public override object Read<T>(ES3Reader reader)
		{
			Shader shader = Shader.Find(reader.ReadProperty<string>(ES3Type_string.Instance));
			if (shader == null)
			{
				shader = Shader.Find("Diffuse");
			}
			this.ReadInto<T>(reader, shader);
			return shader;
		}

		// Token: 0x060003CA RID: 970 RVA: 0x00017254 File Offset: 0x00015454
		public override void ReadInto<T>(ES3Reader reader, object obj)
		{
			Shader shader = (Shader)obj;
			foreach (object obj2 in reader.Properties)
			{
				string a = (string)obj2;
				if (!(a == "name"))
				{
					if (!(a == "maximumLOD"))
					{
						reader.Skip();
					}
					else
					{
						shader.maximumLOD = reader.Read<int>(ES3Type_int.Instance);
					}
				}
				else
				{
					shader.name = reader.Read<string>(ES3Type_string.Instance);
				}
			}
		}

		// Token: 0x04000105 RID: 261
		public static ES3Type Instance;
	}
}
