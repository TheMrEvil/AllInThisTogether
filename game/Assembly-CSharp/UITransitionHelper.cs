using System;
using TMPro;
using UnityEngine;

// Token: 0x02000204 RID: 516
public class UITransitionHelper : MonoBehaviour
{
	// Token: 0x0600160B RID: 5643 RVA: 0x0008B884 File Offset: 0x00089A84
	private void Awake()
	{
		UITransitionHelper.instance = this;
		this.canvasGroup = base.GetComponent<CanvasGroup>();
		this.canvasGroup.alpha = 0f;
		GameplayManager.OnGameStateChanged = (Action<GameState, GameState>)Delegate.Combine(GameplayManager.OnGameStateChanged, new Action<GameState, GameState>(this.GameStateChanged));
	}

	// Token: 0x0600160C RID: 5644 RVA: 0x0008B8D4 File Offset: 0x00089AD4
	private void Update()
	{
		if (this.wantVisibleTime > 0f)
		{
			this.wantVisibleTime -= Time.deltaTime;
		}
		bool shouldShow = this.wantVisibleTime > 0f;
		if (!UITransitionHelper.InWavePrelim() && this.SelectionTimer.isPlaying)
		{
			this.SelectionTimer.volume -= Time.deltaTime * 2f;
			if (this.SelectionTimer.volume <= 0f)
			{
				this.SelectionTimer.Stop();
			}
		}
		this.canvasGroup.UpdateOpacity(shouldShow, 2f, true);
	}

	// Token: 0x0600160D RID: 5645 RVA: 0x0008B96E File Offset: 0x00089B6E
	public static void SetText(string text, float duration)
	{
		if (UITransitionHelper.instance == null)
		{
			return;
		}
		UITransitionHelper.instance.Label.text = text;
		UITransitionHelper.instance.wantVisibleTime = duration;
	}

	// Token: 0x0600160E RID: 5646 RVA: 0x0008B99C File Offset: 0x00089B9C
	private void GameStateChanged(GameState from, GameState to)
	{
		if (to == GameState.Reward_PreEnemy)
		{
			AugmentsPanel.TryClose();
			AudioManager.PlayInterfaceSFX(this.PreSeq, 1f, 0f);
			if (!Settings.GetBool(SystemSetting.Photosensitivity, false))
			{
				Fountain.instance.WaveTornado_Base.Play();
			}
			Fountain.instance.WaveTornadoSFX.time = 0f;
			Fountain.instance.WaveTornadoSFX.Play();
			return;
		}
		if (to == GameState.Reward_Enemy)
		{
			GenreRewardNode genreRewardNode = RewardManager.instance.RewardConfig();
			if (genreRewardNode != null && genreRewardNode.HasReward(GameState.Reward_Enemy) && PanelManager.CurPanel == PanelType.GameInvisible)
			{
				EnemySelectionPanel.instance.GoToUI();
			}
			if (!Settings.GetBool(SystemSetting.Photosensitivity, false))
			{
				Fountain.instance.WaveTornado_Extra.Play();
			}
			this.SelectionTimer.time = 0f;
			this.SelectionTimer.Play();
			this.SelectionTimer.volume = 1f;
			return;
		}
		if (to == GameState.PostRewards)
		{
			Fountain.instance.WavePulse.Play();
			Fountain.instance.WaveTornado_Base.Stop();
			Fountain.instance.WaveTornado_Extra.Stop();
		}
	}

	// Token: 0x0600160F RID: 5647 RVA: 0x0008BAB1 File Offset: 0x00089CB1
	public static bool InWavePrelim()
	{
		return GameplayManager.CurState == GameState.Reward_Enemy || GameplayManager.CurState == GameState.Reward_PreEnemy;
	}

	// Token: 0x06001610 RID: 5648 RVA: 0x0008BAC7 File Offset: 0x00089CC7
	public UITransitionHelper()
	{
	}

	// Token: 0x040015B8 RID: 5560
	public static UITransitionHelper instance;

	// Token: 0x040015B9 RID: 5561
	public TextMeshProUGUI Label;

	// Token: 0x040015BA RID: 5562
	private CanvasGroup canvasGroup;

	// Token: 0x040015BB RID: 5563
	private float wantVisibleTime;

	// Token: 0x040015BC RID: 5564
	public AudioClip PreSeq;

	// Token: 0x040015BD RID: 5565
	public AudioSource SelectionTimer;

	// Token: 0x040015BE RID: 5566
	public AudioClip SelectCompleted;
}
