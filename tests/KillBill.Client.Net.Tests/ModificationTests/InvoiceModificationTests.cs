﻿using System;
using System.Collections.Generic;
using KillBill.Client.Net.Model;
using NUnit.Framework;

namespace KillBill.Client.Net.Tests.ModificationTests
{
    [TestFixture]
    public class InvoiceModificationTests : BaseTestFixture
    {
        [Test]
        public void When_CreatingExternalCharges_TheyAreCreatedCorrectly()
        {
            // arrange
            var externalCharges = new List<InvoiceItem>
            {
                new InvoiceItem
                {
                    AccountId = AccountId,
                    Amount = 100,
                    Currency = "EUR",
                    Description = "LINE ITEM 1"
                },
                new InvoiceItem
                {
                    AccountId = AccountId,
                    Amount = 200,
                    Currency = "EUR",
                    Description = "LINE ITEM 2"
                },
            };

            // act
            var invoiceItems = Client.CreateExternalCharges(externalCharges, DateTime.Now, false, false, RequestOptions);

            // assert
            Assert.That(invoiceItems, Is.Not.Null);
            Assert.That(invoiceItems, Is.Not.Empty);
            Assert.That(invoiceItems.Count, Is.EqualTo(2));
        }
    }
}