using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x020000B4 RID: 180
	[Preserve]
	[ES3Properties(new string[]
	{
		"enabled",
		"shapeType",
		"randomDirectionAmount",
		"sphericalDirectionAmount",
		"alignToDirection",
		"radius",
		"angle",
		"length",
		"box",
		"meshShapeType",
		"mesh",
		"meshRenderer",
		"skinnedMeshRenderer",
		"useMeshMaterialIndex",
		"meshMaterialIndex",
		"useMeshColors",
		"normalOffset",
		"meshScale",
		"arc"
	})]
	public class ES3Type_ShapeModule : ES3Type
	{
		// Token: 0x060003CC RID: 972 RVA: 0x00017315 File Offset: 0x00015515
		public ES3Type_ShapeModule() : base(typeof(ParticleSystem.ShapeModule))
		{
			ES3Type_ShapeModule.Instance = this;
		}

		// Token: 0x060003CD RID: 973 RVA: 0x00017330 File Offset: 0x00015530
		public override void Write(object obj, ES3Writer writer)
		{
			ParticleSystem.ShapeModule shapeModule = (ParticleSystem.ShapeModule)obj;
			writer.WriteProperty("enabled", shapeModule.enabled, ES3Type_bool.Instance);
			writer.WriteProperty("shapeType", shapeModule.shapeType);
			writer.WriteProperty("randomDirectionAmount", shapeModule.randomDirectionAmount, ES3Type_float.Instance);
			writer.WriteProperty("sphericalDirectionAmount", shapeModule.sphericalDirectionAmount, ES3Type_float.Instance);
			writer.WriteProperty("alignToDirection", shapeModule.alignToDirection, ES3Type_bool.Instance);
			writer.WriteProperty("radius", shapeModule.radius, ES3Type_float.Instance);
			writer.WriteProperty("angle", shapeModule.angle, ES3Type_float.Instance);
			writer.WriteProperty("length", shapeModule.length, ES3Type_float.Instance);
			writer.WriteProperty("scale", shapeModule.scale, ES3Type_Vector3.Instance);
			writer.WriteProperty("meshShapeType", shapeModule.meshShapeType);
			writer.WritePropertyByRef("mesh", shapeModule.mesh);
			writer.WritePropertyByRef("meshRenderer", shapeModule.meshRenderer);
			writer.WritePropertyByRef("skinnedMeshRenderer", shapeModule.skinnedMeshRenderer);
			writer.WriteProperty("useMeshMaterialIndex", shapeModule.useMeshMaterialIndex, ES3Type_bool.Instance);
			writer.WriteProperty("meshMaterialIndex", shapeModule.meshMaterialIndex, ES3Type_int.Instance);
			writer.WriteProperty("useMeshColors", shapeModule.useMeshColors, ES3Type_bool.Instance);
			writer.WriteProperty("normalOffset", shapeModule.normalOffset, ES3Type_float.Instance);
			writer.WriteProperty("arc", shapeModule.arc, ES3Type_float.Instance);
		}

		// Token: 0x060003CE RID: 974 RVA: 0x00017514 File Offset: 0x00015714
		public override object Read<T>(ES3Reader reader)
		{
			ParticleSystem.ShapeModule shapeModule = default(ParticleSystem.ShapeModule);
			this.ReadInto<T>(reader, shapeModule);
			return shapeModule;
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0001753C File Offset: 0x0001573C
		public override void ReadInto<T>(ES3Reader reader, object obj)
		{
			ParticleSystem.ShapeModule shapeModule = (ParticleSystem.ShapeModule)obj;
			string text;
			while ((text = reader.ReadPropertyName()) != null)
			{
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
				if (num <= 2190941297U)
				{
					if (num <= 666576729U)
					{
						if (num <= 49525662U)
						{
							if (num != 24602224U)
							{
								if (num == 49525662U)
								{
									if (text == "enabled")
									{
										shapeModule.enabled = reader.Read<bool>(ES3Type_bool.Instance);
										continue;
									}
								}
							}
							else if (text == "useMeshMaterialIndex")
							{
								shapeModule.useMeshMaterialIndex = reader.Read<bool>(ES3Type_bool.Instance);
								continue;
							}
						}
						else if (num != 230313139U)
						{
							if (num == 666576729U)
							{
								if (text == "meshShapeType")
								{
									shapeModule.meshShapeType = reader.Read<ParticleSystemMeshShapeType>();
									continue;
								}
							}
						}
						else if (text == "radius")
						{
							shapeModule.radius = reader.Read<float>(ES3Type_float.Instance);
							continue;
						}
					}
					else if (num <= 955488309U)
					{
						if (num != 923624301U)
						{
							if (num == 955488309U)
							{
								if (text == "normalOffset")
								{
									shapeModule.normalOffset = reader.Read<float>(ES3Type_float.Instance);
									continue;
								}
							}
						}
						else if (text == "skinnedMeshRenderer")
						{
							shapeModule.skinnedMeshRenderer = reader.Read<SkinnedMeshRenderer>();
							continue;
						}
					}
					else if (num != 980363995U)
					{
						if (num != 985988853U)
						{
							if (num == 2190941297U)
							{
								if (text == "scale")
								{
									shapeModule.scale = reader.Read<Vector3>(ES3Type_Vector3.Instance);
									continue;
								}
							}
						}
						else if (text == "sphericalDirectionAmount")
						{
							shapeModule.sphericalDirectionAmount = reader.Read<float>(ES3Type_float.Instance);
							continue;
						}
					}
					else if (text == "arc")
					{
						shapeModule.arc = reader.Read<float>(ES3Type_float.Instance);
						continue;
					}
				}
				else if (num <= 2821156684U)
				{
					if (num <= 2568039286U)
					{
						if (num != 2211460629U)
						{
							if (num == 2568039286U)
							{
								if (text == "shapeType")
								{
									shapeModule.shapeType = reader.Read<ParticleSystemShapeType>();
									continue;
								}
							}
						}
						else if (text == "length")
						{
							shapeModule.length = reader.Read<float>(ES3Type_float.Instance);
							continue;
						}
					}
					else if (num != 2701180604U)
					{
						if (num == 2821156684U)
						{
							if (text == "alignToDirection")
							{
								shapeModule.alignToDirection = reader.Read<bool>(ES3Type_bool.Instance);
								continue;
							}
						}
					}
					else if (text == "mesh")
					{
						shapeModule.mesh = reader.Read<Mesh>();
						continue;
					}
				}
				else if (num <= 2907980824U)
				{
					if (num != 2882927635U)
					{
						if (num == 2907980824U)
						{
							if (text == "angle")
							{
								shapeModule.angle = reader.Read<float>(ES3Type_float.Instance);
								continue;
							}
						}
					}
					else if (text == "randomDirectionAmount")
					{
						shapeModule.randomDirectionAmount = reader.Read<float>(ES3Type_float.Instance);
						continue;
					}
				}
				else if (num != 3710903099U)
				{
					if (num != 3821492541U)
					{
						if (num == 3929280315U)
						{
							if (text == "meshMaterialIndex")
							{
								shapeModule.meshMaterialIndex = reader.Read<int>(ES3Type_int.Instance);
								continue;
							}
						}
					}
					else if (text == "useMeshColors")
					{
						shapeModule.useMeshColors = reader.Read<bool>(ES3Type_bool.Instance);
						continue;
					}
				}
				else if (text == "meshRenderer")
				{
					shapeModule.meshRenderer = reader.Read<MeshRenderer>();
					continue;
				}
				reader.Skip();
			}
		}

		// Token: 0x04000107 RID: 263
		public static ES3Type Instance;
	}
}
