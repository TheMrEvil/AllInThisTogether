using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000B3 RID: 179
public class LibraryMPStartZone : Indicatable
{
	// Token: 0x170000BC RID: 188
	// (get) Token: 0x06000810 RID: 2064 RVA: 0x00038B22 File Offset: 0x00036D22
	public override Transform Root
	{
		get
		{
			return this.IndicatorLoc;
		}
	}

	// Token: 0x06000811 RID: 2065 RVA: 0x00038B2A File Offset: 0x00036D2A
	private void Awake()
	{
		this.Activate();
	}

	// Token: 0x06000812 RID: 2066 RVA: 0x00038B34 File Offset: 0x00036D34
	private void Update()
	{
		this.IsPlayerInside = false;
		this.NearestPlayer = null;
		float num = this.InteractRange;
		foreach (PlayerControl playerControl in PlayerControl.AllPlayers)
		{
			if (!this.IsActive)
			{
				break;
			}
			float num2 = Vector3.Distance(base.transform.position, playerControl.Movement.GetPosition());
			if (num2 < num)
			{
				this.NearestPlayer = playerControl;
				this.IsPlayerInside = true;
				num = num2;
			}
		}
		if (this.IsPlayerInside && !this.InsideSystem.isPlaying)
		{
			this.InsideSystem.Play();
		}
		else if (!this.IsPlayerInside && this.InsideSystem.isPlaying)
		{
			this.InsideSystem.Stop();
		}
		float b = this.IsPlayerInside ? 1f : 0.4f;
		if (!this.IsActive)
		{
			b = 0f;
		}
		this.AuraDisplay.Opacity = Mathf.Lerp(this.AuraDisplay.Opacity, b, Time.deltaTime * 6f);
	}

	// Token: 0x06000813 RID: 2067 RVA: 0x00038C60 File Offset: 0x00036E60
	public void Activate()
	{
		if (this.IsActive)
		{
			return;
		}
		this.IsActive = true;
		this.AuraDisplay.Opacity = 0f;
		this.BaseSystem.Play();
		WorldIndicators.Indicate(this);
	}

	// Token: 0x06000814 RID: 2068 RVA: 0x00038C93 File Offset: 0x00036E93
	public void Deactivate()
	{
		if (!this.IsActive)
		{
			return;
		}
		this.IsActive = false;
		this.BaseSystem.Stop();
		UnityEngine.Object.Destroy(base.gameObject, 3f);
		WorldIndicators.ReleaseIndicator(this);
	}

	// Token: 0x06000815 RID: 2069 RVA: 0x00038CC8 File Offset: 0x00036EC8
	public override bool ShouldIndicate()
	{
		if (!base.ShouldIndicate())
		{
			return false;
		}
		if (this.DisplayMaxDistance > 0f)
		{
			Vector3 position = PlayerControl.myInstance.Movement.GetPosition();
			if (Vector3.Distance(base.transform.position, position) > this.DisplayMaxDistance)
			{
				return false;
			}
		}
		using (List<LibraryMPStartZone>.Enumerator enumerator = LibraryMPStarter.CurZones.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.NearestPlayer == PlayerControl.myInstance)
				{
					return false;
				}
			}
		}
		return this.IsActive && !this.IsPlayerInside;
	}

	// Token: 0x06000816 RID: 2070 RVA: 0x00038D80 File Offset: 0x00036F80
	public LibraryMPStartZone()
	{
	}

	// Token: 0x040006C7 RID: 1735
	public ParticleSystem BaseSystem;

	// Token: 0x040006C8 RID: 1736
	public ParticleSystem InsideSystem;

	// Token: 0x040006C9 RID: 1737
	public AuraController AuraDisplay;

	// Token: 0x040006CA RID: 1738
	public Transform IndicatorLoc;

	// Token: 0x040006CB RID: 1739
	public float InteractRange = 2f;

	// Token: 0x040006CC RID: 1740
	public float DisplayMaxDistance;

	// Token: 0x040006CD RID: 1741
	public bool IsActive;

	// Token: 0x040006CE RID: 1742
	public bool IsPlayerInside;

	// Token: 0x040006CF RID: 1743
	public PlayerControl NearestPlayer;
}
