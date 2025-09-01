using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices
{
	/// <summary>Enables and configures object pooling for a component. This class cannot be inherited.</summary>
	// Token: 0x02000035 RID: 53
	[AttributeUsage(AttributeTargets.Class)]
	[ComVisible(false)]
	public sealed class ObjectPoolingAttribute : Attribute, IConfigurationAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.ObjectPoolingAttribute" /> class and sets the <see cref="P:System.EnterpriseServices.ObjectPoolingAttribute.Enabled" />, <see cref="P:System.EnterpriseServices.ObjectPoolingAttribute.MaxPoolSize" />, <see cref="P:System.EnterpriseServices.ObjectPoolingAttribute.MinPoolSize" />, and <see cref="P:System.EnterpriseServices.ObjectPoolingAttribute.CreationTimeout" /> properties to their default values.</summary>
		// Token: 0x060000A1 RID: 161 RVA: 0x000023FC File Offset: 0x000005FC
		public ObjectPoolingAttribute() : this(true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.ObjectPoolingAttribute" /> class and sets the <see cref="P:System.EnterpriseServices.ObjectPoolingAttribute.Enabled" /> property.</summary>
		/// <param name="enable">
		///   <see langword="true" /> to enable object pooling; otherwise, <see langword="false" />.</param>
		// Token: 0x060000A2 RID: 162 RVA: 0x00002405 File Offset: 0x00000605
		public ObjectPoolingAttribute(bool enable)
		{
			this.enabled = enable;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.ObjectPoolingAttribute" /> class and sets the <see cref="P:System.EnterpriseServices.ObjectPoolingAttribute.MaxPoolSize" /> and <see cref="P:System.EnterpriseServices.ObjectPoolingAttribute.MinPoolSize" /> properties.</summary>
		/// <param name="minPoolSize">The minimum pool size.</param>
		/// <param name="maxPoolSize">The maximum pool size.</param>
		// Token: 0x060000A3 RID: 163 RVA: 0x00002414 File Offset: 0x00000614
		public ObjectPoolingAttribute(int minPoolSize, int maxPoolSize) : this(true, minPoolSize, maxPoolSize)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.ObjectPoolingAttribute" /> class and sets the <see cref="P:System.EnterpriseServices.ObjectPoolingAttribute.Enabled" />, <see cref="P:System.EnterpriseServices.ObjectPoolingAttribute.MaxPoolSize" />, and <see cref="P:System.EnterpriseServices.ObjectPoolingAttribute.MinPoolSize" /> properties.</summary>
		/// <param name="enable">
		///   <see langword="true" /> to enable object pooling; otherwise, <see langword="false" />.</param>
		/// <param name="minPoolSize">The minimum pool size.</param>
		/// <param name="maxPoolSize">The maximum pool size.</param>
		// Token: 0x060000A4 RID: 164 RVA: 0x0000241F File Offset: 0x0000061F
		public ObjectPoolingAttribute(bool enable, int minPoolSize, int maxPoolSize)
		{
			this.enabled = enable;
			this.minPoolSize = minPoolSize;
			this.maxPoolSize = maxPoolSize;
		}

		/// <summary>Gets or sets the length of time to wait for an object to become available in the pool before throwing an exception. This value is in milliseconds.</summary>
		/// <returns>The time-out value in milliseconds.</returns>
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x0000243C File Offset: 0x0000063C
		// (set) Token: 0x060000A6 RID: 166 RVA: 0x00002444 File Offset: 0x00000644
		public int CreationTimeout
		{
			get
			{
				return this.creationTimeout;
			}
			set
			{
				this.creationTimeout = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether object pooling is enabled.</summary>
		/// <returns>
		///   <see langword="true" /> if object pooling is enabled; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x0000244D File Offset: 0x0000064D
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x00002455 File Offset: 0x00000655
		public bool Enabled
		{
			get
			{
				return this.enabled;
			}
			set
			{
				this.enabled = value;
			}
		}

		/// <summary>Gets or sets the value for the maximum size of the pool.</summary>
		/// <returns>The maximum number of objects in the pool.</returns>
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x0000245E File Offset: 0x0000065E
		// (set) Token: 0x060000AA RID: 170 RVA: 0x00002466 File Offset: 0x00000666
		public int MaxPoolSize
		{
			get
			{
				return this.maxPoolSize;
			}
			set
			{
				this.maxPoolSize = value;
			}
		}

		/// <summary>Gets or sets the value for the minimum size of the pool.</summary>
		/// <returns>The minimum number of objects in the pool.</returns>
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000AB RID: 171 RVA: 0x0000246F File Offset: 0x0000066F
		// (set) Token: 0x060000AC RID: 172 RVA: 0x00002477 File Offset: 0x00000677
		public int MinPoolSize
		{
			get
			{
				return this.minPoolSize;
			}
			set
			{
				this.minPoolSize = value;
			}
		}

		/// <summary>Called internally by the .NET Framework infrastructure while installing and configuring assemblies in the COM+ catalog.</summary>
		/// <param name="info">A hash table that contains internal objects referenced by internal keys.</param>
		/// <returns>
		///   <see langword="true" /> if the method has made changes.</returns>
		// Token: 0x060000AD RID: 173 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		public bool AfterSaveChanges(Hashtable info)
		{
			throw new NotImplementedException();
		}

		/// <summary>Called internally by the .NET Framework infrastructure while applying the <see cref="T:System.EnterpriseServices.ObjectPoolingAttribute" /> class attribute to a serviced component.</summary>
		/// <param name="info">A hash table that contains an internal object to which object pooling properties are applied, referenced by an internal key.</param>
		/// <returns>
		///   <see langword="true" /> if the method has made changes; otherwise, <see langword="false" />.</returns>
		// Token: 0x060000AE RID: 174 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		public bool Apply(Hashtable info)
		{
			throw new NotImplementedException();
		}

		/// <summary>Called internally by the .NET Framework infrastructure while installing and configuring assemblies in the COM+ catalog.</summary>
		/// <param name="s">A string generated by the .NET Framework infrastructure that is checked for a special value that indicates a serviced component.</param>
		/// <returns>
		///   <see langword="true" /> if the attribute is applied to a serviced component class.</returns>
		// Token: 0x060000AF RID: 175 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		public bool IsValidTarget(string s)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0400006F RID: 111
		private int creationTimeout;

		// Token: 0x04000070 RID: 112
		private bool enabled;

		// Token: 0x04000071 RID: 113
		private int minPoolSize;

		// Token: 0x04000072 RID: 114
		private int maxPoolSize;
	}
}
