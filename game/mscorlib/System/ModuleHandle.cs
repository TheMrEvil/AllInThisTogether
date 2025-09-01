using System;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Represents a runtime handle for a module.</summary>
	// Token: 0x0200023C RID: 572
	[ComVisible(true)]
	public struct ModuleHandle
	{
		// Token: 0x06001A1E RID: 6686 RVA: 0x0005FF8C File Offset: 0x0005E18C
		internal ModuleHandle(IntPtr v)
		{
			this.value = v;
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06001A1F RID: 6687 RVA: 0x0005FF95 File Offset: 0x0005E195
		internal IntPtr Value
		{
			get
			{
				return this.value;
			}
		}

		/// <summary>Gets the metadata stream version.</summary>
		/// <returns>A 32-bit integer representing the metadata stream version. The high-order two bytes represent the major version number, and the low-order two bytes represent the minor version number.</returns>
		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06001A20 RID: 6688 RVA: 0x0005FF9D File Offset: 0x0005E19D
		public int MDStreamVersion
		{
			get
			{
				if (this.value == IntPtr.Zero)
				{
					throw new ArgumentNullException(string.Empty, "Invalid handle");
				}
				return RuntimeModule.GetMDStreamVersion(this.value);
			}
		}

		// Token: 0x06001A21 RID: 6689 RVA: 0x0005FFCC File Offset: 0x0005E1CC
		internal void GetPEKind(out PortableExecutableKinds peKind, out ImageFileMachine machine)
		{
			if (this.value == IntPtr.Zero)
			{
				throw new ArgumentNullException(string.Empty, "Invalid handle");
			}
			RuntimeModule.GetPEKind(this.value, out peKind, out machine);
		}

		/// <summary>Returns a runtime handle for the field identified by the specified metadata token.</summary>
		/// <param name="fieldToken">A metadata token that identifies a field in the module.</param>
		/// <returns>A <see cref="T:System.RuntimeFieldHandle" /> for the field identified by <paramref name="fieldToken" />.</returns>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="metadataToken" /> is not a valid token in the scope of the current module.  
		/// -or-  
		/// <paramref name="metadataToken" /> is not a token for a field in the scope of the current module.  
		/// -or-  
		/// <paramref name="metadataToken" /> identifies a field whose parent <see langword="TypeSpec" /> has a signature containing element type <see langword="var" /> or <see langword="mvar" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The method is called on an empty field handle.</exception>
		// Token: 0x06001A22 RID: 6690 RVA: 0x0005FFFD File Offset: 0x0005E1FD
		public RuntimeFieldHandle ResolveFieldHandle(int fieldToken)
		{
			return this.ResolveFieldHandle(fieldToken, null, null);
		}

		/// <summary>Returns a runtime method handle for the method or constructor identified by the specified metadata token.</summary>
		/// <param name="methodToken">A metadata token that identifies a method or constructor in the module.</param>
		/// <returns>A <see cref="T:System.RuntimeMethodHandle" /> for the method or constructor identified by <paramref name="methodToken" />.</returns>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="methodToken" /> is not a valid metadata token for a method in the current module.  
		/// -or-  
		/// <paramref name="metadataToken" /> is not a token for a method or constructor in the scope of the current module.  
		/// -or-  
		/// <paramref name="metadataToken" /> is a <see langword="MethodSpec" /> whose signature contains element type <see langword="var" /> or <see langword="mvar" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The method is called on an empty method handle.</exception>
		// Token: 0x06001A23 RID: 6691 RVA: 0x00060008 File Offset: 0x0005E208
		public RuntimeMethodHandle ResolveMethodHandle(int methodToken)
		{
			return this.ResolveMethodHandle(methodToken, null, null);
		}

		/// <summary>Returns a runtime type handle for the type identified by the specified metadata token.</summary>
		/// <param name="typeToken">A metadata token that identifies a type in the module.</param>
		/// <returns>A <see cref="T:System.RuntimeTypeHandle" /> for the type identified by <paramref name="typeToken" />.</returns>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="typeToken" /> is not a valid metadata token for a type in the current module.  
		/// -or-  
		/// <paramref name="metadataToken" /> is not a token for a type in the scope of the current module.  
		/// -or-  
		/// <paramref name="metadataToken" /> is a <see langword="TypeSpec" /> whose signature contains element type <see langword="var" /> or <see langword="mvar" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The method is called on an empty type handle.</exception>
		// Token: 0x06001A24 RID: 6692 RVA: 0x00060013 File Offset: 0x0005E213
		public RuntimeTypeHandle ResolveTypeHandle(int typeToken)
		{
			return this.ResolveTypeHandle(typeToken, null, null);
		}

		// Token: 0x06001A25 RID: 6693 RVA: 0x00060020 File Offset: 0x0005E220
		private IntPtr[] ptrs_from_handles(RuntimeTypeHandle[] handles)
		{
			if (handles == null)
			{
				return null;
			}
			IntPtr[] array = new IntPtr[handles.Length];
			for (int i = 0; i < handles.Length; i++)
			{
				array[i] = handles[i].Value;
			}
			return array;
		}

		/// <summary>Returns a runtime type handle for the type identified by the specified metadata token, specifying the generic type arguments of the type and method where the token is in scope.</summary>
		/// <param name="typeToken">A metadata token that identifies a type in the module.</param>
		/// <param name="typeInstantiationContext">An array of <see cref="T:System.RuntimeTypeHandle" /> structures representing the generic type arguments of the type where the token is in scope, or <see langword="null" /> if that type is not generic.</param>
		/// <param name="methodInstantiationContext">An array of <see cref="T:System.RuntimeTypeHandle" /> structures objects representing the generic type arguments of the method where the token is in scope, or <see langword="null" /> if that method is not generic.</param>
		/// <returns>A <see cref="T:System.RuntimeTypeHandle" /> for the type identified by <paramref name="typeToken" />.</returns>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="typeToken" /> is not a valid metadata token for a type in the current module.  
		/// -or-  
		/// <paramref name="metadataToken" /> is not a token for a type in the scope of the current module.  
		/// -or-  
		/// <paramref name="metadataToken" /> is a <see langword="TypeSpec" /> whose signature contains element type <see langword="var" /> or <see langword="mvar" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The method is called on an empty type handle.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="typeToken" /> is not a valid token.</exception>
		// Token: 0x06001A26 RID: 6694 RVA: 0x0006005C File Offset: 0x0005E25C
		public RuntimeTypeHandle ResolveTypeHandle(int typeToken, RuntimeTypeHandle[] typeInstantiationContext, RuntimeTypeHandle[] methodInstantiationContext)
		{
			if (this.value == IntPtr.Zero)
			{
				throw new ArgumentNullException(string.Empty, "Invalid handle");
			}
			ResolveTokenError resolveTokenError;
			IntPtr intPtr = RuntimeModule.ResolveTypeToken(this.value, typeToken, this.ptrs_from_handles(typeInstantiationContext), this.ptrs_from_handles(methodInstantiationContext), out resolveTokenError);
			if (intPtr == IntPtr.Zero)
			{
				throw new TypeLoadException(string.Format("Could not load type '0x{0:x}' from assembly '0x{1:x}'", typeToken, this.value.ToInt64()));
			}
			return new RuntimeTypeHandle(intPtr);
		}

		/// <summary>Returns a runtime method handle for the method or constructor identified by the specified metadata token, specifying the generic type arguments of the type and method where the token is in scope.</summary>
		/// <param name="methodToken">A metadata token that identifies a method or constructor in the module.</param>
		/// <param name="typeInstantiationContext">An array of <see cref="T:System.RuntimeTypeHandle" /> structures representing the generic type arguments of the type where the token is in scope, or <see langword="null" /> if that type is not generic.</param>
		/// <param name="methodInstantiationContext">An array of <see cref="T:System.RuntimeTypeHandle" /> structures representing the generic type arguments of the method where the token is in scope, or <see langword="null" /> if that method is not generic.</param>
		/// <returns>A <see cref="T:System.RuntimeMethodHandle" /> for the method or constructor identified by <paramref name="methodToken" />.</returns>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="methodToken" /> is not a valid metadata token for a method in the current module.  
		/// -or-  
		/// <paramref name="metadataToken" /> is not a token for a method or constructor in the scope of the current module.  
		/// -or-  
		/// <paramref name="metadataToken" /> is a <see langword="MethodSpec" /> whose signature contains element type <see langword="var" /> or <see langword="mvar" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The method is called on an empty method handle.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="methodToken" /> is not a valid token.</exception>
		// Token: 0x06001A27 RID: 6695 RVA: 0x000600E4 File Offset: 0x0005E2E4
		public RuntimeMethodHandle ResolveMethodHandle(int methodToken, RuntimeTypeHandle[] typeInstantiationContext, RuntimeTypeHandle[] methodInstantiationContext)
		{
			if (this.value == IntPtr.Zero)
			{
				throw new ArgumentNullException(string.Empty, "Invalid handle");
			}
			ResolveTokenError resolveTokenError;
			IntPtr intPtr = RuntimeModule.ResolveMethodToken(this.value, methodToken, this.ptrs_from_handles(typeInstantiationContext), this.ptrs_from_handles(methodInstantiationContext), out resolveTokenError);
			if (intPtr == IntPtr.Zero)
			{
				throw new Exception(string.Format("Could not load method '0x{0:x}' from assembly '0x{1:x}'", methodToken, this.value.ToInt64()));
			}
			return new RuntimeMethodHandle(intPtr);
		}

		/// <summary>Returns a runtime field handle for the field identified by the specified metadata token, specifying the generic type arguments of the type and method where the token is in scope.</summary>
		/// <param name="fieldToken">A metadata token that identifies a field in the module.</param>
		/// <param name="typeInstantiationContext">An array of <see cref="T:System.RuntimeTypeHandle" /> structures representing the generic type arguments of the type where the token is in scope, or <see langword="null" /> if that type is not generic.</param>
		/// <param name="methodInstantiationContext">An array of <see cref="T:System.RuntimeTypeHandle" /> structures representing the generic type arguments of the method where the token is in scope, or <see langword="null" /> if that method is not generic.</param>
		/// <returns>A <see cref="T:System.RuntimeFieldHandle" /> for the field identified by <paramref name="fieldToken" />.</returns>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="metadataToken" /> is not a valid token in the scope of the current module.  
		/// -or-  
		/// <paramref name="metadataToken" /> is not a token for a field in the scope of the current module.  
		/// -or-  
		/// <paramref name="metadataToken" /> identifies a field whose parent <see langword="TypeSpec" /> has a signature containing element type <see langword="var" /> or <see langword="mvar" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The method is called on an empty field handle.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="fieldToken" /> is not a valid token.</exception>
		// Token: 0x06001A28 RID: 6696 RVA: 0x0006016C File Offset: 0x0005E36C
		public RuntimeFieldHandle ResolveFieldHandle(int fieldToken, RuntimeTypeHandle[] typeInstantiationContext, RuntimeTypeHandle[] methodInstantiationContext)
		{
			if (this.value == IntPtr.Zero)
			{
				throw new ArgumentNullException(string.Empty, "Invalid handle");
			}
			ResolveTokenError resolveTokenError;
			IntPtr intPtr = RuntimeModule.ResolveFieldToken(this.value, fieldToken, this.ptrs_from_handles(typeInstantiationContext), this.ptrs_from_handles(methodInstantiationContext), out resolveTokenError);
			if (intPtr == IntPtr.Zero)
			{
				throw new Exception(string.Format("Could not load field '0x{0:x}' from assembly '0x{1:x}'", fieldToken, this.value.ToInt64()));
			}
			return new RuntimeFieldHandle(intPtr);
		}

		/// <summary>Returns a runtime handle for the field identified by the specified metadata token.</summary>
		/// <param name="fieldToken">A metadata token that identifies a field in the module.</param>
		/// <returns>A <see cref="T:System.RuntimeFieldHandle" /> for the field identified by <paramref name="fieldToken" />.</returns>
		// Token: 0x06001A29 RID: 6697 RVA: 0x000601F1 File Offset: 0x0005E3F1
		public RuntimeFieldHandle GetRuntimeFieldHandleFromMetadataToken(int fieldToken)
		{
			return this.ResolveFieldHandle(fieldToken);
		}

		/// <summary>Returns a runtime method handle for the method or constructor identified by the specified metadata token.</summary>
		/// <param name="methodToken">A metadata token that identifies a method or constructor in the module.</param>
		/// <returns>A <see cref="T:System.RuntimeMethodHandle" /> for the method or constructor identified by <paramref name="methodToken" />.</returns>
		// Token: 0x06001A2A RID: 6698 RVA: 0x000601FA File Offset: 0x0005E3FA
		public RuntimeMethodHandle GetRuntimeMethodHandleFromMetadataToken(int methodToken)
		{
			return this.ResolveMethodHandle(methodToken);
		}

		/// <summary>Returns a runtime type handle for the type identified by the specified metadata token.</summary>
		/// <param name="typeToken">A metadata token that identifies a type in the module.</param>
		/// <returns>A <see cref="T:System.RuntimeTypeHandle" /> for the type identified by <paramref name="typeToken" />.</returns>
		// Token: 0x06001A2B RID: 6699 RVA: 0x00060203 File Offset: 0x0005E403
		public RuntimeTypeHandle GetRuntimeTypeHandleFromMetadataToken(int typeToken)
		{
			return this.ResolveTypeHandle(typeToken);
		}

		/// <summary>Returns a <see cref="T:System.Boolean" /> value indicating whether the specified object is a <see cref="T:System.ModuleHandle" /> structure, and equal to the current <see cref="T:System.ModuleHandle" />.</summary>
		/// <param name="obj">The object to be compared with the current <see cref="T:System.ModuleHandle" /> structure.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> is a <see cref="T:System.ModuleHandle" /> structure, and is equal to the current <see cref="T:System.ModuleHandle" /> structure; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001A2C RID: 6700 RVA: 0x0006020C File Offset: 0x0005E40C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.value == ((ModuleHandle)obj).Value;
		}

		/// <summary>Returns a <see cref="T:System.Boolean" /> value indicating whether the specified <see cref="T:System.ModuleHandle" /> structure is equal to the current <see cref="T:System.ModuleHandle" />.</summary>
		/// <param name="handle">The <see cref="T:System.ModuleHandle" /> structure to be compared with the current <see cref="T:System.ModuleHandle" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="handle" /> is equal to the current <see cref="T:System.ModuleHandle" /> structure; otherwise <see langword="false" />.</returns>
		// Token: 0x06001A2D RID: 6701 RVA: 0x00060254 File Offset: 0x0005E454
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public bool Equals(ModuleHandle handle)
		{
			return this.value == handle.Value;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
		// Token: 0x06001A2E RID: 6702 RVA: 0x00060268 File Offset: 0x0005E468
		public override int GetHashCode()
		{
			return this.value.GetHashCode();
		}

		/// <summary>Tests whether two <see cref="T:System.ModuleHandle" /> structures are equal.</summary>
		/// <param name="left">The <see cref="T:System.ModuleHandle" /> structure to the left of the equality operator.</param>
		/// <param name="right">The <see cref="T:System.ModuleHandle" /> structure to the right of the equality operator.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.ModuleHandle" /> structures are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001A2F RID: 6703 RVA: 0x00060275 File Offset: 0x0005E475
		public static bool operator ==(ModuleHandle left, ModuleHandle right)
		{
			return object.Equals(left, right);
		}

		/// <summary>Tests whether two <see cref="T:System.ModuleHandle" /> structures are unequal.</summary>
		/// <param name="left">The <see cref="T:System.ModuleHandle" /> structure to the left of the inequality operator.</param>
		/// <param name="right">The <see cref="T:System.ModuleHandle" /> structure to the right of the inequality operator.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.ModuleHandle" /> structures are unequal; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001A30 RID: 6704 RVA: 0x00060288 File Offset: 0x0005E488
		public static bool operator !=(ModuleHandle left, ModuleHandle right)
		{
			return !object.Equals(left, right);
		}

		// Token: 0x06001A31 RID: 6705 RVA: 0x0006029E File Offset: 0x0005E49E
		// Note: this type is marked as 'beforefieldinit'.
		static ModuleHandle()
		{
		}

		// Token: 0x0400171F RID: 5919
		private IntPtr value;

		/// <summary>Represents an empty module handle.</summary>
		// Token: 0x04001720 RID: 5920
		public static readonly ModuleHandle EmptyHandle = new ModuleHandle(IntPtr.Zero);
	}
}
