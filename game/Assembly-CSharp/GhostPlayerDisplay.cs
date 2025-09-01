using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x02000084 RID: 132
public class GhostPlayerDisplay : MonoBehaviour
{
	// Token: 0x060005A3 RID: 1443 RVA: 0x00028C47 File Offset: 0x00026E47
	private void Awake()
	{
		this.playerControl = base.GetComponentInParent<PlayerControl>();
		if (this.groundPoint == Vector3.zero)
		{
			this.groundPoint = base.transform.position;
		}
	}

	// Token: 0x060005A4 RID: 1444 RVA: 0x00028C78 File Offset: 0x00026E78
	public void Activate(Vector3 Pos)
	{
		Vector3 vector = AIManager.NearestNavPoint(Pos, -1f);
		if (!vector.IsValid())
		{
			return;
		}
		this.groundPoint = vector;
		base.transform.position = vector + Vector3.up * 1.33f;
		base.gameObject.SetActive(true);
		this.IsActive = true;
		base.StartCoroutine("CheckGrounded");
	}

	// Token: 0x060005A5 RID: 1445 RVA: 0x00028CE0 File Offset: 0x00026EE0
	private IEnumerator CheckGrounded()
	{
		for (;;)
		{
			yield return new WaitForSeconds(0.5f);
			Vector3 a = AIManager.NearestNavPoint(base.transform.position, -1f);
			if (Vector3.Distance(a, base.transform.position) > 5f)
			{
				base.transform.position = a + Vector3.up * 1.33f;
				this.groundPoint = a;
			}
		}
		yield break;
	}

	// Token: 0x060005A6 RID: 1446 RVA: 0x00028CF0 File Offset: 0x00026EF0
	private void OnEnable()
	{
		PickupManager.RegisterGhost(this);
		this.VFX.Play();
		if (this.playerControl == null || !this.playerControl.IsMine)
		{
			this.External_VFX.Play();
			this.Projector.Show();
		}
	}

	// Token: 0x060005A7 RID: 1447 RVA: 0x00028D3F File Offset: 0x00026F3F
	private void OnDisable()
	{
		PickupManager.UnregisterGhost(this);
		this.External_VFX.Stop();
		this.VFX.Stop();
		this.IsActive = false;
		base.StopAllCoroutines();
	}

	// Token: 0x060005A8 RID: 1448 RVA: 0x00028D6A File Offset: 0x00026F6A
	public GhostPlayerDisplay()
	{
	}

	// Token: 0x0400046E RID: 1134
	public Transform UIRoot;

	// Token: 0x0400046F RID: 1135
	public ParticleSystem VFX;

	// Token: 0x04000470 RID: 1136
	public ParticleSystem External_VFX;

	// Token: 0x04000471 RID: 1137
	public DynamicDecal Projector;

	// Token: 0x04000472 RID: 1138
	public MeshRenderer[] Meshes;

	// Token: 0x04000473 RID: 1139
	[NonSerialized]
	public bool IsActive;

	// Token: 0x04000474 RID: 1140
	[NonSerialized]
	public PlayerControl playerControl;

	// Token: 0x04000475 RID: 1141
	[NonSerialized]
	public Vector3 groundPoint;

	// Token: 0x04000476 RID: 1142
	private const float SPAWN_HEIGHT = 1.33f;

	// Token: 0x0200049D RID: 1181
	[CompilerGenerated]
	private sealed class <CheckGrounded>d__11 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06002220 RID: 8736 RVA: 0x000C5A5E File Offset: 0x000C3C5E
		[DebuggerHidden]
		public <CheckGrounded>d__11(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06002221 RID: 8737 RVA: 0x000C5A6D File Offset: 0x000C3C6D
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06002222 RID: 8738 RVA: 0x000C5A70 File Offset: 0x000C3C70
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			GhostPlayerDisplay ghostPlayerDisplay = this;
			if (num != 0)
			{
				if (num != 1)
				{
					return false;
				}
				this.<>1__state = -1;
				Vector3 vector = AIManager.NearestNavPoint(ghostPlayerDisplay.transform.position, -1f);
				if (Vector3.Distance(vector, ghostPlayerDisplay.transform.position) > 5f)
				{
					ghostPlayerDisplay.transform.position = vector + Vector3.up * 1.33f;
					ghostPlayerDisplay.groundPoint = vector;
				}
			}
			else
			{
				this.<>1__state = -1;
			}
			this.<>2__current = new WaitForSeconds(0.5f);
			this.<>1__state = 1;
			return true;
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06002223 RID: 8739 RVA: 0x000C5B11 File Offset: 0x000C3D11
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06002224 RID: 8740 RVA: 0x000C5B19 File Offset: 0x000C3D19
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06002225 RID: 8741 RVA: 0x000C5B20 File Offset: 0x000C3D20
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04002390 RID: 9104
		private int <>1__state;

		// Token: 0x04002391 RID: 9105
		private object <>2__current;

		// Token: 0x04002392 RID: 9106
		public GhostPlayerDisplay <>4__this;
	}
}
