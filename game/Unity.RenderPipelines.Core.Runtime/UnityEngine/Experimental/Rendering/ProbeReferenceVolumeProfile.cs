using System;
using UnityEngine.Rendering;

namespace UnityEngine.Experimental.Rendering
{
	// Token: 0x02000010 RID: 16
	public sealed class ProbeReferenceVolumeProfile : ScriptableObject
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x0000619D File Offset: 0x0000439D
		public int cellSizeInBricks
		{
			get
			{
				return (int)Mathf.Pow(3f, (float)this.simplificationLevels);
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x000061B1 File Offset: 0x000043B1
		public int maxSubdivision
		{
			get
			{
				return this.simplificationLevels + 1;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x000061BB File Offset: 0x000043BB
		public float minBrickSize
		{
			get
			{
				return Mathf.Max(0.01f, this.minDistanceBetweenProbes * 3f);
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x000061D3 File Offset: 0x000043D3
		public float cellSizeInMeters
		{
			get
			{
				return (float)this.cellSizeInBricks * this.minBrickSize;
			}
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000061E3 File Offset: 0x000043E3
		private void OnEnable()
		{
			ProbeReferenceVolumeProfile.Version version = this.version;
			CoreUtils.GetLastEnumValue<ProbeReferenceVolumeProfile.Version>();
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000061F2 File Offset: 0x000043F2
		public bool IsEquivalent(ProbeReferenceVolumeProfile otherProfile)
		{
			return this.minDistanceBetweenProbes == otherProfile.minDistanceBetweenProbes && this.cellSizeInMeters == otherProfile.cellSizeInMeters && this.simplificationLevels == otherProfile.simplificationLevels;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00006220 File Offset: 0x00004420
		public ProbeReferenceVolumeProfile()
		{
		}

		// Token: 0x0400007A RID: 122
		[SerializeField]
		private ProbeReferenceVolumeProfile.Version version = CoreUtils.GetLastEnumValue<ProbeReferenceVolumeProfile.Version>();

		// Token: 0x0400007B RID: 123
		[Range(2f, 5f)]
		public int simplificationLevels = 3;

		// Token: 0x0400007C RID: 124
		[Min(0.1f)]
		public float minDistanceBetweenProbes = 1f;

		// Token: 0x02000120 RID: 288
		internal enum Version
		{
			// Token: 0x040004B3 RID: 1203
			Initial
		}
	}
}
