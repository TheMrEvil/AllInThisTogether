using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x020003F6 RID: 1014
	[UsedByNativeCode]
	public struct ScriptableCullingParameters : IEquatable<ScriptableCullingParameters>
	{
		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x0600224C RID: 8780 RVA: 0x00039594 File Offset: 0x00037794
		// (set) Token: 0x0600224D RID: 8781 RVA: 0x000395AC File Offset: 0x000377AC
		public int maximumVisibleLights
		{
			get
			{
				return this.m_maximumVisibleLights;
			}
			set
			{
				this.m_maximumVisibleLights = value;
			}
		}

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x0600224E RID: 8782 RVA: 0x000395B8 File Offset: 0x000377B8
		// (set) Token: 0x0600224F RID: 8783 RVA: 0x000395D0 File Offset: 0x000377D0
		public bool conservativeEnclosingSphere
		{
			get
			{
				return this.m_ConservativeEnclosingSphere;
			}
			set
			{
				this.m_ConservativeEnclosingSphere = value;
			}
		}

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x06002250 RID: 8784 RVA: 0x000395DC File Offset: 0x000377DC
		// (set) Token: 0x06002251 RID: 8785 RVA: 0x000395F4 File Offset: 0x000377F4
		public int numIterationsEnclosingSphere
		{
			get
			{
				return this.m_NumIterationsEnclosingSphere;
			}
			set
			{
				this.m_NumIterationsEnclosingSphere = value;
			}
		}

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x06002252 RID: 8786 RVA: 0x00039600 File Offset: 0x00037800
		// (set) Token: 0x06002253 RID: 8787 RVA: 0x00039618 File Offset: 0x00037818
		public int cullingPlaneCount
		{
			get
			{
				return this.m_CullingPlaneCount;
			}
			set
			{
				bool flag = value < 0 || value > 10;
				if (flag)
				{
					throw new ArgumentOutOfRangeException(string.Format("{0} was {1}, but must be at least 0 and less than {2}", "value", value, 10));
				}
				this.m_CullingPlaneCount = value;
			}
		}

		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x06002254 RID: 8788 RVA: 0x00039660 File Offset: 0x00037860
		// (set) Token: 0x06002255 RID: 8789 RVA: 0x0003967D File Offset: 0x0003787D
		public bool isOrthographic
		{
			get
			{
				return Convert.ToBoolean(this.m_IsOrthographic);
			}
			set
			{
				this.m_IsOrthographic = Convert.ToInt32(value);
			}
		}

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x06002256 RID: 8790 RVA: 0x0003968C File Offset: 0x0003788C
		// (set) Token: 0x06002257 RID: 8791 RVA: 0x000396A4 File Offset: 0x000378A4
		public LODParameters lodParameters
		{
			get
			{
				return this.m_LODParameters;
			}
			set
			{
				this.m_LODParameters = value;
			}
		}

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x06002258 RID: 8792 RVA: 0x000396B0 File Offset: 0x000378B0
		// (set) Token: 0x06002259 RID: 8793 RVA: 0x000396C8 File Offset: 0x000378C8
		public uint cullingMask
		{
			get
			{
				return this.m_CullingMask;
			}
			set
			{
				this.m_CullingMask = value;
			}
		}

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x0600225A RID: 8794 RVA: 0x000396D4 File Offset: 0x000378D4
		// (set) Token: 0x0600225B RID: 8795 RVA: 0x000396EC File Offset: 0x000378EC
		public Matrix4x4 cullingMatrix
		{
			get
			{
				return this.m_CullingMatrix;
			}
			set
			{
				this.m_CullingMatrix = value;
			}
		}

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x0600225C RID: 8796 RVA: 0x000396F8 File Offset: 0x000378F8
		// (set) Token: 0x0600225D RID: 8797 RVA: 0x00039710 File Offset: 0x00037910
		public Vector3 origin
		{
			get
			{
				return this.m_Origin;
			}
			set
			{
				this.m_Origin = value;
			}
		}

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x0600225E RID: 8798 RVA: 0x0003971C File Offset: 0x0003791C
		// (set) Token: 0x0600225F RID: 8799 RVA: 0x00039734 File Offset: 0x00037934
		public float shadowDistance
		{
			get
			{
				return this.m_ShadowDistance;
			}
			set
			{
				this.m_ShadowDistance = value;
			}
		}

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x06002260 RID: 8800 RVA: 0x00039740 File Offset: 0x00037940
		// (set) Token: 0x06002261 RID: 8801 RVA: 0x00039758 File Offset: 0x00037958
		public float shadowNearPlaneOffset
		{
			get
			{
				return this.m_ShadowNearPlaneOffset;
			}
			set
			{
				this.m_ShadowNearPlaneOffset = value;
			}
		}

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x06002262 RID: 8802 RVA: 0x00039764 File Offset: 0x00037964
		// (set) Token: 0x06002263 RID: 8803 RVA: 0x0003977C File Offset: 0x0003797C
		public CullingOptions cullingOptions
		{
			get
			{
				return this.m_CullingOptions;
			}
			set
			{
				this.m_CullingOptions = value;
			}
		}

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x06002264 RID: 8804 RVA: 0x00039788 File Offset: 0x00037988
		// (set) Token: 0x06002265 RID: 8805 RVA: 0x000397A0 File Offset: 0x000379A0
		public ReflectionProbeSortingCriteria reflectionProbeSortingCriteria
		{
			get
			{
				return this.m_ReflectionProbeSortingCriteria;
			}
			set
			{
				this.m_ReflectionProbeSortingCriteria = value;
			}
		}

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x06002266 RID: 8806 RVA: 0x000397AC File Offset: 0x000379AC
		// (set) Token: 0x06002267 RID: 8807 RVA: 0x000397C4 File Offset: 0x000379C4
		public CameraProperties cameraProperties
		{
			get
			{
				return this.m_CameraProperties;
			}
			set
			{
				this.m_CameraProperties = value;
			}
		}

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x06002268 RID: 8808 RVA: 0x000397D0 File Offset: 0x000379D0
		// (set) Token: 0x06002269 RID: 8809 RVA: 0x000397E8 File Offset: 0x000379E8
		public Matrix4x4 stereoViewMatrix
		{
			get
			{
				return this.m_StereoViewMatrix;
			}
			set
			{
				this.m_StereoViewMatrix = value;
			}
		}

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x0600226A RID: 8810 RVA: 0x000397F4 File Offset: 0x000379F4
		// (set) Token: 0x0600226B RID: 8811 RVA: 0x0003980C File Offset: 0x00037A0C
		public Matrix4x4 stereoProjectionMatrix
		{
			get
			{
				return this.m_StereoProjectionMatrix;
			}
			set
			{
				this.m_StereoProjectionMatrix = value;
			}
		}

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x0600226C RID: 8812 RVA: 0x00039818 File Offset: 0x00037A18
		// (set) Token: 0x0600226D RID: 8813 RVA: 0x00039830 File Offset: 0x00037A30
		public float stereoSeparationDistance
		{
			get
			{
				return this.m_StereoSeparationDistance;
			}
			set
			{
				this.m_StereoSeparationDistance = value;
			}
		}

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x0600226E RID: 8814 RVA: 0x0003983C File Offset: 0x00037A3C
		// (set) Token: 0x0600226F RID: 8815 RVA: 0x00039854 File Offset: 0x00037A54
		public float accurateOcclusionThreshold
		{
			get
			{
				return this.m_AccurateOcclusionThreshold;
			}
			set
			{
				this.m_AccurateOcclusionThreshold = Mathf.Max(-1f, value);
			}
		}

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x06002270 RID: 8816 RVA: 0x00039868 File Offset: 0x00037A68
		// (set) Token: 0x06002271 RID: 8817 RVA: 0x00039880 File Offset: 0x00037A80
		public int maximumPortalCullingJobs
		{
			get
			{
				return this.m_MaximumPortalCullingJobs;
			}
			set
			{
				bool flag = value < 1 || value > 16;
				if (flag)
				{
					throw new ArgumentOutOfRangeException(string.Format("{0} was {1}, but must be in range {2} to {3}", new object[]
					{
						"maximumPortalCullingJobs",
						this.maximumPortalCullingJobs,
						1,
						16
					}));
				}
				this.m_MaximumPortalCullingJobs = value;
			}
		}

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x06002272 RID: 8818 RVA: 0x000398E4 File Offset: 0x00037AE4
		public static int cullingJobsLowerLimit
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x06002273 RID: 8819 RVA: 0x000398F8 File Offset: 0x00037AF8
		public static int cullingJobsUpperLimit
		{
			get
			{
				return 16;
			}
		}

		// Token: 0x06002274 RID: 8820 RVA: 0x0003990C File Offset: 0x00037B0C
		public unsafe float GetLayerCullingDistance(int layerIndex)
		{
			bool flag = layerIndex < 0 || layerIndex >= 32;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("{0} was {1}, but must be at least 0 and less than {2}", "layerIndex", layerIndex, 32));
			}
			fixed (float* ptr = &this.m_LayerFarCullDistances.FixedElementField)
			{
				float* ptr2 = ptr;
				return ptr2[layerIndex];
			}
		}

		// Token: 0x06002275 RID: 8821 RVA: 0x0003996C File Offset: 0x00037B6C
		public unsafe void SetLayerCullingDistance(int layerIndex, float distance)
		{
			bool flag = layerIndex < 0 || layerIndex >= 32;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("{0} was {1}, but must be at least 0 and less than {2}", "layerIndex", layerIndex, 32));
			}
			fixed (float* ptr = &this.m_LayerFarCullDistances.FixedElementField)
			{
				float* ptr2 = ptr;
				ptr2[layerIndex] = distance;
			}
		}

		// Token: 0x06002276 RID: 8822 RVA: 0x000399CC File Offset: 0x00037BCC
		public unsafe Plane GetCullingPlane(int index)
		{
			bool flag = index < 0 || index >= this.cullingPlaneCount;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("{0} was {1}, but must be at least 0 and less than {2}", "index", index, this.cullingPlaneCount));
			}
			fixed (byte* ptr = &this.m_CullingPlanes.FixedElementField)
			{
				byte* ptr2 = ptr;
				Plane* ptr3 = (Plane*)ptr2;
				return ptr3[index];
			}
		}

		// Token: 0x06002277 RID: 8823 RVA: 0x00039A40 File Offset: 0x00037C40
		public unsafe void SetCullingPlane(int index, Plane plane)
		{
			bool flag = index < 0 || index >= this.cullingPlaneCount;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("{0} was {1}, but must be at least 0 and less than {2}", "index", index, this.cullingPlaneCount));
			}
			fixed (byte* ptr = &this.m_CullingPlanes.FixedElementField)
			{
				byte* ptr2 = ptr;
				Plane* ptr3 = (Plane*)ptr2;
				ptr3[index] = plane;
			}
		}

		// Token: 0x06002278 RID: 8824 RVA: 0x00039AB4 File Offset: 0x00037CB4
		public bool Equals(ScriptableCullingParameters other)
		{
			for (int i = 0; i < 32; i++)
			{
				bool flag = !this.GetLayerCullingDistance(i).Equals(other.GetLayerCullingDistance(i));
				if (flag)
				{
					return false;
				}
			}
			for (int j = 0; j < this.cullingPlaneCount; j++)
			{
				bool flag2 = !this.GetCullingPlane(j).Equals(other.GetCullingPlane(j));
				if (flag2)
				{
					return false;
				}
			}
			return this.m_IsOrthographic == other.m_IsOrthographic && this.m_LODParameters.Equals(other.m_LODParameters) && this.m_CullingPlaneCount == other.m_CullingPlaneCount && this.m_CullingMask == other.m_CullingMask && this.m_SceneMask == other.m_SceneMask && this.m_LayerCull == other.m_LayerCull && this.m_CullingMatrix.Equals(other.m_CullingMatrix) && this.m_Origin.Equals(other.m_Origin) && this.m_ShadowDistance.Equals(other.m_ShadowDistance) && this.m_ShadowNearPlaneOffset.Equals(other.m_ShadowNearPlaneOffset) && this.m_CullingOptions == other.m_CullingOptions && this.m_ReflectionProbeSortingCriteria == other.m_ReflectionProbeSortingCriteria && this.m_CameraProperties.Equals(other.m_CameraProperties) && this.m_AccurateOcclusionThreshold.Equals(other.m_AccurateOcclusionThreshold) && this.m_StereoViewMatrix.Equals(other.m_StereoViewMatrix) && this.m_StereoProjectionMatrix.Equals(other.m_StereoProjectionMatrix) && this.m_StereoSeparationDistance.Equals(other.m_StereoSeparationDistance) && this.m_maximumVisibleLights == other.m_maximumVisibleLights && this.m_ConservativeEnclosingSphere == other.m_ConservativeEnclosingSphere && this.m_NumIterationsEnclosingSphere == other.m_NumIterationsEnclosingSphere;
		}

		// Token: 0x06002279 RID: 8825 RVA: 0x00039CC4 File Offset: 0x00037EC4
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is ScriptableCullingParameters && this.Equals((ScriptableCullingParameters)obj);
		}

		// Token: 0x0600227A RID: 8826 RVA: 0x00039CFC File Offset: 0x00037EFC
		public override int GetHashCode()
		{
			int num = this.m_IsOrthographic;
			num = (num * 397 ^ this.m_LODParameters.GetHashCode());
			num = (num * 397 ^ this.m_CullingPlaneCount);
			num = (num * 397 ^ (int)this.m_CullingMask);
			num = (num * 397 ^ this.m_SceneMask.GetHashCode());
			num = (num * 397 ^ this.m_LayerCull);
			num = (num * 397 ^ this.m_CullingMatrix.GetHashCode());
			num = (num * 397 ^ this.m_Origin.GetHashCode());
			num = (num * 397 ^ this.m_ShadowDistance.GetHashCode());
			num = (num * 397 ^ this.m_ShadowNearPlaneOffset.GetHashCode());
			num = (num * 397 ^ (int)this.m_CullingOptions);
			num = (num * 397 ^ (int)this.m_ReflectionProbeSortingCriteria);
			num = (num * 397 ^ this.m_CameraProperties.GetHashCode());
			num = (num * 397 ^ this.m_AccurateOcclusionThreshold.GetHashCode());
			num = (num * 397 ^ this.m_MaximumPortalCullingJobs.GetHashCode());
			num = (num * 397 ^ this.m_StereoViewMatrix.GetHashCode());
			num = (num * 397 ^ this.m_StereoProjectionMatrix.GetHashCode());
			num = (num * 397 ^ this.m_StereoSeparationDistance.GetHashCode());
			num = (num * 397 ^ this.m_maximumVisibleLights);
			num = (num * 397 ^ this.m_ConservativeEnclosingSphere.GetHashCode());
			return num * 397 ^ this.m_NumIterationsEnclosingSphere.GetHashCode();
		}

		// Token: 0x0600227B RID: 8827 RVA: 0x00039EB0 File Offset: 0x000380B0
		public static bool operator ==(ScriptableCullingParameters left, ScriptableCullingParameters right)
		{
			return left.Equals(right);
		}

		// Token: 0x0600227C RID: 8828 RVA: 0x00039ECC File Offset: 0x000380CC
		public static bool operator !=(ScriptableCullingParameters left, ScriptableCullingParameters right)
		{
			return !left.Equals(right);
		}

		// Token: 0x0600227D RID: 8829 RVA: 0x00039EE9 File Offset: 0x000380E9
		// Note: this type is marked as 'beforefieldinit'.
		static ScriptableCullingParameters()
		{
		}

		// Token: 0x04000CAB RID: 3243
		private int m_IsOrthographic;

		// Token: 0x04000CAC RID: 3244
		private LODParameters m_LODParameters;

		// Token: 0x04000CAD RID: 3245
		private const int k_MaximumCullingPlaneCount = 10;

		// Token: 0x04000CAE RID: 3246
		public static readonly int maximumCullingPlaneCount = 10;

		// Token: 0x04000CAF RID: 3247
		[FixedBuffer(typeof(byte), 160)]
		internal ScriptableCullingParameters.<m_CullingPlanes>e__FixedBuffer m_CullingPlanes;

		// Token: 0x04000CB0 RID: 3248
		private int m_CullingPlaneCount;

		// Token: 0x04000CB1 RID: 3249
		private uint m_CullingMask;

		// Token: 0x04000CB2 RID: 3250
		private ulong m_SceneMask;

		// Token: 0x04000CB3 RID: 3251
		private const int k_LayerCount = 32;

		// Token: 0x04000CB4 RID: 3252
		public static readonly int layerCount = 32;

		// Token: 0x04000CB5 RID: 3253
		[FixedBuffer(typeof(float), 32)]
		internal ScriptableCullingParameters.<m_LayerFarCullDistances>e__FixedBuffer m_LayerFarCullDistances;

		// Token: 0x04000CB6 RID: 3254
		private int m_LayerCull;

		// Token: 0x04000CB7 RID: 3255
		private Matrix4x4 m_CullingMatrix;

		// Token: 0x04000CB8 RID: 3256
		private Vector3 m_Origin;

		// Token: 0x04000CB9 RID: 3257
		private float m_ShadowDistance;

		// Token: 0x04000CBA RID: 3258
		private float m_ShadowNearPlaneOffset;

		// Token: 0x04000CBB RID: 3259
		private CullingOptions m_CullingOptions;

		// Token: 0x04000CBC RID: 3260
		private ReflectionProbeSortingCriteria m_ReflectionProbeSortingCriteria;

		// Token: 0x04000CBD RID: 3261
		private CameraProperties m_CameraProperties;

		// Token: 0x04000CBE RID: 3262
		private float m_AccurateOcclusionThreshold;

		// Token: 0x04000CBF RID: 3263
		private int m_MaximumPortalCullingJobs;

		// Token: 0x04000CC0 RID: 3264
		private const int k_CullingJobCountLowerLimit = 1;

		// Token: 0x04000CC1 RID: 3265
		private const int k_CullingJobCountUpperLimit = 16;

		// Token: 0x04000CC2 RID: 3266
		private Matrix4x4 m_StereoViewMatrix;

		// Token: 0x04000CC3 RID: 3267
		private Matrix4x4 m_StereoProjectionMatrix;

		// Token: 0x04000CC4 RID: 3268
		private float m_StereoSeparationDistance;

		// Token: 0x04000CC5 RID: 3269
		private int m_maximumVisibleLights;

		// Token: 0x04000CC6 RID: 3270
		private bool m_ConservativeEnclosingSphere;

		// Token: 0x04000CC7 RID: 3271
		private int m_NumIterationsEnclosingSphere;

		// Token: 0x020003F7 RID: 1015
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 160)]
		public struct <m_CullingPlanes>e__FixedBuffer
		{
			// Token: 0x04000CC8 RID: 3272
			public byte FixedElementField;
		}

		// Token: 0x020003F8 RID: 1016
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 128)]
		public struct <m_LayerFarCullDistances>e__FixedBuffer
		{
			// Token: 0x04000CC9 RID: 3273
			public float FixedElementField;
		}
	}
}
