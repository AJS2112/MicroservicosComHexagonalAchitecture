using Domain.Entities;
using Domain.Enums;
using Action = Domain.Enums.Action;

namespace DomainTests.Bookings
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ShouldAlwaysStartWithCreatedStatus()
        {
            var booking = new Booking();
            Assert.That(booking.CurrentStatus, Is.EqualTo(Status.Created));
        }

        [Test]
        public void ShouldSetStatusToPaidWhenPayingForABookingWithCreatedStatus()
        {
            var booking = new Booking();
            booking.ChangeAction(Action.Pay);
            Assert.That(booking.CurrentStatus, Is.EqualTo(Status.Paid));
        }

        [Test]
        public void ShouldSetStatusToCanceledWhenCancelingABookingWithCreatedStatus()
        {
            var booking = new Booking();
            booking.ChangeAction(Action.Cancel);
            Assert.That(booking.CurrentStatus, Is.EqualTo(Status.Canceled));
        }

        [Test]
        public void ShouldSetStatusToFinishedWhenFinishForABookingWithPaidStatus()
        {
            var booking = new Booking();
            booking.ChangeAction(Action.Pay);
            booking.ChangeAction(Action.Finish);
            Assert.That(booking.CurrentStatus, Is.EqualTo(Status.Finished));
        }

        [Test]
        public void ShouldSetStatusToRefoundedWhenRefoundForABookingWithPaidStatus()
        {
            var booking = new Booking();
            booking.ChangeAction(Action.Pay);
            booking.ChangeAction(Action.Refound);
            Assert.That(booking.CurrentStatus, Is.EqualTo(Status.Refounded));
        }

        [Test]
        public void ShouldSetStatusToCreatedWhenReopenForABookingWithCanceledStatus()
        {
            var booking = new Booking();
            booking.ChangeAction(Action.Cancel);
            booking.ChangeAction(Action.Reopen);
            Assert.That(booking.CurrentStatus, Is.EqualTo(Status.Created));
        }

        [Test]
        public void ShouldNotChangeStatusWhenRefoundingABookingWithCreatedStatus()
        {
            var booking = new Booking();
            booking.ChangeAction(Action.Refound);
            Assert.That(booking.CurrentStatus, Is. EqualTo(Status.Created));
        }
    }
}