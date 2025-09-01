using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001BA RID: 442
public class ManaGroup : MonoBehaviour
{
	// Token: 0x1700014F RID: 335
	// (get) Token: 0x06001233 RID: 4659 RVA: 0x00070CCA File Offset: 0x0006EECA
	public List<ManaPip> Mana
	{
		get
		{
			return this.Pips;
		}
	}

	// Token: 0x06001234 RID: 4660 RVA: 0x00070CD2 File Offset: 0x0006EED2
	public void UpdatePosition(int index)
	{
		this.GroupIndex = index;
	}

	// Token: 0x06001235 RID: 4661 RVA: 0x00070CDC File Offset: 0x0006EEDC
	public void UpdateDisplay(List<Mana> mana)
	{
		int num = -1;
		for (int i = 0; i < Mathf.Min(mana.Count, this.Anchors.Count); i++)
		{
			if (this.Pips.Count <= i)
			{
				this.AddPip();
			}
			ManaPip manaPip = null;
			foreach (ManaPip manaPip2 in this.Pips)
			{
				if (manaPip2.mana == mana[i])
				{
					manaPip = manaPip2;
					break;
				}
			}
			if (manaPip != null)
			{
				this.Pips.Move(manaPip, i);
				manaPip.TickUpdate(this.PipLocation(i, this.Pips.Count));
			}
			else
			{
				this.Pips[i].Setup(mana[i], this.PipLocation(i, this.Pips.Count), this);
			}
			num = i;
		}
		for (int j = num + 1; j < this.Pips.Count; j++)
		{
			UnityEngine.Object.Destroy(this.Pips[j].gameObject);
			this.Pips.RemoveAt(j);
		}
	}

	// Token: 0x06001236 RID: 4662 RVA: 0x00070E1C File Offset: 0x0006F01C
	private void AddPip()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.PipRef, this.PipRef.transform.parent);
		gameObject.SetActive(true);
		this.Pips.Add(gameObject.GetComponent<ManaPip>());
	}

	// Token: 0x06001237 RID: 4663 RVA: 0x00070E5D File Offset: 0x0006F05D
	public void RemovePip(ManaPip pip)
	{
		Debug.Log("Removing pip " + ((pip != null) ? pip.ToString() : null));
		this.Pips.Remove(pip);
		UnityEngine.Object.Destroy(pip.gameObject);
	}

	// Token: 0x06001238 RID: 4664 RVA: 0x00070E94 File Offset: 0x0006F094
	private void Update()
	{
		for (int i = 0; i < this.Pips.Count; i++)
		{
			this.Pips[i].TickUpdate(this.PipLocation(i, this.Pips.Count));
		}
	}

	// Token: 0x06001239 RID: 4665 RVA: 0x00070EDC File Offset: 0x0006F0DC
	private Vector3 PipLocation(int index, int pipCount)
	{
		float num = (float)(134 + 64 * this.GroupIndex);
		float num2 = 50f;
		int num3 = Mathf.FloorToInt(6.2831855f * num / num2);
		float num4 = 6.2831855f / (float)num3 * 57.29578f;
		float num5 = -9f;
		if (pipCount > 4)
		{
			num5 = -16f - (float)(pipCount - 4) / 2f * num4;
		}
		float num6 = num5 + num4 * (float)index;
		Quaternion rotation = Quaternion.Euler(0f, 0f, -num6);
		Vector3 point = Vector3.right * num;
		return rotation * point;
	}

	// Token: 0x0600123A RID: 4666 RVA: 0x00070F70 File Offset: 0x0006F170
	public ManaGroup()
	{
	}

	// Token: 0x04001121 RID: 4385
	public List<RectTransform> Anchors;

	// Token: 0x04001122 RID: 4386
	public GameObject PipRef;

	// Token: 0x04001123 RID: 4387
	private List<ManaPip> Pips = new List<ManaPip>();

	// Token: 0x04001124 RID: 4388
	private int GroupIndex;
}
