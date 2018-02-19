using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MentoringProgram5.Attributes;

namespace MentoringProgram5
{
    public class Container
    {
        //почему AddAssembly? нужно по нескольких сборкам работать?
        //private List<Assembly> assemblies = new List<Assembly>();
        private Assembly assembly;
        private List<string> importTypes;
        private List<string> exportTypes;
        private Dictionary<string, string> exportWithBaseTypes;
        private List<string> importConstrTypes;

        public Container()
        {
            importTypes = new List<string>();
            importConstrTypes = new List<string>();
            exportTypes = new List<string>();
            exportWithBaseTypes = new Dictionary<string, string>();
        }

        public void AddAssembly(Assembly assembly)
        {
            this.assembly = Assembly.Load(assembly.FullName);
        }

        public void AddType(Type type)
        {
            try
            {
                var attributesCount = type.GetCustomAttributes().Count();

                if (attributesCount > 1)
                    throw new MyOwnException("У типа должен быть только один аттрибут");
            }
            catch (MyOwnException)
            {
                throw;
            }


            var properites = type.GetProperties();

            foreach (var prop in properites)
            {
                var propAttr = prop.GetCustomAttribute(typeof(ImportAttribute));

                if(propAttr != null)
                    importTypes.Add(prop.PropertyType.Name);
            }

            
            if (Attribute.IsDefined(type, typeof(ImportConstructorAttribute)))
            {
                importConstrTypes.Add(type.ToString());
            }
            else if (Attribute.IsDefined(type, typeof(ExportAttribute)))
            {
                var attr = type.GetCustomAttributes(false);
                var contract = ((ExportAttribute)attr.First()).Contract;

                if (contract == null)
                    exportTypes.Add(type.ToString());
            }
        }

        public void AddType(Type type, Type baseType)
        {
            if (Attribute.IsDefined(type, typeof(ExportAttribute)) && baseType != null)
            {
                exportWithBaseTypes.Add(type.ToString(), baseType.ToString());  
            }
        
        }

        public object CreateInstance(Type type)
        {
            return null;       
        }

        public T CreateInstance<T>(params Type[] argumentsTypes)
        {
            var type = typeof(T);//не могу засунуь дальше просто тип T
            var constrParams = new List<object>();

            if (importConstrTypes.Contains(type.ToString()))
            {
                var constructor = type.GetConstructor(BindingFlags.Instance | BindingFlags.Public, null,
                    argumentsTypes, null);//.Where(con => con.GetParameters().Length == arguments.Length).First();

                try
                {
                    foreach (var constAttr in constructor.GetParameters())
                    {
                        var constAttrType = assembly.GetName().Name + "." + constAttr.ParameterType.Name;
                        if (exportTypes.Contains(constAttrType))
                        {
                            var paramType = assembly.GetType(constAttr.ParameterType.FullName);
                            constrParams.Add(Activator.CreateInstance(paramType));
                        }
                        else if (exportWithBaseTypes.Values.Contains(constAttrType))
                        {
                            var key = exportWithBaseTypes.Where(_ => constAttrType == _.Value).Select(_ => _.Key).FirstOrDefault();
                            var paramKeyType = assembly.GetType(key);
                            var paramValueType = assembly.GetType(constAttrType);

                            var obj = Activator.CreateInstance(paramKeyType);




                            constrParams.Add(obj);
                        }
                        else
                            throw new MyOwnException("Данный тип отсутствует в контейнере");

                    }
                }
                catch (MyOwnException)
                {
                    throw;
                }

                if (constrParams.Count > 0)
                {
                    var resultObject = (T)type.GetConstructors().FirstOrDefault().Invoke(constrParams.ToArray());

                    return resultObject;
                }
                else
                {
                    var resultObject = Activator.CreateInstance<T>();

                    return resultObject;
                }

            }
            else
            {
                var resultObject = (T)type.GetConstructors().FirstOrDefault().Invoke(null);

                var properties = resultObject.GetType().GetProperties();

                foreach (var prop in properties)
                { 
                    var propType = prop.PropertyType.Name.ToString();

                    if (importTypes.Contains(propType))
                    {
                        if(prop.PropertyType.IsInterface)
                        {
                            var constAttrType = assembly.GetName().Name + "." + propType;
                            if (exportWithBaseTypes.Values.Contains(constAttrType))
                            {
                                var key = exportWithBaseTypes.Where(_ => constAttrType == _.Value).Select(_ => _.Key).FirstOrDefault();
                                var paramKeyType = assembly.GetType(key);
                                var propertyValue = Activator.CreateInstance(paramKeyType);
                                prop.SetValue(resultObject, propertyValue, null);
                            }                                                    
                        }
                        else
                        {
                            var propertyValue = Activator.CreateInstance(prop.PropertyType, null);
                            prop.SetValue(resultObject, propertyValue, null);
                        }
                    }
                }

                return resultObject;
            }
        }
    }
}