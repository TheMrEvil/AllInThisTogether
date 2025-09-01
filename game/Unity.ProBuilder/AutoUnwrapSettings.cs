using System;
using UnityEngine.Serialization;

namespace UnityEngine.ProBuilder
{
	// Token: 0x02000004 RID: 4
	[Serializable]
	public struct AutoUnwrapSettings
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000026B8 File Offset: 0x000008B8
		public static AutoUnwrapSettings defaultAutoUnwrapSettings
		{
			get
			{
				AutoUnwrapSettings result = default(AutoUnwrapSettings);
				result.Reset();
				return result;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000024 RID: 36 RVA: 0x000026D5 File Offset: 0x000008D5
		// (set) Token: 0x06000025 RID: 37 RVA: 0x000026DD File Offset: 0x000008DD
		public bool useWorldSpace
		{
			get
			{
				return this.m_UseWorldSpace;
			}
			set
			{
				this.m_UseWorldSpace = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000026 RID: 38 RVA: 0x000026E6 File Offset: 0x000008E6
		// (set) Token: 0x06000027 RID: 39 RVA: 0x000026EE File Offset: 0x000008EE
		public bool flipU
		{
			get
			{
				return this.m_FlipU;
			}
			set
			{
				this.m_FlipU = value;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000028 RID: 40 RVA: 0x000026F7 File Offset: 0x000008F7
		// (set) Token: 0x06000029 RID: 41 RVA: 0x000026FF File Offset: 0x000008FF
		public bool flipV
		{
			get
			{
				return this.m_FlipV;
			}
			set
			{
				this.m_FlipV = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002708 File Offset: 0x00000908
		// (set) Token: 0x0600002B RID: 43 RVA: 0x00002710 File Offset: 0x00000910
		public bool swapUV
		{
			get
			{
				return this.m_SwapUV;
			}
			set
			{
				this.m_SwapUV = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002719 File Offset: 0x00000919
		// (set) Token: 0x0600002D RID: 45 RVA: 0x00002721 File Offset: 0x00000921
		public AutoUnwrapSettings.Fill fill
		{
			get
			{
				return this.m_Fill;
			}
			set
			{
				this.m_Fill = value;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600002E RID: 46 RVA: 0x0000272A File Offset: 0x0000092A
		// (set) Token: 0x0600002F RID: 47 RVA: 0x00002732 File Offset: 0x00000932
		public Vector2 scale
		{
			get
			{
				return this.m_Scale;
			}
			set
			{
				this.m_Scale = value;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000030 RID: 48 RVA: 0x0000273B File Offset: 0x0000093B
		// (set) Token: 0x06000031 RID: 49 RVA: 0x00002743 File Offset: 0x00000943
		public Vector2 offset
		{
			get
			{
				return this.m_Offset;
			}
			set
			{
				this.m_Offset = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000032 RID: 50 RVA: 0x0000274C File Offset: 0x0000094C
		// (set) Token: 0x06000033 RID: 51 RVA: 0x00002754 File Offset: 0x00000954
		public float rotation
		{
			get
			{
				return this.m_Rotation;
			}
			set
			{
				this.m_Rotation = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000034 RID: 52 RVA: 0x0000275D File Offset: 0x0000095D
		// (set) Token: 0x06000035 RID: 53 RVA: 0x00002765 File Offset: 0x00000965
		public AutoUnwrapSettings.Anchor anchor
		{
			get
			{
				return this.m_Anchor;
			}
			set
			{
				this.m_Anchor = value;
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002770 File Offset: 0x00000970
		public AutoUnwrapSettings(AutoUnwrapSettings unwrapSettings)
		{
			this.m_UseWorldSpace = unwrapSettings.m_UseWorldSpace;
			this.m_FlipU = unwrapSettings.m_FlipU;
			this.m_FlipV = unwrapSettings.m_FlipV;
			this.m_SwapUV = unwrapSettings.m_SwapUV;
			this.m_Fill = unwrapSettings.m_Fill;
			this.m_Scale = unwrapSettings.m_Scale;
			this.m_Offset = unwrapSettings.m_Offset;
			this.m_Rotation = unwrapSettings.m_Rotation;
			this.m_Anchor = unwrapSettings.m_Anchor;
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000037 RID: 55 RVA: 0x000027EC File Offset: 0x000009EC
		public static AutoUnwrapSettings tile
		{
			get
			{
				AutoUnwrapSettings result = default(AutoUnwrapSettings);
				result.Reset();
				return result;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000038 RID: 56 RVA: 0x0000280C File Offset: 0x00000A0C
		public static AutoUnwrapSettings fit
		{
			get
			{
				AutoUnwrapSettings result = default(AutoUnwrapSettings);
				result.Reset();
				result.fill = AutoUnwrapSettings.Fill.Fit;
				return result;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002834 File Offset: 0x00000A34
		public static AutoUnwrapSettings stretch
		{
			get
			{
				AutoUnwrapSettings result = default(AutoUnwrapSettings);
				result.Reset();
				result.fill = AutoUnwrapSettings.Fill.Stretch;
				return result;
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x0000285C File Offset: 0x00000A5C
		public void Reset()
		{
			this.m_UseWorldSpace = false;
			this.m_FlipU = false;
			this.m_FlipV = false;
			this.m_SwapUV = false;
			this.m_Fill = AutoUnwrapSettings.Fill.Tile;
			this.m_Scale = new Vector2(1f, 1f);
			this.m_Offset = new Vector2(0f, 0f);
			this.m_Rotation = 0f;
			this.m_Anchor = AutoUnwrapSettings.Anchor.None;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000028CC File Offset: 0x00000ACC
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"Use World Space: ",
				this.useWorldSpace.ToString(),
				"\nFlip U: ",
				this.flipU.ToString(),
				"\nFlip V: ",
				this.flipV.ToString(),
				"\nSwap UV: ",
				this.swapUV.ToString(),
				"\nFill Mode: ",
				this.fill.ToString(),
				"\nAnchor: ",
				this.anchor.ToString(),
				"\nScale: ",
				this.scale.ToString(),
				"\nOffset: ",
				this.offset.ToString(),
				"\nRotation: ",
				this.rotation.ToString()
			});
		}

		// Token: 0x04000003 RID: 3
		[SerializeField]
		[FormerlySerializedAs("useWorldSpace")]
		private bool m_UseWorldSpace;

		// Token: 0x04000004 RID: 4
		[SerializeField]
		[FormerlySerializedAs("flipU")]
		private bool m_FlipU;

		// Token: 0x04000005 RID: 5
		[SerializeField]
		[FormerlySerializedAs("flipV")]
		private bool m_FlipV;

		// Token: 0x04000006 RID: 6
		[SerializeField]
		[FormerlySerializedAs("swapUV")]
		private bool m_SwapUV;

		// Token: 0x04000007 RID: 7
		[SerializeField]
		[FormerlySerializedAs("fill")]
		private AutoUnwrapSettings.Fill m_Fill;

		// Token: 0x04000008 RID: 8
		[SerializeField]
		[FormerlySerializedAs("scale")]
		private Vector2 m_Scale;

		// Token: 0x04000009 RID: 9
		[SerializeField]
		[FormerlySerializedAs("offset")]
		private Vector2 m_Offset;

		// Token: 0x0400000A RID: 10
		[SerializeField]
		[FormerlySerializedAs("rotation")]
		private float m_Rotation;

		// Token: 0x0400000B RID: 11
		[SerializeField]
		[FormerlySerializedAs("anchor")]
		private AutoUnwrapSettings.Anchor m_Anchor;

		// Token: 0x02000091 RID: 145
		public enum Anchor
		{
			// Token: 0x04000284 RID: 644
			UpperLeft,
			// Token: 0x04000285 RID: 645
			UpperCenter,
			// Token: 0x04000286 RID: 646
			UpperRight,
			// Token: 0x04000287 RID: 647
			MiddleLeft,
			// Token: 0x04000288 RID: 648
			MiddleCenter,
			// Token: 0x04000289 RID: 649
			MiddleRight,
			// Token: 0x0400028A RID: 650
			LowerLeft,
			// Token: 0x0400028B RID: 651
			LowerCenter,
			// Token: 0x0400028C RID: 652
			LowerRight,
			// Token: 0x0400028D RID: 653
			None
		}

		// Token: 0x02000092 RID: 146
		public enum Fill
		{
			// Token: 0x0400028F RID: 655
			Fit,
			// Token: 0x04000290 RID: 656
			Tile,
			// Token: 0x04000291 RID: 657
			Stretch
		}
	}
}
