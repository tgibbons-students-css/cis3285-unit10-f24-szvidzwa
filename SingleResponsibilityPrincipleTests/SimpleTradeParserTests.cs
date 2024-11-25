using SingleResponsibilityPrinciple.Contracts;
using SingleResponsibilityPrinciple;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleResponsibilityPrincipleTests
{
    [TestClass()]
    public class SimpleTradeParserTests
    {

        [TestMethod()]
        public void TestNumberOfPosLines()
        {
            //Arrange
            var validator = new dummyPositiveTradeValidator();
            var mapper = new dummyTradeMapper();
            var parser = new SimpleTradeParser(validator, mapper);
            List<string> sampleInput = new List<string> { "XXXYYY,1111,9.99", "XXXYYY,2222,9.99", "XXXYYY,3333,9.99" };

            //Act
            IEnumerable<TradeRecord> result = parser.Parse(sampleInput);
            //Assert
            Assert.AreEqual(result.Count(), sampleInput.Count());
        }

        [TestMethod()]
        public void TestNumberOfNegLines()
        {
            //Arrange
            var validator = new dummyNegativeTradeValidator();
            var mapper = new dummyTradeMapper();
            var parser = new SimpleTradeParser(validator, mapper);
            List<string> sampleInput = new List<string> { "XXXYYY,1111,9.99", "XXXYYY,2222,9.99", "XXXYYY,3333,9.99" };

            //Act
            IEnumerable<TradeRecord> result = parser.Parse(sampleInput);
            //Assert
            Assert.AreEqual(0, result.Count());
        }
    }


    public class dummyPositiveTradeValidator : ITradeValidator
    {
        public bool Validate(string[] tradeData)
        {
            return true;
        }
    }

    public class dummyNegativeTradeValidator : ITradeValidator
    {
        public bool Validate(string[] tradeData)
        {
            return false;
        }
    }

    public class dummyTradeMapper : ITradeMapper
    {
        public TradeRecord Map(string[] fields)
        {
            TradeRecord sampleRec = new TradeRecord();
            sampleRec.DestinationCurrency = "XXX";
            sampleRec.SourceCurrency = "YYY";
            sampleRec.Price = 1.11M;
            sampleRec.Lots = 2.22F;
            return sampleRec;
        }
    }
}
