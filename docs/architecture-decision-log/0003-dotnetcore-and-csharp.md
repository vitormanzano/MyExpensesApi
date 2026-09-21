# 3.  Use .NET Core and C# language

Date: 2026-09-21

## Status

Accepted

## Context

As it is monolith, only one language (or platform) must be selected for implementation.

## Decision

I decided to use:

- .NET Core platform - it is new generation multi-platform, fully supported by Microsoft and open-source community, optimized and designed to replace old .NET Framework
- C# language - most popuplar language in .NET ecosystem

## Consequences

- Whole application will be implemented in C# object-oriented language in .NET Core framework
- .NET Core applications can be executed on Windows, MacOS, Linux
