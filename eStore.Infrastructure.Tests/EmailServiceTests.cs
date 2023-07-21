using System;
using System.Linq;
using System.Threading.Tasks;
using eStore.Application.Interfaces;
using eStore.Domain.Entities;
using eStore.Infrastructure.Services.Email;
using eStore.Tests.Common;
using Moq;
using NUnit.Framework;

namespace eStore.Infrastructure.Tests;

[TestFixture]
public class EmailServiceTests
{
    [SetUp]
    public void Setup()
    {
        _helper = new UnitTestHelper();
        _mockHtmlEmailSender = new Mock<IHtmlEmailSender>();
    }

    private UnitTestHelper _helper;
    private Mock<IHtmlEmailSender> _mockHtmlEmailSender;

    [Test]
    public async Task SendRegisterEmailAsync_ExistingCustomer_SendsEmailToCustomerEmail()
    {
        // Arrange
        _mockHtmlEmailSender.Setup(
            x => x.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));
        IEmailService service = new EmailService(_mockHtmlEmailSender.Object);
        Customer customer = _helper.Customers.First();

        // Act
        await service.SendRegisterEmailAsync(customer);

        // Assert
        _mockHtmlEmailSender.Verify(
            x => x.SendEmailAsync("email1@mail.com", It.IsAny<string>(), It.IsAny<string>()));
    }

    [Test]
    public void SendRegisterEmailAsync_NullCustomer_ThrowsArgumentNullException()
    {
        // Arrange
        IEmailService service = new EmailService(_mockHtmlEmailSender.Object);

        // Act
        var exception =
            Assert.ThrowsAsync<ArgumentNullException>(async () => await service.SendRegisterEmailAsync(null));

        // Assert
        Assert.That(exception, Is.Not.Null, "The method didn't throw ArgumentNullException.");
    }

    [Test]
    public async Task SendDeactivationEmailAsync_ExistingEmail_SendsEmail()
    {
        // Arrange
        _mockHtmlEmailSender.Setup(
            x => x.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));
        IEmailService service = new EmailService(_mockHtmlEmailSender.Object);

        // Act
        await service.SendDeactivationEmailAsync("email1@mail.com");

        // Assert
        _mockHtmlEmailSender.Verify(
            x => x.SendEmailAsync("email1@mail.com", It.IsAny<string>(), It.IsAny<string>()));
    }

    [Test]
    public async Task SendChangePasswordEmailAsync_ExistingEmail_SendsEmail()
    {
        // Arrange
        _mockHtmlEmailSender.Setup(
            x => x.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));
        IEmailService service = new EmailService(_mockHtmlEmailSender.Object);

        // Act
        await service.SendChangePasswordEmailAsync("email1@mail.com");

        // Assert
        _mockHtmlEmailSender.Verify(
            x => x.SendEmailAsync("email1@mail.com", It.IsAny<string>(), It.IsAny<string>()));
    }

    [Test]
    public async Task SendPurchaseEmailAsync_ExistingOrder_SendsEmail()
    {
        _mockHtmlEmailSender.Setup(
            x => x.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));
        IEmailService service = new EmailService(_mockHtmlEmailSender.Object);
        Order order = _helper.Orders.First();
        order.Customer = _helper.Customers.First(c => c.Id == order.CustomerId);

        // Act
        await service.SendPurchaseEmailAsyncAsync(order, "path");

        // Assert
        _mockHtmlEmailSender.Verify(x =>
            x.SendEmailAsync("email1@mail.com", It.IsAny<string>(), It.IsAny<string>(), "path"));
    }

    [Test]
    public void SendPurchaseEmailAsync_NullOrder_ThrowsArgumentNullException()
    {
        // Arrange
        IEmailService service = new EmailService(_mockHtmlEmailSender.Object);

        // Act
        var exception =
            Assert.ThrowsAsync<ArgumentNullException>(async () =>
                await service.SendPurchaseEmailAsyncAsync(null, "test"));

        // Assert
        Assert.That(exception, Is.Not.Null, "The method didn't throw ArgumentNullException.");
    }
}