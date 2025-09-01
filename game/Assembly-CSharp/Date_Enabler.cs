using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x0200021F RID: 543
public class Date_Enabler : MonoBehaviour
{
	// Token: 0x060016BE RID: 5822 RVA: 0x00090AE4 File Offset: 0x0008ECE4
	private IEnumerator Start()
	{
		UnityEngine.Debug.Log("Date Enabler Started");
		AIControl component = base.GetComponent<AIControl>();
		if (component != null)
		{
			yield return new WaitForSeconds((component.Level == EnemyLevel.Boss) ? 1.5f : 1f);
		}
		this.DoActivation();
		yield break;
	}

	// Token: 0x060016BF RID: 5823 RVA: 0x00090AF4 File Offset: 0x0008ECF4
	private void DoActivation()
	{
		foreach (Date_Enabler.DateRef dateRef in this.References)
		{
			if (CosmeticDB.EventIsActive(dateRef.Event) && (float)UnityEngine.Random.Range(0, 100) < dateRef.Chance)
			{
				using (List<GameObject>.Enumerator enumerator2 = dateRef.ToEnable.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						GameObject gameObject = enumerator2.Current;
						gameObject.SetActive(true);
					}
					continue;
				}
			}
			for (int i = 0; i < dateRef.ToEnable.Count; i++)
			{
				UnityEngine.Object.Destroy(dateRef.ToEnable[i]);
			}
		}
	}

	// Token: 0x060016C0 RID: 5824 RVA: 0x00090BCC File Offset: 0x0008EDCC
	public Date_Enabler()
	{
	}

	// Token: 0x040016D8 RID: 5848
	public List<Date_Enabler.DateRef> References;

	// Token: 0x020005FC RID: 1532
	[Serializable]
	public class DateRef
	{
		// Token: 0x060026B3 RID: 9907 RVA: 0x000D3FE8 File Offset: 0x000D21E8
		public DateRef()
		{
		}

		// Token: 0x04002960 RID: 10592
		public DateEvent Event;

		// Token: 0x04002961 RID: 10593
		[Range(0f, 100f)]
		public float Chance = 100f;

		// Token: 0x04002962 RID: 10594
		public List<GameObject> ToEnable;
	}

	// Token: 0x020005FD RID: 1533
	[CompilerGenerated]
	private sealed class <Start>d__1 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x060026B4 RID: 9908 RVA: 0x000D3FFB File Offset: 0x000D21FB
		[DebuggerHidden]
		public <Start>d__1(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x060026B5 RID: 9909 RVA: 0x000D400A File Offset: 0x000D220A
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x060026B6 RID: 9910 RVA: 0x000D400C File Offset: 0x000D220C
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			Date_Enabler date_Enabler = this;
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
				UnityEngine.Debug.Log("Date Enabler Started");
				AIControl component = date_Enabler.GetComponent<AIControl>();
				if (component != null)
				{
					this.<>2__current = new WaitForSeconds((component.Level == EnemyLevel.Boss) ? 1.5f : 1f);
					this.<>1__state = 1;
					return true;
				}
			}
			date_Enabler.DoActivation();
			return false;
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x060026B7 RID: 9911 RVA: 0x000D4089 File Offset: 0x000D2289
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x060026B8 RID: 9912 RVA: 0x000D4091 File Offset: 0x000D2291
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x060026B9 RID: 9913 RVA: 0x000D4098 File Offset: 0x000D2298
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04002963 RID: 10595
		private int <>1__state;

		// Token: 0x04002964 RID: 10596
		private object <>2__current;

		// Token: 0x04002965 RID: 10597
		public Date_Enabler <>4__this;
	}
}
