using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x02000030 RID: 48
public class Symphony_Harp : MonoBehaviour
{
	// Token: 0x06000166 RID: 358 RVA: 0x0000E59A File Offset: 0x0000C79A
	public void Play()
	{
		base.StopAllCoroutines();
		base.StartCoroutine(this.PlaySequence());
	}

	// Token: 0x06000167 RID: 359 RVA: 0x0000E5AF File Offset: 0x0000C7AF
	private IEnumerator PlaySequence()
	{
		float t = 0f;
		float beatDuration = 60f / this.BPM;
		float[] rhythmicValuesInBeats = new float[]
		{
			0.5f,
			1f,
			1f,
			2f
		};
		switch (UnityEngine.Random.Range(0, 3))
		{
		case 0:
		{
			List<int> list = this.majorTriadIntervals.ToList<int>();
			break;
		}
		case 1:
		{
			List<int> list = this.minorTriadIntervals.ToList<int>();
			break;
		}
		case 2:
		{
			List<int> list = this.dominant7Intervals.ToList<int>();
			break;
		}
		default:
		{
			List<int> list = this.majorTriadIntervals.ToList<int>();
			break;
		}
		}
		while (t < this.totalDuration)
		{
			int num = UnityEngine.Random.Range(0, this.scaleIndices.Length);
			int item = this.scaleIndices[num];
			int num2 = UnityEngine.Random.Range(this.minChordSize, this.maxChordSize + 1);
			num2 = Mathf.Min(num2, this.majorTriadIntervals.Length);
			List<int> list2 = new List<int>();
			list2.Add(item);
			for (int i = 1; i < num2; i++)
			{
				int num3 = this.majorTriadIntervals[i];
				int num4 = (num + num3) % this.scaleIndices.Length;
				int item2 = this.scaleIndices[num4];
				list2.Add(item2);
			}
			foreach (int num5 in list2)
			{
				AudioManager.PlayClipAtPoint(this.noteClips[num5], base.transform.position, 1f, 1f, 1f, 10f, 250f);
			}
			float num6 = rhythmicValuesInBeats[UnityEngine.Random.Range(0, rhythmicValuesInBeats.Length)] * beatDuration;
			if (t + num6 > this.totalDuration)
			{
				break;
			}
			t += num6;
			yield return new WaitForSeconds(num6);
		}
		yield break;
	}

	// Token: 0x06000168 RID: 360 RVA: 0x0000E5C0 File Offset: 0x0000C7C0
	public Symphony_Harp()
	{
	}

	// Token: 0x04000193 RID: 403
	public AudioClip[] noteClips;

	// Token: 0x04000194 RID: 404
	private int[] scaleIndices = new int[]
	{
		0,
		1,
		2,
		3,
		4,
		5,
		6,
		7
	};

	// Token: 0x04000195 RID: 405
	private int[] majorTriadIntervals = new int[]
	{
		0,
		2,
		4
	};

	// Token: 0x04000196 RID: 406
	private int[] minorTriadIntervals = new int[]
	{
		0,
		2,
		3
	};

	// Token: 0x04000197 RID: 407
	private int[] dominant7Intervals = new int[]
	{
		0,
		2,
		4,
		6
	};

	// Token: 0x04000198 RID: 408
	public float minWait = 0.2f;

	// Token: 0x04000199 RID: 409
	public float maxWait = 1f;

	// Token: 0x0400019A RID: 410
	public int minChordSize = 1;

	// Token: 0x0400019B RID: 411
	public int maxChordSize = 3;

	// Token: 0x0400019C RID: 412
	public float totalDuration = 3f;

	// Token: 0x0400019D RID: 413
	public float BPM = 200f;

	// Token: 0x020003FB RID: 1019
	[CompilerGenerated]
	private sealed class <PlaySequence>d__12 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06002092 RID: 8338 RVA: 0x000C095B File Offset: 0x000BEB5B
		[DebuggerHidden]
		public <PlaySequence>d__12(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06002093 RID: 8339 RVA: 0x000C096A File Offset: 0x000BEB6A
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06002094 RID: 8340 RVA: 0x000C096C File Offset: 0x000BEB6C
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			Symphony_Harp symphony_Harp = this;
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
				t = 0f;
				beatDuration = 60f / symphony_Harp.BPM;
				rhythmicValuesInBeats = new float[]
				{
					0.5f,
					1f,
					1f,
					2f
				};
				switch (UnityEngine.Random.Range(0, 3))
				{
				case 0:
				{
					List<int> list = symphony_Harp.majorTriadIntervals.ToList<int>();
					break;
				}
				case 1:
				{
					List<int> list = symphony_Harp.minorTriadIntervals.ToList<int>();
					break;
				}
				case 2:
				{
					List<int> list = symphony_Harp.dominant7Intervals.ToList<int>();
					break;
				}
				default:
				{
					List<int> list = symphony_Harp.majorTriadIntervals.ToList<int>();
					break;
				}
				}
			}
			if (t < symphony_Harp.totalDuration)
			{
				int num2 = UnityEngine.Random.Range(0, symphony_Harp.scaleIndices.Length);
				int item = symphony_Harp.scaleIndices[num2];
				int num3 = UnityEngine.Random.Range(symphony_Harp.minChordSize, symphony_Harp.maxChordSize + 1);
				num3 = Mathf.Min(num3, symphony_Harp.majorTriadIntervals.Length);
				List<int> list2 = new List<int>();
				list2.Add(item);
				for (int i = 1; i < num3; i++)
				{
					int num4 = symphony_Harp.majorTriadIntervals[i];
					int num5 = (num2 + num4) % symphony_Harp.scaleIndices.Length;
					int item2 = symphony_Harp.scaleIndices[num5];
					list2.Add(item2);
				}
				foreach (int num6 in list2)
				{
					AudioManager.PlayClipAtPoint(symphony_Harp.noteClips[num6], symphony_Harp.transform.position, 1f, 1f, 1f, 10f, 250f);
				}
				float num7 = rhythmicValuesInBeats[UnityEngine.Random.Range(0, rhythmicValuesInBeats.Length)] * beatDuration;
				if (t + num7 <= symphony_Harp.totalDuration)
				{
					t += num7;
					this.<>2__current = new WaitForSeconds(num7);
					this.<>1__state = 1;
					return true;
				}
			}
			return false;
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06002095 RID: 8341 RVA: 0x000C0B9C File Offset: 0x000BED9C
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06002096 RID: 8342 RVA: 0x000C0BA4 File Offset: 0x000BEDA4
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06002097 RID: 8343 RVA: 0x000C0BAB File Offset: 0x000BEDAB
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0400211C RID: 8476
		private int <>1__state;

		// Token: 0x0400211D RID: 8477
		private object <>2__current;

		// Token: 0x0400211E RID: 8478
		public Symphony_Harp <>4__this;

		// Token: 0x0400211F RID: 8479
		private float <t>5__2;

		// Token: 0x04002120 RID: 8480
		private float <beatDuration>5__3;

		// Token: 0x04002121 RID: 8481
		private float[] <rhythmicValuesInBeats>5__4;
	}
}
