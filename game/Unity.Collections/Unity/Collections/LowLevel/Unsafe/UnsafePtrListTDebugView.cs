using System;
using System.Runtime.CompilerServices;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x0200012D RID: 301
	internal sealed class UnsafePtrListTDebugView<[IsUnmanaged] T> where T : struct, ValueType
	{
		// Token: 0x06000B14 RID: 2836 RVA: 0x000209EC File Offset: 0x0001EBEC
		public UnsafePtrListTDebugView(UnsafePtrList<T> data)
		{
			this.Data = data;
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000B15 RID: 2837 RVA: 0x000209FC File Offset: 0x0001EBFC
		public unsafe T*[] Items
		{
			get
			{
				T*[] array = new T*[this.Data.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = *(IntPtr*)(this.Data.Ptr + (IntPtr)i * (IntPtr)sizeof(T*) / (IntPtr)sizeof(T*));
				}
				return array;
			}
		}

		// Token: 0x04000397 RID: 919
		private UnsafePtrList<T> Data;
	}
}
