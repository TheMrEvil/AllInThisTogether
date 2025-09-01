using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Agoston_R.Aim_Assist_Pro.Scripts.AimAssistCode.Model
{
	// Token: 0x02000006 RID: 6
	public struct AimAssistResult
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000014 RID: 20 RVA: 0x000022D6 File Offset: 0x000004D6
		public readonly float RotationAdditionInDegrees
		{
			[CompilerGenerated]
			get
			{
				return this.<RotationAdditionInDegrees>k__BackingField;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000015 RID: 21 RVA: 0x000022DE File Offset: 0x000004DE
		public readonly Vector3 TurnAddition
		{
			[CompilerGenerated]
			get
			{
				return this.<TurnAddition>k__BackingField;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000016 RID: 22 RVA: 0x000022E6 File Offset: 0x000004E6
		public readonly float PitchAdditionInDegrees
		{
			[CompilerGenerated]
			get
			{
				return this.<PitchAdditionInDegrees>k__BackingField;
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000022EE File Offset: 0x000004EE
		public AimAssistResult(float rotationAdditionInDegrees, Vector3 turnAddition, float pitchAdditionInDegrees)
		{
			this.RotationAdditionInDegrees = rotationAdditionInDegrees;
			this.TurnAddition = turnAddition;
			this.PitchAdditionInDegrees = pitchAdditionInDegrees;
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002308 File Offset: 0x00000508
		public static AimAssistResult Empty
		{
			get
			{
				return default(AimAssistResult);
			}
		}

		// Token: 0x0400000E RID: 14
		[CompilerGenerated]
		private readonly float <RotationAdditionInDegrees>k__BackingField;

		// Token: 0x0400000F RID: 15
		[CompilerGenerated]
		private readonly Vector3 <TurnAddition>k__BackingField;

		// Token: 0x04000010 RID: 16
		[CompilerGenerated]
		private readonly float <PitchAdditionInDegrees>k__BackingField;
	}
}
