using System;
using System.Collections.Generic;
using System.Linq;
using Common.Dto;
using Common.Enums;

namespace Business.Tools
{
    public class TripRecommendationTool
    {
        private readonly TripRecommendationRequestDto _req;
        public TripRecommendationTool(TripRecommendationRequestDto req)
        {
            _req = req;
        }

        public TripRecommendationResponseDto getRecommendation()
        {
            DateTimeOffset startTrip = new DateTimeOffset(DateTime.UtcNow);
            DateTimeOffset endTrip = new DateTimeOffset(startTrip.DateTime);
            double M = 4.5;
            double DJ = 0;
            double P = 0;
            double P2 = 0;
            double realTime = 0;
            double sumRealTime = 0;
            bool PVP = false;
            bool P2P = false;
            double sumH = 0;
            double sumP = 0;
            bool HIsLong = false;
            var routes = _req.Routes.ToList();
            var lastItem = routes.Last();
            var response = new TripRecommendationResponseDto();
            var responseRoutes = new List<TripRecommendationRouteResponseDto>();


            for (int i = 0; i < routes.Count; i++)
            {
                var route = routes[i];
                P = route.previousPauseTimeSeconds / 60 / 60;
                sumP += P;
                if (P > 9)
                {
                    startTrip = startTrip.AddHours(sumH + sumP);
                    endTrip = new DateTimeOffset(startTrip.DateTime);
                    sumH = 0;
                    sumP = 0;
                    sumRealTime = 0;
                }                
                sumH += route.directionTimeSeconds / 60 / 60;
                realTime = route.directionTimeSeconds / 60 / 60;                
                if ((route.directionTimeSeconds / 60 / 60) > 10)
                {
                    HIsLong = true;
                }
                DJ += route.directionTimeSeconds / 60 / 60;                                
                if ((i + 1) >= 0 && ((i + 1) < routes.Count))
                {
                    P2 = routes[i + 1].previousPauseTimeSeconds / 60 / 60;
                }
                else
                {
                    P2 = 0;
                }
                if (P2P)
                {
                    P2P = false;
                    P = 0;
                }
                compute1(route);
                sumRealTime += realTime;
            }

            void setValues()
            {
                var responseRoute = new TripRecommendationRouteResponseDto();
                responseRoute.DJ_InHours = DJ;
                responseRoute.M_InHours = M;
                responseRoute.Real_Time_InHours = realTime;
                responseRoutes.Add(responseRoute);
            }

            bool computePause()
            {
                if (PVP && P >= 0.5)
                {
                    PVP = false;
                    P = 0.75;
                }
                else if (!PVP && P < 0.75)
                {
                    PVP = true;
                    P = 0.25;
                }
                return P >= 0.75;
            }

            void compute1(TripRecommendationRouteRequestDto route)
            {
                M -= route.directionTimeSeconds / 60 / 60;
                if (P >= 0.25)
                {
                    if (computePause())
                    {
                        M = 4.5;
                        PVP = false;
                        P = 0;
                        compute1(route);
                    }
                    else
                    {
                        cycle(route);
                    }
                }
                else
                {
                    P = 0;
                    cycle(route);
                }
            }

            void cycle(TripRecommendationRouteRequestDto route)
            {
                while (M < 0 || (M == 0 && (lastItem != route)))
                {
                    DJ += (0.75 - P);
                    realTime += (0.75 - P);
                    M = 4.5 - Math.Abs(M);
                    P = 0;
                    PVP = false;

                    //if (P == 0.25 && P2 >= 0.5)
                    //{
                    //    P2 = P2;
                    //}
                    //else
                    //{
                    //    DJ -= P2;
                    //    P2 = 0;
                    //    P2P = true;
                    //}
                    //if ((P + P2) >= 0.75)
                    //{
                    //    M = 4.5;
                    //    P = 0;
                    //    PVP = false;
                    //    P2P = true;
                    //    DJ += (0.75 - P);
                    //    realTime += (0.75 - P);
                    //    break;
                    //}
                    //else
                    //{
                    //    DJ += (0.75 - P);
                    //    realTime += (0.75 - P);
                    //    M = 4.5 - Math.Abs(M);
                    //    P = 0;
                    //    PVP = false;
                    //}
                }
                setValues();
            }
            response.type = TripRecommendationType.ONE_DRIVER;
            endTrip = endTrip.AddHours(sumRealTime + sumP);

            var timespan = endTrip - startTrip;
            if (timespan > TimeSpan.FromHours(15))
            {
                response.type = TripRecommendationType.TWO_DRIVERS;
                response.reduce_time_hours = (timespan - TimeSpan.FromHours(15)).TotalHours;
            }
            if (sumH >= 10)
            {
                response.reduce_routes_hours = sumH - 10;
                response.type = TripRecommendationType.TWO_DRIVERS;
            }
            if (HIsLong)
            {
                response.type = TripRecommendationType.TWO_DRIVERS;
            }          
            response.routes = responseRoutes;
            return response;
        }
    }
}
