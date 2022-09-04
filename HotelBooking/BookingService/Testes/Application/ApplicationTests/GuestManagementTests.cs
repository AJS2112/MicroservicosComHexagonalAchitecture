using Application;
using Application.Guest;
using Application.Guest.DTO;
using Application.Guest.Requests;
using Domain.Entities;
using Domain.Ports;
using Moq;

namespace ApplicationTests
{
    public class Tests
    {
        GuestManager guestManager;

        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public async Task HappyPath()
        {
            int expectedId = 2222;
            var fakeRepo = Mock.Of<IGuestRepository>(x =>
            x.Create(It.IsAny<Guest>()) == Task.FromResult(expectedId)
            );
            guestManager = new GuestManager(fakeRepo);


            var guestDTO = new GuestDTO
            {
                Name = "Joe",
                Surname = "Doe",
                Email = "abc@gmail.com",
                IdNumber = "3333",
                IdTypeCode = 1
            };

            var request = new CreateGuestRequest()
            {
                Data = guestDTO,
            };

            var res = await guestManager.CreateGuest(request);
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(res);
                Assert.True(res.Success);
                Assert.That(res.Data.Id, Is.EqualTo(expectedId));
            });
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("a")]
        [TestCase("ab")]
        [TestCase("abc")]
        public async Task Should_Return_InvalidPersonDocumentIdException_WhenDocsAreInvalid(string docNumber)
        {
            var fakeRepo = Mock.Of<IGuestRepository>(x =>
            x.Create(It.IsAny<Guest>()) == Task.FromResult(222)
            );
            guestManager = new GuestManager(fakeRepo);

            var guestDTO = new GuestDTO
            {
                Name = "Joe",
                Surname = "Doe",
                Email = "abc@gmail.com",
                IdNumber = docNumber,
                IdTypeCode = 1
            };

            var request = new CreateGuestRequest()
            {
                Data = guestDTO,
            };

            var res = await guestManager.CreateGuest(request);
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(res);
                Assert.False(res.Success);
                Assert.That(res.ErrorCode, Is.EqualTo(ErrorCodes.INVALID_PERSON_ID));
            });
        }

        [Test]
        [TestCase("", "", "")]
        [TestCase("", "", "email@test.com")]
        [TestCase("", "surnameTest", "email@test.com")]
        [TestCase("nameTest", "", "")]
        [TestCase("nameTest", "surnameTest", "")]
        [TestCase("nameTest", "", "email@test.com")]
        public async Task Should_Return_MissingRequeredInformationException_WhenDocsAreInvalid(string name, string surname, string email)
        {
            var fakeRepo = Mock.Of<IGuestRepository>(x =>
            x.Create(It.IsAny<Guest>()) == Task.FromResult(222)
            );
            guestManager = new GuestManager(fakeRepo);

            var guestDTO = new GuestDTO
            {
                Name = name,
                Surname = surname,
                Email = email,
                IdNumber = "1111",
                IdTypeCode = 1
            };

            var request = new CreateGuestRequest()
            {
                Data = guestDTO,
            };

            var res = await guestManager.CreateGuest(request);
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(res);
                Assert.False(res.Success);
                Assert.That(res.ErrorCode, Is.EqualTo(ErrorCodes.MISSING_REQUIRED_INFORMATION));
            });
        }
    }
}