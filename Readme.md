# Car Price Prediction System

This project is a full-stack web application that predicts the price of a car based on its characteristics using a machine learning model. The backend is implemented in c# using .NET API, the IA is coded in python using Flask for serving the functionality to the user and the frontend is built with Nuxt.js. There's also a Flutter mobile app for user authentication and interaction.

## Frontend (Nuxt.js)

### Requirements

- Nuxt.js
- Vue.js
- Axios
- Pinia
- Element Plus

#### Instalation

1. Navigate to the FrontEnd\NuxtProject directory:

```bash
cd FrontEnd\NuxtProject
```

2. Install the dependencies using npm or yarn

```bash
npm install
```

3. Start the Nuxt.js development server

```bash
npm run dev
```

The development server will start, and you can access the frontend at http://localhost:3000.

## Backend (.NET API)

Backend has already been published, the process for launching it is:

### Requirements

- log4net
- JwtBearer
- SqlServer
- SixLabors.ImageSharp
- Element Plus

#### Instalation

1. Install the required dependencies:

```bash
pip install Flask Flask-CORS scikit-learn pandas
```

2. Run FinalAPI.exe manually or with the following command

```bash
dotnet ./FinalAPI.dll
```

## IA (Python - Flask)

### Requirements

- Python 3.x
- Flask
- Flask-CORS
- scikit-learn
- pandas

#### Instalation

1. Install the required dependencies:

```bash
pip install Flask Flask-CORS scikit-learn pandas
```

2. Run predict_price.py file with the following command:

```bash
python predict_price.py
```

## Mobile APP (Flutter)

### Requirements

- Flutter

1. Navigate to the Flutter proyect directory

1. With the emulator running, run the following command:

```bash
flutter run
```

##

## License

MIT License

Copyright (c) 2024 David Garcia Montenegro

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
