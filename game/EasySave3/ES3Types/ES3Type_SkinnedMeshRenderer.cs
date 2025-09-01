using System;
using ES3Internal;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x020000B7 RID: 183
	[Preserve]
	[ES3Properties(new string[]
	{
		"bones",
		"rootBone",
		"quality",
		"sharedMesh",
		"updateWhenOffscreen",
		"skinnedMotionVectors",
		"localBounds",
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
	public class ES3Type_SkinnedMeshRenderer : ES3ComponentType
	{
		// Token: 0x060003D8 RID: 984 RVA: 0x00018178 File Offset: 0x00016378
		public ES3Type_SkinnedMeshRenderer() : base(typeof(SkinnedMeshRenderer))
		{
			ES3Type_SkinnedMeshRenderer.Instance = this;
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x00018190 File Offset: 0x00016390
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			SkinnedMeshRenderer skinnedMeshRenderer = (SkinnedMeshRenderer)obj;
			writer.WriteProperty("bones", skinnedMeshRenderer.bones);
			writer.WriteProperty("rootBone", skinnedMeshRenderer.rootBone);
			writer.WriteProperty("quality", skinnedMeshRenderer.quality);
			writer.WriteProperty("sharedMesh", skinnedMeshRenderer.sharedMesh);
			writer.WriteProperty("updateWhenOffscreen", skinnedMeshRenderer.updateWhenOffscreen, ES3Type_bool.Instance);
			writer.WriteProperty("skinnedMotionVectors", skinnedMeshRenderer.skinnedMotionVectors, ES3Type_bool.Instance);
			writer.WriteProperty("localBounds", skinnedMeshRenderer.localBounds, ES3Type_Bounds.Instance);
			writer.WriteProperty("enabled", skinnedMeshRenderer.enabled, ES3Type_bool.Instance);
			writer.WriteProperty("shadowCastingMode", skinnedMeshRenderer.shadowCastingMode);
			writer.WriteProperty("receiveShadows", skinnedMeshRenderer.receiveShadows, ES3Type_bool.Instance);
			writer.WriteProperty("sharedMaterials", skinnedMeshRenderer.sharedMaterials);
			writer.WriteProperty("lightmapIndex", skinnedMeshRenderer.lightmapIndex, ES3Type_int.Instance);
			writer.WriteProperty("realtimeLightmapIndex", skinnedMeshRenderer.realtimeLightmapIndex, ES3Type_int.Instance);
			writer.WriteProperty("lightmapScaleOffset", skinnedMeshRenderer.lightmapScaleOffset, ES3Type_Vector4.Instance);
			writer.WriteProperty("motionVectorGenerationMode", skinnedMeshRenderer.motionVectorGenerationMode);
			writer.WriteProperty("realtimeLightmapScaleOffset", skinnedMeshRenderer.realtimeLightmapScaleOffset, ES3Type_Vector4.Instance);
			writer.WriteProperty("lightProbeUsage", skinnedMeshRenderer.lightProbeUsage);
			writer.WriteProperty("lightProbeProxyVolumeOverride", skinnedMeshRenderer.lightProbeProxyVolumeOverride);
			writer.WriteProperty("probeAnchor", skinnedMeshRenderer.probeAnchor);
			writer.WriteProperty("reflectionProbeUsage", skinnedMeshRenderer.reflectionProbeUsage);
			writer.WriteProperty("sortingLayerName", skinnedMeshRenderer.sortingLayerName, ES3Type_string.Instance);
			writer.WriteProperty("sortingLayerID", skinnedMeshRenderer.sortingLayerID, ES3Type_int.Instance);
			writer.WriteProperty("sortingOrder", skinnedMeshRenderer.sortingOrder, ES3Type_int.Instance);
			if (skinnedMeshRenderer.sharedMesh != null)
			{
				float[] array = new float[skinnedMeshRenderer.sharedMesh.blendShapeCount];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = skinnedMeshRenderer.GetBlendShapeWeight(i);
				}
				writer.WriteProperty("blendShapeWeights", array, ES3Type_floatArray.Instance);
			}
		}

		// Token: 0x060003DA RID: 986 RVA: 0x00018400 File Offset: 0x00016600
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			SkinnedMeshRenderer skinnedMeshRenderer = (SkinnedMeshRenderer)obj;
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
				if (num <= 1435400483U)
				{
					if (num <= 394058932U)
					{
						if (num <= 257228247U)
						{
							if (num != 49525662U)
							{
								if (num != 242590710U)
								{
									if (num == 257228247U)
									{
										if (text == "skinnedMotionVectors")
										{
											skinnedMeshRenderer.skinnedMotionVectors = reader.Read<bool>(ES3Type_bool.Instance);
											continue;
										}
									}
								}
								else if (text == "blendShapeWeights")
								{
									float[] array = reader.Read<float[]>(ES3Type_floatArray.Instance);
									if (!(skinnedMeshRenderer.sharedMesh == null))
									{
										if (array.Length != skinnedMeshRenderer.sharedMesh.blendShapeCount)
										{
											ES3Debug.LogError("The number of blend shape weights we are loading does not match the number of blend shapes in this SkinnedMeshRenderer's Mesh", null, 0);
										}
										for (int i = 0; i < array.Length; i++)
										{
											skinnedMeshRenderer.SetBlendShapeWeight(i, array[i]);
										}
										continue;
									}
									continue;
								}
							}
							else if (text == "enabled")
							{
								skinnedMeshRenderer.enabled = reader.Read<bool>(ES3Type_bool.Instance);
								continue;
							}
						}
						else if (num != 258071601U)
						{
							if (num != 369730773U)
							{
								if (num == 394058932U)
								{
									if (text == "lightmapScaleOffset")
									{
										skinnedMeshRenderer.lightmapScaleOffset = reader.Read<Vector4>(ES3Type_Vector4.Instance);
										continue;
									}
								}
							}
							else if (text == "lightmapIndex")
							{
								skinnedMeshRenderer.lightmapIndex = reader.Read<int>(ES3Type_int.Instance);
								continue;
							}
						}
						else if (text == "reflectionProbeUsage")
						{
							skinnedMeshRenderer.reflectionProbeUsage = reader.Read<ReflectionProbeUsage>();
							continue;
						}
					}
					else if (num <= 899577978U)
					{
						if (num != 441150745U)
						{
							if (num != 560621451U)
							{
								if (num == 899577978U)
								{
									if (text == "realtimeLightmapIndex")
									{
										skinnedMeshRenderer.realtimeLightmapIndex = reader.Read<int>(ES3Type_int.Instance);
										continue;
									}
								}
							}
							else if (text == "receiveShadows")
							{
								skinnedMeshRenderer.receiveShadows = reader.Read<bool>(ES3Type_bool.Instance);
								continue;
							}
						}
						else if (text == "sharedMesh")
						{
							skinnedMeshRenderer.sharedMesh = reader.Read<Mesh>(ES3Type_Mesh.Instance);
							continue;
						}
					}
					else if (num != 1039612288U)
					{
						if (num != 1284321245U)
						{
							if (num == 1435400483U)
							{
								if (text == "sortingLayerName")
								{
									skinnedMeshRenderer.sortingLayerName = reader.Read<string>(ES3Type_string.Instance);
									continue;
								}
							}
						}
						else if (text == "updateWhenOffscreen")
						{
							skinnedMeshRenderer.updateWhenOffscreen = reader.Read<bool>(ES3Type_bool.Instance);
							continue;
						}
					}
					else if (text == "probeAnchor")
					{
						skinnedMeshRenderer.probeAnchor = reader.Read<Transform>(ES3Type_Transform.Instance);
						continue;
					}
				}
				else if (num <= 2702426036U)
				{
					if (num <= 2056288458U)
					{
						if (num != 1681590497U)
						{
							if (num != 1913193519U)
							{
								if (num == 2056288458U)
								{
									if (text == "sharedMaterials")
									{
										skinnedMeshRenderer.sharedMaterials = reader.Read<Material[]>();
										continue;
									}
								}
							}
							else if (text == "rootBone")
							{
								skinnedMeshRenderer.rootBone = reader.Read<Transform>(ES3Type_Transform.Instance);
								continue;
							}
						}
						else if (text == "sortingOrder")
						{
							skinnedMeshRenderer.sortingOrder = reader.Read<int>(ES3Type_int.Instance);
							continue;
						}
					}
					else if (num != 2066010489U)
					{
						if (num != 2597670950U)
						{
							if (num == 2702426036U)
							{
								if (text == "bones")
								{
									skinnedMeshRenderer.bones = reader.Read<Transform[]>();
									continue;
								}
							}
						}
						else if (text == "quality")
						{
							skinnedMeshRenderer.quality = reader.Read<SkinQuality>();
							continue;
						}
					}
					else if (text == "motionVectorGenerationMode")
					{
						skinnedMeshRenderer.motionVectorGenerationMode = reader.Read<MotionVectorGenerationMode>();
						continue;
					}
				}
				else if (num <= 3653878719U)
				{
					if (num != 2844334693U)
					{
						if (num != 3415540015U)
						{
							if (num == 3653878719U)
							{
								if (text == "lightProbeProxyVolumeOverride")
								{
									skinnedMeshRenderer.lightProbeProxyVolumeOverride = reader.Read<GameObject>(ES3Type_GameObject.Instance);
									continue;
								}
							}
						}
						else if (text == "realtimeLightmapScaleOffset")
						{
							skinnedMeshRenderer.realtimeLightmapScaleOffset = reader.Read<Vector4>(ES3Type_Vector4.Instance);
							continue;
						}
					}
					else if (text == "shadowCastingMode")
					{
						skinnedMeshRenderer.shadowCastingMode = reader.Read<ShadowCastingMode>();
						continue;
					}
				}
				else if (num != 3921389097U)
				{
					if (num != 4124654845U)
					{
						if (num == 4199108346U)
						{
							if (text == "lightProbeUsage")
							{
								skinnedMeshRenderer.lightProbeUsage = reader.Read<LightProbeUsage>();
								continue;
							}
						}
					}
					else if (text == "sortingLayerID")
					{
						skinnedMeshRenderer.sortingLayerID = reader.Read<int>(ES3Type_int.Instance);
						continue;
					}
				}
				else if (text == "localBounds")
				{
					skinnedMeshRenderer.localBounds = reader.Read<Bounds>(ES3Type_Bounds.Instance);
					continue;
				}
				reader.Skip();
			}
		}

		// Token: 0x0400010A RID: 266
		public static ES3Type Instance;
	}
}
