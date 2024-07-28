using System;
using System.Collections.Generic;

namespace YEDUSO.TDI
{
    public enum TinyDiClassDefinitionType
    {
        Interface,
        Function,
        Assignment
    }

    public class TinyDiClassDefinition
    {
        public TinyDiClassDefinitionType DefinitionType { get; set; }
        public Type ObjectType { get; private set; } // Interface
        public Func<object> CreationFunction { get; set; } // Function
        public object Object { get; set; } // Assignment
        public TinyDiLifeCycle LifeCycle { get; set; }
        public List<TinyDiConstructorInfo> ConstructorInfoList { get; set; } = new List<TinyDiConstructorInfo>();

        public TinyDiClassDefinition(Type objectType, TinyDiLifeCycle lifeCycle = TinyDiLifeCycle.Transient)
        {
            DefinitionType = TinyDiClassDefinitionType.Interface;
            ObjectType = objectType;
            LifeCycle = lifeCycle;
        }

        public TinyDiClassDefinition(Func<object> creationFunction, TinyDiLifeCycle lifeCycle = TinyDiLifeCycle.Transient)
        {
            DefinitionType = TinyDiClassDefinitionType.Function;
            CreationFunction = creationFunction;
            LifeCycle = lifeCycle;
        }

        public TinyDiClassDefinition(object ob)
        {
            DefinitionType = TinyDiClassDefinitionType.Assignment;
            Object = ob;
            LifeCycle = TinyDiLifeCycle.Singleton;
        }
    }
}
