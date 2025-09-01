using System;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000032 RID: 50
	[NativeType(CodegenOptions.Custom, "MonoHumanLimit")]
	[NativeHeader("Modules/Animation/HumanDescription.h")]
	[NativeHeader("Modules/Animation/ScriptBindings/AvatarBuilder.bindings.h")]
	public struct HumanLimit
	{
		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000222 RID: 546 RVA: 0x00003B64 File Offset: 0x00001D64
		// (set) Token: 0x06000223 RID: 547 RVA: 0x00003B7F File Offset: 0x00001D7F
		public bool useDefaultValues
		{
			get
			{
				return this.m_UseDefaultValues != 0;
			}
			set
			{
				this.m_UseDefaultValues = (value ? 1 : 0);
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000224 RID: 548 RVA: 0x00003B90 File Offset: 0x00001D90
		// (set) Token: 0x06000225 RID: 549 RVA: 0x00003BA8 File Offset: 0x00001DA8
		public Vector3 min
		{
			get
			{
				return this.m_Min;
			}
			set
			{
				this.m_Min = value;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000226 RID: 550 RVA: 0x00003BB4 File Offset: 0x00001DB4
		// (set) Token: 0x06000227 RID: 551 RVA: 0x00003BCC File Offset: 0x00001DCC
		public Vector3 max
		{
			get
			{
				return this.m_Max;
			}
			set
			{
				this.m_Max = value;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000228 RID: 552 RVA: 0x00003BD8 File Offset: 0x00001DD8
		// (set) Token: 0x06000229 RID: 553 RVA: 0x00003BF0 File Offset: 0x00001DF0
		public Vector3 center
		{
			get
			{
				return this.m_Center;
			}
			set
			{
				this.m_Center = value;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600022A RID: 554 RVA: 0x00003BFC File Offset: 0x00001DFC
		// (set) Token: 0x0600022B RID: 555 RVA: 0x00003C14 File Offset: 0x00001E14
		public float axisLength
		{
			get
			{
				return this.m_AxisLength;
			}
			set
			{
				this.m_AxisLength = value;
			}
		}

		// Token: 0x0400010E RID: 270
		private Vector3 m_Min;

		// Token: 0x0400010F RID: 271
		private Vector3 m_Max;

		// Token: 0x04000110 RID: 272
		private Vector3 m_Center;

		// Token: 0x04000111 RID: 273
		private float m_AxisLength;

		// Token: 0x04000112 RID: 274
		private int m_UseDefaultValues;
	}
}
