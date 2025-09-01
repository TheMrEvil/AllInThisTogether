using System;
using UnityEngine;

// Token: 0x020002F5 RID: 757
public class ProjectileMoveProps_Random : ProjectileMoveModuleNode
{
	// Token: 0x06001AEC RID: 6892 RVA: 0x000A7448 File Offset: 0x000A5648
	public override void UpdateVelocity(ref Vector3 vel, Projectile p, EffectProperties props, Projectile.ProjectileMoveMods mod)
	{
		int projectileID = p.projectileID;
		Vector3 normalized = vel.normalized;
		float num = p.timeAlive / Mathf.Max(0.001f, this.NoiseScale * this.scaleCurve.Evaluate(p.timeAlive));
		float num2 = this.powerCurve.Evaluate(p.timeAlive);
		float num3 = this.NoiseStrength;
		if (this.Value != null)
		{
			NumberNode numberNode = this.Value as NumberNode;
			if (numberNode != null)
			{
				num3 = numberNode.Evaluate(props);
			}
		}
		float num4 = GameplayManager.deltaTime * 60f;
		normalized.x += (Mathf.PerlinNoise(num + (float)projectileID, 0f) - 0.5f) * num3 * num2 * num4;
		normalized.y += (Mathf.PerlinNoise(num, (float)(projectileID + 1)) - 0.5f) * num3 * num2 * num4;
		normalized.z += (Mathf.PerlinNoise((float)(projectileID + 2), num) - 0.5f) * num3 * num2 * num4;
		normalized.Normalize();
		vel = vel.magnitude * normalized;
	}

	// Token: 0x06001AED RID: 6893 RVA: 0x000A7564 File Offset: 0x000A5764
	public override void OnCloned()
	{
		AnimationCurve curve = new AnimationCurve();
		curve.LoadFromString(this.scaleCurve.GetString());
		this.scaleCurve = curve;
		AnimationCurve curve2 = new AnimationCurve();
		curve2.LoadFromString(this.powerCurve.GetString());
		this.powerCurve = curve2;
	}

	// Token: 0x06001AEE RID: 6894 RVA: 0x000A75AD File Offset: 0x000A57AD
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Random Module"
		};
	}

	// Token: 0x06001AEF RID: 6895 RVA: 0x000A75C0 File Offset: 0x000A57C0
	public ProjectileMoveProps_Random()
	{
	}

	// Token: 0x04001B8C RID: 7052
	public float NoiseScale = 1f;

	// Token: 0x04001B8D RID: 7053
	public AnimationCurve scaleCurve = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(0f, 1f),
		new Keyframe(1f, 1f)
	});

	// Token: 0x04001B8E RID: 7054
	public float NoiseStrength = 1f;

	// Token: 0x04001B8F RID: 7055
	public AnimationCurve powerCurve = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(0f, 1f),
		new Keyframe(1f, 1f)
	});

	// Token: 0x04001B90 RID: 7056
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(NumberNode), false, "Dynamic Strength", PortLocation.Default)]
	public Node Value;
}
