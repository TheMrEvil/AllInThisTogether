using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020000E1 RID: 225
	[RequiredByNativeCode]
	public struct Keyframe
	{
		// Token: 0x06000387 RID: 903 RVA: 0x00005F3C File Offset: 0x0000413C
		public Keyframe(float time, float value)
		{
			this.m_Time = time;
			this.m_Value = value;
			this.m_InTangent = 0f;
			this.m_OutTangent = 0f;
			this.m_WeightedMode = 0;
			this.m_InWeight = 0f;
			this.m_OutWeight = 0f;
		}

		// Token: 0x06000388 RID: 904 RVA: 0x00005F8B File Offset: 0x0000418B
		public Keyframe(float time, float value, float inTangent, float outTangent)
		{
			this.m_Time = time;
			this.m_Value = value;
			this.m_InTangent = inTangent;
			this.m_OutTangent = outTangent;
			this.m_WeightedMode = 0;
			this.m_InWeight = 0f;
			this.m_OutWeight = 0f;
		}

		// Token: 0x06000389 RID: 905 RVA: 0x00005FC8 File Offset: 0x000041C8
		public Keyframe(float time, float value, float inTangent, float outTangent, float inWeight, float outWeight)
		{
			this.m_Time = time;
			this.m_Value = value;
			this.m_InTangent = inTangent;
			this.m_OutTangent = outTangent;
			this.m_WeightedMode = 3;
			this.m_InWeight = inWeight;
			this.m_OutWeight = outWeight;
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600038A RID: 906 RVA: 0x00006000 File Offset: 0x00004200
		// (set) Token: 0x0600038B RID: 907 RVA: 0x00006018 File Offset: 0x00004218
		public float time
		{
			get
			{
				return this.m_Time;
			}
			set
			{
				this.m_Time = value;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600038C RID: 908 RVA: 0x00006024 File Offset: 0x00004224
		// (set) Token: 0x0600038D RID: 909 RVA: 0x0000603C File Offset: 0x0000423C
		public float value
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				this.m_Value = value;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600038E RID: 910 RVA: 0x00006048 File Offset: 0x00004248
		// (set) Token: 0x0600038F RID: 911 RVA: 0x00006060 File Offset: 0x00004260
		public float inTangent
		{
			get
			{
				return this.m_InTangent;
			}
			set
			{
				this.m_InTangent = value;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000390 RID: 912 RVA: 0x0000606C File Offset: 0x0000426C
		// (set) Token: 0x06000391 RID: 913 RVA: 0x00006084 File Offset: 0x00004284
		public float outTangent
		{
			get
			{
				return this.m_OutTangent;
			}
			set
			{
				this.m_OutTangent = value;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000392 RID: 914 RVA: 0x00006090 File Offset: 0x00004290
		// (set) Token: 0x06000393 RID: 915 RVA: 0x000060A8 File Offset: 0x000042A8
		public float inWeight
		{
			get
			{
				return this.m_InWeight;
			}
			set
			{
				this.m_InWeight = value;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000394 RID: 916 RVA: 0x000060B4 File Offset: 0x000042B4
		// (set) Token: 0x06000395 RID: 917 RVA: 0x000060CC File Offset: 0x000042CC
		public float outWeight
		{
			get
			{
				return this.m_OutWeight;
			}
			set
			{
				this.m_OutWeight = value;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000396 RID: 918 RVA: 0x000060D8 File Offset: 0x000042D8
		// (set) Token: 0x06000397 RID: 919 RVA: 0x000060F0 File Offset: 0x000042F0
		public WeightedMode weightedMode
		{
			get
			{
				return (WeightedMode)this.m_WeightedMode;
			}
			set
			{
				this.m_WeightedMode = (int)value;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000398 RID: 920 RVA: 0x000060FC File Offset: 0x000042FC
		// (set) Token: 0x06000399 RID: 921 RVA: 0x00006114 File Offset: 0x00004314
		[Obsolete("Use AnimationUtility.SetKeyLeftTangentMode, AnimationUtility.SetKeyRightTangentMode, AnimationUtility.GetKeyLeftTangentMode or AnimationUtility.GetKeyRightTangentMode instead.")]
		public int tangentMode
		{
			get
			{
				return this.tangentModeInternal;
			}
			set
			{
				this.tangentModeInternal = value;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600039A RID: 922 RVA: 0x00006120 File Offset: 0x00004320
		// (set) Token: 0x0600039B RID: 923 RVA: 0x00004563 File Offset: 0x00002763
		internal int tangentModeInternal
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x040002E8 RID: 744
		private float m_Time;

		// Token: 0x040002E9 RID: 745
		private float m_Value;

		// Token: 0x040002EA RID: 746
		private float m_InTangent;

		// Token: 0x040002EB RID: 747
		private float m_OutTangent;

		// Token: 0x040002EC RID: 748
		private int m_WeightedMode;

		// Token: 0x040002ED RID: 749
		private float m_InWeight;

		// Token: 0x040002EE RID: 750
		private float m_OutWeight;
	}
}
