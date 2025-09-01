using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace DamageNumbersPro.Demo
{
	// Token: 0x0200001C RID: 28
	public class DNP_Target : MonoBehaviour
	{
		// Token: 0x060000B4 RID: 180 RVA: 0x00007C80 File Offset: 0x00005E80
		private void Start()
		{
			this.mat = base.GetComponent<MeshRenderer>().material;
			this.defaultBrightness = this.mat.GetFloat("_Brightness");
			this.flipping = false;
			this.originalPosition = base.transform.position;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00007CCC File Offset: 0x00005ECC
		private void Update()
		{
			base.transform.position = this.originalPosition + this.movementOffset * Mathf.Sin(Time.time);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00007CFC File Offset: 0x00005EFC
		public void Hit()
		{
			if (this.hitRoutine != null)
			{
				base.StopCoroutine(this.hitRoutine);
			}
			this.hitRoutine = base.StartCoroutine(this.HitCoroutine());
			if (!this.flipping)
			{
				if (this.flipRoutine != null)
				{
					base.StopCoroutine(this.flipRoutine);
				}
				this.flipRoutine = base.StartCoroutine(this.FlipCoroutine());
			}
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00007D5D File Offset: 0x00005F5D
		private IEnumerator HitCoroutine()
		{
			float brightness = 1f;
			while (brightness < 3f)
			{
				brightness = Mathf.Min(3f, Mathf.Lerp(brightness, 3.1f, Time.deltaTime * 20f));
				this.mat.SetFloat("_Brightness", brightness);
				yield return null;
			}
			while (brightness > this.defaultBrightness)
			{
				brightness = Mathf.Max(this.defaultBrightness, Mathf.Lerp(brightness, this.defaultBrightness - 0.1f, Time.deltaTime * 10f));
				this.mat.SetFloat("_Brightness", brightness);
				yield return null;
			}
			yield break;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00007D6C File Offset: 0x00005F6C
		private IEnumerator FlipCoroutine()
		{
			this.flipping = true;
			float angle = 0f;
			while (angle < 180f)
			{
				angle = Mathf.Min(180f, Mathf.Lerp(angle, 190f, Time.deltaTime * 7f));
				base.transform.eulerAngles = new Vector3(angle, 0f, 0f);
				yield return null;
				if (angle > 150f)
				{
					this.flipping = false;
				}
			}
			yield break;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00007D7B File Offset: 0x00005F7B
		public DNP_Target()
		{
		}

		// Token: 0x04000168 RID: 360
		public Vector3 movementOffset = new Vector3(0f, 0f, 0f);

		// Token: 0x04000169 RID: 361
		private Material mat;

		// Token: 0x0400016A RID: 362
		private float defaultBrightness;

		// Token: 0x0400016B RID: 363
		private Coroutine hitRoutine;

		// Token: 0x0400016C RID: 364
		private Coroutine flipRoutine;

		// Token: 0x0400016D RID: 365
		private bool flipping;

		// Token: 0x0400016E RID: 366
		private Vector3 originalPosition;

		// Token: 0x0200001E RID: 30
		[CompilerGenerated]
		private sealed class <HitCoroutine>d__10 : IEnumerator<object>, IEnumerator, IDisposable
		{
			// Token: 0x060000C0 RID: 192 RVA: 0x00007F7B File Offset: 0x0000617B
			[DebuggerHidden]
			public <HitCoroutine>d__10(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x060000C1 RID: 193 RVA: 0x00007F8A File Offset: 0x0000618A
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x060000C2 RID: 194 RVA: 0x00007F8C File Offset: 0x0000618C
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				DNP_Target dnp_Target = this;
				switch (num)
				{
				case 0:
					this.<>1__state = -1;
					brightness = 1f;
					break;
				case 1:
					this.<>1__state = -1;
					break;
				case 2:
					this.<>1__state = -1;
					goto IL_FD;
				default:
					return false;
				}
				if (brightness < 3f)
				{
					brightness = Mathf.Min(3f, Mathf.Lerp(brightness, 3.1f, Time.deltaTime * 20f));
					dnp_Target.mat.SetFloat("_Brightness", brightness);
					this.<>2__current = null;
					this.<>1__state = 1;
					return true;
				}
				IL_FD:
				if (brightness <= dnp_Target.defaultBrightness)
				{
					return false;
				}
				brightness = Mathf.Max(dnp_Target.defaultBrightness, Mathf.Lerp(brightness, dnp_Target.defaultBrightness - 0.1f, Time.deltaTime * 10f));
				dnp_Target.mat.SetFloat("_Brightness", brightness);
				this.<>2__current = null;
				this.<>1__state = 2;
				return true;
			}

			// Token: 0x17000003 RID: 3
			// (get) Token: 0x060000C3 RID: 195 RVA: 0x000080A5 File Offset: 0x000062A5
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060000C4 RID: 196 RVA: 0x000080AD File Offset: 0x000062AD
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000004 RID: 4
			// (get) Token: 0x060000C5 RID: 197 RVA: 0x000080B4 File Offset: 0x000062B4
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x04000173 RID: 371
			private int <>1__state;

			// Token: 0x04000174 RID: 372
			private object <>2__current;

			// Token: 0x04000175 RID: 373
			public DNP_Target <>4__this;

			// Token: 0x04000176 RID: 374
			private float <brightness>5__2;
		}

		// Token: 0x0200001F RID: 31
		[CompilerGenerated]
		private sealed class <FlipCoroutine>d__11 : IEnumerator<object>, IEnumerator, IDisposable
		{
			// Token: 0x060000C6 RID: 198 RVA: 0x000080BC File Offset: 0x000062BC
			[DebuggerHidden]
			public <FlipCoroutine>d__11(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x060000C7 RID: 199 RVA: 0x000080CB File Offset: 0x000062CB
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x060000C8 RID: 200 RVA: 0x000080D0 File Offset: 0x000062D0
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				DNP_Target dnp_Target = this;
				if (num != 0)
				{
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -1;
					if (angle > 150f)
					{
						dnp_Target.flipping = false;
					}
				}
				else
				{
					this.<>1__state = -1;
					dnp_Target.flipping = true;
					angle = 0f;
				}
				if (angle >= 180f)
				{
					return false;
				}
				angle = Mathf.Min(180f, Mathf.Lerp(angle, 190f, Time.deltaTime * 7f));
				dnp_Target.transform.eulerAngles = new Vector3(angle, 0f, 0f);
				this.<>2__current = null;
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x17000005 RID: 5
			// (get) Token: 0x060000C9 RID: 201 RVA: 0x00008196 File Offset: 0x00006396
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060000CA RID: 202 RVA: 0x0000819E File Offset: 0x0000639E
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000006 RID: 6
			// (get) Token: 0x060000CB RID: 203 RVA: 0x000081A5 File Offset: 0x000063A5
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x04000177 RID: 375
			private int <>1__state;

			// Token: 0x04000178 RID: 376
			private object <>2__current;

			// Token: 0x04000179 RID: 377
			public DNP_Target <>4__this;

			// Token: 0x0400017A RID: 378
			private float <angle>5__2;
		}
	}
}
