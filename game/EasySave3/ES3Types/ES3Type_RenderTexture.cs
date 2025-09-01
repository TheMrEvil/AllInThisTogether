using System;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x020000AE RID: 174
	[Preserve]
	[ES3Properties(new string[]
	{
		"width",
		"height",
		"dimension",
		"graphicsFormat",
		"useMipMap",
		"vrUsage",
		"memorylessMode",
		"format",
		"stencilFormat",
		"autoGenerateMips",
		"volumeDepth",
		"antiAliasing",
		"bindTextureMS",
		"enableRandomWrite",
		"useDynamicScale",
		"isPowerOfTwo",
		"depth",
		"descriptor",
		"masterTextureLimit",
		"anisotropicFiltering",
		"wrapMode",
		"wrapModeU",
		"wrapModeV",
		"wrapModeW",
		"filterMode",
		"anisoLevel",
		"mipMapBias",
		"imageContentsHash",
		"streamingTextureForceLoadAll",
		"streamingTextureDiscardUnusedMips",
		"allowThreadedTextureCreation",
		"name"
	})]
	public class ES3Type_RenderTexture : ES3ObjectType
	{
		// Token: 0x060003BA RID: 954 RVA: 0x00016234 File Offset: 0x00014434
		public ES3Type_RenderTexture() : base(typeof(RenderTexture))
		{
			ES3Type_RenderTexture.Instance = this;
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0001624C File Offset: 0x0001444C
		protected override void WriteObject(object obj, ES3Writer writer)
		{
			RenderTexture renderTexture = (RenderTexture)obj;
			writer.WriteProperty("descriptor", renderTexture.descriptor);
			writer.WriteProperty("antiAliasing", renderTexture.antiAliasing, ES3Type_int.Instance);
			writer.WriteProperty("isPowerOfTwo", renderTexture.isPowerOfTwo, ES3Type_bool.Instance);
			writer.WriteProperty("anisotropicFiltering", Texture.anisotropicFiltering);
			writer.WriteProperty("wrapMode", renderTexture.wrapMode);
			writer.WriteProperty("wrapModeU", renderTexture.wrapModeU);
			writer.WriteProperty("wrapModeV", renderTexture.wrapModeV);
			writer.WriteProperty("wrapModeW", renderTexture.wrapModeW);
			writer.WriteProperty("filterMode", renderTexture.filterMode);
			writer.WriteProperty("anisoLevel", renderTexture.anisoLevel, ES3Type_int.Instance);
			writer.WriteProperty("mipMapBias", renderTexture.mipMapBias, ES3Type_float.Instance);
			writer.WriteProperty("streamingTextureForceLoadAll", Texture.streamingTextureForceLoadAll, ES3Type_bool.Instance);
			writer.WriteProperty("streamingTextureDiscardUnusedMips", Texture.streamingTextureDiscardUnusedMips, ES3Type_bool.Instance);
			writer.WriteProperty("allowThreadedTextureCreation", Texture.allowThreadedTextureCreation, ES3Type_bool.Instance);
		}

		// Token: 0x060003BC RID: 956 RVA: 0x000163B4 File Offset: 0x000145B4
		protected override void ReadObject<T>(ES3Reader reader, object obj)
		{
			RenderTexture renderTexture = (RenderTexture)obj;
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
				if (num <= 1508062721U)
				{
					if (num <= 698754697U)
					{
						if (num <= 472844700U)
						{
							if (num != 87590155U)
							{
								if (num != 301255720U)
								{
									if (num == 472844700U)
									{
										if (text == "useMipMap")
										{
											renderTexture.useMipMap = reader.Read<bool>(ES3Type_bool.Instance);
											continue;
										}
									}
								}
								else if (text == "volumeDepth")
								{
									renderTexture.volumeDepth = reader.Read<int>(ES3Type_int.Instance);
									continue;
								}
							}
							else if (text == "graphicsFormat")
							{
								renderTexture.graphicsFormat = reader.Read<GraphicsFormat>();
								continue;
							}
						}
						else if (num <= 549791583U)
						{
							if (num != 533013964U)
							{
								if (num == 549791583U)
								{
									if (text == "wrapModeW")
									{
										renderTexture.wrapModeW = reader.Read<TextureWrapMode>();
										continue;
									}
								}
							}
							else if (text == "wrapModeV")
							{
								renderTexture.wrapModeV = reader.Read<TextureWrapMode>();
								continue;
							}
						}
						else if (num != 583346821U)
						{
							if (num == 698754697U)
							{
								if (text == "antiAliasing")
								{
									renderTexture.antiAliasing = reader.Read<int>(ES3Type_int.Instance);
									continue;
								}
							}
						}
						else if (text == "wrapModeU")
						{
							renderTexture.wrapModeU = reader.Read<TextureWrapMode>();
							continue;
						}
					}
					else if (num <= 1235622516U)
					{
						if (num <= 740612720U)
						{
							if (num != 705796271U)
							{
								if (num == 740612720U)
								{
									if (text == "filterMode")
									{
										renderTexture.filterMode = reader.Read<FilterMode>();
										continue;
									}
								}
							}
							else if (text == "isPowerOfTwo")
							{
								renderTexture.isPowerOfTwo = reader.Read<bool>(ES3Type_bool.Instance);
								continue;
							}
						}
						else if (num != 1059843136U)
						{
							if (num == 1235622516U)
							{
								if (text == "anisotropicFiltering")
								{
									Texture.anisotropicFiltering = reader.Read<AnisotropicFiltering>();
									continue;
								}
							}
						}
						else if (text == "descriptor")
						{
							renderTexture.descriptor = reader.Read<RenderTextureDescriptor>();
							continue;
						}
					}
					else if (num <= 1320492961U)
					{
						if (num != 1310955452U)
						{
							if (num == 1320492961U)
							{
								if (text == "useDynamicScale")
								{
									renderTexture.useDynamicScale = reader.Read<bool>(ES3Type_bool.Instance);
									continue;
								}
							}
						}
						else if (text == "streamingTextureForceLoadAll")
						{
							Texture.streamingTextureForceLoadAll = reader.Read<bool>(ES3Type_bool.Instance);
							continue;
						}
					}
					else if (num != 1458352709U)
					{
						if (num == 1508062721U)
						{
							if (text == "streamingTextureDiscardUnusedMips")
							{
								Texture.streamingTextureDiscardUnusedMips = reader.Read<bool>(ES3Type_bool.Instance);
								continue;
							}
						}
					}
					else if (text == "bindTextureMS")
					{
						renderTexture.bindTextureMS = reader.Read<bool>(ES3Type_bool.Instance);
						continue;
					}
				}
				else if (num <= 2772475632U)
				{
					if (num <= 1728801507U)
					{
						if (num != 1560124071U)
						{
							if (num != 1640355324U)
							{
								if (num == 1728801507U)
								{
									if (text == "dimension")
									{
										renderTexture.dimension = reader.Read<TextureDimension>();
										continue;
									}
								}
							}
							else if (text == "stencilFormat")
							{
								renderTexture.stencilFormat = reader.Read<GraphicsFormat>();
								continue;
							}
						}
						else if (text == "allowThreadedTextureCreation")
						{
							Texture.allowThreadedTextureCreation = reader.Read<bool>(ES3Type_bool.Instance);
							continue;
						}
					}
					else if (num <= 2369371622U)
					{
						if (num != 2129631044U)
						{
							if (num == 2369371622U)
							{
								if (text == "name")
								{
									renderTexture.name = reader.Read<string>(ES3Type_string.Instance);
									continue;
								}
							}
						}
						else if (text == "mipMapBias")
						{
							renderTexture.mipMapBias = reader.Read<float>(ES3Type_float.Instance);
							continue;
						}
					}
					else if (num != 2508680735U)
					{
						if (num == 2772475632U)
						{
							if (text == "memorylessMode")
							{
								renderTexture.memorylessMode = reader.Read<RenderTextureMemoryless>();
								continue;
							}
						}
					}
					else if (text == "width")
					{
						renderTexture.width = reader.Read<int>(ES3Type_int.Instance);
						continue;
					}
				}
				else if (num <= 3736605394U)
				{
					if (num <= 3114108242U)
					{
						if (num != 2959222186U)
						{
							if (num == 3114108242U)
							{
								if (text == "format")
								{
									renderTexture.format = reader.Read<RenderTextureFormat>();
									continue;
								}
							}
						}
						else if (text == "autoGenerateMips")
						{
							renderTexture.autoGenerateMips = reader.Read<bool>(ES3Type_bool.Instance);
							continue;
						}
					}
					else if (num != 3585981250U)
					{
						if (num == 3736605394U)
						{
							if (text == "wrapMode")
							{
								renderTexture.wrapMode = reader.Read<TextureWrapMode>();
								continue;
							}
						}
					}
					else if (text == "height")
					{
						renderTexture.height = reader.Read<int>(ES3Type_int.Instance);
						continue;
					}
				}
				else if (num <= 4039756036U)
				{
					if (num != 3949681535U)
					{
						if (num == 4039756036U)
						{
							if (text == "vrUsage")
							{
								renderTexture.vrUsage = reader.Read<VRTextureUsage>();
								continue;
							}
						}
					}
					else if (text == "anisoLevel")
					{
						renderTexture.anisoLevel = reader.Read<int>(ES3Type_int.Instance);
						continue;
					}
				}
				else if (num != 4252881216U)
				{
					if (num == 4269121258U)
					{
						if (text == "depth")
						{
							renderTexture.depth = reader.Read<int>(ES3Type_int.Instance);
							continue;
						}
					}
				}
				else if (text == "enableRandomWrite")
				{
					renderTexture.enableRandomWrite = reader.Read<bool>(ES3Type_bool.Instance);
					continue;
				}
				reader.Skip();
			}
		}

		// Token: 0x060003BD RID: 957 RVA: 0x00016AD4 File Offset: 0x00014CD4
		protected override object ReadObject<T>(ES3Reader reader)
		{
			RenderTexture renderTexture = new RenderTexture(reader.ReadProperty<RenderTextureDescriptor>());
			this.ReadObject<T>(reader, renderTexture);
			return renderTexture;
		}

		// Token: 0x04000101 RID: 257
		public static ES3Type Instance;
	}
}
