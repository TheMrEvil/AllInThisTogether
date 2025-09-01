using System;
using EZCameraShake;
using UnityEngine;

// Token: 0x020002BF RID: 703
public class CameraEffectNode : EffectNode
{
	// Token: 0x17000190 RID: 400
	// (get) Token: 0x06001A0E RID: 6670 RVA: 0x000A268C File Offset: 0x000A088C
	public float Duration
	{
		get
		{
			float num = 0f;
			if (this.RoughnessCurve.keys.Length != 0)
			{
				Keyframe[] keys = this.RoughnessCurve.keys;
				num = keys[keys.Length - 1].time;
			}
			if (this.IntensityCurve.keys.Length != 0)
			{
				float a = num;
				Keyframe[] keys2 = this.IntensityCurve.keys;
				num = Mathf.Max(a, keys2[keys2.Length - 1].time);
			}
			return num;
		}
	}

	// Token: 0x06001A0F RID: 6671 RVA: 0x000A26F8 File Offset: 0x000A08F8
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Camera Shake"
		};
	}

	// Token: 0x06001A10 RID: 6672 RVA: 0x000A270A File Offset: 0x000A090A
	internal override void Apply(EffectProperties properties)
	{
		if (this.ShouldApply(properties, null))
		{
			CameraShaker.Instance.ShakeOnce(this);
		}
	}

	// Token: 0x06001A11 RID: 6673 RVA: 0x000A2724 File Offset: 0x000A0924
	internal override bool ShouldApply(EffectProperties props, EntityControl applyTo = null)
	{
		if (!this.ApplyGlobally)
		{
			return !(PlayerControl.myInstance == null) && (!(props.SourceControl != PlayerControl.myInstance) || !(props.AffectedControl != PlayerControl.myInstance));
		}
		if (this.Range == 0f)
		{
			return true;
		}
		Vector3 outputPoint = props.GetOutputPoint();
		Vector3 position = PlayerControl.myInstance.Movement.GetPosition();
		return Vector3.Distance(outputPoint, position) <= this.Range;
	}

	// Token: 0x06001A12 RID: 6674 RVA: 0x000A27A8 File Offset: 0x000A09A8
	public override void OnCloned()
	{
		AnimationCurve animationCurve = new AnimationCurve();
		animationCurve.LoadFromString(this.RoughnessCurve.GetString());
		this.RoughnessCurve = animationCurve;
		AnimationCurve animationCurve2 = new AnimationCurve();
		animationCurve2.LoadFromString(this.IntensityCurve.GetString());
		this.IntensityCurve = animationCurve2;
	}

	// Token: 0x06001A13 RID: 6675 RVA: 0x000A27F4 File Offset: 0x000A09F4
	public CameraEffectNode()
	{
	}

	// Token: 0x04001A8B RID: 6795
	public AnimationCurve RoughnessCurve = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(0f, 15f),
		new Keyframe(0.25f, 0f)
	});

	// Token: 0x04001A8C RID: 6796
	public AnimationCurve IntensityCurve = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(0f, 1.5f),
		new Keyframe(0.25f, 0f)
	});

	// Token: 0x04001A8D RID: 6797
	public bool ApplyGlobally;

	// Token: 0x04001A8E RID: 6798
	[Tooltip("If 0, applied globally regardless of distance.")]
	public float Range;
}
