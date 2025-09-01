using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

// Token: 0x020002BD RID: 701
public class AudioEffectNode : EffectNode
{
	// Token: 0x06001A04 RID: 6660 RVA: 0x000A21E0 File Offset: 0x000A03E0
	internal override void Apply(EffectProperties properties)
	{
		if (this.Options.Count == 0 || this.Volume == 0f)
		{
			return;
		}
		if (!AudioManager.CanPlay(this.guid) && this.Cooldown > 0f)
		{
			return;
		}
		AudioEffectNode.PlayForMode playFor = this.PlayFor;
		if (playFor != AudioEffectNode.PlayForMode.LocalOnly)
		{
			if (playFor == AudioEffectNode.PlayForMode.OthersOnly)
			{
				if (properties.SourceControl == PlayerControl.myInstance || properties.AffectedControl == PlayerControl.myInstance)
				{
					return;
				}
			}
		}
		else
		{
			if (PlayerControl.myInstance == null)
			{
				return;
			}
			if (properties.SourceControl != PlayerControl.myInstance && properties.AffectedControl != PlayerControl.myInstance)
			{
				return;
			}
		}
		Vector3 vector = Vector3.one.INVALID();
		if (properties.SourceControl != null)
		{
			vector = properties.SourceControl.display.CenterOfMass.position;
		}
		if (this.Loc != null)
		{
			vector = (this.Loc as LocationNode).GetLocation(properties).GetPosition(properties);
		}
		else if (properties.OutLoc != null)
		{
			vector = properties.GetOutputPoint();
		}
		else
		{
			vector = properties.GetOrigin();
		}
		AudioClip randomClip = this.Options.GetRandomClip(-1);
		float volume = this.Volume;
		if (this.ThreeD < 0.5f && this.UseDistanceFade && PlayerControl.MyCamera != null && this.DistanceRange.y > this.DistanceRange.x)
		{
			float num = Vector3.Distance(vector, PlayerControl.MyCamera.transform.position);
			if (num > this.DistanceRange.y)
			{
				volume = 0f;
			}
			else if (num > this.DistanceRange.x)
			{
				float num2 = this.DistanceRange.y - this.DistanceRange.x;
				volume = this.Volume / Mathf.Pow((num - this.DistanceRange.x) / num2, 2f);
			}
		}
		AudioManager.AudioNodeActivated(this.guid, this.Cooldown);
		AudioSource audioSource = AudioManager.PlayClipAtPoint(randomClip, vector, volume, UnityEngine.Random.Range(this.PitchRange.x, this.PitchRange.y), this.ThreeD, this.DistanceRange.x, this.DistanceRange.y);
		if (audioSource == null)
		{
			return;
		}
		if (this.CanCancel)
		{
			AudioManager.ConnectAudioNode(this, audioSource);
		}
		if (this.Follow)
		{
			this.FollowSrc(audioSource, properties);
		}
		if (this.OverrideGroup != null)
		{
			audioSource.outputAudioMixerGroup = this.OverrideGroup;
			return;
		}
		if (properties.SourceControl != null && properties.SourceControl is PlayerControl && properties.SourceControl != PlayerControl.myInstance)
		{
			audioSource.outputAudioMixerGroup = AudioManager.instance.OtherPlayerSFXGroup;
			return;
		}
		audioSource.outputAudioMixerGroup = (this.Loud ? AudioManager.instance.LoudSFXGroup : AudioManager.instance.SFXGroup);
	}

	// Token: 0x06001A05 RID: 6661 RVA: 0x000A24C0 File Offset: 0x000A06C0
	private void FollowSrc(AudioSource src, EffectProperties props)
	{
		if (src == null || src.clip == null)
		{
			return;
		}
		EffectProperties effectProperties = props.Copy(false);
		effectProperties.StartLoc = props.OutLoc.Copy();
		if (this.Loc != null)
		{
			effectProperties.StartLoc = (props.OutLoc = new global::Pose((this.Loc as LocationNode).GetLocation(props), Location.WorldUp()));
		}
		LockFollow.Follow(src.gameObject, this, effectProperties, this.Loc).Release(src.clip.length);
	}

	// Token: 0x06001A06 RID: 6662 RVA: 0x000A2559 File Offset: 0x000A0759
	public override void TryCancel(EffectProperties props)
	{
		this.OnCancel(props);
	}

	// Token: 0x06001A07 RID: 6663 RVA: 0x000A2562 File Offset: 0x000A0762
	internal override void OnCancel(EffectProperties props)
	{
		if (!this.CanCancel)
		{
			return;
		}
		AudioManager.CancelAudioNode(this, this.Follow);
	}

	// Token: 0x06001A08 RID: 6664 RVA: 0x000A2579 File Offset: 0x000A0779
	private bool IsThreeD()
	{
		return this.ThreeD > 0f || this.UseDistanceFade;
	}

	// Token: 0x06001A09 RID: 6665 RVA: 0x000A2590 File Offset: 0x000A0790
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Play Sound",
			MinInspectorSize = new Vector2(200f, 0f)
		};
	}

	// Token: 0x06001A0A RID: 6666 RVA: 0x000A25B8 File Offset: 0x000A07B8
	public AudioEffectNode()
	{
	}

	// Token: 0x04001A7C RID: 6780
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(LocationNode), false, "Location Override", PortLocation.Default)]
	public Node Loc;

	// Token: 0x04001A7D RID: 6781
	public List<AudioClip> Options;

	// Token: 0x04001A7E RID: 6782
	[Range(0f, 1f)]
	public float Volume = 1f;

	// Token: 0x04001A7F RID: 6783
	public Vector2 PitchRange = new Vector2(0.95f, 1.05f);

	// Token: 0x04001A80 RID: 6784
	[Range(0f, 1f)]
	public float ThreeD = 1f;

	// Token: 0x04001A81 RID: 6785
	public bool UseDistanceFade;

	// Token: 0x04001A82 RID: 6786
	public Vector2 DistanceRange = new Vector2(15f, 250f);

	// Token: 0x04001A83 RID: 6787
	public AudioEffectNode.PlayForMode PlayFor;

	// Token: 0x04001A84 RID: 6788
	public bool Follow;

	// Token: 0x04001A85 RID: 6789
	public bool CanCancel;

	// Token: 0x04001A86 RID: 6790
	public bool Loud;

	// Token: 0x04001A87 RID: 6791
	public float Cooldown;

	// Token: 0x04001A88 RID: 6792
	public AudioMixerGroup OverrideGroup;

	// Token: 0x02000647 RID: 1607
	public enum PlayForMode
	{
		// Token: 0x04002ACA RID: 10954
		All,
		// Token: 0x04002ACB RID: 10955
		LocalOnly,
		// Token: 0x04002ACC RID: 10956
		OthersOnly
	}
}
