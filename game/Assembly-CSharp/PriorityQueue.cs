using System;
using System.Collections.Generic;

// Token: 0x020000FA RID: 250
public class PriorityQueue<T>
{
	// Token: 0x1700010B RID: 267
	// (get) Token: 0x06000BDE RID: 3038 RVA: 0x0004CF4D File Offset: 0x0004B14D
	public int Count
	{
		get
		{
			return this.elements.Count;
		}
	}

	// Token: 0x06000BDF RID: 3039 RVA: 0x0004CF5A File Offset: 0x0004B15A
	public void Enqueue(T item, float priority)
	{
		this.elements.Add(new KeyValuePair<T, float>(item, priority));
	}

	// Token: 0x06000BE0 RID: 3040 RVA: 0x0004CF70 File Offset: 0x0004B170
	public T Dequeue()
	{
		int index = 0;
		for (int i = 0; i < this.elements.Count; i++)
		{
			if (this.elements[i].Value < this.elements[index].Value)
			{
				index = i;
			}
		}
		T key = this.elements[index].Key;
		this.elements.RemoveAt(index);
		return key;
	}

	// Token: 0x06000BE1 RID: 3041 RVA: 0x0004CFE1 File Offset: 0x0004B1E1
	public PriorityQueue()
	{
	}

	// Token: 0x040009A5 RID: 2469
	private List<KeyValuePair<T, float>> elements = new List<KeyValuePair<T, float>>();
}
