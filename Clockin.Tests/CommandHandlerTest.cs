using Clockin.Models;
using Clockin.Options;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace Clockin.Tests
{
    public class CommandHandlerTest
    {
        private CommandHandler _sut;
        private IStorage _storage;

        [SetUp]
        public void CommandHandlerTestSetup()
        {
            _storage = Substitute.For<IStorage>();
            _sut = new CommandHandler(_storage);
        }

        [Test]
        public void ShowListTest()
        {
            var dataRoot = new DataRoot();
            _storage.Get().Returns(dataRoot);
                
            var result = _sut.ShowList();
            
            _storage.Received(1).Get();

            result.Should().Be(0);
        }

        [Test]
        public void MaxLinesTest()
        {
            var dataRoot = new DataRoot();
            _storage.Get().Returns(dataRoot);

            var result = _sut.MaxLines(new PreferenceOptions(){MaxLines = 30});
            
            _storage.Received(1).Save(Arg.Is<DataRoot>(x => x.MaxLines == 30));

            result.Should().Be(0);
        }

        [Test]
        public void MaxLinesTest2()
        {
            var dataRoot = new DataRoot();
            _storage.Get().Returns(dataRoot);

            var result = _sut.MaxLines(new PreferenceOptions(){MaxLines = 0});
            
            result.Should().Be(1);
        }
    }
}