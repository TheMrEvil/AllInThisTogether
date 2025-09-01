using System;

namespace UnityEngine
{
	// Token: 0x02000224 RID: 548
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
	public class UnityAPICompatibilityVersionAttribute : Attribute
	{
		// Token: 0x06001798 RID: 6040 RVA: 0x00026362 File Offset: 0x00024562
		[Obsolete("This overload of the attribute has been deprecated. Use the constructor that takes the version and a boolean", true)]
		public UnityAPICompatibilityVersionAttribute(string version)
		{
			this._version = version;
		}

		// Token: 0x06001799 RID: 6041 RVA: 0x00026374 File Offset: 0x00024574
		public UnityAPICompatibilityVersionAttribute(string version, bool checkOnlyUnityVersion)
		{
			bool flag = !checkOnlyUnityVersion;
			if (flag)
			{
				throw new ArgumentException("You must pass 'true' to checkOnlyUnityVersion parameter.");
			}
			this._version = version;
		}

		// Token: 0x0600179A RID: 6042 RVA: 0x000263A3 File Offset: 0x000245A3
		public UnityAPICompatibilityVersionAttribute(string version, string[] configurationAssembliesHashes)
		{
			this._version = version;
			this._configurationAssembliesHashes = configurationAssembliesHashes;
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x0600179B RID: 6043 RVA: 0x000263BC File Offset: 0x000245BC
		public string version
		{
			get
			{
				return this._version;
			}
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x0600179C RID: 6044 RVA: 0x000263D4 File Offset: 0x000245D4
		internal string[] configurationAssembliesHashes
		{
			get
			{
				return this._configurationAssembliesHashes;
			}
		}

		// Token: 0x04000817 RID: 2071
		private string _version;

		// Token: 0x04000818 RID: 2072
		private string[] _configurationAssembliesHashes;
	}
}
