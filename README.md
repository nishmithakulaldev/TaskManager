# TaskManager CLI

A command-line task management application built with C# and .NET. Add, complete, delete, filter, and sort tasks — all persisted to a local JSON file.

Built as a learning project to practice OOP fundamentals, SOLID principles, and design patterns in C#.

## Features

- **CRUD Operations** — Add, complete, delete, and list tasks
- **Filtering** — Filter tasks by status (`pending`/`done`) and priority (`low`/`medium`/`high`)
- **Sorting** — Sort by priority, status, category, title, ID, or creation date
- **Summary Dashboard** — View task counts by status and category breakdown
- **Persistent Storage** — Tasks saved to a local JSON file and survive between sessions
- **Colored Output** — Visual distinction between pending (yellow) and completed (green) tasks
- **Input Validation** — Friendly error messages for invalid inputs instead of crashes

## Usage

```bash
# Build the project
dotnet build

# Add tasks
dotnet run add "Buy groceries" --priority high --category personal
dotnet run add "Fix login bug" --priority medium --category work
dotnet run add "Read chapter 5" --category study

# List all tasks
dotnet run list

# Filter tasks
dotnet run list --status pending
dotnet run list --priority high
dotnet run list --status pending --priority high

# Sort tasks
dotnet run list --sort priority
dotnet run list --sort category

# Complete a task
dotnet run complete 1

# Delete a task
dotnet run delete 2

# View summary
dotnet run summary

# Show help
dotnet run help
```

### Sample Output

```
ID   Title               Priority  Category    Status    Created At
---------------------------------------------------------------------
1    Buy groceries       High      personal    Pending   2026-05-25
2    Fix login bug       Medium    work        Done      2026-05-24
3    Read chapter 5      Medium    study       Pending   2026-05-23
```

```
Total: 3 | Pending: 2 | Done: 1
By Category: personal (1) work (1) study (1)
```

## Architecture

```
Program.cs                          → Entry point. Wires dependencies, routes to commands.
│
├── Commands/                       → Command Pattern. Each command parses its own input.
│   ├── BaseCommand.cs              → Abstract base with shared flag-parsing logic.
│   ├── AddCommand.cs               → Handles: add "title" --priority --category
│   ├── CompleteCommand.cs          → Handles: complete <id>
│   ├── DeleteCommand.cs            → Handles: delete <id>
│   ├── ListCommand.cs              → Handles: list --status --priority --sort
│   ├── SummaryCommand.cs           → Handles: summary
│   └── HelpCommand.cs              → Handles: help
│
├── Services/TaskService.cs         → Business logic: validation, ID generation, filtering.
│
├── Interfaces/                     → Contracts that decouple layers.
│   ├── ICommand.cs
│   ├── ITaskService.cs
│   └── ITaskRepository.cs
│
├── Repositories/
│   └── JsonTaskRepository.cs       → Reads/writes tasks to a local JSON file.
│
├── Models/
│   ├── TaskItem.cs                 → Core data model with encapsulated properties.
│   ├── TaskSummary.cs              → Structured summary (replaces raw dictionary).
│   └── Enums/
│       ├── Priority.cs             → Low, Medium, High
│       └── Status.cs               → Pending, Done
│
├── Exceptions/
│   ├── TaskNotFoundException.cs    → Thrown when a task ID doesn't exist.
│   └── InvalidTaskDataException.cs → Thrown when input fails validation.
│
└── Helpers/
    └── ConsoleHelper.cs            → Table formatting and colored output.
```

## Design Decisions

**Why interfaces for Repository and Service?**
`TaskService` depends on `ITaskRepository`, not `JsonTaskRepository`. This means I could swap in a database-backed repository without changing a single line in the service layer. This is the Dependency Inversion Principle in practice — high-level modules don't depend on low-level modules, both depend on abstractions.

**Why the Command Pattern?**
Initially, `Program.cs` had a large switch statement handling every command. Extracting each command into its own class means adding a new command (like `edit`) requires creating one new file — zero changes to existing code. This follows the Open/Closed Principle.

**Why custom exceptions?**
Using `TaskNotFoundException` and `InvalidTaskDataException` instead of generic `ArgumentException` lets the caller decide how to handle each error type differently. The exception carries context (like the missing task ID) that a generic exception wouldn't.

**Why `init` on CreatedAt?**
The creation date should be set once and never changed. `init` allows `System.Text.Json` to set it during deserialization while preventing modification afterward — a balance between immutability and serialization needs.

## What I Learned

- **OOP in practice** — Classes, interfaces, inheritance, encapsulation, and abstract base classes used to solve real problems, not textbook exercises.
- **SOLID principles** — Single Responsibility (each class has one job), Open/Closed (Command Pattern), Dependency Inversion (interfaces between layers).
- **Design patterns** — Repository Pattern for data access, Command Pattern for extensible CLI commands.
- **C# features** — Enums, LINQ (Where, GroupBy, OrderBy), switch expressions, nullable reference types, JSON serialization with System.Text.Json.
- **Defensive programming** — Input validation at two layers (commands validate shape, service validates business rules), custom exceptions for specific error scenarios.

## Tech Stack

- **Language:** C# 13
- **Framework:** .NET 9
- **Storage:** JSON file (System.Text.Json)
- **No external dependencies** — built entirely with the .NET standard library

## Getting Started

### Prerequisites
- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) or later

### Installation
```bash
git clone https://github.com/nishmithakulaldev/TaskManager.git
cd TaskManager
dotnet build
dotnet run help
```
