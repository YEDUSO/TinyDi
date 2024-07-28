using System;
using System.Collections.Generic;
using System.Linq;

namespace YEDUSO.TDI
{
    public class TinyDi
    {
        private static TinyDi _tinyDi;
        public static TinyDi Instance
        {
            get
            {
                if (_tinyDi == null)
                {
                    _tinyDi = new TinyDi();
                }
                return _tinyDi;
            }
        }

        private readonly Dictionary<Type, TinyDiClassDefinition> _entries;

        public TinyDi()
        {
            _entries = new Dictionary<Type, TinyDiClassDefinition>();
        }

        public void Register(Type type, TinyDiLifeCycle lifeCycle = TinyDiLifeCycle.Transient)
        {
            if (!_entries.ContainsKey(type))
            {
                _entries[type] = new TinyDiClassDefinition(type, lifeCycle);
            }
        }

        public void Register<T>(TinyDiLifeCycle lifeCycle = TinyDiLifeCycle.Transient)
        {
            Register<T, T>(lifeCycle);
        }

        public void Register<T1, T2>(TinyDiLifeCycle lifeCycle = TinyDiLifeCycle.Transient)
        {
            var typeT1 = typeof(T1);
            var typeT2 = typeof(T2);
            if (!_entries.ContainsKey(typeT1))
            {
                _entries[typeT1] = new TinyDiClassDefinition(typeT2, lifeCycle);
            }
            //else
            //{
            //    throw new ApplicationException($"Interface already registered: {typeT1.FullName}");
            //}
        }

        public void Register<T>(Func<object> creationFunction, TinyDiLifeCycle lifeCycle = TinyDiLifeCycle.Transient)
        {
            var typeT = typeof(T);
            if (!_entries.ContainsKey(typeT))
            {
                _entries[typeT] = new TinyDiClassDefinition(creationFunction, lifeCycle);
            }
            //else
            //{
            //    throw new ApplicationException($"Creation function already registered: {typeT.FullName}");
            //}
        }

        public void Register(object singleton)
        {
            var typeT = singleton.GetType();
            if (!_entries.ContainsKey(typeT))
            {
                _entries[typeT] = new TinyDiClassDefinition(singleton);
            }
            //else
            //{
            //    throw new ApplicationException($"Singleton already registered: {typeT.FullName}");
            //}
        }

        public void Clear()
        {
            _entries.Clear();
        }

        public void Build()
        {
            foreach (var kvp in _entries)
            {
                var entry = kvp.Value;
                if (entry.DefinitionType == TinyDiClassDefinitionType.Interface)
                {
                    var constructorInfoList = entry.ObjectType.GetConstructors().ToList();
                    foreach (var ci in constructorInfoList)
                    {
                        var constructorInfo = new TinyDiConstructorInfo();
                        entry.ConstructorInfoList.Add(constructorInfo);

                        foreach (var pi in ci.GetParameters())
                        {
                            var piType = pi.ParameterType;
                            TinyDiClassDefinition searchedEntry = null;
                            if (_entries.ContainsKey(piType))
                            {
                                searchedEntry = _entries[piType];
                            }
                            else
                            {
                                foreach (var kvpEntry in _entries)
                                {
                                    if (kvpEntry.Value.ObjectType == piType)
                                    {
                                        searchedEntry = kvpEntry.Value;
                                    }
                                }
                            }

                            var parameterInfo = new TinyDiParameterInfo
                            {
                                ParameterType = piType
                            };
                            constructorInfo.ParameterList.Add(parameterInfo);
                        }
                    }
                }
            }


            foreach (var entry in _entries)
            {
                var cil = entry.Value.ConstructorInfoList;
                cil = cil.OrderBy(_ => _.ParameterList.Count).ToList();
                entry.Value.ConstructorInfoList = cil;
            }
        }

        public T Resolve<T>()
        {
            var typeT = typeof(T);
            var objectMade = (T)CreateObject(typeT);
            return objectMade;
        }

        private object CreateObject(Type type)
        {
            if (_entries.ContainsKey(type))
            {
                var entry = _entries[type];

                switch (entry.DefinitionType)
                {
                    case TinyDiClassDefinitionType.Function:
                        if (entry.LifeCycle == TinyDiLifeCycle.Singleton)
                        {
                            entry.DefinitionType = TinyDiClassDefinitionType.Assignment;
                            entry.Object = entry.CreationFunction.Invoke();
                            return entry.Object;
                        }
                        else
                        {
                            return entry.CreationFunction.Invoke();
                        }
                        break;

                    case TinyDiClassDefinitionType.Assignment:
                        return entry.Object;

                    default:
                        var parameterList = new List<object>();
                        var ctorToUse = entry.ConstructorInfoList.LastOrDefault();
                        if (ctorToUse != null)
                        {
                            foreach (var parameter in ctorToUse.ParameterList)
                            {
                                parameterList.Add(CreateObject(parameter.ParameterType));
                            }
                        }

                        var obCreated = Activator.CreateInstance(entry.ObjectType, parameterList.ToArray());
                        if (entry.LifeCycle == TinyDiLifeCycle.Singleton)
                        {
                            entry.DefinitionType = TinyDiClassDefinitionType.Assignment;
                            entry.Object = obCreated;
                        }

                        return obCreated;
                }
            }

            return Activator.CreateInstance(type);
        }
    }
}
