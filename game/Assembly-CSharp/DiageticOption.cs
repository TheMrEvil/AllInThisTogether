using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x020000AB RID: 171
public class DiageticOption : MonoBehaviour
{
	// Token: 0x170000B3 RID: 179
	// (get) Token: 0x060007BD RID: 1981 RVA: 0x00037AFB File Offset: 0x00035CFB
	// (set) Token: 0x060007BE RID: 1982 RVA: 0x00037B03 File Offset: 0x00035D03
	public bool IsHeld
	{
		[CompilerGenerated]
		get
		{
			return this.<IsHeld>k__BackingField;
		}
		[CompilerGenerated]
		set
		{
			this.<IsHeld>k__BackingField = value;
		}
	}

	// Token: 0x170000B4 RID: 180
	// (get) Token: 0x060007BF RID: 1983 RVA: 0x00037B0C File Offset: 0x00035D0C
	public bool IsHighlighted
	{
		get
		{
			return this.IsAvailable && DiageticSelector.CurrentSelected == this;
		}
	}

	// Token: 0x170000B5 RID: 181
	// (get) Token: 0x060007C0 RID: 1984 RVA: 0x00037B23 File Offset: 0x00035D23
	// (set) Token: 0x060007C1 RID: 1985 RVA: 0x00037B2B File Offset: 0x00035D2B
	public bool IsInRange
	{
		[CompilerGenerated]
		get
		{
			return this.<IsInRange>k__BackingField;
		}
		[CompilerGenerated]
		set
		{
			this.<IsInRange>k__BackingField = value;
		}
	}

	// Token: 0x060007C2 RID: 1986 RVA: 0x00037B34 File Offset: 0x00035D34
	protected virtual void Awake()
	{
		if (this.HoldLoop != null && this.NeedsHold)
		{
			this.holdSource = base.GetComponent<AudioSource>();
			if (this.holdSource == null)
			{
				this.holdSource = base.gameObject.AddComponent<AudioSource>();
				this.holdSource.outputAudioMixerGroup = AudioManager.instance.LoudSFXGroup;
			}
			this.holdSource.loop = true;
			this.holdSource.clip = this.HoldLoop;
			this.holdSource.volume = 0f;
		}
	}

	// Token: 0x060007C3 RID: 1987 RVA: 0x00037BC4 File Offset: 0x00035DC4
	internal virtual void OnEnable()
	{
		if (this.UIRoot == null)
		{
			this.UIRoot = base.transform;
		}
		this.IsAvailable = false;
		DiageticSelector.RegisterOption(this);
	}

	// Token: 0x060007C4 RID: 1988 RVA: 0x00037BF0 File Offset: 0x00035DF0
	public virtual void TickUpdate()
	{
		if (!this.IsHighlighted)
		{
			this.IsHeld = false;
		}
		if (this.holdSource != null)
		{
			float num = (float)(this.IsHeld ? 1 : 0);
			if (this.holdSource.volume != num)
			{
				this.holdSource.volume = Mathf.Lerp(this.holdSource.volume, num, Time.deltaTime * 4f);
			}
			if (!this.IsHeld && this.holdSource.volume <= 0f && this.holdSource.isPlaying)
			{
				this.holdSource.Stop();
				return;
			}
			if (this.IsHeld && !this.holdSource.isPlaying)
			{
				this.holdSource.Play();
			}
		}
	}

	// Token: 0x060007C5 RID: 1989 RVA: 0x00037CB3 File Offset: 0x00035EB3
	public virtual void Activate()
	{
		this.IsAvailable = true;
		if (this.col != null)
		{
			this.col.enabled = true;
		}
	}

	// Token: 0x060007C6 RID: 1990 RVA: 0x00037CD6 File Offset: 0x00035ED6
	public virtual void Deactivate()
	{
		this.IsAvailable = false;
		if (this.col != null)
		{
			this.col.enabled = false;
		}
	}

	// Token: 0x060007C7 RID: 1991 RVA: 0x00037CF9 File Offset: 0x00035EF9
	public virtual void Select()
	{
		Action onInteract = this.OnInteract;
		if (onInteract == null)
		{
			return;
		}
		onInteract();
	}

	// Token: 0x060007C8 RID: 1992 RVA: 0x00037D0B File Offset: 0x00035F0B
	public virtual List<int> GetVoteInfo()
	{
		return new List<int>();
	}

	// Token: 0x060007C9 RID: 1993 RVA: 0x00037D12 File Offset: 0x00035F12
	private void OnDisable()
	{
		DiageticSelector.UnregisterOption(this);
	}

	// Token: 0x060007CA RID: 1994 RVA: 0x00037D1A File Offset: 0x00035F1A
	public DiageticOption()
	{
	}

	// Token: 0x04000688 RID: 1672
	public bool IsAvailable;

	// Token: 0x04000689 RID: 1673
	public float InteractDistance = 5f;

	// Token: 0x0400068A RID: 1674
	public Transform UIRoot;

	// Token: 0x0400068B RID: 1675
	public Collider col;

	// Token: 0x0400068C RID: 1676
	public bool NeedsVote;

	// Token: 0x0400068D RID: 1677
	public bool NeedsHold;

	// Token: 0x0400068E RID: 1678
	public float HoldTime = 1.5f;

	// Token: 0x0400068F RID: 1679
	public AudioClip HoldLoop;

	// Token: 0x04000690 RID: 1680
	public string PlayEmote;

	// Token: 0x04000691 RID: 1681
	private AudioSource holdSource;

	// Token: 0x04000692 RID: 1682
	public Action OnInteract;

	// Token: 0x04000693 RID: 1683
	[CompilerGenerated]
	private bool <IsHeld>k__BackingField;

	// Token: 0x04000694 RID: 1684
	[CompilerGenerated]
	private bool <IsInRange>k__BackingField;
}
