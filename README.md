We'll define:

The problem statement
User roles (Administrator, Water Officer, Farmer)
Functional requirements
Non-functional requirements
Business rules
Database design
REST API endpoints
Azure architecture
CI/CD pipeline
Future IoT integration

Software Requirements Specification (SRS)
Irrigation Management System (IMS)

Version: 1.0
Author: Makondelela Mutshinya
Technology Stack: ASP.NET Core, PostgreSQL, Docker, Azure, Azure DevOps

## Local database setup

The API uses PostgreSQL through Entity Framework Core. Start PostgreSQL, apply the
idempotent seed script, and then run the API from the solution directory:

```bash
docker compose up -d postgres
docker exec -i irrigation_postgres psql -U postgres -d irrigation_management < database/seed.sql
dotnet run --project IrrigationManagementSystem.API/IrrigationManagementSystem.API.csproj
```

The local connection string is configured in `IrrigationManagementSystem.API/appsettings.json`.
The seed data is maintained in `database/seed.sql`; the API no longer contains hardcoded
schemes, villages, sub-canals, or farmers.

1. Introduction
1.1 Purpose

The purpose of the Irrigation Management System (IMS) is to digitize the management of irrigation schemes where multiple villages share a common irrigation canal.

The system aims to ensure equitable water distribution, efficient scheduling, transparency, and accountability while reducing conflicts among farmers and improving water resource management.

1.2 Problem Statement

Many irrigation schemes rely on manually maintained schedules and paper records.

Common problems include:

Farmers using water longer than allocated
Double-booking of irrigation slots
Poor visibility of water usage
Downstream villages receiving insufficient water
No audit trail
Difficult reporting
Poor communication between farmers and water officers

The proposed system provides a centralized platform for scheduling, monitoring, and managing irrigation allocations.

2. Project Objectives

The system shall:

Digitize irrigation scheduling.
Ensure fair water allocation.
Prevent booking conflicts.
Maintain historical records.
Improve transparency.
Generate reports.
Support future IoT integration.
Be deployable on Microsoft Azure.
3. Scope

Version 1 includes:

User management
Village management
Canal management
Sub-canal management
Farmer management
Farm management
Water request management
Water allocation
Scheduling
Reporting
Notifications
Authentication

Version 2 may include:

ESP32 gate monitoring
Flow sensors
Water level sensors
Weather forecasting
AI-based scheduling
Mobile application
4. Stakeholders
Stakeholder	Responsibility
Farmers	Request irrigation slots
Water Officers	Approve and monitor irrigation
Village Administrators	Manage farmers within villages
Irrigation Scheme Administrator	Configure and maintain the system
Government / Water Authority	Reporting and oversight
5. User Roles
Administrator

Can:

Manage users
Create irrigation schemes
Create villages
Create canals
Configure system settings
View all reports
Water Officer

Can:

Approve requests
Reject requests
Modify schedules
View allocations
Monitor water usage
Generate reports
Farmer

Can:

Login
View farms
Submit irrigation requests
View schedules
Cancel pending requests
View irrigation history
6. System Overview
River
   │
Main Canal
   │
─────────────────────────────────────
│          │          │
Village A  Village B  Village C
│          │          │
Sub Canals Sub Canals Sub Canals
│          │          │
Farms       Farms      Farms
│
Farmers
7. Functional Requirements
FR-001 User Authentication

The system shall:

Register users.
Authenticate users.
Support role-based authorization.
Reset passwords.
FR-002 Irrigation Scheme Management

Administrator shall:

Create irrigation schemes.
Edit irrigation schemes.
Archive irrigation schemes.
FR-003 Village Management

Administrator shall:

Add villages.
Edit villages.
Assign villages to a main canal.
Define village order along the canal.
FR-004 Sub-Canal Management

Administrator shall:

Create sub-canals.
Assign sub-canals to villages.
Configure maximum supported farms.
FR-005 Farmer Management

Administrator shall:

Register farmers.
Assign farmers to villages.
Assign farms.
FR-006 Farm Management

Each farm shall contain:

Name
Size
Crop type
GPS coordinates (future)
Assigned sub-canal
FR-007 Water Request

A farmer shall:

Select farm.
Select irrigation date.
Select preferred start time.
Select preferred duration.
Submit request.
FR-008 Water Scheduling

The system shall:

Detect booking conflicts.
Prevent overlapping bookings.
Check village allocation.
Check sub-canal availability.
Check farmer limits.
Suggest available time slots.
FR-009 Water Allocation

Water officers shall:

Approve requests.
Reject requests.
Modify allocations.
Record reasons.
FR-010 Notifications

Notify farmers when:

Request approved
Request rejected
Irrigation starts soon
Allocation expires
FR-011 Reporting

Generate:

Daily schedule
Village usage
Farmer usage
Monthly consumption
Water allocation reports
Conflict reports
8. Business Rules

This is the most important section.

BR-001

A farmer cannot have overlapping irrigation schedules.

BR-002

A sub-canal cannot supply two irrigation sessions simultaneously.

BR-003

A village has a maximum daily water allocation.

BR-004

A farmer has a maximum irrigation duration per day.

BR-005

A downstream village must not lose its reserved allocation due to excessive upstream usage.

BR-006

Only approved requests may become active.

BR-007

Every approval or rejection must be recorded with the responsible user and timestamp.

BR-008

Historical irrigation records cannot be deleted.

BR-009

Only administrators may modify irrigation scheme structures.

9. Non-Functional Requirements
Performance
Average API response time < 2 seconds.
Support 1,000 concurrent users.
Security
JWT authentication.
HTTPS.
Password hashing.
Role-based authorization.
Reliability
Automatic backups.
Error logging.
Audit trails.
Scalability
Docker containers.
Azure deployment.
Horizontal scaling support.
Availability
99% uptime target.
10. Initial Database Entities
IrrigationScheme
MainCanal
Village
SubCanal
Farmer
Farm
WaterRequest
WaterAllocation
User
Role
AuditLog
11. Future Enhancements
IoT gate controllers
ESP32 flow sensors
Automated gate control
AI water demand prediction
Weather API integration
Mobile application
SMS notifications
GIS map integration
Satellite imagery support
12. Success Criteria

The project will be considered successful if it:

Eliminates scheduling conflicts.
Ensures fair water distribution across villages.
Reduces manual administration.
Provides complete auditability.
Supports cloud deployment and CI/CD.
Can be extended to support IoT and AI.
One Important Addition: Water Allocation Algorithm

This is what will make your project unique.

Instead of simply storing bookings, the system will make allocation decisions based on predefined rules and constraints. We'll dedicate an entire module to this, including:

Allocation strategy (e.g., first-come-first-served, priority-based, or rotational).
Scheduling constraints (sub-canal capacity, village quotas, maintenance periods).
Conflict resolution when requests overlap.
Fairness rules to ensure upstream villages do not consume water needed by downstream villages.

That scheduling and allocation engine will become the core innovation of your project and something you can confidently discuss during technical interviews.