using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000C4 RID: 196
public class PickupObj : MonoBehaviour
{
	// Token: 0x060008FA RID: 2298 RVA: 0x0003CCDC File Offset: 0x0003AEDC
	private void Awake()
	{
		this.rb = base.GetComponent<Rigidbody>();
		this.colliders = base.GetComponents<Collider>();
		this.meshes = new List<MeshRenderer>();
		MeshRenderer component = base.GetComponent<MeshRenderer>();
		if (component != null)
		{
			this.meshes.Add(component);
		}
		foreach (MeshRenderer item in base.GetComponentsInChildren<MeshRenderer>())
		{
			this.meshes.Add(item);
		}
	}

	// Token: 0x060008FB RID: 2299 RVA: 0x0003CD50 File Offset: 0x0003AF50
	private void OnEnable()
	{
		PickupManager.RegisterPickup(this);
		Collider[] array = this.colliders;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].enabled = true;
		}
		this.pullTime = 0f;
		this.isTriggered = false;
		this.timeAlive = 0f;
		this.isDead = false;
		this.ExplodeOut();
		if (this.AutoPickup)
		{
			this.TriggerPickup();
		}
	}

	// Token: 0x060008FC RID: 2300 RVA: 0x0003CDBC File Offset: 0x0003AFBC
	public void TickUpdate(float delta)
	{
		if (this.rb != null && this.rb.IsSleeping() && !this.rb.isKinematic)
		{
			this.DisablePhysics();
		}
		if (PlayerControl.myInstance == null || this.isDead)
		{
			return;
		}
		this.timeAlive += delta;
		Vector3 position = PlayerControl.myInstance.display.CenterOfMass.position;
		if (this.isTriggered)
		{
			this.pullTime += delta;
			this.velocity += Physics.gravity * delta * 0.6f;
			this.velocity -= this.velocity * 0.3f * delta;
			Vector3 vector = base.transform.position;
			vector += this.velocity * Time.deltaTime;
			vector = Vector3.MoveTowards(vector, position, this.pullTime * this.pullTime * delta * 12f);
			base.transform.position = vector;
			if (Vector3.Distance(position, base.transform.position) < 1f || this.pullTime > 3f)
			{
				this.Consume();
			}
		}
		else if (Vector3.Distance(position, base.transform.position) < 7f)
		{
			this.TriggerPickup();
		}
		if (!this.isTriggered && this.timeAlive > 25f)
		{
			ActionPool.ReleaseObject(base.gameObject);
		}
	}

	// Token: 0x060008FD RID: 2301 RVA: 0x0003CF50 File Offset: 0x0003B150
	private void Consume()
	{
		if (this.OnPickup != null)
		{
			EffectProperties effectProperties = new EffectProperties();
			effectProperties.Affected = PlayerControl.myInstance.gameObject;
			effectProperties.SourceControl = PlayerControl.myInstance;
			effectProperties.StartLoc = (effectProperties.OutLoc = global::Pose.WorldPoint(PlayerControl.myInstance.display.CenterOfMass.position, PlayerControl.myInstance.display.GetLocation(ActionLocation.Front).root.forward));
			effectProperties.EffectSource = EffectSource.Pickup;
			effectProperties.AbilityType = PlayerAbilityType.None;
			PlayerControl.myInstance.net.ExecuteActionTree(this.OnPickup.RootNode.guid, effectProperties);
		}
		this.Kill();
	}

	// Token: 0x060008FE RID: 2302 RVA: 0x0003D008 File Offset: 0x0003B208
	private void TriggerPickup()
	{
		this.isTriggered = true;
		this.DisablePhysics();
		this.velocity += Vector3.up * UnityEngine.Random.Range(2f, 5.25f);
		this.pullTime = 0f;
	}

	// Token: 0x060008FF RID: 2303 RVA: 0x0003D058 File Offset: 0x0003B258
	private void DisablePhysics()
	{
		if (this.rb == null)
		{
			return;
		}
		this.velocity = this.rb.velocity;
		this.rb.isKinematic = true;
		Collider[] array = this.colliders;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].enabled = false;
		}
	}

	// Token: 0x06000900 RID: 2304 RVA: 0x0003D0B0 File Offset: 0x0003B2B0
	public void ExplodeOut()
	{
		base.transform.rotation = UnityEngine.Random.rotation;
		Vector3 normalized = new Vector3(UnityEngine.Random.Range(-1f, 1f), 0.25f, UnityEngine.Random.Range(-1f, 1f)).normalized;
		float num = UnityEngine.Random.Range(this.ExpForce.x, this.ExpForce.y);
		this.velocity = num * normalized * 0.1f;
		if (this.rb == null)
		{
			return;
		}
		this.rb.isKinematic = false;
		this.rb.AddExplosionForce(num, base.transform.position + normalized, 10f, this.VerticalComp);
	}

	// Token: 0x06000901 RID: 2305 RVA: 0x0003D174 File Offset: 0x0003B374
	private void Kill()
	{
		this.isDead = true;
		if (this.DestroyTime <= 0f)
		{
			ActionPool.ReleaseObject(base.gameObject);
			return;
		}
		ParticleSystem[] componentsInParent = base.GetComponentsInParent<ParticleSystem>();
		for (int i = 0; i < componentsInParent.Length; i++)
		{
			componentsInParent[i].Stop();
		}
		foreach (MeshRenderer meshRenderer in this.meshes)
		{
			meshRenderer.enabled = false;
		}
		ActionPool.ReleaseDelayed(base.gameObject, this.DestroyTime);
	}

	// Token: 0x06000902 RID: 2306 RVA: 0x0003D214 File Offset: 0x0003B414
	private void OnDisable()
	{
		PickupManager.UnregisterPickup(this);
	}

	// Token: 0x06000903 RID: 2307 RVA: 0x0003D21C File Offset: 0x0003B41C
	public PickupObj()
	{
	}

	// Token: 0x04000789 RID: 1929
	public bool AutoPickup;

	// Token: 0x0400078A RID: 1930
	private Rigidbody rb;

	// Token: 0x0400078B RID: 1931
	public Vector2 ExpForce = new Vector2(40f, 60f);

	// Token: 0x0400078C RID: 1932
	[Range(0f, 1f)]
	public float VerticalComp = 0.1f;

	// Token: 0x0400078D RID: 1933
	private Collider[] colliders;

	// Token: 0x0400078E RID: 1934
	private bool isTriggered;

	// Token: 0x0400078F RID: 1935
	private float pullTime;

	// Token: 0x04000790 RID: 1936
	private float timeAlive;

	// Token: 0x04000791 RID: 1937
	private Vector3 velocity = Vector3.zero;

	// Token: 0x04000792 RID: 1938
	public ActionTree OnPickup;

	// Token: 0x04000793 RID: 1939
	public float DestroyTime;

	// Token: 0x04000794 RID: 1940
	private bool isDead;

	// Token: 0x04000795 RID: 1941
	private List<MeshRenderer> meshes;
}
