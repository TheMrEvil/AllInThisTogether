using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Represents a variable-length argument list; that is, the parameters of a function that takes a variable number of arguments.</summary>
	// Token: 0x02000229 RID: 553
	[StructLayout(LayoutKind.Auto)]
	public struct ArgIterator
	{
		// Token: 0x060018CC RID: 6348
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Setup(IntPtr argsp, IntPtr start);

		/// <summary>Initializes a new instance of the <see cref="T:System.ArgIterator" /> structure using the specified argument list.</summary>
		/// <param name="arglist">An argument list consisting of mandatory and optional arguments.</param>
		// Token: 0x060018CD RID: 6349 RVA: 0x0005E048 File Offset: 0x0005C248
		public ArgIterator(RuntimeArgumentHandle arglist)
		{
			this.sig = IntPtr.Zero;
			this.args = IntPtr.Zero;
			this.next_arg = (this.num_args = 0);
			if (arglist.args == IntPtr.Zero)
			{
				throw new PlatformNotSupportedException();
			}
			this.Setup(arglist.args, IntPtr.Zero);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArgIterator" /> structure using the specified argument list and a pointer to an item in the list.</summary>
		/// <param name="arglist">An argument list consisting of mandatory and optional arguments.</param>
		/// <param name="ptr">A pointer to the argument in <paramref name="arglist" /> to access first, or the first mandatory argument in <paramref name="arglist" /> if <paramref name="ptr" /> is <see langword="null" />.</param>
		// Token: 0x060018CE RID: 6350 RVA: 0x0005E0A8 File Offset: 0x0005C2A8
		[CLSCompliant(false)]
		public unsafe ArgIterator(RuntimeArgumentHandle arglist, void* ptr)
		{
			this.sig = IntPtr.Zero;
			this.args = IntPtr.Zero;
			this.next_arg = (this.num_args = 0);
			if (arglist.args == IntPtr.Zero)
			{
				throw new PlatformNotSupportedException();
			}
			this.Setup(arglist.args, (IntPtr)ptr);
		}

		/// <summary>Concludes processing of the variable-length argument list represented by this instance.</summary>
		// Token: 0x060018CF RID: 6351 RVA: 0x0005E107 File Offset: 0x0005C307
		public void End()
		{
			this.next_arg = this.num_args;
		}

		/// <summary>This method is not supported, and always throws <see cref="T:System.NotSupportedException" />.</summary>
		/// <param name="o">An object to be compared to this instance.</param>
		/// <returns>This comparison is not supported. No value is returned.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not supported.</exception>
		// Token: 0x060018D0 RID: 6352 RVA: 0x0005E115 File Offset: 0x0005C315
		public override bool Equals(object o)
		{
			throw new NotSupportedException("ArgIterator does not support Equals.");
		}

		/// <summary>Returns the hash code of this object.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x060018D1 RID: 6353 RVA: 0x0005E121 File Offset: 0x0005C321
		public override int GetHashCode()
		{
			return this.sig.GetHashCode();
		}

		/// <summary>Returns the next argument in a variable-length argument list.</summary>
		/// <returns>The next argument as a <see cref="T:System.TypedReference" /> object.</returns>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read beyond the end of the list.</exception>
		// Token: 0x060018D2 RID: 6354 RVA: 0x0005E130 File Offset: 0x0005C330
		[CLSCompliant(false)]
		public unsafe TypedReference GetNextArg()
		{
			if (this.num_args == this.next_arg)
			{
				throw new InvalidOperationException("Invalid iterator position.");
			}
			TypedReference result = default(TypedReference);
			this.IntGetNextArg((void*)(&result));
			return result;
		}

		// Token: 0x060018D3 RID: 6355
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe extern void IntGetNextArg(void* res);

		/// <summary>Returns the next argument in a variable-length argument list that has a specified type.</summary>
		/// <param name="rth">A runtime type handle that identifies the type of the argument to retrieve.</param>
		/// <returns>The next argument as a <see cref="T:System.TypedReference" /> object.</returns>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read beyond the end of the list.</exception>
		/// <exception cref="T:System.ArgumentNullException">The pointer to the remaining arguments is zero.</exception>
		// Token: 0x060018D4 RID: 6356 RVA: 0x0005E168 File Offset: 0x0005C368
		[CLSCompliant(false)]
		public unsafe TypedReference GetNextArg(RuntimeTypeHandle rth)
		{
			if (this.num_args == this.next_arg)
			{
				throw new InvalidOperationException("Invalid iterator position.");
			}
			TypedReference result = default(TypedReference);
			this.IntGetNextArgWithType((void*)(&result), rth.Value);
			return result;
		}

		// Token: 0x060018D5 RID: 6357
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe extern void IntGetNextArgWithType(void* res, IntPtr rth);

		/// <summary>Returns the type of the next argument.</summary>
		/// <returns>The type of the next argument.</returns>
		// Token: 0x060018D6 RID: 6358 RVA: 0x0005E1A7 File Offset: 0x0005C3A7
		public RuntimeTypeHandle GetNextArgType()
		{
			if (this.num_args == this.next_arg)
			{
				throw new InvalidOperationException("Invalid iterator position.");
			}
			return new RuntimeTypeHandle(this.IntGetNextArgType());
		}

		// Token: 0x060018D7 RID: 6359
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern IntPtr IntGetNextArgType();

		/// <summary>Returns the number of arguments remaining in the argument list.</summary>
		/// <returns>The number of remaining arguments.</returns>
		// Token: 0x060018D8 RID: 6360 RVA: 0x0005E1CD File Offset: 0x0005C3CD
		public int GetRemainingCount()
		{
			return this.num_args - this.next_arg;
		}

		// Token: 0x040016D7 RID: 5847
		private IntPtr sig;

		// Token: 0x040016D8 RID: 5848
		private IntPtr args;

		// Token: 0x040016D9 RID: 5849
		private int next_arg;

		// Token: 0x040016DA RID: 5850
		private int num_args;
	}
}
