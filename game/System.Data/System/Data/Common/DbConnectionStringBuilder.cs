using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Threading;

namespace System.Data.Common
{
	/// <summary>Provides a base class for strongly typed connection string builders.</summary>
	// Token: 0x0200038E RID: 910
	public class DbConnectionStringBuilder : IDictionary, ICollection, IEnumerable, ICustomTypeDescriptor
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> class.</summary>
		// Token: 0x06002BAB RID: 11179 RVA: 0x000BBE14 File Offset: 0x000BA014
		public DbConnectionStringBuilder()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> class, optionally using ODBC rules for quoting values.</summary>
		/// <param name="useOdbcRules">
		///   <see langword="true" /> to use {} to delimit fields; <see langword="false" /> to use quotation marks.</param>
		// Token: 0x06002BAC RID: 11180 RVA: 0x000BBE3E File Offset: 0x000BA03E
		public DbConnectionStringBuilder(bool useOdbcRules)
		{
			this._useOdbcRules = useOdbcRules;
		}

		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x06002BAD RID: 11181 RVA: 0x000BBE6F File Offset: 0x000BA06F
		private ICollection Collection
		{
			get
			{
				return this.CurrentValues;
			}
		}

		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x06002BAE RID: 11182 RVA: 0x000BBE6F File Offset: 0x000BA06F
		private IDictionary Dictionary
		{
			get
			{
				return this.CurrentValues;
			}
		}

		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x06002BAF RID: 11183 RVA: 0x000BBE78 File Offset: 0x000BA078
		private Dictionary<string, object> CurrentValues
		{
			get
			{
				Dictionary<string, object> dictionary = this._currentValues;
				if (dictionary == null)
				{
					dictionary = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
					this._currentValues = dictionary;
				}
				return dictionary;
			}
		}

		/// <summary>Gets or sets the element with the specified key.</summary>
		/// <param name="keyword">The key of the element to get or set.</param>
		/// <returns>The element with the specified key.</returns>
		// Token: 0x1700076A RID: 1898
		object IDictionary.this[object keyword]
		{
			get
			{
				return this[this.ObjectToString(keyword)];
			}
			set
			{
				this[this.ObjectToString(keyword)] = value;
			}
		}

		/// <summary>Gets or sets the value associated with the specified key.</summary>
		/// <param name="keyword">The key of the item to get or set.</param>
		/// <returns>The value associated with the specified key. If the specified key is not found, trying to get it returns a null reference (<see langword="Nothing" /> in Visual Basic), and trying to set it creates a new element using the specified key.  
		///  Passing a null (<see langword="Nothing" /> in Visual Basic) key throws an <see cref="T:System.ArgumentNullException" />. Assigning a null value removes the key/value pair.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="keyword" /> is a null reference (<see langword="Nothing" /> in Visual Basic).</exception>
		/// <exception cref="T:System.NotSupportedException">The property is set, and the <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> is read-only.  
		///  -or-  
		///  The property is set, <paramref name="keyword" /> does not exist in the collection, and the <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> has a fixed size.</exception>
		// Token: 0x1700076B RID: 1899
		[Browsable(false)]
		public virtual object this[string keyword]
		{
			get
			{
				DataCommonEventSource.Log.Trace<int, string>("<comm.DbConnectionStringBuilder.get_Item|API> {0}, keyword='{1}'", this.ObjectID, keyword);
				ADP.CheckArgumentNull(keyword, "keyword");
				object result;
				if (this.CurrentValues.TryGetValue(keyword, out result))
				{
					return result;
				}
				throw ADP.KeywordNotSupported(keyword);
			}
			set
			{
				ADP.CheckArgumentNull(keyword, "keyword");
				bool flag;
				if (value != null)
				{
					string value2 = DbConnectionStringBuilderUtil.ConvertToString(value);
					DbConnectionOptions.ValidateKeyValuePair(keyword, value2);
					flag = this.CurrentValues.ContainsKey(keyword);
					this.CurrentValues[keyword] = value2;
				}
				else
				{
					flag = this.Remove(keyword);
				}
				this._connectionString = null;
				if (flag)
				{
					this._propertyDescriptors = null;
				}
			}
		}

		/// <summary>Gets or sets a value that indicates whether the <see cref="P:System.Data.Common.DbConnectionStringBuilder.ConnectionString" /> property is visible in Visual Studio designers.</summary>
		/// <returns>
		///   <see langword="true" /> if the connection string is visible within designers; <see langword="false" /> otherwise. The default is <see langword="true" />.</returns>
		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x06002BB4 RID: 11188 RVA: 0x000BBF6C File Offset: 0x000BA16C
		// (set) Token: 0x06002BB5 RID: 11189 RVA: 0x000BBF74 File Offset: 0x000BA174
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DesignOnly(true)]
		[Browsable(false)]
		public bool BrowsableConnectionString
		{
			get
			{
				return this._browsableConnectionString;
			}
			set
			{
				this._browsableConnectionString = value;
				this._propertyDescriptors = null;
			}
		}

		/// <summary>Gets or sets the connection string associated with the <see cref="T:System.Data.Common.DbConnectionStringBuilder" />.</summary>
		/// <returns>The current connection string, created from the key/value pairs that are contained within the <see cref="T:System.Data.Common.DbConnectionStringBuilder" />. The default value is an empty string.</returns>
		/// <exception cref="T:System.ArgumentException">An invalid connection string argument has been supplied.</exception>
		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x06002BB6 RID: 11190 RVA: 0x000BBF84 File Offset: 0x000BA184
		// (set) Token: 0x06002BB7 RID: 11191 RVA: 0x000BC03C File Offset: 0x000BA23C
		[RefreshProperties(RefreshProperties.All)]
		public string ConnectionString
		{
			get
			{
				DataCommonEventSource.Log.Trace<int>("<comm.DbConnectionStringBuilder.get_ConnectionString|API> {0}", this.ObjectID);
				string text = this._connectionString;
				if (text == null)
				{
					StringBuilder stringBuilder = new StringBuilder();
					foreach (object obj in this.Keys)
					{
						string keyword = (string)obj;
						object value;
						if (this.ShouldSerialize(keyword) && this.TryGetValue(keyword, out value))
						{
							string value2 = this.ConvertValueToString(value);
							DbConnectionStringBuilder.AppendKeyValuePair(stringBuilder, keyword, value2, this._useOdbcRules);
						}
					}
					text = stringBuilder.ToString();
					this._connectionString = text;
				}
				return text;
			}
			set
			{
				DataCommonEventSource.Log.Trace<int>("<comm.DbConnectionStringBuilder.set_ConnectionString|API> {0}", this.ObjectID);
				DbConnectionOptions dbConnectionOptions = new DbConnectionOptions(value, null, this._useOdbcRules);
				string connectionString = this.ConnectionString;
				this.Clear();
				try
				{
					for (NameValuePair nameValuePair = dbConnectionOptions._keyChain; nameValuePair != null; nameValuePair = nameValuePair.Next)
					{
						if (nameValuePair.Value != null)
						{
							this[nameValuePair.Name] = nameValuePair.Value;
						}
						else
						{
							this.Remove(nameValuePair.Name);
						}
					}
					this._connectionString = null;
				}
				catch (ArgumentException)
				{
					this.ConnectionString = connectionString;
					this._connectionString = connectionString;
					throw;
				}
			}
		}

		/// <summary>Gets the current number of keys that are contained within the <see cref="P:System.Data.Common.DbConnectionStringBuilder.ConnectionString" /> property.</summary>
		/// <returns>The number of keys that are contained within the connection string maintained by the <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> instance.</returns>
		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x06002BB8 RID: 11192 RVA: 0x000BC0E0 File Offset: 0x000BA2E0
		[Browsable(false)]
		public virtual int Count
		{
			get
			{
				return this.CurrentValues.Count;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> is read-only; otherwise <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x06002BB9 RID: 11193 RVA: 0x00006D64 File Offset: 0x00004F64
		[Browsable(false)]
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> has a fixed size.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> has a fixed size; otherwise <see langword="false" />.</returns>
		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x06002BBA RID: 11194 RVA: 0x00006D64 File Offset: 0x00004F64
		[Browsable(false)]
		public virtual bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).</summary>
		/// <returns>
		///   <see langword="true" /> if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, <see langword="false" />.</returns>
		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x06002BBB RID: 11195 RVA: 0x000BC0ED File Offset: 0x000BA2ED
		bool ICollection.IsSynchronized
		{
			get
			{
				return this.Collection.IsSynchronized;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> that contains the keys in the <see cref="T:System.Data.Common.DbConnectionStringBuilder" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> that contains the keys in the <see cref="T:System.Data.Common.DbConnectionStringBuilder" />.</returns>
		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x06002BBC RID: 11196 RVA: 0x000BC0FA File Offset: 0x000BA2FA
		[Browsable(false)]
		public virtual ICollection Keys
		{
			get
			{
				DataCommonEventSource.Log.Trace<int>("<comm.DbConnectionStringBuilder.Keys|API> {0}", this.ObjectID);
				return this.Dictionary.Keys;
			}
		}

		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x06002BBD RID: 11197 RVA: 0x000BC11C File Offset: 0x000BA31C
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</returns>
		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x06002BBE RID: 11198 RVA: 0x000BC124 File Offset: 0x000BA324
		object ICollection.SyncRoot
		{
			get
			{
				return this.Collection.SyncRoot;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> that contains the values in the <see cref="T:System.Data.Common.DbConnectionStringBuilder" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> that contains the values in the <see cref="T:System.Data.Common.DbConnectionStringBuilder" />.</returns>
		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x06002BBF RID: 11199 RVA: 0x000BC134 File Offset: 0x000BA334
		[Browsable(false)]
		public virtual ICollection Values
		{
			get
			{
				DataCommonEventSource.Log.Trace<int>("<comm.DbConnectionStringBuilder.Values|API> {0}", this.ObjectID);
				ICollection<string> collection = (ICollection<string>)this.Keys;
				IEnumerator<string> enumerator = collection.GetEnumerator();
				object[] array = new object[collection.Count];
				for (int i = 0; i < array.Length; i++)
				{
					enumerator.MoveNext();
					array[i] = this[enumerator.Current];
				}
				return new ReadOnlyCollection<object>(array);
			}
		}

		// Token: 0x06002BC0 RID: 11200 RVA: 0x000BC19D File Offset: 0x000BA39D
		internal virtual string ConvertValueToString(object value)
		{
			if (value != null)
			{
				return Convert.ToString(value, CultureInfo.InvariantCulture);
			}
			return null;
		}

		/// <summary>Adds an element with the provided key and value to the <see cref="T:System.Collections.IDictionary" /> object.</summary>
		/// <param name="keyword">The <see cref="T:System.Object" /> to use as the key of the element to add.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to use as the value of the element to add.</param>
		// Token: 0x06002BC1 RID: 11201 RVA: 0x000BC1AF File Offset: 0x000BA3AF
		void IDictionary.Add(object keyword, object value)
		{
			this.Add(this.ObjectToString(keyword), value);
		}

		/// <summary>Adds an entry with the specified key and value into the <see cref="T:System.Data.Common.DbConnectionStringBuilder" />.</summary>
		/// <param name="keyword">The key to add to the <see cref="T:System.Data.Common.DbConnectionStringBuilder" />.</param>
		/// <param name="value">The value for the specified key.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="keyword" /> is a null reference (<see langword="Nothing" /> in Visual Basic).</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> is read-only.  
		///  -or-  
		///  The <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> has a fixed size.</exception>
		// Token: 0x06002BC2 RID: 11202 RVA: 0x000BC1BF File Offset: 0x000BA3BF
		public void Add(string keyword, object value)
		{
			this[keyword] = value;
		}

		/// <summary>Provides an efficient and safe way to append a key and value to an existing <see cref="T:System.Text.StringBuilder" /> object.</summary>
		/// <param name="builder">The <see cref="T:System.Text.StringBuilder" /> to which to add the key/value pair.</param>
		/// <param name="keyword">The key to be added.</param>
		/// <param name="value">The value for the supplied key.</param>
		// Token: 0x06002BC3 RID: 11203 RVA: 0x000BC1C9 File Offset: 0x000BA3C9
		public static void AppendKeyValuePair(StringBuilder builder, string keyword, string value)
		{
			DbConnectionOptions.AppendKeyValuePairBuilder(builder, keyword, value, false);
		}

		/// <summary>Provides an efficient and safe way to append a key and value to an existing <see cref="T:System.Text.StringBuilder" /> object.</summary>
		/// <param name="builder">The <see cref="T:System.Text.StringBuilder" /> to which to add the key/value pair.</param>
		/// <param name="keyword">The key to be added.</param>
		/// <param name="value">The value for the supplied key.</param>
		/// <param name="useOdbcRules">
		///   <see langword="true" /> to use {} to delimit fields, <see langword="false" /> to use quotation marks.</param>
		// Token: 0x06002BC4 RID: 11204 RVA: 0x000BC1D4 File Offset: 0x000BA3D4
		public static void AppendKeyValuePair(StringBuilder builder, string keyword, string value, bool useOdbcRules)
		{
			DbConnectionOptions.AppendKeyValuePairBuilder(builder, keyword, value, useOdbcRules);
		}

		/// <summary>Clears the contents of the <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> instance.</summary>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> is read-only.</exception>
		// Token: 0x06002BC5 RID: 11205 RVA: 0x000BC1DF File Offset: 0x000BA3DF
		public virtual void Clear()
		{
			DataCommonEventSource.Log.Trace("<comm.DbConnectionStringBuilder.Clear|API>");
			this._connectionString = string.Empty;
			this._propertyDescriptors = null;
			this.CurrentValues.Clear();
		}

		/// <summary>Clears the collection of <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects on the associated <see cref="T:System.Data.Common.DbConnectionStringBuilder" />.</summary>
		// Token: 0x06002BC6 RID: 11206 RVA: 0x000BC20D File Offset: 0x000BA40D
		protected internal void ClearPropertyDescriptors()
		{
			this._propertyDescriptors = null;
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.IDictionary" /> object contains an element with the specified key.</summary>
		/// <param name="keyword">The key to locate in the <see cref="T:System.Collections.IDictionary" /> object.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.IDictionary" /> contains an element with the key; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002BC7 RID: 11207 RVA: 0x000BC216 File Offset: 0x000BA416
		bool IDictionary.Contains(object keyword)
		{
			return this.ContainsKey(this.ObjectToString(keyword));
		}

		/// <summary>Determines whether the <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> contains a specific key.</summary>
		/// <param name="keyword">The key to locate in the <see cref="T:System.Data.Common.DbConnectionStringBuilder" />.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> contains an entry with the specified key; otherwise <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="keyword" /> is a null reference (<see langword="Nothing" /> in Visual Basic).</exception>
		// Token: 0x06002BC8 RID: 11208 RVA: 0x000BC225 File Offset: 0x000BA425
		public virtual bool ContainsKey(string keyword)
		{
			ADP.CheckArgumentNull(keyword, "keyword");
			return this.CurrentValues.ContainsKey(keyword);
		}

		/// <summary>Copies the elements of the <see cref="T:System.Collections.ICollection" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ICollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		// Token: 0x06002BC9 RID: 11209 RVA: 0x000BC23E File Offset: 0x000BA43E
		void ICollection.CopyTo(Array array, int index)
		{
			DataCommonEventSource.Log.Trace<int>("<comm.DbConnectionStringBuilder.ICollection.CopyTo|API> {0}", this.ObjectID);
			this.Collection.CopyTo(array, index);
		}

		/// <summary>Compares the connection information in this <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> object with the connection information in the supplied object.</summary>
		/// <param name="connectionStringBuilder">The <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> to be compared with this <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> object.</param>
		/// <returns>
		///   <see langword="true" /> if the connection information in both of the <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> objects causes an equivalent connection string; otherwise <see langword="false" />.</returns>
		// Token: 0x06002BCA RID: 11210 RVA: 0x000BC264 File Offset: 0x000BA464
		public virtual bool EquivalentTo(DbConnectionStringBuilder connectionStringBuilder)
		{
			ADP.CheckArgumentNull(connectionStringBuilder, "connectionStringBuilder");
			DataCommonEventSource.Log.Trace<int, int>("<comm.DbConnectionStringBuilder.EquivalentTo|API> {0}, connectionStringBuilder={1}", this.ObjectID, connectionStringBuilder.ObjectID);
			if (base.GetType() != connectionStringBuilder.GetType() || this.CurrentValues.Count != connectionStringBuilder.CurrentValues.Count)
			{
				return false;
			}
			foreach (KeyValuePair<string, object> keyValuePair in this.CurrentValues)
			{
				object obj;
				if (!connectionStringBuilder.CurrentValues.TryGetValue(keyValuePair.Key, out obj) || !keyValuePair.Value.Equals(obj))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
		// Token: 0x06002BCB RID: 11211 RVA: 0x000BC330 File Offset: 0x000BA530
		IEnumerator IEnumerable.GetEnumerator()
		{
			DataCommonEventSource.Log.Trace<int>("<comm.DbConnectionStringBuilder.IEnumerable.GetEnumerator|API> {0}", this.ObjectID);
			return this.Collection.GetEnumerator();
		}

		/// <summary>Returns an <see cref="T:System.Collections.IDictionaryEnumerator" /> object for the <see cref="T:System.Collections.IDictionary" /> object.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionaryEnumerator" /> object for the <see cref="T:System.Collections.IDictionary" /> object.</returns>
		// Token: 0x06002BCC RID: 11212 RVA: 0x000BC352 File Offset: 0x000BA552
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			DataCommonEventSource.Log.Trace<int>("<comm.DbConnectionStringBuilder.IDictionary.GetEnumerator|API> {0}", this.ObjectID);
			return this.Dictionary.GetEnumerator();
		}

		// Token: 0x06002BCD RID: 11213 RVA: 0x000BC374 File Offset: 0x000BA574
		private string ObjectToString(object keyword)
		{
			string result;
			try
			{
				result = (string)keyword;
			}
			catch (InvalidCastException)
			{
				throw new ArgumentException("not a string", "keyword");
			}
			return result;
		}

		/// <summary>Removes the element with the specified key from the <see cref="T:System.Collections.IDictionary" /> object.</summary>
		/// <param name="keyword">The key of the element to remove.</param>
		// Token: 0x06002BCE RID: 11214 RVA: 0x000BC3AC File Offset: 0x000BA5AC
		void IDictionary.Remove(object keyword)
		{
			this.Remove(this.ObjectToString(keyword));
		}

		/// <summary>Removes the entry with the specified key from the <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> instance.</summary>
		/// <param name="keyword">The key of the key/value pair to be removed from the connection string in this <see cref="T:System.Data.Common.DbConnectionStringBuilder" />.</param>
		/// <returns>
		///   <see langword="true" /> if the key existed within the connection string and was removed; <see langword="false" /> if the key did not exist.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="keyword" /> is null (<see langword="Nothing" /> in Visual Basic)</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> is read-only, or the <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> has a fixed size.</exception>
		// Token: 0x06002BCF RID: 11215 RVA: 0x000BC3BC File Offset: 0x000BA5BC
		public virtual bool Remove(string keyword)
		{
			DataCommonEventSource.Log.Trace<int, string>("<comm.DbConnectionStringBuilder.Remove|API> {0}, keyword='{1}'", this.ObjectID, keyword);
			ADP.CheckArgumentNull(keyword, "keyword");
			if (this.CurrentValues.Remove(keyword))
			{
				this._connectionString = null;
				this._propertyDescriptors = null;
				return true;
			}
			return false;
		}

		/// <summary>Indicates whether the specified key exists in this <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> instance.</summary>
		/// <param name="keyword">The key to locate in the <see cref="T:System.Data.Common.DbConnectionStringBuilder" />.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> contains an entry with the specified key; otherwise <see langword="false" />.</returns>
		// Token: 0x06002BD0 RID: 11216 RVA: 0x000BC225 File Offset: 0x000BA425
		public virtual bool ShouldSerialize(string keyword)
		{
			ADP.CheckArgumentNull(keyword, "keyword");
			return this.CurrentValues.ContainsKey(keyword);
		}

		/// <summary>Returns the connection string associated with this <see cref="T:System.Data.Common.DbConnectionStringBuilder" />.</summary>
		/// <returns>The current <see cref="P:System.Data.Common.DbConnectionStringBuilder.ConnectionString" /> property.</returns>
		// Token: 0x06002BD1 RID: 11217 RVA: 0x000BC409 File Offset: 0x000BA609
		public override string ToString()
		{
			return this.ConnectionString;
		}

		/// <summary>Retrieves a value corresponding to the supplied key from this <see cref="T:System.Data.Common.DbConnectionStringBuilder" />.</summary>
		/// <param name="keyword">The key of the item to retrieve.</param>
		/// <param name="value">The value corresponding to the <paramref name="keyword" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="keyword" /> was found within the connection string, <see langword="false" /> otherwise.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="keyword" /> contains a null value (<see langword="Nothing" /> in Visual Basic).</exception>
		// Token: 0x06002BD2 RID: 11218 RVA: 0x000BC411 File Offset: 0x000BA611
		public virtual bool TryGetValue(string keyword, out object value)
		{
			ADP.CheckArgumentNull(keyword, "keyword");
			return this.CurrentValues.TryGetValue(keyword, out value);
		}

		// Token: 0x06002BD3 RID: 11219 RVA: 0x000BC42C File Offset: 0x000BA62C
		internal Attribute[] GetAttributesFromCollection(AttributeCollection collection)
		{
			Attribute[] array = new Attribute[collection.Count];
			collection.CopyTo(array, 0);
			return array;
		}

		// Token: 0x06002BD4 RID: 11220 RVA: 0x000BC450 File Offset: 0x000BA650
		private PropertyDescriptorCollection GetProperties()
		{
			PropertyDescriptorCollection propertyDescriptorCollection = this._propertyDescriptors;
			if (propertyDescriptorCollection == null)
			{
				long scopeId = DataCommonEventSource.Log.EnterScope<int>("<comm.DbConnectionStringBuilder.GetProperties|INFO> {0}", this.ObjectID);
				try
				{
					Hashtable hashtable = new Hashtable(StringComparer.OrdinalIgnoreCase);
					this.GetProperties(hashtable);
					PropertyDescriptor[] array = new PropertyDescriptor[hashtable.Count];
					hashtable.Values.CopyTo(array, 0);
					propertyDescriptorCollection = new PropertyDescriptorCollection(array);
					this._propertyDescriptors = propertyDescriptorCollection;
				}
				finally
				{
					DataCommonEventSource.Log.ExitScope(scopeId);
				}
			}
			return propertyDescriptorCollection;
		}

		/// <summary>Fills a supplied <see cref="T:System.Collections.Hashtable" /> with information about all the properties of this <see cref="T:System.Data.Common.DbConnectionStringBuilder" />.</summary>
		/// <param name="propertyDescriptors">The <see cref="T:System.Collections.Hashtable" /> to be filled with information about this <see cref="T:System.Data.Common.DbConnectionStringBuilder" />.</param>
		// Token: 0x06002BD5 RID: 11221 RVA: 0x000BC4D8 File Offset: 0x000BA6D8
		protected virtual void GetProperties(Hashtable propertyDescriptors)
		{
			long scopeId = DataCommonEventSource.Log.EnterScope<int>("<comm.DbConnectionStringBuilder.GetProperties|API> {0}", this.ObjectID);
			try
			{
				foreach (object obj in TypeDescriptor.GetProperties(this, true))
				{
					PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
					if ("ConnectionString" != propertyDescriptor.Name)
					{
						string displayName = propertyDescriptor.DisplayName;
						if (!propertyDescriptors.ContainsKey(displayName))
						{
							Attribute[] array = this.GetAttributesFromCollection(propertyDescriptor.Attributes);
							PropertyDescriptor value = new DbConnectionStringBuilderDescriptor(propertyDescriptor.Name, propertyDescriptor.ComponentType, propertyDescriptor.PropertyType, propertyDescriptor.IsReadOnly, array);
							propertyDescriptors[displayName] = value;
						}
					}
					else if (this.BrowsableConnectionString)
					{
						propertyDescriptors["ConnectionString"] = propertyDescriptor;
					}
					else
					{
						propertyDescriptors.Remove("ConnectionString");
					}
				}
				if (!this.IsFixedSize)
				{
					Attribute[] array = null;
					foreach (object obj2 in this.Keys)
					{
						string text = (string)obj2;
						if (!propertyDescriptors.ContainsKey(text))
						{
							object obj3 = this[text];
							Type type;
							if (obj3 != null)
							{
								type = obj3.GetType();
								if (typeof(string) == type)
								{
									int num;
									bool flag;
									if (int.TryParse((string)obj3, out num))
									{
										type = typeof(int);
									}
									else if (bool.TryParse((string)obj3, out flag))
									{
										type = typeof(bool);
									}
								}
							}
							else
							{
								type = typeof(string);
							}
							Attribute[] attributes = array;
							if (StringComparer.OrdinalIgnoreCase.Equals("Password", text) || StringComparer.OrdinalIgnoreCase.Equals("pwd", text))
							{
								attributes = new Attribute[]
								{
									BrowsableAttribute.Yes,
									PasswordPropertyTextAttribute.Yes,
									RefreshPropertiesAttribute.All
								};
							}
							else if (array == null)
							{
								array = new Attribute[]
								{
									BrowsableAttribute.Yes,
									RefreshPropertiesAttribute.All
								};
								attributes = array;
							}
							PropertyDescriptor value2 = new DbConnectionStringBuilderDescriptor(text, base.GetType(), type, false, attributes);
							propertyDescriptors[text] = value2;
						}
					}
				}
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(scopeId);
			}
		}

		// Token: 0x06002BD6 RID: 11222 RVA: 0x000BC764 File Offset: 0x000BA964
		private PropertyDescriptorCollection GetProperties(Attribute[] attributes)
		{
			PropertyDescriptorCollection properties = this.GetProperties();
			if (attributes == null || attributes.Length == 0)
			{
				return properties;
			}
			PropertyDescriptor[] array = new PropertyDescriptor[properties.Count];
			int num = 0;
			foreach (object obj in properties)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				bool flag = true;
				foreach (Attribute attribute in attributes)
				{
					Attribute attribute2 = propertyDescriptor.Attributes[attribute.GetType()];
					if ((attribute2 == null && !attribute.IsDefaultAttribute()) || !attribute2.Match(attribute))
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					array[num] = propertyDescriptor;
					num++;
				}
			}
			PropertyDescriptor[] array2 = new PropertyDescriptor[num];
			Array.Copy(array, array2, num);
			return new PropertyDescriptorCollection(array2);
		}

		/// <summary>Returns the class name of this instance of a component.</summary>
		/// <returns>The class name of the object, or <see langword="null" /> if the class does not have a name.</returns>
		// Token: 0x06002BD7 RID: 11223 RVA: 0x000BC84C File Offset: 0x000BAA4C
		string ICustomTypeDescriptor.GetClassName()
		{
			return TypeDescriptor.GetClassName(this, true);
		}

		/// <summary>Returns the name of this instance of a component.</summary>
		/// <returns>The name of the object, or <see langword="null" /> if the object does not have a name.</returns>
		// Token: 0x06002BD8 RID: 11224 RVA: 0x000BC855 File Offset: 0x000BAA55
		string ICustomTypeDescriptor.GetComponentName()
		{
			return TypeDescriptor.GetComponentName(this, true);
		}

		/// <summary>Returns a collection of custom attributes for this instance of a component.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.AttributeCollection" /> containing the attributes for this object.</returns>
		// Token: 0x06002BD9 RID: 11225 RVA: 0x000BC85E File Offset: 0x000BAA5E
		AttributeCollection ICustomTypeDescriptor.GetAttributes()
		{
			return TypeDescriptor.GetAttributes(this, true);
		}

		/// <summary>Returns an editor of the specified type for this instance of a component.</summary>
		/// <param name="editorBaseType">A <see cref="T:System.Type" /> that represents the editor for this object.</param>
		/// <returns>An <see cref="T:System.Object" /> of the specified type that is the editor for this object, or <see langword="null" /> if the editor cannot be found.</returns>
		// Token: 0x06002BDA RID: 11226 RVA: 0x000BC867 File Offset: 0x000BAA67
		object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
		{
			return TypeDescriptor.GetEditor(this, editorBaseType, true);
		}

		/// <summary>Returns a type converter for this instance of a component.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter" /> that is the converter for this object, or <see langword="null" /> if there is no <see cref="T:System.ComponentModel.TypeConverter" /> for this object.</returns>
		// Token: 0x06002BDB RID: 11227 RVA: 0x000BC871 File Offset: 0x000BAA71
		TypeConverter ICustomTypeDescriptor.GetConverter()
		{
			return TypeDescriptor.GetConverter(this, true);
		}

		/// <summary>Returns the default property for this instance of a component.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptor" /> that represents the default property for this object, or <see langword="null" /> if this object does not have properties.</returns>
		// Token: 0x06002BDC RID: 11228 RVA: 0x000BC87A File Offset: 0x000BAA7A
		PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
		{
			return TypeDescriptor.GetDefaultProperty(this, true);
		}

		/// <summary>Returns the properties for this instance of a component.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that represents the properties for this component instance.</returns>
		// Token: 0x06002BDD RID: 11229 RVA: 0x000BC883 File Offset: 0x000BAA83
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
		{
			return this.GetProperties();
		}

		/// <summary>Returns the properties for this instance of a component using the attribute array as a filter.</summary>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that is used as a filter.</param>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that represents the filtered properties for this component instance.</returns>
		// Token: 0x06002BDE RID: 11230 RVA: 0x000BC88B File Offset: 0x000BAA8B
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
		{
			return this.GetProperties(attributes);
		}

		/// <summary>Returns the default event for this instance of a component.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.EventDescriptor" /> that represents the default event for this object, or <see langword="null" /> if this object does not have events.</returns>
		// Token: 0x06002BDF RID: 11231 RVA: 0x000BC894 File Offset: 0x000BAA94
		EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
		{
			return TypeDescriptor.GetDefaultEvent(this, true);
		}

		/// <summary>Returns the events for this instance of a component.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.EventDescriptorCollection" /> that represents the events for this component instance.</returns>
		// Token: 0x06002BE0 RID: 11232 RVA: 0x000BC89D File Offset: 0x000BAA9D
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
		{
			return TypeDescriptor.GetEvents(this, true);
		}

		/// <summary>Returns the events for this instance of a component using the specified attribute array as a filter.</summary>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that is used as a filter.</param>
		/// <returns>An <see cref="T:System.ComponentModel.EventDescriptorCollection" /> that represents the filtered events for this component instance.</returns>
		// Token: 0x06002BE1 RID: 11233 RVA: 0x000BC8A6 File Offset: 0x000BAAA6
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
		{
			return TypeDescriptor.GetEvents(this, attributes, true);
		}

		/// <summary>Returns an object that contains the property described by the specified property descriptor.</summary>
		/// <param name="pd">A <see cref="T:System.ComponentModel.PropertyDescriptor" /> that represents the property whose owner is to be found.</param>
		/// <returns>An <see cref="T:System.Object" /> that represents the owner of the specified property.</returns>
		// Token: 0x06002BE2 RID: 11234 RVA: 0x00005696 File Offset: 0x00003896
		object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}

		// Token: 0x04001B37 RID: 6967
		private Dictionary<string, object> _currentValues;

		// Token: 0x04001B38 RID: 6968
		private string _connectionString = string.Empty;

		// Token: 0x04001B39 RID: 6969
		private PropertyDescriptorCollection _propertyDescriptors;

		// Token: 0x04001B3A RID: 6970
		private bool _browsableConnectionString = true;

		// Token: 0x04001B3B RID: 6971
		private readonly bool _useOdbcRules;

		// Token: 0x04001B3C RID: 6972
		private static int s_objectTypeCount;

		// Token: 0x04001B3D RID: 6973
		internal readonly int _objectID = Interlocked.Increment(ref DbConnectionStringBuilder.s_objectTypeCount);
	}
}
