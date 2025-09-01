using System;
using System.Collections.Generic;

namespace System.Runtime.Serialization
{
	// Token: 0x020000EF RID: 239
	internal struct ObjectReferenceStack
	{
		// Token: 0x06000DB9 RID: 3513 RVA: 0x000365AC File Offset: 0x000347AC
		internal void Push(object obj)
		{
			if (this.objectArray == null)
			{
				this.objectArray = new object[4];
				object[] array = this.objectArray;
				int num = this.count;
				this.count = num + 1;
				array[num] = obj;
				return;
			}
			if (this.count < 16)
			{
				if (this.count == this.objectArray.Length)
				{
					Array.Resize<object>(ref this.objectArray, this.objectArray.Length * 2);
				}
				object[] array2 = this.objectArray;
				int num = this.count;
				this.count = num + 1;
				array2[num] = obj;
				return;
			}
			if (this.objectDictionary == null)
			{
				this.objectDictionary = new Dictionary<object, object>();
			}
			this.objectDictionary.Add(obj, null);
			this.count++;
		}

		// Token: 0x06000DBA RID: 3514 RVA: 0x00036660 File Offset: 0x00034860
		internal void EnsureSetAsIsReference(object obj)
		{
			if (this.count == 0)
			{
				return;
			}
			if (this.count > 16)
			{
				Dictionary<object, object> dictionary = this.objectDictionary;
				this.objectDictionary.Remove(obj);
				return;
			}
			if (this.objectArray != null && this.objectArray[this.count - 1] == obj)
			{
				if (this.isReferenceArray == null)
				{
					this.isReferenceArray = new bool[4];
				}
				else if (this.count == this.isReferenceArray.Length)
				{
					Array.Resize<bool>(ref this.isReferenceArray, this.isReferenceArray.Length * 2);
				}
				this.isReferenceArray[this.count - 1] = true;
			}
		}

		// Token: 0x06000DBB RID: 3515 RVA: 0x000366FA File Offset: 0x000348FA
		internal void Pop(object obj)
		{
			if (this.count > 16)
			{
				Dictionary<object, object> dictionary = this.objectDictionary;
				this.objectDictionary.Remove(obj);
			}
			this.count--;
		}

		// Token: 0x06000DBC RID: 3516 RVA: 0x00036728 File Offset: 0x00034928
		internal bool Contains(object obj)
		{
			int num = this.count;
			if (num > 16)
			{
				if (this.objectDictionary != null && this.objectDictionary.ContainsKey(obj))
				{
					return true;
				}
				num = 16;
			}
			for (int i = num - 1; i >= 0; i--)
			{
				if (obj == this.objectArray[i] && this.isReferenceArray != null && !this.isReferenceArray[i])
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000DBD RID: 3517 RVA: 0x0003678A File Offset: 0x0003498A
		internal int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x04000650 RID: 1616
		private const int MaximumArraySize = 16;

		// Token: 0x04000651 RID: 1617
		private const int InitialArraySize = 4;

		// Token: 0x04000652 RID: 1618
		private int count;

		// Token: 0x04000653 RID: 1619
		private object[] objectArray;

		// Token: 0x04000654 RID: 1620
		private bool[] isReferenceArray;

		// Token: 0x04000655 RID: 1621
		private Dictionary<object, object> objectDictionary;
	}
}
