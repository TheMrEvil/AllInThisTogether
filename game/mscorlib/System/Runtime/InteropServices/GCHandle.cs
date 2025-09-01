using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Runtime.InteropServices
{
	/// <summary>Provides a way to access a managed object from unmanaged memory.</summary>
	// Token: 0x0200073F RID: 1855
	[ComVisible(true)]
	public struct GCHandle
	{
		// Token: 0x06004122 RID: 16674 RVA: 0x000E1DDB File Offset: 0x000DFFDB
		private GCHandle(IntPtr h)
		{
			this.handle = h;
		}

		// Token: 0x06004123 RID: 16675 RVA: 0x000E1DE4 File Offset: 0x000DFFE4
		private GCHandle(object obj)
		{
			this = new GCHandle(obj, GCHandleType.Normal);
		}

		// Token: 0x06004124 RID: 16676 RVA: 0x000E1DEE File Offset: 0x000DFFEE
		internal GCHandle(object value, GCHandleType type)
		{
			if (type < GCHandleType.Weak || type > GCHandleType.Pinned)
			{
				type = GCHandleType.Normal;
			}
			this.handle = GCHandle.GetTargetHandle(value, IntPtr.Zero, type);
		}

		/// <summary>Gets a value indicating whether the handle is allocated.</summary>
		/// <returns>
		///   <see langword="true" /> if the handle is allocated; otherwise, <see langword="false" />.</returns>
		// Token: 0x170009F9 RID: 2553
		// (get) Token: 0x06004125 RID: 16677 RVA: 0x000E1E0D File Offset: 0x000E000D
		public bool IsAllocated
		{
			get
			{
				return this.handle != IntPtr.Zero;
			}
		}

		/// <summary>Gets or sets the object this handle represents.</summary>
		/// <returns>The object this handle represents.</returns>
		/// <exception cref="T:System.InvalidOperationException">The handle was freed, or never initialized.</exception>
		// Token: 0x170009FA RID: 2554
		// (get) Token: 0x06004126 RID: 16678 RVA: 0x000E1E1F File Offset: 0x000E001F
		// (set) Token: 0x06004127 RID: 16679 RVA: 0x000E1E3F File Offset: 0x000E003F
		public object Target
		{
			get
			{
				if (!this.IsAllocated)
				{
					throw new InvalidOperationException("Handle is not allocated");
				}
				return GCHandle.GetTarget(this.handle);
			}
			set
			{
				this.handle = GCHandle.GetTargetHandle(value, this.handle, (GCHandleType)(-1));
			}
		}

		/// <summary>Retrieves the address of an object in a <see cref="F:System.Runtime.InteropServices.GCHandleType.Pinned" /> handle.</summary>
		/// <returns>The address of the pinned object as an <see cref="T:System.IntPtr" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The handle is any type other than <see cref="F:System.Runtime.InteropServices.GCHandleType.Pinned" />.</exception>
		// Token: 0x06004128 RID: 16680 RVA: 0x000E1E54 File Offset: 0x000E0054
		public IntPtr AddrOfPinnedObject()
		{
			IntPtr addrOfPinnedObject = GCHandle.GetAddrOfPinnedObject(this.handle);
			if (addrOfPinnedObject == (IntPtr)(-1))
			{
				throw new ArgumentException("Object contains non-primitive or non-blittable data.");
			}
			if (addrOfPinnedObject == (IntPtr)(-2))
			{
				throw new InvalidOperationException("Handle is not pinned.");
			}
			return addrOfPinnedObject;
		}

		/// <summary>Allocates a <see cref="F:System.Runtime.InteropServices.GCHandleType.Normal" /> handle for the specified object.</summary>
		/// <param name="value">The object that uses the <see cref="T:System.Runtime.InteropServices.GCHandle" />.</param>
		/// <returns>A new <see cref="T:System.Runtime.InteropServices.GCHandle" /> that protects the object from garbage collection. This <see cref="T:System.Runtime.InteropServices.GCHandle" /> must be released with <see cref="M:System.Runtime.InteropServices.GCHandle.Free" /> when it is no longer needed.</returns>
		/// <exception cref="T:System.ArgumentException">An instance with nonprimitive (non-blittable) members cannot be pinned.</exception>
		// Token: 0x06004129 RID: 16681 RVA: 0x000E1E94 File Offset: 0x000E0094
		public static GCHandle Alloc(object value)
		{
			return new GCHandle(value);
		}

		/// <summary>Allocates a handle of the specified type for the specified object.</summary>
		/// <param name="value">The object that uses the <see cref="T:System.Runtime.InteropServices.GCHandle" />.</param>
		/// <param name="type">One of the <see cref="T:System.Runtime.InteropServices.GCHandleType" /> values, indicating the type of <see cref="T:System.Runtime.InteropServices.GCHandle" /> to create.</param>
		/// <returns>A new <see cref="T:System.Runtime.InteropServices.GCHandle" /> of the specified type. This <see cref="T:System.Runtime.InteropServices.GCHandle" /> must be released with <see cref="M:System.Runtime.InteropServices.GCHandle.Free" /> when it is no longer needed.</returns>
		/// <exception cref="T:System.ArgumentException">An instance with nonprimitive (non-blittable) members cannot be pinned.</exception>
		// Token: 0x0600412A RID: 16682 RVA: 0x000E1E9C File Offset: 0x000E009C
		public static GCHandle Alloc(object value, GCHandleType type)
		{
			return new GCHandle(value, type);
		}

		/// <summary>Releases a <see cref="T:System.Runtime.InteropServices.GCHandle" />.</summary>
		/// <exception cref="T:System.InvalidOperationException">The handle was freed or never initialized.</exception>
		// Token: 0x0600412B RID: 16683 RVA: 0x000E1EA8 File Offset: 0x000E00A8
		public void Free()
		{
			IntPtr intPtr = this.handle;
			if (intPtr != IntPtr.Zero && Interlocked.CompareExchange(ref this.handle, IntPtr.Zero, intPtr) == intPtr)
			{
				GCHandle.FreeHandle(intPtr);
				return;
			}
			throw new InvalidOperationException("Handle is not initialized.");
		}

		/// <summary>A <see cref="T:System.Runtime.InteropServices.GCHandle" /> is stored using an internal integer representation.</summary>
		/// <param name="value">The <see cref="T:System.Runtime.InteropServices.GCHandle" /> for which the integer is required.</param>
		/// <returns>The integer value.</returns>
		// Token: 0x0600412C RID: 16684 RVA: 0x000E1EF3 File Offset: 0x000E00F3
		public static explicit operator IntPtr(GCHandle value)
		{
			return value.handle;
		}

		/// <summary>A <see cref="T:System.Runtime.InteropServices.GCHandle" /> is stored using an internal integer representation.</summary>
		/// <param name="value">An <see cref="T:System.IntPtr" /> that indicates the handle for which the conversion is required.</param>
		/// <returns>The stored <see cref="T:System.Runtime.InteropServices.GCHandle" /> object using an internal integer representation.</returns>
		// Token: 0x0600412D RID: 16685 RVA: 0x000E1EFB File Offset: 0x000E00FB
		public static explicit operator GCHandle(IntPtr value)
		{
			if (value == IntPtr.Zero)
			{
				throw new InvalidOperationException("GCHandle value cannot be zero");
			}
			if (!GCHandle.CheckCurrentDomain(value))
			{
				throw new ArgumentException("GCHandle value belongs to a different domain");
			}
			return new GCHandle(value);
		}

		// Token: 0x0600412E RID: 16686
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CheckCurrentDomain(IntPtr handle);

		// Token: 0x0600412F RID: 16687
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern object GetTarget(IntPtr handle);

		// Token: 0x06004130 RID: 16688
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetTargetHandle(object obj, IntPtr handle, GCHandleType type);

		// Token: 0x06004131 RID: 16689
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void FreeHandle(IntPtr handle);

		// Token: 0x06004132 RID: 16690
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetAddrOfPinnedObject(IntPtr handle);

		/// <summary>Returns a value indicating whether two <see cref="T:System.Runtime.InteropServices.GCHandle" /> objects are equal.</summary>
		/// <param name="a">A <see cref="T:System.Runtime.InteropServices.GCHandle" /> object to compare with the <paramref name="b" /> parameter.</param>
		/// <param name="b">A <see cref="T:System.Runtime.InteropServices.GCHandle" /> object to compare with the <paramref name="a" /> parameter.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="a" /> and <paramref name="b" /> parameters are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004133 RID: 16691 RVA: 0x000E1F2E File Offset: 0x000E012E
		public static bool operator ==(GCHandle a, GCHandle b)
		{
			return a.handle == b.handle;
		}

		/// <summary>Returns a value indicating whether two <see cref="T:System.Runtime.InteropServices.GCHandle" /> objects are not equal.</summary>
		/// <param name="a">A <see cref="T:System.Runtime.InteropServices.GCHandle" /> object to compare with the <paramref name="b" /> parameter.</param>
		/// <param name="b">A <see cref="T:System.Runtime.InteropServices.GCHandle" /> object to compare with the <paramref name="a" /> parameter.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="a" /> and <paramref name="b" /> parameters are not equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004134 RID: 16692 RVA: 0x000E1F41 File Offset: 0x000E0141
		public static bool operator !=(GCHandle a, GCHandle b)
		{
			return !(a == b);
		}

		/// <summary>Determines whether the specified <see cref="T:System.Runtime.InteropServices.GCHandle" /> object is equal to the current <see cref="T:System.Runtime.InteropServices.GCHandle" /> object.</summary>
		/// <param name="o">The <see cref="T:System.Runtime.InteropServices.GCHandle" /> object to compare with the current <see cref="T:System.Runtime.InteropServices.GCHandle" /> object.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.Runtime.InteropServices.GCHandle" /> object is equal to the current <see cref="T:System.Runtime.InteropServices.GCHandle" /> object; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004135 RID: 16693 RVA: 0x000E1F4D File Offset: 0x000E014D
		public override bool Equals(object o)
		{
			return o is GCHandle && this == (GCHandle)o;
		}

		/// <summary>Returns an identifier for the current <see cref="T:System.Runtime.InteropServices.GCHandle" /> object.</summary>
		/// <returns>An identifier for the current <see cref="T:System.Runtime.InteropServices.GCHandle" /> object.</returns>
		// Token: 0x06004136 RID: 16694 RVA: 0x000E1F6A File Offset: 0x000E016A
		public override int GetHashCode()
		{
			return this.handle.GetHashCode();
		}

		/// <summary>Returns a new <see cref="T:System.Runtime.InteropServices.GCHandle" /> object created from a handle to a managed object.</summary>
		/// <param name="value">An <see cref="T:System.IntPtr" /> handle to a managed object to create a <see cref="T:System.Runtime.InteropServices.GCHandle" /> object from.</param>
		/// <returns>A new <see cref="T:System.Runtime.InteropServices.GCHandle" /> object that corresponds to the value parameter.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <paramref name="value" /> parameter is <see cref="F:System.IntPtr.Zero" />.</exception>
		// Token: 0x06004137 RID: 16695 RVA: 0x000E1F77 File Offset: 0x000E0177
		public static GCHandle FromIntPtr(IntPtr value)
		{
			return (GCHandle)value;
		}

		/// <summary>Returns the internal integer representation of a <see cref="T:System.Runtime.InteropServices.GCHandle" /> object.</summary>
		/// <param name="value">A <see cref="T:System.Runtime.InteropServices.GCHandle" /> object to retrieve an internal integer representation from.</param>
		/// <returns>An <see cref="T:System.IntPtr" /> object that represents a <see cref="T:System.Runtime.InteropServices.GCHandle" /> object.</returns>
		// Token: 0x06004138 RID: 16696 RVA: 0x000E1F7F File Offset: 0x000E017F
		public static IntPtr ToIntPtr(GCHandle value)
		{
			return (IntPtr)value;
		}

		// Token: 0x04002BC1 RID: 11201
		private IntPtr handle;
	}
}
