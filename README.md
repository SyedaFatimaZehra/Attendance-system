# Attendance System

This is an **ASP.NET** project designed for companies or organizations to efficiently manage employee attendance. The system provides functionalities to handle employee check-ins (Time In) and check-outs (Time Out) while ensuring data integrity.

## Features

- **Employee Attendance Management**: Employees can mark their attendance with a Time In and Time Out functionality.
- **Unique Record Handling**: 
  - Each employee is allowed to Time In only once until they have completed a Time Out.
  - If an employee attempts to Time In again without a prior Time Out, the system will display a warning message: 
    *"Time In already recorded. Please Time Out before attempting another Time In."*
- **Admin Dashboard**:
  - Admins can log in using their credentials to access the attendance dashboard.
  - Attendance records can be downloaded in **Excel format** for easy record-keeping and reporting.

## Technology Stack

- **Frontend**: ASP.NET
- **Backend**: C#
- **Database**: SQL Server
- **Export Format**: Excel

## How to Use

### For Employees:
1. **Time In**: Log in to your account and mark your Time In.
2. **Time Out**: After completing your work, log in to mark your Time Out.
3. **Warnings**: The system ensures that you can only Time In once until you have marked a Time Out.

### For Admins:
1. Log in using your admin credentials.
2. View all employee attendance records on the dashboard.
3. Download attendance records in Excel format for further processing or reporting.


   
