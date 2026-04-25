# A MultiSensory Music-Based IoT Platform To Aid Psychological Wellbeing

[![Feature Branch CI](https://github.com/michelleog2351/Music-Multisensory-Platform/actions/workflows/continuous-integration.yml/badge.svg)](https://github.com/michelleog2351/Music-Multisensory-Platform/actions/workflows/continuous-integration.yml)
[![Docs](https://github.com/michelleog2351/Music-Multisensory-Platform/actions/workflows/autodocs.yml/badge.svg)](https://github.com/michelleog2351/Music-Multisensory-Platform/actions/workflows/autodocs.yml)
[![CodeQL](https://github.com/michelleog2351/Music-Multisensory-Platform/actions/workflows/github-code-scanning/codeql/badge.svg)](https://github.com/michelleog2351/Music-Multisensory-Platform/actions/workflows/github-code-scanning/codeql)

----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

# 📌 Overview

This project presents a multisensory IoT-based platform that integrates biometric data from Fitbit devices with music recommendations from Spotify to support emotional regulation and psychological wellbeing.

The system dynamically analyses physiological signals (e.g. heart rate, activity levels) and maps them to mood states, which are then used to generate personalised music recommendations.

# Key Features
- Fitbit biometric data integration (OAuth 2.0)
- Spotify personalised music recommendations
- Mood classification engine
- Recalibration system for baseline mood detection
- Offline fallback using mock data
- Interactive dashboard with data visualisation (D3.js)

# System Architecture
- Frontend: Blazor (ASP.NET Core)
- Backend: Flask API (Raspberry Pi)
- Database: SQLite (EF Core)
- APIs: Fitbit Web API, Spotify Web API
<br>
<img width="616" height="907" alt="image" src="https://github.com/user-attachments/assets/f2e4b81f-dfcd-40dd-8300-2532d476640c" />
<br>

# ⚙️ Setup Instructions
## Prerequisites
- .NET 8+
- Python 3.x
- Raspberry Pi 
- Spotify Developer Account
- Fitbit Developer Account


# Installation Steps
1. Clone the repository
```bash
git clone https://github.com/https://github.com/michelleog2351/Music-Multisensory-Platform.git
```
2. Configure environment variables:
   - Spotify Client ID / Secret
   - Fitbit Client ID / Secret
3. Apply database migrations:
```bash
dotnet ef database update
```
4. Run the application:
```bash
dotnet run
```

# Usage Guide 
(Also see [Limitations](#limitations))
1. Register an account

<img width="761" height="448" alt="image" src="https://github.com/user-attachments/assets/ad7006d0-0d6d-4b12-a176-34608d6c45f6" />
<br>

2. Log in

<img width="761" height="448" alt="image" src="https://github.com/user-attachments/assets/9740d310-0bac-428c-b6cb-fee201f35228" />
<br>

<img width="761" height="448" alt="search_modal_2" src="https://github.com/user-attachments/assets/d9c42310-c1a1-426a-8c20-0ca5e2d842c1" />

<br>

3. Connect Fitbit & Spotify Account 
4. Click Recalibrate
5. View personalised music recommendations
6. Explore dashboard insights

# Testing
1. Unit Testing (xUnit, Moq)
2. Integration Testing (API mocking)
3. Manual Testing (Postman + UI)
4. CI/CD via GitHub Actions

# Limitations
1. Spotify Premium required for playback
2. Fitbit API undergoing migration (Google Health APIs)

- Some features not fully implemented:
  - Recaliration logic & Spotify Auth Token Error (not saving in the SQLite DB)
  - Favourites
  - Sorting/filtering
  - Full accessibility (audio support)

# Future Work
-Fully working recalibration & recommendation system
- Full accessibility (screen reader + audio output)
- Calendar-based historical data view
- Enhanced recommendation algorithms
- Additional wearable & API integrations
- Improved error handling and resilience
  
# Technologies Used
- Blazor / ASP.NET Core
- Flask / Python
- SQLite / EF Core
- D3.js
- Bootstrap
- GitHub Actions

# License
This project uses the GNU License.

# Links
- Figma Prototype
[Figma - (WIP) Prototype](https://www.figma.com/design/ZyjaJOVgRcjiyKquC8M7HV/Music-Multisensory-UI-Interface---Home?node-id=3-2&p=f)

- Miro Board
[MIRO](https://miro.com/app/board/uXjVJQ41Jqk=/)
