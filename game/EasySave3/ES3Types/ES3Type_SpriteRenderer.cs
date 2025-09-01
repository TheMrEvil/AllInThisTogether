using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x020000BA RID: 186
	[Preserve]
	[ES3Properties(new string[]
	{
		"sprite",
		"color",
		"flipX",
		"flipY",
		"enabled",
		"shadowCastingMode",
		"receiveShadows",
		"sharedMaterials",
		"lightmapIndex",
		"realtimeLightmapIndex",
		"lightmapScaleOffset",
		"motionVectorGenerationMode",
		"realtimeLightmapScaleOffset",
		"lightProbeUsage",
		"lightProbeProxyVolumeOverride",
		"probeAnchor",
		"reflectionProbeUsage",
		"sortingLayerName",
		"sortingLayerID",
		"sortingOrder"
	})]
	public class ES3Type_SpriteRenderer : ES3ComponentType
	{
		// Token: 0x060003E0 RID: 992 RVA: 0x00018CA8 File Offset: 0x00016EA8
		public ES3Type_SpriteRenderer() : base(typeof(SpriteRenderer))
		{
			ES3Type_SpriteRenderer.Instance = this;
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x00018CC0 File Offset: 0x00016EC0
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			SpriteRenderer spriteRenderer = (SpriteRenderer)obj;
			writer.WriteProperty("sprite", spriteRenderer.sprite);
			writer.WriteProperty("color", spriteRenderer.color, ES3Type_Color.Instance);
			writer.WriteProperty("flipX", spriteRenderer.flipX, ES3Type_bool.Instance);
			writer.WriteProperty("flipY", spriteRenderer.flipY, ES3Type_bool.Instance);
			writer.WriteProperty("enabled", spriteRenderer.enabled, ES3Type_bool.Instance);
			writer.WriteProperty("shadowCastingMode", spriteRenderer.shadowCastingMode);
			writer.WriteProperty("receiveShadows", spriteRenderer.receiveShadows, ES3Type_bool.Instance);
			writer.WriteProperty("sharedMaterials", spriteRenderer.sharedMaterials);
			writer.WriteProperty("lightmapIndex", spriteRenderer.lightmapIndex, ES3Type_int.Instance);
			writer.WriteProperty("realtimeLightmapIndex", spriteRenderer.realtimeLightmapIndex, ES3Type_int.Instance);
			writer.WriteProperty("lightmapScaleOffset", spriteRenderer.lightmapScaleOffset, ES3Type_Vector4.Instance);
			writer.WriteProperty("motionVectorGenerationMode", spriteRenderer.motionVectorGenerationMode);
			writer.WriteProperty("realtimeLightmapScaleOffset", spriteRenderer.realtimeLightmapScaleOffset, ES3Type_Vector4.Instance);
			writer.WriteProperty("lightProbeUsage", spriteRenderer.lightProbeUsage);
			writer.WriteProperty("lightProbeProxyVolumeOverride", spriteRenderer.lightProbeProxyVolumeOverride, ES3Type_GameObject.Instance);
			writer.WriteProperty("probeAnchor", spriteRenderer.probeAnchor, ES3Type_Transform.Instance);
			writer.WriteProperty("reflectionProbeUsage", spriteRenderer.reflectionProbeUsage);
			writer.WriteProperty("sortingLayerName", spriteRenderer.sortingLayerName, ES3Type_string.Instance);
			writer.WriteProperty("sortingLayerID", spriteRenderer.sortingLayerID, ES3Type_int.Instance);
			writer.WriteProperty("sortingOrder", spriteRenderer.sortingOrder, ES3Type_int.Instance);
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x00018EBC File Offset: 0x000170BC
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			SpriteRenderer spriteRenderer = (SpriteRenderer)obj;
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
				if (num <= 1587639706U)
				{
					if (num <= 560621451U)
					{
						if (num <= 258071601U)
						{
							if (num != 49525662U)
							{
								if (num == 258071601U)
								{
									if (text == "reflectionProbeUsage")
									{
										spriteRenderer.reflectionProbeUsage = reader.Read<ReflectionProbeUsage>();
										continue;
									}
								}
							}
							else if (text == "enabled")
							{
								spriteRenderer.enabled = reader.Read<bool>(ES3Type_bool.Instance);
								continue;
							}
						}
						else if (num != 369730773U)
						{
							if (num != 394058932U)
							{
								if (num == 560621451U)
								{
									if (text == "receiveShadows")
									{
										spriteRenderer.receiveShadows = reader.Read<bool>(ES3Type_bool.Instance);
										continue;
									}
								}
							}
							else if (text == "lightmapScaleOffset")
							{
								spriteRenderer.lightmapScaleOffset = reader.Read<Vector4>(ES3Type_Vector4.Instance);
								continue;
							}
						}
						else if (text == "lightmapIndex")
						{
							spriteRenderer.lightmapIndex = reader.Read<int>(ES3Type_int.Instance);
							continue;
						}
					}
					else if (num <= 1031692888U)
					{
						if (num != 899577978U)
						{
							if (num == 1031692888U)
							{
								if (text == "color")
								{
									spriteRenderer.color = reader.Read<Color>(ES3Type_Color.Instance);
									continue;
								}
							}
						}
						else if (text == "realtimeLightmapIndex")
						{
							spriteRenderer.realtimeLightmapIndex = reader.Read<int>(ES3Type_int.Instance);
							continue;
						}
					}
					else if (num != 1039612288U)
					{
						if (num != 1435400483U)
						{
							if (num == 1587639706U)
							{
								if (text == "flipX")
								{
									spriteRenderer.flipX = reader.Read<bool>(ES3Type_bool.Instance);
									continue;
								}
							}
						}
						else if (text == "sortingLayerName")
						{
							spriteRenderer.sortingLayerName = reader.Read<string>(ES3Type_string.Instance);
							continue;
						}
					}
					else if (text == "probeAnchor")
					{
						spriteRenderer.probeAnchor = reader.Read<Transform>(ES3Type_Transform.Instance);
						continue;
					}
				}
				else if (num <= 2179094556U)
				{
					if (num <= 1681590497U)
					{
						if (num != 1604417325U)
						{
							if (num == 1681590497U)
							{
								if (text == "sortingOrder")
								{
									spriteRenderer.sortingOrder = reader.Read<int>(ES3Type_int.Instance);
									continue;
								}
							}
						}
						else if (text == "flipY")
						{
							spriteRenderer.flipY = reader.Read<bool>(ES3Type_bool.Instance);
							continue;
						}
					}
					else if (num != 2056288458U)
					{
						if (num != 2066010489U)
						{
							if (num == 2179094556U)
							{
								if (text == "sprite")
								{
									spriteRenderer.sprite = reader.Read<Sprite>(ES3Type_Sprite.Instance);
									continue;
								}
							}
						}
						else if (text == "motionVectorGenerationMode")
						{
							spriteRenderer.motionVectorGenerationMode = reader.Read<MotionVectorGenerationMode>();
							continue;
						}
					}
					else if (text == "sharedMaterials")
					{
						spriteRenderer.sharedMaterials = reader.Read<Material[]>();
						continue;
					}
				}
				else if (num <= 3415540015U)
				{
					if (num != 2844334693U)
					{
						if (num == 3415540015U)
						{
							if (text == "realtimeLightmapScaleOffset")
							{
								spriteRenderer.realtimeLightmapScaleOffset = reader.Read<Vector4>(ES3Type_Vector4.Instance);
								continue;
							}
						}
					}
					else if (text == "shadowCastingMode")
					{
						spriteRenderer.shadowCastingMode = reader.Read<ShadowCastingMode>();
						continue;
					}
				}
				else if (num != 3653878719U)
				{
					if (num != 4124654845U)
					{
						if (num == 4199108346U)
						{
							if (text == "lightProbeUsage")
							{
								spriteRenderer.lightProbeUsage = reader.Read<LightProbeUsage>();
								continue;
							}
						}
					}
					else if (text == "sortingLayerID")
					{
						spriteRenderer.sortingLayerID = reader.Read<int>(ES3Type_int.Instance);
						continue;
					}
				}
				else if (text == "lightProbeProxyVolumeOverride")
				{
					spriteRenderer.lightProbeProxyVolumeOverride = reader.Read<GameObject>(ES3Type_GameObject.Instance);
					continue;
				}
				reader.Skip();
			}
		}

		// Token: 0x0400010D RID: 269
		public static ES3Type Instance;
	}
}
