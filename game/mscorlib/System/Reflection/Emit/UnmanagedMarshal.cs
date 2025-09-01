using System;
using System.Runtime.InteropServices;
using Unity;

namespace System.Reflection.Emit
{
	/// <summary>Represents the class that describes how to marshal a field from managed to unmanaged code. This class cannot be inherited.</summary>
	// Token: 0x0200094A RID: 2378
	[ComVisible(true)]
	[Obsolete("An alternate API is available: Emit the MarshalAs custom attribute instead.")]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class UnmanagedMarshal
	{
		// Token: 0x0600535B RID: 21339 RVA: 0x00105C82 File Offset: 0x00103E82
		private UnmanagedMarshal(UnmanagedType maint, int cnt)
		{
			this.count = cnt;
			this.t = maint;
			this.tbase = maint;
		}

		// Token: 0x0600535C RID: 21340 RVA: 0x00105C9F File Offset: 0x00103E9F
		private UnmanagedMarshal(UnmanagedType maint, UnmanagedType elemt)
		{
			this.count = 0;
			this.t = maint;
			this.tbase = elemt;
		}

		/// <summary>Gets an unmanaged base type. This property is read-only.</summary>
		/// <returns>An <see langword="UnmanagedType" /> object.</returns>
		/// <exception cref="T:System.ArgumentException">The unmanaged type is not an <see langword="LPArray" /> or a <see langword="SafeArray" />.</exception>
		// Token: 0x17000DD3 RID: 3539
		// (get) Token: 0x0600535D RID: 21341 RVA: 0x00105CBC File Offset: 0x00103EBC
		public UnmanagedType BaseType
		{
			get
			{
				if (this.t == UnmanagedType.LPArray)
				{
					throw new ArgumentException();
				}
				if (this.t == UnmanagedType.SafeArray)
				{
					throw new ArgumentException();
				}
				return this.tbase;
			}
		}

		/// <summary>Gets a number element. This property is read-only.</summary>
		/// <returns>An integer indicating the element count.</returns>
		/// <exception cref="T:System.ArgumentException">The argument is not an unmanaged element count.</exception>
		// Token: 0x17000DD4 RID: 3540
		// (get) Token: 0x0600535E RID: 21342 RVA: 0x00105CE4 File Offset: 0x00103EE4
		public int ElementCount
		{
			get
			{
				return this.count;
			}
		}

		/// <summary>Indicates an unmanaged type. This property is read-only.</summary>
		/// <returns>An <see cref="T:System.Runtime.InteropServices.UnmanagedType" /> object.</returns>
		// Token: 0x17000DD5 RID: 3541
		// (get) Token: 0x0600535F RID: 21343 RVA: 0x00105CEC File Offset: 0x00103EEC
		public UnmanagedType GetUnmanagedType
		{
			get
			{
				return this.t;
			}
		}

		/// <summary>Gets a GUID. This property is read-only.</summary>
		/// <returns>A <see cref="T:System.Guid" /> object.</returns>
		/// <exception cref="T:System.ArgumentException">The argument is not a custom marshaler.</exception>
		// Token: 0x17000DD6 RID: 3542
		// (get) Token: 0x06005360 RID: 21344 RVA: 0x00105CF4 File Offset: 0x00103EF4
		public Guid IIDGuid
		{
			get
			{
				return new Guid(this.guid);
			}
		}

		/// <summary>Specifies a fixed-length array (ByValArray) to marshal to unmanaged code.</summary>
		/// <param name="elemCount">The number of elements in the fixed-length array.</param>
		/// <returns>An <see cref="T:System.Reflection.Emit.UnmanagedMarshal" /> object.</returns>
		/// <exception cref="T:System.ArgumentException">The argument is not a simple native type.</exception>
		// Token: 0x06005361 RID: 21345 RVA: 0x00105D01 File Offset: 0x00103F01
		public static UnmanagedMarshal DefineByValArray(int elemCount)
		{
			return new UnmanagedMarshal(UnmanagedType.ByValArray, elemCount);
		}

		/// <summary>Specifies a string in a fixed array buffer (ByValTStr) to marshal to unmanaged code.</summary>
		/// <param name="elemCount">The number of elements in the fixed array buffer.</param>
		/// <returns>An <see cref="T:System.Reflection.Emit.UnmanagedMarshal" /> object.</returns>
		/// <exception cref="T:System.ArgumentException">The argument is not a simple native type.</exception>
		// Token: 0x06005362 RID: 21346 RVA: 0x00105D0B File Offset: 0x00103F0B
		public static UnmanagedMarshal DefineByValTStr(int elemCount)
		{
			return new UnmanagedMarshal(UnmanagedType.ByValTStr, elemCount);
		}

		/// <summary>Specifies an <see langword="LPArray" /> to marshal to unmanaged code. The length of an <see langword="LPArray" /> is determined at runtime by the size of the actual marshaled array.</summary>
		/// <param name="elemType">The unmanaged type to which to marshal the array.</param>
		/// <returns>An <see cref="T:System.Reflection.Emit.UnmanagedMarshal" /> object.</returns>
		/// <exception cref="T:System.ArgumentException">The argument is not a simple native type.</exception>
		// Token: 0x06005363 RID: 21347 RVA: 0x00105D15 File Offset: 0x00103F15
		public static UnmanagedMarshal DefineLPArray(UnmanagedType elemType)
		{
			return new UnmanagedMarshal(UnmanagedType.LPArray, elemType);
		}

		/// <summary>Specifies a <see langword="SafeArray" /> to marshal to unmanaged code.</summary>
		/// <param name="elemType">The base type or the <see langword="UnmanagedType" /> of each element of the array.</param>
		/// <returns>An <see cref="T:System.Reflection.Emit.UnmanagedMarshal" /> object.</returns>
		/// <exception cref="T:System.ArgumentException">The argument is not a simple native type.</exception>
		// Token: 0x06005364 RID: 21348 RVA: 0x00105D1F File Offset: 0x00103F1F
		public static UnmanagedMarshal DefineSafeArray(UnmanagedType elemType)
		{
			return new UnmanagedMarshal(UnmanagedType.SafeArray, elemType);
		}

		/// <summary>Specifies a given type that is to be marshaled to unmanaged code.</summary>
		/// <param name="unmanagedType">The unmanaged type to which the type is to be marshaled.</param>
		/// <returns>An <see cref="T:System.Reflection.Emit.UnmanagedMarshal" /> object.</returns>
		/// <exception cref="T:System.ArgumentException">The argument is not a simple native type.</exception>
		// Token: 0x06005365 RID: 21349 RVA: 0x00105D29 File Offset: 0x00103F29
		public static UnmanagedMarshal DefineUnmanagedMarshal(UnmanagedType unmanagedType)
		{
			return new UnmanagedMarshal(unmanagedType, unmanagedType);
		}

		// Token: 0x06005366 RID: 21350 RVA: 0x00105D34 File Offset: 0x00103F34
		internal static UnmanagedMarshal DefineCustom(Type typeref, string cookie, string mtype, Guid id)
		{
			UnmanagedMarshal unmanagedMarshal = new UnmanagedMarshal(UnmanagedType.CustomMarshaler, UnmanagedType.CustomMarshaler);
			unmanagedMarshal.mcookie = cookie;
			unmanagedMarshal.marshaltype = mtype;
			unmanagedMarshal.marshaltyperef = typeref;
			if (id == Guid.Empty)
			{
				unmanagedMarshal.guid = string.Empty;
			}
			else
			{
				unmanagedMarshal.guid = id.ToString();
			}
			return unmanagedMarshal;
		}

		// Token: 0x06005367 RID: 21351 RVA: 0x00105D8E File Offset: 0x00103F8E
		internal static UnmanagedMarshal DefineLPArrayInternal(UnmanagedType elemType, int sizeConst, int sizeParamIndex)
		{
			return new UnmanagedMarshal(UnmanagedType.LPArray, elemType)
			{
				count = sizeConst,
				param_num = sizeParamIndex,
				has_size = true
			};
		}

		// Token: 0x06005368 RID: 21352 RVA: 0x000173AD File Offset: 0x000155AD
		internal UnmanagedMarshal()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x0400333F RID: 13119
		private int count;

		// Token: 0x04003340 RID: 13120
		private UnmanagedType t;

		// Token: 0x04003341 RID: 13121
		private UnmanagedType tbase;

		// Token: 0x04003342 RID: 13122
		private string guid;

		// Token: 0x04003343 RID: 13123
		private string mcookie;

		// Token: 0x04003344 RID: 13124
		private string marshaltype;

		// Token: 0x04003345 RID: 13125
		internal Type marshaltyperef;

		// Token: 0x04003346 RID: 13126
		private int param_num;

		// Token: 0x04003347 RID: 13127
		private bool has_size;
	}
}
