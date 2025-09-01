using System;
using ES3Internal;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x020000BF RID: 191
	[Preserve]
	[ES3Properties(new string[]
	{
		"filterMode",
		"anisoLevel",
		"wrapMode",
		"mipMapBias",
		"rawTextureData"
	})]
	public class ES3Type_Texture2D : ES3UnityObjectType
	{
		// Token: 0x060003ED RID: 1005 RVA: 0x0001966D File Offset: 0x0001786D
		public ES3Type_Texture2D() : base(typeof(Texture2D))
		{
			ES3Type_Texture2D.Instance = this;
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x00019688 File Offset: 0x00017888
		protected override void WriteUnityObject(object obj, ES3Writer writer)
		{
			Texture2D texture2D = (Texture2D)obj;
			if (!this.IsReadable(texture2D))
			{
				ES3Debug.LogWarning("Easy Save cannot save the pixels or properties of this Texture because it is not read/write enabled, so Easy Save will store it by reference instead. To save the pixel data, check the 'Read/Write Enabled' checkbox in the Texture's import settings. Clicking this warning will take you to the Texture, assuming it is not generated at runtime.", texture2D, 0);
				return;
			}
			writer.WriteProperty("width", texture2D.width, ES3Type_int.Instance);
			writer.WriteProperty("height", texture2D.height, ES3Type_int.Instance);
			writer.WriteProperty("format", texture2D.format);
			writer.WriteProperty("mipmapCount", texture2D.mipmapCount, ES3Type_int.Instance);
			writer.WriteProperty("filterMode", texture2D.filterMode);
			writer.WriteProperty("anisoLevel", texture2D.anisoLevel, ES3Type_int.Instance);
			writer.WriteProperty("wrapMode", texture2D.wrapMode);
			writer.WriteProperty("mipMapBias", texture2D.mipMapBias, ES3Type_float.Instance);
			writer.WriteProperty("rawTextureData", texture2D.GetRawTextureData(), ES3Type_byteArray.Instance);
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x00019794 File Offset: 0x00017994
		protected override void ReadUnityObject<T>(ES3Reader reader, object obj)
		{
			if (obj == null)
			{
				return;
			}
			if (obj.GetType() == typeof(RenderTexture))
			{
				ES3Type_RenderTexture.Instance.ReadInto<T>(reader, obj);
				return;
			}
			Texture2D texture2D = (Texture2D)obj;
			if (!this.IsReadable(texture2D))
			{
				ES3Debug.LogWarning("Easy Save cannot load the properties or pixels for this Texture " + texture2D.name + " because it is not read/write enabled, so it will be loaded by reference. To load the properties and pixels for this Texture, check the 'Read/Write Enabled' checkbox in its Import Settings.", texture2D, 0);
			}
			foreach (object obj2 in reader.Properties)
			{
				string a = (string)obj2;
				if (!this.IsReadable(texture2D))
				{
					reader.Skip();
				}
				else if (!(a == "filterMode"))
				{
					if (!(a == "anisoLevel"))
					{
						if (!(a == "wrapMode"))
						{
							if (!(a == "mipMapBias"))
							{
								if (a == "rawTextureData")
								{
									if (!this.IsReadable(texture2D))
									{
										ES3Debug.LogWarning("Easy Save cannot load the pixels of this Texture because it is not read/write enabled, so Easy Save will ignore the pixel data. To load the pixel data, check the 'Read/Write Enabled' checkbox in the Texture's import settings. Clicking this warning will take you to the Texture, assuming it is not generated at runtime.", texture2D, 0);
										reader.Skip();
										continue;
									}
									try
									{
										texture2D.LoadRawTextureData(reader.Read<byte[]>(ES3Type_byteArray.Instance));
										texture2D.Apply();
										continue;
									}
									catch (Exception ex)
									{
										ES3Debug.LogError("Easy Save encountered an error when trying to load this Texture, please see the end of this messasge for the error. This is most likely because the Texture format of the instance we are loading into is different to the Texture we saved.\n" + ex.ToString(), texture2D, 0);
										continue;
									}
								}
								reader.Skip();
							}
							else
							{
								texture2D.mipMapBias = reader.Read<float>(ES3Type_float.Instance);
							}
						}
						else
						{
							texture2D.wrapMode = reader.Read<TextureWrapMode>();
						}
					}
					else
					{
						texture2D.anisoLevel = reader.Read<int>(ES3Type_int.Instance);
					}
				}
				else
				{
					texture2D.filterMode = reader.Read<FilterMode>();
				}
			}
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x00019960 File Offset: 0x00017B60
		protected override object ReadUnityObject<T>(ES3Reader reader)
		{
			Texture2D texture2D = new Texture2D(reader.Read<int>(ES3Type_int.Instance), reader.ReadProperty<int>(ES3Type_int.Instance), reader.ReadProperty<TextureFormat>(), reader.ReadProperty<int>(ES3Type_int.Instance) > 1);
			this.ReadObject<T>(reader, texture2D);
			return texture2D;
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x000199A6 File Offset: 0x00017BA6
		protected bool IsReadable(Texture2D instance)
		{
			return instance != null && instance.isReadable;
		}

		// Token: 0x04000112 RID: 274
		public static ES3Type Instance;
	}
}
