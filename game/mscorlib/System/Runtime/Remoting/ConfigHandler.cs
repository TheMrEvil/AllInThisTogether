using System;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Lifetime;
using Mono.Xml;

namespace System.Runtime.Remoting
{
	// Token: 0x02000566 RID: 1382
	internal class ConfigHandler : SmallXmlParser.IContentHandler
	{
		// Token: 0x06003646 RID: 13894 RVA: 0x000C3607 File Offset: 0x000C1807
		public ConfigHandler(bool onlyDelayedChannels)
		{
			this.onlyDelayedChannels = onlyDelayedChannels;
		}

		// Token: 0x06003647 RID: 13895 RVA: 0x000C3638 File Offset: 0x000C1838
		private void ValidatePath(string element, params string[] paths)
		{
			foreach (string path in paths)
			{
				if (this.CheckPath(path))
				{
					return;
				}
			}
			throw new RemotingException("Element " + element + " not allowed in this context");
		}

		// Token: 0x06003648 RID: 13896 RVA: 0x000C3678 File Offset: 0x000C1878
		private bool CheckPath(string path)
		{
			CompareInfo compareInfo = CultureInfo.InvariantCulture.CompareInfo;
			if (compareInfo.IsPrefix(path, "/", CompareOptions.Ordinal))
			{
				return path == this.currentXmlPath;
			}
			return compareInfo.IsSuffix(this.currentXmlPath, path, CompareOptions.Ordinal);
		}

		// Token: 0x06003649 RID: 13897 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public void OnStartParsing(SmallXmlParser parser)
		{
		}

		// Token: 0x0600364A RID: 13898 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public void OnProcessingInstruction(string name, string text)
		{
		}

		// Token: 0x0600364B RID: 13899 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public void OnIgnorableWhitespace(string s)
		{
		}

		// Token: 0x0600364C RID: 13900 RVA: 0x000C36C4 File Offset: 0x000C18C4
		public void OnStartElement(string name, SmallXmlParser.IAttrList attrs)
		{
			try
			{
				if (this.currentXmlPath.StartsWith("/configuration/system.runtime.remoting"))
				{
					this.ParseElement(name, attrs);
				}
				this.currentXmlPath = this.currentXmlPath + "/" + name;
			}
			catch (Exception ex)
			{
				throw new RemotingException("Error in element " + name + ": " + ex.Message, ex);
			}
		}

		// Token: 0x0600364D RID: 13901 RVA: 0x000C3734 File Offset: 0x000C1934
		public void ParseElement(string name, SmallXmlParser.IAttrList attrs)
		{
			if (this.currentProviderData != null)
			{
				this.ReadCustomProviderData(name, attrs);
				return;
			}
			uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
			if (num <= 1889220888U)
			{
				if (num <= 1338032792U)
				{
					if (num <= 566383268U)
					{
						if (num != 524788293U)
						{
							if (num == 566383268U)
							{
								if (name == "channel")
								{
									this.ValidatePath(name, new string[]
									{
										"channels"
									});
									if (this.currentXmlPath.IndexOf("application") != -1)
									{
										this.ReadChannel(attrs, false);
										return;
									}
									this.ReadChannel(attrs, true);
									return;
								}
							}
						}
						else if (name == "application")
						{
							this.ValidatePath(name, new string[]
							{
								"system.runtime.remoting"
							});
							if (attrs.Names.Length != 0)
							{
								this.appName = attrs.Values[0];
								return;
							}
							return;
						}
					}
					else if (num != 653843437U)
					{
						if (num == 1338032792U)
						{
							if (name == "wellknown")
							{
								this.ValidatePath(name, new string[]
								{
									"client",
									"service"
								});
								if (this.CheckPath("client"))
								{
									this.ReadClientWellKnown(attrs);
									return;
								}
								this.ReadServiceWellKnown(attrs);
								return;
							}
						}
					}
					else if (name == "interopXmlElement")
					{
						this.ValidatePath(name, new string[]
						{
							"soapInterop"
						});
						this.ReadInteropXml(attrs, false);
						return;
					}
				}
				else if (num <= 1457512036U)
				{
					if (num != 1376955374U)
					{
						if (num == 1457512036U)
						{
							if (name == "service")
							{
								this.ValidatePath(name, new string[]
								{
									"application"
								});
								return;
							}
						}
					}
					else if (name == "lifetime")
					{
						this.ValidatePath(name, new string[]
						{
							"application"
						});
						this.ReadLifetine(attrs);
						return;
					}
				}
				else if (num != 1483009432U)
				{
					if (num != 1743807633U)
					{
						if (num == 1889220888U)
						{
							if (name == "clientProviders")
							{
								this.ValidatePath(name, new string[]
								{
									"channelSinkProviders",
									"channel"
								});
								return;
							}
						}
					}
					else if (name == "customErrors")
					{
						this.ValidatePath(name, new string[]
						{
							"system.runtime.remoting"
						});
						RemotingConfiguration.SetCustomErrorsMode(attrs.GetValue("mode"));
						return;
					}
				}
				else if (name == "debug")
				{
					this.ValidatePath(name, new string[]
					{
						"system.runtime.remoting"
					});
					return;
				}
			}
			else if (num <= 3082861500U)
			{
				if (num <= 2837523493U)
				{
					if (num != 2408750110U)
					{
						if (num != 2837523493U)
						{
							goto IL_5DF;
						}
						if (!(name == "formatter"))
						{
							goto IL_5DF;
						}
					}
					else
					{
						if (!(name == "client"))
						{
							goto IL_5DF;
						}
						this.ValidatePath(name, new string[]
						{
							"application"
						});
						this.currentClientUrl = attrs.GetValue("url");
						return;
					}
				}
				else if (num != 2866667388U)
				{
					if (num != 2988283755U)
					{
						if (num != 3082861500U)
						{
							goto IL_5DF;
						}
						if (!(name == "provider"))
						{
							goto IL_5DF;
						}
					}
					else
					{
						if (!(name == "soapInterop"))
						{
							goto IL_5DF;
						}
						this.ValidatePath(name, new string[]
						{
							"application"
						});
						return;
					}
				}
				else
				{
					if (!(name == "activated"))
					{
						goto IL_5DF;
					}
					this.ValidatePath(name, new string[]
					{
						"client",
						"service"
					});
					if (this.CheckPath("client"))
					{
						this.ReadClientActivated(attrs);
						return;
					}
					this.ReadServiceActivated(attrs);
					return;
				}
				if (this.CheckPath("application/channels/channel/serverProviders") || this.CheckPath("channels/channel/serverProviders"))
				{
					ProviderData providerData = this.ReadProvider(name, attrs, false);
					this.currentChannel.ServerProviders.Add(providerData);
					return;
				}
				if (this.CheckPath("application/channels/channel/clientProviders") || this.CheckPath("channels/channel/clientProviders"))
				{
					ProviderData providerData = this.ReadProvider(name, attrs, false);
					this.currentChannel.ClientProviders.Add(providerData);
					return;
				}
				if (this.CheckPath("channelSinkProviders/serverProviders"))
				{
					ProviderData providerData = this.ReadProvider(name, attrs, true);
					RemotingConfiguration.RegisterServerProviderTemplate(providerData);
					return;
				}
				if (this.CheckPath("channelSinkProviders/clientProviders"))
				{
					ProviderData providerData = this.ReadProvider(name, attrs, true);
					RemotingConfiguration.RegisterClientProviderTemplate(providerData);
					return;
				}
				this.ValidatePath(name, Array.Empty<string>());
				return;
			}
			else if (num <= 3638887060U)
			{
				if (num != 3588091843U)
				{
					if (num == 3638887060U)
					{
						if (name == "serverProviders")
						{
							this.ValidatePath(name, new string[]
							{
								"channelSinkProviders",
								"channel"
							});
							return;
						}
					}
				}
				else if (name == "interopXmlType")
				{
					this.ValidatePath(name, new string[]
					{
						"soapInterop"
					});
					this.ReadInteropXml(attrs, false);
					return;
				}
			}
			else if (num != 4033672166U)
			{
				if (num != 4187488551U)
				{
					if (num == 4226312309U)
					{
						if (name == "channels")
						{
							this.ValidatePath(name, new string[]
							{
								"system.runtime.remoting",
								"application"
							});
							return;
						}
					}
				}
				else if (name == "channelSinkProviders")
				{
					this.ValidatePath(name, new string[]
					{
						"system.runtime.remoting"
					});
					return;
				}
			}
			else if (name == "preLoad")
			{
				this.ValidatePath(name, new string[]
				{
					"soapInterop"
				});
				this.ReadPreload(attrs);
				return;
			}
			IL_5DF:
			throw new RemotingException("Element '" + name + "' is not valid in system.remoting.configuration section");
		}

		// Token: 0x0600364E RID: 13902 RVA: 0x000C3D38 File Offset: 0x000C1F38
		public void OnEndElement(string name)
		{
			if (this.currentProviderData != null)
			{
				this.currentProviderData.Pop();
				if (this.currentProviderData.Count == 0)
				{
					this.currentProviderData = null;
				}
			}
			this.currentXmlPath = this.currentXmlPath.Substring(0, this.currentXmlPath.Length - name.Length - 1);
		}

		// Token: 0x0600364F RID: 13903 RVA: 0x000C3D94 File Offset: 0x000C1F94
		private void ReadCustomProviderData(string name, SmallXmlParser.IAttrList attrs)
		{
			SinkProviderData sinkProviderData = (SinkProviderData)this.currentProviderData.Peek();
			SinkProviderData sinkProviderData2 = new SinkProviderData(name);
			for (int i = 0; i < attrs.Names.Length; i++)
			{
				sinkProviderData2.Properties[attrs.Names[i]] = attrs.GetValue(i);
			}
			sinkProviderData.Children.Add(sinkProviderData2);
			this.currentProviderData.Push(sinkProviderData2);
		}

		// Token: 0x06003650 RID: 13904 RVA: 0x000C3E00 File Offset: 0x000C2000
		private void ReadLifetine(SmallXmlParser.IAttrList attrs)
		{
			for (int i = 0; i < attrs.Names.Length; i++)
			{
				string a = attrs.Names[i];
				if (!(a == "leaseTime"))
				{
					if (!(a == "sponsorshipTimeout"))
					{
						if (!(a == "renewOnCallTime"))
						{
							if (!(a == "leaseManagerPollTime"))
							{
								throw new RemotingException("Invalid attribute: " + attrs.Names[i]);
							}
							LifetimeServices.LeaseManagerPollTime = this.ParseTime(attrs.GetValue(i));
						}
						else
						{
							LifetimeServices.RenewOnCallTime = this.ParseTime(attrs.GetValue(i));
						}
					}
					else
					{
						LifetimeServices.SponsorshipTimeout = this.ParseTime(attrs.GetValue(i));
					}
				}
				else
				{
					LifetimeServices.LeaseTime = this.ParseTime(attrs.GetValue(i));
				}
			}
		}

		// Token: 0x06003651 RID: 13905 RVA: 0x000C3ED0 File Offset: 0x000C20D0
		private TimeSpan ParseTime(string s)
		{
			if (s == "" || s == null)
			{
				throw new RemotingException("Invalid time value");
			}
			int num = s.IndexOfAny(new char[]
			{
				'D',
				'H',
				'M',
				'S'
			});
			string text;
			if (num == -1)
			{
				text = "S";
			}
			else
			{
				text = s.Substring(num);
				s = s.Substring(0, num);
			}
			double value;
			try
			{
				value = double.Parse(s);
			}
			catch
			{
				throw new RemotingException("Invalid time value: " + s);
			}
			if (text == "D")
			{
				return TimeSpan.FromDays(value);
			}
			if (text == "H")
			{
				return TimeSpan.FromHours(value);
			}
			if (text == "M")
			{
				return TimeSpan.FromMinutes(value);
			}
			if (text == "S")
			{
				return TimeSpan.FromSeconds(value);
			}
			if (text == "MS")
			{
				return TimeSpan.FromMilliseconds(value);
			}
			throw new RemotingException("Invalid time unit: " + text);
		}

		// Token: 0x06003652 RID: 13906 RVA: 0x000C3FD0 File Offset: 0x000C21D0
		private void ReadChannel(SmallXmlParser.IAttrList attrs, bool isTemplate)
		{
			ChannelData channelData = new ChannelData();
			for (int i = 0; i < attrs.Names.Length; i++)
			{
				string text = attrs.Names[i];
				string text2 = attrs.Values[i];
				if (text == "ref" && !isTemplate)
				{
					channelData.Ref = text2;
				}
				else if (text == "delayLoadAsClientChannel")
				{
					channelData.DelayLoadAsClientChannel = text2;
				}
				else if (text == "id" && isTemplate)
				{
					channelData.Id = text2;
				}
				else if (text == "type")
				{
					channelData.Type = text2;
				}
				else
				{
					channelData.CustomProperties.Add(text, text2);
				}
			}
			if (isTemplate)
			{
				if (channelData.Id == null)
				{
					throw new RemotingException("id attribute is required");
				}
				if (channelData.Type == null)
				{
					throw new RemotingException("id attribute is required");
				}
				RemotingConfiguration.RegisterChannelTemplate(channelData);
			}
			else
			{
				this.channelInstances.Add(channelData);
			}
			this.currentChannel = channelData;
		}

		// Token: 0x06003653 RID: 13907 RVA: 0x000C40C0 File Offset: 0x000C22C0
		private ProviderData ReadProvider(string name, SmallXmlParser.IAttrList attrs, bool isTemplate)
		{
			ProviderData providerData = (name == "provider") ? new ProviderData() : new FormatterData();
			SinkProviderData sinkProviderData = new SinkProviderData("root");
			providerData.CustomData = sinkProviderData.Children;
			this.currentProviderData = new Stack();
			this.currentProviderData.Push(sinkProviderData);
			for (int i = 0; i < attrs.Names.Length; i++)
			{
				string text = attrs.Names[i];
				string text2 = attrs.Values[i];
				if (text == "id" && isTemplate)
				{
					providerData.Id = text2;
				}
				else if (text == "type")
				{
					providerData.Type = text2;
				}
				else if (text == "ref" && !isTemplate)
				{
					providerData.Ref = text2;
				}
				else
				{
					providerData.CustomProperties.Add(text, text2);
				}
			}
			if (providerData.Id == null && isTemplate)
			{
				throw new RemotingException("id attribute is required");
			}
			return providerData;
		}

		// Token: 0x06003654 RID: 13908 RVA: 0x000C41AC File Offset: 0x000C23AC
		private void ReadClientActivated(SmallXmlParser.IAttrList attrs)
		{
			string notNull = this.GetNotNull(attrs, "type");
			string assemblyName = this.ExtractAssembly(ref notNull);
			if (this.currentClientUrl == null || this.currentClientUrl == "")
			{
				throw new RemotingException("url attribute is required in client element when it contains activated entries");
			}
			this.typeEntries.Add(new ActivatedClientTypeEntry(notNull, assemblyName, this.currentClientUrl));
		}

		// Token: 0x06003655 RID: 13909 RVA: 0x000C4210 File Offset: 0x000C2410
		private void ReadServiceActivated(SmallXmlParser.IAttrList attrs)
		{
			string notNull = this.GetNotNull(attrs, "type");
			string assemblyName = this.ExtractAssembly(ref notNull);
			this.typeEntries.Add(new ActivatedServiceTypeEntry(notNull, assemblyName));
		}

		// Token: 0x06003656 RID: 13910 RVA: 0x000C4248 File Offset: 0x000C2448
		private void ReadClientWellKnown(SmallXmlParser.IAttrList attrs)
		{
			string notNull = this.GetNotNull(attrs, "url");
			string notNull2 = this.GetNotNull(attrs, "type");
			string assemblyName = this.ExtractAssembly(ref notNull2);
			this.typeEntries.Add(new WellKnownClientTypeEntry(notNull2, assemblyName, notNull));
		}

		// Token: 0x06003657 RID: 13911 RVA: 0x000C428C File Offset: 0x000C248C
		private void ReadServiceWellKnown(SmallXmlParser.IAttrList attrs)
		{
			string notNull = this.GetNotNull(attrs, "objectUri");
			string notNull2 = this.GetNotNull(attrs, "mode");
			string notNull3 = this.GetNotNull(attrs, "type");
			string assemblyName = this.ExtractAssembly(ref notNull3);
			WellKnownObjectMode mode;
			if (notNull2 == "SingleCall")
			{
				mode = WellKnownObjectMode.SingleCall;
			}
			else
			{
				if (!(notNull2 == "Singleton"))
				{
					throw new RemotingException("wellknown object mode '" + notNull2 + "' is invalid");
				}
				mode = WellKnownObjectMode.Singleton;
			}
			this.typeEntries.Add(new WellKnownServiceTypeEntry(notNull3, assemblyName, notNull, mode));
		}

		// Token: 0x06003658 RID: 13912 RVA: 0x000C431C File Offset: 0x000C251C
		private void ReadInteropXml(SmallXmlParser.IAttrList attrs, bool isElement)
		{
			Type type = Type.GetType(this.GetNotNull(attrs, "clr"));
			string[] array = this.GetNotNull(attrs, "xml").Split(',', StringSplitOptions.None);
			string text = array[0].Trim();
			string text2 = (array.Length != 0) ? array[1].Trim() : null;
			if (isElement)
			{
				SoapServices.RegisterInteropXmlElement(text, text2, type);
				return;
			}
			SoapServices.RegisterInteropXmlType(text, text2, type);
		}

		// Token: 0x06003659 RID: 13913 RVA: 0x000C4380 File Offset: 0x000C2580
		private void ReadPreload(SmallXmlParser.IAttrList attrs)
		{
			string value = attrs.GetValue("type");
			string value2 = attrs.GetValue("assembly");
			if (value != null && value2 != null)
			{
				throw new RemotingException("Type and assembly attributes cannot be specified together");
			}
			if (value != null)
			{
				SoapServices.PreLoad(Type.GetType(value));
				return;
			}
			if (value2 != null)
			{
				SoapServices.PreLoad(Assembly.Load(value2));
				return;
			}
			throw new RemotingException("Either type or assembly attributes must be specified");
		}

		// Token: 0x0600365A RID: 13914 RVA: 0x000C43E0 File Offset: 0x000C25E0
		private string GetNotNull(SmallXmlParser.IAttrList attrs, string name)
		{
			string value = attrs.GetValue(name);
			if (value == null || value == "")
			{
				throw new RemotingException(name + " attribute is required");
			}
			return value;
		}

		// Token: 0x0600365B RID: 13915 RVA: 0x000C4418 File Offset: 0x000C2618
		private string ExtractAssembly(ref string type)
		{
			int num = type.IndexOf(',');
			if (num == -1)
			{
				return "";
			}
			string result = type.Substring(num + 1).Trim();
			type = type.Substring(0, num).Trim();
			return result;
		}

		// Token: 0x0600365C RID: 13916 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public void OnChars(string ch)
		{
		}

		// Token: 0x0600365D RID: 13917 RVA: 0x000C4458 File Offset: 0x000C2658
		public void OnEndParsing(SmallXmlParser parser)
		{
			RemotingConfiguration.RegisterChannels(this.channelInstances, this.onlyDelayedChannels);
			if (this.appName != null)
			{
				RemotingConfiguration.ApplicationName = this.appName;
			}
			if (!this.onlyDelayedChannels)
			{
				RemotingConfiguration.RegisterTypes(this.typeEntries);
			}
		}

		// Token: 0x0400253A RID: 9530
		private ArrayList typeEntries = new ArrayList();

		// Token: 0x0400253B RID: 9531
		private ArrayList channelInstances = new ArrayList();

		// Token: 0x0400253C RID: 9532
		private ChannelData currentChannel;

		// Token: 0x0400253D RID: 9533
		private Stack currentProviderData;

		// Token: 0x0400253E RID: 9534
		private string currentClientUrl;

		// Token: 0x0400253F RID: 9535
		private string appName;

		// Token: 0x04002540 RID: 9536
		private string currentXmlPath = "";

		// Token: 0x04002541 RID: 9537
		private bool onlyDelayedChannels;
	}
}
