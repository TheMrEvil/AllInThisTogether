using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Jobs;

namespace MagicaCloth2
{
	// Token: 0x020000EB RID: 235
	public class ExTransformAccessArray : IDisposable
	{
		// Token: 0x06000437 RID: 1079 RVA: 0x00021E44 File Offset: 0x00020044
		public ExTransformAccessArray(int capacity, int desiredJobCount = -1)
		{
			this.transformArray = new TransformAccessArray(capacity, desiredJobCount);
			this.nativeLength = this.transformArray.length;
			this.emptyStack = new Queue<int>(capacity);
			this.useIndexDict = new Dictionary<int, int>(capacity);
			this.indexDict = new Dictionary<int, int>(capacity);
			this.referenceDict = new Dictionary<int, int>(capacity);
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x00021EA8 File Offset: 0x000200A8
		public void Dispose()
		{
			if (this.transformArray.isCreated)
			{
				this.transformArray.Dispose();
			}
			this.emptyStack.Clear();
			this.useIndexDict.Clear();
			this.indexDict.Clear();
			this.referenceDict.Clear();
			this.nativeLength = 0;
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x00021F00 File Offset: 0x00020100
		public TransformAccessArray GetTransformAccessArray()
		{
			return this.transformArray;
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x00021F08 File Offset: 0x00020108
		public int Add(Transform element)
		{
			int instanceID = element.GetInstanceID();
			if (this.referenceDict.ContainsKey(instanceID))
			{
				this.referenceDict[instanceID] = this.referenceDict[instanceID] + 1;
				return this.indexDict[instanceID];
			}
			int num;
			if (this.emptyStack.Count > 0)
			{
				num = this.emptyStack.Dequeue();
				this.transformArray[num] = element;
			}
			else
			{
				num = this.transformArray.length;
				this.transformArray.Add(element);
			}
			this.useIndexDict.Add(num, instanceID);
			this.indexDict.Add(instanceID, num);
			this.referenceDict.Add(instanceID, 1);
			this.nativeLength = this.transformArray.length;
			return num;
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x00021FD0 File Offset: 0x000201D0
		public void Remove(int index)
		{
			if (this.useIndexDict.ContainsKey(index))
			{
				int key = this.useIndexDict[index];
				int num = this.referenceDict[key] - 1;
				if (num > 0)
				{
					this.referenceDict[key] = num;
					return;
				}
				this.transformArray[index] = null;
				this.emptyStack.Enqueue(index);
				this.useIndexDict.Remove(index);
				this.indexDict.Remove(key);
				this.referenceDict.Remove(key);
				this.nativeLength = this.transformArray.length;
			}
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x0002206A File Offset: 0x0002026A
		public bool Exist(int index)
		{
			return this.useIndexDict.ContainsKey(index);
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x00022078 File Offset: 0x00020278
		public bool Exist(Transform element)
		{
			return !(element == null) && this.indexDict.ContainsKey(element.GetInstanceID());
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600043E RID: 1086 RVA: 0x00022096 File Offset: 0x00020296
		public int Count
		{
			get
			{
				return this.useIndexDict.Count;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600043F RID: 1087 RVA: 0x000220A3 File Offset: 0x000202A3
		public int Length
		{
			get
			{
				return this.nativeLength;
			}
		}

		// Token: 0x1700006E RID: 110
		public Transform this[int index]
		{
			get
			{
				return this.transformArray[index];
			}
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x000220BC File Offset: 0x000202BC
		public int GetIndex(Transform element)
		{
			if (element == null)
			{
				return -1;
			}
			int instanceID = element.GetInstanceID();
			if (this.indexDict.ContainsKey(instanceID))
			{
				return this.indexDict[instanceID];
			}
			return -1;
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x000220F8 File Offset: 0x000202F8
		public void Clear()
		{
			foreach (int item in this.useIndexDict.Keys)
			{
				this.emptyStack.Enqueue(item);
			}
			this.useIndexDict.Clear();
			int i = 0;
			int length = this.Length;
			while (i < length)
			{
				this.transformArray[i] = null;
				i++;
			}
			this.indexDict.Clear();
			this.referenceDict.Clear();
			this.nativeLength = 0;
		}

		// Token: 0x04000644 RID: 1604
		private TransformAccessArray transformArray;

		// Token: 0x04000645 RID: 1605
		private int nativeLength;

		// Token: 0x04000646 RID: 1606
		private Queue<int> emptyStack;

		// Token: 0x04000647 RID: 1607
		private Dictionary<int, int> useIndexDict;

		// Token: 0x04000648 RID: 1608
		private Dictionary<int, int> indexDict;

		// Token: 0x04000649 RID: 1609
		private Dictionary<int, int> referenceDict;
	}
}
