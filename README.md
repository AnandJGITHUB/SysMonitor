# SysMonitor

A lightweight C# application to monitor system CPU, RAM, and disk usage, expose the data through a REST API, and log it to the console in real-time.

---

## Description
SysMonitor collects real-time system performance metrics, including CPU usage, RAM usage, and disk usage.
It exposes this data through a REST API and logs it to the console at configurable intervals.
Additionally, it logs the data to a local file, with the file path specified in appsettings.json.
This makes it useful for monitoring system performance or integrating with other tools for analytics.

---

## Features

- Monitor CPU, RAM, and Disk usage in real-time
- REST API endpoints to submit and fetch system data
- Console logging of every received data point
- Easy integration with other tools and scripts

##Assumptions
- There is only one drive on system for example C or D.
- Implementation to get systems done only for Windows OS. Sample is given for linux also with empty method 

---

## Getting Started

### Prerequisites

- .NET 8 SDK or higher
- Visual Studio 2022 / VS Code
- Optional: Postman for testing API endpoints

### Installation

1. Clone the repository:
```bash
git clone https://github.com/yourusername/SysMonitor.git


2. Navigate to the project folder:

cd SysMonitor


Open the solution in Visual Studio or VS Code.

3. Restore dependencies:

dotnet restore


4. Run the project:

dotnet run
