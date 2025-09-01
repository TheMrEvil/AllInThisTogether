using System;
using System.Runtime.CompilerServices;

namespace System.Buffers
{
	// Token: 0x02000AD1 RID: 2769
	public abstract class ArrayPool<T>
	{
		// Token: 0x1700117D RID: 4477
		// (get) Token: 0x060062CD RID: 25293 RVA: 0x0014A568 File Offset: 0x00148768
		public static ArrayPool<T> Shared
		{
			[CompilerGenerated]
			get
			{
				return ArrayPool<T>.<Shared>k__BackingField;
			}
		} = new TlsOverPerCoreLockedStacksArrayPool<T>();

		// Token: 0x060062CE RID: 25294 RVA: 0x0014A56F File Offset: 0x0014876F
		public static ArrayPool<T> Create()
		{
			return new ConfigurableArrayPool<T>();
		}

		// Token: 0x060062CF RID: 25295 RVA: 0x0014A576 File Offset: 0x00148776
		public static ArrayPool<T> Create(int maxArrayLength, int maxArraysPerBucket)
		{
			return new ConfigurableArrayPool<T>(maxArrayLength, maxArraysPerBucket);
		}

		// Token: 0x060062D0 RID: 25296
		public abstract T[] Rent(int minimumLength);

		// Token: 0x060062D1 RID: 25297
		public abstract void Return(T[] array, bool clearArray = false);

		// Token: 0x060062D2 RID: 25298 RVA: 0x0000259F File Offset: 0x0000079F
		protected ArrayPool()
		{
		}

		// Token: 0x060062D3 RID: 25299 RVA: 0x0014A57F File Offset: 0x0014877F
		// Note: this type is marked as 'beforefieldinit'.
		static ArrayPool()
		{
		}

		// Token: 0x04003A37 RID: 14903
		[CompilerGenerated]
		private static readonly ArrayPool<T> <Shared>k__BackingField;
	}
}
