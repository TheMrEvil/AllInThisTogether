using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000016 RID: 22
	[RequiredByNativeCode(Optional = true, GenerateProxy = true)]
	[NativeHeader("Modules/Physics2D/Public/Collider2D.h")]
	[NativeClass("ContactFilter", "struct ContactFilter;")]
	[Serializable]
	public struct ContactFilter2D
	{
		// Token: 0x0600020F RID: 527 RVA: 0x00006538 File Offset: 0x00004738
		public ContactFilter2D NoFilter()
		{
			this.useTriggers = true;
			this.useLayerMask = false;
			this.layerMask = -1;
			this.useDepth = false;
			this.useOutsideDepth = false;
			this.minDepth = float.NegativeInfinity;
			this.maxDepth = float.PositiveInfinity;
			this.useNormalAngle = false;
			this.useOutsideNormalAngle = false;
			this.minNormalAngle = 0f;
			this.maxNormalAngle = 359.9999f;
			return this;
		}

		// Token: 0x06000210 RID: 528 RVA: 0x000065B2 File Offset: 0x000047B2
		private void CheckConsistency()
		{
			ContactFilter2D.CheckConsistency_Injected(ref this);
		}

		// Token: 0x06000211 RID: 529 RVA: 0x000065BA File Offset: 0x000047BA
		public void ClearLayerMask()
		{
			this.useLayerMask = false;
		}

		// Token: 0x06000212 RID: 530 RVA: 0x000065C4 File Offset: 0x000047C4
		public void SetLayerMask(LayerMask layerMask)
		{
			this.layerMask = layerMask;
			this.useLayerMask = true;
		}

		// Token: 0x06000213 RID: 531 RVA: 0x000065D5 File Offset: 0x000047D5
		public void ClearDepth()
		{
			this.useDepth = false;
		}

		// Token: 0x06000214 RID: 532 RVA: 0x000065DF File Offset: 0x000047DF
		public void SetDepth(float minDepth, float maxDepth)
		{
			this.minDepth = minDepth;
			this.maxDepth = maxDepth;
			this.useDepth = true;
			this.CheckConsistency();
		}

		// Token: 0x06000215 RID: 533 RVA: 0x000065FE File Offset: 0x000047FE
		public void ClearNormalAngle()
		{
			this.useNormalAngle = false;
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00006608 File Offset: 0x00004808
		public void SetNormalAngle(float minNormalAngle, float maxNormalAngle)
		{
			this.minNormalAngle = minNormalAngle;
			this.maxNormalAngle = maxNormalAngle;
			this.useNormalAngle = true;
			this.CheckConsistency();
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000217 RID: 535 RVA: 0x00006628 File Offset: 0x00004828
		public bool isFiltering
		{
			get
			{
				return !this.useTriggers || this.useLayerMask || this.useDepth || this.useNormalAngle;
			}
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000665C File Offset: 0x0000485C
		public bool IsFilteringTrigger([Writable] Collider2D collider)
		{
			return !this.useTriggers && collider.isTrigger;
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00006680 File Offset: 0x00004880
		public bool IsFilteringLayerMask(GameObject obj)
		{
			return this.useLayerMask && (this.layerMask & 1 << obj.layer) == 0;
		}

		// Token: 0x0600021A RID: 538 RVA: 0x000066B8 File Offset: 0x000048B8
		public bool IsFilteringDepth(GameObject obj)
		{
			bool flag = !this.useDepth;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = this.minDepth > this.maxDepth;
				if (flag2)
				{
					float num = this.minDepth;
					this.minDepth = this.maxDepth;
					this.maxDepth = num;
				}
				float z = obj.transform.position.z;
				bool flag3 = z < this.minDepth || z > this.maxDepth;
				bool flag4 = this.useOutsideDepth;
				if (flag4)
				{
					result = !flag3;
				}
				else
				{
					result = flag3;
				}
			}
			return result;
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00006748 File Offset: 0x00004948
		public bool IsFilteringNormalAngle(Vector2 normal)
		{
			return ContactFilter2D.IsFilteringNormalAngle_Injected(ref this, ref normal);
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00006754 File Offset: 0x00004954
		public bool IsFilteringNormalAngle(float angle)
		{
			return this.IsFilteringNormalAngleUsingAngle(angle);
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000676D File Offset: 0x0000496D
		private bool IsFilteringNormalAngleUsingAngle(float angle)
		{
			return ContactFilter2D.IsFilteringNormalAngleUsingAngle_Injected(ref this, angle);
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00006778 File Offset: 0x00004978
		internal static ContactFilter2D CreateLegacyFilter(int layerMask, float minDepth, float maxDepth)
		{
			ContactFilter2D result = default(ContactFilter2D);
			result.useTriggers = Physics2D.queriesHitTriggers;
			result.SetLayerMask(layerMask);
			result.SetDepth(minDepth, maxDepth);
			return result;
		}

		// Token: 0x0600021F RID: 543
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CheckConsistency_Injected(ref ContactFilter2D _unity_self);

		// Token: 0x06000220 RID: 544
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsFilteringNormalAngle_Injected(ref ContactFilter2D _unity_self, ref Vector2 normal);

		// Token: 0x06000221 RID: 545
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsFilteringNormalAngleUsingAngle_Injected(ref ContactFilter2D _unity_self, float angle);

		// Token: 0x0400004F RID: 79
		[NativeName("m_UseTriggers")]
		public bool useTriggers;

		// Token: 0x04000050 RID: 80
		[NativeName("m_UseLayerMask")]
		public bool useLayerMask;

		// Token: 0x04000051 RID: 81
		[NativeName("m_UseDepth")]
		public bool useDepth;

		// Token: 0x04000052 RID: 82
		[NativeName("m_UseOutsideDepth")]
		public bool useOutsideDepth;

		// Token: 0x04000053 RID: 83
		[NativeName("m_UseNormalAngle")]
		public bool useNormalAngle;

		// Token: 0x04000054 RID: 84
		[NativeName("m_UseOutsideNormalAngle")]
		public bool useOutsideNormalAngle;

		// Token: 0x04000055 RID: 85
		[NativeName("m_LayerMask")]
		public LayerMask layerMask;

		// Token: 0x04000056 RID: 86
		[NativeName("m_MinDepth")]
		public float minDepth;

		// Token: 0x04000057 RID: 87
		[NativeName("m_MaxDepth")]
		public float maxDepth;

		// Token: 0x04000058 RID: 88
		[NativeName("m_MinNormalAngle")]
		public float minNormalAngle;

		// Token: 0x04000059 RID: 89
		[NativeName("m_MaxNormalAngle")]
		public float maxNormalAngle;

		// Token: 0x0400005A RID: 90
		public const float NormalAngleUpperLimit = 359.9999f;
	}
}
