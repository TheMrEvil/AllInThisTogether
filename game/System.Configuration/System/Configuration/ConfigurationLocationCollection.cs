using System;
using System.Collections;

namespace System.Configuration
{
	/// <summary>Contains a collection of <see cref="T:System.Configuration.ConfigurationLocationCollection" /> objects.</summary>
	// Token: 0x02000023 RID: 35
	public class ConfigurationLocationCollection : ReadOnlyCollectionBase
	{
		// Token: 0x06000132 RID: 306 RVA: 0x0000595B File Offset: 0x00003B5B
		internal ConfigurationLocationCollection()
		{
		}

		/// <summary>Gets the <see cref="T:System.Configuration.ConfigurationLocationCollection" /> object at the specified index.</summary>
		/// <param name="index">The index location of the <see cref="T:System.Configuration.ConfigurationLocationCollection" /> to return.</param>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationLocationCollection" /> at the specified index.</returns>
		// Token: 0x17000054 RID: 84
		public ConfigurationLocation this[int index]
		{
			get
			{
				return base.InnerList[index] as ConfigurationLocation;
			}
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00005976 File Offset: 0x00003B76
		internal void Add(ConfigurationLocation loc)
		{
			base.InnerList.Add(loc);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00005988 File Offset: 0x00003B88
		internal ConfigurationLocation Find(string location)
		{
			foreach (object obj in base.InnerList)
			{
				ConfigurationLocation configurationLocation = (ConfigurationLocation)obj;
				if (string.Compare(configurationLocation.Path, location, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return configurationLocation;
				}
			}
			return null;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x000059F0 File Offset: 0x00003BF0
		internal ConfigurationLocation FindBest(string location)
		{
			if (string.IsNullOrEmpty(location))
			{
				return null;
			}
			ConfigurationLocation configurationLocation = null;
			int length = location.Length;
			int num = 0;
			foreach (object obj in base.InnerList)
			{
				ConfigurationLocation configurationLocation2 = (ConfigurationLocation)obj;
				string path = configurationLocation2.Path;
				if (!string.IsNullOrEmpty(path))
				{
					int length2 = path.Length;
					if (location.StartsWith(path, StringComparison.OrdinalIgnoreCase))
					{
						if (length == length2)
						{
							return configurationLocation2;
						}
						if (length <= length2 || location[length2] == '/')
						{
							if (configurationLocation == null)
							{
								configurationLocation = configurationLocation2;
							}
							else if (num < length2)
							{
								configurationLocation = configurationLocation2;
								num = length2;
							}
						}
					}
				}
			}
			return configurationLocation;
		}
	}
}
