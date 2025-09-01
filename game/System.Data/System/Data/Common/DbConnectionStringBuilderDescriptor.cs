using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace System.Data.Common
{
	// Token: 0x0200038F RID: 911
	internal class DbConnectionStringBuilderDescriptor : PropertyDescriptor
	{
		// Token: 0x06002BE3 RID: 11235 RVA: 0x000BC8B0 File Offset: 0x000BAAB0
		internal DbConnectionStringBuilderDescriptor(string propertyName, Type componentType, Type propertyType, bool isReadOnly, Attribute[] attributes) : base(propertyName, attributes)
		{
			this.ComponentType = componentType;
			this.PropertyType = propertyType;
			this.IsReadOnly = isReadOnly;
		}

		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x06002BE4 RID: 11236 RVA: 0x000BC8D1 File Offset: 0x000BAAD1
		// (set) Token: 0x06002BE5 RID: 11237 RVA: 0x000BC8D9 File Offset: 0x000BAAD9
		internal bool RefreshOnChange
		{
			[CompilerGenerated]
			get
			{
				return this.<RefreshOnChange>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<RefreshOnChange>k__BackingField = value;
			}
		}

		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x06002BE6 RID: 11238 RVA: 0x000BC8E2 File Offset: 0x000BAAE2
		public override Type ComponentType
		{
			[CompilerGenerated]
			get
			{
				return this.<ComponentType>k__BackingField;
			}
		}

		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x06002BE7 RID: 11239 RVA: 0x000BC8EA File Offset: 0x000BAAEA
		public override bool IsReadOnly
		{
			[CompilerGenerated]
			get
			{
				return this.<IsReadOnly>k__BackingField;
			}
		}

		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x06002BE8 RID: 11240 RVA: 0x000BC8F2 File Offset: 0x000BAAF2
		public override Type PropertyType
		{
			[CompilerGenerated]
			get
			{
				return this.<PropertyType>k__BackingField;
			}
		}

		// Token: 0x06002BE9 RID: 11241 RVA: 0x000BC8FC File Offset: 0x000BAAFC
		public override bool CanResetValue(object component)
		{
			DbConnectionStringBuilder dbConnectionStringBuilder = component as DbConnectionStringBuilder;
			return dbConnectionStringBuilder != null && dbConnectionStringBuilder.ShouldSerialize(this.DisplayName);
		}

		// Token: 0x06002BEA RID: 11242 RVA: 0x000BC924 File Offset: 0x000BAB24
		public override object GetValue(object component)
		{
			DbConnectionStringBuilder dbConnectionStringBuilder = component as DbConnectionStringBuilder;
			object result;
			if (dbConnectionStringBuilder != null && dbConnectionStringBuilder.TryGetValue(this.DisplayName, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06002BEB RID: 11243 RVA: 0x000BC950 File Offset: 0x000BAB50
		public override void ResetValue(object component)
		{
			DbConnectionStringBuilder dbConnectionStringBuilder = component as DbConnectionStringBuilder;
			if (dbConnectionStringBuilder != null)
			{
				dbConnectionStringBuilder.Remove(this.DisplayName);
				if (this.RefreshOnChange)
				{
					dbConnectionStringBuilder.ClearPropertyDescriptors();
				}
			}
		}

		// Token: 0x06002BEC RID: 11244 RVA: 0x000BC984 File Offset: 0x000BAB84
		public override void SetValue(object component, object value)
		{
			DbConnectionStringBuilder dbConnectionStringBuilder = component as DbConnectionStringBuilder;
			if (dbConnectionStringBuilder != null)
			{
				if (typeof(string) == this.PropertyType && string.Empty.Equals(value))
				{
					value = null;
				}
				dbConnectionStringBuilder[this.DisplayName] = value;
				if (this.RefreshOnChange)
				{
					dbConnectionStringBuilder.ClearPropertyDescriptors();
				}
			}
		}

		// Token: 0x06002BED RID: 11245 RVA: 0x000BC9E0 File Offset: 0x000BABE0
		public override bool ShouldSerializeValue(object component)
		{
			DbConnectionStringBuilder dbConnectionStringBuilder = component as DbConnectionStringBuilder;
			return dbConnectionStringBuilder != null && dbConnectionStringBuilder.ShouldSerialize(this.DisplayName);
		}

		// Token: 0x04001B3E RID: 6974
		[CompilerGenerated]
		private bool <RefreshOnChange>k__BackingField;

		// Token: 0x04001B3F RID: 6975
		[CompilerGenerated]
		private readonly Type <ComponentType>k__BackingField;

		// Token: 0x04001B40 RID: 6976
		[CompilerGenerated]
		private readonly bool <IsReadOnly>k__BackingField;

		// Token: 0x04001B41 RID: 6977
		[CompilerGenerated]
		private readonly Type <PropertyType>k__BackingField;
	}
}
