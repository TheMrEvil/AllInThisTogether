using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002F1 RID: 753
public class ProjectileMoveProps : Node
{
	// Token: 0x06001AD8 RID: 6872 RVA: 0x000A6BD4 File Offset: 0x000A4DD4
	public void Move(Projectile p, EffectProperties props, List<Projectile.ProjectileMoveMods> mods)
	{
		Vector3 velocity = p.velocity;
		float num = p.GetSpeed();
		velocity = velocity.normalized * num;
		foreach (Projectile.ProjectileMoveMods projectileMoveMods in mods)
		{
			projectileMoveMods.node.UpdateVelocity(ref velocity, p, props, projectileMoveMods);
		}
		p.velocity = velocity;
		if (velocity.magnitude == 0f)
		{
			return;
		}
		float num2 = Mathf.Max(4f, num * 0.2f);
		p.transform.forward = Vector3.Lerp(p.transform.forward, velocity.normalized, Time.deltaTime * num2);
	}

	// Token: 0x06001AD9 RID: 6873 RVA: 0x000A6C9C File Offset: 0x000A4E9C
	public override void OnCloned()
	{
		AnimationCurve curve = new AnimationCurve();
		curve.LoadFromString(this.speedCurve.GetString());
		this.speedCurve = curve;
	}

	// Token: 0x06001ADA RID: 6874 RVA: 0x000A6CC7 File Offset: 0x000A4EC7
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Projectile Movement"
		};
	}

	// Token: 0x06001ADB RID: 6875 RVA: 0x000A6CDC File Offset: 0x000A4EDC
	public ProjectileMoveProps()
	{
	}

	// Token: 0x04001B75 RID: 7029
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(NumberNode), true, "Dynamic Speed", PortLocation.Default)]
	public Node Value;

	// Token: 0x04001B76 RID: 7030
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(ProjectileMoveModuleNode), true, "Modules", PortLocation.Default)]
	public List<Node> Modules = new List<Node>();

	// Token: 0x04001B77 RID: 7031
	public float speed = 25f;

	// Token: 0x04001B78 RID: 7032
	public AnimationCurve speedCurve = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(0f, 1f),
		new Keyframe(1f, 1f)
	});

	// Token: 0x04001B79 RID: 7033
	public float MaxDist = 1500f;
}
