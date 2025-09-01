using System;
using System.Runtime.CompilerServices;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.AI
{
	// Token: 0x0200000D RID: 13
	[MovedFrom("UnityEngine")]
	public sealed class OffMeshLink : Behaviour
	{
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000A0 RID: 160
		// (set) Token: 0x060000A1 RID: 161
		public extern bool activated { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000A2 RID: 162
		public extern bool occupied { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000A3 RID: 163
		// (set) Token: 0x060000A4 RID: 164
		public extern float costOverride { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000A5 RID: 165
		// (set) Token: 0x060000A6 RID: 166
		public extern bool biDirectional { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x060000A7 RID: 167
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void UpdatePositions();

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x00002554 File Offset: 0x00000754
		// (set) Token: 0x060000A9 RID: 169 RVA: 0x0000256C File Offset: 0x0000076C
		[Obsolete("Use area instead.")]
		public int navMeshLayer
		{
			get
			{
				return this.area;
			}
			set
			{
				this.area = value;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000AA RID: 170
		// (set) Token: 0x060000AB RID: 171
		public extern int area { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000AC RID: 172
		// (set) Token: 0x060000AD RID: 173
		public extern bool autoUpdatePositions { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000AE RID: 174
		// (set) Token: 0x060000AF RID: 175
		public extern Transform startTransform { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000B0 RID: 176
		// (set) Token: 0x060000B1 RID: 177
		public extern Transform endTransform { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x060000B2 RID: 178 RVA: 0x000024AF File Offset: 0x000006AF
		public OffMeshLink()
		{
		}
	}
}
