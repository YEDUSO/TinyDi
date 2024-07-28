using System;

namespace YEDUSO.TDITester
{
    public class TestClassSubOne
    {
        private readonly ITestClassBaseOne _baseClassOne;
        private readonly TestClassBaseTwo _baseClassTwo;
        private readonly TestClassSingleton _singleton;
        private readonly Random _random;

        public TestClassSubOne()
        {
            _baseClassOne = null;
        }

        public TestClassSubOne(ITestClassBaseOne baseClassOne, TestClassBaseTwo baseClassTwo, TestClassSingleton singleton, Random random)
        {
            _baseClassOne = baseClassOne;
            _baseClassTwo = baseClassTwo;
            _singleton = singleton;
            _random = random;
        }

        public void CallSubOne()
        {
            Console.WriteLine("SubOne calling base one:");
            _baseClassOne.CallClassBaseOne();

            Console.WriteLine("SubOne calling base two:");
            _baseClassTwo.CallClassBaseTwo();

            _singleton.CallSingleton();

            Console.WriteLine($"RANDOM!  {_random.Next()}");
        }
    }
}
