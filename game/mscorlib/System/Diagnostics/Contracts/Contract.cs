using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security;

namespace System.Diagnostics.Contracts
{
	/// <summary>Contains static methods for representing program contracts such as preconditions, postconditions, and object invariants.</summary>
	// Token: 0x020009CF RID: 2511
	public static class Contract
	{
		/// <summary>Instructs code analysis tools to assume that the specified condition is <see langword="true" />, even if it cannot be statically proven to always be <see langword="true" />.</summary>
		/// <param name="condition">The conditional expression to assume <see langword="true" />.</param>
		// Token: 0x06005A0F RID: 23055 RVA: 0x00133DBD File Offset: 0x00131FBD
		[Conditional("DEBUG")]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[Conditional("CONTRACTS_FULL")]
		public static void Assume(bool condition)
		{
			if (!condition)
			{
				Contract.ReportFailure(ContractFailureKind.Assume, null, null, null);
			}
		}

		/// <summary>Instructs code analysis tools to assume that a condition is <see langword="true" />, even if it cannot be statically proven to always be <see langword="true" />, and displays a message if the assumption fails.</summary>
		/// <param name="condition">The conditional expression to assume <see langword="true" />.</param>
		/// <param name="userMessage">The message to post if the assumption fails.</param>
		// Token: 0x06005A10 RID: 23056 RVA: 0x00133DCB File Offset: 0x00131FCB
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[Conditional("CONTRACTS_FULL")]
		[Conditional("DEBUG")]
		public static void Assume(bool condition, string userMessage)
		{
			if (!condition)
			{
				Contract.ReportFailure(ContractFailureKind.Assume, userMessage, null, null);
			}
		}

		/// <summary>Checks for a condition; if the condition is <see langword="false" />, follows the escalation policy set for the analyzer.</summary>
		/// <param name="condition">The conditional expression to test.</param>
		// Token: 0x06005A11 RID: 23057 RVA: 0x00133DD9 File Offset: 0x00131FD9
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[Conditional("CONTRACTS_FULL")]
		[Conditional("DEBUG")]
		public static void Assert(bool condition)
		{
			if (!condition)
			{
				Contract.ReportFailure(ContractFailureKind.Assert, null, null, null);
			}
		}

		/// <summary>Checks for a condition; if the condition is <see langword="false" />, follows the escalation policy set by the analyzer and displays the specified message.</summary>
		/// <param name="condition">The conditional expression to test.</param>
		/// <param name="userMessage">A message to display if the condition is not met.</param>
		// Token: 0x06005A12 RID: 23058 RVA: 0x00133DE7 File Offset: 0x00131FE7
		[Conditional("CONTRACTS_FULL")]
		[Conditional("DEBUG")]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static void Assert(bool condition, string userMessage)
		{
			if (!condition)
			{
				Contract.ReportFailure(ContractFailureKind.Assert, userMessage, null, null);
			}
		}

		/// <summary>Specifies a precondition contract for the enclosing method or property.</summary>
		/// <param name="condition">The conditional expression to test.</param>
		// Token: 0x06005A13 RID: 23059 RVA: 0x00133DF5 File Offset: 0x00131FF5
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[Conditional("CONTRACTS_FULL")]
		public static void Requires(bool condition)
		{
			Contract.AssertMustUseRewriter(ContractFailureKind.Precondition, "Requires");
		}

		/// <summary>Specifies a precondition contract for the enclosing method or property, and displays a message if the condition for the contract fails.</summary>
		/// <param name="condition">The conditional expression to test.</param>
		/// <param name="userMessage">The message to display if the condition is <see langword="false" />.</param>
		// Token: 0x06005A14 RID: 23060 RVA: 0x00133DF5 File Offset: 0x00131FF5
		[Conditional("CONTRACTS_FULL")]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static void Requires(bool condition, string userMessage)
		{
			Contract.AssertMustUseRewriter(ContractFailureKind.Precondition, "Requires");
		}

		/// <summary>Specifies a precondition contract for the enclosing method or property, and throws an exception if the condition for the contract fails.</summary>
		/// <param name="condition">The conditional expression to test.</param>
		/// <typeparam name="TException">The exception to throw if the condition is <see langword="false" />.</typeparam>
		// Token: 0x06005A15 RID: 23061 RVA: 0x00133E02 File Offset: 0x00132002
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static void Requires<TException>(bool condition) where TException : Exception
		{
			Contract.AssertMustUseRewriter(ContractFailureKind.Precondition, "Requires<TException>");
		}

		/// <summary>Specifies a precondition contract for the enclosing method or property, and throws an exception with the provided message if the condition for the contract fails.</summary>
		/// <param name="condition">The conditional expression to test.</param>
		/// <param name="userMessage">The message to display if the condition is <see langword="false" />.</param>
		/// <typeparam name="TException">The exception to throw if the condition is <see langword="false" />.</typeparam>
		// Token: 0x06005A16 RID: 23062 RVA: 0x00133E02 File Offset: 0x00132002
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static void Requires<TException>(bool condition, string userMessage) where TException : Exception
		{
			Contract.AssertMustUseRewriter(ContractFailureKind.Precondition, "Requires<TException>");
		}

		/// <summary>Specifies a postcondition contract for the enclosing method or property.</summary>
		/// <param name="condition">The conditional expression to test. The expression may include <see cref="M:System.Diagnostics.Contracts.Contract.OldValue``1(``0)" />, <see cref="M:System.Diagnostics.Contracts.Contract.ValueAtReturn``1(``0@)" />, and <see cref="M:System.Diagnostics.Contracts.Contract.Result``1" /> values.</param>
		// Token: 0x06005A17 RID: 23063 RVA: 0x00133E0F File Offset: 0x0013200F
		[Conditional("CONTRACTS_FULL")]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static void Ensures(bool condition)
		{
			Contract.AssertMustUseRewriter(ContractFailureKind.Postcondition, "Ensures");
		}

		/// <summary>Specifies a postcondition contract for a provided exit condition and a message to display if the condition is <see langword="false" />.</summary>
		/// <param name="condition">The conditional expression to test. The expression may include <see cref="M:System.Diagnostics.Contracts.Contract.OldValue``1(``0)" /> and <see cref="M:System.Diagnostics.Contracts.Contract.Result``1" /> values.</param>
		/// <param name="userMessage">The message to display if the expression is not <see langword="true" />.</param>
		// Token: 0x06005A18 RID: 23064 RVA: 0x00133E0F File Offset: 0x0013200F
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[Conditional("CONTRACTS_FULL")]
		public static void Ensures(bool condition, string userMessage)
		{
			Contract.AssertMustUseRewriter(ContractFailureKind.Postcondition, "Ensures");
		}

		/// <summary>Specifies a postcondition contract for the enclosing method or property, based on the provided exception and condition.</summary>
		/// <param name="condition">The conditional expression to test.</param>
		/// <typeparam name="TException">The type of exception that invokes the postcondition check.</typeparam>
		// Token: 0x06005A19 RID: 23065 RVA: 0x00133E1C File Offset: 0x0013201C
		[Conditional("CONTRACTS_FULL")]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static void EnsuresOnThrow<TException>(bool condition) where TException : Exception
		{
			Contract.AssertMustUseRewriter(ContractFailureKind.PostconditionOnException, "EnsuresOnThrow");
		}

		/// <summary>Specifies a postcondition contract and a message to display if the condition is <see langword="false" /> for the enclosing method or property, based on the provided exception and condition.</summary>
		/// <param name="condition">The conditional expression to test.</param>
		/// <param name="userMessage">The message to display if the expression is <see langword="false" />.</param>
		/// <typeparam name="TException">The type of exception that invokes the postcondition check.</typeparam>
		// Token: 0x06005A1A RID: 23066 RVA: 0x00133E1C File Offset: 0x0013201C
		[Conditional("CONTRACTS_FULL")]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static void EnsuresOnThrow<TException>(bool condition, string userMessage) where TException : Exception
		{
			Contract.AssertMustUseRewriter(ContractFailureKind.PostconditionOnException, "EnsuresOnThrow");
		}

		/// <summary>Represents the return value of a method or property.</summary>
		/// <typeparam name="T">Type of return value of the enclosing method or property.</typeparam>
		/// <returns>Return value of the enclosing method or property.</returns>
		// Token: 0x06005A1B RID: 23067 RVA: 0x00133E2C File Offset: 0x0013202C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static T Result<T>()
		{
			return default(T);
		}

		/// <summary>Represents the final (output) value of an <see langword="out" /> parameter when returning from a method.</summary>
		/// <param name="value">The <see langword="out" /> parameter.</param>
		/// <typeparam name="T">The type of the <see langword="out" /> parameter.</typeparam>
		/// <returns>The output value of the <see langword="out" /> parameter.</returns>
		// Token: 0x06005A1C RID: 23068 RVA: 0x00133E42 File Offset: 0x00132042
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static T ValueAtReturn<T>(out T value)
		{
			value = default(T);
			return value;
		}

		/// <summary>Represents values as they were at the start of a method or property.</summary>
		/// <param name="value">The value to represent (field or parameter).</param>
		/// <typeparam name="T">The type of value.</typeparam>
		/// <returns>The value of the parameter or field at the start of a method or property.</returns>
		// Token: 0x06005A1D RID: 23069 RVA: 0x00133E54 File Offset: 0x00132054
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static T OldValue<T>(T value)
		{
			return default(T);
		}

		/// <summary>Specifies an invariant contract for the enclosing method or property.</summary>
		/// <param name="condition">The conditional expression to test.</param>
		// Token: 0x06005A1E RID: 23070 RVA: 0x00133E6A File Offset: 0x0013206A
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[Conditional("CONTRACTS_FULL")]
		public static void Invariant(bool condition)
		{
			Contract.AssertMustUseRewriter(ContractFailureKind.Invariant, "Invariant");
		}

		/// <summary>Specifies an invariant contract for the enclosing method or property, and displays a message if the condition for the contract fails.</summary>
		/// <param name="condition">The conditional expression to test.</param>
		/// <param name="userMessage">The message to display if the condition is <see langword="false" />.</param>
		// Token: 0x06005A1F RID: 23071 RVA: 0x00133E6A File Offset: 0x0013206A
		[Conditional("CONTRACTS_FULL")]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static void Invariant(bool condition, string userMessage)
		{
			Contract.AssertMustUseRewriter(ContractFailureKind.Invariant, "Invariant");
		}

		/// <summary>Determines whether a particular condition is valid for all integers in a specified range.</summary>
		/// <param name="fromInclusive">The first integer to pass to <paramref name="predicate" />.</param>
		/// <param name="toExclusive">One more than the last integer to pass to <paramref name="predicate" />.</param>
		/// <param name="predicate">The function to evaluate for the existence of the integers in the specified range.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="predicate" /> returns <see langword="true" /> for all integers starting from <paramref name="fromInclusive" /> to <paramref name="toExclusive" /> - 1.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="predicate" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="toExclusive" /> is less than <paramref name="fromInclusive" />.</exception>
		// Token: 0x06005A20 RID: 23072 RVA: 0x00133E78 File Offset: 0x00132078
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static bool ForAll(int fromInclusive, int toExclusive, Predicate<int> predicate)
		{
			if (fromInclusive > toExclusive)
			{
				throw new ArgumentException("fromInclusive must be less than or equal to toExclusive.");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			for (int i = fromInclusive; i < toExclusive; i++)
			{
				if (!predicate(i))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Determines whether all the elements in a collection exist within a function.</summary>
		/// <param name="collection">The collection from which elements of type T will be drawn to pass to <paramref name="predicate" />.</param>
		/// <param name="predicate">The function to evaluate for the existence of all the elements in <paramref name="collection" />.</param>
		/// <typeparam name="T">The type that is contained in <paramref name="collection" />.</typeparam>
		/// <returns>
		///   <see langword="true" /> if and only if <paramref name="predicate" /> returns <see langword="true" /> for all elements of type <paramref name="T" /> in <paramref name="collection" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="collection" /> or <paramref name="predicate" /> is <see langword="null" />.</exception>
		// Token: 0x06005A21 RID: 23073 RVA: 0x00133EBC File Offset: 0x001320BC
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static bool ForAll<T>(IEnumerable<T> collection, Predicate<T> predicate)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			foreach (T obj in collection)
			{
				if (!predicate(obj))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Determines whether a specified test is true for any integer within a range of integers.</summary>
		/// <param name="fromInclusive">The first integer to pass to <paramref name="predicate" />.</param>
		/// <param name="toExclusive">One more than the last integer to pass to <paramref name="predicate" />.</param>
		/// <param name="predicate">The function to evaluate for any value of the integer in the specified range.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="predicate" /> returns <see langword="true" /> for any integer starting from <paramref name="fromInclusive" /> to <paramref name="toExclusive" /> - 1.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="predicate" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="toExclusive" /> is less than <paramref name="fromInclusive" />.</exception>
		// Token: 0x06005A22 RID: 23074 RVA: 0x00133F2C File Offset: 0x0013212C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static bool Exists(int fromInclusive, int toExclusive, Predicate<int> predicate)
		{
			if (fromInclusive > toExclusive)
			{
				throw new ArgumentException("fromInclusive must be less than or equal to toExclusive.");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			for (int i = fromInclusive; i < toExclusive; i++)
			{
				if (predicate(i))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Determines whether an element within a collection of elements exists within a function.</summary>
		/// <param name="collection">The collection from which elements of type T will be drawn to pass to <paramref name="predicate" />.</param>
		/// <param name="predicate">The function to evaluate for an element in <paramref name="collection" />.</param>
		/// <typeparam name="T">The type that is contained in <paramref name="collection" />.</typeparam>
		/// <returns>
		///   <see langword="true" /> if and only if <paramref name="predicate" /> returns <see langword="true" /> for any element of type <paramref name="T" /> in <paramref name="collection" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="collection" /> or <paramref name="predicate" /> is <see langword="null" />.</exception>
		// Token: 0x06005A23 RID: 23075 RVA: 0x00133F70 File Offset: 0x00132170
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static bool Exists<T>(IEnumerable<T> collection, Predicate<T> predicate)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			foreach (T obj in collection)
			{
				if (predicate(obj))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Marks the end of the contract section when a method's contracts contain only preconditions in the <see langword="if" />-<see langword="then" />-<see langword="throw" /> form.</summary>
		// Token: 0x06005A24 RID: 23076 RVA: 0x00004BF9 File Offset: 0x00002DF9
		[Conditional("CONTRACTS_FULL")]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static void EndContractBlock()
		{
		}

		// Token: 0x06005A25 RID: 23077 RVA: 0x00133FE0 File Offset: 0x001321E0
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[DebuggerNonUserCode]
		private static void ReportFailure(ContractFailureKind failureKind, string userMessage, string conditionText, Exception innerException)
		{
			if (failureKind < ContractFailureKind.Precondition || failureKind > ContractFailureKind.Assume)
			{
				throw new ArgumentException(Environment.GetResourceString("Illegal enum value: {0}.", new object[]
				{
					failureKind
				}), "failureKind");
			}
			string text = ContractHelper.RaiseContractFailedEvent(failureKind, userMessage, conditionText, innerException);
			if (text == null)
			{
				return;
			}
			ContractHelper.TriggerFailure(failureKind, text, userMessage, conditionText, innerException);
		}

		// Token: 0x06005A26 RID: 23078 RVA: 0x00134034 File Offset: 0x00132234
		[SecuritySafeCritical]
		private static void AssertMustUseRewriter(ContractFailureKind kind, string contractKind)
		{
			if (Contract._assertingMustUseRewriter)
			{
				System.Diagnostics.Assert.Fail("Asserting that we must use the rewriter went reentrant.", "Didn't rewrite this mscorlib?");
			}
			Contract._assertingMustUseRewriter = true;
			Assembly assembly = typeof(Contract).Assembly;
			StackTrace stackTrace = new StackTrace();
			Assembly assembly2 = null;
			for (int i = 0; i < stackTrace.FrameCount; i++)
			{
				Assembly assembly3 = stackTrace.GetFrame(i).GetMethod().DeclaringType.Assembly;
				if (assembly3 != assembly)
				{
					assembly2 = assembly3;
					break;
				}
			}
			if (assembly2 == null)
			{
				assembly2 = assembly;
			}
			string name = assembly2.GetName().Name;
			ContractHelper.TriggerFailure(kind, Environment.GetResourceString("An assembly (probably \"{1}\") must be rewritten using the code contracts binary rewriter (CCRewrite) because it is calling Contract.{0} and the CONTRACTS_FULL symbol is defined.  Remove any explicit definitions of the CONTRACTS_FULL symbol from your project and rebuild.  CCRewrite can be downloaded from http://go.microsoft.com/fwlink/?LinkID=169180. \\r\\nAfter the rewriter is installed, it can be enabled in Visual Studio from the project's Properties page on the Code Contracts pane.  Ensure that \"Perform Runtime Contract Checking\" is enabled, which will define CONTRACTS_FULL.", new object[]
			{
				contractKind,
				name
			}), null, null, null);
			Contract._assertingMustUseRewriter = false;
		}

		/// <summary>Occurs when a contract fails.</summary>
		// Token: 0x1400001F RID: 31
		// (add) Token: 0x06005A27 RID: 23079 RVA: 0x001340F0 File Offset: 0x001322F0
		// (remove) Token: 0x06005A28 RID: 23080 RVA: 0x001340F8 File Offset: 0x001322F8
		public static event EventHandler<ContractFailedEventArgs> ContractFailed
		{
			[SecurityCritical]
			add
			{
				ContractHelper.InternalContractFailed += value;
			}
			[SecurityCritical]
			remove
			{
				ContractHelper.InternalContractFailed -= value;
			}
		}

		// Token: 0x040037AB RID: 14251
		[ThreadStatic]
		private static bool _assertingMustUseRewriter;
	}
}
