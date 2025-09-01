using System;
using System.Collections.Generic;
using IKVM.Reflection.Metadata;
using IKVM.Reflection.Reader;

namespace IKVM.Reflection
{
	// Token: 0x02000053 RID: 83
	internal sealed class WindowsRuntimeProjection
	{
		// Token: 0x060003E6 RID: 998 RVA: 0x0000B180 File Offset: 0x00009380
		static WindowsRuntimeProjection()
		{
			WindowsRuntimeProjection.projections.Add(new TypeName("System", "Attribute"), new WindowsRuntimeProjection.Mapping(WindowsRuntimeProjection.ProjectionAssembly.System_Runtime, "System", "Attribute"));
			WindowsRuntimeProjection.projections.Add(new TypeName("System", "MulticastDelegate"), new WindowsRuntimeProjection.Mapping(WindowsRuntimeProjection.ProjectionAssembly.System_Runtime, "System", "MulticastDelegate"));
			WindowsRuntimeProjection.projections.Add(new TypeName("Windows.Foundation", "DateTime"), new WindowsRuntimeProjection.Mapping(WindowsRuntimeProjection.ProjectionAssembly.System_Runtime, "System", "DateTimeOffset"));
			WindowsRuntimeProjection.projections.Add(new TypeName("Windows.Foundation", "EventHandler`1"), new WindowsRuntimeProjection.Mapping(WindowsRuntimeProjection.ProjectionAssembly.System_Runtime, "System", "EventHandler`1"));
			WindowsRuntimeProjection.projections.Add(new TypeName("Windows.Foundation", "EventRegistrationToken"), new WindowsRuntimeProjection.Mapping(WindowsRuntimeProjection.ProjectionAssembly.System_Runtime_InteropServices_WindowsRuntime, "System.Runtime.InteropServices.WindowsRuntime", "EventRegistrationToken"));
			WindowsRuntimeProjection.projections.Add(new TypeName("Windows.Foundation", "HResult"), new WindowsRuntimeProjection.Mapping(WindowsRuntimeProjection.ProjectionAssembly.System_Runtime, "System", "Exception"));
			WindowsRuntimeProjection.projections.Add(new TypeName("Windows.Foundation", "IClosable"), new WindowsRuntimeProjection.Mapping(WindowsRuntimeProjection.ProjectionAssembly.System_Runtime, "System", "IDisposable"));
			WindowsRuntimeProjection.projections.Add(new TypeName("Windows.Foundation", "IReference`1"), new WindowsRuntimeProjection.Mapping(WindowsRuntimeProjection.ProjectionAssembly.System_Runtime, "System", "Nullable`1"));
			WindowsRuntimeProjection.projections.Add(new TypeName("Windows.Foundation", "Point"), new WindowsRuntimeProjection.Mapping(WindowsRuntimeProjection.ProjectionAssembly.System_Runtime_WindowsRuntime, "Windows.Foundation", "Point"));
			WindowsRuntimeProjection.projections.Add(new TypeName("Windows.Foundation", "Rect"), new WindowsRuntimeProjection.Mapping(WindowsRuntimeProjection.ProjectionAssembly.System_Runtime_WindowsRuntime, "Windows.Foundation", "Rect"));
			WindowsRuntimeProjection.projections.Add(new TypeName("Windows.Foundation", "Size"), new WindowsRuntimeProjection.Mapping(WindowsRuntimeProjection.ProjectionAssembly.System_Runtime_WindowsRuntime, "Windows.Foundation", "Size"));
			WindowsRuntimeProjection.projections.Add(new TypeName("Windows.Foundation", "TimeSpan"), new WindowsRuntimeProjection.Mapping(WindowsRuntimeProjection.ProjectionAssembly.System_Runtime, "System", "TimeSpan"));
			WindowsRuntimeProjection.projections.Add(new TypeName("Windows.Foundation", "Uri"), new WindowsRuntimeProjection.Mapping(WindowsRuntimeProjection.ProjectionAssembly.System_Runtime, "System", "Uri"));
			WindowsRuntimeProjection.projections.Add(new TypeName("Windows.Foundation.Collections", "IIterable`1"), new WindowsRuntimeProjection.Mapping(WindowsRuntimeProjection.ProjectionAssembly.System_Runtime, "System.Collections.Generic", "IEnumerable`1"));
			WindowsRuntimeProjection.projections.Add(new TypeName("Windows.Foundation.Collections", "IKeyValuePair`2"), new WindowsRuntimeProjection.Mapping(WindowsRuntimeProjection.ProjectionAssembly.System_Runtime, "System.Collections.Generic", "KeyValuePair`2"));
			WindowsRuntimeProjection.projections.Add(new TypeName("Windows.Foundation.Collections", "IMap`2"), new WindowsRuntimeProjection.Mapping(WindowsRuntimeProjection.ProjectionAssembly.System_Runtime, "System.Collections.Generic", "IDictionary`2"));
			WindowsRuntimeProjection.projections.Add(new TypeName("Windows.Foundation.Collections", "IMapView`2"), new WindowsRuntimeProjection.Mapping(WindowsRuntimeProjection.ProjectionAssembly.System_Runtime, "System.Collections.Generic", "IReadOnlyDictionary`2"));
			WindowsRuntimeProjection.projections.Add(new TypeName("Windows.Foundation.Collections", "IVector`1"), new WindowsRuntimeProjection.Mapping(WindowsRuntimeProjection.ProjectionAssembly.System_Runtime, "System.Collections.Generic", "IList`1"));
			WindowsRuntimeProjection.projections.Add(new TypeName("Windows.Foundation.Collections", "IVectorView`1"), new WindowsRuntimeProjection.Mapping(WindowsRuntimeProjection.ProjectionAssembly.System_Runtime, "System.Collections.Generic", "IReadOnlyList`1"));
			WindowsRuntimeProjection.projections.Add(new TypeName("Windows.Foundation.Metadata", "AttributeTargets"), new WindowsRuntimeProjection.Mapping(WindowsRuntimeProjection.ProjectionAssembly.System_Runtime, "System", "AttributeTargets"));
			WindowsRuntimeProjection.projections.Add(new TypeName("Windows.Foundation.Metadata", "AttributeUsageAttribute"), new WindowsRuntimeProjection.Mapping(WindowsRuntimeProjection.ProjectionAssembly.System_Runtime, "System", "AttributeUsageAttribute"));
			WindowsRuntimeProjection.projections.Add(new TypeName("Windows.UI", "Color"), new WindowsRuntimeProjection.Mapping(WindowsRuntimeProjection.ProjectionAssembly.System_Runtime_WindowsRuntime, "Windows.UI", "Color"));
			WindowsRuntimeProjection.projections.Add(new TypeName("Windows.UI.Xaml", "CornerRadius"), new WindowsRuntimeProjection.Mapping(WindowsRuntimeProjection.ProjectionAssembly.System_Runtime_WindowsRuntime_UI_Xaml, "Windows.UI.Xaml", "CornerRadius"));
			WindowsRuntimeProjection.projections.Add(new TypeName("Windows.UI.Xaml", "Duration"), new WindowsRuntimeProjection.Mapping(WindowsRuntimeProjection.ProjectionAssembly.System_Runtime_WindowsRuntime_UI_Xaml, "Windows.UI.Xaml", "Duration"));
			WindowsRuntimeProjection.projections.Add(new TypeName("Windows.UI.Xaml", "DurationType"), new WindowsRuntimeProjection.Mapping(WindowsRuntimeProjection.ProjectionAssembly.System_Runtime_WindowsRuntime_UI_Xaml, "Windows.UI.Xaml", "DurationType"));
			WindowsRuntimeProjection.projections.Add(new TypeName("Windows.UI.Xaml", "GridLength"), new WindowsRuntimeProjection.Mapping(WindowsRuntimeProjection.ProjectionAssembly.System_Runtime_WindowsRuntime_UI_Xaml, "Windows.UI.Xaml", "GridLength"));
			WindowsRuntimeProjection.projections.Add(new TypeName("Windows.UI.Xaml", "GridUnitType"), new WindowsRuntimeProjection.Mapping(WindowsRuntimeProjection.ProjectionAssembly.System_Runtime_WindowsRuntime_UI_Xaml, "Windows.UI.Xaml", "GridUnitType"));
			WindowsRuntimeProjection.projections.Add(new TypeName("Windows.UI.Xaml", "Thickness"), new WindowsRuntimeProjection.Mapping(WindowsRuntimeProjection.ProjectionAssembly.System_Runtime_WindowsRuntime_UI_Xaml, "Windows.UI.Xaml", "Thickness"));
			WindowsRuntimeProjection.projections.Add(new TypeName("Windows.UI.Xaml.Controls.Primitives", "GeneratorPosition"), new WindowsRuntimeProjection.Mapping(WindowsRuntimeProjection.ProjectionAssembly.System_Runtime_WindowsRuntime_UI_Xaml, "Windows.UI.Xaml.Controls.Primitives", "GeneratorPosition"));
			WindowsRuntimeProjection.projections.Add(new TypeName("Windows.UI.Xaml.Data", "INotifyPropertyChanged"), new WindowsRuntimeProjection.Mapping(WindowsRuntimeProjection.ProjectionAssembly.System_ObjectModel, "System.ComponentModel", "INotifyPropertyChanged"));
			WindowsRuntimeProjection.projections.Add(new TypeName("Windows.UI.Xaml.Data", "PropertyChangedEventArgs"), new WindowsRuntimeProjection.Mapping(WindowsRuntimeProjection.ProjectionAssembly.System_ObjectModel, "System.ComponentModel", "PropertyChangedEventArgs"));
			WindowsRuntimeProjection.projections.Add(new TypeName("Windows.UI.Xaml.Data", "PropertyChangedEventHandler"), new WindowsRuntimeProjection.Mapping(WindowsRuntimeProjection.ProjectionAssembly.System_ObjectModel, "System.ComponentModel", "PropertyChangedEventHandler"));
			WindowsRuntimeProjection.projections.Add(new TypeName("Windows.UI.Xaml.Input", "ICommand"), new WindowsRuntimeProjection.Mapping(WindowsRuntimeProjection.ProjectionAssembly.System_ObjectModel, "System.Windows.Input", "ICommand"));
			WindowsRuntimeProjection.projections.Add(new TypeName("Windows.UI.Xaml.Interop", "IBindableIterable"), new WindowsRuntimeProjection.Mapping(WindowsRuntimeProjection.ProjectionAssembly.System_Runtime, "System.Collections", "IEnumerable"));
			WindowsRuntimeProjection.projections.Add(new TypeName("Windows.UI.Xaml.Interop", "IBindableVector"), new WindowsRuntimeProjection.Mapping(WindowsRuntimeProjection.ProjectionAssembly.System_Runtime, "System.Collections", "IList"));
			WindowsRuntimeProjection.projections.Add(new TypeName("Windows.UI.Xaml.Interop", "NotifyCollectionChangedAction"), new WindowsRuntimeProjection.Mapping(WindowsRuntimeProjection.ProjectionAssembly.System_ObjectModel, "System.Collections.Specialized", "NotifyCollectionChangedAction"));
			WindowsRuntimeProjection.projections.Add(new TypeName("Windows.UI.Xaml.Interop", "NotifyCollectionChangedEventArgs"), new WindowsRuntimeProjection.Mapping(WindowsRuntimeProjection.ProjectionAssembly.System_ObjectModel, "System.Collections.Specialized", "NotifyCollectionChangedEventArgs"));
			WindowsRuntimeProjection.projections.Add(new TypeName("Windows.UI.Xaml.Interop", "NotifyCollectionChangedEventHandler"), new WindowsRuntimeProjection.Mapping(WindowsRuntimeProjection.ProjectionAssembly.System_ObjectModel, "System.Collections.Specialized", "NotifyCollectionChangedEventHandler"));
			WindowsRuntimeProjection.projections.Add(new TypeName("Windows.UI.Xaml.Interop", "TypeName"), new WindowsRuntimeProjection.Mapping(WindowsRuntimeProjection.ProjectionAssembly.System_Runtime, "System", "Type"));
			WindowsRuntimeProjection.projections.Add(new TypeName("Windows.UI.Xaml.Media", "Matrix"), new WindowsRuntimeProjection.Mapping(WindowsRuntimeProjection.ProjectionAssembly.System_Runtime_WindowsRuntime_UI_Xaml, "Windows.UI.Xaml.Media", "Matrix"));
			WindowsRuntimeProjection.projections.Add(new TypeName("Windows.UI.Xaml.Media.Animation", "KeyTime"), new WindowsRuntimeProjection.Mapping(WindowsRuntimeProjection.ProjectionAssembly.System_Runtime_WindowsRuntime_UI_Xaml, "Windows.UI.Xaml.Media.Animation", "KeyTime"));
			WindowsRuntimeProjection.projections.Add(new TypeName("Windows.UI.Xaml.Media.Animation", "RepeatBehavior"), new WindowsRuntimeProjection.Mapping(WindowsRuntimeProjection.ProjectionAssembly.System_Runtime_WindowsRuntime_UI_Xaml, "Windows.UI.Xaml.Media.Animation", "RepeatBehavior"));
			WindowsRuntimeProjection.projections.Add(new TypeName("Windows.UI.Xaml.Media.Animation", "RepeatBehaviorType"), new WindowsRuntimeProjection.Mapping(WindowsRuntimeProjection.ProjectionAssembly.System_Runtime_WindowsRuntime_UI_Xaml, "Windows.UI.Xaml.Media.Animation", "RepeatBehaviorType"));
			WindowsRuntimeProjection.projections.Add(new TypeName("Windows.UI.Xaml.Media.Media3D", "Matrix3D"), new WindowsRuntimeProjection.Mapping(WindowsRuntimeProjection.ProjectionAssembly.System_Runtime_WindowsRuntime_UI_Xaml, "Windows.UI.Xaml.Media.Media3D", "Matrix3D"));
			WindowsRuntimeProjection.projections.Add(new TypeName("Windows.Foundation", "IPropertyValue"), null);
			WindowsRuntimeProjection.projections.Add(new TypeName("Windows.Foundation", "IReferenceArray`1"), null);
			WindowsRuntimeProjection.projections.Add(new TypeName("Windows.Foundation.Metadata", "GCPressureAmount"), null);
			WindowsRuntimeProjection.projections.Add(new TypeName("Windows.Foundation.Metadata", "GCPressureAttribute"), null);
			WindowsRuntimeProjection.projections.Add(new TypeName("Windows.UI.Xaml", "CornerRadiusHelper"), null);
			WindowsRuntimeProjection.projections.Add(new TypeName("Windows.UI.Xaml", "DurationHelper"), null);
			WindowsRuntimeProjection.projections.Add(new TypeName("Windows.UI.Xaml", "GridLengthHelper"), null);
			WindowsRuntimeProjection.projections.Add(new TypeName("Windows.UI.Xaml", "ThicknessHelper"), null);
			WindowsRuntimeProjection.projections.Add(new TypeName("Windows.UI.Xaml.Controls.Primitives", "GeneratorPositionHelper"), null);
			WindowsRuntimeProjection.projections.Add(new TypeName("Windows.UI.Xaml.Interop", "INotifyCollectionChanged"), null);
			WindowsRuntimeProjection.projections.Add(new TypeName("Windows.UI.Xaml.Media", "MatrixHelper"), null);
			WindowsRuntimeProjection.projections.Add(new TypeName("Windows.UI.Xaml.Media.Animation", "KeyTimeHelper"), null);
			WindowsRuntimeProjection.projections.Add(new TypeName("Windows.UI.Xaml.Media.Animation", "RepeatBehaviorHelper"), null);
			WindowsRuntimeProjection.projections.Add(new TypeName("Windows.UI.Xaml.Media.Media3D", "Matrix3DHelper"), null);
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x0000BA10 File Offset: 0x00009C10
		private WindowsRuntimeProjection(ModuleReader module, Dictionary<int, string> strings)
		{
			this.module = module;
			this.strings = strings;
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x0000BA74 File Offset: 0x00009C74
		internal static void Patch(ModuleReader module, Dictionary<int, string> strings, ref string imageRuntimeVersion, ref byte[] blobHeap)
		{
			if (!module.CustomAttribute.Sorted)
			{
				throw new NotImplementedException("CustomAttribute table must be sorted");
			}
			bool flag = imageRuntimeVersion.Contains(";");
			if (flag)
			{
				string text = imageRuntimeVersion;
				imageRuntimeVersion = text.Substring(text.IndexOf(';') + 1);
				if (imageRuntimeVersion.StartsWith("CLR", StringComparison.OrdinalIgnoreCase))
				{
					imageRuntimeVersion = imageRuntimeVersion.Substring(3);
				}
				imageRuntimeVersion = imageRuntimeVersion.TrimStart(new char[]
				{
					' '
				});
			}
			else
			{
				Assembly mscorlib = module.universe.Mscorlib;
				imageRuntimeVersion = (mscorlib.__IsMissing ? "v4.0.30319" : mscorlib.ImageRuntimeVersion);
			}
			WindowsRuntimeProjection windowsRuntimeProjection = new WindowsRuntimeProjection(module, strings);
			windowsRuntimeProjection.PatchAssemblyRef(ref blobHeap);
			windowsRuntimeProjection.PatchTypeRef();
			windowsRuntimeProjection.PatchTypes(flag);
			windowsRuntimeProjection.PatchMethodImpl();
			windowsRuntimeProjection.PatchCustomAttribute(ref blobHeap);
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x0000BB38 File Offset: 0x00009D38
		private void PatchAssemblyRef(ref byte[] blobHeap)
		{
			AssemblyRefTable assemblyRef = this.module.AssemblyRef;
			for (int i = 0; i < assemblyRef.records.Length; i++)
			{
				if (this.module.GetString(assemblyRef.records[i].Name) == "mscorlib")
				{
					Version mscorlibVersion = this.GetMscorlibVersion();
					assemblyRef.records[i].MajorVersion = (ushort)mscorlibVersion.Major;
					assemblyRef.records[i].MinorVersion = (ushort)mscorlibVersion.Minor;
					assemblyRef.records[i].BuildNumber = (ushort)mscorlibVersion.Build;
					assemblyRef.records[i].RevisionNumber = (ushort)mscorlibVersion.Revision;
					break;
				}
			}
			int publicKeyToken = WindowsRuntimeProjection.AddBlob(ref blobHeap, new byte[]
			{
				176,
				63,
				95,
				127,
				17,
				213,
				10,
				58
			});
			int publicKeyToken2 = WindowsRuntimeProjection.AddBlob(ref blobHeap, new byte[]
			{
				183,
				122,
				92,
				86,
				25,
				52,
				224,
				137
			});
			this.assemblyRefTokens[0] = this.AddAssemblyReference("System.Runtime", publicKeyToken);
			this.assemblyRefTokens[1] = this.AddAssemblyReference("System.Runtime.InteropServices.WindowsRuntime", publicKeyToken);
			this.assemblyRefTokens[2] = this.AddAssemblyReference("System.ObjectModel", publicKeyToken);
			this.assemblyRefTokens[3] = this.AddAssemblyReference("System.Runtime.WindowsRuntime", publicKeyToken2);
			this.assemblyRefTokens[4] = this.AddAssemblyReference("System.Runtime.WindowsRuntime.UI.Xaml", publicKeyToken2);
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x0000BC94 File Offset: 0x00009E94
		private void PatchTypeRef()
		{
			TypeRefTable.Record[] records = this.module.TypeRef.records;
			this.projectedTypeRefs = new bool[records.Length];
			for (int i = 0; i < records.Length; i++)
			{
				TypeName typeRefName = this.GetTypeRefName(i);
				WindowsRuntimeProjection.Mapping mapping;
				WindowsRuntimeProjection.projections.TryGetValue(typeRefName, out mapping);
				if (mapping != null)
				{
					records[i].ResolutionScope = this.assemblyRefTokens[(int)mapping.Assembly];
					records[i].TypeNamespace = this.GetString(mapping.TypeNamespace);
					records[i].TypeName = this.GetString(mapping.TypeName);
					this.projectedTypeRefs[i] = true;
				}
				string a = typeRefName.Namespace;
				if (!(a == "System"))
				{
					if (!(a == "Windows.Foundation"))
					{
						if (a == "Windows.Foundation.Metadata")
						{
							a = typeRefName.Name;
							if (!(a == "AllowMultipleAttribute"))
							{
								if (a == "AttributeUsageAttribute")
								{
									this.typeofSystemAttributeUsageAttribute = (1 << 24) + i + 1;
								}
							}
							else
							{
								this.typeofWindowsFoundationMetadataAllowMultipleAttribute = (1 << 24) + i + 1;
							}
						}
					}
					else
					{
						a = typeRefName.Name;
						if (a == "IClosable")
						{
							this.typeofSystemIDisposable = (1 << 24) + i + 1;
						}
					}
				}
				else
				{
					a = typeRefName.Name;
					if (!(a == "Attribute"))
					{
						if (!(a == "Enum"))
						{
							if (a == "MulticastDelegate")
							{
								this.typeofSystemMulticastDelegate = (1 << 24) + i + 1;
							}
						}
						else
						{
							this.typeofSystemEnum = (1 << 24) + i + 1;
						}
					}
					else
					{
						this.typeofSystemAttribute = (1 << 24) + i + 1;
					}
				}
			}
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x0000BE54 File Offset: 0x0000A054
		private void PatchTypes(bool clr)
		{
			TypeDefTable.Record[] records = this.module.TypeDef.records;
			MethodDefTable.Record[] records2 = this.module.MethodDef.records;
			FieldTable.Record[] records3 = this.module.Field.records;
			for (int i = 0; i < records.Length; i++)
			{
				TypeAttributes flags = (TypeAttributes)records[i].Flags;
				if ((flags & TypeAttributes.WindowsRuntime) != TypeAttributes.AnsiClass)
				{
					if (clr && (flags & (TypeAttributes.Public | TypeAttributes.NestedPublic | TypeAttributes.NestedFamily | TypeAttributes.ClassSemanticsMask | TypeAttributes.WindowsRuntime)) == (TypeAttributes.Public | TypeAttributes.WindowsRuntime))
					{
						records[i].TypeName = this.GetString("<WinRT>" + this.module.GetString(records[i].TypeName));
						TypeDefTable.Record[] array = records;
						int num = i;
						array[num].Flags = (array[num].Flags & -2);
					}
					if (records[i].Extends != this.typeofSystemAttribute && (!clr || (flags & TypeAttributes.ClassSemanticsMask) == TypeAttributes.AnsiClass))
					{
						TypeDefTable.Record[] array2 = records;
						int num2 = i;
						array2[num2].Flags = (array2[num2].Flags | 4096);
					}
					if (WindowsRuntimeProjection.projections.ContainsKey(this.GetTypeDefName(i)))
					{
						TypeDefTable.Record[] array3 = records;
						int num3 = i;
						array3[num3].Flags = (array3[num3].Flags & -2);
					}
					int num4 = (i == records.Length - 1) ? records2.Length : (records[i + 1].MethodList - 1);
					for (int j = records[i].MethodList - 1; j < num4; j++)
					{
						if (records[i].Extends == this.typeofSystemMulticastDelegate)
						{
							if (this.module.GetString(records2[j].Name) == ".ctor")
							{
								MethodDefTable.Record[] array4 = records2;
								int num5 = j;
								array4[num5].Flags = (array4[num5].Flags & -8);
								MethodDefTable.Record[] array5 = records2;
								int num6 = j;
								array5[num6].Flags = (array5[num6].Flags | 6);
							}
						}
						else if (records2[j].RVA == 0)
						{
							records2[j].ImplFlags = 4099;
						}
					}
					if (records[i].Extends == this.typeofSystemEnum)
					{
						int num7 = (i == records.Length - 1) ? records3.Length : (records[i + 1].FieldList - 1);
						for (int k = records[i].FieldList - 1; k < num7; k++)
						{
							FieldTable.Record[] array6 = records3;
							int num8 = k;
							array6[num8].Flags = (array6[num8].Flags & -8);
							FieldTable.Record[] array7 = records3;
							int num9 = k;
							array7[num9].Flags = (array7[num9].Flags | 6);
						}
					}
				}
				else if (clr && (flags & (TypeAttributes.Public | TypeAttributes.NestedPublic | TypeAttributes.NestedFamily | TypeAttributes.SpecialName)) == TypeAttributes.SpecialName)
				{
					string @string = this.module.GetString(records[i].TypeName);
					if (@string.StartsWith("<CLR>", StringComparison.Ordinal))
					{
						records[i].TypeName = this.GetString(@string.Substring(5));
						TypeDefTable.Record[] array8 = records;
						int num10 = i;
						array8[num10].Flags = (array8[num10].Flags | 1);
						TypeDefTable.Record[] array9 = records;
						int num11 = i;
						array9[num11].Flags = (array9[num11].Flags & -1025);
					}
				}
			}
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x0000C130 File Offset: 0x0000A330
		private void PatchMethodImpl()
		{
			MethodImplTable.Record[] records = this.module.MethodImpl.records;
			MemberRefTable.Record[] records2 = this.module.MemberRef.records;
			MethodDefTable.Record[] records3 = this.module.MethodDef.records;
			int[] records4 = this.module.TypeSpec.records;
			for (int i = 0; i < records.Length; i++)
			{
				int methodDeclaration = records[i].MethodDeclaration;
				if (methodDeclaration >> 24 == 10)
				{
					int num = records2[(methodDeclaration & 16777215) - 1].Class;
					if (num >> 24 == 27)
					{
						num = WindowsRuntimeProjection.ReadTypeSpec(this.module.GetBlob(records4[(num & 16777215) - 1]));
					}
					if (num >> 24 == 1)
					{
						if (num == this.typeofSystemIDisposable)
						{
							int @string = this.GetString("Dispose");
							records3[(records[i].MethodBody & 16777215) - 1].Name = @string;
							records2[(records[i].MethodDeclaration & 16777215) - 1].Name = @string;
						}
						else if (this.projectedTypeRefs[(num & 16777215) - 1])
						{
							MethodDefTable.Record[] array = records3;
							int num2 = (records[i].MethodBody & 16777215) - 1;
							array[num2].Flags = (array[num2].Flags & -8);
							MethodDefTable.Record[] array2 = records3;
							int num3 = (records[i].MethodBody & 16777215) - 1;
							array2[num3].Flags = (array2[num3].Flags | 1);
							records[i].MethodBody = 0;
							records[i].MethodDeclaration = 0;
						}
					}
					else if (num >> 24 != 2)
					{
						if (num >> 24 == 27)
						{
							throw new NotImplementedException();
						}
						throw new BadImageFormatException();
					}
				}
			}
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x0000C2FC File Offset: 0x0000A4FC
		private void PatchCustomAttribute(ref byte[] blobHeap)
		{
			MemberRefTable.Record[] records = this.module.MemberRef.records;
			int num = -1;
			int ctorWindowsFoundationMetadataAllowMultipleAttribute = -1;
			for (int i = 0; i < records.Length; i++)
			{
				if (records[i].Class == this.typeofSystemAttributeUsageAttribute && this.module.GetString(records[i].Name) == ".ctor")
				{
					num = (10 << 24) + i + 1;
				}
				else if (records[i].Class == this.typeofWindowsFoundationMetadataAllowMultipleAttribute && this.module.GetString(records[i].Name) == ".ctor")
				{
					ctorWindowsFoundationMetadataAllowMultipleAttribute = (10 << 24) + i + 1;
				}
			}
			if (num != -1)
			{
				CustomAttributeTable.Record[] records2 = this.module.CustomAttribute.records;
				Dictionary<int, int> map = new Dictionary<int, int>();
				for (int j = 0; j < records2.Length; j++)
				{
					if (records2[j].Type == num)
					{
						ByteReader blob = this.module.GetBlob(records2[j].Value);
						blob.ReadInt16();
						AttributeTargets attributeTargets = WindowsRuntimeProjection.MapAttributeTargets(blob.ReadInt32());
						if ((attributeTargets & AttributeTargets.Method) != (AttributeTargets)0)
						{
							attributeTargets |= AttributeTargets.Constructor;
							if (records2[j].Parent >> 24 == 2)
							{
								TypeName typeDefName = this.GetTypeDefName((records2[j].Parent & 16777215) - 1);
								if (typeDefName.Namespace == "Windows.Foundation.Metadata" && (typeDefName.Name == "OverloadAttribute" || typeDefName.Name == "DefaultOverloadAttribute"))
								{
									attributeTargets &= ~AttributeTargets.Constructor;
								}
							}
						}
						records2[j].Value = WindowsRuntimeProjection.GetAttributeUsageAttributeBlob(ref blobHeap, map, attributeTargets, WindowsRuntimeProjection.HasAllowMultipleAttribute(records2, j, ctorWindowsFoundationMetadataAllowMultipleAttribute));
					}
				}
			}
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x0000C4D4 File Offset: 0x0000A6D4
		private int AddAssemblyReference(string name, int publicKeyToken)
		{
			Version mscorlibVersion = this.GetMscorlibVersion();
			AssemblyRefTable.Record rec;
			rec.MajorVersion = (ushort)mscorlibVersion.Major;
			rec.MinorVersion = (ushort)mscorlibVersion.Minor;
			rec.BuildNumber = (ushort)mscorlibVersion.Build;
			rec.RevisionNumber = (ushort)mscorlibVersion.Revision;
			rec.Flags = 0;
			rec.PublicKeyOrToken = publicKeyToken;
			rec.Name = this.GetString(name);
			rec.Culture = 0;
			rec.HashValue = 0;
			int result = 587202560 | this.module.AssemblyRef.FindOrAddRecord(rec);
			Array.Resize<AssemblyRefTable.Record>(ref this.module.AssemblyRef.records, this.module.AssemblyRef.RowCount);
			return result;
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x0000C58C File Offset: 0x0000A78C
		private TypeName GetTypeRefName(int index)
		{
			return new TypeName(this.module.GetString(this.module.TypeRef.records[index].TypeNamespace), this.module.GetString(this.module.TypeRef.records[index].TypeName));
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x0000C5EC File Offset: 0x0000A7EC
		private TypeName GetTypeDefName(int index)
		{
			return new TypeName(this.module.GetString(this.module.TypeDef.records[index].TypeNamespace), this.module.GetString(this.module.TypeDef.records[index].TypeName));
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x0000C64C File Offset: 0x0000A84C
		private int GetString(string str)
		{
			int num;
			if (!this.added.TryGetValue(str, out num))
			{
				num = -(this.added.Count + 1);
				this.added.Add(str, num);
				this.strings.Add(num, str);
			}
			return num;
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x0000C694 File Offset: 0x0000A894
		private Version GetMscorlibVersion()
		{
			Assembly mscorlib = this.module.universe.Mscorlib;
			if (!mscorlib.__IsMissing)
			{
				return mscorlib.GetName().Version;
			}
			return new Version(4, 0, 0, 0);
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x0000C6D0 File Offset: 0x0000A8D0
		private static bool HasAllowMultipleAttribute(CustomAttributeTable.Record[] customAttributes, int i, int ctorWindowsFoundationMetadataAllowMultipleAttribute)
		{
			int parent = customAttributes[i].Parent;
			while (i > 0)
			{
				if (customAttributes[i - 1].Parent != parent)
				{
					break;
				}
				i--;
			}
			while (i < customAttributes.Length && customAttributes[i].Parent == parent)
			{
				if (customAttributes[i].Type == ctorWindowsFoundationMetadataAllowMultipleAttribute)
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0000C734 File Offset: 0x0000A934
		private static AttributeTargets MapAttributeTargets(int targets)
		{
			if (targets == -1)
			{
				return AttributeTargets.All;
			}
			AttributeTargets attributeTargets = (AttributeTargets)0;
			if ((targets & 1) != 0)
			{
				attributeTargets |= AttributeTargets.Delegate;
			}
			if ((targets & 2) != 0)
			{
				attributeTargets |= AttributeTargets.Enum;
			}
			if ((targets & 4) != 0)
			{
				attributeTargets |= AttributeTargets.Event;
			}
			if ((targets & 8) != 0)
			{
				attributeTargets |= AttributeTargets.Field;
			}
			if ((targets & 16) != 0)
			{
				attributeTargets |= AttributeTargets.Interface;
			}
			if ((targets & 64) != 0)
			{
				attributeTargets |= AttributeTargets.Method;
			}
			if ((targets & 128) != 0)
			{
				attributeTargets |= AttributeTargets.Parameter;
			}
			if ((targets & 256) != 0)
			{
				attributeTargets |= AttributeTargets.Property;
			}
			if ((targets & 512) != 0)
			{
				attributeTargets |= AttributeTargets.Class;
			}
			if ((targets & 1024) != 0)
			{
				attributeTargets |= AttributeTargets.Struct;
			}
			return attributeTargets;
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0000C7D4 File Offset: 0x0000A9D4
		private static int GetAttributeUsageAttributeBlob(ref byte[] blobHeap, Dictionary<int, int> map, AttributeTargets targets, bool allowMultiple)
		{
			int num = (int)targets;
			if (allowMultiple)
			{
				num |= int.MinValue;
			}
			int num2;
			if (!map.TryGetValue(num, out num2))
			{
				byte[] array = new byte[]
				{
					1,
					0,
					0,
					0,
					0,
					0,
					1,
					0,
					84,
					2,
					13,
					65,
					108,
					108,
					111,
					119,
					77,
					117,
					108,
					116,
					105,
					112,
					108,
					101,
					0
				};
				array[2] = (byte)targets;
				array[3] = (byte)(targets >> 8);
				array[4] = (byte)(targets >> 16);
				array[5] = (byte)(targets >> 24);
				array[24] = (allowMultiple ? 1 : 0);
				num2 = WindowsRuntimeProjection.AddBlob(ref blobHeap, array);
				map.Add(num, num2);
			}
			return num2;
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0000C844 File Offset: 0x0000AA44
		private static int ReadTypeSpec(ByteReader br)
		{
			if (br.ReadByte() != 21)
			{
				throw new NotImplementedException("Expected ELEMENT_TYPE_GENERICINST");
			}
			byte b = br.ReadByte();
			if (b != 17 && b != 18)
			{
				throw new NotImplementedException("Expected ELEMENT_TYPE_CLASS or ELEMENT_TYPE_VALUETYPE");
			}
			int num = br.ReadCompressedUInt();
			switch (num & 3)
			{
			case 0:
				return (2 << 24) + (num >> 2);
			case 1:
				return (1 << 24) + (num >> 2);
			case 2:
				return (27 << 24) + (num >> 2);
			default:
				throw new BadImageFormatException();
			}
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0000C8C4 File Offset: 0x0000AAC4
		private static int AddBlob(ref byte[] blobHeap, byte[] blob)
		{
			if (blob.Length > 127)
			{
				throw new NotImplementedException();
			}
			int num = blobHeap.Length;
			Array.Resize<byte>(ref blobHeap, num + blob.Length + 1);
			blobHeap[num] = (byte)blob.Length;
			Buffer.BlockCopy(blob, 0, blobHeap, num + 1, blob.Length);
			return num;
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x0000C908 File Offset: 0x0000AB08
		internal static bool IsProjectedValueType(string ns, string name, Module module)
		{
			return ((ns == "System.Collections.Generic" && name == "KeyValuePair`2") || (ns == "System" && name == "Nullable`1")) && module.Assembly.GetName().Name == "System.Runtime";
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x0000C968 File Offset: 0x0000AB68
		internal static bool IsProjectedReferenceType(string ns, string name, Module module)
		{
			return ((ns == "System" && name == "Exception") || (ns == "System" && name == "Type")) && module.Assembly.GetName().Name == "System.Runtime";
		}

		// Token: 0x040001B7 RID: 439
		private static readonly Dictionary<TypeName, WindowsRuntimeProjection.Mapping> projections = new Dictionary<TypeName, WindowsRuntimeProjection.Mapping>();

		// Token: 0x040001B8 RID: 440
		private readonly ModuleReader module;

		// Token: 0x040001B9 RID: 441
		private readonly Dictionary<int, string> strings;

		// Token: 0x040001BA RID: 442
		private readonly Dictionary<string, int> added = new Dictionary<string, int>();

		// Token: 0x040001BB RID: 443
		private readonly int[] assemblyRefTokens = new int[5];

		// Token: 0x040001BC RID: 444
		private int typeofSystemAttribute = -1;

		// Token: 0x040001BD RID: 445
		private int typeofSystemAttributeUsageAttribute = -1;

		// Token: 0x040001BE RID: 446
		private int typeofSystemEnum = -1;

		// Token: 0x040001BF RID: 447
		private int typeofSystemIDisposable = -1;

		// Token: 0x040001C0 RID: 448
		private int typeofSystemMulticastDelegate = -1;

		// Token: 0x040001C1 RID: 449
		private int typeofWindowsFoundationMetadataAllowMultipleAttribute = -1;

		// Token: 0x040001C2 RID: 450
		private bool[] projectedTypeRefs;

		// Token: 0x02000329 RID: 809
		private enum ProjectionAssembly
		{
			// Token: 0x04000E49 RID: 3657
			System_Runtime,
			// Token: 0x04000E4A RID: 3658
			System_Runtime_InteropServices_WindowsRuntime,
			// Token: 0x04000E4B RID: 3659
			System_ObjectModel,
			// Token: 0x04000E4C RID: 3660
			System_Runtime_WindowsRuntime,
			// Token: 0x04000E4D RID: 3661
			System_Runtime_WindowsRuntime_UI_Xaml,
			// Token: 0x04000E4E RID: 3662
			Count
		}

		// Token: 0x0200032A RID: 810
		private sealed class Mapping
		{
			// Token: 0x0600259E RID: 9630 RVA: 0x000B3F16 File Offset: 0x000B2116
			internal Mapping(WindowsRuntimeProjection.ProjectionAssembly assembly, string typeNamespace, string typeName)
			{
				this.Assembly = assembly;
				this.TypeNamespace = typeNamespace;
				this.TypeName = typeName;
			}

			// Token: 0x04000E4F RID: 3663
			internal readonly WindowsRuntimeProjection.ProjectionAssembly Assembly;

			// Token: 0x04000E50 RID: 3664
			internal readonly string TypeNamespace;

			// Token: 0x04000E51 RID: 3665
			internal readonly string TypeName;
		}
	}
}
