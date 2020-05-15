using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Runtime.Serialization;
using Task.DB;

namespace Task
{
    public class EntityDataContractSurrogate : IDataContractSurrogate
    {
        private int _count;

        public Type GetDataContractType(Type type)
            => type;

        public object GetDeserializedObject(object obj, Type targetType)
            => obj;

        public object GetObjectToSerialize(object obj, Type targetType)
        {
            if (_count > 0 && obj is IEnumerable collection)
                foreach (var variable in collection)
                    _count++;

            if (!obj.GetType().FullName.StartsWith("System.Data.Entity.DynamicProxies."))
                return obj;

            var newObj = Activator.CreateInstance(targetType);
            var newObjProp = newObj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            if (_count > 0)
            {
                _count--;
                foreach (var prop in newObjProp)
                    prop.SetValue(newObj, prop.GetAccessors()[0].IsVirtual ? default : prop.GetValue(obj));
            }
            else
                foreach (var prop in newObjProp)
                {
                    var val = prop.GetValue(obj);
                    if (prop.GetAccessors()[0].IsVirtual && !(val is IEnumerable))
                        _count++;
                    prop.SetValue(newObj, val);
                }
            return newObj;
        }

        public void GetKnownCustomDataTypes(Collection<Type> customDataTypes) 
            => throw new NotImplementedException();

        public Type GetReferencedTypeOnImport(string typeName, string typeNamespace, object customData) 
            => throw new NotImplementedException();

        public CodeTypeDeclaration ProcessImportedType(CodeTypeDeclaration typeDeclaration, CodeCompileUnit compileUnit) 
            => throw new NotImplementedException();
        public object GetCustomDataToExport(MemberInfo memberInfo, Type dataContractType) 
            => throw new NotImplementedException();

        public object GetCustomDataToExport(Type clrType, Type dataContractType) 
            => throw new NotImplementedException();
    }
}
