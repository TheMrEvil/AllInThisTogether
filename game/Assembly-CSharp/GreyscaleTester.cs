using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x02000251 RID: 593
[ExecuteInEditMode]
public class GreyscaleTester : MonoBehaviour
{
	// Token: 0x060017EF RID: 6127 RVA: 0x00095B94 File Offset: 0x00093D94
	private void Awake()
	{
	}

	// Token: 0x060017F0 RID: 6128 RVA: 0x00095B96 File Offset: 0x00093D96
	private IEnumerator Start()
	{
		yield return true;
		this.Setup();
		yield break;
	}

	// Token: 0x060017F1 RID: 6129 RVA: 0x00095BA8 File Offset: 0x00093DA8
	private void Setup()
	{
		GreyscaleAreas.Init();
		this.zones = new Dictionary<Transform, GreyscaleAreas.GSZone>();
		if (this.Testers == null || this.Testers.Count == 0)
		{
			return;
		}
		foreach (Transform transform in this.Testers)
		{
			if (transform != null)
			{
				this.zones.Add(transform, GreyscaleAreas.AddZone(transform.position, this.Radius));
			}
		}
	}

	// Token: 0x060017F2 RID: 6130 RVA: 0x00095C40 File Offset: 0x00093E40
	private void Update()
	{
		if (this.zones == null)
		{
			this.zones = new Dictionary<Transform, GreyscaleAreas.GSZone>();
		}
		foreach (Transform transform in this.Testers)
		{
			if (transform != null && this.zones.ContainsKey(transform) && this.zones[transform] != null)
			{
				this.zones[transform].Update(transform.position, this.Radius);
			}
		}
		GreyscaleAreas.UpdateZones(this.InnerRadius);
	}

	// Token: 0x060017F3 RID: 6131 RVA: 0x00095CEC File Offset: 0x00093EEC
	private void TestPulse()
	{
		if (this.Testers.Count <= 0)
		{
			return;
		}
		base.StartCoroutine("PulseSequence");
	}

	// Token: 0x060017F4 RID: 6132 RVA: 0x00095D09 File Offset: 0x00093F09
	private IEnumerator PulseSequence()
	{
		Vector3 pos = this.Testers[0].position;
		GreyscaleAreas.GSZone zone = GreyscaleAreas.AddZone(pos, 0f);
		float t = 0f;
		while (t < 1f)
		{
			yield return true;
			t += Time.deltaTime / (this.PulseDuration / 1.5f);
			float num = this.PulseCurve.Evaluate(t) * 250f;
			float r = num;
			float innerRadius = Mathf.Max(0f, num - this.PulseWidth);
			zone.Update(pos, r);
			GreyscaleAreas.UpdateZones(innerRadius);
		}
		GreyscaleAreas.RemoveZone(zone);
		GreyscaleAreas.UpdateZones(0f);
		yield break;
	}

	// Token: 0x060017F5 RID: 6133 RVA: 0x00095D18 File Offset: 0x00093F18
	private void OnDrawGizmos()
	{
		if (GreyscaleAreas.Zones == null)
		{
			return;
		}
		Color black = Color.black;
		black.a = 0.5f;
		Gizmos.color = black;
		foreach (GreyscaleAreas.GSZone gszone in GreyscaleAreas.Zones)
		{
		}
	}

	// Token: 0x060017F6 RID: 6134 RVA: 0x00095D84 File Offset: 0x00093F84
	private void OnDestroy()
	{
		GreyscaleAreas.ClearZones();
	}

	// Token: 0x060017F7 RID: 6135 RVA: 0x00095D8B File Offset: 0x00093F8B
	public GreyscaleTester()
	{
	}

	// Token: 0x040017BA RID: 6074
	public List<Transform> Testers = new List<Transform>();

	// Token: 0x040017BB RID: 6075
	public float InnerRadius;

	// Token: 0x040017BC RID: 6076
	public float Radius;

	// Token: 0x040017BD RID: 6077
	public float PulseDuration = 2f;

	// Token: 0x040017BE RID: 6078
	public AnimationCurve PulseCurve;

	// Token: 0x040017BF RID: 6079
	public float PulseWidth = 7.5f;

	// Token: 0x040017C0 RID: 6080
	private Dictionary<Transform, GreyscaleAreas.GSZone> zones;

	// Token: 0x02000615 RID: 1557
	[CompilerGenerated]
	private sealed class <Start>d__8 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x0600272B RID: 10027 RVA: 0x000D512C File Offset: 0x000D332C
		[DebuggerHidden]
		public <Start>d__8(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x0600272C RID: 10028 RVA: 0x000D513B File Offset: 0x000D333B
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x0600272D RID: 10029 RVA: 0x000D5140 File Offset: 0x000D3340
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			GreyscaleTester greyscaleTester = this;
			if (num == 0)
			{
				this.<>1__state = -1;
				this.<>2__current = true;
				this.<>1__state = 1;
				return true;
			}
			if (num != 1)
			{
				return false;
			}
			this.<>1__state = -1;
			greyscaleTester.Setup();
			return false;
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x0600272E RID: 10030 RVA: 0x000D518E File Offset: 0x000D338E
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0600272F RID: 10031 RVA: 0x000D5196 File Offset: 0x000D3396
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06002730 RID: 10032 RVA: 0x000D519D File Offset: 0x000D339D
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x040029BB RID: 10683
		private int <>1__state;

		// Token: 0x040029BC RID: 10684
		private object <>2__current;

		// Token: 0x040029BD RID: 10685
		public GreyscaleTester <>4__this;
	}

	// Token: 0x02000616 RID: 1558
	[CompilerGenerated]
	private sealed class <PulseSequence>d__12 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06002731 RID: 10033 RVA: 0x000D51A5 File Offset: 0x000D33A5
		[DebuggerHidden]
		public <PulseSequence>d__12(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06002732 RID: 10034 RVA: 0x000D51B4 File Offset: 0x000D33B4
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06002733 RID: 10035 RVA: 0x000D51B8 File Offset: 0x000D33B8
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			GreyscaleTester greyscaleTester = this;
			if (num != 0)
			{
				if (num != 1)
				{
					return false;
				}
				this.<>1__state = -1;
				t += Time.deltaTime / (greyscaleTester.PulseDuration / 1.5f);
				float num2 = greyscaleTester.PulseCurve.Evaluate(t) * 250f;
				float r = num2;
				float innerRadius = Mathf.Max(0f, num2 - greyscaleTester.PulseWidth);
				zone.Update(pos, r);
				GreyscaleAreas.UpdateZones(innerRadius);
			}
			else
			{
				this.<>1__state = -1;
				pos = greyscaleTester.Testers[0].position;
				zone = GreyscaleAreas.AddZone(pos, 0f);
				t = 0f;
			}
			if (t >= 1f)
			{
				GreyscaleAreas.RemoveZone(zone);
				GreyscaleAreas.UpdateZones(0f);
				return false;
			}
			this.<>2__current = true;
			this.<>1__state = 1;
			return true;
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06002734 RID: 10036 RVA: 0x000D52C1 File Offset: 0x000D34C1
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06002735 RID: 10037 RVA: 0x000D52C9 File Offset: 0x000D34C9
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06002736 RID: 10038 RVA: 0x000D52D0 File Offset: 0x000D34D0
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x040029BE RID: 10686
		private int <>1__state;

		// Token: 0x040029BF RID: 10687
		private object <>2__current;

		// Token: 0x040029C0 RID: 10688
		public GreyscaleTester <>4__this;

		// Token: 0x040029C1 RID: 10689
		private Vector3 <pos>5__2;

		// Token: 0x040029C2 RID: 10690
		private GreyscaleAreas.GSZone <zone>5__3;

		// Token: 0x040029C3 RID: 10691
		private float <t>5__4;
	}
}
