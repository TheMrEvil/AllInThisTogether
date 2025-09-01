using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.TextCore.LowLevel
{
	// Token: 0x02000021 RID: 33
	[UsedByNativeCode]
	[Serializable]
	internal struct GlyphAnchorPoint
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000144 RID: 324 RVA: 0x00004EC8 File Offset: 0x000030C8
		// (set) Token: 0x06000145 RID: 325 RVA: 0x00004EE0 File Offset: 0x000030E0
		public float xCoordinate
		{
			get
			{
				return this.m_XCoordinate;
			}
			set
			{
				this.m_XCoordinate = value;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000146 RID: 326 RVA: 0x00004EEC File Offset: 0x000030EC
		// (set) Token: 0x06000147 RID: 327 RVA: 0x00004F04 File Offset: 0x00003104
		public float yCoordinate
		{
			get
			{
				return this.m_YCoordinate;
			}
			set
			{
				this.m_YCoordinate = value;
			}
		}

		// Token: 0x040000C4 RID: 196
		[NativeName("xPositionAdjustment")]
		[SerializeField]
		private float m_XCoordinate;

		// Token: 0x040000C5 RID: 197
		[NativeName("yPositionAdjustment")]
		[SerializeField]
		private float m_YCoordinate;
	}
}
