using System;
using System.Text;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200000A RID: 10
	[UsedByNativeCode]
	internal sealed class _AndroidJNIHelper
	{
		// Token: 0x06000052 RID: 82 RVA: 0x00004A9C File Offset: 0x00002C9C
		public static IntPtr CreateJavaProxy(IntPtr player, IntPtr delegateHandle, AndroidJavaProxy proxy)
		{
			return AndroidReflection.NewProxyInstance(player, delegateHandle, proxy.javaInterface.GetRawClass());
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00004AC0 File Offset: 0x00002CC0
		public static IntPtr CreateJavaRunnable(AndroidJavaRunnable jrunnable)
		{
			return AndroidJNIHelper.CreateJavaProxy(new AndroidJavaRunnableProxy(jrunnable));
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00004AE0 File Offset: 0x00002CE0
		[RequiredByNativeCode]
		public static IntPtr InvokeJavaProxyMethod(AndroidJavaProxy proxy, IntPtr jmethodName, IntPtr jargs)
		{
			IntPtr result;
			try
			{
				int num = 0;
				bool flag = jargs != IntPtr.Zero;
				if (flag)
				{
					num = AndroidJNISafe.GetArrayLength(jargs);
				}
				AndroidJavaObject[] array = new AndroidJavaObject[num];
				for (int i = 0; i < num; i++)
				{
					IntPtr objectArrayElement = AndroidJNISafe.GetObjectArrayElement(jargs, i);
					array[i] = ((objectArrayElement != IntPtr.Zero) ? new AndroidJavaObject(objectArrayElement) : null);
				}
				using (AndroidJavaObject androidJavaObject = proxy.Invoke(AndroidJNI.GetStringChars(jmethodName), array))
				{
					bool flag2 = androidJavaObject == null;
					if (flag2)
					{
						result = IntPtr.Zero;
					}
					else
					{
						result = AndroidJNI.NewLocalRef(androidJavaObject.GetRawObject());
					}
				}
			}
			catch (Exception ex)
			{
				result = AndroidReflection.CreateInvocationError(ex, false);
			}
			return result;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00004BB8 File Offset: 0x00002DB8
		public static jvalue[] CreateJNIArgArray(object[] args)
		{
			jvalue[] array = new jvalue[args.GetLength(0)];
			int num = 0;
			foreach (object obj in args)
			{
				bool flag = obj == null;
				if (flag)
				{
					array[num].l = IntPtr.Zero;
				}
				else
				{
					bool flag2 = AndroidReflection.IsPrimitive(obj.GetType());
					if (flag2)
					{
						bool flag3 = obj is int;
						if (flag3)
						{
							array[num].i = (int)obj;
						}
						else
						{
							bool flag4 = obj is bool;
							if (flag4)
							{
								array[num].z = (bool)obj;
							}
							else
							{
								bool flag5 = obj is byte;
								if (flag5)
								{
									Debug.LogWarning("Passing Byte arguments to Java methods is obsolete, pass SByte parameters instead");
									array[num].b = (sbyte)((byte)obj);
								}
								else
								{
									bool flag6 = obj is sbyte;
									if (flag6)
									{
										array[num].b = (sbyte)obj;
									}
									else
									{
										bool flag7 = obj is short;
										if (flag7)
										{
											array[num].s = (short)obj;
										}
										else
										{
											bool flag8 = obj is long;
											if (flag8)
											{
												array[num].j = (long)obj;
											}
											else
											{
												bool flag9 = obj is float;
												if (flag9)
												{
													array[num].f = (float)obj;
												}
												else
												{
													bool flag10 = obj is double;
													if (flag10)
													{
														array[num].d = (double)obj;
													}
													else
													{
														bool flag11 = obj is char;
														if (flag11)
														{
															array[num].c = (char)obj;
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
					else
					{
						bool flag12 = obj is string;
						if (flag12)
						{
							array[num].l = AndroidJNISafe.NewString((string)obj);
						}
						else
						{
							bool flag13 = obj is AndroidJavaClass;
							if (flag13)
							{
								array[num].l = ((AndroidJavaClass)obj).GetRawClass();
							}
							else
							{
								bool flag14 = obj is AndroidJavaObject;
								if (flag14)
								{
									array[num].l = ((AndroidJavaObject)obj).GetRawObject();
								}
								else
								{
									bool flag15 = obj is Array;
									if (flag15)
									{
										array[num].l = _AndroidJNIHelper.ConvertToJNIArray((Array)obj);
									}
									else
									{
										bool flag16 = obj is AndroidJavaProxy;
										if (flag16)
										{
											array[num].l = ((AndroidJavaProxy)obj).GetRawProxy();
										}
										else
										{
											bool flag17 = obj is AndroidJavaRunnable;
											if (!flag17)
											{
												string str = "JNI; Unknown argument type '";
												Type type = obj.GetType();
												throw new Exception(str + ((type != null) ? type.ToString() : null) + "'");
											}
											array[num].l = AndroidJNIHelper.CreateJavaRunnable((AndroidJavaRunnable)obj);
										}
									}
								}
							}
						}
					}
				}
				num++;
			}
			return array;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00004EE0 File Offset: 0x000030E0
		public static object UnboxArray(AndroidJavaObject obj)
		{
			bool flag = obj == null;
			object result;
			if (flag)
			{
				result = null;
			}
			else
			{
				AndroidJavaClass androidJavaClass = new AndroidJavaClass("java/lang/reflect/Array");
				AndroidJavaObject androidJavaObject = obj.Call<AndroidJavaObject>("getClass", new object[0]);
				AndroidJavaObject androidJavaObject2 = androidJavaObject.Call<AndroidJavaObject>("getComponentType", new object[0]);
				string text = androidJavaObject2.Call<string>("getName", new object[0]);
				int num = androidJavaClass.CallStatic<int>("getLength", new object[]
				{
					obj
				});
				bool flag2 = androidJavaObject2.Call<bool>("isPrimitive", new object[0]);
				Array array;
				if (flag2)
				{
					bool flag3 = "int" == text;
					if (flag3)
					{
						array = new int[num];
					}
					else
					{
						bool flag4 = "boolean" == text;
						if (flag4)
						{
							array = new bool[num];
						}
						else
						{
							bool flag5 = "byte" == text;
							if (flag5)
							{
								array = new sbyte[num];
							}
							else
							{
								bool flag6 = "short" == text;
								if (flag6)
								{
									array = new short[num];
								}
								else
								{
									bool flag7 = "long" == text;
									if (flag7)
									{
										array = new long[num];
									}
									else
									{
										bool flag8 = "float" == text;
										if (flag8)
										{
											array = new float[num];
										}
										else
										{
											bool flag9 = "double" == text;
											if (flag9)
											{
												array = new double[num];
											}
											else
											{
												bool flag10 = "char" == text;
												if (!flag10)
												{
													throw new Exception("JNI; Unknown argument type '" + text + "'");
												}
												array = new char[num];
											}
										}
									}
								}
							}
						}
					}
				}
				else
				{
					bool flag11 = "java.lang.String" == text;
					if (flag11)
					{
						array = new string[num];
					}
					else
					{
						bool flag12 = "java.lang.Class" == text;
						if (flag12)
						{
							array = new AndroidJavaClass[num];
						}
						else
						{
							array = new AndroidJavaObject[num];
						}
					}
				}
				for (int i = 0; i < num; i++)
				{
					array.SetValue(_AndroidJNIHelper.Unbox(androidJavaClass.CallStatic<AndroidJavaObject>("get", new object[]
					{
						obj,
						i
					})), i);
				}
				androidJavaClass.Dispose();
				result = array;
			}
			return result;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00005110 File Offset: 0x00003310
		public static object Unbox(AndroidJavaObject obj)
		{
			bool flag = obj == null;
			object result;
			if (flag)
			{
				result = null;
			}
			else
			{
				using (AndroidJavaObject androidJavaObject = obj.Call<AndroidJavaObject>("getClass", new object[0]))
				{
					string b = androidJavaObject.Call<string>("getName", new object[0]);
					bool flag2 = "java.lang.Integer" == b;
					if (flag2)
					{
						result = obj.Call<int>("intValue", new object[0]);
					}
					else
					{
						bool flag3 = "java.lang.Boolean" == b;
						if (flag3)
						{
							result = obj.Call<bool>("booleanValue", new object[0]);
						}
						else
						{
							bool flag4 = "java.lang.Byte" == b;
							if (flag4)
							{
								result = obj.Call<sbyte>("byteValue", new object[0]);
							}
							else
							{
								bool flag5 = "java.lang.Short" == b;
								if (flag5)
								{
									result = obj.Call<short>("shortValue", new object[0]);
								}
								else
								{
									bool flag6 = "java.lang.Long" == b;
									if (flag6)
									{
										result = obj.Call<long>("longValue", new object[0]);
									}
									else
									{
										bool flag7 = "java.lang.Float" == b;
										if (flag7)
										{
											result = obj.Call<float>("floatValue", new object[0]);
										}
										else
										{
											bool flag8 = "java.lang.Double" == b;
											if (flag8)
											{
												result = obj.Call<double>("doubleValue", new object[0]);
											}
											else
											{
												bool flag9 = "java.lang.Character" == b;
												if (flag9)
												{
													result = obj.Call<char>("charValue", new object[0]);
												}
												else
												{
													bool flag10 = "java.lang.String" == b;
													if (flag10)
													{
														result = obj.Call<string>("toString", new object[0]);
													}
													else
													{
														bool flag11 = "java.lang.Class" == b;
														if (flag11)
														{
															result = new AndroidJavaClass(obj.GetRawObject());
														}
														else
														{
															bool flag12 = androidJavaObject.Call<bool>("isArray", new object[0]);
															if (flag12)
															{
																result = _AndroidJNIHelper.UnboxArray(obj);
															}
															else
															{
																result = obj;
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00005348 File Offset: 0x00003548
		public static AndroidJavaObject Box(object obj)
		{
			bool flag = obj == null;
			AndroidJavaObject result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = AndroidReflection.IsPrimitive(obj.GetType());
				if (flag2)
				{
					bool flag3 = obj is int;
					if (flag3)
					{
						result = new AndroidJavaObject("java.lang.Integer", new object[]
						{
							(int)obj
						});
					}
					else
					{
						bool flag4 = obj is bool;
						if (flag4)
						{
							result = new AndroidJavaObject("java.lang.Boolean", new object[]
							{
								(bool)obj
							});
						}
						else
						{
							bool flag5 = obj is byte;
							if (flag5)
							{
								result = new AndroidJavaObject("java.lang.Byte", new object[]
								{
									(sbyte)obj
								});
							}
							else
							{
								bool flag6 = obj is sbyte;
								if (flag6)
								{
									result = new AndroidJavaObject("java.lang.Byte", new object[]
									{
										(sbyte)obj
									});
								}
								else
								{
									bool flag7 = obj is short;
									if (flag7)
									{
										result = new AndroidJavaObject("java.lang.Short", new object[]
										{
											(short)obj
										});
									}
									else
									{
										bool flag8 = obj is long;
										if (flag8)
										{
											result = new AndroidJavaObject("java.lang.Long", new object[]
											{
												(long)obj
											});
										}
										else
										{
											bool flag9 = obj is float;
											if (flag9)
											{
												result = new AndroidJavaObject("java.lang.Float", new object[]
												{
													(float)obj
												});
											}
											else
											{
												bool flag10 = obj is double;
												if (flag10)
												{
													result = new AndroidJavaObject("java.lang.Double", new object[]
													{
														(double)obj
													});
												}
												else
												{
													bool flag11 = obj is char;
													if (!flag11)
													{
														string str = "JNI; Unknown argument type '";
														Type type = obj.GetType();
														throw new Exception(str + ((type != null) ? type.ToString() : null) + "'");
													}
													result = new AndroidJavaObject("java.lang.Character", new object[]
													{
														(char)obj
													});
												}
											}
										}
									}
								}
							}
						}
					}
				}
				else
				{
					bool flag12 = obj is string;
					if (flag12)
					{
						result = new AndroidJavaObject("java.lang.String", new object[]
						{
							(string)obj
						});
					}
					else
					{
						bool flag13 = obj is AndroidJavaClass;
						if (flag13)
						{
							result = new AndroidJavaObject(((AndroidJavaClass)obj).GetRawClass());
						}
						else
						{
							bool flag14 = obj is AndroidJavaObject;
							if (flag14)
							{
								result = (AndroidJavaObject)obj;
							}
							else
							{
								bool flag15 = obj is Array;
								if (flag15)
								{
									result = AndroidJavaObject.AndroidJavaObjectDeleteLocalRef(_AndroidJNIHelper.ConvertToJNIArray((Array)obj));
								}
								else
								{
									bool flag16 = obj is AndroidJavaProxy;
									if (flag16)
									{
										result = ((AndroidJavaProxy)obj).GetProxyObject();
									}
									else
									{
										bool flag17 = obj is AndroidJavaRunnable;
										if (!flag17)
										{
											string str2 = "JNI; Unknown argument type '";
											Type type2 = obj.GetType();
											throw new Exception(str2 + ((type2 != null) ? type2.ToString() : null) + "'");
										}
										result = AndroidJavaObject.AndroidJavaObjectDeleteLocalRef(AndroidJNIHelper.CreateJavaRunnable((AndroidJavaRunnable)obj));
									}
								}
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00005668 File Offset: 0x00003868
		public static void DeleteJNIArgArray(object[] args, jvalue[] jniArgs)
		{
			int num = 0;
			foreach (object obj in args)
			{
				bool flag = obj is string || obj is AndroidJavaRunnable || obj is AndroidJavaProxy || obj is Array;
				if (flag)
				{
					AndroidJNISafe.DeleteLocalRef(jniArgs[num].l);
				}
				num++;
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000056D0 File Offset: 0x000038D0
		public static IntPtr ConvertToJNIArray(Array array)
		{
			Type elementType = array.GetType().GetElementType();
			bool flag = AndroidReflection.IsPrimitive(elementType);
			IntPtr result;
			if (flag)
			{
				bool flag2 = elementType == typeof(int);
				if (flag2)
				{
					result = AndroidJNISafe.ToIntArray((int[])array);
				}
				else
				{
					bool flag3 = elementType == typeof(bool);
					if (flag3)
					{
						result = AndroidJNISafe.ToBooleanArray((bool[])array);
					}
					else
					{
						bool flag4 = elementType == typeof(byte);
						if (flag4)
						{
							Debug.LogWarning("AndroidJNIHelper: converting Byte array is obsolete, use SByte array instead");
							result = AndroidJNISafe.ToByteArray((byte[])array);
						}
						else
						{
							bool flag5 = elementType == typeof(sbyte);
							if (flag5)
							{
								result = AndroidJNISafe.ToSByteArray((sbyte[])array);
							}
							else
							{
								bool flag6 = elementType == typeof(short);
								if (flag6)
								{
									result = AndroidJNISafe.ToShortArray((short[])array);
								}
								else
								{
									bool flag7 = elementType == typeof(long);
									if (flag7)
									{
										result = AndroidJNISafe.ToLongArray((long[])array);
									}
									else
									{
										bool flag8 = elementType == typeof(float);
										if (flag8)
										{
											result = AndroidJNISafe.ToFloatArray((float[])array);
										}
										else
										{
											bool flag9 = elementType == typeof(double);
											if (flag9)
											{
												result = AndroidJNISafe.ToDoubleArray((double[])array);
											}
											else
											{
												bool flag10 = elementType == typeof(char);
												if (flag10)
												{
													result = AndroidJNISafe.ToCharArray((char[])array);
												}
												else
												{
													result = IntPtr.Zero;
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			else
			{
				bool flag11 = elementType == typeof(string);
				if (flag11)
				{
					string[] array2 = (string[])array;
					int length = array.GetLength(0);
					IntPtr intPtr = AndroidJNISafe.FindClass("java/lang/String");
					IntPtr intPtr2 = AndroidJNI.NewObjectArray(length, intPtr, IntPtr.Zero);
					for (int i = 0; i < length; i++)
					{
						IntPtr intPtr3 = AndroidJNISafe.NewString(array2[i]);
						AndroidJNI.SetObjectArrayElement(intPtr2, i, intPtr3);
						AndroidJNISafe.DeleteLocalRef(intPtr3);
					}
					AndroidJNISafe.DeleteLocalRef(intPtr);
					result = intPtr2;
				}
				else
				{
					bool flag12 = elementType == typeof(AndroidJavaObject);
					if (flag12)
					{
						AndroidJavaObject[] array3 = (AndroidJavaObject[])array;
						int length2 = array.GetLength(0);
						IntPtr[] array4 = new IntPtr[length2];
						IntPtr intPtr4 = AndroidJNISafe.FindClass("java/lang/Object");
						IntPtr intPtr5 = IntPtr.Zero;
						for (int j = 0; j < length2; j++)
						{
							bool flag13 = array3[j] != null;
							if (flag13)
							{
								array4[j] = array3[j].GetRawObject();
								IntPtr rawClass = array3[j].GetRawClass();
								bool flag14 = intPtr5 == IntPtr.Zero;
								if (flag14)
								{
									intPtr5 = rawClass;
								}
								else
								{
									bool flag15 = intPtr5 != intPtr4 && !AndroidJNI.IsSameObject(intPtr5, rawClass);
									if (flag15)
									{
										intPtr5 = intPtr4;
									}
								}
							}
							else
							{
								array4[j] = IntPtr.Zero;
							}
						}
						IntPtr intPtr6 = AndroidJNISafe.ToObjectArray(array4, intPtr5);
						AndroidJNISafe.DeleteLocalRef(intPtr4);
						result = intPtr6;
					}
					else
					{
						bool flag16 = AndroidReflection.IsAssignableFrom(typeof(AndroidJavaProxy), elementType);
						if (!flag16)
						{
							string str = "JNI; Unknown array type '";
							Type type = elementType;
							throw new Exception(str + ((type != null) ? type.ToString() : null) + "'");
						}
						AndroidJavaProxy[] array5 = (AndroidJavaProxy[])array;
						int length3 = array.GetLength(0);
						IntPtr[] array6 = new IntPtr[length3];
						IntPtr intPtr7 = AndroidJNISafe.FindClass("java/lang/Object");
						IntPtr intPtr8 = IntPtr.Zero;
						for (int k = 0; k < length3; k++)
						{
							bool flag17 = array5[k] != null;
							if (flag17)
							{
								array6[k] = array5[k].GetRawProxy();
								IntPtr rawClass2 = array5[k].javaInterface.GetRawClass();
								bool flag18 = intPtr8 == IntPtr.Zero;
								if (flag18)
								{
									intPtr8 = rawClass2;
								}
								else
								{
									bool flag19 = intPtr8 != intPtr7 && !AndroidJNI.IsSameObject(intPtr8, rawClass2);
									if (flag19)
									{
										intPtr8 = intPtr7;
									}
								}
							}
							else
							{
								array6[k] = IntPtr.Zero;
							}
						}
						IntPtr intPtr9 = AndroidJNISafe.ToObjectArray(array6, intPtr8);
						AndroidJNISafe.DeleteLocalRef(intPtr7);
						result = intPtr9;
					}
				}
			}
			return result;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00005AF8 File Offset: 0x00003CF8
		public static ArrayType ConvertFromJNIArray<ArrayType>(IntPtr array)
		{
			Type elementType = typeof(ArrayType).GetElementType();
			bool flag = AndroidReflection.IsPrimitive(elementType);
			ArrayType result;
			if (flag)
			{
				bool flag2 = elementType == typeof(int);
				if (flag2)
				{
					result = (ArrayType)((object)AndroidJNISafe.FromIntArray(array));
				}
				else
				{
					bool flag3 = elementType == typeof(bool);
					if (flag3)
					{
						result = (ArrayType)((object)AndroidJNISafe.FromBooleanArray(array));
					}
					else
					{
						bool flag4 = elementType == typeof(byte);
						if (flag4)
						{
							Debug.LogWarning("AndroidJNIHelper: converting from Byte array is obsolete, use SByte array instead");
							result = (ArrayType)((object)AndroidJNISafe.FromByteArray(array));
						}
						else
						{
							bool flag5 = elementType == typeof(sbyte);
							if (flag5)
							{
								result = (ArrayType)((object)AndroidJNISafe.FromSByteArray(array));
							}
							else
							{
								bool flag6 = elementType == typeof(short);
								if (flag6)
								{
									result = (ArrayType)((object)AndroidJNISafe.FromShortArray(array));
								}
								else
								{
									bool flag7 = elementType == typeof(long);
									if (flag7)
									{
										result = (ArrayType)((object)AndroidJNISafe.FromLongArray(array));
									}
									else
									{
										bool flag8 = elementType == typeof(float);
										if (flag8)
										{
											result = (ArrayType)((object)AndroidJNISafe.FromFloatArray(array));
										}
										else
										{
											bool flag9 = elementType == typeof(double);
											if (flag9)
											{
												result = (ArrayType)((object)AndroidJNISafe.FromDoubleArray(array));
											}
											else
											{
												bool flag10 = elementType == typeof(char);
												if (flag10)
												{
													result = (ArrayType)((object)AndroidJNISafe.FromCharArray(array));
												}
												else
												{
													result = default(ArrayType);
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			else
			{
				bool flag11 = elementType == typeof(string);
				if (flag11)
				{
					int arrayLength = AndroidJNISafe.GetArrayLength(array);
					string[] array2 = new string[arrayLength];
					for (int i = 0; i < arrayLength; i++)
					{
						IntPtr objectArrayElement = AndroidJNI.GetObjectArrayElement(array, i);
						array2[i] = AndroidJNISafe.GetStringChars(objectArrayElement);
						AndroidJNISafe.DeleteLocalRef(objectArrayElement);
					}
					result = (ArrayType)((object)array2);
				}
				else
				{
					bool flag12 = elementType == typeof(AndroidJavaObject);
					if (!flag12)
					{
						string str = "JNI: Unknown generic array type '";
						Type type = elementType;
						throw new Exception(str + ((type != null) ? type.ToString() : null) + "'");
					}
					int arrayLength2 = AndroidJNISafe.GetArrayLength(array);
					AndroidJavaObject[] array3 = new AndroidJavaObject[arrayLength2];
					for (int j = 0; j < arrayLength2; j++)
					{
						IntPtr objectArrayElement2 = AndroidJNI.GetObjectArrayElement(array, j);
						array3[j] = new AndroidJavaObject(objectArrayElement2);
						AndroidJNISafe.DeleteLocalRef(objectArrayElement2);
					}
					result = (ArrayType)((object)array3);
				}
			}
			return result;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00005D78 File Offset: 0x00003F78
		public static IntPtr GetConstructorID(IntPtr jclass, object[] args)
		{
			return AndroidJNIHelper.GetConstructorID(jclass, _AndroidJNIHelper.GetSignature(args));
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00005D98 File Offset: 0x00003F98
		public static IntPtr GetMethodID(IntPtr jclass, string methodName, object[] args, bool isStatic)
		{
			return AndroidJNIHelper.GetMethodID(jclass, methodName, _AndroidJNIHelper.GetSignature(args), isStatic);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00005DB8 File Offset: 0x00003FB8
		public static IntPtr GetMethodID<ReturnType>(IntPtr jclass, string methodName, object[] args, bool isStatic)
		{
			return AndroidJNIHelper.GetMethodID(jclass, methodName, _AndroidJNIHelper.GetSignature<ReturnType>(args), isStatic);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00005DD8 File Offset: 0x00003FD8
		public static IntPtr GetFieldID<ReturnType>(IntPtr jclass, string fieldName, bool isStatic)
		{
			return AndroidJNIHelper.GetFieldID(jclass, fieldName, _AndroidJNIHelper.GetSignature(typeof(ReturnType)), isStatic);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00005E04 File Offset: 0x00004004
		public static IntPtr GetConstructorID(IntPtr jclass, string signature)
		{
			IntPtr intPtr = IntPtr.Zero;
			IntPtr result;
			try
			{
				intPtr = AndroidReflection.GetConstructorMember(jclass, signature);
				result = AndroidJNISafe.FromReflectedMethod(intPtr);
			}
			catch (Exception ex)
			{
				IntPtr methodID = AndroidJNISafe.GetMethodID(jclass, "<init>", signature);
				bool flag = methodID != IntPtr.Zero;
				if (!flag)
				{
					throw ex;
				}
				result = methodID;
			}
			finally
			{
				AndroidJNISafe.DeleteLocalRef(intPtr);
			}
			return result;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00005E78 File Offset: 0x00004078
		public static IntPtr GetMethodID(IntPtr jclass, string methodName, string signature, bool isStatic)
		{
			IntPtr intPtr = IntPtr.Zero;
			IntPtr result;
			try
			{
				intPtr = AndroidReflection.GetMethodMember(jclass, methodName, signature, isStatic);
				result = AndroidJNISafe.FromReflectedMethod(intPtr);
			}
			catch (Exception ex)
			{
				IntPtr methodIDFallback = _AndroidJNIHelper.GetMethodIDFallback(jclass, methodName, signature, isStatic);
				bool flag = methodIDFallback != IntPtr.Zero;
				if (!flag)
				{
					throw ex;
				}
				result = methodIDFallback;
			}
			finally
			{
				AndroidJNISafe.DeleteLocalRef(intPtr);
			}
			return result;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00005EEC File Offset: 0x000040EC
		private static IntPtr GetMethodIDFallback(IntPtr jclass, string methodName, string signature, bool isStatic)
		{
			try
			{
				return isStatic ? AndroidJNISafe.GetStaticMethodID(jclass, methodName, signature) : AndroidJNISafe.GetMethodID(jclass, methodName, signature);
			}
			catch (Exception)
			{
			}
			return IntPtr.Zero;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00005F34 File Offset: 0x00004134
		public static IntPtr GetFieldID(IntPtr jclass, string fieldName, string signature, bool isStatic)
		{
			IntPtr intPtr = IntPtr.Zero;
			Exception ex = null;
			AndroidJNI.PushLocalFrame(10);
			try
			{
				IntPtr fieldMember = AndroidReflection.GetFieldMember(jclass, fieldName, signature, isStatic);
				bool flag = !isStatic;
				if (flag)
				{
					jclass = AndroidReflection.GetFieldClass(fieldMember);
				}
				signature = AndroidReflection.GetFieldSignature(fieldMember);
			}
			catch (Exception ex2)
			{
				ex = ex2;
			}
			IntPtr result;
			try
			{
				intPtr = (isStatic ? AndroidJNISafe.GetStaticFieldID(jclass, fieldName, signature) : AndroidJNISafe.GetFieldID(jclass, fieldName, signature));
				bool flag2 = intPtr == IntPtr.Zero;
				if (flag2)
				{
					bool flag3 = ex != null;
					if (flag3)
					{
						throw ex;
					}
					throw new Exception(string.Format("Field {0} or type signature {1} not found", fieldName, signature));
				}
				else
				{
					result = intPtr;
				}
			}
			finally
			{
				AndroidJNI.PopLocalFrame(IntPtr.Zero);
			}
			return result;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00005FFC File Offset: 0x000041FC
		public static string GetSignature(object obj)
		{
			bool flag = obj == null;
			string result;
			if (flag)
			{
				result = "Ljava/lang/Object;";
			}
			else
			{
				Type type = (obj is Type) ? ((Type)obj) : obj.GetType();
				bool flag2 = AndroidReflection.IsPrimitive(type);
				if (flag2)
				{
					bool flag3 = type.Equals(typeof(int));
					if (flag3)
					{
						result = "I";
					}
					else
					{
						bool flag4 = type.Equals(typeof(bool));
						if (flag4)
						{
							result = "Z";
						}
						else
						{
							bool flag5 = type.Equals(typeof(byte));
							if (flag5)
							{
								Debug.LogWarning("AndroidJNIHelper.GetSignature: using Byte parameters is obsolete, use SByte parameters instead");
								result = "B";
							}
							else
							{
								bool flag6 = type.Equals(typeof(sbyte));
								if (flag6)
								{
									result = "B";
								}
								else
								{
									bool flag7 = type.Equals(typeof(short));
									if (flag7)
									{
										result = "S";
									}
									else
									{
										bool flag8 = type.Equals(typeof(long));
										if (flag8)
										{
											result = "J";
										}
										else
										{
											bool flag9 = type.Equals(typeof(float));
											if (flag9)
											{
												result = "F";
											}
											else
											{
												bool flag10 = type.Equals(typeof(double));
												if (flag10)
												{
													result = "D";
												}
												else
												{
													bool flag11 = type.Equals(typeof(char));
													if (flag11)
													{
														result = "C";
													}
													else
													{
														result = "";
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
				else
				{
					bool flag12 = type.Equals(typeof(string));
					if (flag12)
					{
						result = "Ljava/lang/String;";
					}
					else
					{
						bool flag13 = obj is AndroidJavaProxy;
						if (flag13)
						{
							using (AndroidJavaObject androidJavaObject = new AndroidJavaObject(((AndroidJavaProxy)obj).javaInterface.GetRawClass()))
							{
								return "L" + androidJavaObject.Call<string>("getName", new object[0]) + ";";
							}
						}
						bool flag14 = obj == type && AndroidReflection.IsAssignableFrom(typeof(AndroidJavaProxy), type);
						if (flag14)
						{
							result = "";
						}
						else
						{
							bool flag15 = type.Equals(typeof(AndroidJavaRunnable));
							if (flag15)
							{
								result = "Ljava/lang/Runnable;";
							}
							else
							{
								bool flag16 = type.Equals(typeof(AndroidJavaClass));
								if (flag16)
								{
									result = "Ljava/lang/Class;";
								}
								else
								{
									bool flag17 = type.Equals(typeof(AndroidJavaObject));
									if (flag17)
									{
										bool flag18 = obj == type;
										if (flag18)
										{
											return "Ljava/lang/Object;";
										}
										AndroidJavaObject androidJavaObject2 = (AndroidJavaObject)obj;
										using (AndroidJavaObject androidJavaObject3 = androidJavaObject2.Call<AndroidJavaObject>("getClass", new object[0]))
										{
											return "L" + androidJavaObject3.Call<string>("getName", new object[0]) + ";";
										}
									}
									bool flag19 = AndroidReflection.IsAssignableFrom(typeof(Array), type);
									if (!flag19)
									{
										string[] array = new string[6];
										array[0] = "JNI: Unknown signature for type '";
										int num = 1;
										Type type2 = type;
										array[num] = ((type2 != null) ? type2.ToString() : null);
										array[2] = "' (obj = ";
										array[3] = ((obj != null) ? obj.ToString() : null);
										array[4] = ") ";
										array[5] = ((type == obj) ? "equal" : "instance");
										throw new Exception(string.Concat(array));
									}
									bool flag20 = type.GetArrayRank() != 1;
									if (flag20)
									{
										throw new Exception("JNI: System.Array in n dimensions is not allowed");
									}
									StringBuilder stringBuilder = new StringBuilder();
									stringBuilder.Append('[');
									stringBuilder.Append(_AndroidJNIHelper.GetSignature(type.GetElementType()));
									result = ((stringBuilder.Length > 1) ? stringBuilder.ToString() : "");
								}
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000063DC File Offset: 0x000045DC
		public static string GetSignature(object[] args)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('(');
			foreach (object obj in args)
			{
				stringBuilder.Append(_AndroidJNIHelper.GetSignature(obj));
			}
			stringBuilder.Append(")V");
			return stringBuilder.ToString();
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00006438 File Offset: 0x00004638
		public static string GetSignature<ReturnType>(object[] args)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('(');
			foreach (object obj in args)
			{
				stringBuilder.Append(_AndroidJNIHelper.GetSignature(obj));
			}
			stringBuilder.Append(')');
			stringBuilder.Append(_AndroidJNIHelper.GetSignature(typeof(ReturnType)));
			return stringBuilder.ToString();
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000049C2 File Offset: 0x00002BC2
		public _AndroidJNIHelper()
		{
		}
	}
}
