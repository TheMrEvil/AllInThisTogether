using System;

namespace System.IO
{
	/// <summary>Provides data for the directory events: <see cref="E:System.IO.FileSystemWatcher.Changed" />, <see cref="E:System.IO.FileSystemWatcher.Created" />, <see cref="E:System.IO.FileSystemWatcher.Deleted" />.</summary>
	// Token: 0x020004F2 RID: 1266
	public class FileSystemEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileSystemEventArgs" /> class.</summary>
		/// <param name="changeType">One of the <see cref="T:System.IO.WatcherChangeTypes" /> values, which represents the kind of change detected in the file system.</param>
		/// <param name="directory">The root directory of the affected file or directory.</param>
		/// <param name="name">The name of the affected file or directory.</param>
		// Token: 0x06002966 RID: 10598 RVA: 0x0008E8E4 File Offset: 0x0008CAE4
		public FileSystemEventArgs(WatcherChangeTypes changeType, string directory, string name)
		{
			this._changeType = changeType;
			this._name = name;
			this._fullPath = Path.GetFullPath(FileSystemEventArgs.Combine(directory, name));
		}

		// Token: 0x06002967 RID: 10599 RVA: 0x0008E90C File Offset: 0x0008CB0C
		internal static string Combine(string directoryPath, string name)
		{
			bool flag = false;
			if (directoryPath.Length > 0)
			{
				char c = directoryPath[directoryPath.Length - 1];
				flag = (c == Path.DirectorySeparatorChar || c == Path.AltDirectorySeparatorChar);
			}
			if (!flag)
			{
				return directoryPath + Path.DirectorySeparatorChar.ToString() + name;
			}
			return directoryPath + name;
		}

		/// <summary>Gets the type of directory event that occurred.</summary>
		/// <returns>One of the <see cref="T:System.IO.WatcherChangeTypes" /> values that represents the kind of change detected in the file system.</returns>
		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x06002968 RID: 10600 RVA: 0x0008E963 File Offset: 0x0008CB63
		public WatcherChangeTypes ChangeType
		{
			get
			{
				return this._changeType;
			}
		}

		/// <summary>Gets the fully qualifed path of the affected file or directory.</summary>
		/// <returns>The path of the affected file or directory.</returns>
		// Token: 0x17000886 RID: 2182
		// (get) Token: 0x06002969 RID: 10601 RVA: 0x0008E96B File Offset: 0x0008CB6B
		public string FullPath
		{
			get
			{
				return this._fullPath;
			}
		}

		/// <summary>Gets the name of the affected file or directory.</summary>
		/// <returns>The name of the affected file or directory.</returns>
		// Token: 0x17000887 RID: 2183
		// (get) Token: 0x0600296A RID: 10602 RVA: 0x0008E973 File Offset: 0x0008CB73
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x040015E3 RID: 5603
		private readonly WatcherChangeTypes _changeType;

		// Token: 0x040015E4 RID: 5604
		private readonly string _name;

		// Token: 0x040015E5 RID: 5605
		private readonly string _fullPath;
	}
}
