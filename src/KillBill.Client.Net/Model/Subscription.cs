﻿using System;
using System.Collections.Generic;
using System.Text;
using KillBill.Client.Net.JSON;
using Newtonsoft.Json;

namespace KillBill.Client.Net.Model
{
    public class Subscription : KillBillObject
    {
        public Guid AccountId { get; set; }

        public Guid BundleId { get; set; }

        public Guid SubscriptionId { get; set; }

        public string ExternalKey { get; set; }

        [JsonConverter(typeof(ShortDateTimeConverter))]
        public DateTime? StartDate { get; set; }

        public string ProductName { get; set; }

        public string ProductCategory { get; set; }

        public string BillingPeriod { get; set; }

        public string PriceList { get; set; }

        public string PlanName { get; set; }

        public EntitlementState State { get; set; }

        [JsonConverter(typeof(ShortDateTimeConverter))]
        public DateTime? CancelledDate { get; set; }

        [JsonConverter(typeof(ShortDateTimeConverter))]
        public DateTime? ChargedThroughDate { get; set; }

        [JsonConverter(typeof(ShortDateTimeConverter))]
        public DateTime? BillingStartDate { get; set; }

        [JsonConverter(typeof(ShortDateTimeConverter))]
        public DateTime? BillingEndDate { get; set; }

        public List<EventSubscription> Events { get; set; }

        public List<NewEventSubscription> NewEvents { get; set; }

        public List<DeletedEventSubscription> DeletedEvents { get; set; }
    }
}