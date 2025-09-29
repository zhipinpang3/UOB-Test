# Library Management System (Web API)

## Title
library management system API.

### 1. Models / Entities
- Book
- Member
- Loan

### 2. Database Design
Included in `Script/create_tables.sql`

### 3. Endpoints
- **Book**
  - GET `/api/books`
  - GET `/api/books/{id}`
  - POST `/api/books`
  - PUT `/api/books/{id}`
  - DELETE `/api/books/{id}`

- **Member**
  - GET `/api/members`
  - GET `/api/members/{id}`
  - POST `/api/members`
  - PUT `/api/members/{id}`
  - DELETE `/api/members/{id}`

- **Loan**
  - POST `/api/loans` (Borrow Book)
  - POST `/api/loans/return/{bookId}` (Return Book)
  - GET `/api/loans/member/{memberId}` (Loans by Member)
  - GET `/api/loans/active` (Active Loans)

### 4. Implementation
- Built with C# Web API (.NET 8.0)
- Follows OOP principles (separate Models, Controllers, DbContext)
- Includes business logic for borrowing, returning, fine calculation.
