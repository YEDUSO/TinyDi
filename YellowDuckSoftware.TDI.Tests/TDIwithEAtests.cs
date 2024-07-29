//
// This is to be used with the YellowDuckSoftware.EA package.
// https://github.com/YellowDuckSoftware/YellowDuckSoftware.EA
//
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using YellowDuckSoftware.EA;

//namespace YellowDuckSoftware.TDI.Tests
//{
//    public class TestMessage
//    {
//        public object Ob { get; set; }
//    }

//    [TestClass]
//    public class TDIwithEAtests :
//        IHandle<TestMessage>
//    {
//        [TestInitialize]
//        public void TestInitialize()
//        {
//            TinyDi.Instance.Register<IEventAggregator, EventAggregator>(TinyDiLifeCycle.Singleton);
//        }

//        [TestMethod]
//        public void ShouldSendMessage()
//        {
//            var evAg = TinyDi.Instance.Resolve<IEventAggregator>();
//            evAg.Subscribe(this);
//            evAg.Publish(new TestMessage() { Ob = "SUCCESS" });
//        }

//        public void Handle(TestMessage message)
//        {
//            var str = message.Ob as string;
//            Assert.AreEqual("SUCCESS", str);
//        }
//    }
//}
