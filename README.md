# 📊 Signal Visualizer — Vizualizator de Semnale pentru Sisteme de Ordinul I


## 🇷🇴 Despre proiect

**Signal Visualizer** este o aplicație desktop dezvoltată în **C# și Windows Forms**, concepută pentru vizualizarea răspunsului unui **sistem dinamic de ordinul I**.

Aplicația utilizează funcția de transfer:

\[
H(s)=\frac{K}{Ts+1}
\]

Utilizatorul poate modifica parametrii sistemului și poate analiza grafic comportamentul acestuia pentru trei tipuri de semnale de intrare:

- 🔹 impuls;
- 🔹 treaptă;
- 🔹 sinusoidal.

Graficul este generat dinamic folosind `System.Drawing`, iar valorile introduse de utilizator sunt validate înainte de efectuarea calculelor.

---

## ✨ Caracteristici

- ⚙️ Configurarea parametrilor **K** și **T**;
- 📐 Afișarea automată a funcției de transfer;
- 📈 Vizualizarea răspunsului sistemului în timp real;
- 💥 Răspuns la semnal **impuls**;
- 📊 Răspuns la semnal **treaptă**;
- 〰️ Răspuns la semnal **sinusoidal**;
- 🧮 Calcul matematic al răspunsului sistemului;
- ✅ Validarea parametrilor introduși;
- 🔄 Actualizarea automată a graficului;
- 📏 Axe și grilă pentru interpretarea graficului;
- 🖥️ Interfață Windows Forms simplă și intuitivă;
- ⚡ Panou de desenare cu `DoubleBuffered` pentru reducerea efectului de flickering;
- 📐 Redimensionarea automată a zonei de grafic.

---

## 🧠 Fundamente matematice

Sistemul analizat este un sistem liniar de ordinul I cu funcția de transfer:

\[
H(s)=\frac{K}{Ts+1}
\]

unde:

| Parametru | Descriere |
|-----------|-----------|
| `K` | Câștigul sistemului |
| `T` | Constanta de timp |
| `s` | Variabila complexă Laplace |

### 💥 Răspuns la impuls

Pentru un semnal impuls:

\[
y(t)=\frac{K}{T}e^{-t/T}
\]

Răspunsul are o evoluție exponențială și depinde direct de raportul dintre câștigul `K` și constanta de timp `T`.

### 📊 Răspuns la treaptă

Pentru un semnal treaptă:

\[
y(t)=K(1-e^{-t/T})
\]

Valoarea răspunsului tinde către `K` pe măsură ce timpul crește.

În aplicație, intervalul de afișare este ales astfel încât să permită observarea evoluției sistemului până la aproximativ `8T`, cu o durată minimă de 10 secunde.

### 〰️ Răspuns la sinusoidă

Pentru semnalul sinusoidal este utilizată frecvența unghiulară:

\[
\omega=1\ rad/s
\]

Răspunsul calculat include atât componenta staționară, cât și componenta tranzitorie:

\[
y(t)=
\frac{K}{1+(\omega T)^2}
\left[
\sin(\omega t)
-\omega T\cos(\omega t)
+\omega T e^{-t/T}
\right]
\]

Astfel, graficul evidențiază tranziția sistemului de la comportamentul inițial către regimul staționar.

---

## 🖥️ Interfața aplicației

Interfața este organizată în trei zone principale:

### 1. Parametrii sistemului

Utilizatorul poate introduce:

```text
K = câștigul sistemului
T = constanta de timp
```

### 2. Funcția de transfer

Aplicația afișează automat funcția:

```text
K / (Ts + 1)
```

folosind valorile curente ale parametrilor.

### 3. Zona de vizualizare

Răspunsul sistemului este reprezentat grafic împreună cu:

- axele de coordonate;
- grila;
- eticheta timpului `t`;
- eticheta ieșirii `y(t)`;
- tipul răspunsului selectat;
- valorile curente `K` și `T`.

---

## 🚀 Getting Started

### Cerințe

Pentru compilarea și rularea proiectului sunt necesare:

- **Windows**;
- **.NET 6 SDK**;
- **Visual Studio 2022** sau un IDE compatibil;
- suport pentru **Windows Forms**.

### Rularea proiectului

Deschide:

```text
SignalVisualizer.sln
```

în Visual Studio.

Apoi:

1. Selectează configurația `Debug` sau `Release`;
2. Compilează proiectul;
3. Rulează aplicația folosind `F5` sau butonul **Start**.

---

## 🎮 Utilizare

La pornire, aplicația utilizează valorile implicite:

```text
K = 1
T = 1
```

Funcția de transfer rezultată este:

```text
1 / (1s + 1)
```

### Modificarea parametrilor

Introdu valorile dorite în câmpurile:

```text
K
T
```

și apasă:

```text
Aplică parametrii
```

### Selectarea semnalului

Din meniul superior pot fi selectate:

```text
Impuls
Treaptă
Sinusoidă
```

Graficul este redesenat automat după selectarea semnalului.

---

## ✅ Validarea datelor

Aplicația verifică parametrii înainte de efectuarea calculelor.

### Parametrul K

`K` trebuie să fie o valoare numerică validă și poate avea orice semn.

Exemple valide:

```text
1
2.5
-3
0
```

### Parametrul T

`T` trebuie să fie strict pozitiv.

Exemple valide:

```text
0.5
1
2.5
10
```

Valori precum:

```text
0
-1
abc
```

sunt respinse de aplicație.

---

## 🏗️ Structura proiectului

```text
SignalVisualizer/
│
├── README.md
│
├── SignalVisualizer/
│   ├── SignalVisualizer.sln
│   ├── SignalVisualizer.csproj
│   ├── Program.cs
│   ├── MainForm.cs
│   ├── MainForm.Designer.cs
│   └── MainForm.resx
│
├── simulink/
│   └── SignalVisualizer.slx
│
└── docs/
    ├── rezultate.docx
    └── rezultate.pdf
```

---

## 🔧 Arhitectura aplicației

Aplicația este construită în jurul formularului principal `MainForm`.

Fluxul principal este:

```text
             ┌──────────────────┐
             │   User Input      │
             │     K și T        │
             └────────┬─────────┘
                      │
                      ▼
             ┌──────────────────┐
             │ Input Validation │
             └────────┬─────────┘
                      │
                      ▼
             ┌──────────────────┐
             │ Transfer Function│
             │  H(s)=K/(Ts+1)   │
             └────────┬─────────┘
                      │
                      ▼
             ┌──────────────────┐
             │ Signal Selection │
             │ Impulse / Step / │
             │      Sine        │
             └────────┬─────────┘
                      │
                      ▼
             ┌──────────────────┐
             │ Response         │
             │ Calculation      │
             └────────┬─────────┘
                      │
                      ▼
             ┌──────────────────┐
             │ Graph Rendering  │
             │   System.Drawing │
             └──────────────────┘
```

---

## 🛠️ Tehnologii și concepte utilizate

### Programming

- C#
- Object-Oriented Programming
- Enumerations
- Methods and event handlers
- Input validation
- Exception-safe parsing

### .NET

- .NET 6
- Windows Forms
- `System.Windows.Forms`
- `System.Drawing`
- `System.Globalization`

### Graphical rendering

Graficul este desenat manual folosind API-ul `System.Drawing`.

Sunt utilizate:

- `Graphics`;
- `Pen`;
- `Brush`;
- `Font`;
- `PointF`;
- `Rectangle`.

Pentru reducerea flickering-ului în timpul redesenării este utilizat un panou personalizat:

```csharp
internal sealed class SignalPlotPanel : Panel
{
    public SignalPlotPanel()
    {
        DoubleBuffered = true;
        ResizeRedraw = true;
    }
}
```

---

## 📚 Scop educațional

Proiectul poate fi utilizat pentru înțelegerea practică a unor concepte din:

- teoria sistemelor;
- automatică;
- funcții de transfer;
- transformata Laplace;
- sisteme de ordinul I;
- răspunsuri tranzitorii;
- răspunsuri în regim staționar;
- reprezentarea grafică a semnalelor.

De asemenea, proiectul oferă un exemplu simplu de integrare între **calcule matematice** și o **interfață grafică desktop**.

---

# 🇬🇧 English

## About

**Signal Visualizer** is a desktop application built with **C# and Windows Forms** for analyzing and visualizing the response of a **first-order dynamic system**.

The application uses the transfer function:

\[
H(s)=\frac{K}{Ts+1}
\]

where:

- `K` represents the system gain;
- `T` represents the time constant;
- `T > 0`.

Users can modify the system parameters and visualize its response to three different input signals:

- **Impulse**
- **Step**
- **Sine**

The graph is rendered dynamically using `System.Drawing`.

---

## ✨ Features

- Adjustable gain `K`;
- Adjustable time constant `T`;
- Automatic transfer function display;
- Input validation;
- Impulse response visualization;
- Step response visualization;
- Sinusoidal response visualization;
- Dynamic graph updates;
- Coordinate axes and grid;
- Resizable graph area;
- Double-buffered custom drawing panel;
- Simple Windows Forms interface.

---

## 🧮 Mathematical Model

The application models a first-order system:

\[
H(s)=\frac{K}{Ts+1}
\]

### Impulse Response

\[
y(t)=\frac{K}{T}e^{-t/T}
\]

### Step Response

\[
y(t)=K(1-e^{-t/T})
\]

### Sinusoidal Response

The sinusoidal response uses:

\[
\omega=1\ rad/s
\]

and includes both transient and steady-state components:

\[
y(t)=
\frac{K}{1+(\omega T)^2}
\left[
\sin(\omega t)
-\omega T\cos(\omega t)
+\omega T e^{-t/T}
\right]
\]

---

## 🖥️ User Interface

The application consists of three main areas:

**System parameters**

```text
K = system gain
T = time constant
```

**Transfer function**

```text
K / (Ts + 1)
```

**Graph**

The graph displays:

- system response;
- coordinate axes;
- grid;
- time axis;
- output axis;
- selected signal type;
- current `K` and `T` values.

---

## 🚀 Getting Started

### Requirements

- Windows;
- .NET 6 SDK;
- Visual Studio 2022 or a compatible IDE;
- Windows Forms support.

### Run

Open:

```text
SignalVisualizer.sln
```

in Visual Studio.

Build the solution and run it with:

```text
F5
```

or the Visual Studio **Start** button.

---

## 🎮 Usage

The application starts with:

```text
K = 1
T = 1
```

The corresponding transfer function is:

```text
1 / (1s + 1)
```

To change the system:

1. Enter a value for `K`;
2. Enter a positive value for `T`;
3. Click **Aplică parametrii**;
4. Select **Impuls**, **Treaptă**, or **Sinusoidă**;
5. The graph is automatically refreshed.

---

## 🏗️ Project Structure

```text
SignalVisualizer/
│
├── README.md
│
├── SignalVisualizer/
│   ├── SignalVisualizer.sln
│   ├── SignalVisualizer.csproj
│   ├── Program.cs
│   ├── MainForm.cs
│   ├── MainForm.Designer.cs
│   └── MainForm.resx
│
├── simulink/
│   └── SignalVisualizer.slx
│
└── docs/
    ├── rezultate.docx
    └── rezultate.pdf
```

### Main components

| File | Purpose |
|------|---------|
| `MainForm.cs` | Application logic and response calculations |
| `MainForm.Designer.cs` | Windows Forms UI definition |
| `MainForm.resx` | Form resources |
| `Program.cs` | Application entry point |
| `SignalVisualizer.csproj` | .NET project configuration |
| `SignalVisualizer.sln` | Visual Studio solution |

---

## 🔧 Technologies

| Technology | Usage |
|------------|-------|
| **C#** | Application development |
| **.NET 6** | Runtime and framework |
| **Windows Forms** | Desktop user interface |
| **System.Drawing** | Graph rendering |
| **Visual Studio** | Development environment |

---

## 📈 Signal Visualization Workflow

```text
User Parameters
      │
      ▼
Input Validation
      │
      ▼
Transfer Function
 H(s) = K/(Ts+1)
      │
      ▼
Signal Selection
 ┌────┼────┐
 ▼    ▼    ▼
Impulse Step Sine
 └────┼────┘
      ▼
Response Calculation
      │
      ▼
Graph Rendering
```

---

## 🎯 Educational Purpose

This project provides a practical example of combining mathematical system modeling with desktop application development.

It can be used to study:

- first-order systems;
- transfer functions;
- Laplace-domain representations;
- time constants;
- transient response;
- steady-state response;
- standard input signals;
- graphical signal visualization;
- C# Windows Forms development.

---

## 👤 Autor / Author

**IonutD**
