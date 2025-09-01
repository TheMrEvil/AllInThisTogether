using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x0200001E RID: 30
public class Beam : ActionEffect
{
	// Token: 0x17000004 RID: 4
	// (get) Token: 0x060000BE RID: 190 RVA: 0x0000934E File Offset: 0x0000754E
	public int TotaleEntered
	{
		get
		{
			if (this.Properties == null)
			{
				return 0;
			}
			return this.everInside.Count;
		}
	}

	// Token: 0x17000005 RID: 5
	// (get) Token: 0x060000BF RID: 191 RVA: 0x0000936C File Offset: 0x0000756C
	public int CurrentInside
	{
		get
		{
			int num = 0;
			foreach (KeyValuePair<EntityControl, float> keyValuePair in this.inside)
			{
				EntityControl entityControl;
				float num2;
				keyValuePair.Deconstruct(out entityControl, out num2);
				EntityControl entityControl2 = entityControl;
				if (!(entityControl2 == null) && !entityControl2.IsDead)
				{
					num++;
				}
			}
			return num;
		}
	}

	// Token: 0x060000C0 RID: 192 RVA: 0x000093E0 File Offset: 0x000075E0
	public override void Awake()
	{
		base.Awake();
		this.tubes = base.GetComponentsInChildren<TubeMesh>();
	}

	// Token: 0x060000C1 RID: 193 RVA: 0x000093F4 File Offset: 0x000075F4
	public void Activate(BeamNode props)
	{
		Beam.AllBeams.Add(this);
		this.actionProps.EffectSource = EffectSource.Beam;
		this.Properties = (props.Clone(null, false) as BeamNode);
		base.SetupSFX();
		this.tubes = base.GetComponentsInChildren<TubeMesh>();
		this.TickUpdate();
		Debug.DrawLine(base.transform.position, base.transform.position + base.transform.forward, Color.green, 2f);
	}

	// Token: 0x060000C2 RID: 194 RVA: 0x00009478 File Offset: 0x00007678
	public override void TickUpdate()
	{
		if ((this.Properties.Duration > 0f && this.timeAlive >= this.GetDuration()) | (this.Properties.EndWithCaster && (this.actionProps.SourceControl == null || this.actionProps.SourceControl.IsDead)))
		{
			this.Expire();
			return;
		}
		if (this.isFinished)
		{
			return;
		}
		Vector3 position = base.transform.position;
		Vector3 forward = base.transform.forward;
		bool stopsOnCollision = this.Properties.StopsOnCollision;
		Ray ray = new Ray(position, forward);
		float num = this.GetMaxLength();
		List<EntityControl> list = new List<EntityControl>();
		LayerMask mask = GameplayManager.instance.EntityMask;
		if (this.TouchesEnvironment)
		{
			mask += GameplayManager.instance.EnvironmentMask;
		}
		this.AllHits.Clear();
		this.hitColliders.Clear();
		int num2 = Physics.SphereCastNonAlloc(ray, this.Radius, this.physicsHitBuffer, num - this.Radius, mask);
		for (int i = 0; i < num2; i++)
		{
			RaycastHit item = this.physicsHitBuffer[i];
			if (!(item.collider == null) && !(item.point == Vector3.zero) && !this.hitColliders.Contains(item.collider))
			{
				this.AllHits.Add(item);
				this.hitColliders.Add(item.collider);
			}
		}
		if (this.AllHits.Count > 0)
		{
			this.AllHits.Sort((RaycastHit x, RaycastHit y) => x.distance.CompareTo(y.distance));
		}
		List<ValueTuple<Vector3, Vector3>> list2 = new List<ValueTuple<Vector3, Vector3>>();
		foreach (RaycastHit raycastHit in this.AllHits)
		{
			EntityControl entityControl = raycastHit.collider.GetComponent<EntityControl>();
			if (entityControl == null)
			{
				entityControl = raycastHit.collider.GetComponentInParent<EntityControl>();
			}
			if (!list.Contains(entityControl) && (!this.Properties.IgnoreCaster || !(entityControl != null) || !(entityControl == this.actionProps.SourceControl)))
			{
				bool flag = entityControl != null;
				if (entityControl != null && (!this.actionProps.CanInteractWith(entityControl, this.Properties.InteractsWith) || !entityControl.CanBeInteractedBy(this.actionProps.SourceControl)))
				{
					entityControl = null;
				}
				if (!flag || !(entityControl == null))
				{
					if (entityControl != null)
					{
						list.Add(entityControl);
					}
					list2.Add(new ValueTuple<Vector3, Vector3>(raycastHit.point, raycastHit.normal));
					if (this.Properties.StopsOnCollision)
					{
						num = raycastHit.distance;
						break;
					}
				}
			}
		}
		this.UpdateImpacts(list2);
		num = Mathf.Max(0.05f, num);
		float radiusMult = this.GetRadiusMult();
		this.UpdateHit(list);
		TubeMesh[] array = this.tubes;
		for (int j = 0; j < array.Length; j++)
		{
			array[j].UpdateSize(num + radiusMult, radiusMult, this.FastUpdate);
		}
		this.actionProps.Lifetime = this.timeAlive;
		this.actionProps.SetExtra(EProp.AoE_EnteredCount, (float)this.TotaleEntered);
		this.actionProps.SetExtra(EProp.AoE_CurrentInside, (float)this.CurrentInside);
		if (this.BeamSoundPoint != null && PlayerCamera.myInstance != null)
		{
			Vector3 end = position + forward * num;
			Vector3 position2 = PlayerCamera.myInstance.transform.position;
			Vector3 position3 = ExtensionMethods.ClosestPointOnLine(position, end, position2);
			this.BeamSoundPoint.position = position3;
		}
		base.TickUpdate();
	}

	// Token: 0x060000C3 RID: 195 RVA: 0x00009878 File Offset: 0x00007A78
	public void Expire()
	{
		for (int i = 0; i < this.ImpactEffects.Count; i++)
		{
			if (this.ImpactEffects[i].isPlaying)
			{
				this.ImpactEffects[i].Stop();
			}
		}
		foreach (EntityControl entity in this.inside.Keys)
		{
			base.ApplyEffects(this.Properties.OnExit, entity, false);
		}
		this.Finish();
	}

	// Token: 0x060000C4 RID: 196 RVA: 0x0000991C File Offset: 0x00007B1C
	private float GetMaxLength()
	{
		if (this.cacheLengthT > 0f)
		{
			this.cacheLengthT -= GameplayManager.deltaTime;
		}
		else
		{
			if (this.actionProps != null && this.actionProps.SourceControl != null)
			{
				this.cacheLengthMult = this.actionProps.ModifyAbilityPassives(Passive.AbilityValue.Beam_Range, 1f);
			}
			this.cacheLengthT = 0.24331f;
		}
		return this.Properties.GetLength(this.actionProps) * this.cacheLengthMult * this.Properties.LengthOverTime.Evaluate(this.timeAlive);
	}

	// Token: 0x060000C5 RID: 197 RVA: 0x000099BA File Offset: 0x00007BBA
	private float GetRadiusMult()
	{
		return 1f * this.Properties.WidthOverTime.Evaluate(this.timeAlive);
	}

	// Token: 0x060000C6 RID: 198 RVA: 0x000099D8 File Offset: 0x00007BD8
	private float GetDuration()
	{
		return this.Properties.Duration;
	}

	// Token: 0x060000C7 RID: 199 RVA: 0x000099E8 File Offset: 0x00007BE8
	private void UpdateHit(List<EntityControl> entities)
	{
		if (this.Properties == null)
		{
			return;
		}
		List<EntityControl> list = new List<EntityControl>();
		foreach (KeyValuePair<EntityControl, float> keyValuePair in this.inside)
		{
			if (!entities.Contains(keyValuePair.Key))
			{
				list.Add(keyValuePair.Key);
			}
		}
		foreach (EntityControl entityControl in list)
		{
			base.ApplyEffects(this.Properties.OnExit, entityControl, false);
			this.inside.Remove(entityControl);
		}
		bool flag = false;
		foreach (EntityControl entityControl2 in entities)
		{
			if (this.inside.ContainsKey(entityControl2))
			{
				Dictionary<EntityControl, float> dictionary = this.inside;
				EntityControl key = entityControl2;
				dictionary[key] += Time.deltaTime;
				if (this.inside[entityControl2] >= this.Properties.TickRate)
				{
					base.ApplyEffects(this.Properties.OnTick, entityControl2, true);
					this.inside[entityControl2] = 0f;
				}
			}
			else
			{
				this.inside.Add(entityControl2, 0f);
				this.everInside.Add(entityControl2);
				base.ApplyEffects(this.Properties.OnEnter, entityControl2, true);
				if (!flag && !this.actionProps.HasExtra(EProp.Snip_DidHit))
				{
					flag = true;
					EffectProperties effectProperties = this.actionProps.Copy(false);
					this.actionProps.SetExtra(EProp.Snip_DidHit, 1f);
					effectProperties.SourceControl.TriggerSnippets(EventTrigger.AbilityHit, effectProperties.Copy(false), 1f);
					effectProperties.TryAbilityFirstHit();
				}
			}
		}
	}

	// Token: 0x060000C8 RID: 200 RVA: 0x00009C30 File Offset: 0x00007E30
	private void UpdateImpacts([TupleElementNames(new string[]
	{
		"point",
		"normal"
	})] List<ValueTuple<Vector3, Vector3>> Impacts)
	{
		if (this.ImpactSystem == null)
		{
			return;
		}
		int num = -1;
		for (int i = 0; i < Impacts.Count; i++)
		{
			num = i;
			if (this.ImpactEffects.Count <= i)
			{
				this.AddImpactEffect();
			}
			if (!this.ImpactEffects[i].isPlaying)
			{
				this.ImpactEffects[i].Play();
			}
			this.ImpactEffects[i].transform.position = Impacts[i].Item1;
			this.ImpactEffects[i].transform.forward = Impacts[i].Item2;
		}
		num++;
		for (int j = num; j < this.ImpactEffects.Count; j++)
		{
			if (this.ImpactEffects[j].isPlaying)
			{
				this.ImpactEffects[j].Stop();
			}
		}
	}

	// Token: 0x060000C9 RID: 201 RVA: 0x00009D24 File Offset: 0x00007F24
	private void AddImpactEffect()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ImpactSystem.gameObject);
		this.ImpactEffects.Add(gameObject.GetComponent<ParticleSystem>());
	}

	// Token: 0x060000CA RID: 202 RVA: 0x00009D54 File Offset: 0x00007F54
	internal override void FadeOut()
	{
		base.FadeOut();
		TubeMesh[] array = this.tubes;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].TickClipOut(Time.deltaTime * 3f);
		}
	}

	// Token: 0x060000CB RID: 203 RVA: 0x00009D8F File Offset: 0x00007F8F
	internal override void Finish()
	{
		Beam.AllBeams.Remove(this);
		base.Finish();
	}

	// Token: 0x060000CC RID: 204 RVA: 0x00009DA4 File Offset: 0x00007FA4
	private void OnDestroy()
	{
		Beam.AllBeams.Remove(this);
		foreach (ParticleSystem particleSystem in this.ImpactEffects)
		{
			if (particleSystem != null)
			{
				UnityEngine.Object.Destroy(particleSystem.gameObject);
			}
		}
	}

	// Token: 0x060000CD RID: 205 RVA: 0x00009E10 File Offset: 0x00008010
	public Beam()
	{
	}

	// Token: 0x060000CE RID: 206 RVA: 0x00009E97 File Offset: 0x00008097
	// Note: this type is marked as 'beforefieldinit'.
	static Beam()
	{
	}

	// Token: 0x040000C5 RID: 197
	public float Radius = 0.1f;

	// Token: 0x040000C6 RID: 198
	public bool IsNegative = true;

	// Token: 0x040000C7 RID: 199
	public bool FastUpdate;

	// Token: 0x040000C8 RID: 200
	public ParticleSystem ImpactSystem;

	// Token: 0x040000C9 RID: 201
	private BeamNode Properties;

	// Token: 0x040000CA RID: 202
	private TubeMesh[] tubes = new TubeMesh[0];

	// Token: 0x040000CB RID: 203
	private Dictionary<EntityControl, float> inside = new Dictionary<EntityControl, float>();

	// Token: 0x040000CC RID: 204
	private HashSet<EntityControl> everInside = new HashSet<EntityControl>();

	// Token: 0x040000CD RID: 205
	public bool TouchesEnvironment = true;

	// Token: 0x040000CE RID: 206
	public static List<Beam> AllBeams = new List<Beam>();

	// Token: 0x040000CF RID: 207
	public Transform BeamSoundPoint;

	// Token: 0x040000D0 RID: 208
	private RaycastHit[] physicsHitBuffer = new RaycastHit[32];

	// Token: 0x040000D1 RID: 209
	private List<RaycastHit> AllHits = new List<RaycastHit>();

	// Token: 0x040000D2 RID: 210
	private HashSet<Collider> hitColliders = new HashSet<Collider>();

	// Token: 0x040000D3 RID: 211
	private float cacheLengthMult = 1f;

	// Token: 0x040000D4 RID: 212
	private float cacheLengthT;

	// Token: 0x040000D5 RID: 213
	private List<ParticleSystem> ImpactEffects = new List<ParticleSystem>();

	// Token: 0x020003EE RID: 1006
	[CompilerGenerated]
	[Serializable]
	private sealed class <>c
	{
		// Token: 0x06002067 RID: 8295 RVA: 0x000C022D File Offset: 0x000BE42D
		// Note: this type is marked as 'beforefieldinit'.
		static <>c()
		{
		}

		// Token: 0x06002068 RID: 8296 RVA: 0x000C0239 File Offset: 0x000BE439
		public <>c()
		{
		}

		// Token: 0x06002069 RID: 8297 RVA: 0x000C0244 File Offset: 0x000BE444
		internal int <TickUpdate>b__20_0(RaycastHit x, RaycastHit y)
		{
			return x.distance.CompareTo(y.distance);
		}

		// Token: 0x040020E4 RID: 8420
		public static readonly Beam.<>c <>9 = new Beam.<>c();

		// Token: 0x040020E5 RID: 8421
		public static Comparison<RaycastHit> <>9__20_0;
	}
}
