using System;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000010 RID: 16
	[RequiredByNativeCode]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class AnimationEvent
	{
		// Token: 0x0600006E RID: 110 RVA: 0x00002324 File Offset: 0x00000524
		public AnimationEvent()
		{
			this.m_Time = 0f;
			this.m_FunctionName = "";
			this.m_StringParameter = "";
			this.m_ObjectReferenceParameter = null;
			this.m_FloatParameter = 0f;
			this.m_IntParameter = 0;
			this.m_MessageOptions = 0;
			this.m_Source = AnimationEventSource.NoSource;
			this.m_StateSender = null;
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00002388 File Offset: 0x00000588
		// (set) Token: 0x06000070 RID: 112 RVA: 0x000023A0 File Offset: 0x000005A0
		[Obsolete("Use stringParameter instead")]
		public string data
		{
			get
			{
				return this.m_StringParameter;
			}
			set
			{
				this.m_StringParameter = value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000071 RID: 113 RVA: 0x000023AC File Offset: 0x000005AC
		// (set) Token: 0x06000072 RID: 114 RVA: 0x000023A0 File Offset: 0x000005A0
		public string stringParameter
		{
			get
			{
				return this.m_StringParameter;
			}
			set
			{
				this.m_StringParameter = value;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000073 RID: 115 RVA: 0x000023C4 File Offset: 0x000005C4
		// (set) Token: 0x06000074 RID: 116 RVA: 0x000023DC File Offset: 0x000005DC
		public float floatParameter
		{
			get
			{
				return this.m_FloatParameter;
			}
			set
			{
				this.m_FloatParameter = value;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000075 RID: 117 RVA: 0x000023E8 File Offset: 0x000005E8
		// (set) Token: 0x06000076 RID: 118 RVA: 0x00002400 File Offset: 0x00000600
		public int intParameter
		{
			get
			{
				return this.m_IntParameter;
			}
			set
			{
				this.m_IntParameter = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000077 RID: 119 RVA: 0x0000240C File Offset: 0x0000060C
		// (set) Token: 0x06000078 RID: 120 RVA: 0x00002424 File Offset: 0x00000624
		public Object objectReferenceParameter
		{
			get
			{
				return this.m_ObjectReferenceParameter;
			}
			set
			{
				this.m_ObjectReferenceParameter = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00002430 File Offset: 0x00000630
		// (set) Token: 0x0600007A RID: 122 RVA: 0x00002448 File Offset: 0x00000648
		public string functionName
		{
			get
			{
				return this.m_FunctionName;
			}
			set
			{
				this.m_FunctionName = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00002454 File Offset: 0x00000654
		// (set) Token: 0x0600007C RID: 124 RVA: 0x0000246C File Offset: 0x0000066C
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

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00002478 File Offset: 0x00000678
		// (set) Token: 0x0600007E RID: 126 RVA: 0x00002490 File Offset: 0x00000690
		public SendMessageOptions messageOptions
		{
			get
			{
				return (SendMessageOptions)this.m_MessageOptions;
			}
			set
			{
				this.m_MessageOptions = (int)value;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600007F RID: 127 RVA: 0x0000249C File Offset: 0x0000069C
		public bool isFiredByLegacy
		{
			get
			{
				return this.m_Source == AnimationEventSource.Legacy;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000080 RID: 128 RVA: 0x000024B8 File Offset: 0x000006B8
		public bool isFiredByAnimator
		{
			get
			{
				return this.m_Source == AnimationEventSource.Animator;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000081 RID: 129 RVA: 0x000024D4 File Offset: 0x000006D4
		public AnimationState animationState
		{
			get
			{
				bool flag = !this.isFiredByLegacy;
				if (flag)
				{
					Debug.LogError("AnimationEvent was not fired by Animation component, you shouldn't use AnimationEvent.animationState");
				}
				return this.m_StateSender;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00002504 File Offset: 0x00000704
		public AnimatorStateInfo animatorStateInfo
		{
			get
			{
				bool flag = !this.isFiredByAnimator;
				if (flag)
				{
					Debug.LogError("AnimationEvent was not fired by Animator component, you shouldn't use AnimationEvent.animatorStateInfo");
				}
				return this.m_AnimatorStateInfo;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00002534 File Offset: 0x00000734
		public AnimatorClipInfo animatorClipInfo
		{
			get
			{
				bool flag = !this.isFiredByAnimator;
				if (flag)
				{
					Debug.LogError("AnimationEvent was not fired by Animator component, you shouldn't use AnimationEvent.animatorClipInfo");
				}
				return this.m_AnimatorClipInfo;
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00002564 File Offset: 0x00000764
		internal int GetHash()
		{
			int hashCode = this.functionName.GetHashCode();
			return 33 * hashCode + this.time.GetHashCode();
		}

		// Token: 0x04000019 RID: 25
		internal float m_Time;

		// Token: 0x0400001A RID: 26
		internal string m_FunctionName;

		// Token: 0x0400001B RID: 27
		internal string m_StringParameter;

		// Token: 0x0400001C RID: 28
		internal Object m_ObjectReferenceParameter;

		// Token: 0x0400001D RID: 29
		internal float m_FloatParameter;

		// Token: 0x0400001E RID: 30
		internal int m_IntParameter;

		// Token: 0x0400001F RID: 31
		internal int m_MessageOptions;

		// Token: 0x04000020 RID: 32
		internal AnimationEventSource m_Source;

		// Token: 0x04000021 RID: 33
		internal AnimationState m_StateSender;

		// Token: 0x04000022 RID: 34
		internal AnimatorStateInfo m_AnimatorStateInfo;

		// Token: 0x04000023 RID: 35
		internal AnimatorClipInfo m_AnimatorClipInfo;
	}
}
