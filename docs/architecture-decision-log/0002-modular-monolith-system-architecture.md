# 2.  Use Modular Monolith System Architecture

Date: 2026-09-21

## Status

Accepted

## Context

Learn the modular way to apply DDD with .Net

## Decision

Design a modular monolith architecture and DDD tactical patterns 

## Consequences

- All modules must run in one single process as single application (Monolith)
- All modules should have maximum autonomy (Modular)
- DDD Bounded Contexts will be used to divide monolith into modules
- DDD tactical patterns will be used to implement most of modules
