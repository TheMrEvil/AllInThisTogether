using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Common;

namespace System.Data.Odbc
{
	/// <summary>Provides a simple way to create and manage the contents of connection strings used by the <see cref="T:System.Data.Odbc.OdbcConnection" /> class.</summary>
	// Token: 0x020002DE RID: 734
	public sealed class OdbcConnectionStringBuilder : DbConnectionStringBuilder
	{
		// Token: 0x0600205F RID: 8287 RVA: 0x00096CF0 File Offset: 0x00094EF0
		static OdbcConnectionStringBuilder()
		{
			string[] array = new string[]
			{
				null,
				"Driver"
			};
			array[0] = "Dsn";
			OdbcConnectionStringBuilder.s_validKeywords = array;
			OdbcConnectionStringBuilder.s_keywords = new Dictionary<string, OdbcConnectionStringBuilder.Keywords>(2, StringComparer.OrdinalIgnoreCase)
			{
				{
					"Driver",
					OdbcConnectionStringBuilder.Keywords.Driver
				},
				{
					"Dsn",
					OdbcConnectionStringBuilder.Keywords.Dsn
				}
			};
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcConnectionStringBuilder" /> class.</summary>
		// Token: 0x06002060 RID: 8288 RVA: 0x00096D40 File Offset: 0x00094F40
		public OdbcConnectionStringBuilder() : this(null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcConnectionStringBuilder" /> class. The provided connection string provides the data for the instance's internal connection information.</summary>
		/// <param name="connectionString">The basis for the object's internal connection information. Parsed into key/value pairs.</param>
		/// <exception cref="T:System.ArgumentException">The connection string is incorrectly formatted (perhaps missing the required "=" within a key/value pair).</exception>
		// Token: 0x06002061 RID: 8289 RVA: 0x00096D49 File Offset: 0x00094F49
		public OdbcConnectionStringBuilder(string connectionString) : base(true)
		{
			if (!string.IsNullOrEmpty(connectionString))
			{
				base.ConnectionString = connectionString;
			}
		}

		/// <summary>Gets or sets the value associated with the specified key. In C#, this property is the indexer.</summary>
		/// <param name="keyword">The key of the item to get or set.</param>
		/// <returns>The value associated with the specified key.</returns>
		/// <exception cref="T:System.ArgumentException">The connection string is incorrectly formatted (perhaps missing the required "=" within a key/value pair).</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="keyword" /> is a null reference (<see langword="Nothing" /> in Visual Basic).</exception>
		// Token: 0x170005C5 RID: 1477
		public override object this[string keyword]
		{
			get
			{
				ADP.CheckArgumentNull(keyword, "keyword");
				OdbcConnectionStringBuilder.Keywords index;
				if (OdbcConnectionStringBuilder.s_keywords.TryGetValue(keyword, out index))
				{
					return this.GetAt(index);
				}
				return base[keyword];
			}
			set
			{
				ADP.CheckArgumentNull(keyword, "keyword");
				if (value == null)
				{
					this.Remove(keyword);
					return;
				}
				OdbcConnectionStringBuilder.Keywords keywords;
				if (!OdbcConnectionStringBuilder.s_keywords.TryGetValue(keyword, out keywords))
				{
					base[keyword] = value;
					base.ClearPropertyDescriptors();
					this._knownKeywords = null;
					return;
				}
				if (keywords == OdbcConnectionStringBuilder.Keywords.Dsn)
				{
					this.Dsn = OdbcConnectionStringBuilder.ConvertToString(value);
					return;
				}
				if (keywords == OdbcConnectionStringBuilder.Keywords.Driver)
				{
					this.Driver = OdbcConnectionStringBuilder.ConvertToString(value);
					return;
				}
				throw ADP.KeywordNotSupported(keyword);
			}
		}

		/// <summary>Gets or sets the name of the ODBC driver associated with the connection.</summary>
		/// <returns>The value of the <see cref="P:System.Data.Odbc.OdbcConnectionStringBuilder.Driver" /> property, or <see langword="String.Empty" /> if none has been supplied.</returns>
		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x06002064 RID: 8292 RVA: 0x00096E20 File Offset: 0x00095020
		// (set) Token: 0x06002065 RID: 8293 RVA: 0x00096E28 File Offset: 0x00095028
		[DisplayName("Driver")]
		public string Driver
		{
			get
			{
				return this._driver;
			}
			set
			{
				this.SetValue("Driver", value);
				this._driver = value;
			}
		}

		/// <summary>Gets or sets the name of the data source name (DSN) associated with the connection.</summary>
		/// <returns>The value of the <see cref="P:System.Data.Odbc.OdbcConnectionStringBuilder.Dsn" /> property, or <see langword="String.Empty" /> if none has been supplied.</returns>
		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x06002066 RID: 8294 RVA: 0x00096E3D File Offset: 0x0009503D
		// (set) Token: 0x06002067 RID: 8295 RVA: 0x00096E45 File Offset: 0x00095045
		[DisplayName("Dsn")]
		public string Dsn
		{
			get
			{
				return this._dsn;
			}
			set
			{
				this.SetValue("Dsn", value);
				this._dsn = value;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> that contains the keys in the <see cref="T:System.Data.Odbc.OdbcConnectionStringBuilder" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> that contains the keys in the <see cref="T:System.Data.Odbc.OdbcConnectionStringBuilder" />.</returns>
		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x06002068 RID: 8296 RVA: 0x00096E5C File Offset: 0x0009505C
		public override ICollection Keys
		{
			get
			{
				string[] array = this._knownKeywords;
				if (array == null)
				{
					array = OdbcConnectionStringBuilder.s_validKeywords;
					int num = 0;
					foreach (object obj in base.Keys)
					{
						string b = (string)obj;
						bool flag = true;
						string[] array2 = array;
						for (int i = 0; i < array2.Length; i++)
						{
							if (array2[i] == b)
							{
								flag = false;
								break;
							}
						}
						if (flag)
						{
							num++;
						}
					}
					if (0 < num)
					{
						string[] array3 = new string[array.Length + num];
						array.CopyTo(array3, 0);
						int num2 = array.Length;
						foreach (object obj2 in base.Keys)
						{
							string text = (string)obj2;
							bool flag2 = true;
							string[] array2 = array;
							for (int i = 0; i < array2.Length; i++)
							{
								if (array2[i] == text)
								{
									flag2 = false;
									break;
								}
							}
							if (flag2)
							{
								array3[num2++] = text;
							}
						}
						array = array3;
					}
					this._knownKeywords = array;
				}
				return new ReadOnlyCollection<string>(array);
			}
		}

		/// <summary>Clears the contents of the <see cref="T:System.Data.Odbc.OdbcConnectionStringBuilder" /> instance.</summary>
		// Token: 0x06002069 RID: 8297 RVA: 0x00096FAC File Offset: 0x000951AC
		public override void Clear()
		{
			base.Clear();
			for (int i = 0; i < OdbcConnectionStringBuilder.s_validKeywords.Length; i++)
			{
				this.Reset((OdbcConnectionStringBuilder.Keywords)i);
			}
			this._knownKeywords = OdbcConnectionStringBuilder.s_validKeywords;
		}

		/// <summary>Determines whether the <see cref="T:System.Data.Odbc.OdbcConnectionStringBuilder" /> contains a specific key.</summary>
		/// <param name="keyword">The key to locate in the <see cref="T:System.Data.Odbc.OdbcConnectionStringBuilder" />.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Data.Odbc.OdbcConnectionStringBuilder" /> contains an element that has the specified key; otherwise <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="keyword" /> is null (<see langword="Nothing" /> in Visual Basic).</exception>
		// Token: 0x0600206A RID: 8298 RVA: 0x00096FE3 File Offset: 0x000951E3
		public override bool ContainsKey(string keyword)
		{
			ADP.CheckArgumentNull(keyword, "keyword");
			return OdbcConnectionStringBuilder.s_keywords.ContainsKey(keyword) || base.ContainsKey(keyword);
		}

		// Token: 0x0600206B RID: 8299 RVA: 0x00069B5C File Offset: 0x00067D5C
		private static string ConvertToString(object value)
		{
			return DbConnectionStringBuilderUtil.ConvertToString(value);
		}

		// Token: 0x0600206C RID: 8300 RVA: 0x00097006 File Offset: 0x00095206
		private object GetAt(OdbcConnectionStringBuilder.Keywords index)
		{
			if (index == OdbcConnectionStringBuilder.Keywords.Dsn)
			{
				return this.Dsn;
			}
			if (index == OdbcConnectionStringBuilder.Keywords.Driver)
			{
				return this.Driver;
			}
			throw ADP.KeywordNotSupported(OdbcConnectionStringBuilder.s_validKeywords[(int)index]);
		}

		/// <summary>Removes the entry with the specified key from the <see cref="T:System.Data.Odbc.OdbcConnectionStringBuilder" /> instance.</summary>
		/// <param name="keyword">The key of the key/value pair to be removed from the connection string in this <see cref="T:System.Data.Odbc.OdbcConnectionStringBuilder" />.</param>
		/// <returns>
		///   <see langword="true" /> if the key existed within the connection string and was removed; <see langword="false" /> if the key did not exist.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="keyword" /> is null (<see langword="Nothing" /> in Visual Basic).</exception>
		// Token: 0x0600206D RID: 8301 RVA: 0x0009702C File Offset: 0x0009522C
		public override bool Remove(string keyword)
		{
			ADP.CheckArgumentNull(keyword, "keyword");
			if (base.Remove(keyword))
			{
				OdbcConnectionStringBuilder.Keywords index;
				if (OdbcConnectionStringBuilder.s_keywords.TryGetValue(keyword, out index))
				{
					this.Reset(index);
				}
				else
				{
					base.ClearPropertyDescriptors();
					this._knownKeywords = null;
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600206E RID: 8302 RVA: 0x00097075 File Offset: 0x00095275
		private void Reset(OdbcConnectionStringBuilder.Keywords index)
		{
			if (index == OdbcConnectionStringBuilder.Keywords.Dsn)
			{
				this._dsn = "";
				return;
			}
			if (index == OdbcConnectionStringBuilder.Keywords.Driver)
			{
				this._driver = "";
				return;
			}
			throw ADP.KeywordNotSupported(OdbcConnectionStringBuilder.s_validKeywords[(int)index]);
		}

		// Token: 0x0600206F RID: 8303 RVA: 0x00069F75 File Offset: 0x00068175
		private void SetValue(string keyword, string value)
		{
			ADP.CheckArgumentNull(value, keyword);
			base[keyword] = value;
		}

		/// <summary>Retrieves a value corresponding to the supplied key from this <see cref="T:System.Data.Odbc.OdbcConnectionStringBuilder" />.</summary>
		/// <param name="keyword">The key of the item to retrieve.</param>
		/// <param name="value">The value corresponding to <paramref name="keyword" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="keyword" /> was found within the connection string; otherwise <see langword="false" />.</returns>
		// Token: 0x06002070 RID: 8304 RVA: 0x000970A4 File Offset: 0x000952A4
		public override bool TryGetValue(string keyword, out object value)
		{
			ADP.CheckArgumentNull(keyword, "keyword");
			OdbcConnectionStringBuilder.Keywords index;
			if (OdbcConnectionStringBuilder.s_keywords.TryGetValue(keyword, out index))
			{
				value = this.GetAt(index);
				return true;
			}
			return base.TryGetValue(keyword, out value);
		}

		// Token: 0x04001777 RID: 6007
		private static readonly string[] s_validKeywords;

		// Token: 0x04001778 RID: 6008
		private static readonly Dictionary<string, OdbcConnectionStringBuilder.Keywords> s_keywords;

		// Token: 0x04001779 RID: 6009
		private string[] _knownKeywords;

		// Token: 0x0400177A RID: 6010
		private string _dsn = "";

		// Token: 0x0400177B RID: 6011
		private string _driver = "";

		// Token: 0x020002DF RID: 735
		private enum Keywords
		{
			// Token: 0x0400177D RID: 6013
			Dsn,
			// Token: 0x0400177E RID: 6014
			Driver
		}
	}
}
