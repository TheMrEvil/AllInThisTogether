using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace ExitGames.Client.Photon.StructWrapping
{
	// Token: 0x02000042 RID: 66
	public class StructWrapper<T> : StructWrapper
	{
		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600037B RID: 891 RVA: 0x00019D65 File Offset: 0x00017F65
		// (set) Token: 0x0600037C RID: 892 RVA: 0x00019D6D File Offset: 0x00017F6D
		public StructWrapperPool<T> ReturnPool
		{
			[CompilerGenerated]
			get
			{
				return this.<ReturnPool>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<ReturnPool>k__BackingField = value;
			}
		}

		// Token: 0x0600037D RID: 893 RVA: 0x00019D76 File Offset: 0x00017F76
		public StructWrapper(Pooling releasing) : base(typeof(T), StructWrapperPool.GetWrappedType(typeof(T)))
		{
			this.pooling = releasing;
		}

		// Token: 0x0600037E RID: 894 RVA: 0x00019DA0 File Offset: 0x00017FA0
		public StructWrapper(Pooling releasing, Type tType, WrappedType wType) : base(tType, wType)
		{
			this.pooling = releasing;
		}

		// Token: 0x0600037F RID: 895 RVA: 0x00019DB4 File Offset: 0x00017FB4
		public StructWrapper<T> Poke(byte value)
		{
			bool flag = this.pooling == Pooling.Readonly;
			if (flag)
			{
				throw new InvalidOperationException("Trying to Poke the value of a readonly StructWrapper<byte>. Value cannot be modified.");
			}
			return this;
		}

		// Token: 0x06000380 RID: 896 RVA: 0x00019DE0 File Offset: 0x00017FE0
		public StructWrapper<T> Poke(bool value)
		{
			bool flag = this.pooling == Pooling.Readonly;
			if (flag)
			{
				throw new InvalidOperationException("Trying to Poke the value of a readonly StructWrapper<bool>. Value cannot be modified.");
			}
			return this;
		}

		// Token: 0x06000381 RID: 897 RVA: 0x00019E0C File Offset: 0x0001800C
		public StructWrapper<T> Poke(T value)
		{
			this.value = value;
			return this;
		}

		// Token: 0x06000382 RID: 898 RVA: 0x00019E28 File Offset: 0x00018028
		public T Unwrap()
		{
			T result = this.value;
			bool flag = this.pooling != Pooling.Readonly;
			if (flag)
			{
				this.ReturnPool.Release(this);
			}
			return result;
		}

		// Token: 0x06000383 RID: 899 RVA: 0x00019E60 File Offset: 0x00018060
		public T Peek()
		{
			return this.value;
		}

		// Token: 0x06000384 RID: 900 RVA: 0x00019E7C File Offset: 0x0001807C
		public override object Box()
		{
			T t = this.value;
			bool flag = this.ReturnPool != null;
			if (flag)
			{
				this.ReturnPool.Release(this);
			}
			return t;
		}

		// Token: 0x06000385 RID: 901 RVA: 0x00019EB8 File Offset: 0x000180B8
		public override void Dispose()
		{
			bool flag = (this.pooling & Pooling.CheckedOut) == Pooling.CheckedOut;
			if (flag)
			{
				bool flag2 = this.ReturnPool != null;
				if (flag2)
				{
					this.ReturnPool.Release(this);
				}
			}
		}

		// Token: 0x06000386 RID: 902 RVA: 0x00019EF4 File Offset: 0x000180F4
		public override void DisconnectFromPool()
		{
			bool flag = this.pooling != Pooling.Readonly;
			if (flag)
			{
				this.pooling = Pooling.Disconnected;
				this.ReturnPool = null;
			}
		}

		// Token: 0x06000387 RID: 903 RVA: 0x00019F24 File Offset: 0x00018124
		public override string ToString()
		{
			T t = this.Unwrap();
			return t.ToString();
		}

		// Token: 0x06000388 RID: 904 RVA: 0x00019F4C File Offset: 0x0001814C
		public override string ToString(bool writeTypeInfo)
		{
			string result;
			if (writeTypeInfo)
			{
				string format = "(StructWrapper<{0}>){1}";
				object arg = this.wrappedType;
				T t = this.Unwrap();
				result = string.Format(format, arg, t.ToString());
			}
			else
			{
				T t = this.Unwrap();
				result = t.ToString();
			}
			return result;
		}

		// Token: 0x06000389 RID: 905 RVA: 0x00019FA8 File Offset: 0x000181A8
		public static implicit operator StructWrapper<T>(T value)
		{
			return StructWrapper<T>.staticPool.Acquire(value);
		}

		// Token: 0x0600038A RID: 906 RVA: 0x00019FC7 File Offset: 0x000181C7
		// Note: this type is marked as 'beforefieldinit'.
		static StructWrapper()
		{
		}

		// Token: 0x040001ED RID: 493
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private StructWrapperPool<T> <ReturnPool>k__BackingField;

		// Token: 0x040001EE RID: 494
		internal Pooling pooling;

		// Token: 0x040001EF RID: 495
		internal T value;

		// Token: 0x040001F0 RID: 496
		internal static StructWrapperPool<T> staticPool = new StructWrapperPool<T>(true);
	}
}
