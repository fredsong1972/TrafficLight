// src/components/TrafficLightsContainer.tsx
import React, { useEffect, useState } from 'react';
import TrafficLight, { TrafficColor } from './TrafficLight';
import { Grid } from '@mui/material';

interface TrafficLightData {
  direction: string;
  currentStateColor: 'Green' | 'Yellow' | 'Red';
}

const TrafficLights: React.FC = () => {
  const [trafficLights, setTrafficLights] = useState<TrafficLightData[]>([]);

  useEffect(() => {
    const fetchTrafficLights = async () => {
      try {
        const response = await fetch('trafficlights');
        const data = await response.json();
        setTrafficLights(data);
      } catch (error) {
        console.error('Failed to fetch traffic lights', error);
      }
    };

    fetchTrafficLights();
    const interval = setInterval(fetchTrafficLights, 5000); // Poll every 5 seconds

    return () => clearInterval(interval); // Cleanup on unmount
  }, []);

  return (
    <Grid container spacing={2} justifyContent="center">
      {trafficLights.map((light, index) => (
        <Grid item key={index}>
          <TrafficLight color={ light.currentStateColor.toLowerCase() as TrafficColor } direction={light.direction} />
        </Grid>
      ))}
    </Grid>
  );
};

export default TrafficLights;
