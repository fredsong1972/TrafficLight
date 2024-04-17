using TrafficLightAPI.Models;

namespace TrafficLightAPI.Services
{
    public class TrafficLightService
    {
        private List<TrafficLight> _lights;

        public TrafficLightService()
        {
            _lights = new List<TrafficLight>
        {
            new TrafficLight { Direction = "North", GreenDuration = 20 },
            new TrafficLight { Direction = "South", GreenDuration = 20 },
            new TrafficLight { Direction = "East", GreenDuration = 20 },
            new TrafficLight { Direction = "West", GreenDuration = 20 }
        };

            foreach (var light in _lights)
            {
                light.CurrentState = LightState.Green;  // Initially, all lights are green.
            }
        }


        public List<TrafficLight> RetrieveLights() => _lights;

        public void UpdateLights()
        {
            lock (_lights)
            {
                DateTime currentTime = DateTime.Now;
                bool isPeakHours = IsPeakHours(currentTime);

                foreach (var light in _lights)
                {
                    TimeSpan elapsed = currentTime - light.LastTransitionTime;
                    switch (light.CurrentState)
                    {
                        case LightState.Green:
                            if (light.Direction == "North" && ShouldActivateRightTurn(elapsed.TotalSeconds, light.GreenDuration))
                            {
                                light.IsRightTurnActive = true;
                                light.LastTransitionTime = currentTime;
                            }
                            else if (ShouldSwitchFromGreen(elapsed.TotalSeconds, isPeakHours, light.Direction))
                            {
                                light.CurrentState = LightState.Yellow;
                                light.LastTransitionTime = currentTime;
                                light.IsRightTurnActive = false;
                            }
                            break;
                        case LightState.Yellow:
                            if (elapsed.TotalSeconds >= light.YellowDuration)
                            {
                                light.CurrentState = LightState.Red;
                                light.LastTransitionTime = currentTime;
                            }
                            break;
                        case LightState.Red:
                            if (!light.IsRightTurnActive && ShouldSwitchFromRed(elapsed.TotalSeconds, light.Direction))
                            {
                                light.CurrentState = LightState.Green;
                                light.LastTransitionTime = currentTime;
                                AdjustGreenDuration(light, isPeakHours);
                            }
                            break;
                    }
                    if (light.Direction == "South" && NorthRightTurnActive())
                    {
                        light.CurrentState = LightState.Red;
                        light.LastTransitionTime = currentTime;
                    }
                }
            }

        }

        #region Private Methods

        private bool IsPeakHours(DateTime time)
        {
            return (time.Hour >= 8 && time.Hour < 10) || (time.Hour >= 17 && time.Hour < 19);
        }

        private bool ShouldSwitchFromGreen(double elapsedSeconds, bool isPeakHours, string direction)
        {
            int requiredSeconds = direction == "North" || direction == "South" ?
                                  isPeakHours ? 40 : 20 :
                                  isPeakHours ? 10 : 20;
            return elapsedSeconds >= requiredSeconds;
        }

        private bool ShouldSwitchFromRed(double elapsedSeconds, string direction)
        {
            // Red lights stay on until the cross-traffic is red for at least 4 seconds
            return elapsedSeconds >= 4;
        }

        private void AdjustGreenDuration(TrafficLight light, bool isPeakHours)
        {
            if (light.Direction == "North" || light.Direction == "South")
            {
                light.GreenDuration = isPeakHours ? 40 : 20;
            }
            else
            {
                light.GreenDuration = isPeakHours ? 10 : 20;
            }
        }

        private bool ShouldActivateRightTurn(double elapsedSeconds, int greenDuration)
        {
            // Activate right-turn signal for the last 10 seconds of the green phase
            return elapsedSeconds >= (greenDuration - 10);
        }

        private bool NorthRightTurnActive()
        {
            return _lights.Any(a => a.Direction == "North" && a.IsRightTurnActive);
        }

        #endregion

    }


}
