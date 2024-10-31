# CarSeer
CarSeer is a powerful application designed to provide users with comprehensive vehicle insights. This document outlines how to run the application both without Docker and using Docker locally.

## Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (if running without Docker)
- [Docker](https://www.docker.com/get-started) (if using Docker)

## Running the Application Locally (Without Docker)

1. **Clone the Repository**

   ```bash
   git clone https://github.com/yourusername/CarSeer.git
   ```

2. **Navigate to the Project Directory**

   ```bash
   cd CarSeer/CarSeer
   ```

3. **Run the Application**

   ```bash
   dotnet run
   ```

4. **Access the Application**

   Open your browser and go to [http://localhost:5000/](http://localhost:5250/).

---

## Running the Application Using Docker

1. **Clone the Repository**

   ```bash
   git clone https://github.com/yourusername/CarSeer.git
   ```

2. **Navigate to the Project Directory**

   ```bash
   cd CarSeer
   ```

3. **Build the Docker Image**

   ```bash
   docker build --rm -t cloud-car:latest .
   ```

4. **Run the Docker Container**

   ```bash
   docker run --rm -p 5000:5000 -p 5001:5001 -e ASPNETCORE_HTTP_PORT=https://+:5001 -e ASPNETCORE_URLS=http://+:5000 cloud-car
   ```

5. **Access the Application**

   Open your browser and go to [http://localhost:5000/](http://localhost:5000/).
