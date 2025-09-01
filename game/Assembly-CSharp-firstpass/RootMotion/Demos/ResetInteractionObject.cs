using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using RootMotion.FinalIK;
using UnityEngine;

namespace RootMotion.Demos
{
	// Token: 0x0200014A RID: 330
	public class ResetInteractionObject : MonoBehaviour
	{
		// Token: 0x06000D2C RID: 3372 RVA: 0x00059654 File Offset: 0x00057854
		private void Start()
		{
			this.defaultPosition = base.transform.position;
			this.defaultRotation = base.transform.rotation;
			this.defaultParent = base.transform.parent;
			this.r = base.GetComponent<Rigidbody>();
		}

		// Token: 0x06000D2D RID: 3373 RVA: 0x000596A0 File Offset: 0x000578A0
		private void OnPickUp(Transform t)
		{
			base.StopAllCoroutines();
			base.StartCoroutine(this.ResetObject(Time.time + this.resetDelay));
		}

		// Token: 0x06000D2E RID: 3374 RVA: 0x000596C1 File Offset: 0x000578C1
		private IEnumerator ResetObject(float resetTime)
		{
			while (Time.time < resetTime)
			{
				yield return null;
			}
			Poser component = base.transform.parent.GetComponent<Poser>();
			if (component != null)
			{
				component.poseRoot = null;
				component.weight = 0f;
			}
			base.transform.parent = this.defaultParent;
			base.transform.position = this.defaultPosition;
			base.transform.rotation = this.defaultRotation;
			if (this.r != null)
			{
				this.r.isKinematic = false;
			}
			yield break;
		}

		// Token: 0x06000D2F RID: 3375 RVA: 0x000596D7 File Offset: 0x000578D7
		public ResetInteractionObject()
		{
		}

		// Token: 0x04000AD9 RID: 2777
		public float resetDelay = 1f;

		// Token: 0x04000ADA RID: 2778
		private Vector3 defaultPosition;

		// Token: 0x04000ADB RID: 2779
		private Quaternion defaultRotation;

		// Token: 0x04000ADC RID: 2780
		private Transform defaultParent;

		// Token: 0x04000ADD RID: 2781
		private Rigidbody r;

		// Token: 0x02000234 RID: 564
		[CompilerGenerated]
		private sealed class <ResetObject>d__7 : IEnumerator<object>, IEnumerator, IDisposable
		{
			// Token: 0x060011AF RID: 4527 RVA: 0x0006DD69 File Offset: 0x0006BF69
			[DebuggerHidden]
			public <ResetObject>d__7(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x060011B0 RID: 4528 RVA: 0x0006DD78 File Offset: 0x0006BF78
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x060011B1 RID: 4529 RVA: 0x0006DD7C File Offset: 0x0006BF7C
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				ResetInteractionObject resetInteractionObject = this;
				if (num != 0)
				{
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -1;
				}
				else
				{
					this.<>1__state = -1;
				}
				if (Time.time >= resetTime)
				{
					Poser component = resetInteractionObject.transform.parent.GetComponent<Poser>();
					if (component != null)
					{
						component.poseRoot = null;
						component.weight = 0f;
					}
					resetInteractionObject.transform.parent = resetInteractionObject.defaultParent;
					resetInteractionObject.transform.position = resetInteractionObject.defaultPosition;
					resetInteractionObject.transform.rotation = resetInteractionObject.defaultRotation;
					if (resetInteractionObject.r != null)
					{
						resetInteractionObject.r.isKinematic = false;
					}
					return false;
				}
				this.<>2__current = null;
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x1700025D RID: 605
			// (get) Token: 0x060011B2 RID: 4530 RVA: 0x0006DE47 File Offset: 0x0006C047
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060011B3 RID: 4531 RVA: 0x0006DE4F File Offset: 0x0006C04F
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x1700025E RID: 606
			// (get) Token: 0x060011B4 RID: 4532 RVA: 0x0006DE56 File Offset: 0x0006C056
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0400109B RID: 4251
			private int <>1__state;

			// Token: 0x0400109C RID: 4252
			private object <>2__current;

			// Token: 0x0400109D RID: 4253
			public float resetTime;

			// Token: 0x0400109E RID: 4254
			public ResetInteractionObject <>4__this;
		}
	}
}
