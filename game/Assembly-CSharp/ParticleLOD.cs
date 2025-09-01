using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000237 RID: 567
public class ParticleLOD : MonoBehaviour
{
	// Token: 0x06001752 RID: 5970 RVA: 0x00093534 File Offset: 0x00091734
	private void OnEnable()
	{
		using (List<ParticleLOD.LODProps>.Enumerator enumerator = this.Systems.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (!enumerator.Current.Init())
				{
					Debug.LogError("ParticleLOD " + base.name + " has a null ParticleSystem", base.gameObject);
				}
			}
		}
		Scene_Settings.AddParticleLOD(this);
	}

	// Token: 0x06001753 RID: 5971 RVA: 0x000935AC File Offset: 0x000917AC
	public void TickUpdate(Vector3 pos)
	{
		foreach (ParticleLOD.LODProps lodprops in this.Systems)
		{
			bool flag = (lodprops.Position - pos).sqrMagnitude < lodprops.SqrDistance;
			if (!flag && !lodprops.IsStopped)
			{
				lodprops.Stop();
			}
			else if (flag && lodprops.IsStopped)
			{
				lodprops.Play();
			}
		}
	}

	// Token: 0x06001754 RID: 5972 RVA: 0x0009363C File Offset: 0x0009183C
	private void OnDisable()
	{
		Scene_Settings.RemoveParticleLOD(this);
	}

	// Token: 0x06001755 RID: 5973 RVA: 0x00093644 File Offset: 0x00091844
	private void OnDestroy()
	{
		Scene_Settings.RemoveParticleLOD(this);
	}

	// Token: 0x06001756 RID: 5974 RVA: 0x0009364C File Offset: 0x0009184C
	public ParticleLOD()
	{
	}

	// Token: 0x0400170D RID: 5901
	public List<ParticleLOD.LODProps> Systems = new List<ParticleLOD.LODProps>();

	// Token: 0x02000600 RID: 1536
	[Serializable]
	public class LODProps
	{
		// Token: 0x1700037C RID: 892
		// (get) Token: 0x060026C6 RID: 9926 RVA: 0x000D419F File Offset: 0x000D239F
		public Vector3 Position
		{
			get
			{
				if (!this.IsStatic)
				{
					return this.Transform.position;
				}
				return this.pos;
			}
		}

		// Token: 0x060026C7 RID: 9927 RVA: 0x000D41BB File Offset: 0x000D23BB
		public void Play()
		{
			if (!this.IsStopped)
			{
				return;
			}
			this.IsStopped = false;
			this.ps.Play();
		}

		// Token: 0x060026C8 RID: 9928 RVA: 0x000D41D8 File Offset: 0x000D23D8
		public void Stop()
		{
			if (this.IsStopped)
			{
				return;
			}
			this.IsStopped = true;
			this.ps.Stop();
		}

		// Token: 0x060026C9 RID: 9929 RVA: 0x000D41F8 File Offset: 0x000D23F8
		public bool Init()
		{
			if (this.ps == null)
			{
				return false;
			}
			this.SqrDistance = this.Distance * this.Distance;
			this.Transform = this.ps.transform;
			this.objRef = this.ps.gameObject;
			this.pos = this.Transform.position;
			return true;
		}

		// Token: 0x060026CA RID: 9930 RVA: 0x000D425C File Offset: 0x000D245C
		public LODProps()
		{
		}

		// Token: 0x0400296D RID: 10605
		public ParticleSystem ps;

		// Token: 0x0400296E RID: 10606
		public GameObject objRef;

		// Token: 0x0400296F RID: 10607
		[Range(5f, 250f)]
		public float Distance = 30f;

		// Token: 0x04002970 RID: 10608
		public bool IsStatic;

		// Token: 0x04002971 RID: 10609
		[NonSerialized]
		public float SqrDistance;

		// Token: 0x04002972 RID: 10610
		[NonSerialized]
		public Transform Transform;

		// Token: 0x04002973 RID: 10611
		[NonSerialized]
		public Vector3 pos;

		// Token: 0x04002974 RID: 10612
		[NonSerialized]
		public bool IsStopped;
	}
}
