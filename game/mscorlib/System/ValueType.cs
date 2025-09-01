using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace System
{
	/// <summary>Provides the base class for value types.</summary>
	// Token: 0x02000266 RID: 614
	[ComVisible(true)]
	[Serializable]
	public abstract class ValueType
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ValueType" /> class.</summary>
		// Token: 0x06001C0D RID: 7181 RVA: 0x0000259F File Offset: 0x0000079F
		protected ValueType()
		{
		}

		// Token: 0x06001C0E RID: 7182
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool InternalEquals(object o1, object o2, out object[] fields);

		// Token: 0x06001C0F RID: 7183 RVA: 0x00068AC8 File Offset: 0x00066CC8
		internal static bool DefaultEquals(object o1, object o2)
		{
			if (o1 == null && o2 == null)
			{
				return true;
			}
			if (o1 == null || o2 == null)
			{
				return false;
			}
			RuntimeType left = (RuntimeType)o1.GetType();
			RuntimeType right = (RuntimeType)o2.GetType();
			if (left != right)
			{
				return false;
			}
			object[] array;
			bool result = ValueType.InternalEquals(o1, o2, out array);
			if (array == null)
			{
				return result;
			}
			for (int i = 0; i < array.Length; i += 2)
			{
				object obj = array[i];
				object obj2 = array[i + 1];
				if (obj == null)
				{
					if (obj2 != null)
					{
						return false;
					}
				}
				else if (!obj.Equals(obj2))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Indicates whether this instance and a specified object are equal.</summary>
		/// <param name="obj">The object to compare with the current instance.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> and this instance are the same type and represent the same value; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001C10 RID: 7184 RVA: 0x000558B3 File Offset: 0x00053AB3
		public override bool Equals(object obj)
		{
			return ValueType.DefaultEquals(this, obj);
		}

		// Token: 0x06001C11 RID: 7185
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int InternalGetHashCode(object o, out object[] fields);

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
		// Token: 0x06001C12 RID: 7186 RVA: 0x00068B48 File Offset: 0x00066D48
		public override int GetHashCode()
		{
			object[] array;
			int num = ValueType.InternalGetHashCode(this, out array);
			if (array != null)
			{
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i] != null)
					{
						num ^= array[i].GetHashCode();
					}
				}
			}
			return num;
		}

		// Token: 0x06001C13 RID: 7187 RVA: 0x00068B80 File Offset: 0x00066D80
		internal static int GetHashCodeOfPtr(IntPtr ptr)
		{
			int num = (int)ptr;
			int num2 = ValueType.Internal.hash_code_of_ptr_seed;
			if (num2 == 0)
			{
				num2 = num;
				Interlocked.CompareExchange(ref ValueType.Internal.hash_code_of_ptr_seed, num2, 0);
				num2 = ValueType.Internal.hash_code_of_ptr_seed;
			}
			return num - num2;
		}

		/// <summary>Returns the fully qualified type name of this instance.</summary>
		/// <returns>The fully qualified type name.</returns>
		// Token: 0x06001C14 RID: 7188 RVA: 0x00068BB5 File Offset: 0x00066DB5
		public override string ToString()
		{
			return base.GetType().FullName;
		}

		// Token: 0x02000267 RID: 615
		private static class Internal
		{
			// Token: 0x040019A5 RID: 6565
			public static int hash_code_of_ptr_seed;
		}
	}
}
