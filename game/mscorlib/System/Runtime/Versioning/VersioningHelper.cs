using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using Microsoft.Win32;

namespace System.Runtime.Versioning
{
	/// <summary>Provides methods to aid developers in writing version-safe code. This class cannot be inherited.</summary>
	// Token: 0x02000643 RID: 1603
	public static class VersioningHelper
	{
		// Token: 0x06003C31 RID: 15409
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetRuntimeId();

		/// <summary>Returns a version-safe name based on the specified resource name and the intended resource consumption source.</summary>
		/// <param name="name">The name of the resource.</param>
		/// <param name="from">The scope of the resource.</param>
		/// <param name="to">The desired resource consumption scope.</param>
		/// <returns>A version-safe name.</returns>
		// Token: 0x06003C32 RID: 15410 RVA: 0x000D11BE File Offset: 0x000CF3BE
		public static string MakeVersionSafeName(string name, ResourceScope from, ResourceScope to)
		{
			return VersioningHelper.MakeVersionSafeName(name, from, to, null);
		}

		/// <summary>Returns a version-safe name based on the specified resource name, the intended resource consumption scope, and the type using the resource.</summary>
		/// <param name="name">The name of the resource.</param>
		/// <param name="from">The beginning of the scope range.</param>
		/// <param name="to">The end of the scope range.</param>
		/// <param name="type">The <see cref="T:System.Type" /> of the resource.</param>
		/// <returns>A version-safe name.</returns>
		/// <exception cref="T:System.ArgumentException">The values for <paramref name="from" /> and <paramref name="to" /> are invalid. The resource type in the <see cref="T:System.Runtime.Versioning.ResourceScope" /> enumeration is going from a more restrictive resource type to a more general resource type.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is <see langword="null" />.</exception>
		// Token: 0x06003C33 RID: 15411 RVA: 0x000D11CC File Offset: 0x000CF3CC
		[SecuritySafeCritical]
		public static string MakeVersionSafeName(string name, ResourceScope from, ResourceScope to, Type type)
		{
			ResourceScope resourceScope = from & (ResourceScope.Machine | ResourceScope.Process | ResourceScope.AppDomain | ResourceScope.Library);
			ResourceScope resourceScope2 = to & (ResourceScope.Machine | ResourceScope.Process | ResourceScope.AppDomain | ResourceScope.Library);
			if (resourceScope > resourceScope2)
			{
				throw new ArgumentException(Environment.GetResourceString("Resource type in the ResourceScope enum is going from a more restrictive resource type to a more general one.  From: \"{0}\"  To: \"{1}\"", new object[]
				{
					resourceScope,
					resourceScope2
				}), "from");
			}
			SxSRequirements requirements = VersioningHelper.GetRequirements(to, from);
			if ((requirements & (SxSRequirements.AssemblyName | SxSRequirements.TypeName)) != SxSRequirements.None && type == null)
			{
				throw new ArgumentNullException("type", Environment.GetResourceString("The type parameter cannot be null when scoping the resource's visibility to Private or Assembly."));
			}
			StringBuilder stringBuilder = new StringBuilder(name);
			char value = '_';
			if ((requirements & SxSRequirements.ProcessID) != SxSRequirements.None)
			{
				stringBuilder.Append(value);
				stringBuilder.Append('p');
				stringBuilder.Append(NativeMethods.GetCurrentProcessId());
			}
			if ((requirements & SxSRequirements.CLRInstanceID) != SxSRequirements.None)
			{
				string clrinstanceString = VersioningHelper.GetCLRInstanceString();
				stringBuilder.Append(value);
				stringBuilder.Append('r');
				stringBuilder.Append(clrinstanceString);
			}
			if ((requirements & SxSRequirements.AppDomainID) != SxSRequirements.None)
			{
				stringBuilder.Append(value);
				stringBuilder.Append("ad");
				stringBuilder.Append(AppDomain.CurrentDomain.Id);
			}
			if ((requirements & SxSRequirements.TypeName) != SxSRequirements.None)
			{
				stringBuilder.Append(value);
				stringBuilder.Append(type.Name);
			}
			if ((requirements & SxSRequirements.AssemblyName) != SxSRequirements.None)
			{
				stringBuilder.Append(value);
				stringBuilder.Append(type.Assembly.FullName);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003C34 RID: 15412 RVA: 0x000D12FC File Offset: 0x000CF4FC
		private static string GetCLRInstanceString()
		{
			return VersioningHelper.GetRuntimeId().ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x06003C35 RID: 15413 RVA: 0x000D131C File Offset: 0x000CF51C
		private static SxSRequirements GetRequirements(ResourceScope consumeAsScope, ResourceScope calleeScope)
		{
			SxSRequirements sxSRequirements = SxSRequirements.None;
			switch (calleeScope & (ResourceScope.Machine | ResourceScope.Process | ResourceScope.AppDomain | ResourceScope.Library))
			{
			case ResourceScope.Machine:
				switch (consumeAsScope & (ResourceScope.Machine | ResourceScope.Process | ResourceScope.AppDomain | ResourceScope.Library))
				{
				case ResourceScope.Machine:
					goto IL_9F;
				case ResourceScope.Process:
					sxSRequirements |= SxSRequirements.ProcessID;
					goto IL_9F;
				case ResourceScope.AppDomain:
					sxSRequirements |= (SxSRequirements.AppDomainID | SxSRequirements.ProcessID | SxSRequirements.CLRInstanceID);
					goto IL_9F;
				}
				throw new ArgumentException(Environment.GetResourceString("Unknown value for the ResourceScope: {0}  Too many resource type bits may be set.", new object[]
				{
					consumeAsScope
				}), "consumeAsScope");
			case ResourceScope.Process:
				if ((consumeAsScope & ResourceScope.AppDomain) != ResourceScope.None)
				{
					sxSRequirements |= (SxSRequirements.AppDomainID | SxSRequirements.CLRInstanceID);
					goto IL_9F;
				}
				goto IL_9F;
			case ResourceScope.AppDomain:
				goto IL_9F;
			}
			throw new ArgumentException(Environment.GetResourceString("Unknown value for the ResourceScope: {0}  Too many resource type bits may be set.", new object[]
			{
				calleeScope
			}), "calleeScope");
			IL_9F:
			ResourceScope resourceScope = calleeScope & (ResourceScope.Private | ResourceScope.Assembly);
			if (resourceScope != ResourceScope.None)
			{
				if (resourceScope != ResourceScope.Private)
				{
					if (resourceScope != ResourceScope.Assembly)
					{
						throw new ArgumentException(Environment.GetResourceString("Unknown value for the ResourceScope: {0}  Too many resource visibility bits may be set.", new object[]
						{
							calleeScope
						}), "calleeScope");
					}
					if ((consumeAsScope & ResourceScope.Private) != ResourceScope.None)
					{
						sxSRequirements |= SxSRequirements.TypeName;
					}
				}
			}
			else
			{
				ResourceScope resourceScope2 = consumeAsScope & (ResourceScope.Private | ResourceScope.Assembly);
				if (resourceScope2 != ResourceScope.None)
				{
					if (resourceScope2 != ResourceScope.Private)
					{
						if (resourceScope2 != ResourceScope.Assembly)
						{
							throw new ArgumentException(Environment.GetResourceString("Unknown value for the ResourceScope: {0}  Too many resource visibility bits may be set.", new object[]
							{
								consumeAsScope
							}), "consumeAsScope");
						}
						sxSRequirements |= SxSRequirements.AssemblyName;
					}
					else
					{
						sxSRequirements |= (SxSRequirements.AssemblyName | SxSRequirements.TypeName);
					}
				}
			}
			return sxSRequirements;
		}

		// Token: 0x04002708 RID: 9992
		private const ResourceScope ResTypeMask = ResourceScope.Machine | ResourceScope.Process | ResourceScope.AppDomain | ResourceScope.Library;

		// Token: 0x04002709 RID: 9993
		private const ResourceScope VisibilityMask = ResourceScope.Private | ResourceScope.Assembly;
	}
}
