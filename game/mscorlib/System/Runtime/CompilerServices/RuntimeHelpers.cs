using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Serialization;
using System.Security;

namespace System.Runtime.CompilerServices
{
	/// <summary>Provides a set of static methods and properties that provide support for compilers. This class cannot be inherited.</summary>
	// Token: 0x02000852 RID: 2130
	public static class RuntimeHelpers
	{
		// Token: 0x060046F1 RID: 18161
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InitializeArray(Array array, IntPtr fldHandle);

		/// <summary>Provides a fast way to initialize an array from data that is stored in a module.</summary>
		/// <param name="array">The array to be initialized.</param>
		/// <param name="fldHandle">A field handle that specifies the location of the data used to initialize the array.</param>
		// Token: 0x060046F2 RID: 18162 RVA: 0x000E7DA7 File Offset: 0x000E5FA7
		public static void InitializeArray(Array array, RuntimeFieldHandle fldHandle)
		{
			if (array == null || fldHandle.Value == IntPtr.Zero)
			{
				throw new ArgumentNullException();
			}
			RuntimeHelpers.InitializeArray(array, fldHandle.Value);
		}

		/// <summary>Gets the offset, in bytes, to the data in the given string.</summary>
		/// <returns>The byte offset, from the start of the <see cref="T:System.String" /> object to the first character in the string.</returns>
		// Token: 0x17000AEA RID: 2794
		// (get) Token: 0x060046F3 RID: 18163
		public static extern int OffsetToStringData { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		/// <summary>Serves as a hash function for a particular object, and is suitable for use in algorithms and data structures that use hash codes, such as a hash table.</summary>
		/// <param name="o">An object to retrieve the hash code for.</param>
		/// <returns>A hash code for the object identified by the <paramref name="o" /> parameter.</returns>
		// Token: 0x060046F4 RID: 18164 RVA: 0x00064577 File Offset: 0x00062777
		public static int GetHashCode(object o)
		{
			return object.InternalGetHashCode(o);
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> instances are considered equal.</summary>
		/// <param name="o1">The first object to compare.</param>
		/// <param name="o2">The second object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="o1" /> parameter is the same instance as the <paramref name="o2" /> parameter, or if both are <see langword="null" />, or if o1.Equals(o2) returns <see langword="true" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060046F5 RID: 18165 RVA: 0x000E7DD2 File Offset: 0x000E5FD2
		public new static bool Equals(object o1, object o2)
		{
			if (o1 == o2)
			{
				return true;
			}
			if (o1 == null || o2 == null)
			{
				return false;
			}
			if (o1 is ValueType)
			{
				return ValueType.DefaultEquals(o1, o2);
			}
			return object.Equals(o1, o2);
		}

		/// <summary>Boxes a value type.</summary>
		/// <param name="obj">The value type to be boxed.</param>
		/// <returns>A boxed copy of <paramref name="obj" /> if it is a value class; otherwise, <paramref name="obj" /> itself.</returns>
		// Token: 0x060046F6 RID: 18166
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern object GetObjectValue(object obj);

		// Token: 0x060046F7 RID: 18167
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RunClassConstructor(IntPtr type);

		/// <summary>Runs a specified class constructor method.</summary>
		/// <param name="type">A type handle that specifies the class constructor method to run.</param>
		/// <exception cref="T:System.TypeInitializationException">The class initializer throws an exception.</exception>
		// Token: 0x060046F8 RID: 18168 RVA: 0x000E7DF9 File Offset: 0x000E5FF9
		public static void RunClassConstructor(RuntimeTypeHandle type)
		{
			if (type.Value == IntPtr.Zero)
			{
				throw new ArgumentException("Handle is not initialized.", "type");
			}
			RuntimeHelpers.RunClassConstructor(type.Value);
		}

		// Token: 0x060046F9 RID: 18169
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SufficientExecutionStack();

		/// <summary>Ensures that the remaining stack space is large enough to execute the average .NET Framework function.</summary>
		/// <exception cref="T:System.InsufficientExecutionStackException">The available stack space is insufficient to execute the average .NET Framework function.</exception>
		// Token: 0x060046FA RID: 18170 RVA: 0x000E7E2A File Offset: 0x000E602A
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static void EnsureSufficientExecutionStack()
		{
			if (RuntimeHelpers.SufficientExecutionStack())
			{
				return;
			}
			throw new InsufficientExecutionStackException();
		}

		// Token: 0x060046FB RID: 18171 RVA: 0x000E7E39 File Offset: 0x000E6039
		public static bool TryEnsureSufficientExecutionStack()
		{
			return RuntimeHelpers.SufficientExecutionStack();
		}

		/// <summary>Executes code using a <see cref="T:System.Delegate" /> while using another <see cref="T:System.Delegate" /> to execute additional code in case of an exception.</summary>
		/// <param name="code">A delegate to the code to try.</param>
		/// <param name="backoutCode">A delegate to the code to run if an exception occurs.</param>
		/// <param name="userData">The data to pass to <paramref name="code" /> and <paramref name="backoutCode" />.</param>
		// Token: 0x060046FC RID: 18172 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public static void ExecuteCodeWithGuaranteedCleanup(RuntimeHelpers.TryCode code, RuntimeHelpers.CleanupCode backoutCode, object userData)
		{
		}

		/// <summary>Designates a body of code as a constrained execution region (CER).</summary>
		// Token: 0x060046FD RID: 18173 RVA: 0x00004BF9 File Offset: 0x00002DF9
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static void PrepareConstrainedRegions()
		{
		}

		/// <summary>Designates a body of code as a constrained execution region (CER) without performing any probing.</summary>
		// Token: 0x060046FE RID: 18174 RVA: 0x00004BF9 File Offset: 0x00002DF9
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static void PrepareConstrainedRegionsNoOP()
		{
		}

		/// <summary>Probes for a certain amount of stack space to ensure that a stack overflow cannot happen within a subsequent block of code (assuming that your code uses only a finite and moderate amount of stack space). We recommend that you use a constrained execution region (CER) instead of this method.</summary>
		// Token: 0x060046FF RID: 18175 RVA: 0x00004BF9 File Offset: 0x00002DF9
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static void ProbeForSufficientStack()
		{
		}

		/// <summary>Indicates that the specified delegate should be prepared for inclusion in a constrained execution region (CER).</summary>
		/// <param name="d">The delegate type to prepare.</param>
		// Token: 0x06004700 RID: 18176 RVA: 0x00004BF9 File Offset: 0x00002DF9
		[SecurityCritical]
		public static void PrepareDelegate(Delegate d)
		{
		}

		/// <summary>Provides a way for applications to dynamically prepare <see cref="T:System.AppDomain" /> event delegates.</summary>
		/// <param name="d">The event delegate to prepare.</param>
		// Token: 0x06004701 RID: 18177 RVA: 0x00004BF9 File Offset: 0x00002DF9
		[SecurityCritical]
		public static void PrepareContractedDelegate(Delegate d)
		{
		}

		/// <summary>Prepares a method for inclusion in a constrained execution region (CER).</summary>
		/// <param name="method">A handle to the method to prepare.</param>
		// Token: 0x06004702 RID: 18178 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public static void PrepareMethod(RuntimeMethodHandle method)
		{
		}

		/// <summary>Prepares a method for inclusion in a constrained execution region (CER) with the specified instantiation.</summary>
		/// <param name="method">A handle to the method to prepare.</param>
		/// <param name="instantiation">The instantiation to pass to the method.</param>
		// Token: 0x06004703 RID: 18179 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public static void PrepareMethod(RuntimeMethodHandle method, RuntimeTypeHandle[] instantiation)
		{
		}

		/// <summary>Runs a specified module constructor method.</summary>
		/// <param name="module">A handle that specifies the module constructor method to run.</param>
		/// <exception cref="T:System.TypeInitializationException">The module constructor throws an exception.</exception>
		// Token: 0x06004704 RID: 18180 RVA: 0x000E7E40 File Offset: 0x000E6040
		public static void RunModuleConstructor(ModuleHandle module)
		{
			if (module == ModuleHandle.EmptyHandle)
			{
				throw new ArgumentException("Handle is not initialized.", "module");
			}
			RuntimeHelpers.RunModuleConstructor(module.Value);
		}

		// Token: 0x06004705 RID: 18181
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RunModuleConstructor(IntPtr module);

		// Token: 0x06004706 RID: 18182 RVA: 0x000E7E6B File Offset: 0x000E606B
		public static bool IsReferenceOrContainsReferences<T>()
		{
			return !typeof(T).IsValueType || RuntimeTypeHandle.HasReferences(typeof(T) as RuntimeType);
		}

		// Token: 0x06004707 RID: 18183 RVA: 0x000E7E94 File Offset: 0x000E6094
		public static object GetUninitializedObject(Type type)
		{
			return FormatterServices.GetUninitializedObject(type);
		}

		// Token: 0x06004708 RID: 18184 RVA: 0x000E7E9C File Offset: 0x000E609C
		public static T[] GetSubArray<T>(T[] array, Range range)
		{
			Type elementType = array.GetType().GetElementType();
			Span<T> span = array.AsSpan(range);
			if (elementType.IsValueType)
			{
				return span.ToArray();
			}
			T[] array2 = (T[])Array.CreateInstance(elementType, span.Length);
			span.CopyTo(array2);
			return array2;
		}

		/// <summary>Represents a delegate to code that should be run in a try block.</summary>
		/// <param name="userData">Data to pass to the delegate.</param>
		// Token: 0x02000853 RID: 2131
		// (Invoke) Token: 0x0600470A RID: 18186
		public delegate void TryCode(object userData);

		/// <summary>Represents a method to run when an exception occurs.</summary>
		/// <param name="userData">Data to pass to the delegate.</param>
		/// <param name="exceptionThrown">
		///   <see langword="true" /> to express that an exception was thrown; otherwise, <see langword="false" />.</param>
		// Token: 0x02000854 RID: 2132
		// (Invoke) Token: 0x0600470E RID: 18190
		public delegate void CleanupCode(object userData, bool exceptionThrown);
	}
}
