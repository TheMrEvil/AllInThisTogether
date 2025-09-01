using System;
using System.Buffers;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Text;

namespace System.IO
{
	/// <summary>Performs operations on <see cref="T:System.String" /> instances that contain file or directory path information. These operations are performed in a cross-platform manner.</summary>
	// Token: 0x02000B67 RID: 2919
	[ComVisible(true)]
	public static class Path
	{
		/// <summary>Changes the extension of a path string.</summary>
		/// <param name="path">The path information to modify. The path cannot contain any of the characters defined in <see cref="M:System.IO.Path.GetInvalidPathChars" />.</param>
		/// <param name="extension">The new extension (with or without a leading period). Specify <see langword="null" /> to remove an existing extension from <paramref name="path" />.</param>
		/// <returns>The modified path information.  
		///  On Windows-based desktop platforms, if <paramref name="path" /> is <see langword="null" /> or an empty string (""), the path information is returned unmodified. If <paramref name="extension" /> is <see langword="null" />, the returned string contains the specified path with its extension removed. If <paramref name="path" /> has no extension, and <paramref name="extension" /> is not <see langword="null" />, the returned path string contains <paramref name="extension" /> appended to the end of <paramref name="path" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> contains one or more of the invalid characters defined in <see cref="M:System.IO.Path.GetInvalidPathChars" />.</exception>
		// Token: 0x06006A14 RID: 27156 RVA: 0x0016A0D4 File Offset: 0x001682D4
		public static string ChangeExtension(string path, string extension)
		{
			if (path == null)
			{
				return null;
			}
			if (path.IndexOfAny(Path.InvalidPathChars) != -1)
			{
				throw new ArgumentException("Illegal characters in path.");
			}
			int num = Path.findExtension(path);
			if (extension == null)
			{
				if (num >= 0)
				{
					return path.Substring(0, num);
				}
				return path;
			}
			else if (extension.Length == 0)
			{
				if (num >= 0)
				{
					return path.Substring(0, num + 1);
				}
				return path + ".";
			}
			else
			{
				if (path.Length != 0)
				{
					if (extension.Length > 0 && extension[0] != '.')
					{
						extension = "." + extension;
					}
				}
				else
				{
					extension = string.Empty;
				}
				if (num < 0)
				{
					return path + extension;
				}
				if (num > 0)
				{
					return path.Substring(0, num) + extension;
				}
				return extension;
			}
		}

		/// <summary>Combines two strings into a path.</summary>
		/// <param name="path1">The first path to combine.</param>
		/// <param name="path2">The second path to combine.</param>
		/// <returns>The combined paths. If one of the specified paths is a zero-length string, this method returns the other path. If <paramref name="path2" /> contains an absolute path, this method returns <paramref name="path2" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path1" /> or <paramref name="path2" /> contains one or more of the invalid characters defined in <see cref="M:System.IO.Path.GetInvalidPathChars" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path1" /> or <paramref name="path2" /> is <see langword="null" />.</exception>
		// Token: 0x06006A15 RID: 27157 RVA: 0x0016A190 File Offset: 0x00168390
		public static string Combine(string path1, string path2)
		{
			if (path1 == null)
			{
				throw new ArgumentNullException("path1");
			}
			if (path2 == null)
			{
				throw new ArgumentNullException("path2");
			}
			if (path1.Length == 0)
			{
				return path2;
			}
			if (path2.Length == 0)
			{
				return path1;
			}
			if (path1.IndexOfAny(Path.InvalidPathChars) != -1)
			{
				throw new ArgumentException("Illegal characters in path.");
			}
			if (path2.IndexOfAny(Path.InvalidPathChars) != -1)
			{
				throw new ArgumentException("Illegal characters in path.");
			}
			if (Path.IsPathRooted(path2))
			{
				return path2;
			}
			char c = path1[path1.Length - 1];
			if (c != Path.DirectorySeparatorChar && c != Path.AltDirectorySeparatorChar && c != Path.VolumeSeparatorChar)
			{
				return path1 + Path.DirectorySeparatorStr + path2;
			}
			return path1 + path2;
		}

		// Token: 0x06006A16 RID: 27158 RVA: 0x0016A244 File Offset: 0x00168444
		internal static string CleanPath(string s)
		{
			int length = s.Length;
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			char c = s[0];
			if (length > 2 && c == '\\' && s[1] == '\\')
			{
				num3 = 2;
			}
			if (length == 1 && (c == Path.DirectorySeparatorChar || c == Path.AltDirectorySeparatorChar))
			{
				return s;
			}
			for (int i = num3; i < length; i++)
			{
				char c2 = s[i];
				if (c2 == Path.DirectorySeparatorChar || c2 == Path.AltDirectorySeparatorChar)
				{
					if (Path.DirectorySeparatorChar != Path.AltDirectorySeparatorChar && c2 == Path.AltDirectorySeparatorChar)
					{
						num2++;
					}
					if (i + 1 == length)
					{
						num++;
					}
					else
					{
						c2 = s[i + 1];
						if (c2 == Path.DirectorySeparatorChar || c2 == Path.AltDirectorySeparatorChar)
						{
							num++;
						}
					}
				}
			}
			if (num == 0 && num2 == 0)
			{
				return s;
			}
			char[] array = new char[length - num];
			if (num3 != 0)
			{
				array[0] = '\\';
				array[1] = '\\';
			}
			int j = num3;
			int num4 = num3;
			while (j < length && num4 < array.Length)
			{
				char c3 = s[j];
				if (c3 != Path.DirectorySeparatorChar && c3 != Path.AltDirectorySeparatorChar)
				{
					array[num4++] = c3;
				}
				else if (num4 + 1 != array.Length)
				{
					array[num4++] = Path.DirectorySeparatorChar;
					while (j < length - 1)
					{
						c3 = s[j + 1];
						if (c3 != Path.DirectorySeparatorChar && c3 != Path.AltDirectorySeparatorChar)
						{
							break;
						}
						j++;
					}
				}
				j++;
			}
			return new string(array);
		}

		/// <summary>Returns the directory information for the specified path string.</summary>
		/// <param name="path">The path of a file or directory.</param>
		/// <returns>Directory information for <paramref name="path" />, or <see langword="null" /> if <paramref name="path" /> denotes a root directory or is null. Returns <see cref="F:System.String.Empty" /> if <paramref name="path" /> does not contain directory information.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="path" /> parameter contains invalid characters, is empty, or contains only white spaces.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">In the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.IO.IOException" />, instead.  
		///
		///
		///
		///
		///  The <paramref name="path" /> parameter is longer than the system-defined maximum length.</exception>
		// Token: 0x06006A17 RID: 27159 RVA: 0x0016A3C0 File Offset: 0x001685C0
		public static string GetDirectoryName(string path)
		{
			if (path == string.Empty)
			{
				throw new ArgumentException("Invalid path");
			}
			if (path == null || Path.GetPathRoot(path) == path)
			{
				return null;
			}
			if (path.Trim().Length == 0)
			{
				throw new ArgumentException("Argument string consists of whitespace characters only.");
			}
			if (path.IndexOfAny(Path.InvalidPathChars) > -1)
			{
				throw new ArgumentException("Path contains invalid characters");
			}
			int num = path.LastIndexOfAny(Path.PathSeparatorChars);
			if (num == 0)
			{
				num++;
			}
			if (num <= 0)
			{
				return string.Empty;
			}
			string text = path.Substring(0, num);
			int length = text.Length;
			if (length >= 2 && Path.DirectorySeparatorChar == '\\' && text[length - 1] == Path.VolumeSeparatorChar)
			{
				return text + Path.DirectorySeparatorChar.ToString();
			}
			if (length == 1 && Path.DirectorySeparatorChar == '\\' && path.Length >= 2 && path[num] == Path.VolumeSeparatorChar)
			{
				return text + Path.VolumeSeparatorChar.ToString();
			}
			return Path.CleanPath(text);
		}

		// Token: 0x06006A18 RID: 27160 RVA: 0x0016A4BF File Offset: 0x001686BF
		public static ReadOnlySpan<char> GetDirectoryName(ReadOnlySpan<char> path)
		{
			return Path.GetDirectoryName(path.ToString()).AsSpan();
		}

		/// <summary>Returns the extension of the specified path string.</summary>
		/// <param name="path">The path string from which to get the extension.</param>
		/// <returns>The extension of the specified path (including the period "."), or <see langword="null" />, or <see cref="F:System.String.Empty" />. If <paramref name="path" /> is <see langword="null" />, <see cref="M:System.IO.Path.GetExtension(System.String)" /> returns <see langword="null" />. If <paramref name="path" /> does not have extension information, <see cref="M:System.IO.Path.GetExtension(System.String)" /> returns <see cref="F:System.String.Empty" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> contains one or more of the invalid characters defined in <see cref="M:System.IO.Path.GetInvalidPathChars" />.</exception>
		// Token: 0x06006A19 RID: 27161 RVA: 0x0016A4D8 File Offset: 0x001686D8
		public static string GetExtension(string path)
		{
			if (path == null)
			{
				return null;
			}
			if (path.IndexOfAny(Path.InvalidPathChars) != -1)
			{
				throw new ArgumentException("Illegal characters in path.");
			}
			int num = Path.findExtension(path);
			if (num > -1 && num < path.Length - 1)
			{
				return path.Substring(num);
			}
			return string.Empty;
		}

		/// <summary>Returns the file name and extension of the specified path string.</summary>
		/// <param name="path">The path string from which to obtain the file name and extension.</param>
		/// <returns>The characters after the last directory character in <paramref name="path" />. If the last character of <paramref name="path" /> is a directory or volume separator character, this method returns <see cref="F:System.String.Empty" />. If <paramref name="path" /> is <see langword="null" />, this method returns <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> contains one or more of the invalid characters defined in <see cref="M:System.IO.Path.GetInvalidPathChars" />.</exception>
		// Token: 0x06006A1A RID: 27162 RVA: 0x0016A528 File Offset: 0x00168728
		public static string GetFileName(string path)
		{
			if (path == null || path.Length == 0)
			{
				return path;
			}
			if (path.IndexOfAny(Path.InvalidPathChars) != -1)
			{
				throw new ArgumentException("Illegal characters in path.");
			}
			int num = path.LastIndexOfAny(Path.PathSeparatorChars);
			if (num >= 0)
			{
				return path.Substring(num + 1);
			}
			return path;
		}

		/// <summary>Returns the file name of the specified path string without the extension.</summary>
		/// <param name="path">The path of the file.</param>
		/// <returns>The string returned by <see cref="M:System.IO.Path.GetFileName(System.String)" />, minus the last period (.) and all characters following it.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> contains one or more of the invalid characters defined in <see cref="M:System.IO.Path.GetInvalidPathChars" />.</exception>
		// Token: 0x06006A1B RID: 27163 RVA: 0x0016A576 File Offset: 0x00168776
		public static string GetFileNameWithoutExtension(string path)
		{
			return Path.ChangeExtension(Path.GetFileName(path), null);
		}

		/// <summary>Returns the absolute path for the specified path string.</summary>
		/// <param name="path">The file or directory for which to obtain absolute path information.</param>
		/// <returns>The fully qualified location of <paramref name="path" />, such as "C:\MyFile.txt".</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more of the invalid characters defined in <see cref="M:System.IO.Path.GetInvalidPathChars" />.  
		/// -or-  
		/// The system could not retrieve the absolute path.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permissions.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> contains a colon (":") that is not part of a volume identifier (for example, "c:\").</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		// Token: 0x06006A1C RID: 27164 RVA: 0x0016A584 File Offset: 0x00168784
		public static string GetFullPath(string path)
		{
			return Path.InsecureGetFullPath(path);
		}

		// Token: 0x06006A1D RID: 27165 RVA: 0x0016A584 File Offset: 0x00168784
		internal static string GetFullPathInternal(string path)
		{
			return Path.InsecureGetFullPath(path);
		}

		// Token: 0x06006A1E RID: 27166
		[DllImport("Kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern int GetFullPathName(string path, int numBufferChars, StringBuilder buffer, ref IntPtr lpFilePartOrNull);

		// Token: 0x06006A1F RID: 27167 RVA: 0x0016A58C File Offset: 0x0016878C
		internal static string GetFullPathName(string path)
		{
			StringBuilder stringBuilder = new StringBuilder(260);
			IntPtr zero = IntPtr.Zero;
			int fullPathName = Path.GetFullPathName(path, 260, stringBuilder, ref zero);
			if (fullPathName == 0)
			{
				throw new IOException("Windows API call to GetFullPathName failed, Windows error code: " + Marshal.GetLastWin32Error().ToString());
			}
			if (fullPathName > 260)
			{
				stringBuilder = new StringBuilder(fullPathName);
				Path.GetFullPathName(path, fullPathName, stringBuilder, ref zero);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06006A20 RID: 27168 RVA: 0x0016A5FC File Offset: 0x001687FC
		internal static string WindowsDriveAdjustment(string path)
		{
			if (path.Length < 2)
			{
				if (path.Length == 1 && (path[0] == '\\' || path[0] == '/'))
				{
					return Path.GetPathRoot(Directory.GetCurrentDirectory());
				}
				return path;
			}
			else
			{
				if (path[1] != ':' || !char.IsLetter(path[0]))
				{
					return path;
				}
				string text = Directory.InsecureGetCurrentDirectory();
				if (path.Length == 2)
				{
					if (text[0] == path[0])
					{
						path = text;
					}
					else
					{
						path = Path.GetFullPathName(path);
					}
				}
				else if (path[2] != Path.DirectorySeparatorChar && path[2] != Path.AltDirectorySeparatorChar)
				{
					if (text[0] == path[0])
					{
						path = Path.Combine(text, path.Substring(2, path.Length - 2));
					}
					else
					{
						path = Path.GetFullPathName(path);
					}
				}
				return path;
			}
		}

		// Token: 0x06006A21 RID: 27169 RVA: 0x0016A6D8 File Offset: 0x001688D8
		internal static string InsecureGetFullPath(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Trim().Length == 0)
			{
				throw new ArgumentException(Locale.GetText("The specified path is not of a legal form (empty)."));
			}
			if (Environment.IsRunningOnWindows)
			{
				path = Path.WindowsDriveAdjustment(path);
			}
			char c = path[path.Length - 1];
			bool flag = true;
			if (path.Length >= 2 && Path.IsDirectorySeparator(path[0]) && Path.IsDirectorySeparator(path[1]))
			{
				if (path.Length == 2 || path.IndexOf(path[0], 2) < 0)
				{
					throw new ArgumentException("UNC paths should be of the form \\\\server\\share.");
				}
				if (path[0] != Path.DirectorySeparatorChar)
				{
					path = path.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
				}
			}
			else if (!Path.IsPathRooted(path))
			{
				if (!Environment.IsRunningOnWindows)
				{
					int num = 0;
					while ((num = path.IndexOf('.', num)) != -1 && ++num != path.Length && path[num] != Path.DirectorySeparatorChar && path[num] != Path.AltDirectorySeparatorChar)
					{
					}
					flag = (num > 0);
				}
				string text = Directory.InsecureGetCurrentDirectory();
				if (text[text.Length - 1] == Path.DirectorySeparatorChar)
				{
					path = text + path;
				}
				else
				{
					path = text + Path.DirectorySeparatorChar.ToString() + path;
				}
			}
			else if (Path.DirectorySeparatorChar == '\\' && path.Length >= 2 && Path.IsDirectorySeparator(path[0]) && !Path.IsDirectorySeparator(path[1]))
			{
				string text2 = Directory.InsecureGetCurrentDirectory();
				if (text2[1] == Path.VolumeSeparatorChar)
				{
					path = text2.Substring(0, 2) + path;
				}
				else
				{
					path = text2.Substring(0, text2.IndexOf('\\', text2.IndexOfUnchecked("\\\\", 0, text2.Length) + 1));
				}
			}
			if (flag)
			{
				path = Path.CanonicalizePath(path);
			}
			if (Path.IsDirectorySeparator(c) && path[path.Length - 1] != Path.DirectorySeparatorChar)
			{
				path += Path.DirectorySeparatorChar.ToString();
			}
			string text3;
			if (MonoIO.RemapPath(path, out text3))
			{
				path = text3;
			}
			return path;
		}

		// Token: 0x06006A22 RID: 27170 RVA: 0x0016A905 File Offset: 0x00168B05
		internal static bool IsDirectorySeparator(char c)
		{
			return c == Path.DirectorySeparatorChar || c == Path.AltDirectorySeparatorChar;
		}

		/// <summary>Gets the root directory information of the specified path.</summary>
		/// <param name="path">The path from which to obtain root directory information.</param>
		/// <returns>The root directory of <paramref name="path" />, or <see langword="null" /> if <paramref name="path" /> is <see langword="null" />, or an empty string if <paramref name="path" /> does not contain root directory information.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> contains one or more of the invalid characters defined in <see cref="M:System.IO.Path.GetInvalidPathChars" />.  
		/// -or-  
		/// <see cref="F:System.String.Empty" /> was passed to <paramref name="path" />.</exception>
		// Token: 0x06006A23 RID: 27171 RVA: 0x0016A91C File Offset: 0x00168B1C
		public static string GetPathRoot(string path)
		{
			if (path == null)
			{
				return null;
			}
			if (path.Trim().Length == 0)
			{
				throw new ArgumentException("The specified path is not of a legal form.");
			}
			if (!Path.IsPathRooted(path))
			{
				return string.Empty;
			}
			if (Path.DirectorySeparatorChar == '/')
			{
				if (!Path.IsDirectorySeparator(path[0]))
				{
					return string.Empty;
				}
				return Path.DirectorySeparatorStr;
			}
			else
			{
				int num = 2;
				if (path.Length == 1 && Path.IsDirectorySeparator(path[0]))
				{
					return Path.DirectorySeparatorStr;
				}
				if (path.Length < 2)
				{
					return string.Empty;
				}
				if (Path.IsDirectorySeparator(path[0]) && Path.IsDirectorySeparator(path[1]))
				{
					while (num < path.Length && !Path.IsDirectorySeparator(path[num]))
					{
						num++;
					}
					if (num < path.Length)
					{
						num++;
						while (num < path.Length && !Path.IsDirectorySeparator(path[num]))
						{
							num++;
						}
					}
					return Path.DirectorySeparatorStr + Path.DirectorySeparatorStr + path.Substring(2, num - 2).Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
				}
				if (Path.IsDirectorySeparator(path[0]))
				{
					return Path.DirectorySeparatorStr;
				}
				if (path[1] == Path.VolumeSeparatorChar)
				{
					if (path.Length >= 3 && Path.IsDirectorySeparator(path[2]))
					{
						num++;
					}
					return path.Substring(0, num);
				}
				return Directory.GetCurrentDirectory().Substring(0, 2);
			}
		}

		/// <summary>Creates a uniquely named, zero-byte temporary file on disk and returns the full path of that file.</summary>
		/// <returns>The full path of the temporary file.</returns>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs, such as no unique temporary file name is available.  
		/// -or-
		///  This method was unable to create a temporary file.</exception>
		// Token: 0x06006A24 RID: 27172 RVA: 0x0016AA88 File Offset: 0x00168C88
		[FileIOPermission(SecurityAction.Assert, Unrestricted = true)]
		public static string GetTempFileName()
		{
			FileStream fileStream = null;
			int num = 0;
			Random random = new Random();
			string tempPath = Path.GetTempPath();
			string text;
			do
			{
				int num2 = random.Next();
				text = Path.Combine(tempPath, "tmp" + (num2 + 1).ToString("x", CultureInfo.InvariantCulture) + ".tmp");
				try
				{
					fileStream = new FileStream(text, FileMode.CreateNew, FileAccess.ReadWrite, FileShare.Read, 8192, false, (FileOptions)1);
				}
				catch (IOException ex)
				{
					if (ex._HResult != -2147024816 || num++ > 65536)
					{
						throw;
					}
				}
				catch (UnauthorizedAccessException ex2)
				{
					if (num++ > 65536)
					{
						throw new IOException(ex2.Message, ex2);
					}
				}
			}
			while (fileStream == null);
			fileStream.Close();
			return text;
		}

		/// <summary>Returns the path of the current user's temporary folder.</summary>
		/// <returns>The path to the temporary folder, ending with a backslash.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permissions.</exception>
		// Token: 0x06006A25 RID: 27173 RVA: 0x0016AB58 File Offset: 0x00168D58
		[EnvironmentPermission(SecurityAction.Demand, Unrestricted = true)]
		public static string GetTempPath()
		{
			string temp_path = Path.get_temp_path();
			if (temp_path.Length > 0 && temp_path[temp_path.Length - 1] != Path.DirectorySeparatorChar)
			{
				return temp_path + Path.DirectorySeparatorChar.ToString();
			}
			return temp_path;
		}

		// Token: 0x06006A26 RID: 27174
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string get_temp_path();

		/// <summary>Determines whether a path includes a file name extension.</summary>
		/// <param name="path">The path to search for an extension.</param>
		/// <returns>
		///   <see langword="true" /> if the characters that follow the last directory separator (\\ or /) or volume separator (:) in the path include a period (.) followed by one or more characters; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> contains one or more of the invalid characters defined in <see cref="M:System.IO.Path.GetInvalidPathChars" />.</exception>
		// Token: 0x06006A27 RID: 27175 RVA: 0x0016AB9C File Offset: 0x00168D9C
		public static bool HasExtension(string path)
		{
			if (path == null || path.Trim().Length == 0)
			{
				return false;
			}
			if (path.IndexOfAny(Path.InvalidPathChars) != -1)
			{
				throw new ArgumentException("Illegal characters in path.");
			}
			int num = Path.findExtension(path);
			return 0 <= num && num < path.Length - 1;
		}

		// Token: 0x06006A28 RID: 27176 RVA: 0x0016ABEC File Offset: 0x00168DEC
		public unsafe static bool IsPathRooted(ReadOnlySpan<char> path)
		{
			if (path.Length == 0)
			{
				return false;
			}
			char c = (char)(*path[0]);
			return c == Path.DirectorySeparatorChar || c == Path.AltDirectorySeparatorChar || (!Path.dirEqualsVolume && path.Length > 1 && *path[1] == (ushort)Path.VolumeSeparatorChar);
		}

		/// <summary>Gets a value indicating whether the specified path string contains a root.</summary>
		/// <param name="path">The path to test.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="path" /> contains a root; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> contains one or more of the invalid characters defined in <see cref="M:System.IO.Path.GetInvalidPathChars" />.</exception>
		// Token: 0x06006A29 RID: 27177 RVA: 0x0016AC43 File Offset: 0x00168E43
		public static bool IsPathRooted(string path)
		{
			if (path == null || path.Length == 0)
			{
				return false;
			}
			if (path.IndexOfAny(Path.InvalidPathChars) != -1)
			{
				throw new ArgumentException("Illegal characters in path.");
			}
			return Path.IsPathRooted(path.AsSpan());
		}

		/// <summary>Gets an array containing the characters that are not allowed in file names.</summary>
		/// <returns>An array containing the characters that are not allowed in file names.</returns>
		// Token: 0x06006A2A RID: 27178 RVA: 0x0016AC76 File Offset: 0x00168E76
		public static char[] GetInvalidFileNameChars()
		{
			if (Environment.IsRunningOnWindows)
			{
				return new char[]
				{
					'\0',
					'\u0001',
					'\u0002',
					'\u0003',
					'\u0004',
					'\u0005',
					'\u0006',
					'\a',
					'\b',
					'\t',
					'\n',
					'\v',
					'\f',
					'\r',
					'\u000e',
					'\u000f',
					'\u0010',
					'\u0011',
					'\u0012',
					'\u0013',
					'\u0014',
					'\u0015',
					'\u0016',
					'\u0017',
					'\u0018',
					'\u0019',
					'\u001a',
					'\u001b',
					'\u001c',
					'\u001d',
					'\u001e',
					'\u001f',
					'"',
					'<',
					'>',
					'|',
					':',
					'*',
					'?',
					'\\',
					'/'
				};
			}
			return new char[]
			{
				'\0',
				'/'
			};
		}

		/// <summary>Gets an array containing the characters that are not allowed in path names.</summary>
		/// <returns>An array containing the characters that are not allowed in path names.</returns>
		// Token: 0x06006A2B RID: 27179 RVA: 0x0016AC9D File Offset: 0x00168E9D
		public static char[] GetInvalidPathChars()
		{
			if (Environment.IsRunningOnWindows)
			{
				return new char[]
				{
					'"',
					'<',
					'>',
					'|',
					'\0',
					'\u0001',
					'\u0002',
					'\u0003',
					'\u0004',
					'\u0005',
					'\u0006',
					'\a',
					'\b',
					'\t',
					'\n',
					'\v',
					'\f',
					'\r',
					'\u000e',
					'\u000f',
					'\u0010',
					'\u0011',
					'\u0012',
					'\u0013',
					'\u0014',
					'\u0015',
					'\u0016',
					'\u0017',
					'\u0018',
					'\u0019',
					'\u001a',
					'\u001b',
					'\u001c',
					'\u001d',
					'\u001e',
					'\u001f'
				};
			}
			return new char[1];
		}

		/// <summary>Returns a random folder name or file name.</summary>
		/// <returns>A random folder name or file name.</returns>
		// Token: 0x06006A2C RID: 27180 RVA: 0x0016ACC0 File Offset: 0x00168EC0
		public static string GetRandomFileName()
		{
			StringBuilder stringBuilder = new StringBuilder(12);
			RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
			byte[] array = new byte[11];
			randomNumberGenerator.GetBytes(array);
			for (int i = 0; i < array.Length; i++)
			{
				if (stringBuilder.Length == 8)
				{
					stringBuilder.Append('.');
				}
				int num = (int)(array[i] % 36);
				char value = (char)((num < 26) ? (num + 97) : (num - 26 + 48));
				stringBuilder.Append(value);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06006A2D RID: 27181 RVA: 0x0016AD34 File Offset: 0x00168F34
		private static int findExtension(string path)
		{
			if (path != null)
			{
				int num = path.LastIndexOf('.');
				int num2 = path.LastIndexOfAny(Path.PathSeparatorChars);
				if (num > num2)
				{
					return num;
				}
			}
			return -1;
		}

		// Token: 0x06006A2E RID: 27182 RVA: 0x0016AD60 File Offset: 0x00168F60
		static Path()
		{
			Path.VolumeSeparatorChar = MonoIO.VolumeSeparatorChar;
			Path.DirectorySeparatorChar = MonoIO.DirectorySeparatorChar;
			Path.AltDirectorySeparatorChar = MonoIO.AltDirectorySeparatorChar;
			Path.PathSeparator = MonoIO.PathSeparator;
			Path.InvalidPathChars = Path.GetInvalidPathChars();
			Path.DirectorySeparatorStr = Path.DirectorySeparatorChar.ToString();
			Path.PathSeparatorChars = new char[]
			{
				Path.DirectorySeparatorChar,
				Path.AltDirectorySeparatorChar,
				Path.VolumeSeparatorChar
			};
			Path.dirEqualsVolume = (Path.DirectorySeparatorChar == Path.VolumeSeparatorChar);
		}

		// Token: 0x06006A2F RID: 27183 RVA: 0x0016AE04 File Offset: 0x00169004
		private static string GetServerAndShare(string path)
		{
			int num = 2;
			while (num < path.Length && !Path.IsDirectorySeparator(path[num]))
			{
				num++;
			}
			if (num < path.Length)
			{
				num++;
				while (num < path.Length && !Path.IsDirectorySeparator(path[num]))
				{
					num++;
				}
			}
			return path.Substring(2, num - 2).Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
		}

		// Token: 0x06006A30 RID: 27184 RVA: 0x0016AE74 File Offset: 0x00169074
		private static bool SameRoot(string root, string path)
		{
			if (root.Length < 2 || path.Length < 2)
			{
				return false;
			}
			if (!Path.IsDirectorySeparator(root[0]) || !Path.IsDirectorySeparator(root[1]))
			{
				return root[0].Equals(path[0]) && path[1] == Path.VolumeSeparatorChar && (root.Length <= 2 || path.Length <= 2 || (Path.IsDirectorySeparator(root[2]) && Path.IsDirectorySeparator(path[2])));
			}
			if (!Path.IsDirectorySeparator(path[0]) || !Path.IsDirectorySeparator(path[1]))
			{
				return false;
			}
			string serverAndShare = Path.GetServerAndShare(root);
			string serverAndShare2 = Path.GetServerAndShare(path);
			return string.Compare(serverAndShare, serverAndShare2, true, CultureInfo.InvariantCulture) == 0;
		}

		// Token: 0x06006A31 RID: 27185 RVA: 0x0016AF48 File Offset: 0x00169148
		private static string CanonicalizePath(string path)
		{
			if (path == null)
			{
				return path;
			}
			if (Environment.IsRunningOnWindows)
			{
				path = path.Trim();
			}
			if (path.Length == 0)
			{
				return path;
			}
			string pathRoot = Path.GetPathRoot(path);
			string[] array = path.Split(new char[]
			{
				Path.DirectorySeparatorChar,
				Path.AltDirectorySeparatorChar
			});
			int num = 0;
			bool flag = Environment.IsRunningOnWindows && pathRoot.Length > 2 && Path.IsDirectorySeparator(pathRoot[0]) && Path.IsDirectorySeparator(pathRoot[1]);
			int num2 = flag ? 3 : 0;
			for (int i = 0; i < array.Length; i++)
			{
				if (Environment.IsRunningOnWindows)
				{
					array[i] = array[i].TrimEnd();
				}
				if (((flag && i == 2) || !(array[i] == ".")) && (i == 0 || array[i].Length != 0))
				{
					if (array[i] == "..")
					{
						if (num > num2)
						{
							num--;
						}
					}
					else
					{
						array[num++] = array[i];
					}
				}
			}
			if (num == 0 || (num == 1 && array[0] == ""))
			{
				return pathRoot;
			}
			string text = string.Join(Path.DirectorySeparatorStr, array, 0, num);
			if (!Environment.IsRunningOnWindows)
			{
				if (pathRoot != "" && text.Length > 0 && text[0] != '/')
				{
					text = pathRoot + text;
				}
				return text;
			}
			if (flag)
			{
				text = Path.DirectorySeparatorStr + text;
			}
			if (!Path.SameRoot(pathRoot, text))
			{
				text = pathRoot + text;
			}
			if (flag)
			{
				return text;
			}
			if (!Path.IsDirectorySeparator(path[0]) && Path.SameRoot(pathRoot, path))
			{
				if (text.Length <= 2 && !text.EndsWith(Path.DirectorySeparatorStr))
				{
					text += Path.DirectorySeparatorChar.ToString();
				}
				return text;
			}
			string currentDirectory = Directory.GetCurrentDirectory();
			if (currentDirectory.Length > 1 && currentDirectory[1] == Path.VolumeSeparatorChar)
			{
				if (text.Length == 0 || Path.IsDirectorySeparator(text[0]))
				{
					text += "\\";
				}
				return currentDirectory.Substring(0, 2) + text;
			}
			if (Path.IsDirectorySeparator(currentDirectory[currentDirectory.Length - 1]) && Path.IsDirectorySeparator(text[0]))
			{
				return currentDirectory + text.Substring(1);
			}
			return currentDirectory + text;
		}

		// Token: 0x06006A32 RID: 27186 RVA: 0x0016B1AC File Offset: 0x001693AC
		internal static bool IsPathSubsetOf(string subset, string path)
		{
			if (subset.Length > path.Length)
			{
				return false;
			}
			int num = subset.LastIndexOfAny(Path.PathSeparatorChars);
			if (string.Compare(subset, 0, path, 0, num) != 0)
			{
				return false;
			}
			num++;
			int num2 = path.IndexOfAny(Path.PathSeparatorChars, num);
			if (num2 >= num)
			{
				return string.Compare(subset, num, path, num, path.Length - num2) == 0;
			}
			return subset.Length == path.Length && string.Compare(subset, num, path, num, subset.Length - num) == 0;
		}

		/// <summary>Combines an array of strings into a path.</summary>
		/// <param name="paths">An array of parts of the path.</param>
		/// <returns>The combined paths.</returns>
		/// <exception cref="T:System.ArgumentException">One of the strings in the array contains one or more of the invalid characters defined in <see cref="M:System.IO.Path.GetInvalidPathChars" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">One of the strings in the array is <see langword="null" />.</exception>
		// Token: 0x06006A33 RID: 27187 RVA: 0x0016B234 File Offset: 0x00169434
		public static string Combine(params string[] paths)
		{
			if (paths == null)
			{
				throw new ArgumentNullException("paths");
			}
			StringBuilder stringBuilder = new StringBuilder();
			int num = paths.Length;
			bool flag = false;
			foreach (string text in paths)
			{
				if (text == null)
				{
					throw new ArgumentNullException("One of the paths contains a null value", "paths");
				}
				if (text.Length != 0)
				{
					if (text.IndexOfAny(Path.InvalidPathChars) != -1)
					{
						throw new ArgumentException("Illegal characters in path.");
					}
					if (flag)
					{
						flag = false;
						stringBuilder.Append(Path.DirectorySeparatorStr);
					}
					num--;
					if (Path.IsPathRooted(text))
					{
						stringBuilder.Length = 0;
					}
					stringBuilder.Append(text);
					int length = text.Length;
					if (length > 0 && num > 0)
					{
						char c = text[length - 1];
						if (c != Path.DirectorySeparatorChar && c != Path.AltDirectorySeparatorChar && c != Path.VolumeSeparatorChar)
						{
							flag = true;
						}
					}
				}
			}
			return stringBuilder.ToString();
		}

		/// <summary>Combines three strings into a path.</summary>
		/// <param name="path1">The first path to combine.</param>
		/// <param name="path2">The second path to combine.</param>
		/// <param name="path3">The third path to combine.</param>
		/// <returns>The combined paths.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path1" />, <paramref name="path2" />, or <paramref name="path3" /> contains one or more of the invalid characters defined in <see cref="M:System.IO.Path.GetInvalidPathChars" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path1" />, <paramref name="path2" />, or <paramref name="path3" /> is <see langword="null" />.</exception>
		// Token: 0x06006A34 RID: 27188 RVA: 0x0016B328 File Offset: 0x00169528
		public static string Combine(string path1, string path2, string path3)
		{
			if (path1 == null)
			{
				throw new ArgumentNullException("path1");
			}
			if (path2 == null)
			{
				throw new ArgumentNullException("path2");
			}
			if (path3 == null)
			{
				throw new ArgumentNullException("path3");
			}
			return Path.Combine(new string[]
			{
				path1,
				path2,
				path3
			});
		}

		/// <summary>Combines four strings into a path.</summary>
		/// <param name="path1">The first path to combine.</param>
		/// <param name="path2">The second path to combine.</param>
		/// <param name="path3">The third path to combine.</param>
		/// <param name="path4">The fourth path to combine.</param>
		/// <returns>The combined paths.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path1" />, <paramref name="path2" />, <paramref name="path3" />, or <paramref name="path4" /> contains one or more of the invalid characters defined in <see cref="M:System.IO.Path.GetInvalidPathChars" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path1" />, <paramref name="path2" />, <paramref name="path3" />, or <paramref name="path4" /> is <see langword="null" />.</exception>
		// Token: 0x06006A35 RID: 27189 RVA: 0x0016B378 File Offset: 0x00169578
		public static string Combine(string path1, string path2, string path3, string path4)
		{
			if (path1 == null)
			{
				throw new ArgumentNullException("path1");
			}
			if (path2 == null)
			{
				throw new ArgumentNullException("path2");
			}
			if (path3 == null)
			{
				throw new ArgumentNullException("path3");
			}
			if (path4 == null)
			{
				throw new ArgumentNullException("path4");
			}
			return Path.Combine(new string[]
			{
				path1,
				path2,
				path3,
				path4
			});
		}

		// Token: 0x06006A36 RID: 27190 RVA: 0x0016B3D8 File Offset: 0x001695D8
		internal static void Validate(string path)
		{
			Path.Validate(path, "path");
		}

		// Token: 0x06006A37 RID: 27191 RVA: 0x0016B3E8 File Offset: 0x001695E8
		internal static void Validate(string path, string parameterName)
		{
			if (path == null)
			{
				throw new ArgumentNullException(parameterName);
			}
			if (string.IsNullOrWhiteSpace(path))
			{
				throw new ArgumentException(Locale.GetText("Path is empty"));
			}
			if (path.IndexOfAny(Path.InvalidPathChars) != -1)
			{
				throw new ArgumentException(Locale.GetText("Path contains invalid chars"));
			}
			if (Environment.IsRunningOnWindows)
			{
				int num = path.IndexOf(':');
				if (num >= 0 && num != 1)
				{
					throw new ArgumentException(parameterName);
				}
			}
		}

		// Token: 0x17001247 RID: 4679
		// (get) Token: 0x06006A38 RID: 27192 RVA: 0x0016B454 File Offset: 0x00169654
		internal static string DirectorySeparatorCharAsString
		{
			get
			{
				return Path.DirectorySeparatorStr;
			}
		}

		// Token: 0x17001248 RID: 4680
		// (get) Token: 0x06006A39 RID: 27193 RVA: 0x0016B45B File Offset: 0x0016965B
		internal static char[] TrimEndChars
		{
			get
			{
				if (!Environment.IsRunningOnWindows)
				{
					return Path.trimEndCharsUnix;
				}
				return Path.trimEndCharsWindows;
			}
		}

		// Token: 0x06006A3A RID: 27194 RVA: 0x0016B470 File Offset: 0x00169670
		internal static void CheckSearchPattern(string searchPattern)
		{
			int num;
			while ((num = searchPattern.IndexOf("..", StringComparison.Ordinal)) != -1)
			{
				if (num + 2 == searchPattern.Length)
				{
					throw new ArgumentException(Environment.GetResourceString("Search pattern cannot contain \"..\" to move up directories and can be contained only internally in file/directory names, as in \"a..b\"."));
				}
				if (searchPattern[num + 2] == Path.DirectorySeparatorChar || searchPattern[num + 2] == Path.AltDirectorySeparatorChar)
				{
					throw new ArgumentException(Environment.GetResourceString("Search pattern cannot contain \"..\" to move up directories and can be contained only internally in file/directory names, as in \"a..b\"."));
				}
				searchPattern = searchPattern.Substring(num + 2);
			}
		}

		// Token: 0x06006A3B RID: 27195 RVA: 0x0016B4E6 File Offset: 0x001696E6
		internal static void CheckInvalidPathChars(string path, bool checkAdditional = false)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (PathInternal.HasIllegalCharacters(path, checkAdditional))
			{
				throw new ArgumentException(Environment.GetResourceString("Illegal characters in path."));
			}
		}

		// Token: 0x06006A3C RID: 27196 RVA: 0x0016B510 File Offset: 0x00169710
		internal static string InternalCombine(string path1, string path2)
		{
			if (path1 == null || path2 == null)
			{
				throw new ArgumentNullException((path1 == null) ? "path1" : "path2");
			}
			Path.CheckInvalidPathChars(path1, false);
			Path.CheckInvalidPathChars(path2, false);
			if (path2.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Path cannot be the empty string or all whitespace."), "path2");
			}
			if (Path.IsPathRooted(path2))
			{
				throw new ArgumentException(Environment.GetResourceString("Second path fragment must not be a drive or UNC name."), "path2");
			}
			int length = path1.Length;
			if (length == 0)
			{
				return path2;
			}
			char c = path1[length - 1];
			if (c != Path.DirectorySeparatorChar && c != Path.AltDirectorySeparatorChar && c != Path.VolumeSeparatorChar)
			{
				return path1 + Path.DirectorySeparatorCharAsString + path2;
			}
			return path1 + path2;
		}

		// Token: 0x06006A3D RID: 27197 RVA: 0x0016B5C4 File Offset: 0x001697C4
		public unsafe static ReadOnlySpan<char> GetFileName(ReadOnlySpan<char> path)
		{
			int length = Path.GetPathRoot(new string(path)).Length;
			int num = path.Length;
			while (--num >= 0)
			{
				if (num < length || Path.IsDirectorySeparator((char)(*path[num])))
				{
					return path.Slice(num + 1, path.Length - num - 1);
				}
			}
			return path;
		}

		// Token: 0x06006A3E RID: 27198 RVA: 0x0016B61F File Offset: 0x0016981F
		public static string Join(ReadOnlySpan<char> path1, ReadOnlySpan<char> path2)
		{
			if (path1.Length == 0)
			{
				return new string(path2);
			}
			if (path2.Length == 0)
			{
				return new string(path1);
			}
			return Path.JoinInternal(path1, path2);
		}

		// Token: 0x06006A3F RID: 27199 RVA: 0x0016B648 File Offset: 0x00169848
		public static string Join(ReadOnlySpan<char> path1, ReadOnlySpan<char> path2, ReadOnlySpan<char> path3)
		{
			if (path1.Length == 0)
			{
				return Path.Join(path2, path3);
			}
			if (path2.Length == 0)
			{
				return Path.Join(path1, path3);
			}
			if (path3.Length == 0)
			{
				return Path.Join(path1, path2);
			}
			return Path.JoinInternal(path1, path2, path3);
		}

		// Token: 0x06006A40 RID: 27200 RVA: 0x0016B688 File Offset: 0x00169888
		public unsafe static bool TryJoin(ReadOnlySpan<char> path1, ReadOnlySpan<char> path2, Span<char> destination, out int charsWritten)
		{
			charsWritten = 0;
			if (path1.Length == 0 && path2.Length == 0)
			{
				return true;
			}
			if (path1.Length == 0 || path2.Length == 0)
			{
				ref ReadOnlySpan<char> ptr = ref (path1.Length == 0) ? ref path2 : ref path1;
				if (destination.Length < ptr.Length)
				{
					return false;
				}
				ptr.CopyTo(destination);
				charsWritten = ptr.Length;
				return true;
			}
			else
			{
				bool flag = !PathInternal.EndsInDirectorySeparator(path1) && !PathInternal.StartsWithDirectorySeparator(path2);
				int num = path1.Length + path2.Length + (flag ? 1 : 0);
				if (destination.Length < num)
				{
					return false;
				}
				path1.CopyTo(destination);
				if (flag)
				{
					*destination[path1.Length] = Path.DirectorySeparatorChar;
				}
				path2.CopyTo(destination.Slice(path1.Length + (flag ? 1 : 0)));
				charsWritten = num;
				return true;
			}
		}

		// Token: 0x06006A41 RID: 27201 RVA: 0x0016B76C File Offset: 0x0016996C
		public unsafe static bool TryJoin(ReadOnlySpan<char> path1, ReadOnlySpan<char> path2, ReadOnlySpan<char> path3, Span<char> destination, out int charsWritten)
		{
			charsWritten = 0;
			if (path1.Length == 0 && path2.Length == 0 && path3.Length == 0)
			{
				return true;
			}
			if (path1.Length == 0)
			{
				return Path.TryJoin(path2, path3, destination, out charsWritten);
			}
			if (path2.Length == 0)
			{
				return Path.TryJoin(path1, path3, destination, out charsWritten);
			}
			if (path3.Length == 0)
			{
				return Path.TryJoin(path1, path2, destination, out charsWritten);
			}
			int num = (PathInternal.EndsInDirectorySeparator(path1) || PathInternal.StartsWithDirectorySeparator(path2)) ? 0 : 1;
			bool flag = !PathInternal.EndsInDirectorySeparator(path2) && !PathInternal.StartsWithDirectorySeparator(path3);
			if (flag)
			{
				num++;
			}
			int num2 = path1.Length + path2.Length + path3.Length + num;
			if (destination.Length < num2)
			{
				return false;
			}
			Path.TryJoin(path1, path2, destination, out charsWritten);
			if (flag)
			{
				int num3 = charsWritten;
				charsWritten = num3 + 1;
				*destination[num3] = Path.DirectorySeparatorChar;
			}
			path3.CopyTo(destination.Slice(charsWritten));
			charsWritten += path3.Length;
			return true;
		}

		// Token: 0x06006A42 RID: 27202 RVA: 0x0016B874 File Offset: 0x00169A74
		private unsafe static string JoinInternal(ReadOnlySpan<char> first, ReadOnlySpan<char> second)
		{
			bool flag = PathInternal.IsDirectorySeparator((char)(*first[first.Length - 1])) || PathInternal.IsDirectorySeparator((char)(*second[0]));
			fixed (char* reference = MemoryMarshal.GetReference<char>(first))
			{
				char* value = reference;
				fixed (char* reference2 = MemoryMarshal.GetReference<char>(second))
				{
					char* value2 = reference2;
					return string.Create<ValueTuple<IntPtr, int, IntPtr, int, bool>>(first.Length + second.Length + (flag ? 0 : 1), new ValueTuple<IntPtr, int, IntPtr, int, bool>((IntPtr)((void*)value), first.Length, (IntPtr)((void*)value2), second.Length, flag), delegate(Span<char> destination, [TupleElementNames(new string[]
					{
						"First",
						"FirstLength",
						"Second",
						"SecondLength",
						"HasSeparator"
					})] ValueTuple<IntPtr, int, IntPtr, int, bool> state)
					{
						Span<char> span = new Span<char>((void*)state.Item1, state.Item2);
						span.CopyTo(destination);
						if (!state.Item5)
						{
							*destination[state.Item2] = '\\';
						}
						span = new Span<char>((void*)state.Item3, state.Item4);
						span.CopyTo(destination.Slice(state.Item2 + (state.Item5 ? 0 : 1)));
					});
				}
			}
		}

		// Token: 0x06006A43 RID: 27203 RVA: 0x0016B91C File Offset: 0x00169B1C
		private unsafe static string JoinInternal(ReadOnlySpan<char> first, ReadOnlySpan<char> second, ReadOnlySpan<char> third)
		{
			bool flag = PathInternal.IsDirectorySeparator((char)(*first[first.Length - 1])) || PathInternal.IsDirectorySeparator((char)(*second[0]));
			bool flag2 = PathInternal.IsDirectorySeparator((char)(*second[second.Length - 1])) || PathInternal.IsDirectorySeparator((char)(*third[0]));
			fixed (char* reference = MemoryMarshal.GetReference<char>(first))
			{
				char* value = reference;
				fixed (char* reference2 = MemoryMarshal.GetReference<char>(second))
				{
					char* value2 = reference2;
					fixed (char* reference3 = MemoryMarshal.GetReference<char>(third))
					{
						char* value3 = reference3;
						return string.Create<ValueTuple<IntPtr, int, IntPtr, int, IntPtr, int, bool, ValueTuple<bool>>>(first.Length + second.Length + third.Length + (flag ? 0 : 1) + (flag2 ? 0 : 1), new ValueTuple<IntPtr, int, IntPtr, int, IntPtr, int, bool, ValueTuple<bool>>((IntPtr)((void*)value), first.Length, (IntPtr)((void*)value2), second.Length, (IntPtr)((void*)value3), third.Length, flag, new ValueTuple<bool>(flag2)), delegate(Span<char> destination, [TupleElementNames(new string[]
						{
							"First",
							"FirstLength",
							"Second",
							"SecondLength",
							"Third",
							"ThirdLength",
							"FirstHasSeparator",
							"ThirdHasSeparator",
							null
						})] ValueTuple<IntPtr, int, IntPtr, int, IntPtr, int, bool, ValueTuple<bool>> state)
						{
							Span<char> span = new Span<char>((void*)state.Item1, state.Item2);
							span.CopyTo(destination);
							if (!state.Item7)
							{
								*destination[state.Item2] = '\\';
							}
							span = new Span<char>((void*)state.Item3, state.Item4);
							span.CopyTo(destination.Slice(state.Item2 + (state.Item7 ? 0 : 1)));
							if (!state.Rest.Item1)
							{
								*destination[destination.Length - state.Item6 - 1] = '\\';
							}
							span = new Span<char>((void*)state.Item5, state.Item6);
							span.CopyTo(destination.Slice(destination.Length - state.Item6));
						});
					}
				}
			}
		}

		// Token: 0x06006A44 RID: 27204 RVA: 0x0016BA24 File Offset: 0x00169C24
		private unsafe static string JoinInternal(ReadOnlySpan<char> first, ReadOnlySpan<char> second, ReadOnlySpan<char> third, ReadOnlySpan<char> fourth)
		{
			bool flag = PathInternal.IsDirectorySeparator((char)(*first[first.Length - 1])) || PathInternal.IsDirectorySeparator((char)(*second[0]));
			bool flag2 = PathInternal.IsDirectorySeparator((char)(*second[second.Length - 1])) || PathInternal.IsDirectorySeparator((char)(*third[0]));
			bool flag3 = PathInternal.IsDirectorySeparator((char)(*third[third.Length - 1])) || PathInternal.IsDirectorySeparator((char)(*fourth[0]));
			fixed (char* reference = MemoryMarshal.GetReference<char>(first))
			{
				char* value = reference;
				fixed (char* reference2 = MemoryMarshal.GetReference<char>(second))
				{
					char* value2 = reference2;
					fixed (char* reference3 = MemoryMarshal.GetReference<char>(third))
					{
						char* value3 = reference3;
						fixed (char* reference4 = MemoryMarshal.GetReference<char>(fourth))
						{
							char* value4 = reference4;
							return string.Create<ValueTuple<IntPtr, int, IntPtr, int, IntPtr, int, IntPtr, ValueTuple<int, bool, bool, bool>>>(first.Length + second.Length + third.Length + fourth.Length + (flag ? 0 : 1) + (flag2 ? 0 : 1) + (flag3 ? 0 : 1), new ValueTuple<IntPtr, int, IntPtr, int, IntPtr, int, IntPtr, ValueTuple<int, bool, bool, bool>>((IntPtr)((void*)value), first.Length, (IntPtr)((void*)value2), second.Length, (IntPtr)((void*)value3), third.Length, (IntPtr)((void*)value4), new ValueTuple<int, bool, bool, bool>(fourth.Length, flag, flag2, flag3)), delegate(Span<char> destination, [TupleElementNames(new string[]
							{
								"First",
								"FirstLength",
								"Second",
								"SecondLength",
								"Third",
								"ThirdLength",
								"Fourth",
								"FourthLength",
								"FirstHasSeparator",
								"ThirdHasSeparator",
								"FourthHasSeparator",
								null,
								null,
								null,
								null
							})] ValueTuple<IntPtr, int, IntPtr, int, IntPtr, int, IntPtr, ValueTuple<int, bool, bool, bool>> state)
							{
								Span<char> span = new Span<char>((void*)state.Item1, state.Item2);
								span.CopyTo(destination);
								if (!state.Rest.Item2)
								{
									*destination[state.Item2] = '\\';
								}
								span = new Span<char>((void*)state.Item3, state.Item4);
								span.CopyTo(destination.Slice(state.Item2 + (state.Rest.Item2 ? 0 : 1)));
								if (!state.Rest.Item3)
								{
									*destination[state.Item2 + state.Item4 + (state.Rest.Item2 ? 0 : 1)] = '\\';
								}
								span = new Span<char>((void*)state.Item5, state.Item6);
								span.CopyTo(destination.Slice(state.Item2 + state.Item4 + (state.Rest.Item2 ? 0 : 1) + (state.Rest.Item3 ? 0 : 1)));
								if (!state.Rest.Item4)
								{
									*destination[destination.Length - state.Rest.Item1 - 1] = '\\';
								}
								span = new Span<char>((void*)state.Item7, state.Rest.Item1);
								span.CopyTo(destination.Slice(destination.Length - state.Rest.Item1));
							});
						}
					}
				}
			}
		}

		// Token: 0x06006A45 RID: 27205 RVA: 0x0016BB81 File Offset: 0x00169D81
		public static ReadOnlySpan<char> GetExtension(ReadOnlySpan<char> path)
		{
			return Path.GetExtension(path.ToString()).AsSpan();
		}

		// Token: 0x06006A46 RID: 27206 RVA: 0x0016BB9A File Offset: 0x00169D9A
		public static ReadOnlySpan<char> GetFileNameWithoutExtension(ReadOnlySpan<char> path)
		{
			return Path.GetFileNameWithoutExtension(path.ToString()).AsSpan();
		}

		// Token: 0x06006A47 RID: 27207 RVA: 0x0016BBB3 File Offset: 0x00169DB3
		public static ReadOnlySpan<char> GetPathRoot(ReadOnlySpan<char> path)
		{
			return Path.GetPathRoot(path.ToString()).AsSpan();
		}

		// Token: 0x06006A48 RID: 27208 RVA: 0x0016BBCC File Offset: 0x00169DCC
		public static bool HasExtension(ReadOnlySpan<char> path)
		{
			return Path.HasExtension(path.ToString());
		}

		// Token: 0x06006A49 RID: 27209 RVA: 0x0016BBE0 File Offset: 0x00169DE0
		public static string GetRelativePath(string relativeTo, string path)
		{
			return Path.GetRelativePath(relativeTo, path, Path.StringComparison);
		}

		// Token: 0x06006A4A RID: 27210 RVA: 0x0016BBF0 File Offset: 0x00169DF0
		private static string GetRelativePath(string relativeTo, string path, StringComparison comparisonType)
		{
			if (string.IsNullOrEmpty(relativeTo))
			{
				throw new ArgumentNullException("relativeTo");
			}
			if (PathInternal.IsEffectivelyEmpty(path.AsSpan()))
			{
				throw new ArgumentNullException("path");
			}
			relativeTo = Path.GetFullPath(relativeTo);
			path = Path.GetFullPath(path);
			if (!PathInternal.AreRootsEqual(relativeTo, path, comparisonType))
			{
				return path;
			}
			int num = PathInternal.GetCommonPathLength(relativeTo, path, comparisonType == StringComparison.OrdinalIgnoreCase);
			if (num == 0)
			{
				return path;
			}
			int num2 = relativeTo.Length;
			if (PathInternal.EndsInDirectorySeparator(relativeTo.AsSpan()))
			{
				num2--;
			}
			bool flag = PathInternal.EndsInDirectorySeparator(path.AsSpan());
			int num3 = path.Length;
			if (flag)
			{
				num3--;
			}
			if (num2 == num3 && num >= num2)
			{
				return ".";
			}
			StringBuilder stringBuilder = StringBuilderCache.Acquire(Math.Max(relativeTo.Length, path.Length));
			if (num < num2)
			{
				stringBuilder.Append("..");
				for (int i = num + 1; i < num2; i++)
				{
					if (PathInternal.IsDirectorySeparator(relativeTo[i]))
					{
						stringBuilder.Append(Path.DirectorySeparatorChar);
						stringBuilder.Append("..");
					}
				}
			}
			else if (PathInternal.IsDirectorySeparator(path[num]))
			{
				num++;
			}
			int num4 = num3 - num;
			if (flag)
			{
				num4++;
			}
			if (num4 > 0)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(Path.DirectorySeparatorChar);
				}
				stringBuilder.Append(path, num, num4);
			}
			return StringBuilderCache.GetStringAndRelease(stringBuilder);
		}

		// Token: 0x17001249 RID: 4681
		// (get) Token: 0x06006A4B RID: 27211 RVA: 0x0016BD4A File Offset: 0x00169F4A
		internal static StringComparison StringComparison
		{
			get
			{
				if (!Path.IsCaseSensitive)
				{
					return StringComparison.OrdinalIgnoreCase;
				}
				return StringComparison.Ordinal;
			}
		}

		// Token: 0x1700124A RID: 4682
		// (get) Token: 0x06006A4C RID: 27212 RVA: 0x0016BD56 File Offset: 0x00169F56
		internal static bool IsCaseSensitive
		{
			get
			{
				return !Path.IsWindows;
			}
		}

		// Token: 0x1700124B RID: 4683
		// (get) Token: 0x06006A4D RID: 27213 RVA: 0x0016BD60 File Offset: 0x00169F60
		private static bool IsWindows
		{
			get
			{
				PlatformID platform = Environment.OSVersion.Platform;
				return platform == PlatformID.Win32S || platform == PlatformID.Win32Windows || platform == PlatformID.Win32NT || platform == PlatformID.WinCE;
			}
		}

		// Token: 0x06006A4E RID: 27214 RVA: 0x0016BD8A File Offset: 0x00169F8A
		public static bool IsPathFullyQualified(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			return Path.IsPathFullyQualified(path.AsSpan());
		}

		// Token: 0x06006A4F RID: 27215 RVA: 0x0016BDA5 File Offset: 0x00169FA5
		public static bool IsPathFullyQualified(ReadOnlySpan<char> path)
		{
			return !PathInternal.IsPartiallyQualified(path);
		}

		// Token: 0x06006A50 RID: 27216 RVA: 0x0016BDB0 File Offset: 0x00169FB0
		public static string GetFullPath(string path, string basePath)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (basePath == null)
			{
				throw new ArgumentNullException("basePath");
			}
			if (!Path.IsPathFullyQualified(basePath))
			{
				throw new ArgumentException("Basepath argument is not fully qualified.", "basePath");
			}
			if (basePath.Contains('\0') || path.Contains('\0'))
			{
				throw new ArgumentException("Illegal characters in path '{0}'.");
			}
			if (Path.IsPathFullyQualified(path))
			{
				return Path.GetFullPath(path);
			}
			return Path.GetFullPath(Path.CombineInternal(basePath, path));
		}

		// Token: 0x06006A51 RID: 27217 RVA: 0x0016BE29 File Offset: 0x0016A029
		private static string CombineInternal(string first, string second)
		{
			if (string.IsNullOrEmpty(first))
			{
				return second;
			}
			if (string.IsNullOrEmpty(second))
			{
				return first;
			}
			if (Path.IsPathRooted(second.AsSpan()))
			{
				return second;
			}
			return Path.JoinInternal(first.AsSpan(), second.AsSpan());
		}

		/// <summary>Provides a platform-specific array of characters that cannot be specified in path string arguments passed to members of the <see cref="T:System.IO.Path" /> class.</summary>
		// Token: 0x04003D79 RID: 15737
		[Obsolete("see GetInvalidPathChars and GetInvalidFileNameChars methods.")]
		public static readonly char[] InvalidPathChars;

		/// <summary>Provides a platform-specific alternate character used to separate directory levels in a path string that reflects a hierarchical file system organization.</summary>
		// Token: 0x04003D7A RID: 15738
		public static readonly char AltDirectorySeparatorChar;

		/// <summary>Provides a platform-specific character used to separate directory levels in a path string that reflects a hierarchical file system organization.</summary>
		// Token: 0x04003D7B RID: 15739
		public static readonly char DirectorySeparatorChar;

		/// <summary>A platform-specific separator character used to separate path strings in environment variables.</summary>
		// Token: 0x04003D7C RID: 15740
		public static readonly char PathSeparator;

		// Token: 0x04003D7D RID: 15741
		internal static readonly string DirectorySeparatorStr;

		/// <summary>Provides a platform-specific volume separator character.</summary>
		// Token: 0x04003D7E RID: 15742
		public static readonly char VolumeSeparatorChar;

		// Token: 0x04003D7F RID: 15743
		internal static readonly char[] PathSeparatorChars;

		// Token: 0x04003D80 RID: 15744
		private static readonly bool dirEqualsVolume;

		// Token: 0x04003D81 RID: 15745
		internal const int MAX_PATH = 260;

		// Token: 0x04003D82 RID: 15746
		internal static readonly char[] trimEndCharsWindows = new char[]
		{
			'\t',
			'\n',
			'\v',
			'\f',
			'\r',
			' ',
			'\u0085',
			'\u00a0'
		};

		// Token: 0x04003D83 RID: 15747
		internal static readonly char[] trimEndCharsUnix = new char[0];

		// Token: 0x02000B68 RID: 2920
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06006A52 RID: 27218 RVA: 0x0016BE5F File Offset: 0x0016A05F
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06006A53 RID: 27219 RVA: 0x0000259F File Offset: 0x0000079F
			public <>c()
			{
			}

			// Token: 0x06006A54 RID: 27220 RVA: 0x0016BE6C File Offset: 0x0016A06C
			internal unsafe void <JoinInternal>b__59_0(Span<char> destination, [TupleElementNames(new string[]
			{
				"First",
				"FirstLength",
				"Second",
				"SecondLength",
				"HasSeparator"
			})] ValueTuple<IntPtr, int, IntPtr, int, bool> state)
			{
				Span<char> span = new Span<char>((void*)state.Item1, state.Item2);
				span.CopyTo(destination);
				if (!state.Item5)
				{
					*destination[state.Item2] = '\\';
				}
				span = new Span<char>((void*)state.Item3, state.Item4);
				span.CopyTo(destination.Slice(state.Item2 + (state.Item5 ? 0 : 1)));
			}

			// Token: 0x06006A55 RID: 27221 RVA: 0x0016BEE8 File Offset: 0x0016A0E8
			internal unsafe void <JoinInternal>b__60_0(Span<char> destination, [TupleElementNames(new string[]
			{
				"First",
				"FirstLength",
				"Second",
				"SecondLength",
				"Third",
				"ThirdLength",
				"FirstHasSeparator",
				"ThirdHasSeparator",
				null
			})] ValueTuple<IntPtr, int, IntPtr, int, IntPtr, int, bool, ValueTuple<bool>> state)
			{
				Span<char> span = new Span<char>((void*)state.Item1, state.Item2);
				span.CopyTo(destination);
				if (!state.Item7)
				{
					*destination[state.Item2] = '\\';
				}
				span = new Span<char>((void*)state.Item3, state.Item4);
				span.CopyTo(destination.Slice(state.Item2 + (state.Item7 ? 0 : 1)));
				if (!state.Rest.Item1)
				{
					*destination[destination.Length - state.Item6 - 1] = '\\';
				}
				span = new Span<char>((void*)state.Item5, state.Item6);
				span.CopyTo(destination.Slice(destination.Length - state.Item6));
			}

			// Token: 0x06006A56 RID: 27222 RVA: 0x0016BFC0 File Offset: 0x0016A1C0
			internal unsafe void <JoinInternal>b__61_0(Span<char> destination, [TupleElementNames(new string[]
			{
				"First",
				"FirstLength",
				"Second",
				"SecondLength",
				"Third",
				"ThirdLength",
				"Fourth",
				"FourthLength",
				"FirstHasSeparator",
				"ThirdHasSeparator",
				"FourthHasSeparator",
				null,
				null,
				null,
				null
			})] ValueTuple<IntPtr, int, IntPtr, int, IntPtr, int, IntPtr, ValueTuple<int, bool, bool, bool>> state)
			{
				Span<char> span = new Span<char>((void*)state.Item1, state.Item2);
				span.CopyTo(destination);
				if (!state.Rest.Item2)
				{
					*destination[state.Item2] = '\\';
				}
				span = new Span<char>((void*)state.Item3, state.Item4);
				span.CopyTo(destination.Slice(state.Item2 + (state.Rest.Item2 ? 0 : 1)));
				if (!state.Rest.Item3)
				{
					*destination[state.Item2 + state.Item4 + (state.Rest.Item2 ? 0 : 1)] = '\\';
				}
				span = new Span<char>((void*)state.Item5, state.Item6);
				span.CopyTo(destination.Slice(state.Item2 + state.Item4 + (state.Rest.Item2 ? 0 : 1) + (state.Rest.Item3 ? 0 : 1)));
				if (!state.Rest.Item4)
				{
					*destination[destination.Length - state.Rest.Item1 - 1] = '\\';
				}
				span = new Span<char>((void*)state.Item7, state.Rest.Item1);
				span.CopyTo(destination.Slice(destination.Length - state.Rest.Item1));
			}

			// Token: 0x04003D84 RID: 15748
			public static readonly Path.<>c <>9 = new Path.<>c();

			// Token: 0x04003D85 RID: 15749
			[TupleElementNames(new string[]
			{
				"First",
				"FirstLength",
				"Second",
				"SecondLength",
				"HasSeparator"
			})]
			public static SpanAction<char, ValueTuple<IntPtr, int, IntPtr, int, bool>> <>9__59_0;

			// Token: 0x04003D86 RID: 15750
			[TupleElementNames(new string[]
			{
				"First",
				"FirstLength",
				"Second",
				"SecondLength",
				"Third",
				"ThirdLength",
				"FirstHasSeparator",
				"ThirdHasSeparator",
				null
			})]
			public static SpanAction<char, ValueTuple<IntPtr, int, IntPtr, int, IntPtr, int, bool, ValueTuple<bool>>> <>9__60_0;

			// Token: 0x04003D87 RID: 15751
			[TupleElementNames(new string[]
			{
				"First",
				"FirstLength",
				"Second",
				"SecondLength",
				"Third",
				"ThirdLength",
				"Fourth",
				"FourthLength",
				"FirstHasSeparator",
				"ThirdHasSeparator",
				"FourthHasSeparator",
				null,
				null,
				null,
				null
			})]
			public static SpanAction<char, ValueTuple<IntPtr, int, IntPtr, int, IntPtr, int, IntPtr, ValueTuple<int, bool, bool, bool>>> <>9__61_0;
		}
	}
}
