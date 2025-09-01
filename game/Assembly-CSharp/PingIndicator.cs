using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001BE RID: 446
public class PingIndicator : MonoBehaviour
{
	// Token: 0x17000151 RID: 337
	// (get) Token: 0x0600125D RID: 4701 RVA: 0x000717C3 File Offset: 0x0006F9C3
	public PlayerControl refPlayer
	{
		get
		{
			return this.target.control;
		}
	}

	// Token: 0x0600125E RID: 4702 RVA: 0x000717D0 File Offset: 0x0006F9D0
	private void Awake()
	{
		this.rect = base.GetComponent<RectTransform>();
	}

	// Token: 0x0600125F RID: 4703 RVA: 0x000717DE File Offset: 0x0006F9DE
	public void Setup(PlayerPing target)
	{
		this.target = target;
		target.OnPinged = (Action<PlayerDB.PingType>)Delegate.Combine(target.OnPinged, new Action<PlayerDB.PingType>(this.OnPinged));
		this.SetupDisplay();
	}

	// Token: 0x06001260 RID: 4704 RVA: 0x0007180F File Offset: 0x0006FA0F
	private void OnPinged(PlayerDB.PingType type)
	{
		this.CoreGroup.alpha = 1f;
		this.ArrowGroup.alpha = 0f;
		this.SetupDisplay(type);
		this.SpawnPulse.Play();
	}

	// Token: 0x06001261 RID: 4705 RVA: 0x00071844 File Offset: 0x0006FA44
	public void UpdatePosition(int index)
	{
		if (this.target == null)
		{
			return;
		}
		float num = this.rect.FollowWorldObject(this.target.transform, this.canvas, index, 0.1f);
		Vector3 a = -this.rect.localPosition.normalized;
		this.rect.up = Vector3.Lerp(a, Vector3.up, num);
		this.AtEdge = (num < 0.2f);
	}

	// Token: 0x06001262 RID: 4706 RVA: 0x000718C4 File Offset: 0x0006FAC4
	public void UpdateOpacity()
	{
		bool flag = true;
		if (MapManager.InLobbyScene && PlayerCamera.myInstance != null && this.target != null)
		{
			flag = (Vector3.Distance(PlayerCamera.myInstance.transform.position, this.target.transform.position) < 512f);
		}
		this.CoreGroup.UpdateOpacity(this.target != null && this.target.IsVisible && flag, 4f, false);
		this.ArrowGroup.UpdateOpacity(this.AtEdge && flag, 3f, false);
	}

	// Token: 0x06001263 RID: 4707 RVA: 0x00071969 File Offset: 0x0006FB69
	private void SetupDisplay()
	{
		if (this.target.FollowEntity != null)
		{
			this.SetupDisplay(PlayerDB.PingType.Attack);
			return;
		}
		this.SetupDisplay(PlayerDB.PingType.Generic);
	}

	// Token: 0x06001264 RID: 4708 RVA: 0x00071990 File Offset: 0x0006FB90
	private void SetupDisplay(PlayerDB.PingType type)
	{
		PlayerDB.PingDisplay ping = PlayerDB.GetPing(type);
		if (ping == null)
		{
			return;
		}
		this.CenterIcon.sprite = ping.Icon;
		this.ArrowIcon.sprite = ping.Arrow;
	}

	// Token: 0x06001265 RID: 4709 RVA: 0x000719CA File Offset: 0x0006FBCA
	public PingIndicator()
	{
	}

	// Token: 0x0400114A RID: 4426
	[Header("Indicator Elements")]
	private RectTransform rect;

	// Token: 0x0400114B RID: 4427
	public RectTransform canvas;

	// Token: 0x0400114C RID: 4428
	[Header("Ping Elements")]
	public CanvasGroup ArrowGroup;

	// Token: 0x0400114D RID: 4429
	public CanvasGroup CoreGroup;

	// Token: 0x0400114E RID: 4430
	public ParticleSystem SpawnPulse;

	// Token: 0x0400114F RID: 4431
	[Header("Ping Context")]
	public Image CenterIcon;

	// Token: 0x04001150 RID: 4432
	public Image ArrowIcon;

	// Token: 0x04001151 RID: 4433
	public PlayerPing target;

	// Token: 0x04001152 RID: 4434
	private bool AtEdge;
}
