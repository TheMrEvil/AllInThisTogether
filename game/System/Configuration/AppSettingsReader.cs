using System;
using System.Collections.Specialized;
using System.Reflection;

namespace System.Configuration
{
	/// <summary>Provides a method for reading values of a particular type from the configuration.</summary>
	// Token: 0x02000197 RID: 407
	public class AppSettingsReader
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.AppSettingsReader" /> class.</summary>
		// Token: 0x06000AA8 RID: 2728 RVA: 0x0002DC3A File Offset: 0x0002BE3A
		public AppSettingsReader()
		{
			this.appSettings = ConfigurationSettings.AppSettings;
		}

		/// <summary>Gets the value for a specified key from the <see cref="P:System.Configuration.ConfigurationSettings.AppSettings" /> property and returns an object of the specified type containing the value from the configuration.</summary>
		/// <param name="key">The key for which to get the value.</param>
		/// <param name="type">The type of the object to return.</param>
		/// <returns>The value of the specified key.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.  
		/// -or-
		///  <paramref name="type" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="key" /> does not exist in the <see langword="&lt;appSettings&gt;" /> configuration section.  
		/// -or-
		///  The value in the <see langword="&lt;appSettings&gt;" /> configuration section for <paramref name="key" /> is not of type <paramref name="type" />.</exception>
		// Token: 0x06000AA9 RID: 2729 RVA: 0x0002DC50 File Offset: 0x0002BE50
		public object GetValue(string key, Type type)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			string text = this.appSettings[key];
			if (text == null)
			{
				throw new InvalidOperationException("'" + key + "' could not be found.");
			}
			if (type == typeof(string))
			{
				return text;
			}
			MethodInfo method = type.GetMethod("Parse", new Type[]
			{
				typeof(string)
			});
			if (method == null)
			{
				throw new InvalidOperationException("Type " + ((type != null) ? type.ToString() : null) + " does not have a Parse method");
			}
			object result = null;
			try
			{
				result = method.Invoke(null, new object[]
				{
					text
				});
			}
			catch (Exception innerException)
			{
				throw new InvalidOperationException("Parse error.", innerException);
			}
			return result;
		}

		// Token: 0x04000722 RID: 1826
		private NameValueCollection appSettings;
	}
}
