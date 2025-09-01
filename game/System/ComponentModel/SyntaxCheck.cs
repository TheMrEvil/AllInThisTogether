using System;
using System.IO;

namespace System.ComponentModel
{
	/// <summary>Provides methods to verify the machine name and path conform to a specific syntax. This class cannot be inherited.</summary>
	// Token: 0x020003EE RID: 1006
	public static class SyntaxCheck
	{
		/// <summary>Checks the syntax of the machine name to confirm that it does not contain "\".</summary>
		/// <param name="value">A string containing the machine name to check.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="value" /> matches the proper machine name format; otherwise, <see langword="false" />.</returns>
		// Token: 0x060020EA RID: 8426 RVA: 0x00071914 File Offset: 0x0006FB14
		public static bool CheckMachineName(string value)
		{
			if (value == null)
			{
				return false;
			}
			value = value.Trim();
			return !value.Equals(string.Empty) && value.IndexOf('\\') == -1;
		}

		/// <summary>Checks the syntax of the path to see whether it starts with "\\".</summary>
		/// <param name="value">A string containing the path to check.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="value" /> matches the proper path format; otherwise, <see langword="false" />.</returns>
		// Token: 0x060020EB RID: 8427 RVA: 0x0007193D File Offset: 0x0006FB3D
		public static bool CheckPath(string value)
		{
			if (value == null)
			{
				return false;
			}
			value = value.Trim();
			return !value.Equals(string.Empty) && value.StartsWith("\\\\");
		}

		/// <summary>Checks the syntax of the path to see if it starts with "\" or drive letter "C:".</summary>
		/// <param name="value">A string containing the path to check.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="value" /> matches the proper path format; otherwise, <see langword="false" />.</returns>
		// Token: 0x060020EC RID: 8428 RVA: 0x00071966 File Offset: 0x0006FB66
		public static bool CheckRootedPath(string value)
		{
			if (value == null)
			{
				return false;
			}
			value = value.Trim();
			return !value.Equals(string.Empty) && Path.IsPathRooted(value);
		}
	}
}
