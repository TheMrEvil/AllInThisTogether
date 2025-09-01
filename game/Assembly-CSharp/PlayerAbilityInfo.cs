using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002A0 RID: 672
[Serializable]
public struct PlayerAbilityInfo
{
	// Token: 0x040019A8 RID: 6568
	public Sprite Icon;

	// Token: 0x040019A9 RID: 6569
	public string Name;

	// Token: 0x040019AA RID: 6570
	[TextArea(3, 4)]
	public string Detail;

	// Token: 0x040019AB RID: 6571
	public string DamageText;

	// Token: 0x040019AC RID: 6572
	[Tooltip("Mana Cost to use ability, 0 = No Cost")]
	public float ManaCost;

	// Token: 0x040019AD RID: 6573
	public bool Locked;

	// Token: 0x040019AE RID: 6574
	public List<ActionTree> AbilityActions;

	// Token: 0x040019AF RID: 6575
	public bool Ephemeral;

	// Token: 0x040019B0 RID: 6576
	public PlayerAbilityInfo.ResetType ResetOn;

	// Token: 0x040019B1 RID: 6577
	public AbilityTree OriginalAbility;

	// Token: 0x0200063F RID: 1599
	public enum ResetType
	{
		// Token: 0x04002AAF RID: 10927
		Explicit,
		// Token: 0x04002AB0 RID: 10928
		ReturnToLobby,
		// Token: 0x04002AB1 RID: 10929
		MapChange,
		// Token: 0x04002AB2 RID: 10930
		WaveComplete
	}
}
