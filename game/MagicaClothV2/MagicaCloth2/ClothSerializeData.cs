using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;

namespace MagicaCloth2
{
	// Token: 0x02000013 RID: 19
	[Serializable]
	public class ClothSerializeData : IDataValidate, IValid, ITransform
	{
		// Token: 0x0600004B RID: 75 RVA: 0x00004724 File Offset: 0x00002924
		public ClothSerializeData()
		{
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00004854 File Offset: 0x00002A54
		public bool IsValid()
		{
			if (this.clothType == ClothProcess.ClothType.BoneCloth)
			{
				if (this.rootBones == null || this.rootBones.Count == 0)
				{
					return false;
				}
				if (this.rootBones.Count((Transform x) => x != null) == 0)
				{
					return false;
				}
			}
			else
			{
				if (this.clothType != ClothProcess.ClothType.MeshCloth)
				{
					return false;
				}
				if (this.sourceRenderers == null || this.sourceRenderers.Count == 0)
				{
					return false;
				}
				if (this.sourceRenderers.Count((Renderer x) => x != null) == 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00004900 File Offset: 0x00002B00
		public void DataValidate()
		{
			this.rotationalInterpolation = Mathf.Clamp01(this.rotationalInterpolation);
			this.rootRotation = Mathf.Clamp01(this.rootRotation);
			this.animationPoseRatio = Mathf.Clamp01(this.animationPoseRatio);
			this.reductionSetting.DataValidate();
			this.customSkinningSetting.DataValidate();
			this.normalAlignmentSetting.DataValidate();
			this.gravity = Mathf.Clamp(this.gravity, 0f, 20f);
			if (math.length(this.gravityDirection) > 1E-08f)
			{
				this.gravityDirection = math.normalize(this.gravityDirection);
			}
			else
			{
				this.gravityDirection = 0;
			}
			this.gravityFalloff = Mathf.Clamp01(this.gravityFalloff);
			this.stablizationTimeAfterReset = Mathf.Clamp01(this.stablizationTimeAfterReset);
			this.blendWeight = Mathf.Clamp01(this.blendWeight);
			this.damping.DataValidate(0f, 1f);
			this.radius.DataValidate(0.001f, 1f);
			this.inertiaConstraint.DataValidate();
			this.tetherConstraint.DataValidate();
			this.distanceConstraint.DataValidate();
			this.triangleBendingConstraint.DataValidate();
			this.angleRestorationConstraint.DataValidate();
			this.angleLimitConstraint.DataValidate();
			this.motionConstraint.DataValidate();
			this.colliderCollisionConstraint.DataValidate();
			this.selfCollisionConstraint.DataValidate();
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00004A70 File Offset: 0x00002C70
		public override int GetHashCode()
		{
			int num = 0;
			num = (int)(num + this.clothType);
			foreach (Renderer renderer in this.sourceRenderers)
			{
				num += ((renderer != null) ? renderer.GetInstanceID() : 0);
			}
			foreach (Transform item in this.rootBones)
			{
				Stack<Transform> stack = new Stack<Transform>(30);
				stack.Push(item);
				while (stack.Count > 0)
				{
					Transform transform = stack.Pop();
					if (!(transform == null))
					{
						num += transform.GetInstanceID();
						num += transform.localPosition.GetHashCode();
						num += transform.localRotation.GetHashCode();
						int childCount = transform.childCount;
						for (int i = 0; i < childCount; i++)
						{
							stack.Push(transform.GetChild(i));
						}
					}
				}
			}
			num = (int)(num + this.connectionMode * (RenderSetupData.BoneConnectionMode)10);
			num += this.reductionSetting.GetHashCode();
			num += this.customSkinningSetting.GetHashCode();
			num += this.normalAlignmentSetting.GetHashCode();
			num = (int)(num + this.paintMode);
			foreach (Texture2D texture2D in this.paintMaps)
			{
				if (texture2D)
				{
					num += texture2D.GetInstanceID();
					num += (texture2D.isReadable ? 1 : 0);
				}
			}
			return num;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00004C54 File Offset: 0x00002E54
		public ClothParameters GetClothParameters()
		{
			ClothParameters result = default(ClothParameters);
			result.solverFrequency = 90;
			result.gravity = this.gravity;
			result.gravityDirection = this.gravityDirection;
			result.gravityFalloff = this.gravityFalloff;
			result.stablizationTimeAfterReset = this.stablizationTimeAfterReset;
			result.blendWeight = this.blendWeight;
			result.dampingCurveData = this.damping.ConvertFloatArray() * 0.2f;
			result.radiusCurveData = this.radius.ConvertFloatArray();
			result.normalAxis = this.normalAxis;
			result.rotationalInterpolation = this.rotationalInterpolation;
			result.rootRotation = this.rootRotation;
			result.inertiaConstraint.Convert(this.inertiaConstraint);
			result.tetherConstraint.Convert(this.tetherConstraint);
			result.distanceConstraint.Convert(this.distanceConstraint);
			result.triangleBendingConstraint.Convert(this.triangleBendingConstraint);
			result.angleConstraint.Convert(this.angleRestorationConstraint, this.angleLimitConstraint);
			result.motionConstraint.Convert(this.motionConstraint);
			result.colliderCollisionConstraint.Convert(this.colliderCollisionConstraint);
			result.selfCollisionConstraint.Convert(this.selfCollisionConstraint);
			return result;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00004D9F File Offset: 0x00002F9F
		public string ExportJson()
		{
			return JsonUtility.ToJson(this);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00004DA8 File Offset: 0x00002FA8
		public bool ImportJson(string json)
		{
			bool result;
			try
			{
				ClothSerializeData.TempBuffer tempBuffer = new ClothSerializeData.TempBuffer(this);
				JsonUtility.FromJsonOverwrite(json, this);
				tempBuffer.Pop(this);
				this.DataValidate();
				result = true;
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
				result = false;
			}
			return result;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00004DEC File Offset: 0x00002FEC
		public void Import(ClothSerializeData sdata, bool deepCopy = false)
		{
			ClothSerializeData.TempBuffer tempBuffer = deepCopy ? null : new ClothSerializeData.TempBuffer(this);
			if (deepCopy)
			{
				this.clothType = sdata.clothType;
				this.sourceRenderers = new List<Renderer>(sdata.sourceRenderers);
				this.paintMode = sdata.paintMode;
				this.paintMaps = new List<Texture2D>(sdata.paintMaps);
				this.rootBones = new List<Transform>(sdata.rootBones);
				this.connectionMode = sdata.connectionMode;
				this.rotationalInterpolation = sdata.rotationalInterpolation;
				this.rootRotation = sdata.rootRotation;
				this.updateMode = sdata.updateMode;
				this.animationPoseRatio = sdata.animationPoseRatio;
				this.reductionSetting = sdata.reductionSetting.Clone();
				this.customSkinningSetting = sdata.customSkinningSetting.Clone();
				this.normalAlignmentSetting = sdata.normalAlignmentSetting.Clone();
				this.normalAxis = sdata.normalAxis;
				this.stablizationTimeAfterReset = sdata.stablizationTimeAfterReset;
				this.blendWeight = sdata.blendWeight;
			}
			this.gravity = sdata.gravity;
			this.gravityDirection = sdata.gravityDirection;
			this.gravityFalloff = sdata.gravityFalloff;
			this.damping = sdata.damping.Clone();
			this.radius = sdata.radius.Clone();
			this.inertiaConstraint = sdata.inertiaConstraint.Clone();
			this.tetherConstraint = sdata.tetherConstraint.Clone();
			this.distanceConstraint = sdata.distanceConstraint.Clone();
			this.triangleBendingConstraint = sdata.triangleBendingConstraint.Clone();
			this.angleRestorationConstraint = sdata.angleRestorationConstraint.Clone();
			this.angleLimitConstraint = sdata.angleLimitConstraint.Clone();
			this.motionConstraint = sdata.motionConstraint.Clone();
			this.colliderCollisionConstraint = sdata.colliderCollisionConstraint.Clone();
			this.selfCollisionConstraint = sdata.selfCollisionConstraint.Clone();
			if (!deepCopy)
			{
				tempBuffer.Pop(this);
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00004FD3 File Offset: 0x000031D3
		public void Import(MagicaCloth src, bool deepCopy = false)
		{
			this.Import(src.SerializeData, deepCopy);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00004FE4 File Offset: 0x000031E4
		public void GetUsedTransform(HashSet<Transform> transformSet)
		{
			foreach (Transform transform in this.rootBones)
			{
				if (transform)
				{
					transformSet.Add(transform);
				}
			}
			this.customSkinningSetting.GetUsedTransform(transformSet);
			this.normalAlignmentSetting.GetUsedTransform(transformSet);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00005058 File Offset: 0x00003258
		public void ReplaceTransform(Dictionary<int, Transform> replaceDict)
		{
			for (int i = 0; i < this.rootBones.Count; i++)
			{
				Transform transform = this.rootBones[i];
				if (transform && replaceDict.ContainsKey(transform.GetInstanceID()))
				{
					this.rootBones[i] = replaceDict[transform.GetInstanceID()];
				}
			}
			this.customSkinningSetting.ReplaceTransform(replaceDict);
			this.normalAlignmentSetting.ReplaceTransform(replaceDict);
		}

		// Token: 0x04000067 RID: 103
		public ClothProcess.ClothType clothType;

		// Token: 0x04000068 RID: 104
		public List<Renderer> sourceRenderers = new List<Renderer>();

		// Token: 0x04000069 RID: 105
		public ClothSerializeData.PaintMode paintMode;

		// Token: 0x0400006A RID: 106
		public List<Texture2D> paintMaps = new List<Texture2D>();

		// Token: 0x0400006B RID: 107
		public List<Transform> rootBones = new List<Transform>();

		// Token: 0x0400006C RID: 108
		public RenderSetupData.BoneConnectionMode connectionMode;

		// Token: 0x0400006D RID: 109
		[Range(0f, 1f)]
		public float rotationalInterpolation = 0.5f;

		// Token: 0x0400006E RID: 110
		[Range(0f, 1f)]
		public float rootRotation = 0.5f;

		// Token: 0x0400006F RID: 111
		public ClothUpdateMode updateMode;

		// Token: 0x04000070 RID: 112
		[Range(0f, 1f)]
		public float animationPoseRatio;

		// Token: 0x04000071 RID: 113
		public ReductionSettings reductionSetting = new ReductionSettings();

		// Token: 0x04000072 RID: 114
		public CustomSkinningSettings customSkinningSetting = new CustomSkinningSettings();

		// Token: 0x04000073 RID: 115
		public NormalAlignmentSettings normalAlignmentSetting = new NormalAlignmentSettings();

		// Token: 0x04000074 RID: 116
		public ClothNormalAxis normalAxis = ClothNormalAxis.Up;

		// Token: 0x04000075 RID: 117
		[Range(0f, 10f)]
		public float gravity = 5f;

		// Token: 0x04000076 RID: 118
		public float3 gravityDirection = new float3(0f, -1f, 0f);

		// Token: 0x04000077 RID: 119
		[Range(0f, 1f)]
		public float gravityFalloff;

		// Token: 0x04000078 RID: 120
		[Range(0f, 1f)]
		public float stablizationTimeAfterReset = 0.1f;

		// Token: 0x04000079 RID: 121
		[NonSerialized]
		public float blendWeight = 1f;

		// Token: 0x0400007A RID: 122
		public CurveSerializeData damping = new CurveSerializeData(0.05f);

		// Token: 0x0400007B RID: 123
		public CurveSerializeData radius = new CurveSerializeData(0.02f);

		// Token: 0x0400007C RID: 124
		public InertiaConstraint.SerializeData inertiaConstraint = new InertiaConstraint.SerializeData();

		// Token: 0x0400007D RID: 125
		public TetherConstraint.SerializeData tetherConstraint = new TetherConstraint.SerializeData();

		// Token: 0x0400007E RID: 126
		public DistanceConstraint.SerializeData distanceConstraint = new DistanceConstraint.SerializeData();

		// Token: 0x0400007F RID: 127
		public TriangleBendingConstraint.SerializeData triangleBendingConstraint = new TriangleBendingConstraint.SerializeData();

		// Token: 0x04000080 RID: 128
		public AngleConstraint.RestorationSerializeData angleRestorationConstraint = new AngleConstraint.RestorationSerializeData();

		// Token: 0x04000081 RID: 129
		public AngleConstraint.LimitSerializeData angleLimitConstraint = new AngleConstraint.LimitSerializeData();

		// Token: 0x04000082 RID: 130
		public MotionConstraint.SerializeData motionConstraint = new MotionConstraint.SerializeData();

		// Token: 0x04000083 RID: 131
		public ColliderCollisionConstraint.SerializeData colliderCollisionConstraint = new ColliderCollisionConstraint.SerializeData();

		// Token: 0x04000084 RID: 132
		public SelfCollisionConstraint.SerializeData selfCollisionConstraint = new SelfCollisionConstraint.SerializeData();

		// Token: 0x02000014 RID: 20
		public enum PaintMode
		{
			// Token: 0x04000086 RID: 134
			Manual,
			// Token: 0x04000087 RID: 135
			[InspectorName("Texture Fixed(RD) Move(GR) Ignore(BK)")]
			Texture_Fixed_Move,
			// Token: 0x04000088 RID: 136
			[InspectorName("Texture Fixed(RD) Move(GR) Limit(BL) Ignore(BK)")]
			Texture_Fixed_Move_Limit
		}

		// Token: 0x02000015 RID: 21
		private class TempBuffer
		{
			// Token: 0x06000056 RID: 86 RVA: 0x000050CE File Offset: 0x000032CE
			internal TempBuffer(ClothSerializeData sdata)
			{
				this.Push(sdata);
			}

			// Token: 0x06000057 RID: 87 RVA: 0x000050E0 File Offset: 0x000032E0
			internal void Push(ClothSerializeData sdata)
			{
				this.clothType = sdata.clothType;
				this.sourceRenderers = new List<Renderer>(sdata.sourceRenderers);
				this.paintMode = sdata.paintMode;
				this.paintMaps = new List<Texture2D>(sdata.paintMaps);
				this.rootBones = new List<Transform>(sdata.rootBones);
				this.connectionMode = sdata.connectionMode;
				this.rotationalInterpolation = sdata.rotationalInterpolation;
				this.rootRotation = sdata.rootRotation;
				this.updateMode = sdata.updateMode;
				this.animationPoseRatio = sdata.animationPoseRatio;
				this.reductionSetting = sdata.reductionSetting.Clone();
				this.customSkinningSetting = sdata.customSkinningSetting.Clone();
				this.normalAlignmentSetting = sdata.normalAlignmentSetting.Clone();
				this.normalAxis = sdata.normalAxis;
				this.colliderList = new List<ColliderComponent>(sdata.colliderCollisionConstraint.colliderList);
				this.synchronization = sdata.selfCollisionConstraint.syncPartner;
				this.stablizationTimeAfterReset = sdata.stablizationTimeAfterReset;
				this.blendWeight = sdata.blendWeight;
			}

			// Token: 0x06000058 RID: 88 RVA: 0x000051F4 File Offset: 0x000033F4
			internal void Pop(ClothSerializeData sdata)
			{
				sdata.clothType = this.clothType;
				sdata.sourceRenderers = this.sourceRenderers;
				sdata.paintMode = this.paintMode;
				sdata.paintMaps = this.paintMaps;
				sdata.rootBones = this.rootBones;
				sdata.connectionMode = this.connectionMode;
				sdata.rotationalInterpolation = this.rotationalInterpolation;
				sdata.rootRotation = this.rootRotation;
				sdata.updateMode = this.updateMode;
				sdata.animationPoseRatio = this.animationPoseRatio;
				sdata.reductionSetting = this.reductionSetting;
				sdata.customSkinningSetting = this.customSkinningSetting;
				sdata.normalAlignmentSetting = this.normalAlignmentSetting;
				sdata.normalAxis = this.normalAxis;
				sdata.colliderCollisionConstraint.colliderList = this.colliderList;
				sdata.selfCollisionConstraint.syncPartner = this.synchronization;
				sdata.stablizationTimeAfterReset = this.stablizationTimeAfterReset;
				sdata.blendWeight = this.blendWeight;
			}

			// Token: 0x04000089 RID: 137
			private ClothProcess.ClothType clothType;

			// Token: 0x0400008A RID: 138
			private List<Renderer> sourceRenderers;

			// Token: 0x0400008B RID: 139
			private ClothSerializeData.PaintMode paintMode;

			// Token: 0x0400008C RID: 140
			private List<Texture2D> paintMaps;

			// Token: 0x0400008D RID: 141
			private List<Transform> rootBones;

			// Token: 0x0400008E RID: 142
			private RenderSetupData.BoneConnectionMode connectionMode;

			// Token: 0x0400008F RID: 143
			private float rotationalInterpolation;

			// Token: 0x04000090 RID: 144
			private float rootRotation;

			// Token: 0x04000091 RID: 145
			private ClothUpdateMode updateMode;

			// Token: 0x04000092 RID: 146
			private float animationPoseRatio;

			// Token: 0x04000093 RID: 147
			private ReductionSettings reductionSetting;

			// Token: 0x04000094 RID: 148
			private CustomSkinningSettings customSkinningSetting;

			// Token: 0x04000095 RID: 149
			private NormalAlignmentSettings normalAlignmentSetting;

			// Token: 0x04000096 RID: 150
			private ClothNormalAxis normalAxis;

			// Token: 0x04000097 RID: 151
			private List<ColliderComponent> colliderList;

			// Token: 0x04000098 RID: 152
			private MagicaCloth synchronization;

			// Token: 0x04000099 RID: 153
			private float stablizationTimeAfterReset;

			// Token: 0x0400009A RID: 154
			private float blendWeight;
		}

		// Token: 0x02000016 RID: 22
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000059 RID: 89 RVA: 0x000052E3 File Offset: 0x000034E3
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600005A RID: 90 RVA: 0x00002058 File Offset: 0x00000258
			public <>c()
			{
			}

			// Token: 0x0600005B RID: 91 RVA: 0x000046CE File Offset: 0x000028CE
			internal bool <IsValid>b__32_0(Transform x)
			{
				return x != null;
			}

			// Token: 0x0600005C RID: 92 RVA: 0x000046CE File Offset: 0x000028CE
			internal bool <IsValid>b__32_1(Renderer x)
			{
				return x != null;
			}

			// Token: 0x0400009B RID: 155
			public static readonly ClothSerializeData.<>c <>9 = new ClothSerializeData.<>c();

			// Token: 0x0400009C RID: 156
			public static Func<Transform, bool> <>9__32_0;

			// Token: 0x0400009D RID: 157
			public static Func<Renderer, bool> <>9__32_1;
		}
	}
}
