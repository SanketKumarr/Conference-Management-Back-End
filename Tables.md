# Users
- User_Id => int
- Name => string
- Email => string
- Password => string
- Designation => string
- IsAdmin => bool

`
    CREATE TABLE Users (
    User_Id INT PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    Email VARCHAR(255) NOT NULL,
    Password VARCHAR(255) NOT NULL,
    Designation VARCHAR(100),
    IsAdmin BIT DEFAULT 0 );
`

# Booking Hall
- Booking_Id => int
- Request_Id => string => Guid
- User_Id => int
- Hall_Name => string
- Hall_No => int
- Hall_Name => int
- Date => DateTime
- TimeSlot => DateTime
- Status => stringb