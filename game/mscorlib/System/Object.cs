using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Supports all classes in the .NET Framework class hierarchy and provides low-level services to derived classes. This is the ultimate base class of all classes in the .NET Framework; it is the root of the type hierarchy.</summary>
	// Token: 0x02000247 RID: 583
	[ClassInterface(ClassInterfaceType.AutoDual)]
	[ComVisible(true)]
	[Serializable]
	public class Object
	{
		/// <summary>Determines whether the specified object is equal to the current object.</summary>
		/// <param name="obj">The object to compare with the current object.</param>
		/// <returns>
		///   <see langword="true" /> if the specified object  is equal to the current object; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001AE0 RID: 6880 RVA: 0x00028426 File Offset: 0x00026626
		public virtual bool Equals(object obj)
		{
			return this == obj;
		}

		/// <summary>Determines whether the specified object instances are considered equal.</summary>
		/// <param name="objA">The first object to compare.</param>
		/// <param name="objB">The second object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the objects are considered equal; otherwise, <see langword="false" />. If both <paramref name="objA" /> and <paramref name="objB" /> are null, the method returns <see langword="true" />.</returns>
		// Token: 0x06001AE1 RID: 6881 RVA: 0x00064560 File Offset: 0x00062760
		public static bool Equals(object objA, object objB)
		{
			return objA == objB || (objA != null && objB != null && objA.Equals(objB));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
		// Token: 0x06001AE2 RID: 6882 RVA: 0x00004BF9 File Offset: 0x00002DF9
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public Object()
		{
		}

		/// <summary>Allows an object to try to free resources and perform other cleanup operations before it is reclaimed by garbage collection.</summary>
		// Token: 0x06001AE3 RID: 6883 RVA: 0x00004BF9 File Offset: 0x00002DF9
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		protected virtual void Finalize()
		{
		}

		/// <summary>Serves as the default hash function.</summary>
		/// <returns>A hash code for the current object.</returns>
		// Token: 0x06001AE4 RID: 6884 RVA: 0x00064577 File Offset: 0x00062777
		public virtual int GetHashCode()
		{
			return object.InternalGetHashCode(this);
		}

		/// <summary>Gets the <see cref="T:System.Type" /> of the current instance.</summary>
		/// <returns>The exact runtime type of the current instance.</returns>
		// Token: 0x06001AE5 RID: 6885
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Type GetType();

		/// <summary>Creates a shallow copy of the current <see cref="T:System.Object" />.</summary>
		/// <returns>A shallow copy of the current <see cref="T:System.Object" />.</returns>
		// Token: 0x06001AE6 RID: 6886
		[MethodImpl(MethodImplOptions.InternalCall)]
		protected extern object MemberwiseClone();

		/// <summary>Returns a string that represents the current object.</summary>
		/// <returns>A string that represents the current object.</returns>
		// Token: 0x06001AE7 RID: 6887 RVA: 0x0006457F File Offset: 0x0006277F
		public virtual string ToString()
		{
			return this.GetType().ToString();
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> instances are the same instance.</summary>
		/// <param name="objA">The first object to compare.</param>
		/// <param name="objB">The second object  to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="objA" /> is the same instance as <paramref name="objB" /> or if both are null; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001AE8 RID: 6888 RVA: 0x00028426 File Offset: 0x00026626
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static bool ReferenceEquals(object objA, object objB)
		{
			return objA == objB;
		}

		// Token: 0x06001AE9 RID: 6889
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int InternalGetHashCode(object o);

		// Token: 0x06001AEA RID: 6890 RVA: 0x00004BF9 File Offset: 0x00002DF9
		private void FieldGetter(string typeName, string fieldName, ref object val)
		{
		}

		// Token: 0x06001AEB RID: 6891 RVA: 0x00004BF9 File Offset: 0x00002DF9
		private void FieldSetter(string typeName, string fieldName, object val)
		{
		}
	}
}
