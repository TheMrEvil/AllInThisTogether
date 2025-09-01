using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace MimicSpace
{
	// Token: 0x020003DB RID: 987
	public class Leg : MonoBehaviour
	{
		// Token: 0x06002020 RID: 8224 RVA: 0x000BEBF0 File Offset: 0x000BCDF0
		public void Initialize(Vector3 footPosition, int legResolution, float maxLegDistance, float growCoef, Mimic myMimic, float lifeTime)
		{
			this.myColor = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f));
			this.footPosition = footPosition;
			this.legResolution = legResolution;
			this.maxLegDistance = maxLegDistance;
			this.growCoef = growCoef;
			this.myMimic = myMimic;
			this.legLine = base.GetComponent<LineRenderer>();
			this.handles = new Vector3[this.handlesCount];
			this.handleOffsets = new Vector3[6];
			this.handleOffsets[0] = UnityEngine.Random.onUnitSphere * UnityEngine.Random.Range(this.handleOffsetMinRadius, this.handleOffsetMaxRadius);
			this.handleOffsets[1] = UnityEngine.Random.onUnitSphere * UnityEngine.Random.Range(this.handleOffsetMinRadius, this.handleOffsetMaxRadius);
			this.handleOffsets[2] = UnityEngine.Random.onUnitSphere * UnityEngine.Random.Range(this.handleOffsetMinRadius, this.handleOffsetMaxRadius);
			this.handleOffsets[3] = UnityEngine.Random.onUnitSphere * UnityEngine.Random.Range(this.handleOffsetMinRadius, this.handleOffsetMaxRadius);
			this.handleOffsets[4] = UnityEngine.Random.onUnitSphere * UnityEngine.Random.Range(this.handleOffsetMinRadius, this.handleOffsetMaxRadius);
			this.handleOffsets[5] = UnityEngine.Random.onUnitSphere * UnityEngine.Random.Range(this.handleOffsetMinRadius, this.handleOffsetMaxRadius);
			Vector2 vector = UnityEngine.Random.insideUnitCircle.normalized * this.finalFootDistance;
			RaycastHit raycastHit;
			Physics.Raycast(footPosition + Vector3.up * 5f + new Vector3(vector.x, 0f, vector.y), -Vector3.up, out raycastHit, 25f, myMimic.LegLayerMask);
			this.handles[7] = raycastHit.point;
			this.legHeight = UnityEngine.Random.Range(this.legMinHeight, this.legMaxHeight);
			this.rotationSpeed = UnityEngine.Random.Range(this.minRotSpeed, this.maxRotSpeed);
			this.rotationSign = 1f;
			this.oscillationSpeed = UnityEngine.Random.Range(this.minOscillationSpeed, this.maxOscillationSpeed);
			this.oscillationProgress = 0f;
			myMimic.legCount++;
			this.growTarget = 1f;
			this.isRemoved = false;
			this.canDie = false;
			this.isDeployed = false;
			base.StartCoroutine("WaitToDie");
			base.StartCoroutine("WaitAndDie", lifeTime);
			this.Sethandles();
		}

		// Token: 0x06002021 RID: 8225 RVA: 0x000BEE9B File Offset: 0x000BD09B
		private IEnumerator WaitToDie()
		{
			yield return new WaitForSeconds(this.minDuration);
			this.canDie = true;
			yield break;
		}

		// Token: 0x06002022 RID: 8226 RVA: 0x000BEEAA File Offset: 0x000BD0AA
		private IEnumerator WaitAndDie(float lifeTime)
		{
			yield return new WaitForSeconds(lifeTime);
			while (this.myMimic.deployedLegs < this.myMimic.minimumAnchoredParts)
			{
				yield return null;
			}
			this.growTarget = 0f;
			yield break;
		}

		// Token: 0x06002023 RID: 8227 RVA: 0x000BEEC0 File Offset: 0x000BD0C0
		private void Update()
		{
			RaycastHit raycastHit;
			if (this.growTarget == 1f && Vector3.Distance(new Vector3(this.myMimic.legPlacerOrigin.x, 0f, this.myMimic.legPlacerOrigin.z), new Vector3(this.footPosition.x, 0f, this.footPosition.z)) > this.maxLegDistance && this.canDie && this.myMimic.deployedLegs > this.myMimic.minimumAnchoredParts)
			{
				this.growTarget = 0f;
			}
			else if (this.growTarget == 1f && Physics.Linecast(this.footPosition, base.transform.position, out raycastHit, this.myMimic.LegLayerMask))
			{
				this.growTarget = 0f;
			}
			this.progression = Mathf.Lerp(this.progression, this.growTarget, this.growCoef * Time.deltaTime);
			if (!this.isDeployed && this.progression > 0.9f)
			{
				this.myMimic.deployedLegs++;
				this.isDeployed = true;
			}
			else if (this.isDeployed && this.progression < 0.9f)
			{
				this.myMimic.deployedLegs--;
				this.isDeployed = false;
			}
			if (this.progression < 0.5f && this.growTarget == 0f)
			{
				if (!this.isRemoved)
				{
					base.GetComponentInParent<Mimic>().legCount--;
					this.isRemoved = true;
				}
				if (this.progression < 0.05f)
				{
					this.legLine.positionCount = 0;
					this.myMimic.RecycleLeg(base.gameObject);
					return;
				}
			}
			this.Sethandles();
			Vector3[] samplePoints = this.GetSamplePoints((Vector3[])this.handles.Clone(), this.legResolution, this.progression);
			this.legLine.positionCount = samplePoints.Length;
			this.legLine.SetPositions(samplePoints);
		}

		// Token: 0x06002024 RID: 8228 RVA: 0x000BF0D0 File Offset: 0x000BD2D0
		private void Sethandles()
		{
			this.handles[0] = base.transform.position;
			this.handles[6] = this.footPosition + Vector3.up * 0.05f;
			this.handles[2] = Vector3.Lerp(this.handles[0], this.handles[6], 0.4f);
			this.handles[2].y = this.handles[0].y + this.legHeight;
			this.handles[1] = Vector3.Lerp(this.handles[0], this.handles[2], 0.5f);
			this.handles[3] = Vector3.Lerp(this.handles[2], this.handles[6], 0.25f);
			this.handles[4] = Vector3.Lerp(this.handles[2], this.handles[6], 0.5f);
			this.handles[5] = Vector3.Lerp(this.handles[2], this.handles[6], 0.75f);
			this.RotateHandleOffset();
			this.handles[1] += this.handleOffsets[0];
			this.handles[2] += this.handleOffsets[1];
			this.handles[3] += this.handleOffsets[2];
			this.handles[4] += this.handleOffsets[3] / 2f;
			this.handles[5] += this.handleOffsets[4] / 4f;
		}

		// Token: 0x06002025 RID: 8229 RVA: 0x000BF30C File Offset: 0x000BD50C
		private void RotateHandleOffset()
		{
			this.oscillationProgress += Time.deltaTime * this.oscillationSpeed;
			if (this.oscillationProgress >= 360f)
			{
				this.oscillationProgress -= 360f;
			}
			float angle = this.rotationSpeed * Time.deltaTime * Mathf.Cos(this.oscillationProgress * 0.017453292f) + 1f;
			for (int i = 1; i < 6; i++)
			{
				Vector3 a = (this.handles[i + 1] - this.handles[i - 1]) / 2f;
				this.handleOffsets[i - 1] = Quaternion.AngleAxis(angle, this.rotationSign * a) * this.handleOffsets[i - 1];
			}
		}

		// Token: 0x06002026 RID: 8230 RVA: 0x000BF3E4 File Offset: 0x000BD5E4
		private Vector3[] GetSamplePoints(Vector3[] curveHandles, int resolution, float t)
		{
			List<Vector3> list = new List<Vector3>();
			float num = 1f / (float)resolution;
			for (float num2 = 0f; num2 <= t; num2 += num)
			{
				list.Add(this.GetPointOnCurve((Vector3[])curveHandles.Clone(), num2));
			}
			list.Add(this.GetPointOnCurve(curveHandles, t));
			return list.ToArray();
		}

		// Token: 0x06002027 RID: 8231 RVA: 0x000BF43C File Offset: 0x000BD63C
		private Vector3 GetPointOnCurve(Vector3[] curveHandles, float t)
		{
			for (int i = curveHandles.Length; i > 1; i--)
			{
				for (int j = 0; j < i - 1; j++)
				{
					curveHandles[j] = Vector3.Lerp(curveHandles[j], curveHandles[j + 1], t);
				}
			}
			return curveHandles[0];
		}

		// Token: 0x06002028 RID: 8232 RVA: 0x000BF489 File Offset: 0x000BD689
		public Leg()
		{
		}

		// Token: 0x0400206D RID: 8301
		private Mimic myMimic;

		// Token: 0x0400206E RID: 8302
		public bool isDeployed;

		// Token: 0x0400206F RID: 8303
		public Vector3 footPosition;

		// Token: 0x04002070 RID: 8304
		public float maxLegDistance;

		// Token: 0x04002071 RID: 8305
		public int legResolution;

		// Token: 0x04002072 RID: 8306
		public LineRenderer legLine;

		// Token: 0x04002073 RID: 8307
		public int handlesCount = 8;

		// Token: 0x04002074 RID: 8308
		public float legMinHeight;

		// Token: 0x04002075 RID: 8309
		public float legMaxHeight;

		// Token: 0x04002076 RID: 8310
		private float legHeight;

		// Token: 0x04002077 RID: 8311
		public Vector3[] handles;

		// Token: 0x04002078 RID: 8312
		public float handleOffsetMinRadius;

		// Token: 0x04002079 RID: 8313
		public float handleOffsetMaxRadius;

		// Token: 0x0400207A RID: 8314
		public Vector3[] handleOffsets;

		// Token: 0x0400207B RID: 8315
		public float finalFootDistance;

		// Token: 0x0400207C RID: 8316
		public float growCoef;

		// Token: 0x0400207D RID: 8317
		public float growTarget = 1f;

		// Token: 0x0400207E RID: 8318
		[Range(0f, 1f)]
		public float progression;

		// Token: 0x0400207F RID: 8319
		private bool isRemoved;

		// Token: 0x04002080 RID: 8320
		private bool canDie;

		// Token: 0x04002081 RID: 8321
		public float minDuration;

		// Token: 0x04002082 RID: 8322
		[Header("Rotation")]
		public float rotationSpeed;

		// Token: 0x04002083 RID: 8323
		public float minRotSpeed;

		// Token: 0x04002084 RID: 8324
		public float maxRotSpeed;

		// Token: 0x04002085 RID: 8325
		private float rotationSign = 1f;

		// Token: 0x04002086 RID: 8326
		public float oscillationSpeed;

		// Token: 0x04002087 RID: 8327
		public float minOscillationSpeed;

		// Token: 0x04002088 RID: 8328
		public float maxOscillationSpeed;

		// Token: 0x04002089 RID: 8329
		private float oscillationProgress;

		// Token: 0x0400208A RID: 8330
		public Color myColor;

		// Token: 0x020006A7 RID: 1703
		[CompilerGenerated]
		private sealed class <WaitToDie>d__31 : IEnumerator<object>, IEnumerator, IDisposable
		{
			// Token: 0x06002845 RID: 10309 RVA: 0x000D853D File Offset: 0x000D673D
			[DebuggerHidden]
			public <WaitToDie>d__31(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x06002846 RID: 10310 RVA: 0x000D854C File Offset: 0x000D674C
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06002847 RID: 10311 RVA: 0x000D8550 File Offset: 0x000D6750
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				Leg leg = this;
				if (num == 0)
				{
					this.<>1__state = -1;
					this.<>2__current = new WaitForSeconds(leg.minDuration);
					this.<>1__state = 1;
					return true;
				}
				if (num != 1)
				{
					return false;
				}
				this.<>1__state = -1;
				leg.canDie = true;
				return false;
			}

			// Token: 0x170003D7 RID: 983
			// (get) Token: 0x06002848 RID: 10312 RVA: 0x000D85A4 File Offset: 0x000D67A4
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06002849 RID: 10313 RVA: 0x000D85AC File Offset: 0x000D67AC
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170003D8 RID: 984
			// (get) Token: 0x0600284A RID: 10314 RVA: 0x000D85B3 File Offset: 0x000D67B3
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x04002C80 RID: 11392
			private int <>1__state;

			// Token: 0x04002C81 RID: 11393
			private object <>2__current;

			// Token: 0x04002C82 RID: 11394
			public Leg <>4__this;
		}

		// Token: 0x020006A8 RID: 1704
		[CompilerGenerated]
		private sealed class <WaitAndDie>d__32 : IEnumerator<object>, IEnumerator, IDisposable
		{
			// Token: 0x0600284B RID: 10315 RVA: 0x000D85BB File Offset: 0x000D67BB
			[DebuggerHidden]
			public <WaitAndDie>d__32(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x0600284C RID: 10316 RVA: 0x000D85CA File Offset: 0x000D67CA
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x0600284D RID: 10317 RVA: 0x000D85CC File Offset: 0x000D67CC
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				Leg leg = this;
				switch (num)
				{
				case 0:
					this.<>1__state = -1;
					this.<>2__current = new WaitForSeconds(lifeTime);
					this.<>1__state = 1;
					return true;
				case 1:
					this.<>1__state = -1;
					break;
				case 2:
					this.<>1__state = -1;
					break;
				default:
					return false;
				}
				if (leg.myMimic.deployedLegs >= leg.myMimic.minimumAnchoredParts)
				{
					leg.growTarget = 0f;
					return false;
				}
				this.<>2__current = null;
				this.<>1__state = 2;
				return true;
			}

			// Token: 0x170003D9 RID: 985
			// (get) Token: 0x0600284E RID: 10318 RVA: 0x000D8660 File Offset: 0x000D6860
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600284F RID: 10319 RVA: 0x000D8668 File Offset: 0x000D6868
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170003DA RID: 986
			// (get) Token: 0x06002850 RID: 10320 RVA: 0x000D866F File Offset: 0x000D686F
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x04002C83 RID: 11395
			private int <>1__state;

			// Token: 0x04002C84 RID: 11396
			private object <>2__current;

			// Token: 0x04002C85 RID: 11397
			public float lifeTime;

			// Token: 0x04002C86 RID: 11398
			public Leg <>4__this;
		}
	}
}
