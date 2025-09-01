using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Serialization.Formatters
{
	/// <summary>Logs tracing messages when the .NET Framework serialization infrastructure is compiled.</summary>
	// Token: 0x0200067E RID: 1662
	[SecurityCritical]
	[ComVisible(true)]
	public sealed class InternalST
	{
		// Token: 0x06003DD9 RID: 15833 RVA: 0x0000259F File Offset: 0x0000079F
		private InternalST()
		{
		}

		/// <summary>Prints SOAP trace messages.</summary>
		/// <param name="messages">An array of trace messages to print.</param>
		// Token: 0x06003DDA RID: 15834 RVA: 0x00004BF9 File Offset: 0x00002DF9
		[Conditional("_LOGGING")]
		public static void InfoSoap(params object[] messages)
		{
		}

		/// <summary>Checks if SOAP tracing is enabled.</summary>
		/// <returns>
		///   <see langword="true" />, if tracing is enabled; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003DDB RID: 15835 RVA: 0x000D59A4 File Offset: 0x000D3BA4
		public static bool SoapCheckEnabled()
		{
			return BCLDebug.CheckEnabled("Soap");
		}

		/// <summary>Processes the specified array of messages.</summary>
		/// <param name="messages">An array of messages to process.</param>
		// Token: 0x06003DDC RID: 15836 RVA: 0x000D59B0 File Offset: 0x000D3BB0
		[Conditional("SER_LOGGING")]
		public static void Soap(params object[] messages)
		{
			if (!(messages[0] is string))
			{
				messages[0] = messages[0].GetType().Name + " ";
				return;
			}
			int num = 0;
			object obj = messages[0];
			messages[num] = ((obj != null) ? obj.ToString() : null) + " ";
		}

		/// <summary>Asserts the specified message.</summary>
		/// <param name="condition">A Boolean value to use when asserting.</param>
		/// <param name="message">The message to use when asserting.</param>
		// Token: 0x06003DDD RID: 15837 RVA: 0x00004BF9 File Offset: 0x00002DF9
		[Conditional("_DEBUG")]
		public static void SoapAssert(bool condition, string message)
		{
		}

		/// <summary>Sets the value of a field.</summary>
		/// <param name="fi">A <see cref="T:System.Reflection.FieldInfo" /> containing data about the target field.</param>
		/// <param name="target">The field to change.</param>
		/// <param name="value">The value to set.</param>
		// Token: 0x06003DDE RID: 15838 RVA: 0x000D59FE File Offset: 0x000D3BFE
		public static void SerializationSetValue(FieldInfo fi, object target, object value)
		{
			if (fi == null)
			{
				throw new ArgumentNullException("fi");
			}
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			FormatterServices.SerializationSetValue(fi, target, value);
		}

		/// <summary>Loads a specified assembly to debug.</summary>
		/// <param name="assemblyString">The name of the assembly to load.</param>
		/// <returns>The <see cref="T:System.Reflection.Assembly" /> to debug.</returns>
		// Token: 0x06003DDF RID: 15839 RVA: 0x000D5A38 File Offset: 0x000D3C38
		public static Assembly LoadAssemblyFromString(string assemblyString)
		{
			return FormatterServices.LoadAssemblyFromString(assemblyString);
		}
	}
}
