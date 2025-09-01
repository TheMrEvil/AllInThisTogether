using System;
using UnityEngine;

// Token: 0x020001C3 RID: 451
public class ProgressUI : MonoBehaviour
{
	// Token: 0x06001288 RID: 4744 RVA: 0x000726D4 File Offset: 0x000708D4
	private void Awake()
	{
		ProgressUI.instance = this;
		GameplayManager.OnGenereChanged = (Action<GenreTree>)Delegate.Combine(GameplayManager.OnGenereChanged, new Action<GenreTree>(this.OnGenreChanged));
		GameplayManager.OnGameStateChanged = (Action<GameState, GameState>)Delegate.Combine(GameplayManager.OnGameStateChanged, new Action<GameState, GameState>(this.OnGameStateChanged));
	}

	// Token: 0x06001289 RID: 4745 RVA: 0x00072727 File Offset: 0x00070927
	private void OnGameStateChanged(GameState from, GameState to)
	{
		WaveProgressBar.instance.OnGameStateChanged(from, to);
	}

	// Token: 0x0600128A RID: 4746 RVA: 0x00072735 File Offset: 0x00070935
	private void OnGenreChanged(GenreTree tree)
	{
		tree == null;
	}

	// Token: 0x0600128B RID: 4747 RVA: 0x0007273F File Offset: 0x0007093F
	public ProgressUI()
	{
	}

	// Token: 0x0400118B RID: 4491
	public static ProgressUI instance;
}
