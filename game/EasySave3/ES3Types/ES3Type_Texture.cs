using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x020000BD RID: 189
	[Preserve]
	[ES3Properties(new string[]
	{
		"filterMode",
		"anisoLevel",
		"wrapMode",
		"mipMapBias",
		"rawTextureData"
	})]
	public class ES3Type_Texture : ES3Type
	{
		// Token: 0x060003E8 RID: 1000 RVA: 0x00019579 File Offset: 0x00017779
		public ES3Type_Texture() : base(typeof(Texture))
		{
			ES3Type_Texture.Instance = this;
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x00019594 File Offset: 0x00017794
		public override void Write(object obj, ES3Writer writer)
		{
			if (obj.GetType() == typeof(Texture2D))
			{
				ES3Type_Texture2D.Instance.Write(obj, writer);
				return;
			}
			string str = "Textures of type ";
			Type type = obj.GetType();
			throw new NotSupportedException(str + ((type != null) ? type.ToString() : null) + " are not currently supported.");
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x000195EC File Offset: 0x000177EC
		public override void ReadInto<T>(ES3Reader reader, object obj)
		{
			if (obj.GetType() == typeof(Texture2D))
			{
				ES3Type_Texture2D.Instance.ReadInto<T>(reader, obj);
				return;
			}
			string str = "Textures of type ";
			Type type = obj.GetType();
			throw new NotSupportedException(str + ((type != null) ? type.ToString() : null) + " are not currently supported.");
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x00019643 File Offset: 0x00017843
		public override object Read<T>(ES3Reader reader)
		{
			return ES3Type_Texture2D.Instance.Read<T>(reader);
		}

		// Token: 0x04000110 RID: 272
		public static ES3Type Instance;
	}
}
