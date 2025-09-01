using System;

namespace UnityEngine.Playables
{
	// Token: 0x02000430 RID: 1072
	public struct FrameData
	{
		// Token: 0x06002560 RID: 9568 RVA: 0x0003F24C File Offset: 0x0003D44C
		private bool HasFlags(FrameData.Flags flag)
		{
			return (this.m_Flags & flag) == flag;
		}

		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x06002561 RID: 9569 RVA: 0x0003F26C File Offset: 0x0003D46C
		public ulong frameId
		{
			get
			{
				return this.m_FrameID;
			}
		}

		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x06002562 RID: 9570 RVA: 0x0003F284 File Offset: 0x0003D484
		public float deltaTime
		{
			get
			{
				return (float)this.m_DeltaTime;
			}
		}

		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x06002563 RID: 9571 RVA: 0x0003F2A0 File Offset: 0x0003D4A0
		public float weight
		{
			get
			{
				return this.m_Weight;
			}
		}

		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x06002564 RID: 9572 RVA: 0x0003F2B8 File Offset: 0x0003D4B8
		public float effectiveWeight
		{
			get
			{
				return this.m_EffectiveWeight;
			}
		}

		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x06002565 RID: 9573 RVA: 0x0003F2D0 File Offset: 0x0003D4D0
		[Obsolete("effectiveParentDelay is obsolete; use a custom ScriptPlayable to implement this feature", false)]
		public double effectiveParentDelay
		{
			get
			{
				return this.m_EffectiveParentDelay;
			}
		}

		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x06002566 RID: 9574 RVA: 0x0003F2E8 File Offset: 0x0003D4E8
		public float effectiveParentSpeed
		{
			get
			{
				return this.m_EffectiveParentSpeed;
			}
		}

		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x06002567 RID: 9575 RVA: 0x0003F300 File Offset: 0x0003D500
		public float effectiveSpeed
		{
			get
			{
				return this.m_EffectiveSpeed;
			}
		}

		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x06002568 RID: 9576 RVA: 0x0003F318 File Offset: 0x0003D518
		public FrameData.EvaluationType evaluationType
		{
			get
			{
				return this.HasFlags(FrameData.Flags.Evaluate) ? FrameData.EvaluationType.Evaluate : FrameData.EvaluationType.Playback;
			}
		}

		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x06002569 RID: 9577 RVA: 0x0003F338 File Offset: 0x0003D538
		public bool seekOccurred
		{
			get
			{
				return this.HasFlags(FrameData.Flags.SeekOccured);
			}
		}

		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x0600256A RID: 9578 RVA: 0x0003F354 File Offset: 0x0003D554
		public bool timeLooped
		{
			get
			{
				return this.HasFlags(FrameData.Flags.Loop);
			}
		}

		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x0600256B RID: 9579 RVA: 0x0003F370 File Offset: 0x0003D570
		public bool timeHeld
		{
			get
			{
				return this.HasFlags(FrameData.Flags.Hold);
			}
		}

		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x0600256C RID: 9580 RVA: 0x0003F38C File Offset: 0x0003D58C
		public PlayableOutput output
		{
			get
			{
				return this.m_Output;
			}
		}

		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x0600256D RID: 9581 RVA: 0x0003F3A4 File Offset: 0x0003D5A4
		public PlayState effectivePlayState
		{
			get
			{
				bool flag = this.HasFlags(FrameData.Flags.EffectivePlayStateDelayed);
				PlayState result;
				if (flag)
				{
					result = PlayState.Delayed;
				}
				else
				{
					bool flag2 = this.HasFlags(FrameData.Flags.EffectivePlayStatePlaying);
					if (flag2)
					{
						result = PlayState.Playing;
					}
					else
					{
						result = PlayState.Paused;
					}
				}
				return result;
			}
		}

		// Token: 0x04000DEF RID: 3567
		internal ulong m_FrameID;

		// Token: 0x04000DF0 RID: 3568
		internal double m_DeltaTime;

		// Token: 0x04000DF1 RID: 3569
		internal float m_Weight;

		// Token: 0x04000DF2 RID: 3570
		internal float m_EffectiveWeight;

		// Token: 0x04000DF3 RID: 3571
		internal double m_EffectiveParentDelay;

		// Token: 0x04000DF4 RID: 3572
		internal float m_EffectiveParentSpeed;

		// Token: 0x04000DF5 RID: 3573
		internal float m_EffectiveSpeed;

		// Token: 0x04000DF6 RID: 3574
		internal FrameData.Flags m_Flags;

		// Token: 0x04000DF7 RID: 3575
		internal PlayableOutput m_Output;

		// Token: 0x02000431 RID: 1073
		[Flags]
		internal enum Flags
		{
			// Token: 0x04000DF9 RID: 3577
			Evaluate = 1,
			// Token: 0x04000DFA RID: 3578
			SeekOccured = 2,
			// Token: 0x04000DFB RID: 3579
			Loop = 4,
			// Token: 0x04000DFC RID: 3580
			Hold = 8,
			// Token: 0x04000DFD RID: 3581
			EffectivePlayStateDelayed = 16,
			// Token: 0x04000DFE RID: 3582
			EffectivePlayStatePlaying = 32
		}

		// Token: 0x02000432 RID: 1074
		public enum EvaluationType
		{
			// Token: 0x04000E00 RID: 3584
			Evaluate,
			// Token: 0x04000E01 RID: 3585
			Playback
		}
	}
}
