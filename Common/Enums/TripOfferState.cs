using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Enums
{
    public enum TripOfferState
    {
        NO_TRANSPORTER = 1,
        SET_TRANSPORTER_25 = 2,
        SET_TRANSPORTER_100 = 3,
        PAYD_25 = 4,
        PAYD_100 = 5,
        CLOSED = 6
    }

    public enum TripOfferAcceptMethod
    {
        PAY_25 = 1,
        PAY_100 = 2
    }

    public enum TripOfferChange
    {
        PAYD_25 = 1,
        PAYD_100 = 2,
        CLOSE = 3,
        UNPAID_25 = 4,
        UNPAID_75 = 5,
        UNPAID_100 = 6
    }

    public enum ScheduleType
    {
        TRIP_END = 1,
        WARNING_PAY = 2,
        NO_OFFER = 3,
        WARNING_REST_PAY = 4,
        PAY_COMMISSION_AFTER_TRIP = 5
    }
}
