# ⚔️ Turn-Based Combat System

A Unity 2D turn-based combat architecture showcase built with **C#**, focusing on clean project organization, command-driven gameplay flow, MVP-style presentation separation, dependency injection, async workflows, Addressables, and Assembly Definitions.

This repository is intended as a technical portfolio project to demonstrate how a Unity gameplay system can be structured with maintainability and scalability in mind.

---

## 📌 Overview

This project explores a clean approach to building a turn-based combat system in Unity.

The main goal is to separate responsibilities across different layers instead of placing gameplay, UI, input, object creation, and asset loading logic into large MonoBehaviour scripts.

The project is organized around:

* **Core contracts and abstractions**
* **Gameplay commands and combat orchestration**
* **Unit models and combat data**
* **Infrastructure services**
* **Presentation / visual layer**
* **Dependency injection setup**
* **Addressables-based asset loading**
* **Assembly Definition-based code separation**

---

## 🎯 Project Purpose

This project is built as a **code architecture showcase**.

It is designed to demonstrate:

* Clean Unity C# structure
* Separation of gameplay and presentation logic
* Command-based action execution
* Async command flow using UniTask
* Dependency injection with Zenject / Extenject
* Addressables asset loading abstraction
* Input abstraction using Unity Input System
* Modular organization through Assembly Definitions

---

## 🛠️ Tech Stack

| Area                      | Technology           |
| ------------------------- | -------------------- |
| Engine                    | Unity                |
| Language                  | C#                   |
| Async Workflow            | UniTask              |
| Dependency Injection      | Zenject / Extenject  |
| Asset Loading             | Unity Addressables   |
| Input                     | Unity Input System   |
| UI / Presentation Pattern | MVP-style separation |
| Project Organization      | Assembly Definitions |
| Version Control           | Git / GitHub         |

---

## 📁 Repository Structure

```text
Turn-Based-Combat-System
├── Assets
│   ├── Plugins
│   ├── Resources
│   ├── Scenes
│   ├── Scripts
│   │   ├── Core
│   │   ├── Gameplay
│   │   ├── Infrastructure
│   │   └── Presentation
│   ├── Settings
│   └── InputSystem_Actions.inputactions
│
├── Packages
├── ProjectSettings
├── .gitattributes
├── .gitignore
├── .vsconfig
└── README.md
```

---

## 🧱 Script Layer Structure

The code is separated into four main script layers:

```text
Assets/Scripts
├── Core
├── Gameplay
├── Infrastructure
└── Presentation
```

---

## 🧩 Core Layer

The **Core** layer contains shared contracts and low-level abstractions used by other parts of the project.

Current visible scripts include:

```text
Core
├── IAddressableProvider.cs
├── ICommand.cs
├── IUnitMovementView.cs
└── Project.Core.asmdef
```

### Responsibility

The Core layer defines interfaces and contracts such as:

* Command execution contract
* Addressables provider contract
* Unit movement view contract

This helps higher-level systems depend on abstractions instead of concrete implementations.

---

## 🎮 Gameplay Layer

The **Gameplay** layer contains combat-related logic, commands, unit models, and combat orchestration.

Current visible structure:

```text
Gameplay
├── Commands
│   ├── AttackCommand.cs
│   ├── CommandProcessor.cs
│   └── MoveCommand.cs
│
├── Orchestration
│   ├── CombatOrchestrator.cs
│   └── CombatState.cs
│
├── Units
│   ├── UnitFactions.cs
│   └── UnitModel.cs
│
└── Project.Gameplay.asmdef
```

### Responsibility

The Gameplay layer is responsible for the combat-side logic of the project.

It includes:

* Combat commands
* Command processing
* Combat orchestration
* Unit model/data
* Unit faction definition
* Combat state representation

---

## 🔄 Command System

The project uses a command-driven approach for gameplay actions.

The command system is based around an `ICommand` contract and concrete commands such as:

* `AttackCommand`
* `MoveCommand`

A `CommandProcessor` is used to process commands in a controlled flow.

### Why Commands?

Turn-based combat often requires actions to happen in order:

```text
Player chooses action
        ↓
Command is created
        ↓
Command is processed
        ↓
Gameplay result is applied
        ↓
Next combat step continues
```

Using commands helps keep gameplay actions isolated and easier to extend.

---

## ⚔️ Combat Orchestration

The project includes a combat orchestration layer:

```text
Orchestration
├── CombatOrchestrator.cs
└── CombatState.cs
```

### Responsibility

The orchestration layer is responsible for coordinating combat flow.

It acts as the central point where combat progression can be controlled without mixing that responsibility directly into UI views or individual unit scripts.

---

## 🧍 Unit System

The project includes a simple unit model structure:

```text
Units
├── UnitFactions.cs
└── UnitModel.cs
```

### Responsibility

The unit system represents combat units and their runtime data.

It separates unit data/model logic from visual presentation.

This supports the logic-visual separation used throughout the project.

---

## 🏗️ Infrastructure Layer

The **Infrastructure** layer contains services, installers, generated input classes, input reader logic, and Addressables-related implementation.

Current visible structure:

```text
Infrastructure
├── AddressableProvider.cs
├── CombatSceneInstaller.cs
├── GameInputControls.cs
├── GameInputControls.inputactions
├── InputReader.asset
├── InputReader.cs
├── Project.Infrastructure.asmdef
└── ProjectInfrastructureInstaller.cs
```

### Responsibility

The Infrastructure layer handles technical support systems such as:

* Dependency injection setup
* Addressables implementation
* Input abstraction
* Project-level service binding

---

## 📦 Addressables Provider

The project includes an `AddressableProvider` implementation.

### Responsibility

The Addressables provider acts as a wrapper around Unity Addressables.

It provides a centralized place for loading and releasing assets instead of calling Addressables directly from gameplay or presentation scripts.

This improves separation of concerns and makes asset loading easier to manage.

Current responsibilities include:

* Loading assets through an `AssetReference`
* Loading assets by key
* Tracking loaded handles
* Releasing loaded assets

---

## 🎮 Input Reader

The project includes an `InputReader` implemented as a ScriptableObject using Unity Input System callbacks.

Current visible input events include:

* Attack input
* Movement input

### Responsibility

The InputReader abstracts player input and exposes events to the rest of the project.

This avoids spreading direct input checks across gameplay scripts.

---

## 💉 Dependency Injection

The project uses Zenject / Extenject installers.

Current visible installer scripts include:

```text
Infrastructure
├── CombatSceneInstaller.cs
└── ProjectInfrastructureInstaller.cs
```

### Responsibility

Installers are used to bind dependencies such as:

* Addressables provider
* Command processor
* Combat orchestrator
* Unit visual factory
* Health bar factory
* Unit factory

This keeps object creation and dependency wiring centralized.

---

## 🖼️ Presentation Layer

The **Presentation** layer contains view, presenter, and factory classes related to unit visuals and health bar display.

Current visible structure:

```text
Presentation
├── HealthBarFactory.cs
├── HealthBarView.cs
├── Project.Presentation.asmdef
├── UnitFactory.cs
├── UnitPresenter.cs
├── UnitVisual.cs
└── UnitVisualFactory.cs
```

### Responsibility

The Presentation layer handles visual representation and UI-related behavior.

It includes:

* Unit visual creation
* Health bar display
* Unit presenter logic
* View/model UI updates

---

## 🧩 MVP-Style Presentation

The project uses an MVP-style separation for presentation logic.

A visible example is the relationship between:

```text
UnitModel
    ↓
UnitPresenter
    ↓
HealthBarView
```

### Responsibility Split

| Component       | Responsibility                     |
| --------------- | ---------------------------------- |
| `UnitModel`     | Holds unit data/state              |
| `UnitPresenter` | Connects model changes to the view |
| `HealthBarView` | Displays health information        |
| `UnitVisual`    | Represents the unit visually       |
| Factories       | Create presentation objects        |

This keeps UI update behavior separate from the unit model.

---

## 🧱 Assembly Definitions

The project uses Assembly Definitions to separate code into clearer module boundaries.

Current visible Assembly Definition files include:

```text
Project.Core.asmdef
Project.Gameplay.asmdef
Project.Infrastructure.asmdef
Project.Presentation.asmdef
```

### Why This Matters

Assembly Definitions help with:

* Cleaner code boundaries
* Better dependency organization
* Faster compile times in larger Unity projects
* More professional Unity project structure
* Clear separation between Core, Gameplay, Infrastructure, and Presentation layers

---

## 🧠 Architecture Summary

The project follows this general dependency direction:

```text
Core
 ↓
Gameplay
 ↓
Infrastructure / Presentation
```

The intention is to keep shared contracts in Core, gameplay logic in Gameplay, technical services in Infrastructure, and visual/UI-related logic in Presentation.

---

## 🔄 Example Flow

A simplified example of how the system is intended to work:

```text
InputReader receives player input
        ↓
CombatOrchestrator coordinates combat flow
        ↓
Command is created or queued
        ↓
CommandProcessor executes command
        ↓
UnitModel changes
        ↓
UnitPresenter reacts to model changes
        ↓
HealthBarView updates display
```

This keeps each system focused on its own responsibility.

---

## ✅ What This Project Demonstrates

This repository demonstrates practical Unity architecture concepts, including:

* Command Pattern
* MVP-style UI separation
* Dependency Injection with Zenject / Extenject
* UniTask-based async command flow
* Addressables abstraction
* ScriptableObject-based input abstraction
* Factory usage for presentation objects
* Assembly Definition-based module separation
* Clean separation between gameplay, infrastructure, and presentation

---

## 🧭 How To Review This Repository

If you are reviewing this project from a hiring or technical perspective, suggested areas to inspect are:

### 1. Core Contracts

Check:

```text
Assets/Scripts/Core
```

Look at how interfaces define contracts for commands, Addressables, and unit movement views.

### 2. Command Flow

Check:

```text
Assets/Scripts/Gameplay/Commands
```

Review how combat actions are represented as commands and processed through the command processor.

### 3. Combat Orchestration

Check:

```text
Assets/Scripts/Gameplay/Orchestration
```

Review how combat state and orchestration are separated from individual commands and views.

### 4. Unit Model

Check:

```text
Assets/Scripts/Gameplay/Units
```

Review how unit data is represented separately from visuals.

### 5. Infrastructure

Check:

```text
Assets/Scripts/Infrastructure
```

Review Addressables loading, input abstraction, and Zenject installers.

### 6. Presentation

Check:

```text
Assets/Scripts/Presentation
```

Review how views, presenters, visuals, and factories are separated.

---

## 📚 Documentation Goal

This README is written to make the project easier to review without needing to open every script first.

The documentation focuses on:

* What the project is
* How the code is organized
* Why the structure exists
* Where to inspect important systems
* What architectural ideas the project demonstrates

---

## 👤 Author

Created by **Burhan Uddin**
Senior Unity Gameplay Programmer

* GitHub: https://github.com/whoisburhan
* Portfolio: https://fantasyrealms.itch.io/
* LinkedIn: https://linkedin.com/in/whoisburhan

---

## 📄 License / Usage

This repository is intended as a portfolio and technical showcase project.

Please do not redistribute the project as your own work.
