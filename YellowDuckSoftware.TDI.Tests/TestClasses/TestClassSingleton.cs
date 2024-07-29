using System;

namespace YellowDuckSoftware.TDITester
{
    public class TestClassSingleton
    {
        private int _value;

        public TestClassSingleton()
        {
            _value = new Random().Next();
        }

        public void CallSingleton()
        {
            Console.WriteLine($"Singleton: {_value}");
        }
    }
}
