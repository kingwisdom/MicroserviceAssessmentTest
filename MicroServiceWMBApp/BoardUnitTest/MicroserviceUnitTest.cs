using MicroServiceAppWMB.Board;
using System;
using Xunit;

namespace BoardUnitTest
{
    public class MicroserviceUnitTest
    {
        [Fact]
        public void Test_InvalidEndpoint_OnBoardCustomer()
        {
            var boardProcessor = new BoardProcessor();

            Assert.Throws<ArgumentOutOfRangeException>(() => boardProcessor.OnBoardCustomer(new boardDTO()));
        }

        [Fact]
        public void Test_InvalidEndpoint_PhoneNumber()
        {
            var boardProcessor = new BoardProcessor();

            Assert.Throws<ArgumentOutOfRangeException>(() => boardProcessor.OnBoardCustomer(new boardDTO { Email = "kenny@gmail.com", Password = "test", PhoneNumber = ""}));
        }

        [Fact]
        public void Test_InvalidEndpoint_Email()
        {
            var boardProcessor = new BoardProcessor();

            Assert.Throws<ArgumentOutOfRangeException>(() => boardProcessor.OnBoardCustomer(new boardDTO { Email = "", Password = "test", PhoneNumber = "09056131918" }));
        }

        [Fact]
        public void Test_InvalidEndpoint_Password()
        {
            var boardProcessor = new BoardProcessor();

            Assert.Throws<ArgumentOutOfRangeException>(() => boardProcessor.OnBoardCustomer(new boardDTO { Email = "kenny@gmail.com", Password = "", PhoneNumber = "07056131918" }));
        }

        [Fact]
        public void Test_validEndpoint_OnBoardCustomer()
        {
            var boardProcessor = new BoardProcessor();

            Assert.True(boardProcessor.OnBoardCustomer(new boardDTO { Email = "kenny@gmail.com", Password = "test", PhoneNumber = "07056131918" }));
        }
    }
}
