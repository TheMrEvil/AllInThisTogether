using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace MimicSpace
{
	// Token: 0x020003DC RID: 988
	public class Mimic : MonoBehaviour
	{
		// Token: 0x06002029 RID: 8233 RVA: 0x000BF4AE File Offset: 0x000BD6AE
		private void Start()
		{
			this.ResetMimic();
		}

		// Token: 0x0600202A RID: 8234 RVA: 0x000BF4B6 File Offset: 0x000BD6B6
		private void OnValidate()
		{
			this.ResetMimic();
		}

		// Token: 0x0600202B RID: 8235 RVA: 0x000BF4C0 File Offset: 0x000BD6C0
		private void ResetMimic()
		{
			Leg[] array = UnityEngine.Object.FindObjectsOfType<Leg>();
			for (int i = 0; i < array.Length; i++)
			{
				UnityEngine.Object.Destroy(array[i].gameObject);
			}
			this.legCount = 0;
			this.deployedLegs = 0;
			this.maxLegs = this.numberOfLegs * this.partsPerLeg;
			float num = 360f / (float)this.maxLegs;
			Vector2 insideUnitCircle = UnityEngine.Random.insideUnitCircle;
			this.velocity = new Vector3(insideUnitCircle.x, 0f, insideUnitCircle.y);
			this.minimumAnchoredParts = this.minimumAnchoredLegs * this.partsPerLeg;
			this.maxLegDistance = this.newLegRadius * 2.1f;
		}

		// Token: 0x0600202C RID: 8236 RVA: 0x000BF564 File Offset: 0x000BD764
		private IEnumerator NewLegCooldown()
		{
			this.canCreateLeg = false;
			yield return new WaitForSeconds(this.newLegCooldown);
			this.canCreateLeg = true;
			yield break;
		}

		// Token: 0x0600202D RID: 8237 RVA: 0x000BF574 File Offset: 0x000BD774
		private void Update()
		{
			if (!this.canCreateLeg)
			{
				return;
			}
			this.legPlacerOrigin = base.transform.position + this.velocity.normalized * this.newLegRadius;
			if (this.legCount <= this.maxLegs - this.partsPerLeg)
			{
				Vector2 vector = UnityEngine.Random.insideUnitCircle * this.newLegRadius;
				Vector3 a = this.legPlacerOrigin + new Vector3(vector.x, 0f, vector.y);
				if (this.velocity.magnitude > 1f && Mathf.Abs(Vector3.Angle(this.velocity, a - base.transform.position)) > 90f)
				{
					a = base.transform.position - (a - base.transform.position);
				}
				if (Vector3.Distance(new Vector3(base.transform.position.x, 0f, base.transform.position.z), new Vector3(this.legPlacerOrigin.x, 0f, this.legPlacerOrigin.z)) < this.minLegDistance)
				{
					a = (a - base.transform.position).normalized * this.minLegDistance + base.transform.position;
				}
				if (Vector3.Angle(this.velocity, a - base.transform.position) > 45f)
				{
					a = base.transform.position + (a - base.transform.position + this.velocity.normalized * (a - base.transform.position).magnitude) / 2f;
				}
				RaycastHit raycastHit;
				Physics.Raycast(a + Vector3.up * 10f, -Vector3.up, out raycastHit, 25f, this.LegLayerMask);
				Vector3 point = raycastHit.point;
				if (Physics.Linecast(base.transform.position, raycastHit.point, out raycastHit, this.LegLayerMask))
				{
					point = raycastHit.point;
				}
				float lifeTime = UnityEngine.Random.Range(this.minLegLifetime, this.maxLegLifetime);
				base.StartCoroutine("NewLegCooldown");
				for (int i = 0; i < this.partsPerLeg; i++)
				{
					this.RequestLeg(point, this.legResolution, this.maxLegDistance, UnityEngine.Random.Range(this.minGrowCoef, this.maxGrowCoef), this, lifeTime);
					if (this.legCount >= this.maxLegs)
					{
						return;
					}
				}
			}
		}

		// Token: 0x0600202E RID: 8238 RVA: 0x000BF840 File Offset: 0x000BDA40
		private void RequestLeg(Vector3 footPosition, int legResolution, float maxLegDistance, float growCoef, Mimic myMimic, float lifeTime)
		{
			GameObject gameObject;
			if (this.availableLegPool.Count > 0)
			{
				gameObject = this.availableLegPool[this.availableLegPool.Count - 1];
				this.availableLegPool.RemoveAt(this.availableLegPool.Count - 1);
			}
			else
			{
				gameObject = UnityEngine.Object.Instantiate<GameObject>(this.legPrefab, base.transform.position, Quaternion.identity);
			}
			gameObject.SetActive(true);
			gameObject.GetComponent<Leg>().Initialize(footPosition, legResolution, maxLegDistance, growCoef, myMimic, lifeTime);
			gameObject.transform.SetParent(myMimic.transform);
		}

		// Token: 0x0600202F RID: 8239 RVA: 0x000BF8D7 File Offset: 0x000BDAD7
		public void RecycleLeg(GameObject leg)
		{
			this.availableLegPool.Add(leg);
			leg.SetActive(false);
		}

		// Token: 0x06002030 RID: 8240 RVA: 0x000BF8EC File Offset: 0x000BDAEC
		public Mimic()
		{
		}

		// Token: 0x0400208B RID: 8331
		[Header("Animation")]
		public GameObject legPrefab;

		// Token: 0x0400208C RID: 8332
		public LayerMask LegLayerMask;

		// Token: 0x0400208D RID: 8333
		[Range(2f, 20f)]
		public int numberOfLegs = 5;

		// Token: 0x0400208E RID: 8334
		[Tooltip("The number of splines per leg")]
		[Range(1f, 10f)]
		public int partsPerLeg = 4;

		// Token: 0x0400208F RID: 8335
		private int maxLegs;

		// Token: 0x04002090 RID: 8336
		public int legCount;

		// Token: 0x04002091 RID: 8337
		public int deployedLegs;

		// Token: 0x04002092 RID: 8338
		[Range(0f, 19f)]
		public int minimumAnchoredLegs = 2;

		// Token: 0x04002093 RID: 8339
		public int minimumAnchoredParts;

		// Token: 0x04002094 RID: 8340
		[Tooltip("Minimum duration before leg is replaced")]
		public float minLegLifetime = 5f;

		// Token: 0x04002095 RID: 8341
		[Tooltip("Maximum duration before leg is replaced")]
		public float maxLegLifetime = 15f;

		// Token: 0x04002096 RID: 8342
		public Vector3 legPlacerOrigin = Vector3.zero;

		// Token: 0x04002097 RID: 8343
		[Tooltip("Leg placement radius offset")]
		public float newLegRadius = 3f;

		// Token: 0x04002098 RID: 8344
		public float minLegDistance = 4.5f;

		// Token: 0x04002099 RID: 8345
		public float maxLegDistance = 6.3f;

		// Token: 0x0400209A RID: 8346
		[Range(2f, 50f)]
		[Tooltip("Number of spline samples per legpart")]
		public int legResolution = 40;

		// Token: 0x0400209B RID: 8347
		[Tooltip("Minimum lerp coeficient for leg growth smoothing")]
		public float minGrowCoef = 4.5f;

		// Token: 0x0400209C RID: 8348
		[Tooltip("MAximum lerp coeficient for leg growth smoothing")]
		public float maxGrowCoef = 6.5f;

		// Token: 0x0400209D RID: 8349
		[Tooltip("Minimum duration before a new leg can be placed")]
		public float newLegCooldown = 0.3f;

		// Token: 0x0400209E RID: 8350
		private bool canCreateLeg = true;

		// Token: 0x0400209F RID: 8351
		private List<GameObject> availableLegPool = new List<GameObject>();

		// Token: 0x040020A0 RID: 8352
		[Tooltip("This must be updates as the Mimin moves to assure great leg placement")]
		public Vector3 velocity;

		// Token: 0x020006A9 RID: 1705
		[CompilerGenerated]
		private sealed class <NewLegCooldown>d__25 : IEnumerator<object>, IEnumerator, IDisposable
		{
			// Token: 0x06002851 RID: 10321 RVA: 0x000D8677 File Offset: 0x000D6877
			[DebuggerHidden]
			public <NewLegCooldown>d__25(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x06002852 RID: 10322 RVA: 0x000D8686 File Offset: 0x000D6886
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06002853 RID: 10323 RVA: 0x000D8688 File Offset: 0x000D6888
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				Mimic mimic = this;
				if (num == 0)
				{
					this.<>1__state = -1;
					mimic.canCreateLeg = false;
					this.<>2__current = new WaitForSeconds(mimic.newLegCooldown);
					this.<>1__state = 1;
					return true;
				}
				if (num != 1)
				{
					return false;
				}
				this.<>1__state = -1;
				mimic.canCreateLeg = true;
				return false;
			}

			// Token: 0x170003DB RID: 987
			// (get) Token: 0x06002854 RID: 10324 RVA: 0x000D86E3 File Offset: 0x000D68E3
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06002855 RID: 10325 RVA: 0x000D86EB File Offset: 0x000D68EB
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170003DC RID: 988
			// (get) Token: 0x06002856 RID: 10326 RVA: 0x000D86F2 File Offset: 0x000D68F2
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x04002C87 RID: 11399
			private int <>1__state;

			// Token: 0x04002C88 RID: 11400
			private object <>2__current;

			// Token: 0x04002C89 RID: 11401
			public Mimic <>4__this;
		}
	}
}
