#  TuneFinder – Song Recommendation App

##  Overview
**TuneFinder** is a **C# WinForms desktop application** that delivers personalized song recommendations using the **Spotify Web API**.  
It allows users to search for tracks, discover similar songs, explore playlists, and manage favorites — all while keeping user sessions secure with **JWT authentication**.  

<img width="375" height="434" alt="image" src="https://github.com/user-attachments/assets/febf17ad-30bb-4c6e-a0ee-994b842ed82f" />


##  Features
- **Secure User Authentication** – JWT-based login for secure session handling.
- **Search Songs & Artists** – Find tracks by title, artist, or album name.
- **Personalized Recommendations** – Discover new music based on selected songs.
- **Track Details** – View album art, release date, and related metadata.
- **Playlist Discovery** – Browse curated Spotify playlists.
- **Audio Preview** – Listen to 30-second track previews *(Spotify Premium only)*.
- **Favorites Management** – Save favorite songs locally for quick access.
- **Modern UI** – Clean and responsive WinForms interface.


## ⚠️ Known Limitation
Due to **Spotify API restrictions**, **audio preview playback** is only available for **Spotify Premium** subscribers.  
All other features work perfectly for free-tier users.

---

##  Tech Stack
- **Language:** C# (.NET Framework / .NET 6+)
- **UI Framework:** Windows Forms (WinForms)
- **API:** Spotify Web API
- **Authentication:** JWT (JSON Web Token) + OAuth 2.0 (Spotify Developer)
- **Data Storage:** Local storage / JSON

##  Installation & Setup
1. **Clone the repository**
   ```bash
   git clone https://github.com/TaskeenZehra/TuneFinder.git
Open the project in Visual Studio.

#### Install required NuGet packages:

SpotifyAPI-NET

Newtonsoft.Json

System.IdentityModel.Tokens.Jwt

#### Configure API keys:

Create a Spotify Developer account: https://developer.spotify.com

Create an app to get Client ID and Client Secret.

Add them to app.config or your config file.

Build and run the project.

#### 🔑 Spotify API Setup
Go to Spotify Developer Dashboard.

Create a new application.

Set a redirect URI (e.g., http://localhost:5000/callback).

Copy your Client ID and Client Secret into the project configuration.

#### 📄 License
This project is licensed under the MIT License
