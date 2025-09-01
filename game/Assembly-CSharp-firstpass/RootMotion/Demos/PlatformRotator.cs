using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace RootMotion.Demos
{
	// Token: 0x02000151 RID: 337
	public class PlatformRotator : MonoBehaviour
	{
		// Token: 0x06000D45 RID: 3397 RVA: 0x00059C9C File Offset: 0x00057E9C
		private void Start()
		{
			this.defaultRotation = base.transform.rotation;
			this.targetPosition = base.transform.position + this.movePosition;
			this.r = base.GetComponent<Rigidbody>();
			base.StartCoroutine(this.SwitchRotation());
		}

		// Token: 0x06000D46 RID: 3398 RVA: 0x00059CF0 File Offset: 0x00057EF0
		private void FixedUpdate()
		{
			this.r.MovePosition(Vector3.SmoothDamp(this.r.position, this.targetPosition, ref this.velocity, 1f, this.moveSpeed));
			if (Vector3.Distance(base.GetComponent<Rigidbody>().position, this.targetPosition) < 0.1f)
			{
				this.movePosition = -this.movePosition;
				this.targetPosition += this.movePosition;
			}
			this.r.MoveRotation(Quaternion.RotateTowards(this.r.rotation, this.targetRotation, this.rotationSpeed * Time.deltaTime));
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x00059DA1 File Offset: 0x00057FA1
		private IEnumerator SwitchRotation()
		{
			for (;;)
			{
				float angle = UnityEngine.Random.Range(-this.maxAngle, this.maxAngle);
				Vector3 onUnitSphere = UnityEngine.Random.onUnitSphere;
				this.targetRotation = Quaternion.AngleAxis(angle, onUnitSphere) * this.defaultRotation;
				yield return new WaitForSeconds(this.switchRotationTime + UnityEngine.Random.value * this.random);
			}
			yield break;
		}

		// Token: 0x06000D48 RID: 3400 RVA: 0x00059DB0 File Offset: 0x00057FB0
		private void OnCollisionEnter(Collision collision)
		{
			if (collision.gameObject.layer == this.characterLayer)
			{
				CharacterThirdPerson component = collision.gameObject.GetComponent<CharacterThirdPerson>();
				if (component == null)
				{
					return;
				}
				if (component.smoothPhysics)
				{
					component.smoothPhysics = false;
				}
			}
		}

		// Token: 0x06000D49 RID: 3401 RVA: 0x00059DF8 File Offset: 0x00057FF8
		private void OnCollisionExit(Collision collision)
		{
			if (collision.gameObject.layer == this.characterLayer)
			{
				CharacterThirdPerson component = collision.gameObject.GetComponent<CharacterThirdPerson>();
				if (component == null)
				{
					return;
				}
				component.smoothPhysics = true;
			}
		}

		// Token: 0x06000D4A RID: 3402 RVA: 0x00059E35 File Offset: 0x00058035
		public PlatformRotator()
		{
		}

		// Token: 0x04000AEF RID: 2799
		public float maxAngle = 70f;

		// Token: 0x04000AF0 RID: 2800
		public float switchRotationTime = 0.5f;

		// Token: 0x04000AF1 RID: 2801
		public float random = 0.5f;

		// Token: 0x04000AF2 RID: 2802
		public float rotationSpeed = 50f;

		// Token: 0x04000AF3 RID: 2803
		public Vector3 movePosition;

		// Token: 0x04000AF4 RID: 2804
		public float moveSpeed = 5f;

		// Token: 0x04000AF5 RID: 2805
		public int characterLayer;

		// Token: 0x04000AF6 RID: 2806
		private Quaternion defaultRotation;

		// Token: 0x04000AF7 RID: 2807
		private Quaternion targetRotation;

		// Token: 0x04000AF8 RID: 2808
		private Vector3 targetPosition;

		// Token: 0x04000AF9 RID: 2809
		private Vector3 velocity;

		// Token: 0x04000AFA RID: 2810
		private Rigidbody r;

		// Token: 0x02000237 RID: 567
		[CompilerGenerated]
		private sealed class <SwitchRotation>d__14 : IEnumerator<object>, IEnumerator, IDisposable
		{
			// Token: 0x060011C4 RID: 4548 RVA: 0x0006E504 File Offset: 0x0006C704
			[DebuggerHidden]
			public <SwitchRotation>d__14(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x060011C5 RID: 4549 RVA: 0x0006E513 File Offset: 0x0006C713
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x060011C6 RID: 4550 RVA: 0x0006E518 File Offset: 0x0006C718
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				PlatformRotator platformRotator = this;
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
				float angle = UnityEngine.Random.Range(-platformRotator.maxAngle, platformRotator.maxAngle);
				Vector3 onUnitSphere = UnityEngine.Random.onUnitSphere;
				platformRotator.targetRotation = Quaternion.AngleAxis(angle, onUnitSphere) * platformRotator.defaultRotation;
				this.<>2__current = new WaitForSeconds(platformRotator.switchRotationTime + UnityEngine.Random.value * platformRotator.random);
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x17000261 RID: 609
			// (get) Token: 0x060011C7 RID: 4551 RVA: 0x0006E5A3 File Offset: 0x0006C7A3
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060011C8 RID: 4552 RVA: 0x0006E5AB File Offset: 0x0006C7AB
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000262 RID: 610
			// (get) Token: 0x060011C9 RID: 4553 RVA: 0x0006E5B2 File Offset: 0x0006C7B2
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x040010B7 RID: 4279
			private int <>1__state;

			// Token: 0x040010B8 RID: 4280
			private object <>2__current;

			// Token: 0x040010B9 RID: 4281
			public PlatformRotator <>4__this;
		}
	}
}
