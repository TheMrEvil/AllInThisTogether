using System;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x02000289 RID: 649
public class Logic_Unlocked : LogicNode
{
	// Token: 0x0600191B RID: 6427 RVA: 0x0009C890 File Offset: 0x0009AA90
	public override bool Evaluate(EffectProperties props)
	{
		Logic_Unlocked.UnlockCheck checkType = this.CheckType;
		bool flag;
		switch (checkType)
		{
		case Logic_Unlocked.UnlockCheck.Ability:
			flag = (this.Ability != null && UnlockManager.IsAbilityUnlocked(this.Ability));
			break;
		case Logic_Unlocked.UnlockCheck.Augment:
			flag = (this.Augment != null && UnlockManager.IsAugmentUnlocked(this.Augment));
			break;
		case Logic_Unlocked.UnlockCheck.Core:
			flag = (this.Core != null && UnlockManager.IsCoreUnlocked(this.Core));
			break;
		case Logic_Unlocked.UnlockCheck.Genre:
			flag = (this.Genre != null && UnlockManager.IsGenreUnlocked(this.Genre));
			break;
		case Logic_Unlocked.UnlockCheck.Binding:
			flag = (this.Binding != null && UnlockManager.IsBindingUnlocked(this.Binding));
			break;
		default:
			throw new SwitchExpressionException(checkType);
		}
		bool flag2 = flag;
		this.EditorStateDisplay = (flag2 ? NodeState.Success : NodeState.Fail);
		return flag2;
	}

	// Token: 0x0600191C RID: 6428 RVA: 0x0009C97D File Offset: 0x0009AB7D
	public override Node.InspectorProps GetInspectorProps()
	{
		Node.InspectorProps inspectorProps = base.GetInspectorProps();
		inspectorProps.Title = "Is Unlocked";
		inspectorProps.MinInspectorSize = new Vector2(150f, 0f);
		inspectorProps.MaxInspectorSize = new Vector2(150f, 0f);
		return inspectorProps;
	}

	// Token: 0x0600191D RID: 6429 RVA: 0x0009C9BA File Offset: 0x0009ABBA
	public Logic_Unlocked()
	{
	}

	// Token: 0x04001932 RID: 6450
	public Logic_Unlocked.UnlockCheck CheckType;

	// Token: 0x04001933 RID: 6451
	public AbilityTree Ability;

	// Token: 0x04001934 RID: 6452
	public AugmentTree Augment;

	// Token: 0x04001935 RID: 6453
	public AugmentTree Core;

	// Token: 0x04001936 RID: 6454
	public GenreTree Genre;

	// Token: 0x04001937 RID: 6455
	public AugmentTree Binding;

	// Token: 0x02000639 RID: 1593
	public enum UnlockCheck
	{
		// Token: 0x04002A79 RID: 10873
		Ability,
		// Token: 0x04002A7A RID: 10874
		Augment,
		// Token: 0x04002A7B RID: 10875
		Core,
		// Token: 0x04002A7C RID: 10876
		Genre,
		// Token: 0x04002A7D RID: 10877
		Binding
	}
}
