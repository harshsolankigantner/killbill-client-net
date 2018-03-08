﻿using System;
using System.Linq;
using KillBill.Client.Net.Model;
using NUnit.Framework;

namespace KillBill.Client.Net.Tests.ModificationTests
{
    [TestFixture]
    public class AccountModificationTests : BaseTestFixture
    {
        private Account _account;

        [SetUp]
        public void SetUp()
        {
            _account = new Account
            {
                ExternalKey = Guid.NewGuid().ToString(),
                Name = "Test",
                Email = "test@test.com",
                Currency = "EUR",
                Address1 = "Test Address 1",
                Address2 = "Test Address 2"
            };
        }

        [Test]
        public void When_UpdatingAccount_Then_TheResultReturnsTheChangedFields()
        {
            // arrange
            const string newAddress1 = "NEW ADDRESS 1";
            const string newAddress2 = "NEW ADDRESS 2";

            var updateAccount = new Account
            {
                AccountId = AccountId,
                Address1 = newAddress1,
                Address2 = newAddress2
            };

            // act
            var updatedAccount = Client.UpdateAccount(updateAccount, RequestOptions);

            // assert
            Assert.That(updateAccount, Is.Not.Null);
            Assert.That(updatedAccount.Address1, Is.EqualTo(newAddress1));
            Assert.That(updatedAccount.Address2, Is.EqualTo(newAddress2));
        }

        [Test]
        public void When_AddingEmailToAccount_Then_TheEmailIsIncludedInTheAccount()
        {
            // arrange
            var email = new AccountEmail { AccountId = AccountId, Email = "tester1@test.com" };

            // act
            Client.AddEmailToAccount(email, RequestOptions);

            // assert
            var result = Client.GetEmailsForAccount(AccountId, RequestOptions);
            Assert.That(result.Any(e => e.Email == "tester1@test.com"), Is.True);
        }

        [Test]
        public void When_RemovingEmailToAccount_Then_TheEmailIsRemovedFromTheAccount()
        {
            // arrange
            var email = new AccountEmail { AccountId = AccountId, Email = "tester2@test.com" };
            Client.AddEmailToAccount(email, RequestOptions);
            var emails = Client.GetEmailsForAccount(AccountId, RequestOptions);
            Assume.That(emails.Any(e => e.Email == "tester2@test.com"), Is.True);

            // act
            Client.RemoveEmailFromAccount(email, RequestOptions);

            // assert
            var result = Client.GetEmailsForAccount(AccountId, RequestOptions);
            Assert.That(result.Any(e => e.Email == "tester2@test.com"), Is.False);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void When_UpdatingEmailNotificationsForAccount_Then_TheSettingsAreReturnedCorrectly(bool notificationSetting)
        {
            // arrange
            var invoiceEmail = new InvoiceEmail { AccountId = AccountId, IsNotifiedForInvoices = notificationSetting };
            Client.UpdateEmailNotificationsForAccount(invoiceEmail, RequestOptions);

            // act
            var setting = Client.GetEmailNotificationsForAccount(AccountId, RequestOptions);

            // assert
            Assert.That(setting, Is.Not.Null);
            Assert.That(setting.AccountId, Is.EqualTo(AccountId));
            Assert.That(setting.IsNotifiedForInvoices, Is.EqualTo(notificationSetting));
        }
    }
}