using System;
using System.Collections.Generic;
using Xunit;
using System.Linq;
using Common.Enums;
using Common.Dto;
using Business.Tools;

namespace Tests.Services
{
    class TestRecommendationRoute
    {
        public double directionTimeInHours;
        public double previousPauseTimeHours;
    }

    class TestRecommendationInputs
    {
        public DateTimeOffset StartTrip;
        public DateTimeOffset EndTrip;
        public IEnumerable<TestRecommendationRoute> Routes;
    }

    public class TripServiceTest
    {
        private TripRecommendationResponseDto _getRecommendation(TestRecommendationInputs inputs)
        {
            var routesForReq = new List<TripRecommendationRouteRequestDto>();
            var req = new TripRecommendationRequestDto();

            var routes = inputs.Routes.ToList();
            routes.ForEach(route =>
            {
                var r = new TripRecommendationRouteRequestDto();
                r.directionTimeSeconds = route.directionTimeInHours * 60 * 60;
                r.previousPauseTimeSeconds = route.previousPauseTimeHours * 60 * 60;
                routesForReq.Add(r);
            });
            req.Routes = routesForReq;
            var tool = new TripRecommendationTool(req);
            return tool.getRecommendation();
        }


        [Fact]
        public void TripRecommendation_1()
        {
            var inputs = new TestRecommendationInputs
            {
                Routes = new List<TestRecommendationRoute>() {
                    new TestRecommendationRoute { 
                        directionTimeInHours = 1,
                        previousPauseTimeHours = 0
                    }
                }
            };

        
            var recommendation = _getRecommendation(inputs);
            var routes = recommendation.routes.ToList();


            Assert.Equal(TripRecommendationType.ONE_DRIVER, recommendation.type);

            Assert.Equal(1, routes[0].DJ_InHours);
            Assert.Equal(3.5, routes[0].M_InHours);

            return;
        }

        [Fact]
        public void TripRecommendation_2()
        {
            var inputs = new TestRecommendationInputs
            {
                Routes = new List<TestRecommendationRoute>() {
                    new TestRecommendationRoute {
                        directionTimeInHours = 1,
                        previousPauseTimeHours = 0
                    },
                    new TestRecommendationRoute {
                        directionTimeInHours = 1,
                        previousPauseTimeHours = 0
                    },
                    new TestRecommendationRoute {
                        directionTimeInHours = 1,
                        previousPauseTimeHours = 0
                    },
                    new TestRecommendationRoute {
                        directionTimeInHours = 1,
                        previousPauseTimeHours = 0
                    }
                }
            };


            var recommendation = _getRecommendation(inputs);
            var routes = recommendation.routes.ToList();

            Assert.Equal(TripRecommendationType.ONE_DRIVER, recommendation.type);

            Assert.Equal(1, routes[0].DJ_InHours);
            Assert.Equal(3.5, routes[0].M_InHours);
            Assert.Equal(1, routes[0].Real_Time_InHours);

            Assert.Equal(2, routes[1].DJ_InHours);
            Assert.Equal(2.5, routes[1].M_InHours);
            Assert.Equal(1, routes[1].Real_Time_InHours);

            Assert.Equal(3, routes[2].DJ_InHours);
            Assert.Equal(1.5, routes[2].M_InHours);
            Assert.Equal(1, routes[2].Real_Time_InHours);

            Assert.Equal(4, routes[3].DJ_InHours);
            Assert.Equal(0.5, routes[3].M_InHours);
            Assert.Equal(1, routes[3].Real_Time_InHours);

            return;
        }

        [Fact]
        public void TripRecommendation_3()
        {
            var inputs = new TestRecommendationInputs
            {
                Routes = new List<TestRecommendationRoute>() {
                    new TestRecommendationRoute {
                        directionTimeInHours = 3.5,
                        previousPauseTimeHours = 0
                    },
                    new TestRecommendationRoute {
                        directionTimeInHours = 2,
                        previousPauseTimeHours = 0.25
                    },
                    new TestRecommendationRoute {
                        directionTimeInHours = 3,
                        previousPauseTimeHours = 0.75
                    }
                }
            };


            var recommendation = _getRecommendation(inputs);
            var routes = recommendation.routes.ToList();

            Assert.Equal(TripRecommendationType.ONE_DRIVER, recommendation.type);

            Assert.Equal(3.5, routes[0].DJ_InHours);
            Assert.Equal(1, routes[0].M_InHours);
            Assert.Equal(3.5, routes[0].Real_Time_InHours);

            Assert.Equal(6, routes[1].DJ_InHours);
            Assert.Equal(3.5, routes[1].M_InHours);
            Assert.Equal(2.5, routes[1].Real_Time_InHours);

            Assert.Equal(9, routes[2].DJ_InHours);
            Assert.Equal(1.5, routes[2].M_InHours);
            Assert.Equal(3, routes[2].Real_Time_InHours);


            return;
        }

        [Fact]
        public void TripRecommendation_4()
        {
            var inputs = new TestRecommendationInputs
            {
                Routes = new List<TestRecommendationRoute>() {
                    new TestRecommendationRoute {
                        directionTimeInHours = 1,
                        previousPauseTimeHours = 0
                    },
                    new TestRecommendationRoute {
                        directionTimeInHours = 3.5,
                        previousPauseTimeHours = 0.25
                    },
                    new TestRecommendationRoute {
                        directionTimeInHours = 1,
                        previousPauseTimeHours = 0.25
                    }
                }
            };


            var recommendation = _getRecommendation(inputs);
            var routes = recommendation.routes.ToList();

            Assert.Equal(TripRecommendationType.ONE_DRIVER, recommendation.type);

            Assert.Equal(1, routes[0].DJ_InHours);
            Assert.Equal(3.5, routes[0].M_InHours);
            Assert.Equal(1, routes[0].Real_Time_InHours);

            Assert.Equal(5, routes[1].DJ_InHours);
            Assert.Equal(4.5, routes[1].M_InHours);
            Assert.Equal(4, routes[1].Real_Time_InHours);

            Assert.Equal(6, routes[2].DJ_InHours);
            Assert.Equal(3.5, routes[2].M_InHours);
            Assert.Equal(1, routes[2].Real_Time_InHours);


            return;
        }

        [Fact]
        public void TripRecommendation_Reduce_Time()
        {
            var inputs = new TestRecommendationInputs
            {
                Routes = new List<TestRecommendationRoute>() {
                    new TestRecommendationRoute {
                        directionTimeInHours = 5,
                        previousPauseTimeHours = 0
                    },
                    new TestRecommendationRoute {
                        directionTimeInHours = 9.8,
                        previousPauseTimeHours = 15
                    }
                }
            };


            var recommendation = _getRecommendation(inputs);
            var routes = recommendation.routes.ToList();

            Assert.Equal(TripRecommendationType.ONE_DRIVER, recommendation.type);

            return;
        }
    }
}
