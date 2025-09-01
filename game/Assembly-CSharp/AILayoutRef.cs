using System;
using UnityEngine;

// Token: 0x02000042 RID: 66
[CreateAssetMenu(menuName = "Tome Config/AI Spawn Preset", order = -4)]
public class AILayoutRef : ScriptableObject
{
	// Token: 0x0600020D RID: 525 RVA: 0x00012540 File Offset: 0x00010740
	public override string ToString()
	{
		return "LayoutName_" + base.name;
	}

	// Token: 0x0600020E RID: 526 RVA: 0x00012552 File Offset: 0x00010752
	public static implicit operator AILayout(AILayoutRef a)
	{
		return a.Layout;
	}

	// Token: 0x0600020F RID: 527 RVA: 0x0001255A File Offset: 0x0001075A
	public AILayoutRef()
	{
	}

	// Token: 0x04000204 RID: 516
	public const string NAME_KEY = "LayoutName_";

	// Token: 0x04000205 RID: 517
	[Range(0f, 100f)]
	public float Abundance = 100f;

	// Token: 0x04000206 RID: 518
	public int MinBindingLevel;

	// Token: 0x04000207 RID: 519
	public AILayout Layout;
}
