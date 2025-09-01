using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace MagicFX5
{
	// Token: 0x02000022 RID: 34
	public class MagicFX5_StarParticleEmission : MonoBehaviour
	{
		// Token: 0x060000B8 RID: 184 RVA: 0x000060BE File Offset: 0x000042BE
		private void Awake()
		{
			this._ps = base.GetComponent<ParticleSystem>();
			this._psShape = this._ps.shape;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000060DD File Offset: 0x000042DD
		private void OnEnable()
		{
			base.StartCoroutine(this.Emit());
		}

		// Token: 0x060000BA RID: 186 RVA: 0x000060EC File Offset: 0x000042EC
		private void OnDisable()
		{
			base.StopCoroutine(this.Emit());
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000060FA File Offset: 0x000042FA
		private IEnumerator Emit()
		{
			float angleStep = 360f / (float)this._indexRemap.Count;
			angleStep += base.transform.rotation.eulerAngles.z / 360f;
			this._currentParticleIndex = 0;
			float radius = this._psShape.radius;
			if (this.StartDelay > 0.0001f)
			{
				yield return new WaitForSeconds(this.StartDelay);
			}
			while (this._currentParticleIndex < this._indexRemap.Count)
			{
				float f = (float)this._indexRemap[this._currentParticleIndex] * angleStep * 0.017453292f;
				Vector3 vector = new Vector3(Mathf.Cos(f) * radius, Mathf.Sin(f) * radius, 0f);
				float f2 = (float)this._indexRemap[(this._currentParticleIndex + 1) % this._indexRemap.Count] * angleStep * 0.017453292f;
				Vector3 a = new Vector3(Mathf.Cos(f2) * radius, Mathf.Sin(f2) * radius, 0f);
				ParticleSystem.EmitParams emitParams = default(ParticleSystem.EmitParams);
				emitParams.position = vector;
				emitParams.velocity = (a - vector).normalized * this.Velocity;
				this._ps.Emit(emitParams, 1);
				this._currentParticleIndex++;
				yield return new WaitForSeconds(this.Delay);
			}
			yield break;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x0000610C File Offset: 0x0000430C
		public MagicFX5_StarParticleEmission()
		{
		}

		// Token: 0x0400011A RID: 282
		public float StartDelay;

		// Token: 0x0400011B RID: 283
		public float Delay = 0.25f;

		// Token: 0x0400011C RID: 284
		public float Velocity = 0.01f;

		// Token: 0x0400011D RID: 285
		private ParticleSystem _ps;

		// Token: 0x0400011E RID: 286
		private ParticleSystem.ShapeModule _psShape;

		// Token: 0x0400011F RID: 287
		private int _currentParticleIndex;

		// Token: 0x04000120 RID: 288
		private Dictionary<int, int> _indexRemap = new Dictionary<int, int>
		{
			{
				0,
				0
			},
			{
				2,
				1
			},
			{
				4,
				2
			},
			{
				1,
				3
			},
			{
				3,
				4
			}
		};

		// Token: 0x0200003C RID: 60
		[CompilerGenerated]
		private sealed class <Emit>d__10 : IEnumerator<object>, IEnumerator, IDisposable
		{
			// Token: 0x060000F6 RID: 246 RVA: 0x000078AA File Offset: 0x00005AAA
			[DebuggerHidden]
			public <Emit>d__10(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x060000F7 RID: 247 RVA: 0x000078B9 File Offset: 0x00005AB9
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x060000F8 RID: 248 RVA: 0x000078BC File Offset: 0x00005ABC
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				MagicFX5_StarParticleEmission magicFX5_StarParticleEmission = this;
				switch (num)
				{
				case 0:
					this.<>1__state = -1;
					angleStep = 360f / (float)magicFX5_StarParticleEmission._indexRemap.Count;
					angleStep += magicFX5_StarParticleEmission.transform.rotation.eulerAngles.z / 360f;
					magicFX5_StarParticleEmission._currentParticleIndex = 0;
					radius = magicFX5_StarParticleEmission._psShape.radius;
					if (magicFX5_StarParticleEmission.StartDelay > 0.0001f)
					{
						this.<>2__current = new WaitForSeconds(magicFX5_StarParticleEmission.StartDelay);
						this.<>1__state = 1;
						return true;
					}
					break;
				case 1:
					this.<>1__state = -1;
					break;
				case 2:
					this.<>1__state = -1;
					break;
				default:
					return false;
				}
				if (magicFX5_StarParticleEmission._currentParticleIndex >= magicFX5_StarParticleEmission._indexRemap.Count)
				{
					return false;
				}
				float f = (float)magicFX5_StarParticleEmission._indexRemap[magicFX5_StarParticleEmission._currentParticleIndex] * angleStep * 0.017453292f;
				Vector3 vector = new Vector3(Mathf.Cos(f) * radius, Mathf.Sin(f) * radius, 0f);
				float f2 = (float)magicFX5_StarParticleEmission._indexRemap[(magicFX5_StarParticleEmission._currentParticleIndex + 1) % magicFX5_StarParticleEmission._indexRemap.Count] * angleStep * 0.017453292f;
				Vector3 a = new Vector3(Mathf.Cos(f2) * radius, Mathf.Sin(f2) * radius, 0f);
				ParticleSystem.EmitParams emitParams = default(ParticleSystem.EmitParams);
				emitParams.position = vector;
				emitParams.velocity = (a - vector).normalized * magicFX5_StarParticleEmission.Velocity;
				magicFX5_StarParticleEmission._ps.Emit(emitParams, 1);
				magicFX5_StarParticleEmission._currentParticleIndex++;
				this.<>2__current = new WaitForSeconds(magicFX5_StarParticleEmission.Delay);
				this.<>1__state = 2;
				return true;
			}

			// Token: 0x1700000B RID: 11
			// (get) Token: 0x060000F9 RID: 249 RVA: 0x00007AA9 File Offset: 0x00005CA9
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060000FA RID: 250 RVA: 0x00007AB1 File Offset: 0x00005CB1
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x1700000C RID: 12
			// (get) Token: 0x060000FB RID: 251 RVA: 0x00007AB8 File Offset: 0x00005CB8
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0400019D RID: 413
			private int <>1__state;

			// Token: 0x0400019E RID: 414
			private object <>2__current;

			// Token: 0x0400019F RID: 415
			public MagicFX5_StarParticleEmission <>4__this;

			// Token: 0x040001A0 RID: 416
			private float <angleStep>5__2;

			// Token: 0x040001A1 RID: 417
			private float <radius>5__3;
		}
	}
}
