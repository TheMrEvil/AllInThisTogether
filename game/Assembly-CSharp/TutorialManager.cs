using System;
using UnityEngine;

// Token: 0x020000DA RID: 218
public class TutorialManager : MonoBehaviour
{
	// Token: 0x060009DE RID: 2526 RVA: 0x00041443 File Offset: 0x0003F643
	private void Awake()
	{
		TutorialManager.instance = this;
		TutorialManager.ResetTutorial();
	}

	// Token: 0x060009DF RID: 2527 RVA: 0x00041450 File Offset: 0x0003F650
	public void StartTutorial()
	{
		if (LibraryManager.instance == null || PlayerControl.myInstance != null)
		{
			return;
		}
		Debug.Log("Starting Tutorial from Main");
		TutorialManager.InTutorial = true;
	}

	// Token: 0x060009E0 RID: 2528 RVA: 0x0004147D File Offset: 0x0003F67D
	public void LibraryStart()
	{
		Debug.Log("Transitioning to Tutorial Level");
		GameplayManager.instance.TrySetGenreMaster(this.TutorialGenre.Root.guid);
		MapManager.instance.ChangeLevelSequence("Tutorial");
	}

	// Token: 0x060009E1 RID: 2529 RVA: 0x000414B2 File Offset: 0x0003F6B2
	public static void ResetTutorial()
	{
		TutorialManager.InTutorial = false;
		TutorialManager.CurrentStep = TutorialStep.PreLibrary;
	}

	// Token: 0x060009E2 RID: 2530 RVA: 0x000414C0 File Offset: 0x0003F6C0
	public void ChangeStep(TutorialStep step)
	{
		if (step <= TutorialManager.CurrentStep)
		{
			return;
		}
		TutorialManager.CurrentStep = step;
		Action<TutorialStep> onStepChanged = TutorialManager.OnStepChanged;
		if (onStepChanged != null)
		{
			onStepChanged(step);
		}
		Analytics.TutorialStep((int)step, GameplayManager.instance.GameTime);
	}

	// Token: 0x060009E3 RID: 2531 RVA: 0x000414F4 File Offset: 0x0003F6F4
	public static bool ShouldShowAbilityUI(PlayerAbilityType ability)
	{
		bool result;
		switch (ability)
		{
		case PlayerAbilityType.Primary:
			result = (TutorialManager.CurrentStep >= TutorialStep.Generator);
			break;
		case PlayerAbilityType.Secondary:
			result = (TutorialManager.CurrentStep >= TutorialStep.Spender);
			break;
		case PlayerAbilityType.Utility:
			result = (TutorialManager.CurrentStep >= TutorialStep.CoreObjective);
			break;
		case PlayerAbilityType.Movement:
			result = (TutorialManager.CurrentStep >= TutorialStep.MoveAbility);
			break;
		default:
			result = true;
			break;
		}
		return result;
	}

	// Token: 0x060009E4 RID: 2532 RVA: 0x00041558 File Offset: 0x0003F758
	public TutorialManager()
	{
	}

	// Token: 0x0400083D RID: 2109
	public static TutorialManager instance;

	// Token: 0x0400083E RID: 2110
	public GenreTree TutorialGenre;

	// Token: 0x0400083F RID: 2111
	public static bool InTutorial;

	// Token: 0x04000840 RID: 2112
	public static TutorialStep CurrentStep;

	// Token: 0x04000841 RID: 2113
	public static bool IsReturning;

	// Token: 0x04000842 RID: 2114
	public static Action<TutorialStep> OnStepChanged;
}
