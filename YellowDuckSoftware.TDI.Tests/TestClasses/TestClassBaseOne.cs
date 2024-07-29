using System;

namespace YellowDuckSoftware.TDITester
{
    public class TestClassBaseOne : ITestClassBaseOne
    {
        public TestClassBaseOne()
        {
        }

        public void CallClassBaseOne()
        {
            Console.WriteLine("Class Base One");
        }
    }
}
