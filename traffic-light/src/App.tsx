// src/App.tsx
import './App.css';
import TrafficLights from './components/TrafficLights';
import { CssBaseline, Container, Typography } from '@mui/material';

function App() {
  return (
    <div className="App">
      <CssBaseline />
      <Container maxWidth="sm">
        <header className="App-header">
          <Typography variant="h4" component="h1" gutterBottom>
            Traffic Lights System
          </Typography>
          <TrafficLights />
        </header>
      </Container>
    </div>
  );
}

export default App;
