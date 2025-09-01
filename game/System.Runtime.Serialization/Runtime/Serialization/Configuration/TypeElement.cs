using System;
using System.Configuration;

namespace System.Runtime.Serialization.Configuration
{
	/// <summary>Handles the XML elements used to configure serialization by the <see cref="T:System.Runtime.Serialization.DataContractSerializer" />.</summary>
	// Token: 0x020001A8 RID: 424
	public sealed class TypeElement : ConfigurationElement
	{
		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06001557 RID: 5463 RVA: 0x00054434 File Offset: 0x00052634
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("", typeof(ParameterElementCollection), null, null, null, ConfigurationPropertyOptions.IsDefaultCollection),
						new ConfigurationProperty("type", typeof(string), string.Empty, null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("index", typeof(int), 0, null, new IntegerValidator(0, int.MaxValue, false), ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.Configuration.TypeElement" /> class.</summary>
		// Token: 0x06001558 RID: 5464 RVA: 0x000544D8 File Offset: 0x000526D8
		public TypeElement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.Configuration.TypeElement" /> class with the specified type name.</summary>
		/// <param name="typeName">The name of the type that uses known types.</param>
		// Token: 0x06001559 RID: 5465 RVA: 0x00054504 File Offset: 0x00052704
		public TypeElement(string typeName) : this()
		{
			if (string.IsNullOrEmpty(typeName))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("typeName");
			}
			this.Type = typeName;
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x0600155A RID: 5466 RVA: 0x00054526 File Offset: 0x00052726
		internal string Key
		{
			get
			{
				return this.key;
			}
		}

		/// <summary>Gets a collection of parameters.</summary>
		/// <returns>A <see cref="T:System.Runtime.Serialization.Configuration.ParameterElementCollection" /> that contains the parameters for the type.</returns>
		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x0600155B RID: 5467 RVA: 0x0005422C File Offset: 0x0005242C
		[ConfigurationProperty("", DefaultValue = null, Options = ConfigurationPropertyOptions.IsDefaultCollection)]
		public ParameterElementCollection Parameters
		{
			get
			{
				return (ParameterElementCollection)base[""];
			}
		}

		// Token: 0x0600155C RID: 5468 RVA: 0x00054530 File Offset: 0x00052730
		protected override void Reset(ConfigurationElement parentElement)
		{
			TypeElement typeElement = (TypeElement)parentElement;
			this.key = typeElement.key;
			base.Reset(parentElement);
		}

		/// <summary>Gets or sets the name of the type.</summary>
		/// <returns>The name of the type.</returns>
		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x0600155D RID: 5469 RVA: 0x00053E6A File Offset: 0x0005206A
		// (set) Token: 0x0600155E RID: 5470 RVA: 0x00053E7C File Offset: 0x0005207C
		[StringValidator(MinLength = 0)]
		[ConfigurationProperty("type", DefaultValue = "")]
		public string Type
		{
			get
			{
				return (string)base["type"];
			}
			set
			{
				base["type"] = value;
			}
		}

		/// <summary>Gets or sets the position of the element.</summary>
		/// <returns>The position of the element.</returns>
		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x0600155F RID: 5471 RVA: 0x00054207 File Offset: 0x00052407
		// (set) Token: 0x06001560 RID: 5472 RVA: 0x00054219 File Offset: 0x00052419
		[IntegerValidator(MinValue = 0)]
		[ConfigurationProperty("index", DefaultValue = 0)]
		public int Index
		{
			get
			{
				return (int)base["index"];
			}
			set
			{
				base["index"] = value;
			}
		}

		// Token: 0x06001561 RID: 5473 RVA: 0x00054557 File Offset: 0x00052757
		internal Type GetType(string rootType, Type[] typeArgs)
		{
			return TypeElement.GetType(rootType, typeArgs, this.Type, this.Index, this.Parameters);
		}

		// Token: 0x06001562 RID: 5474 RVA: 0x00054574 File Offset: 0x00052774
		internal static Type GetType(string rootType, Type[] typeArgs, string type, int index, ParameterElementCollection parameters)
		{
			if (!string.IsNullOrEmpty(type))
			{
				Type type2 = System.Type.GetType(type, true);
				if (type2.IsGenericTypeDefinition)
				{
					if (parameters.Count != type2.GetGenericArguments().Length)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("Generic parameter count do not match between known type and configuration. Type is '{0}', known type has {1} parameters, configuration has {2} parameters.", new object[]
						{
							type,
							type2.GetGenericArguments().Length,
							parameters.Count
						}));
					}
					Type[] array = new Type[parameters.Count];
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = parameters[i].GetType(rootType, typeArgs);
					}
					type2 = type2.MakeGenericType(array);
				}
				return type2;
			}
			if (typeArgs != null && index < typeArgs.Length)
			{
				return typeArgs[index];
			}
			int num = (typeArgs == null) ? 0 : typeArgs.Length;
			if (num == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("For known type configuration, index is out of bound. Root type: '{0}' has {1} type arguments, and index was {2}.", new object[]
				{
					rootType,
					num,
					index
				}));
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("For known type configuration, index is out of bound. Root type: '{0}' has {1} type arguments, and index was {2}.", new object[]
			{
				rootType,
				num,
				index
			}));
		}

		// Token: 0x04000A88 RID: 2696
		private ConfigurationPropertyCollection properties;

		// Token: 0x04000A89 RID: 2697
		private string key = Guid.NewGuid().ToString();
	}
}
