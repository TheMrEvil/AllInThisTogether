using System;
using System.Collections;
using System.Configuration;
using System.Xml;

namespace System.Diagnostics
{
	// Token: 0x0200021C RID: 540
	internal class ListenerElement : TypedElement
	{
		// Token: 0x06000FAC RID: 4012 RVA: 0x00045944 File Offset: 0x00043B44
		static ListenerElement()
		{
		}

		// Token: 0x06000FAD RID: 4013 RVA: 0x000459A8 File Offset: 0x00043BA8
		public ListenerElement(bool allowReferences) : base(typeof(TraceListener))
		{
			this._allowReferences = allowReferences;
			ConfigurationPropertyOptions configurationPropertyOptions = ConfigurationPropertyOptions.None;
			if (!this._allowReferences)
			{
				configurationPropertyOptions |= ConfigurationPropertyOptions.IsRequired;
			}
			this._propListenerTypeName = new ConfigurationProperty("type", typeof(string), null, configurationPropertyOptions);
			this._properties.Remove("type");
			this._properties.Add(this._propListenerTypeName);
			this._properties.Add(ListenerElement._propFilter);
			this._properties.Add(ListenerElement._propName);
			this._properties.Add(ListenerElement._propOutputOpts);
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000FAE RID: 4014 RVA: 0x00045A48 File Offset: 0x00043C48
		public Hashtable Attributes
		{
			get
			{
				if (this._attributes == null)
				{
					this._attributes = new Hashtable(StringComparer.OrdinalIgnoreCase);
				}
				return this._attributes;
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000FAF RID: 4015 RVA: 0x00045A68 File Offset: 0x00043C68
		[ConfigurationProperty("filter")]
		public FilterElement Filter
		{
			get
			{
				return (FilterElement)base[ListenerElement._propFilter];
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000FB0 RID: 4016 RVA: 0x00045A7A File Offset: 0x00043C7A
		// (set) Token: 0x06000FB1 RID: 4017 RVA: 0x00045A8C File Offset: 0x00043C8C
		[ConfigurationProperty("name", IsRequired = true, IsKey = true)]
		public string Name
		{
			get
			{
				return (string)base[ListenerElement._propName];
			}
			set
			{
				base[ListenerElement._propName] = value;
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000FB2 RID: 4018 RVA: 0x00045A9A File Offset: 0x00043C9A
		// (set) Token: 0x06000FB3 RID: 4019 RVA: 0x00045AAC File Offset: 0x00043CAC
		[ConfigurationProperty("traceOutputOptions", DefaultValue = TraceOptions.None)]
		public TraceOptions TraceOutputOptions
		{
			get
			{
				return (TraceOptions)base[ListenerElement._propOutputOpts];
			}
			set
			{
				base[ListenerElement._propOutputOpts] = value;
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000FB4 RID: 4020 RVA: 0x00045ABF File Offset: 0x00043CBF
		// (set) Token: 0x06000FB5 RID: 4021 RVA: 0x00045AD2 File Offset: 0x00043CD2
		[ConfigurationProperty("type")]
		public override string TypeName
		{
			get
			{
				return (string)base[this._propListenerTypeName];
			}
			set
			{
				base[this._propListenerTypeName] = value;
			}
		}

		// Token: 0x06000FB6 RID: 4022 RVA: 0x00045AE4 File Offset: 0x00043CE4
		public override bool Equals(object compareTo)
		{
			if (this.Name.Equals("Default") && this.TypeName.Equals(typeof(DefaultTraceListener).FullName))
			{
				ListenerElement listenerElement = compareTo as ListenerElement;
				return listenerElement != null && listenerElement.Name.Equals("Default") && listenerElement.TypeName.Equals(typeof(DefaultTraceListener).FullName);
			}
			return base.Equals(compareTo);
		}

		// Token: 0x06000FB7 RID: 4023 RVA: 0x00045B5F File Offset: 0x00043D5F
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000FB8 RID: 4024 RVA: 0x00045B68 File Offset: 0x00043D68
		public TraceListener GetRuntimeObject()
		{
			if (this._runtimeObject != null)
			{
				return (TraceListener)this._runtimeObject;
			}
			TraceListener result;
			try
			{
				if (string.IsNullOrEmpty(this.TypeName))
				{
					if (this._attributes != null || base.ElementInformation.Properties[ListenerElement._propFilter.Name].ValueOrigin == PropertyValueOrigin.SetHere || this.TraceOutputOptions != TraceOptions.None || !string.IsNullOrEmpty(base.InitData))
					{
						throw new ConfigurationErrorsException(SR.GetString("A listener with no type name specified references the sharedListeners section and cannot have any attributes other than 'Name'.  Listener: '{0}'.", new object[]
						{
							this.Name
						}));
					}
					if (DiagnosticsConfiguration.SharedListeners == null)
					{
						throw new ConfigurationErrorsException(SR.GetString("Listener '{0}' does not exist in the sharedListeners section.", new object[]
						{
							this.Name
						}));
					}
					ListenerElement listenerElement = DiagnosticsConfiguration.SharedListeners[this.Name];
					if (listenerElement == null)
					{
						throw new ConfigurationErrorsException(SR.GetString("Listener '{0}' does not exist in the sharedListeners section.", new object[]
						{
							this.Name
						}));
					}
					this._runtimeObject = listenerElement.GetRuntimeObject();
					result = (TraceListener)this._runtimeObject;
				}
				else
				{
					TraceListener traceListener = (TraceListener)base.BaseGetRuntimeObject();
					traceListener.initializeData = base.InitData;
					traceListener.Name = this.Name;
					traceListener.SetAttributes(this.Attributes);
					traceListener.TraceOutputOptions = this.TraceOutputOptions;
					if (this.Filter != null && this.Filter.TypeName != null && this.Filter.TypeName.Length != 0)
					{
						traceListener.Filter = this.Filter.GetRuntimeObject();
						XmlWriterTraceListener xmlWriterTraceListener = traceListener as XmlWriterTraceListener;
						if (xmlWriterTraceListener != null)
						{
							xmlWriterTraceListener.shouldRespectFilterOnTraceTransfer = true;
						}
					}
					this._runtimeObject = traceListener;
					result = traceListener;
				}
			}
			catch (ArgumentException inner)
			{
				throw new ConfigurationErrorsException(SR.GetString("Couldn't create listener '{0}'.", new object[]
				{
					this.Name
				}), inner);
			}
			return result;
		}

		// Token: 0x06000FB9 RID: 4025 RVA: 0x00045D40 File Offset: 0x00043F40
		protected override bool OnDeserializeUnrecognizedAttribute(string name, string value)
		{
			this.Attributes.Add(name, value);
			return true;
		}

		// Token: 0x06000FBA RID: 4026 RVA: 0x00045D50 File Offset: 0x00043F50
		protected override void PreSerialize(XmlWriter writer)
		{
			if (this._attributes != null)
			{
				IDictionaryEnumerator enumerator = this._attributes.GetEnumerator();
				while (enumerator.MoveNext())
				{
					string text = (string)enumerator.Value;
					string localName = (string)enumerator.Key;
					if (text != null && writer != null)
					{
						writer.WriteAttributeString(localName, text);
					}
				}
			}
		}

		// Token: 0x06000FBB RID: 4027 RVA: 0x00045DA1 File Offset: 0x00043FA1
		protected override bool SerializeElement(XmlWriter writer, bool serializeCollectionKey)
		{
			return base.SerializeElement(writer, serializeCollectionKey) || (this._attributes != null && this._attributes.Count > 0);
		}

		// Token: 0x06000FBC RID: 4028 RVA: 0x00045DC8 File Offset: 0x00043FC8
		protected override void Unmerge(ConfigurationElement sourceElement, ConfigurationElement parentElement, ConfigurationSaveMode saveMode)
		{
			base.Unmerge(sourceElement, parentElement, saveMode);
			ListenerElement listenerElement = sourceElement as ListenerElement;
			if (listenerElement != null && listenerElement._attributes != null)
			{
				this._attributes = listenerElement._attributes;
			}
		}

		// Token: 0x06000FBD RID: 4029 RVA: 0x00045DFC File Offset: 0x00043FFC
		internal void ResetProperties()
		{
			if (this._attributes != null)
			{
				this._attributes.Clear();
				this._properties.Clear();
				this._properties.Add(this._propListenerTypeName);
				this._properties.Add(ListenerElement._propFilter);
				this._properties.Add(ListenerElement._propName);
				this._properties.Add(ListenerElement._propOutputOpts);
			}
		}

		// Token: 0x06000FBE RID: 4030 RVA: 0x00045E68 File Offset: 0x00044068
		internal TraceListener RefreshRuntimeObject(TraceListener listener)
		{
			this._runtimeObject = null;
			TraceListener result;
			try
			{
				string typeName = this.TypeName;
				if (string.IsNullOrEmpty(typeName))
				{
					if (this._attributes != null || base.ElementInformation.Properties[ListenerElement._propFilter.Name].ValueOrigin == PropertyValueOrigin.SetHere || this.TraceOutputOptions != TraceOptions.None || !string.IsNullOrEmpty(base.InitData))
					{
						throw new ConfigurationErrorsException(SR.GetString("A listener with no type name specified references the sharedListeners section and cannot have any attributes other than 'Name'.  Listener: '{0}'.", new object[]
						{
							this.Name
						}));
					}
					if (DiagnosticsConfiguration.SharedListeners == null)
					{
						throw new ConfigurationErrorsException(SR.GetString("Listener '{0}' does not exist in the sharedListeners section.", new object[]
						{
							this.Name
						}));
					}
					ListenerElement listenerElement = DiagnosticsConfiguration.SharedListeners[this.Name];
					if (listenerElement == null)
					{
						throw new ConfigurationErrorsException(SR.GetString("Listener '{0}' does not exist in the sharedListeners section.", new object[]
						{
							this.Name
						}));
					}
					this._runtimeObject = listenerElement.RefreshRuntimeObject(listener);
					result = (TraceListener)this._runtimeObject;
				}
				else if (Type.GetType(typeName) != listener.GetType() || base.InitData != listener.initializeData)
				{
					result = this.GetRuntimeObject();
				}
				else
				{
					listener.SetAttributes(this.Attributes);
					listener.TraceOutputOptions = this.TraceOutputOptions;
					if (listener.Filter != null)
					{
						if (base.ElementInformation.Properties[ListenerElement._propFilter.Name].ValueOrigin == PropertyValueOrigin.SetHere || base.ElementInformation.Properties[ListenerElement._propFilter.Name].ValueOrigin == PropertyValueOrigin.Inherited)
						{
							listener.Filter = this.Filter.RefreshRuntimeObject(listener.Filter);
						}
						else
						{
							listener.Filter = null;
						}
					}
					this._runtimeObject = listener;
					result = listener;
				}
			}
			catch (ArgumentException inner)
			{
				throw new ConfigurationErrorsException(SR.GetString("Couldn't create listener '{0}'.", new object[]
				{
					this.Name
				}), inner);
			}
			return result;
		}

		// Token: 0x0400099A RID: 2458
		private static readonly ConfigurationProperty _propFilter = new ConfigurationProperty("filter", typeof(FilterElement), null, ConfigurationPropertyOptions.None);

		// Token: 0x0400099B RID: 2459
		private static readonly ConfigurationProperty _propName = new ConfigurationProperty("name", typeof(string), null, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x0400099C RID: 2460
		private static readonly ConfigurationProperty _propOutputOpts = new ConfigurationProperty("traceOutputOptions", typeof(TraceOptions), TraceOptions.None, ConfigurationPropertyOptions.None);

		// Token: 0x0400099D RID: 2461
		private ConfigurationProperty _propListenerTypeName;

		// Token: 0x0400099E RID: 2462
		private bool _allowReferences;

		// Token: 0x0400099F RID: 2463
		private Hashtable _attributes;

		// Token: 0x040009A0 RID: 2464
		internal bool _isAddedByDefault;
	}
}
