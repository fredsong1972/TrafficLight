# Traffic Light System

This project simulates a traffic light system using .NET 8 and React with TypeScript. It features a backend service that manages the traffic light logic and a frontend that displays the current state of the traffic lights.

## Features

- **Automatic Light Cycling**: The traffic lights change automatically based on predefined rules, simulating real-world traffic light behavior.
- **Peak and Off-Peak Hours**: Adjusts the duration of green lights during peak and off-peak hours.
- **Northbound Right-Turn Signal**: A special feature that allows a green right-turn signal for northbound traffic, temporarily stopping southbound traffic.

## Technologies

- **TrafficLightAPI(Backend)**: .NET 8
- **traffic-light(Frontend)**: React with TypeScript
- **UI Library**: Material-UI (MUI)

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites

What things you need to install the software and how to install them:

```bash
dotnet --version     # Verify .NET installation
npm --version        # Verify npm is installed
```

### Clone the repository
```bash
git clone https://github.com/fredsong1972/TrafficLight.git
cd TrafficLigh
```

### Set up the backend

Navigate to the backend directory and run the application:

```bash
cd TrafficLightAPI
dotnet restore
dotnet run
```

### Set up the frontend

Navigate to the frontend directory, install dependencies, and start the server:

```bash
cd traffic-light
npm install
npm start
```

Access the application at https://localhost:5173


