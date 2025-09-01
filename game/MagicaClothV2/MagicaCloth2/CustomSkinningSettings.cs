using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace MagicaCloth2
{
	// Token: 0x0200005A RID: 90
	[Serializable]
	public class CustomSkinningSettings : IValid, IDataValidate, ITransform
	{
		// Token: 0x06000119 RID: 281 RVA: 0x00005305 File Offset: 0x00003505
		public void DataValidate()
		{
		}

		// Token: 0x0600011A RID: 282 RVA: 0x0000D65C File Offset: 0x0000B85C
		public bool IsValid()
		{
			if (!this.enable)
			{
				return false;
			}
			if (this.skinningBones.Count == 0)
			{
				return false;
			}
			return this.skinningBones.Any((Transform n) => n != null);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x0000D6B1 File Offset: 0x0000B8B1
		public CustomSkinningSettings Clone()
		{
			return new CustomSkinningSettings
			{
				enable = this.enable,
				skinningBones = new List<Transform>(this.skinningBones)
			};
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00005307 File Offset: 0x00003507
		public override int GetHashCode()
		{
			return 0;
		}

		// Token: 0x0600011D RID: 285 RVA: 0x0000D6D8 File Offset: 0x0000B8D8
		public void GetUsedTransform(HashSet<Transform> transformSet)
		{
			foreach (Transform transform in this.skinningBones)
			{
				if (transform)
				{
					transformSet.Add(transform);
				}
			}
		}

		// Token: 0x0600011E RID: 286 RVA: 0x0000D734 File Offset: 0x0000B934
		public void ReplaceTransform(Dictionary<int, Transform> replaceDict)
		{
			for (int i = 0; i < this.skinningBones.Count; i++)
			{
				Transform transform = this.skinningBones[i];
				if (transform && replaceDict.ContainsKey(transform.GetInstanceID()))
				{
					this.skinningBones[i] = replaceDict[transform.GetInstanceID()];
				}
			}
		}

		// Token: 0x0600011F RID: 287 RVA: 0x0000D792 File Offset: 0x0000B992
		public CustomSkinningSettings()
		{
		}

		// Token: 0x04000222 RID: 546
		public bool enable;

		// Token: 0x04000223 RID: 547
		public List<Transform> skinningBones = new List<Transform>();

		// Token: 0x0200005B RID: 91
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000120 RID: 288 RVA: 0x0000D7A5 File Offset: 0x0000B9A5
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000121 RID: 289 RVA: 0x00002058 File Offset: 0x00000258
			public <>c()
			{
			}

			// Token: 0x06000122 RID: 290 RVA: 0x000046CE File Offset: 0x000028CE
			internal bool <IsValid>b__3_0(Transform n)
			{
				return n != null;
			}

			// Token: 0x04000224 RID: 548
			public static readonly CustomSkinningSettings.<>c <>9 = new CustomSkinningSettings.<>c();

			// Token: 0x04000225 RID: 549
			public static Func<Transform, bool> <>9__3_0;
		}
	}
}
