using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x020000F5 RID: 245
public class SignatureUnlocker : MonoBehaviour
{
	// Token: 0x06000B8C RID: 2956 RVA: 0x0004B61C File Offset: 0x0004981C
	private void Awake()
	{
		SignatureUnlocker.instance = this;
	}

	// Token: 0x06000B8D RID: 2957 RVA: 0x0004B624 File Offset: 0x00049824
	public void Test()
	{
		this.Unlocked(MagicColor.Yellow);
	}

	// Token: 0x06000B8E RID: 2958 RVA: 0x0004B62D File Offset: 0x0004982D
	public void Unlocked(MagicColor color)
	{
		base.StartCoroutine(this.UnlockSequence(color));
	}

	// Token: 0x06000B8F RID: 2959 RVA: 0x0004B63D File Offset: 0x0004983D
	private IEnumerator UnlockSequence(MagicColor color)
	{
		AudioManager.PlayInterfaceSFX(this.StartClip, 1f, 0f);
		PlayerControl.myInstance.Input.LockoutInput(6f);
		PlayerControl.myInstance.Input.OverrideCamera(this.CameraLoc, 2f, false);
		PlayerControl.myInstance.movement.SetPositionImmediate(this.TeleportLoc.position, this.TeleportLoc.forward, true);
		yield return new WaitForSeconds(3f);
		AudioManager.PlayInterfaceSFX(this.FXClip, 1f, 0f);
		yield return new WaitForSeconds(3f);
		PlayerControl.myInstance.Input.ReturnCamera(6f, true);
		yield break;
	}

	// Token: 0x06000B90 RID: 2960 RVA: 0x0004B64C File Offset: 0x0004984C
	public SignatureUnlocker()
	{
	}

	// Token: 0x04000973 RID: 2419
	public Transform CameraLoc;

	// Token: 0x04000974 RID: 2420
	public Transform TeleportLoc;

	// Token: 0x04000975 RID: 2421
	public ParticleSystem UnlockFX;

	// Token: 0x04000976 RID: 2422
	public AudioClip StartClip;

	// Token: 0x04000977 RID: 2423
	public AudioClip FXClip;

	// Token: 0x04000978 RID: 2424
	private static SignatureUnlocker instance;

	// Token: 0x04000979 RID: 2425
	private Transform camHolderParent;

	// Token: 0x020004F3 RID: 1267
	[CompilerGenerated]
	private sealed class <UnlockSequence>d__10 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06002355 RID: 9045 RVA: 0x000C9616 File Offset: 0x000C7816
		[DebuggerHidden]
		public <UnlockSequence>d__10(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06002356 RID: 9046 RVA: 0x000C9625 File Offset: 0x000C7825
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06002357 RID: 9047 RVA: 0x000C9628 File Offset: 0x000C7828
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			SignatureUnlocker signatureUnlocker = this;
			switch (num)
			{
			case 0:
				this.<>1__state = -1;
				AudioManager.PlayInterfaceSFX(signatureUnlocker.StartClip, 1f, 0f);
				PlayerControl.myInstance.Input.LockoutInput(6f);
				PlayerControl.myInstance.Input.OverrideCamera(signatureUnlocker.CameraLoc, 2f, false);
				PlayerControl.myInstance.movement.SetPositionImmediate(signatureUnlocker.TeleportLoc.position, signatureUnlocker.TeleportLoc.forward, true);
				this.<>2__current = new WaitForSeconds(3f);
				this.<>1__state = 1;
				return true;
			case 1:
				this.<>1__state = -1;
				AudioManager.PlayInterfaceSFX(signatureUnlocker.FXClip, 1f, 0f);
				this.<>2__current = new WaitForSeconds(3f);
				this.<>1__state = 2;
				return true;
			case 2:
				this.<>1__state = -1;
				PlayerControl.myInstance.Input.ReturnCamera(6f, true);
				return false;
			default:
				return false;
			}
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06002358 RID: 9048 RVA: 0x000C9733 File Offset: 0x000C7933
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06002359 RID: 9049 RVA: 0x000C973B File Offset: 0x000C793B
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x0600235A RID: 9050 RVA: 0x000C9742 File Offset: 0x000C7942
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04002523 RID: 9507
		private int <>1__state;

		// Token: 0x04002524 RID: 9508
		private object <>2__current;

		// Token: 0x04002525 RID: 9509
		public SignatureUnlocker <>4__this;
	}
}
