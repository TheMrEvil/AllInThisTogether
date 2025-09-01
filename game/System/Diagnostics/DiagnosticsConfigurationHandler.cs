using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Reflection;
using System.Xml;

namespace System.Diagnostics
{
	/// <summary>Handles the diagnostics section of configuration files.</summary>
	// Token: 0x02000254 RID: 596
	[Obsolete("This class is obsoleted")]
	public class DiagnosticsConfigurationHandler : IConfigurationSectionHandler
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.DiagnosticsConfigurationHandler" /> class.</summary>
		// Token: 0x06001258 RID: 4696 RVA: 0x0004EE48 File Offset: 0x0004D048
		public DiagnosticsConfigurationHandler()
		{
			this.elementHandlers["assert"] = new DiagnosticsConfigurationHandler.ElementHandler(this.AddAssertNode);
			this.elementHandlers["performanceCounters"] = new DiagnosticsConfigurationHandler.ElementHandler(this.AddPerformanceCountersNode);
			this.elementHandlers["switches"] = new DiagnosticsConfigurationHandler.ElementHandler(this.AddSwitchesNode);
			this.elementHandlers["trace"] = new DiagnosticsConfigurationHandler.ElementHandler(this.AddTraceNode);
			this.elementHandlers["sources"] = new DiagnosticsConfigurationHandler.ElementHandler(this.AddSourcesNode);
		}

		/// <summary>Parses the configuration settings for the &lt;system.diagnostics&gt; Element section of configuration files.</summary>
		/// <param name="parent">The object inherited from the parent path</param>
		/// <param name="configContext">Reserved. Used in ASP.NET to convey the virtual path of the configuration being evaluated.</param>
		/// <param name="section">The root XML node at the section to handle.</param>
		/// <returns>A new configuration object, in the form of a <see cref="T:System.Collections.Hashtable" />.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">Switches could not be found.  
		///  -or-  
		///  Assert could not be found.  
		///  -or-  
		///  Trace could not be found.  
		///  -or-  
		///  Performance counters could not be found.</exception>
		// Token: 0x06001259 RID: 4697 RVA: 0x0004EEF4 File Offset: 0x0004D0F4
		public virtual object Create(object parent, object configContext, XmlNode section)
		{
			IDictionary dictionary;
			if (parent == null)
			{
				dictionary = new Hashtable(CaseInsensitiveHashCodeProvider.Default, CaseInsensitiveComparer.Default);
			}
			else
			{
				dictionary = (IDictionary)((ICloneable)parent).Clone();
			}
			if (dictionary.Contains(".__TraceInfoSettingsKey__."))
			{
				this.configValues = (TraceImplSettings)dictionary[".__TraceInfoSettingsKey__."];
			}
			else
			{
				dictionary.Add(".__TraceInfoSettingsKey__.", this.configValues = new TraceImplSettings());
			}
			foreach (object obj in section.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.NodeType == XmlNodeType.Element && !(xmlNode.LocalName != "sharedListeners"))
				{
					this.AddTraceListeners(dictionary, xmlNode, this.GetSharedListeners(dictionary));
				}
			}
			foreach (object obj2 in section.ChildNodes)
			{
				XmlNode xmlNode2 = (XmlNode)obj2;
				XmlNodeType nodeType = xmlNode2.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType != XmlNodeType.Comment && nodeType != XmlNodeType.Whitespace)
					{
						this.ThrowUnrecognizedElement(xmlNode2);
					}
				}
				else if (!(xmlNode2.LocalName == "sharedListeners"))
				{
					DiagnosticsConfigurationHandler.ElementHandler elementHandler = (DiagnosticsConfigurationHandler.ElementHandler)this.elementHandlers[xmlNode2.Name];
					if (elementHandler != null)
					{
						elementHandler(dictionary, xmlNode2);
					}
					else
					{
						this.ThrowUnrecognizedElement(xmlNode2);
					}
				}
			}
			return dictionary;
		}

		// Token: 0x0600125A RID: 4698 RVA: 0x0004F088 File Offset: 0x0004D288
		private void AddAssertNode(IDictionary d, XmlNode node)
		{
			XmlAttributeCollection attributes = node.Attributes;
			string attribute = this.GetAttribute(attributes, "assertuienabled", false, node);
			string attribute2 = this.GetAttribute(attributes, "logfilename", false, node);
			this.ValidateInvalidAttributes(attributes, node);
			if (attribute != null)
			{
				try
				{
					d["assertuienabled"] = bool.Parse(attribute);
				}
				catch (Exception inner)
				{
					throw new ConfigurationException("The `assertuienabled' attribute must be `true' or `false'", inner, node);
				}
			}
			if (attribute2 != null)
			{
				d["logfilename"] = attribute2;
			}
			DefaultTraceListener defaultTraceListener = (DefaultTraceListener)this.configValues.Listeners["Default"];
			if (defaultTraceListener != null)
			{
				if (attribute != null)
				{
					defaultTraceListener.AssertUiEnabled = (bool)d["assertuienabled"];
				}
				if (attribute2 != null)
				{
					defaultTraceListener.LogFileName = attribute2;
				}
			}
			if (node.ChildNodes.Count > 0)
			{
				this.ThrowUnrecognizedElement(node.ChildNodes[0]);
			}
		}

		// Token: 0x0600125B RID: 4699 RVA: 0x0004F170 File Offset: 0x0004D370
		private void AddPerformanceCountersNode(IDictionary d, XmlNode node)
		{
			XmlAttributeCollection attributes = node.Attributes;
			string attribute = this.GetAttribute(attributes, "filemappingsize", false, node);
			this.ValidateInvalidAttributes(attributes, node);
			if (attribute != null)
			{
				try
				{
					d["filemappingsize"] = int.Parse(attribute);
				}
				catch (Exception inner)
				{
					throw new ConfigurationException("The `filemappingsize' attribute must be an integral value.", inner, node);
				}
			}
			if (node.ChildNodes.Count > 0)
			{
				this.ThrowUnrecognizedElement(node.ChildNodes[0]);
			}
		}

		// Token: 0x0600125C RID: 4700 RVA: 0x0004F1F4 File Offset: 0x0004D3F4
		private void AddSwitchesNode(IDictionary d, XmlNode node)
		{
			this.ValidateInvalidAttributes(node.Attributes, node);
			IDictionary dictionary = new Hashtable();
			foreach (object obj in node.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				XmlNodeType nodeType = xmlNode.NodeType;
				if (nodeType != XmlNodeType.Whitespace && nodeType != XmlNodeType.Comment)
				{
					if (nodeType == XmlNodeType.Element)
					{
						XmlAttributeCollection attributes = xmlNode.Attributes;
						string name = xmlNode.Name;
						if (!(name == "add"))
						{
							if (!(name == "remove"))
							{
								if (!(name == "clear"))
								{
									this.ThrowUnrecognizedElement(xmlNode);
								}
								else
								{
									dictionary.Clear();
								}
							}
							else
							{
								string attribute = this.GetAttribute(attributes, "name", true, xmlNode);
								dictionary.Remove(attribute);
							}
						}
						else
						{
							string attribute = this.GetAttribute(attributes, "name", true, xmlNode);
							string attribute2 = this.GetAttribute(attributes, "value", true, xmlNode);
							dictionary[attribute] = DiagnosticsConfigurationHandler.GetSwitchValue(attribute, attribute2);
						}
						this.ValidateInvalidAttributes(attributes, xmlNode);
					}
					else
					{
						this.ThrowUnrecognizedNode(xmlNode);
					}
				}
			}
			d[node.Name] = dictionary;
		}

		// Token: 0x0600125D RID: 4701 RVA: 0x00003914 File Offset: 0x00001B14
		private static object GetSwitchValue(string name, string value)
		{
			return value;
		}

		// Token: 0x0600125E RID: 4702 RVA: 0x0004F340 File Offset: 0x0004D540
		private void AddTraceNode(IDictionary d, XmlNode node)
		{
			this.AddTraceAttributes(d, node);
			foreach (object obj in node.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				XmlNodeType nodeType = xmlNode.NodeType;
				if (nodeType != XmlNodeType.Whitespace && nodeType != XmlNodeType.Comment)
				{
					if (nodeType == XmlNodeType.Element)
					{
						if (xmlNode.Name == "listeners")
						{
							this.AddTraceListeners(d, xmlNode, this.configValues.Listeners);
						}
						else
						{
							this.ThrowUnrecognizedElement(xmlNode);
						}
						this.ValidateInvalidAttributes(xmlNode.Attributes, xmlNode);
					}
					else
					{
						this.ThrowUnrecognizedNode(xmlNode);
					}
				}
			}
		}

		// Token: 0x0600125F RID: 4703 RVA: 0x0004F3F4 File Offset: 0x0004D5F4
		private void AddTraceAttributes(IDictionary d, XmlNode node)
		{
			XmlAttributeCollection attributes = node.Attributes;
			string attribute = this.GetAttribute(attributes, "autoflush", false, node);
			string attribute2 = this.GetAttribute(attributes, "indentsize", false, node);
			this.ValidateInvalidAttributes(attributes, node);
			if (attribute != null)
			{
				bool flag = false;
				try
				{
					flag = bool.Parse(attribute);
					d["autoflush"] = flag;
				}
				catch (Exception inner)
				{
					throw new ConfigurationException("The `autoflush' attribute must be `true' or `false'", inner, node);
				}
				this.configValues.AutoFlush = flag;
			}
			if (attribute2 != null)
			{
				int num = 0;
				try
				{
					num = int.Parse(attribute2);
					d["indentsize"] = num;
				}
				catch (Exception inner2)
				{
					throw new ConfigurationException("The `indentsize' attribute must be an integral value.", inner2, node);
				}
				this.configValues.IndentSize = num;
			}
		}

		// Token: 0x06001260 RID: 4704 RVA: 0x0004F4C4 File Offset: 0x0004D6C4
		private TraceListenerCollection GetSharedListeners(IDictionary d)
		{
			TraceListenerCollection traceListenerCollection = d["sharedListeners"] as TraceListenerCollection;
			if (traceListenerCollection == null)
			{
				traceListenerCollection = new TraceListenerCollection();
				d["sharedListeners"] = traceListenerCollection;
			}
			return traceListenerCollection;
		}

		// Token: 0x06001261 RID: 4705 RVA: 0x0004F4F8 File Offset: 0x0004D6F8
		private void AddSourcesNode(IDictionary d, XmlNode node)
		{
			this.ValidateInvalidAttributes(node.Attributes, node);
			Hashtable hashtable = d["sources"] as Hashtable;
			if (hashtable == null)
			{
				hashtable = new Hashtable();
				d["sources"] = hashtable;
			}
			foreach (object obj in node.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				XmlNodeType nodeType = xmlNode.NodeType;
				if (nodeType != XmlNodeType.Whitespace && nodeType != XmlNodeType.Comment)
				{
					if (nodeType == XmlNodeType.Element)
					{
						if (xmlNode.Name == "source")
						{
							this.AddTraceSource(d, hashtable, xmlNode);
						}
						else
						{
							this.ThrowUnrecognizedElement(xmlNode);
						}
					}
					else
					{
						this.ThrowUnrecognizedNode(xmlNode);
					}
				}
			}
		}

		// Token: 0x06001262 RID: 4706 RVA: 0x0004F5C4 File Offset: 0x0004D7C4
		private void AddTraceSource(IDictionary d, Hashtable sources, XmlNode node)
		{
			string text = null;
			SourceLevels levels = SourceLevels.Error;
			StringDictionary stringDictionary = new StringDictionary();
			foreach (object obj in node.Attributes)
			{
				XmlAttribute xmlAttribute = (XmlAttribute)obj;
				string name = xmlAttribute.Name;
				if (!(name == "name"))
				{
					if (!(name == "switchValue"))
					{
						stringDictionary[xmlAttribute.Name] = xmlAttribute.Value;
					}
					else
					{
						levels = (SourceLevels)Enum.Parse(typeof(SourceLevels), xmlAttribute.Value);
					}
				}
				else
				{
					text = xmlAttribute.Value;
				}
			}
			if (text == null)
			{
				throw new ConfigurationException("Mandatory attribute 'name' is missing in 'source' element.");
			}
			if (sources.ContainsKey(text))
			{
				return;
			}
			TraceSourceInfo traceSourceInfo = new TraceSourceInfo(text, levels, this.configValues);
			sources.Add(traceSourceInfo.Name, traceSourceInfo);
			foreach (object obj2 in node.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj2;
				XmlNodeType nodeType = xmlNode.NodeType;
				if (nodeType != XmlNodeType.Whitespace && nodeType != XmlNodeType.Comment)
				{
					if (nodeType == XmlNodeType.Element)
					{
						if (xmlNode.Name == "listeners")
						{
							this.AddTraceListeners(d, xmlNode, traceSourceInfo.Listeners);
						}
						else
						{
							this.ThrowUnrecognizedElement(xmlNode);
						}
						this.ValidateInvalidAttributes(xmlNode.Attributes, xmlNode);
					}
					else
					{
						this.ThrowUnrecognizedNode(xmlNode);
					}
				}
			}
		}

		// Token: 0x06001263 RID: 4707 RVA: 0x0004F768 File Offset: 0x0004D968
		private void AddTraceListeners(IDictionary d, XmlNode listenersNode, TraceListenerCollection listeners)
		{
			this.ValidateInvalidAttributes(listenersNode.Attributes, listenersNode);
			foreach (object obj in listenersNode.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				XmlNodeType nodeType = xmlNode.NodeType;
				if (nodeType != XmlNodeType.Whitespace && nodeType != XmlNodeType.Comment)
				{
					if (nodeType == XmlNodeType.Element)
					{
						XmlAttributeCollection attributes = xmlNode.Attributes;
						string name = xmlNode.Name;
						if (!(name == "add"))
						{
							if (!(name == "remove"))
							{
								if (!(name == "clear"))
								{
									this.ThrowUnrecognizedElement(xmlNode);
								}
								else
								{
									this.configValues.Listeners.Clear();
								}
							}
							else
							{
								string attribute = this.GetAttribute(attributes, "name", true, xmlNode);
								this.RemoveTraceListener(attribute);
							}
						}
						else
						{
							this.AddTraceListener(d, xmlNode, attributes, listeners);
						}
						this.ValidateInvalidAttributes(attributes, xmlNode);
					}
					else
					{
						this.ThrowUnrecognizedNode(xmlNode);
					}
				}
			}
		}

		// Token: 0x06001264 RID: 4708 RVA: 0x0004F87C File Offset: 0x0004DA7C
		private void AddTraceListener(IDictionary d, XmlNode child, XmlAttributeCollection attributes, TraceListenerCollection listeners)
		{
			string attribute = this.GetAttribute(attributes, "name", true, child);
			string attribute2 = this.GetAttribute(attributes, "type", false, child);
			if (attribute2 == null)
			{
				TraceListener traceListener = this.GetSharedListeners(d)[attribute];
				if (traceListener == null)
				{
					throw new ConfigurationException(string.Format("Shared trace listener {0} does not exist.", attribute));
				}
				if (attributes.Count != 0)
				{
					throw new ConfigurationErrorsException(string.Format("Listener '{0}' references a shared listener and can only have a 'Name' attribute.", attribute));
				}
				traceListener.IndentSize = this.configValues.IndentSize;
				listeners.Add(traceListener);
				return;
			}
			else
			{
				Type type = Type.GetType(attribute2);
				if (type == null)
				{
					throw new ConfigurationException(string.Format("Invalid Type Specified: {0}", attribute2));
				}
				string attribute3 = this.GetAttribute(attributes, "initializeData", false, child);
				object[] parameters;
				Type[] types;
				if (attribute3 != null)
				{
					parameters = new object[]
					{
						attribute3
					};
					types = new Type[]
					{
						typeof(string)
					};
				}
				else
				{
					parameters = null;
					types = Type.EmptyTypes;
				}
				BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public;
				if (type.Assembly == base.GetType().Assembly)
				{
					bindingFlags |= BindingFlags.NonPublic;
				}
				ConstructorInfo constructor = type.GetConstructor(bindingFlags, null, types, null);
				if (constructor == null)
				{
					throw new ConfigurationException("Couldn't find constructor for class " + attribute2);
				}
				TraceListener traceListener2 = (TraceListener)constructor.Invoke(parameters);
				traceListener2.Name = attribute;
				string attribute4 = this.GetAttribute(attributes, "traceOutputOptions", false, child);
				if (attribute4 != null)
				{
					if (attribute4 != attribute4.Trim())
					{
						throw new ConfigurationErrorsException(string.Format("Invalid value '{0}' for 'traceOutputOptions'.", attribute4), child);
					}
					TraceOptions traceOutputOptions;
					try
					{
						traceOutputOptions = (TraceOptions)Enum.Parse(typeof(TraceOptions), attribute4);
					}
					catch (ArgumentException)
					{
						throw new ConfigurationErrorsException(string.Format("Invalid value '{0}' for 'traceOutputOptions'.", attribute4), child);
					}
					traceListener2.TraceOutputOptions = traceOutputOptions;
				}
				string[] supportedAttributes = traceListener2.GetSupportedAttributes();
				if (supportedAttributes != null)
				{
					foreach (string text in supportedAttributes)
					{
						string attribute5 = this.GetAttribute(attributes, text, false, child);
						if (attribute5 != null)
						{
							traceListener2.Attributes.Add(text, attribute5);
						}
					}
				}
				traceListener2.IndentSize = this.configValues.IndentSize;
				listeners.Add(traceListener2);
				return;
			}
		}

		// Token: 0x06001265 RID: 4709 RVA: 0x0004FAA8 File Offset: 0x0004DCA8
		private void RemoveTraceListener(string name)
		{
			try
			{
				this.configValues.Listeners.Remove(name);
			}
			catch (ArgumentException)
			{
			}
			catch (Exception inner)
			{
				throw new ConfigurationException(string.Format("Unknown error removing listener: {0}", name), inner);
			}
		}

		// Token: 0x06001266 RID: 4710 RVA: 0x0004FAFC File Offset: 0x0004DCFC
		private string GetAttribute(XmlAttributeCollection attrs, string attr, bool required, XmlNode node)
		{
			XmlAttribute xmlAttribute = attrs[attr];
			string text = null;
			if (xmlAttribute != null)
			{
				text = xmlAttribute.Value;
				if (required)
				{
					this.ValidateAttribute(attr, text, node);
				}
				attrs.Remove(xmlAttribute);
			}
			else if (required)
			{
				this.ThrowMissingAttribute(attr, node);
			}
			return text;
		}

		// Token: 0x06001267 RID: 4711 RVA: 0x0004FB41 File Offset: 0x0004DD41
		private void ValidateAttribute(string attribute, string value, XmlNode node)
		{
			if (value == null || value.Length == 0)
			{
				throw new ConfigurationException(string.Format("Required attribute '{0}' cannot be empty.", attribute), node);
			}
		}

		// Token: 0x06001268 RID: 4712 RVA: 0x0004FB60 File Offset: 0x0004DD60
		private void ValidateInvalidAttributes(XmlAttributeCollection c, XmlNode node)
		{
			if (c.Count != 0)
			{
				this.ThrowUnrecognizedAttribute(c[0].Name, node);
			}
		}

		// Token: 0x06001269 RID: 4713 RVA: 0x0004FB7D File Offset: 0x0004DD7D
		private void ThrowMissingAttribute(string attribute, XmlNode node)
		{
			throw new ConfigurationException(string.Format("Required attribute '{0}' not found.", attribute), node);
		}

		// Token: 0x0600126A RID: 4714 RVA: 0x0004FB90 File Offset: 0x0004DD90
		private void ThrowUnrecognizedNode(XmlNode node)
		{
			throw new ConfigurationException(string.Format("Unrecognized node `{0}'; nodeType={1}", node.Name, node.NodeType), node);
		}

		// Token: 0x0600126B RID: 4715 RVA: 0x0004FBB3 File Offset: 0x0004DDB3
		private void ThrowUnrecognizedElement(XmlNode node)
		{
			throw new ConfigurationException(string.Format("Unrecognized element '{0}'.", node.Name), node);
		}

		// Token: 0x0600126C RID: 4716 RVA: 0x0004FBCB File Offset: 0x0004DDCB
		private void ThrowUnrecognizedAttribute(string attribute, XmlNode node)
		{
			throw new ConfigurationException(string.Format("Unrecognized attribute '{0}' on element <{1}/>.", attribute, node.Name), node);
		}

		// Token: 0x04000AA5 RID: 2725
		private TraceImplSettings configValues;

		// Token: 0x04000AA6 RID: 2726
		private IDictionary elementHandlers = new Hashtable();

		// Token: 0x02000255 RID: 597
		// (Invoke) Token: 0x0600126E RID: 4718
		private delegate void ElementHandler(IDictionary d, XmlNode node);
	}
}
