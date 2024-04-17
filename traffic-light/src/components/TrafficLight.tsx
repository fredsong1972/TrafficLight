// src/components/TrafficLight.tsx
import React from 'react';
import { Paper } from '@mui/material';

export type TrafficColor = 'red' | 'green' | 'yellow';

interface TrafficLightProps {
  color: TrafficColor;
  direction: string;
}

const TrafficLight: React.FC<TrafficLightProps> = ({ color, direction }) => {
  const style = {
    backgroundColor: color,
    height: '100px',
    width: '100px',
    borderRadius: '50%',
    margin: '5px',
    display: 'flex',
    alignItems: 'center',
    justifyContent: 'center',
    color: 'white',
    fontSize: '20px',
    fontWeight: 'bold'
  };

  return (
    <div style={{ textAlign: 'center', margin: '10px' }}>
      <Paper elevation={4} style={style}>{direction}</Paper>
    </div>
  );
};

export default TrafficLight;
