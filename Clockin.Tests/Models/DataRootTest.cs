using System;
using System.Linq;
using Clockin.Models;
using FluentAssertions;
using NUnit.Framework;

namespace Clockin.Tests.Models
{
    public class DataRootTest
    {
        private DataRoot _sut;

        [SetUp]
        public void DataRootTestSetup()
        {
            _sut = new DataRoot();
        }

        [Test]
        public void PushShiftTest()
        {
            var date = DateTime.Today;
            var time = TimeSpan.FromHours(1);
            var time2 = TimeSpan.FromHours(2);
            var date2 = DateTime.Today.AddDays(1);
            var time3 = TimeSpan.FromHours(3);

            _sut.PushShift(date, time);
            _sut.PushShift(date, time2);
            _sut.PushShift(date2, time3);

            _sut.Shifts.Should().HaveCount(2);
            _sut.Shifts.First().Key.Should().Be(date);

            _sut.Shifts.First().Value.Should().HaveCount(2);
            _sut.Shifts.First().Value.First().Should().Be(time);

            _sut.Shifts.ToList()[1].Key.Should().Be(date2);
            _sut.Shifts.ToList()[1].Value.Should().HaveCount(1);
            _sut.Shifts.ToList()[1].Value.First().Should().Be(time3);
        }

        [Test]
        public void PopShiftSome()
        {
            var date = DateTime.Today;
            var date2 = DateTime.Today.AddDays(1);
            var time = TimeSpan.FromHours(1);
            var time2 = TimeSpan.FromHours(2);
            var time3 = TimeSpan.FromHours(3);
            
            _sut.PushShift(date, time);
            _sut.PushShift(date, time2);
            _sut.PushShift(date2, time3);

            _sut.PopShift();
            _sut.PopShift();

            _sut.Shifts.Should().HaveCount(1);
            _sut.Shifts.First().Value.Should().HaveCount(1);
        }
        
        [Test]
        public void PopShiftNone()
        {
            var date = DateTime.Today;
            var date2 = DateTime.Today.AddDays(1);
            var time = TimeSpan.FromHours(1);
            var time2 = TimeSpan.FromHours(2);
            var time3 = TimeSpan.FromHours(3);
            
            _sut.PushShift(date, time);
            _sut.PushShift(date, time2);
            _sut.PushShift(date2, time3);

            _sut.PopShift();
            _sut.PopShift();
            _sut.PopShift();
            _sut.PopShift();

            _sut.Shifts.Should().HaveCount(0);
        }
    }
}