using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace IKVM.Reflection
{
	// Token: 0x0200002E RID: 46
	internal static class Fusion
	{
		// Token: 0x0600016F RID: 367 RVA: 0x00005CC8 File Offset: 0x00003EC8
		internal static bool CompareAssemblyIdentityNative(string assemblyIdentity1, bool unified1, string assemblyIdentity2, bool unified2, out AssemblyComparisonResult result)
		{
			bool result2;
			Marshal.ThrowExceptionForHR(Fusion.CompareAssemblyIdentity(assemblyIdentity1, unified1, assemblyIdentity2, unified2, out result2, out result));
			return result2;
		}

		// Token: 0x06000170 RID: 368
		[DllImport("fusion", CharSet = CharSet.Unicode)]
		private static extern int CompareAssemblyIdentity(string pwzAssemblyIdentity1, bool fUnified1, string pwzAssemblyIdentity2, bool fUnified2, out bool pfEquivalent, out AssemblyComparisonResult pResult);

		// Token: 0x06000171 RID: 369 RVA: 0x00005CE8 File Offset: 0x00003EE8
		internal static bool CompareAssemblyIdentityPure(string assemblyIdentity1, bool unified1, string assemblyIdentity2, bool unified2, out AssemblyComparisonResult result)
		{
			ParsedAssemblyName parsedAssemblyName;
			ParseAssemblyResult parseAssemblyResult = Fusion.ParseAssemblyName(assemblyIdentity1, out parsedAssemblyName);
			ParsedAssemblyName parsedAssemblyName2;
			ParseAssemblyResult parseAssemblyResult2 = Fusion.ParseAssemblyName(assemblyIdentity2, out parsedAssemblyName2);
			Version frameworkVersion;
			if (unified1 && (parsedAssemblyName.Name == null || !Fusion.ParseVersion(parsedAssemblyName.Version, out frameworkVersion) || frameworkVersion == null || frameworkVersion.Revision == -1 || parsedAssemblyName.Culture == null || parsedAssemblyName.PublicKeyToken == null || parsedAssemblyName.PublicKeyToken.Length < 2))
			{
				result = AssemblyComparisonResult.NonEquivalent;
				throw new ArgumentException();
			}
			Version version = null;
			if (!Fusion.ParseVersion(parsedAssemblyName2.Version, out version) || version == null || version.Revision == -1 || parsedAssemblyName2.Culture == null || parsedAssemblyName2.PublicKeyToken == null || parsedAssemblyName2.PublicKeyToken.Length < 2)
			{
				result = AssemblyComparisonResult.NonEquivalent;
				throw new ArgumentException();
			}
			if (parsedAssemblyName2.Name != null && parsedAssemblyName2.Name.Equals("mscorlib", StringComparison.OrdinalIgnoreCase))
			{
				if (parsedAssemblyName.Name != null && parsedAssemblyName.Name.Equals(parsedAssemblyName2.Name, StringComparison.OrdinalIgnoreCase))
				{
					result = AssemblyComparisonResult.EquivalentFullMatch;
					return true;
				}
				result = AssemblyComparisonResult.NonEquivalent;
				return false;
			}
			else if (parseAssemblyResult != ParseAssemblyResult.OK)
			{
				result = AssemblyComparisonResult.NonEquivalent;
				if (parseAssemblyResult != ParseAssemblyResult.GenericError && parseAssemblyResult == ParseAssemblyResult.DuplicateKey)
				{
					throw new FileLoadException();
				}
				throw new ArgumentException();
			}
			else if (parseAssemblyResult2 != ParseAssemblyResult.OK)
			{
				result = AssemblyComparisonResult.NonEquivalent;
				if (parseAssemblyResult2 != ParseAssemblyResult.GenericError && parseAssemblyResult2 == ParseAssemblyResult.DuplicateKey)
				{
					throw new FileLoadException();
				}
				throw new ArgumentException();
			}
			else
			{
				if (!Fusion.ParseVersion(parsedAssemblyName.Version, out frameworkVersion))
				{
					result = AssemblyComparisonResult.NonEquivalent;
					throw new ArgumentException();
				}
				bool flag = Fusion.IsPartial(parsedAssemblyName, frameworkVersion);
				if (flag && parsedAssemblyName.Retargetable != null)
				{
					result = AssemblyComparisonResult.NonEquivalent;
					throw new FileLoadException();
				}
				if ((flag && unified1) || Fusion.IsPartial(parsedAssemblyName2, version))
				{
					result = AssemblyComparisonResult.NonEquivalent;
					throw new ArgumentException();
				}
				if (!parsedAssemblyName.Name.Equals(parsedAssemblyName2.Name, StringComparison.OrdinalIgnoreCase))
				{
					result = AssemblyComparisonResult.NonEquivalent;
					return false;
				}
				if ((!flag || parsedAssemblyName.Culture != null) && !parsedAssemblyName.Culture.Equals(parsedAssemblyName2.Culture, StringComparison.OrdinalIgnoreCase))
				{
					result = AssemblyComparisonResult.NonEquivalent;
					return false;
				}
				if (!parsedAssemblyName.Retargetable.GetValueOrDefault() && parsedAssemblyName2.Retargetable.GetValueOrDefault())
				{
					result = AssemblyComparisonResult.NonEquivalent;
					return false;
				}
				if (parsedAssemblyName.PublicKeyToken == parsedAssemblyName2.PublicKeyToken && frameworkVersion != null && parsedAssemblyName.Retargetable.GetValueOrDefault() && !parsedAssemblyName2.Retargetable.GetValueOrDefault() && Fusion.GetRemappedPublicKeyToken(ref parsedAssemblyName, frameworkVersion) != null)
				{
					parsedAssemblyName.Retargetable = new bool?(false);
				}
				string text = null;
				if (frameworkVersion != null && (text = Fusion.GetRemappedPublicKeyToken(ref parsedAssemblyName, frameworkVersion)) != null)
				{
					parsedAssemblyName.PublicKeyToken = text;
					frameworkVersion = Fusion.FrameworkVersion;
				}
				string remappedPublicKeyToken;
				if ((remappedPublicKeyToken = Fusion.GetRemappedPublicKeyToken(ref parsedAssemblyName2, version)) != null)
				{
					parsedAssemblyName2.PublicKeyToken = remappedPublicKeyToken;
					version = Fusion.FrameworkVersion;
				}
				if (parsedAssemblyName.Retargetable.GetValueOrDefault())
				{
					if (parsedAssemblyName2.Retargetable.GetValueOrDefault())
					{
						if (text != null ^ remappedPublicKeyToken != null)
						{
							result = AssemblyComparisonResult.NonEquivalent;
							return false;
						}
					}
					else if (text == null || remappedPublicKeyToken != null)
					{
						result = AssemblyComparisonResult.Unknown;
						return false;
					}
				}
				bool flag2 = false;
				bool flag3 = frameworkVersion == version;
				if (Fusion.IsFrameworkAssembly(parsedAssemblyName))
				{
					flag2 |= !flag3;
					frameworkVersion = Fusion.FrameworkVersion;
				}
				if (Fusion.IsFrameworkAssembly(parsedAssemblyName2) && version < Fusion.FrameworkVersionNext)
				{
					flag2 |= !flag3;
					version = Fusion.FrameworkVersion;
				}
				if (Fusion.IsStrongNamed(parsedAssemblyName2))
				{
					if (parsedAssemblyName.PublicKeyToken != null && parsedAssemblyName.PublicKeyToken != parsedAssemblyName2.PublicKeyToken)
					{
						result = AssemblyComparisonResult.NonEquivalent;
						return false;
					}
					if (frameworkVersion == null)
					{
						result = AssemblyComparisonResult.EquivalentPartialMatch;
						return true;
					}
					if (frameworkVersion.Revision == -1 || version.Revision == -1)
					{
						result = AssemblyComparisonResult.NonEquivalent;
						throw new ArgumentException();
					}
					if (frameworkVersion < version)
					{
						if (unified2)
						{
							result = (flag ? AssemblyComparisonResult.EquivalentPartialUnified : AssemblyComparisonResult.EquivalentUnified);
							return true;
						}
						result = (flag ? AssemblyComparisonResult.NonEquivalentPartialVersion : AssemblyComparisonResult.NonEquivalentVersion);
						return false;
					}
					else if (frameworkVersion > version)
					{
						if (unified1)
						{
							result = (flag ? AssemblyComparisonResult.EquivalentPartialUnified : AssemblyComparisonResult.EquivalentUnified);
							return true;
						}
						result = (flag ? AssemblyComparisonResult.NonEquivalentPartialVersion : AssemblyComparisonResult.NonEquivalentVersion);
						return false;
					}
					else
					{
						if (!flag3 || flag2)
						{
							result = (flag ? AssemblyComparisonResult.EquivalentPartialFXUnified : AssemblyComparisonResult.EquivalentFXUnified);
							return true;
						}
						result = (flag ? AssemblyComparisonResult.EquivalentPartialMatch : AssemblyComparisonResult.EquivalentFullMatch);
						return true;
					}
				}
				else
				{
					if (Fusion.IsStrongNamed(parsedAssemblyName))
					{
						result = AssemblyComparisonResult.NonEquivalent;
						return false;
					}
					result = (flag ? AssemblyComparisonResult.EquivalentPartialWeakNamed : AssemblyComparisonResult.EquivalentWeakNamed);
					return true;
				}
			}
		}

		// Token: 0x06000172 RID: 370 RVA: 0x000060F4 File Offset: 0x000042F4
		private static bool IsFrameworkAssembly(ParsedAssemblyName name)
		{
			string name2 = name.Name;
			uint num = <PrivateImplementationDetails>.ComputeStringHash(name2);
			if (num <= 2331618185U)
			{
				if (num <= 1080666498U)
				{
					if (num <= 491911314U)
					{
						if (num <= 313345719U)
						{
							if (num <= 188908086U)
							{
								if (num != 35798259U)
								{
									if (num != 131786028U)
									{
										if (num != 188908086U)
										{
											return false;
										}
										if (!(name2 == "System.ComponentModel.DataAnnotations"))
										{
											return false;
										}
										goto IL_E13;
									}
									else
									{
										if (!(name2 == "System.Web.Routing"))
										{
											return false;
										}
										goto IL_E13;
									}
								}
								else if (!(name2 == "System.Transactions"))
								{
									return false;
								}
							}
							else if (num != 198186186U)
							{
								if (num != 304901598U)
								{
									if (num != 313345719U)
									{
										return false;
									}
									if (!(name2 == "System.Runtime.WindowsRuntime.UI.Xaml"))
									{
										return false;
									}
								}
								else
								{
									if (!(name2 == "System.ComponentModel.Annotations"))
									{
										return false;
									}
									goto IL_E02;
								}
							}
							else
							{
								if (!(name2 == "System.Runtime.Extensions"))
								{
									return false;
								}
								goto IL_E02;
							}
						}
						else if (num <= 434644658U)
						{
							if (num != 381465706U)
							{
								if (num != 383765384U)
								{
									if (num != 434644658U)
									{
										return false;
									}
									if (!(name2 == "System.Threading.Tasks"))
									{
										return false;
									}
									goto IL_E02;
								}
								else
								{
									if (!(name2 == "System.Runtime.Numerics"))
									{
										return false;
									}
									goto IL_E02;
								}
							}
							else if (!(name2 == "System.Reflection.Context"))
							{
								return false;
							}
						}
						else if (num != 452471429U)
						{
							if (num != 456588834U)
							{
								if (num != 491911314U)
								{
									return false;
								}
								if (!(name2 == "System.ServiceModel.Web"))
								{
									return false;
								}
								goto IL_E13;
							}
							else
							{
								if (!(name2 == "System.ServiceModel.Security"))
								{
									return false;
								}
								goto IL_E02;
							}
						}
						else if (!(name2 == "System.IdentityModel.Selectors"))
						{
							return false;
						}
					}
					else if (num <= 885067901U)
					{
						if (num <= 665970248U)
						{
							if (num != 550404468U)
							{
								if (num != 649747655U)
								{
									if (num != 665970248U)
									{
										return false;
									}
									if (!(name2 == "System.Drawing"))
									{
										return false;
									}
									goto IL_E02;
								}
								else
								{
									if (!(name2 == "System.Collections"))
									{
										return false;
									}
									goto IL_E02;
								}
							}
							else
							{
								if (!(name2 == "System.Xml.XDocument"))
								{
									return false;
								}
								goto IL_E02;
							}
						}
						else if (num != 669678873U)
						{
							if (num != 880581792U)
							{
								if (num != 885067901U)
								{
									return false;
								}
								if (!(name2 == "System.EnterpriseServices"))
								{
									return false;
								}
								goto IL_E02;
							}
							else
							{
								if (!(name2 == "System.Xml.XmlSerializer"))
								{
									return false;
								}
								goto IL_E02;
							}
						}
						else
						{
							if (!(name2 == "System.Reflection.Primitives"))
							{
								return false;
							}
							goto IL_E02;
						}
					}
					else if (num <= 1013775530U)
					{
						if (num != 893943215U)
						{
							if (num != 937460914U)
							{
								if (num != 1013775530U)
								{
									return false;
								}
								if (!(name2 == "System.ServiceModel.Primitives"))
								{
									return false;
								}
								goto IL_E02;
							}
							else if (!(name2 == "System.Windows.Forms"))
							{
								return false;
							}
						}
						else
						{
							if (!(name2 == "System.Net.Primitives"))
							{
								return false;
							}
							goto IL_E02;
						}
					}
					else if (num != 1038436392U)
					{
						if (num != 1064576906U)
						{
							if (num != 1080666498U)
							{
								return false;
							}
							if (!(name2 == "System.IO.Compression"))
							{
								return false;
							}
						}
						else
						{
							if (!(name2 == "System.ObjectModel"))
							{
								return false;
							}
							goto IL_E02;
						}
					}
					else
					{
						if (!(name2 == "System.Runtime.Serialization.Primitives"))
						{
							return false;
						}
						goto IL_E02;
					}
				}
				else if (num <= 1666131246U)
				{
					if (num <= 1419525797U)
					{
						if (num <= 1341696477U)
						{
							if (num != 1221334708U)
							{
								if (num != 1314853709U)
								{
									if (num != 1341696477U)
									{
										return false;
									}
									if (!(name2 == "System.DirectoryServices"))
									{
										return false;
									}
									goto IL_E02;
								}
								else
								{
									if (!(name2 == "System.Runtime.InteropServices"))
									{
										return false;
									}
									goto IL_E02;
								}
							}
							else
							{
								if (!(name2 == "System.Security.Principal"))
								{
									return false;
								}
								goto IL_E02;
							}
						}
						else if (num != 1370449338U)
						{
							if (num != 1404670083U)
							{
								if (num != 1419525797U)
								{
									return false;
								}
								if (!(name2 == "System.Net.NetworkInformation"))
								{
									return false;
								}
								goto IL_E02;
							}
							else
							{
								if (!(name2 == "System.Threading.Tasks.Parallel"))
								{
									return false;
								}
								goto IL_E02;
							}
						}
						else
						{
							if (!(name2 == "System.Text.RegularExpressions"))
							{
								return false;
							}
							goto IL_E02;
						}
					}
					else if (num <= 1576915898U)
					{
						if (num != 1430561240U)
						{
							if (num != 1560674155U)
							{
								if (num != 1576915898U)
								{
									return false;
								}
								if (!(name2 == "System.Data.DataSetExtensions"))
								{
									return false;
								}
							}
							else if (!(name2 == "System.Xml"))
							{
								return false;
							}
						}
						else
						{
							if (!(name2 == "System.Design"))
							{
								return false;
							}
							goto IL_E02;
						}
					}
					else if (num != 1613268841U)
					{
						if (num != 1648468625U)
						{
							if (num != 1666131246U)
							{
								return false;
							}
							if (!(name2 == "System.Resources.ResourceManager"))
							{
								return false;
							}
							goto IL_E02;
						}
						else
						{
							if (!(name2 == "System.ComponentModel.EventBasedAsync"))
							{
								return false;
							}
							goto IL_E02;
						}
					}
					else
					{
						if (!(name2 == "System.Dynamic.Runtime"))
						{
							return false;
						}
						goto IL_E02;
					}
				}
				else if (num <= 2128656086U)
				{
					if (num <= 1890419039U)
					{
						if (num != 1692796700U)
						{
							if (num != 1786963967U)
							{
								if (num != 1890419039U)
								{
									return false;
								}
								if (!(name2 == "System.Reflection"))
								{
									return false;
								}
								goto IL_E02;
							}
							else
							{
								if (!(name2 == "Microsoft.VisualBasic"))
								{
									return false;
								}
								goto IL_E02;
							}
						}
						else
						{
							if (!(name2 == "System.Configuration"))
							{
								return false;
							}
							goto IL_E02;
						}
					}
					else if (num != 1893461208U)
					{
						if (num != 1930792561U)
						{
							if (num != 2128656086U)
							{
								return false;
							}
							if (!(name2 == "System.Runtime.InteropServices.WindowsRuntime"))
							{
								return false;
							}
							goto IL_E02;
						}
						else if (!(name2 == "System.Data.OracleClient"))
						{
							return false;
						}
					}
					else if (!(name2 == "System.Data.Services"))
					{
						return false;
					}
				}
				else if (num <= 2195561099U)
				{
					if (num != 2128808423U)
					{
						if (num != 2154036817U)
						{
							if (num != 2195561099U)
							{
								return false;
							}
							if (!(name2 == "System.Runtime.Serialization.Xml"))
							{
								return false;
							}
							goto IL_E02;
						}
						else if (!(name2 == "System.Data.Services.Client"))
						{
							return false;
						}
					}
					else
					{
						if (!(name2 == "System.Globalization"))
						{
							return false;
						}
						goto IL_E02;
					}
				}
				else if (num <= 2213813148U)
				{
					if (num != 2205724446U)
					{
						if (num != 2213813148U)
						{
							return false;
						}
						if (!(name2 == "System.Web.Mobile"))
						{
							return false;
						}
						goto IL_E02;
					}
					else if (!(name2 == "System.Numerics"))
					{
						return false;
					}
				}
				else if (num != 2325243790U)
				{
					if (num != 2331618185U)
					{
						return false;
					}
					if (!(name2 == "System.Core"))
					{
						return false;
					}
				}
				else
				{
					if (!(name2 == "System.ServiceModel.Duplex"))
					{
						return false;
					}
					goto IL_E02;
				}
			}
			else if (num <= 3372287210U)
			{
				if (num <= 2898348737U)
				{
					if (num <= 2571566515U)
					{
						if (num <= 2416369721U)
						{
							if (num != 2387594636U)
							{
								if (num != 2402387132U)
								{
									if (num != 2416369721U)
									{
										return false;
									}
									if (!(name2 == "System.Xml.Serialization"))
									{
										return false;
									}
								}
								else if (!(name2 == "System"))
								{
									return false;
								}
							}
							else
							{
								if (!(name2 == "System.Text.Encoding"))
								{
									return false;
								}
								goto IL_E02;
							}
						}
						else if (num != 2456272638U)
						{
							if (num != 2497128998U)
							{
								if (num != 2571566515U)
								{
									return false;
								}
								if (!(name2 == "System.Runtime.WindowsRuntime"))
								{
									return false;
								}
							}
							else
							{
								if (!(name2 == "System.IO"))
								{
									return false;
								}
								goto IL_E02;
							}
						}
						else
						{
							if (!(name2 == "System.Runtime"))
							{
								return false;
							}
							goto IL_E02;
						}
					}
					else if (num <= 2823335057U)
					{
						if (num != 2765595158U)
						{
							if (num != 2773720602U)
							{
								if (num != 2823335057U)
								{
									return false;
								}
								if (!(name2 == "System.Linq.Expressions"))
								{
									return false;
								}
								goto IL_E02;
							}
							else
							{
								if (!(name2 == "System.Linq.Queryable"))
								{
									return false;
								}
								goto IL_E02;
							}
						}
						else if (!(name2 == "System.Data.Linq"))
						{
							return false;
						}
					}
					else if (num != 2833192089U)
					{
						if (num != 2862778125U)
						{
							if (num != 2898348737U)
							{
								return false;
							}
							if (!(name2 == "System.IdentityModel"))
							{
								return false;
							}
						}
						else
						{
							if (!(name2 == "System.Management"))
							{
								return false;
							}
							goto IL_E02;
						}
					}
					else
					{
						if (!(name2 == "System.Net"))
						{
							return false;
						}
						goto IL_E02;
					}
				}
				else if (num <= 3106654476U)
				{
					if (num <= 3016133270U)
					{
						if (num != 2903740131U)
						{
							if (num != 2968983462U)
							{
								if (num != 3016133270U)
								{
									return false;
								}
								if (!(name2 == "System.Data"))
								{
									return false;
								}
							}
							else
							{
								if (!(name2 == "System.Web.Services"))
								{
									return false;
								}
								goto IL_E02;
							}
						}
						else
						{
							if (!(name2 == "System.Diagnostics.Contracts"))
							{
								return false;
							}
							goto IL_E02;
						}
					}
					else if (num != 3074843816U)
					{
						if (num != 3087167260U)
						{
							if (num != 3106654476U)
							{
								return false;
							}
							if (!(name2 == "Microsoft.CSharp"))
							{
								return false;
							}
							goto IL_E02;
						}
						else if (!(name2 == "System.Runtime.Serialization"))
						{
							return false;
						}
					}
					else
					{
						if (!(name2 == "System.Security"))
						{
							return false;
						}
						goto IL_E02;
					}
				}
				else if (num <= 3232434094U)
				{
					if (num != 3174110234U)
					{
						if (num != 3230063426U)
						{
							if (num != 3232434094U)
							{
								return false;
							}
							if (!(name2 == "System.Threading"))
							{
								return false;
							}
							goto IL_E02;
						}
						else
						{
							if (!(name2 == "System.ServiceModel.NetTcp"))
							{
								return false;
							}
							goto IL_E02;
						}
					}
					else
					{
						if (!(name2 == "System.ComponentModel"))
						{
							return false;
						}
						goto IL_E02;
					}
				}
				else if (num != 3298553582U)
				{
					if (num != 3321480831U)
					{
						if (num != 3372287210U)
						{
							return false;
						}
						if (!(name2 == "System.Diagnostics.Tracing"))
						{
							return false;
						}
						goto IL_E02;
					}
					else
					{
						if (!(name2 == "System.Web.Abstractions"))
						{
							return false;
						}
						goto IL_E13;
					}
				}
				else
				{
					if (!(name2 == "System.Messaging"))
					{
						return false;
					}
					goto IL_E02;
				}
			}
			else if (num <= 3688780149U)
			{
				if (num <= 3500150413U)
				{
					if (num <= 3477801219U)
					{
						if (num != 3378336992U)
						{
							if (num != 3462746850U)
							{
								if (num != 3477801219U)
								{
									return false;
								}
								if (!(name2 == "System.Net.Requests"))
								{
									return false;
								}
								goto IL_E02;
							}
							else if (!(name2 == "System.ServiceModel"))
							{
								return false;
							}
						}
						else
						{
							if (!(name2 == "System.Web"))
							{
								return false;
							}
							goto IL_E02;
						}
					}
					else if (num != 3493251057U)
					{
						if (num != 3498418688U)
						{
							if (num != 3500150413U)
							{
								return false;
							}
							if (!(name2 == "System.Configuration.Install"))
							{
								return false;
							}
							goto IL_E02;
						}
						else
						{
							if (!(name2 == "System.Linq"))
							{
								return false;
							}
							goto IL_E02;
						}
					}
					else if (!(name2 == "System.Runtime.Remoting"))
					{
						return false;
					}
				}
				else if (num <= 3589203163U)
				{
					if (num != 3510614132U)
					{
						if (num != 3578476172U)
						{
							if (num != 3589203163U)
							{
								return false;
							}
							if (!(name2 == "System.Windows"))
							{
								return false;
							}
							goto IL_E02;
						}
						else
						{
							if (!(name2 == "System.Net.Http.Rtc"))
							{
								return false;
							}
							goto IL_E02;
						}
					}
					else
					{
						if (!(name2 == "System.Web.Extensions.Design"))
						{
							return false;
						}
						goto IL_E13;
					}
				}
				else if (num != 3619461527U)
				{
					if (num != 3650176852U)
					{
						if (num != 3688780149U)
						{
							return false;
						}
						if (!(name2 == "System.Reflection.Extensions"))
						{
							return false;
						}
						goto IL_E02;
					}
					else
					{
						if (!(name2 == "System.ServiceModel.Http"))
						{
							return false;
						}
						goto IL_E02;
					}
				}
				else
				{
					if (!(name2 == "System.Diagnostics.Debug"))
					{
						return false;
					}
					goto IL_E02;
				}
			}
			else if (num <= 3820308056U)
			{
				if (num <= 3768569444U)
				{
					if (num != 3723126785U)
					{
						if (num != 3725550837U)
						{
							if (num != 3768569444U)
							{
								return false;
							}
							if (!(name2 == "System.Drawing.Design"))
							{
								return false;
							}
							goto IL_E02;
						}
						else
						{
							if (!(name2 == "System.Net.Http"))
							{
								return false;
							}
							goto IL_E02;
						}
					}
					else if (!(name2 == "System.Xml.Linq"))
					{
						return false;
					}
				}
				else if (num != 3789206530U)
				{
					if (num != 3792034782U)
					{
						if (num != 3820308056U)
						{
							return false;
						}
						if (!(name2 == "System.Web.Extensions"))
						{
							return false;
						}
						goto IL_E13;
					}
					else
					{
						if (!(name2 == "System.Collections.Concurrent"))
						{
							return false;
						}
						goto IL_E02;
					}
				}
				else
				{
					if (!(name2 == "System.ServiceProcess"))
					{
						return false;
					}
					goto IL_E02;
				}
			}
			else if (num <= 3946096543U)
			{
				if (num != 3827034070U)
				{
					if (num != 3911679899U)
					{
						if (num != 3946096543U)
						{
							return false;
						}
						if (!(name2 == "System.Web.DynamicData"))
						{
							return false;
						}
						goto IL_E13;
					}
					else
					{
						if (!(name2 == "System.Diagnostics.Tools"))
						{
							return false;
						}
						goto IL_E02;
					}
				}
				else
				{
					if (!(name2 == "System.Runtime.Serialization.Formatters.Soap"))
					{
						return false;
					}
					goto IL_E02;
				}
			}
			else if (num <= 4081432144U)
			{
				if (num != 4028796997U)
				{
					if (num != 4081432144U)
					{
						return false;
					}
					if (!(name2 == "System.Runtime.Serialization.Json"))
					{
						return false;
					}
					goto IL_E02;
				}
				else
				{
					if (!(name2 == "System.Linq.Parallel"))
					{
						return false;
					}
					goto IL_E02;
				}
			}
			else if (num != 4124077140U)
			{
				if (num != 4235772519U)
				{
					return false;
				}
				if (!(name2 == "System.Xml.ReaderWriter"))
				{
					return false;
				}
				goto IL_E02;
			}
			else
			{
				if (!(name2 == "System.Text.Encoding.Extensions"))
				{
					return false;
				}
				goto IL_E02;
			}
			return name.PublicKeyToken == "b77a5c561934e089";
			IL_E02:
			return name.PublicKeyToken == "b03f5f7f11d50a3a";
			IL_E13:
			return name.PublicKeyToken == "31bf3856ad364e35";
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00006F28 File Offset: 0x00005128
		private static string GetRemappedPublicKeyToken(ref ParsedAssemblyName name, Version version)
		{
			if (name.Retargetable.GetValueOrDefault() && version < Fusion.SilverlightVersion)
			{
				return null;
			}
			if (name.PublicKeyToken == "ddd0da4d3e678217" && name.Name == "System.ComponentModel.DataAnnotations" && name.Retargetable.GetValueOrDefault())
			{
				return "31bf3856ad364e35";
			}
			if (Fusion.SilverlightVersionMinimum <= version && version <= Fusion.SilverlightVersionMaximum)
			{
				string a = name.PublicKeyToken;
				if (!(a == "7cec85d7bea7798e"))
				{
					if (a == "31bf3856ad364e35")
					{
						a = name.Name;
						if (a == "System.ComponentModel.Composition")
						{
							return "b77a5c561934e089";
						}
						if (name.Retargetable.GetValueOrDefault())
						{
							a = name.Name;
							if (a == "Microsoft.CSharp")
							{
								return "b03f5f7f11d50a3a";
							}
							if (a == "System.Numerics" || a == "System.ServiceModel" || a == "System.Xml.Serialization" || a == "System.Xml.Linq")
							{
								return "b77a5c561934e089";
							}
						}
					}
				}
				else
				{
					a = name.Name;
					if (a == "System" || a == "System.Core")
					{
						return "b77a5c561934e089";
					}
					if (name.Retargetable.GetValueOrDefault())
					{
						a = name.Name;
						if (a == "System.Runtime.Serialization" || a == "System.Xml")
						{
							return "b77a5c561934e089";
						}
						if (a == "System.Net" || a == "System.Windows")
						{
							return "b03f5f7f11d50a3a";
						}
						if (a == "System.ServiceModel.Web")
						{
							return "31bf3856ad364e35";
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06000174 RID: 372 RVA: 0x000070E5 File Offset: 0x000052E5
		internal static ParseAssemblyResult ParseAssemblySimpleName(string fullName, out int pos, out string simpleName)
		{
			pos = 0;
			if (!Fusion.TryParse(fullName, ref pos, out simpleName) || simpleName.Length == 0)
			{
				return ParseAssemblyResult.GenericError;
			}
			if (pos == fullName.Length && fullName[fullName.Length - 1] == ',')
			{
				return ParseAssemblyResult.GenericError;
			}
			return ParseAssemblyResult.OK;
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00007120 File Offset: 0x00005320
		private static bool TryParse(string fullName, ref int pos, out string value)
		{
			value = null;
			StringBuilder stringBuilder = new StringBuilder();
			while (pos < fullName.Length && char.IsWhiteSpace(fullName[pos]))
			{
				pos++;
			}
			int num = -1;
			if (pos < fullName.Length && (fullName[pos] == '"' || fullName[pos] == '\''))
			{
				int num2 = pos;
				pos = num2 + 1;
				num = (int)fullName[num2];
			}
			while (pos < fullName.Length)
			{
				char c = fullName[pos];
				if (c == '\\')
				{
					int num2 = pos + 1;
					pos = num2;
					if (num2 == fullName.Length)
					{
						return false;
					}
					c = fullName[pos];
					if (c == '\\')
					{
						return false;
					}
				}
				else
				{
					if ((int)c == num)
					{
						for (pos++; pos != fullName.Length; pos++)
						{
							c = fullName[pos];
							if (c == ',' || c == '=')
							{
								break;
							}
							if (!char.IsWhiteSpace(c))
							{
								return false;
							}
						}
						break;
					}
					if (num == -1 && (c == '"' || c == '\''))
					{
						return false;
					}
					if (num == -1 && (c == ',' || c == '='))
					{
						break;
					}
				}
				stringBuilder.Append(c);
				pos++;
			}
			value = stringBuilder.ToString().Trim();
			return value.Length != 0 || num != -1;
		}

		// Token: 0x06000176 RID: 374 RVA: 0x0000725A File Offset: 0x0000545A
		private static bool TryConsume(string fullName, char ch, ref int pos)
		{
			if (pos < fullName.Length && fullName[pos] == ch)
			{
				pos++;
				return true;
			}
			return false;
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0000727A File Offset: 0x0000547A
		private static bool TryParseAssemblyAttribute(string fullName, ref int pos, ref string key, ref string value)
		{
			return Fusion.TryConsume(fullName, ',', ref pos) && Fusion.TryParse(fullName, ref pos, out key) && Fusion.TryConsume(fullName, '=', ref pos) && Fusion.TryParse(fullName, ref pos, out value);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x000072A8 File Offset: 0x000054A8
		internal static ParseAssemblyResult ParseAssemblyName(string fullName, out ParsedAssemblyName parsedName)
		{
			parsedName = default(ParsedAssemblyName);
			int num;
			ParseAssemblyResult parseAssemblyResult = Fusion.ParseAssemblySimpleName(fullName, out num, out parsedName.Name);
			if (parseAssemblyResult != ParseAssemblyResult.OK)
			{
				return parseAssemblyResult;
			}
			Dictionary<string, string> dictionary = null;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			while (num != fullName.Length)
			{
				string text = null;
				string text2 = null;
				if (!Fusion.TryParseAssemblyAttribute(fullName, ref num, ref text, ref text2))
				{
					return ParseAssemblyResult.GenericError;
				}
				text = text.ToLowerInvariant();
				uint num2 = <PrivateImplementationDetails>.ComputeStringHash(text);
				if (num2 <= 1395321340U)
				{
					if (num2 != 1181855383U)
					{
						if (num2 != 1352603410U)
						{
							if (num2 == 1395321340U)
							{
								if (text == "processorarchitecture")
								{
									if (flag)
									{
										return ParseAssemblyResult.DuplicateKey;
									}
									flag = true;
									string a = text2.ToLowerInvariant();
									if (a == "none")
									{
										parsedName.ProcessorArchitecture = ProcessorArchitecture.None;
										continue;
									}
									if (a == "msil")
									{
										parsedName.ProcessorArchitecture = ProcessorArchitecture.MSIL;
										continue;
									}
									if (a == "x86")
									{
										parsedName.ProcessorArchitecture = ProcessorArchitecture.X86;
										continue;
									}
									if (a == "ia64")
									{
										parsedName.ProcessorArchitecture = ProcessorArchitecture.IA64;
										continue;
									}
									if (a == "amd64")
									{
										parsedName.ProcessorArchitecture = ProcessorArchitecture.Amd64;
										continue;
									}
									if (!(a == "arm"))
									{
										return ParseAssemblyResult.GenericError;
									}
									parsedName.ProcessorArchitecture = ProcessorArchitecture.Arm;
									continue;
								}
							}
						}
						else if (text == "contenttype")
						{
							if (flag2)
							{
								return ParseAssemblyResult.DuplicateKey;
							}
							flag2 = true;
							if (!text2.Equals("windowsruntime", StringComparison.OrdinalIgnoreCase))
							{
								return ParseAssemblyResult.GenericError;
							}
							parsedName.WindowsRuntime = true;
							continue;
						}
					}
					else if (text == "version")
					{
						if (parsedName.Version != null)
						{
							return ParseAssemblyResult.DuplicateKey;
						}
						parsedName.Version = text2;
						continue;
					}
				}
				else if (num2 <= 2399368031U)
				{
					if (num2 != 1499762217U)
					{
						if (num2 == 2399368031U)
						{
							if (text == "publickey")
							{
								if (parsedName.HasPublicKey)
								{
									return ParseAssemblyResult.DuplicateKey;
								}
								string text3;
								if (!Fusion.ParsePublicKey(text2, out text3))
								{
									return ParseAssemblyResult.GenericError;
								}
								if (flag3 && parsedName.PublicKeyToken != text3)
								{
									Marshal.ThrowExceptionForHR(-2147010794);
								}
								parsedName.PublicKeyToken = text3;
								parsedName.HasPublicKey = true;
								continue;
							}
						}
					}
					else if (text == "retargetable")
					{
						if (parsedName.Retargetable != null)
						{
							return ParseAssemblyResult.DuplicateKey;
						}
						string a = text2.ToLowerInvariant();
						if (a == "yes")
						{
							parsedName.Retargetable = new bool?(true);
							continue;
						}
						if (!(a == "no"))
						{
							return ParseAssemblyResult.GenericError;
						}
						parsedName.Retargetable = new bool?(false);
						continue;
					}
				}
				else if (num2 != 2927506036U)
				{
					if (num2 == 3303907537U)
					{
						if (text == "culture")
						{
							if (parsedName.Culture != null)
							{
								return ParseAssemblyResult.DuplicateKey;
							}
							if (!Fusion.ParseCulture(text2, out parsedName.Culture))
							{
								return ParseAssemblyResult.GenericError;
							}
							continue;
						}
					}
				}
				else if (text == "publickeytoken")
				{
					if (flag3)
					{
						return ParseAssemblyResult.DuplicateKey;
					}
					string text3;
					if (!Fusion.ParsePublicKeyToken(text2, out text3))
					{
						return ParseAssemblyResult.GenericError;
					}
					if (parsedName.HasPublicKey && parsedName.PublicKeyToken != text3)
					{
						Marshal.ThrowExceptionForHR(-2147010794);
					}
					parsedName.PublicKeyToken = text3;
					flag3 = true;
					continue;
				}
				if (text.Length == 0)
				{
					return ParseAssemblyResult.GenericError;
				}
				if (dictionary == null)
				{
					dictionary = new Dictionary<string, string>();
				}
				if (dictionary.ContainsKey(text))
				{
					return ParseAssemblyResult.DuplicateKey;
				}
				dictionary.Add(text, null);
			}
			return ParseAssemblyResult.OK;
		}

		// Token: 0x06000179 RID: 377 RVA: 0x0000761C File Offset: 0x0000581C
		private static bool ParseVersion(string str, out Version version)
		{
			if (str == null)
			{
				version = null;
				return true;
			}
			string[] array = str.Split(new char[]
			{
				'.'
			});
			if (array.Length < 2 || array.Length > 4)
			{
				version = null;
				ushort num;
				return array.Length == 1 && ushort.TryParse(array[0], NumberStyles.Integer, null, out num);
			}
			if (array[0] == "" || array[1] == "")
			{
				version = null;
				return true;
			}
			ushort maxValue = ushort.MaxValue;
			ushort maxValue2 = ushort.MaxValue;
			ushort major;
			ushort minor;
			if (ushort.TryParse(array[0], NumberStyles.Integer, null, out major) && ushort.TryParse(array[1], NumberStyles.Integer, null, out minor) && (array.Length <= 2 || array[2] == "" || ushort.TryParse(array[2], NumberStyles.Integer, null, out maxValue)) && (array.Length <= 3 || array[3] == "" || (array[2] != "" && ushort.TryParse(array[3], NumberStyles.Integer, null, out maxValue2))))
			{
				if (array.Length == 4 && array[3] != "" && array[2] != "")
				{
					version = new Version((int)major, (int)minor, (int)maxValue, (int)maxValue2);
				}
				else if (array.Length == 3 && array[2] != "")
				{
					version = new Version((int)major, (int)minor, (int)maxValue);
				}
				else
				{
					version = new Version((int)major, (int)minor);
				}
				return true;
			}
			version = null;
			return false;
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00007773 File Offset: 0x00005973
		private static bool ParseCulture(string str, out string culture)
		{
			if (str == null)
			{
				culture = null;
				return false;
			}
			culture = str;
			return true;
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00007781 File Offset: 0x00005981
		private static bool ParsePublicKeyToken(string str, out string publicKeyToken)
		{
			if (str == null)
			{
				publicKeyToken = null;
				return false;
			}
			publicKeyToken = str.ToLowerInvariant();
			return true;
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00007794 File Offset: 0x00005994
		private static bool ParsePublicKey(string str, out string publicKeyToken)
		{
			if (str == null)
			{
				publicKeyToken = null;
				return false;
			}
			publicKeyToken = AssemblyName.ComputePublicKeyToken(str);
			return true;
		}

		// Token: 0x0600017D RID: 381 RVA: 0x000077A7 File Offset: 0x000059A7
		private static bool IsPartial(ParsedAssemblyName name, Version version)
		{
			return version == null || name.Culture == null || name.PublicKeyToken == null;
		}

		// Token: 0x0600017E RID: 382 RVA: 0x000077C5 File Offset: 0x000059C5
		private static bool IsStrongNamed(ParsedAssemblyName name)
		{
			return name.PublicKeyToken != null && name.PublicKeyToken != "null";
		}

		// Token: 0x0600017F RID: 383 RVA: 0x000077E4 File Offset: 0x000059E4
		// Note: this type is marked as 'beforefieldinit'.
		static Fusion()
		{
		}

		// Token: 0x04000131 RID: 305
		private static readonly Version FrameworkVersion = new Version(4, 0, 0, 0);

		// Token: 0x04000132 RID: 306
		private static readonly Version FrameworkVersionNext = new Version(4, 1, 0, 0);

		// Token: 0x04000133 RID: 307
		private static readonly Version SilverlightVersion = new Version(2, 0, 5, 0);

		// Token: 0x04000134 RID: 308
		private static readonly Version SilverlightVersionMinimum = new Version(2, 0, 0, 0);

		// Token: 0x04000135 RID: 309
		private static readonly Version SilverlightVersionMaximum = new Version(5, 9, 0, 0);

		// Token: 0x04000136 RID: 310
		private const string PublicKeyTokenEcma = "b77a5c561934e089";

		// Token: 0x04000137 RID: 311
		private const string PublicKeyTokenMicrosoft = "b03f5f7f11d50a3a";

		// Token: 0x04000138 RID: 312
		private const string PublicKeyTokenSilverlight = "7cec85d7bea7798e";

		// Token: 0x04000139 RID: 313
		private const string PublicKeyTokenWinFX = "31bf3856ad364e35";
	}
}
