using Xunit;
using YellowDuckSoftware.TDITester;

namespace YellowDuckSoftware.TDI.Tests
{
    public class TDI_Tests
    {
        private TinyDi _tinyDi;

        public TDI_Tests()
        {
            _tinyDi = TinyDi.Instance;
        }

        [Fact]
        public void RegisterTests()
        {
            _tinyDi.Register(new TestClassSingleton());
            _tinyDi.Register<TestClassBaseTwo>(() => new TestClassBaseTwo(), TinyDiLifeCycle.Singleton);
            _tinyDi.Register<ITestClassBaseOne, TestClassBaseOne>(TinyDiLifeCycle.Singleton);
            _tinyDi.Register<TestClassSubOne>(TinyDiLifeCycle.Singleton);
            _tinyDi.Build();

            var tcso = _tinyDi.Resolve<TestClassSubOne>();
            tcso.CallSubOne();
        }
    }
}
