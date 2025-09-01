using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security;

namespace System
{
	/// <summary>Describes objects that contain both a managed pointer to a location and a runtime representation of the type that may be stored at that location.</summary>
	// Token: 0x0200020F RID: 527
	[CLSCompliant(false)]
	[ComVisible(true)]
	[NonVersionable]
	public ref struct TypedReference
	{
		/// <summary>Makes a <see langword="TypedReference" /> for a field identified by a specified object and list of field descriptions.</summary>
		/// <param name="target">An object that contains the field described by the first element of <paramref name="flds" />.</param>
		/// <param name="flds">A list of field descriptions where each element describes a field that contains the field described by the succeeding element. Each described field must be a value type. The field descriptions must be <see langword="RuntimeFieldInfo" /> objects supplied by the type system.</param>
		/// <returns>A <see cref="T:System.TypedReference" /> for the field described by the last element of <paramref name="flds" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="target" /> or <paramref name="flds" /> is <see langword="null" />.  
		/// -or-  
		/// An element of <paramref name="flds" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="flds" /> array has no elements.  
		///  -or-  
		///  An element of <paramref name="flds" /> is not a <see langword="RuntimeFieldInfo" /> object.  
		///  -or-  
		///  The <see cref="P:System.Reflection.FieldInfo.IsInitOnly" /> or <see cref="P:System.Reflection.FieldInfo.IsStatic" /> property of an element of <paramref name="flds" /> is <see langword="true" />.</exception>
		/// <exception cref="T:System.MissingMemberException">Parameter <paramref name="target" /> does not contain the field described by the first element of <paramref name="flds" />, or an element of <paramref name="flds" /> describes a field that is not contained in the field described by the succeeding element of <paramref name="flds" />.  
		///  -or-  
		///  The field described by an element of <paramref name="flds" /> is not a value type.</exception>
		// Token: 0x06001736 RID: 5942 RVA: 0x0005A738 File Offset: 0x00058938
		[CLSCompliant(false)]
		[SecurityCritical]
		public unsafe static TypedReference MakeTypedReference(object target, FieldInfo[] flds)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			if (flds == null)
			{
				throw new ArgumentNullException("flds");
			}
			if (flds.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Array must not be of length zero."), "flds");
			}
			IntPtr[] array = new IntPtr[flds.Length];
			RuntimeType runtimeType = (RuntimeType)target.GetType();
			for (int i = 0; i < flds.Length; i++)
			{
				RuntimeFieldInfo runtimeFieldInfo = flds[i] as RuntimeFieldInfo;
				if (runtimeFieldInfo == null)
				{
					throw new ArgumentException(Environment.GetResourceString("FieldInfo must be a runtime FieldInfo object."));
				}
				if (runtimeFieldInfo.IsStatic)
				{
					throw new ArgumentException(Environment.GetResourceString("Field in TypedReferences cannot be static or init only."));
				}
				if (runtimeType != runtimeFieldInfo.GetDeclaringTypeInternal() && !runtimeType.IsSubclassOf(runtimeFieldInfo.GetDeclaringTypeInternal()))
				{
					throw new MissingMemberException(Environment.GetResourceString("FieldInfo does not match the target Type."));
				}
				RuntimeType runtimeType2 = (RuntimeType)runtimeFieldInfo.FieldType;
				if (runtimeType2.IsPrimitive)
				{
					throw new ArgumentException(Environment.GetResourceString("TypedReferences cannot be redefined as primitives."));
				}
				if (i < flds.Length - 1 && !runtimeType2.IsValueType)
				{
					throw new MissingMemberException(Environment.GetResourceString("TypedReference can only be made on nested value Types."));
				}
				array[i] = runtimeFieldInfo.FieldHandle.Value;
				runtimeType = runtimeType2;
			}
			TypedReference result = default(TypedReference);
			TypedReference.InternalMakeTypedReference((void*)(&result), target, array, runtimeType);
			return result;
		}

		// Token: 0x06001737 RID: 5943
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void InternalMakeTypedReference(void* result, object target, IntPtr[] flds, RuntimeType lastFieldType);

		/// <summary>Returns the hash code of this object.</summary>
		/// <returns>The hash code of this object.</returns>
		// Token: 0x06001738 RID: 5944 RVA: 0x0005A880 File Offset: 0x00058A80
		public override int GetHashCode()
		{
			if (this.Type == IntPtr.Zero)
			{
				return 0;
			}
			return __reftype(this).GetHashCode();
		}

		/// <summary>Checks if this object is equal to the specified object.</summary>
		/// <param name="o">The object with which to compare the current object.</param>
		/// <returns>
		///   <see langword="true" /> if this object is equal to the specified object; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not implemented.</exception>
		// Token: 0x06001739 RID: 5945 RVA: 0x0005A8A8 File Offset: 0x00058AA8
		public override bool Equals(object o)
		{
			throw new NotSupportedException(Environment.GetResourceString("This feature is not currently implemented."));
		}

		/// <summary>Converts the specified <see langword="TypedReference" /> to an <see langword="Object" />.</summary>
		/// <param name="value">The <see langword="TypedReference" /> to be converted.</param>
		/// <returns>An <see cref="T:System.Object" /> converted from a <see langword="TypedReference" />.</returns>
		// Token: 0x0600173A RID: 5946 RVA: 0x0005A8B9 File Offset: 0x00058AB9
		[SecuritySafeCritical]
		public unsafe static object ToObject(TypedReference value)
		{
			return TypedReference.InternalToObject((void*)(&value));
		}

		// Token: 0x0600173B RID: 5947
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern object InternalToObject(void* value);

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x0600173C RID: 5948 RVA: 0x0005A8C3 File Offset: 0x00058AC3
		internal bool IsNull
		{
			get
			{
				return this.Value == IntPtr.Zero && this.Type == IntPtr.Zero;
			}
		}

		/// <summary>Returns the type of the target of the specified <see langword="TypedReference" />.</summary>
		/// <param name="value">The value whose target's type is to be returned.</param>
		/// <returns>The type of the target of the specified <see langword="TypedReference" />.</returns>
		// Token: 0x0600173D RID: 5949 RVA: 0x0005A8E9 File Offset: 0x00058AE9
		public static Type GetTargetType(TypedReference value)
		{
			return __reftype(value);
		}

		/// <summary>Returns the internal metadata type handle for the specified <see langword="TypedReference" />.</summary>
		/// <param name="value">The <see langword="TypedReference" /> for which the type handle is requested.</param>
		/// <returns>The internal metadata type handle for the specified <see langword="TypedReference" />.</returns>
		// Token: 0x0600173E RID: 5950 RVA: 0x0005A8F3 File Offset: 0x00058AF3
		public static RuntimeTypeHandle TargetTypeToken(TypedReference value)
		{
			return __reftype(value).TypeHandle;
		}

		/// <summary>Converts the specified value to a <see langword="TypedReference" />. This method is not supported.</summary>
		/// <param name="target">The target of the conversion.</param>
		/// <param name="value">The value to be converted.</param>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x0600173F RID: 5951 RVA: 0x0005A902 File Offset: 0x00058B02
		[SecuritySafeCritical]
		[CLSCompliant(false)]
		public static void SetTypedReference(TypedReference target, object value)
		{
			throw new NotImplementedException("SetTypedReference");
		}

		// Token: 0x0400162C RID: 5676
		private RuntimeTypeHandle type;

		// Token: 0x0400162D RID: 5677
		private IntPtr Value;

		// Token: 0x0400162E RID: 5678
		private IntPtr Type;
	}
}
