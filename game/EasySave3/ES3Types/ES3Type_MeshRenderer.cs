using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x0200006A RID: 106
	[Preserve]
	[ES3Properties(new string[]
	{
		"additionalVertexStreams",
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
	public class ES3Type_MeshRenderer : ES3ComponentType
	{
		// Token: 0x060002FD RID: 765 RVA: 0x0000C749 File Offset: 0x0000A949
		public ES3Type_MeshRenderer() : base(typeof(MeshRenderer))
		{
			ES3Type_MeshRenderer.Instance = this;
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000C764 File Offset: 0x0000A964
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			MeshRenderer meshRenderer = (MeshRenderer)obj;
			writer.WriteProperty("additionalVertexStreams", meshRenderer.additionalVertexStreams, ES3Type_Mesh.Instance);
			writer.WriteProperty("enabled", meshRenderer.enabled, ES3Type_bool.Instance);
			writer.WriteProperty("shadowCastingMode", meshRenderer.shadowCastingMode);
			writer.WriteProperty("receiveShadows", meshRenderer.receiveShadows, ES3Type_bool.Instance);
			writer.WriteProperty("sharedMaterials", meshRenderer.sharedMaterials, ES3Type_MaterialArray.Instance);
			writer.WriteProperty("lightmapIndex", meshRenderer.lightmapIndex, ES3Type_int.Instance);
			writer.WriteProperty("realtimeLightmapIndex", meshRenderer.realtimeLightmapIndex, ES3Type_int.Instance);
			writer.WriteProperty("lightmapScaleOffset", meshRenderer.lightmapScaleOffset, ES3Type_Vector4.Instance);
			writer.WriteProperty("motionVectorGenerationMode", meshRenderer.motionVectorGenerationMode);
			writer.WriteProperty("realtimeLightmapScaleOffset", meshRenderer.realtimeLightmapScaleOffset, ES3Type_Vector4.Instance);
			writer.WriteProperty("lightProbeUsage", meshRenderer.lightProbeUsage);
			writer.WriteProperty("lightProbeProxyVolumeOverride", meshRenderer.lightProbeProxyVolumeOverride);
			writer.WriteProperty("probeAnchor", meshRenderer.probeAnchor, ES3Type_Transform.Instance);
			writer.WriteProperty("reflectionProbeUsage", meshRenderer.reflectionProbeUsage);
			writer.WriteProperty("sortingLayerName", meshRenderer.sortingLayerName, ES3Type_string.Instance);
			writer.WriteProperty("sortingLayerID", meshRenderer.sortingLayerID, ES3Type_int.Instance);
			writer.WriteProperty("sortingOrder", meshRenderer.sortingOrder, ES3Type_int.Instance);
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000C914 File Offset: 0x0000AB14
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			MeshRenderer meshRenderer = (MeshRenderer)obj;
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
				if (num <= 1039612288U)
				{
					if (num <= 394058932U)
					{
						if (num <= 258071601U)
						{
							if (num != 49525662U)
							{
								if (num == 258071601U)
								{
									if (text == "reflectionProbeUsage")
									{
										meshRenderer.reflectionProbeUsage = reader.Read<ReflectionProbeUsage>();
										continue;
									}
								}
							}
							else if (text == "enabled")
							{
								meshRenderer.enabled = reader.Read<bool>(ES3Type_bool.Instance);
								continue;
							}
						}
						else if (num != 369730773U)
						{
							if (num == 394058932U)
							{
								if (text == "lightmapScaleOffset")
								{
									meshRenderer.lightmapScaleOffset = reader.Read<Vector4>(ES3Type_Vector4.Instance);
									continue;
								}
							}
						}
						else if (text == "lightmapIndex")
						{
							meshRenderer.lightmapIndex = reader.Read<int>(ES3Type_int.Instance);
							continue;
						}
					}
					else if (num <= 594369155U)
					{
						if (num != 560621451U)
						{
							if (num == 594369155U)
							{
								if (text == "additionalVertexStreams")
								{
									meshRenderer.additionalVertexStreams = reader.Read<Mesh>(ES3Type_Mesh.Instance);
									continue;
								}
							}
						}
						else if (text == "receiveShadows")
						{
							meshRenderer.receiveShadows = reader.Read<bool>(ES3Type_bool.Instance);
							continue;
						}
					}
					else if (num != 899577978U)
					{
						if (num == 1039612288U)
						{
							if (text == "probeAnchor")
							{
								meshRenderer.probeAnchor = reader.Read<Transform>(ES3Type_Transform.Instance);
								continue;
							}
						}
					}
					else if (text == "realtimeLightmapIndex")
					{
						meshRenderer.realtimeLightmapIndex = reader.Read<int>(ES3Type_int.Instance);
						continue;
					}
				}
				else if (num <= 2066010489U)
				{
					if (num <= 1681590497U)
					{
						if (num != 1435400483U)
						{
							if (num == 1681590497U)
							{
								if (text == "sortingOrder")
								{
									meshRenderer.sortingOrder = reader.Read<int>(ES3Type_int.Instance);
									continue;
								}
							}
						}
						else if (text == "sortingLayerName")
						{
							meshRenderer.sortingLayerName = reader.Read<string>(ES3Type_string.Instance);
							continue;
						}
					}
					else if (num != 2056288458U)
					{
						if (num == 2066010489U)
						{
							if (text == "motionVectorGenerationMode")
							{
								meshRenderer.motionVectorGenerationMode = reader.Read<MotionVectorGenerationMode>();
								continue;
							}
						}
					}
					else if (text == "sharedMaterials")
					{
						meshRenderer.sharedMaterials = reader.Read<Material[]>();
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
								meshRenderer.realtimeLightmapScaleOffset = reader.Read<Vector4>(ES3Type_Vector4.Instance);
								continue;
							}
						}
					}
					else if (text == "shadowCastingMode")
					{
						meshRenderer.shadowCastingMode = reader.Read<ShadowCastingMode>();
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
								meshRenderer.lightProbeUsage = reader.Read<LightProbeUsage>();
								continue;
							}
						}
					}
					else if (text == "sortingLayerID")
					{
						meshRenderer.sortingLayerID = reader.Read<int>(ES3Type_int.Instance);
						continue;
					}
				}
				else if (text == "lightProbeProxyVolumeOverride")
				{
					meshRenderer.lightProbeProxyVolumeOverride = reader.Read<GameObject>(ES3Type_GameObject.Instance);
					continue;
				}
				reader.Skip();
			}
		}

		// Token: 0x040000B9 RID: 185
		public static ES3Type Instance;
	}
}
