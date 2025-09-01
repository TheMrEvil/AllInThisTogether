using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Tilemaps
{
	// Token: 0x02000015 RID: 21
	[RequiredByNativeCode]
	[NativeType(Header = "Modules/Tilemap/TilemapScripting.h")]
	public struct TileAnimationData
	{
		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x00003144 File Offset: 0x00001344
		// (set) Token: 0x060000F8 RID: 248 RVA: 0x0000315C File Offset: 0x0000135C
		public Sprite[] animatedSprites
		{
			get
			{
				return this.m_AnimatedSprites;
			}
			set
			{
				this.m_AnimatedSprites = value;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00003168 File Offset: 0x00001368
		// (set) Token: 0x060000FA RID: 250 RVA: 0x00003180 File Offset: 0x00001380
		public float animationSpeed
		{
			get
			{
				return this.m_AnimationSpeed;
			}
			set
			{
				this.m_AnimationSpeed = value;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000FB RID: 251 RVA: 0x0000318C File Offset: 0x0000138C
		// (set) Token: 0x060000FC RID: 252 RVA: 0x000031A4 File Offset: 0x000013A4
		public float animationStartTime
		{
			get
			{
				return this.m_AnimationStartTime;
			}
			set
			{
				this.m_AnimationStartTime = value;
			}
		}

		// Token: 0x0400004D RID: 77
		private Sprite[] m_AnimatedSprites;

		// Token: 0x0400004E RID: 78
		private float m_AnimationSpeed;

		// Token: 0x0400004F RID: 79
		private float m_AnimationStartTime;
	}
}
